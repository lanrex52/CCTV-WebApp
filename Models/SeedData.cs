using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCTV_App.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CCTV_AppContext(
              serviceProvider.GetRequiredService<DbContextOptions<CCTV_AppContext>>()) )
            {
                if (context.Users.Any())
                {
                    return;
                }
                var passwordHasher = new PasswordHasher<string>();
                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User
                        {
                            FirstName = "Admin",
                            LastName = "Admin",
                            Username = "Admin",
                            Password = passwordHasher.HashPassword(null, "admin"),
                            ConfirmPassword = passwordHasher.HashPassword(null, "admin"),
                            Email = "admin@lotusbetaanalytics.com",
                            Role = "admin",
                            MobileNo = "000000000000",
                            Location= "System",
                            CreatedBy = "System",
                            CreatedDate = DateTime.Now



                }
                        );
                    context.SaveChanges();
                }

            }

        }
        }
}
