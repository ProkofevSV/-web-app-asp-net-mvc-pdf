using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ToDoIdentity.Models;

namespace ToDoIdentity.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }

    public class ToDoContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ToDoIdentity.Models.Task> Tasks { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<DayOfWeek> DayOfWeeks { get; set; }
        public DbSet<PerformerImage> PerformerImages { get; set; }
        public DbSet<Performer> Performers { get; set; }


        public ToDoContext() : base("ToDoEntity")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Performer>().HasOptional(x => x.PerformerImage).WithRequired().WillCascadeOnDelete(true);
            base.OnModelCreating(modelBuilder);
        }

        public static ToDoContext Create()
        {
            return new ToDoContext();
        }
    }
}