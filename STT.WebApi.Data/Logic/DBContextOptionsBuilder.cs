using Microsoft.EntityFrameworkCore;

namespace STT.WebApi.Data.Logic
{
    public static class DBContextOptionsBuilder
    {
        public static DbContextOptions<FootballDBContext> GetOptions(string connectionstring)
        {
            var options = new DbContextOptionsBuilder<FootballDBContext>();
            options.UseMySql(connectionstring);
            return options.Options;
        }
    }
}
