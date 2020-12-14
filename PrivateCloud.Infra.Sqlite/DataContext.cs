using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PrivateCloud.Infra.Sqlite.Dto.Users;

namespace PrivateCloud.Infra.Sqlite
{
    public class DataContext :
        DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(
            IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(
            DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlite(Configuration.GetConnectionString("ApiDatabase"));
        }

        public DbSet<UserDto> Users { get; set; }
    }
}
