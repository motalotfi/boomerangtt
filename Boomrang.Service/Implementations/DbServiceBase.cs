using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Dapper;
using Boomrang.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Boomrang.Service.Implementations
{
    public class DbServiceBase
    {
        public IConfiguration Configuration { get; set; }
        private readonly string BoomrangConnectionStringName = "AppConnectionString";
        public BoomrangDbContext DbContext { get; set; }
        private async Task<List<T>> RunQueryAsync<T>(
            string connectionString,
            string query,
            object paramContainer,
            ParamTableInfo[] paramTables = null,
            CommandType commandType = CommandType.Text,
            SqlTransaction transaction = null)
        {
            await using var connection = new SqlConnection(connectionString);
            connection.Open();
            var parameters = new DynamicParameters();
            parameters.AddDynamicParams((paramContainer));

            if (paramTables is not null)
            {
                foreach (var paramTable in paramTables)
                {
                    var dataTable = ToDataTable(paramTable.ParameterValue);
                    parameters.Add(paramTable.ParameterName, dataTable.AsTableValuedParameter(paramTable.DbType));
                }
            }

            var queryStream = await connection.QueryAsync<T>(query, parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
            var result = queryStream.ToList();
            return result;
        }

        private async Task ExecuteStoredProcedureAsync(string connectionString, string query, object paramContainer, ParamTableInfo[] paramTables = null, CommandType commandType = CommandType.Text, SqlTransaction transaction = null)
        {
            await using var connection = new SqlConnection(connectionString);
            connection.Open();
            var parameters = new DynamicParameters();
            parameters.AddDynamicParams((paramContainer));

            if (paramTables is not null)
            {
                foreach (var paramTable in paramTables)
                {
                    parameters.Add(paramTable.ParameterName, ToDataTable(paramTable.ParameterValue).AsTableValuedParameter(paramTable.DbType));
                }
            }

            await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }

        public IQueryable<T> SqlFunction<T>(FormattableString functionCall) where T : class
        {
            var formattableFunctionCall =
                FormattableStringFactory.Create($"SELECT * from {functionCall.Format}", functionCall.GetArguments());
            return DbContext.Set<T>().FromSqlInterpolated(formattableFunctionCall);
        }

        public async Task<List<T>> QueryStoredProcedureAsync<T>(string spName, object paramContainer = null,
            ParamTableInfo[] paramTables = null, SqlTransaction transaction = null,
            string connectionName = null)
        {
            if (string.IsNullOrWhiteSpace(connectionName))
            {
                connectionName = BoomrangConnectionStringName;
            }
            var connectionString = Configuration.GetConnectionString(connectionName);
            return await RunQueryAsync<T>(connectionString, spName, paramContainer, paramTables, commandType: CommandType.StoredProcedure, transaction: transaction);
        }

        public async Task ExecuteStoredProcedureAsync(string spName, object paramContainer = null,
            ParamTableInfo[] paramTables = null, SqlTransaction transaction = null,
            string connectionName = null)
        {
            if (string.IsNullOrWhiteSpace(connectionName))
            {
                connectionName = BoomrangConnectionStringName;
            }
            var connectionString = Configuration.GetConnectionString(connectionName);
            await ExecuteStoredProcedureAsync(connectionString, spName, paramContainer, paramTables, commandType: CommandType.StoredProcedure, transaction: transaction);
        }

        private DataTable ToDataTable(IList items)
        {
            var rows = items;
            if (rows is null || !rows.Any())
            {
                return null;
            }

            DataTable dataTable = new DataTable();
            var properties = rows[0].GetType().GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                var property = properties[i];
                Type type = property.PropertyType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    type = Nullable.GetUnderlyingType(type);

                dataTable.Columns.Add(property.Name, type);
            }
            object[] values = new object[properties.Length];
            foreach (var item in rows)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = properties[i].GetValue(item);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        public static IDictionary<string, object> ToDictionary(object values)
        {
            var dict = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            if (values != null)
            {
                foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(values))
                {
                    object obj = propertyDescriptor.GetValue(values);
                    dict.Add(propertyDescriptor.Name, obj);
                }
            }

            return dict;
        }

    }

    public class ParamTableInfo
    {
        public ParamTableInfo()
        {

        }

        public ParamTableInfo(string name, IList value, string type)
        {
            ParameterName = name;
            ParameterValue = value;
            DbType = type;
        }
        public string ParameterName { get; set; }
        public string DbType { get; set; }
        public IList ParameterValue { get; set; }
    }
}