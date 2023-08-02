using desafioDotNet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace desafioDotNet.Context {
    public class RegisterContext : DbContext {

        public RegisterContext(DbContextOptions<RegisterContext> options) : base(options) {
            try {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator != null) {
                    if (!databaseCreator.CanConnect()) databaseCreator.Create();
                    if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    
        public DbSet<RegisterModel> Register { get; set; }

    }
}
