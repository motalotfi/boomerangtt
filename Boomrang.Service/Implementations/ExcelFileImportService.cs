using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Boomrang.Service.Implementations
{
    public class ExcelFileImportService
    {
        public List<T> GetList<T>(ExcelWorksheet sheet)
        {
            List<T> list = new List<T>();

            //sheet.Cells[1, 1].Value = "CompanyId";
            sheet.Cells[1, 1].Value = "FirstName";
            sheet.Cells[1, 2].Value = "LastName";
            sheet.Cells[1, 3].Value = "Gender";
            sheet.Cells[1, 4].Value = "Education";
            sheet.Cells[1, 5].Value = "Major";
            sheet.Cells[1, 6].Value = "CityId";
            sheet.Cells[1, 7].Value = "CityTitle";
            sheet.Cells[1, 8].Value = "MobileNumber";
            sheet.Cells[1, 9].Value = "EmailAddress";
            sheet.Cells[1, 10].Value = "ProficiencyField1";
            sheet.Cells[1, 11].Value = "ProficiencyField2";
            sheet.Cells[1, 12].Value = "ProficiencyField3";
            sheet.Cells[1, 13].Value = "ActivityField1";
            sheet.Cells[1, 14].Value = "ActivityField2";
            sheet.Cells[1, 15].Value = "ActivityField3";
            sheet.Cells[1, 16].Value = "CompanyName";
            sheet.Cells[1, 17].Value = "UserPosition";
            sheet.Cells[1, 18].Value = "IsKnowledgeEnterprise";
            sheet.Cells[1, 19].Value = "PhoneNumber";
            sheet.Cells[1, 20].Value = "WebsiteAddress";
            sheet.Cells[1, 21].Value = "EstablishmentYear";
            sheet.Cells[1, 22].Value = "ManpowerCount";
            sheet.Cells[1, 23].Value = "CompanyImportantProduct1";
            sheet.Cells[1, 24].Value = "ProductKnowledgeField1";
            sheet.Cells[1, 25].Value = "ProductApplication1";
            sheet.Cells[1, 26].Value = "ProductDescription1";
            sheet.Cells[1, 27].Value = "CompanyImportantProduct2";
            sheet.Cells[1, 28].Value = "ProductKnowledgeField2";
            sheet.Cells[1, 29].Value = "ProductApplication2";
            sheet.Cells[1, 30].Value = "ProductDescription2";
            sheet.Cells[1, 31].Value = "CompanyImportantProduct3";
            sheet.Cells[1, 32].Value = "ProductKnowledgeField3";
            sheet.Cells[1, 33].Value = "ProductApplication3";
            sheet.Cells[1, 34].Value = "ProductDescription3";
            sheet.Cells[1, 35].Value = "DataImporter";
            sheet.Cells[1, 36].Value = "DataEntryDateTime";
            sheet.Cells[1, 37].Value = "UpdatedDataImporter";
            sheet.Cells[1, 38].Value = "DataUpdateDateTime";

            var columnInfo = Enumerable.Range(1, sheet.Dimension.Columns).ToList().Select(n =>
            new { Index = n, ColumnName = sheet.Cells[1, n].Value.ToString() });

            for (int row = 2; row < sheet.Dimension.Rows; row++)
            {
                T obj = (T)Activator.CreateInstance(typeof(T));

                foreach (var prop in typeof(T).GetProperties())
                {
                    int col = columnInfo.SingleOrDefault(c => c.ColumnName == prop.Name).Index;
                    var val = sheet.Cells[row, col].Value;
                    var propType = prop.PropertyType;
                    prop.SetValue(obj, Convert.ChangeType(val, propType));
                }
                list.Add(obj);
            }
            return list;

        }

    }

}
