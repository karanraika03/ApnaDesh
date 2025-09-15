using ApiProject3.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication1.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
    }
}
