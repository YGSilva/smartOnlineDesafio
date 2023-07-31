using desafioDotNet.Models;
using Microsoft.EntityFrameworkCore;

namespace desafioDotNet.Context {
    public class RegisterContext : DbContext {

        public RegisterContext(DbContextOptions<RegisterContext> options) : base(options ) {}
    
        public DbSet<RegisterModel> Register { get; set; }

    }
}
