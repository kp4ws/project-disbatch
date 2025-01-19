using Microsoft.EntityFrameworkCore;
using ProjectDisbatch.API.Models.Domain;

namespace ProjectDisbatch.API.Data
{
    public class ProjectDisbatchDbContext : DbContext
    {
        public ProjectDisbatchDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) //Passes dbContextOptions to base class
        {

        }

        //Represents collections in our database. When running EntityCoreMigrations, the following props will create tables inside the database
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectType> ProjectTypes { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
