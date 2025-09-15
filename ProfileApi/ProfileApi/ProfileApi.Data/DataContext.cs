using Microsoft.EntityFrameworkCore;
using ProfileApi.Countries;

namespace ProfileApi.Data;

public class DataContext:DbContext
{
    public DataContext(DbContextOptions<DataContext> options):base(options)
    {

    }

    public DbSet<Country> Countries  { get; set; }
}
