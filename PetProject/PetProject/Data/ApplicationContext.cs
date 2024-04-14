using Microsoft.EntityFrameworkCore;
using PetProject.Models;

namespace PetProject.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<ToDo> ToDos => Set<ToDo>();
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
