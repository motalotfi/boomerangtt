using Boomrang.Model.Dto;
using Boomrang.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Boomrang.Data
{
    public class BoomrangDbContext: DbContext
    {
        public BoomrangDbContext(DbContextOptions<BoomrangDbContext> options)
            : base(options)
        {
        }
        public BoomrangDbContext() : base(new DbContextOptionsBuilder<BoomrangDbContext>()
                                        .UseSqlServer(BoomrangConfigurationProvider.GetConfiguration().GetConnectionString("AppConnectionString"), sqlServerOptions => sqlServerOptions.CommandTimeout(20 * 60)).Options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var userDefinedTypes =
                from type in typeof(IDbDto).Assembly.GetExportedTypes()
                where type.IsAssignableTo(typeof(IDbDto)) &&
                      type.IsClass &&
                      !type.IsAbstract &&
                      !type.IsInterface
                select type;

            foreach (var userDefinedType in userDefinedTypes)
            {
                modelBuilder.Entity(userDefinedType);
            }
        }
    }
}