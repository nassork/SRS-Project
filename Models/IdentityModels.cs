using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace SRSWepApp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        //create application user
        public string UserFullName { get; set; }

        //create roles

        public virtual string GetUserDetails()
        {
            string userDetails = UserFullName;

            userDetails += "is in roles";

            ApplicationDbContext db = new ApplicationDbContext();

            IdentityRole role;

            foreach(IdentityUserRole eachRole in this.Roles)
            {
                role = db.Roles.Find(eachRole.RoleId);
                userDetails += role.Name + ' ';
            }

            return userDetails;
        }

        public ApplicationUser()
        { }
        
        public ApplicationUser(string userFullName, string email, string phoneNumber)
        {
            PasswordHasher hasher = new PasswordHasher();

            this.UserFullName = userFullName;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            //required
            this.SecurityStamp = Guid.NewGuid().ToString();
            this.PasswordHash = hasher.HashPassword(userFullName); //makes password the userFullName
            this.UserName = email; //making email the username
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<CourseOffering> CourseOfferings { get; set; }
         
        public DbSet<CourseSignUp> CourseSignUps { get; set; }
    
    }
}