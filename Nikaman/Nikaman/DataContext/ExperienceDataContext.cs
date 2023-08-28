using Microsoft.EntityFrameworkCore;
using Nikaman.Models;
using System;

namespace Nikaman.DataContext
{
    public class ExperienceDataContext:DbContext
    {
        public DbSet<Exp> Exps { get; set; }
        public ExperienceDataContext(DbContextOptions<ExperienceDataContext> options) : base(options)
        {

        }
        public ExperienceDataContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=DESKTOP-AU6MOVM\\MSSQLSERVER01;Database=Experience;Trusted_Connection=true;Encrypt=False;");
            optionsBuilder.UseSqlite("Data Source=C:\\\\Users\\1\\Desktop\\C#\\ASP NET\\Nikaman\\Db.db");
        }
    }
}
