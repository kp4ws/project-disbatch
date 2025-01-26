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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed data for Project Types
            //Engineering Project, Geoscience Project, Rocketry Project

            var projectTypes = new List<ProjectType>
            {
                new ProjectType()
                {
                    Id = Guid.Parse("c1b2b207-d7cc-4fff-a637-0fcac3aa7dce"),
                    Name = "Engineering"
                },

                new ProjectType()
                {
                    Id = Guid.Parse("b0770281-e108-4329-83a4-513cd3146eca"),
                    Name = "Geoscience"
                },

                new ProjectType()
                {
                    Id = Guid.Parse("fb07258c-7bd6-4905-8b66-a5fef02fae07"),
                    Name = "Rocketry"
                }
            };

            //Seed project types to the database
            modelBuilder.Entity<ProjectType>().HasData(projectTypes);


            //Seed data for Departments
            var departments = new List<Department>
            {
                new Department()
                {
                    Id = Guid.Parse("bb7376ed-d846-4a13-962f-0e5f6e7370c5"),
                    Code = "1A",
                    Name = "New Inventions",
                    Description = "Responsible for taking on new inventions"
                },

                new Department()
                {
                    Id = Guid.Parse("994e60b1-778b-4f76-b9f0-ee15c91dd63e"),
                    Code = "1B",
                    Name = "Experimental",
                    Description = "Responsible for experimental design"
                },

                new Department()
                {
                    Id = Guid.Parse("fe4846ac-0060-4cb1-a2ba-74270ad8055c"),
                    Code = "1C",
                    Name = "Research",
                    Description = "Responsible for research"
                }
            };

            //Seed departments into database
            modelBuilder.Entity<Department>().HasData(departments);


            //Seed data for Projects
            var projects = new List<Project>
            {
                new Project()
                {
                    Id = Guid.Parse("9a9cb972-3f9f-4e78-8459-bd52a0b2853a"),
                    Name = "Project 1",
                    Description = "",
                    DepartmentId = Guid.Parse("fe4846ac-0060-4cb1-a2ba-74270ad8055c"),
                    ProjectTypeId = Guid.Parse("c1b2b207-d7cc-4fff-a637-0fcac3aa7dce")

                },

                new Project()
                {
                    Id = Guid.Parse("6b452b93-d55f-41f7-8cd3-d20f9726372d"),
                    Name = "Project 2",
                    Description = "",
                    DepartmentId = Guid.Parse("994e60b1-778b-4f76-b9f0-ee15c91dd63e"),
                    ProjectTypeId = Guid.Parse("c1b2b207-d7cc-4fff-a637-0fcac3aa7dce")

                },

                new Project()
                {
                    Id = Guid.Parse("ffc3920f-1e35-41dd-9f19-465019868fa3"),
                    Name = "Project 3",
                    Description = "",
                    DepartmentId = Guid.Parse("bb7376ed-d846-4a13-962f-0e5f6e7370c5"),
                    ProjectTypeId = Guid.Parse("b0770281-e108-4329-83a4-513cd3146eca")

                }
            };

            //Seed projects into database
            modelBuilder.Entity<Project>().HasData(projects);
        }
    }
}
