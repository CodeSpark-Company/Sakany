using Microsoft.AspNetCore.Identity;
using Sakany.Domain.Entities.Users;
using Sakany.Domain.Enumerations.Users;
using Sakany.Domain.IdentityEntities;

namespace Sakany.Persistence.DataSeeding.Security.Users
{
    public static class UsersDataSeeding
    {
        private static async Task InitializeSuperAdminsAsync(this UserManager<ApplicationUser> userManager)
        {
            var superAdmin = new ApplicationUser()
            {
                UserName = $"superadmin",
                Email = $"superadmin@sakany.com",
                EmailConfirmed = true,
                PhoneNumber = "+201234567890",
                FirstName = $"SuperAdmin",
                LastName = $"SuperAdmin",
                BirthDate = DateOnly.FromDateTime(DateTime.UtcNow).AddYears(-20),
                UserProfile = new SuperAdminProfile()
                {
                    Bio = "Loading...",
                    ImageUrl = "http://google.com",
                    CivilId = "http://google.com",
                }
            };

            await userManager.CreateAsync(superAdmin, "P@ssw0rd");
            await userManager.AddToRoleAsync(superAdmin, UserRole.SuperAdmin.ToString());
        }

        private static async Task InitializeAdminsAsync(this UserManager<ApplicationUser> userManager)
        {
            for (int i = 1; i <= 20; i++)
            {
                var admin = new ApplicationUser()
                {
                    UserName = $"admin{i}",
                    Email = $"admin{i}@sakany.com",
                    EmailConfirmed = true,
                    PhoneNumber = "+201234567890",
                    FirstName = $"Admin{i}",
                    LastName = $"Admin{i}",
                    BirthDate = DateOnly.FromDateTime(DateTime.UtcNow).AddYears(-20),
                    UserProfile = new AdminProfile()
                    {
                        Bio = "Loading...",
                        ImageUrl = "http://google.com",
                        CivilId = "http://google.com",
                    }
                };

                await userManager.CreateAsync(admin, "P@ssw0rd");
                await userManager.AddToRoleAsync(admin, UserRole.Admin.ToString());
            }
        }

        private static async Task InitializeRealtorsAsync(this UserManager<ApplicationUser> userManager)
        {
            for (int i = 1; i <= 20; i++)
            {
                var realtor = new ApplicationUser()
                {
                    UserName = $"realtor{i}",
                    Email = $"realtor{i}@sakany.com",
                    EmailConfirmed = true,
                    PhoneNumber = "+201234567890",
                    FirstName = $"Realtor{i}",
                    LastName = $"Realtor{i}",
                    BirthDate = DateOnly.FromDateTime(DateTime.UtcNow).AddYears(-20),
                    UserProfile = new RealtorProfile()
                    {
                        Bio = "Loading...",
                        ImageUrl = "http://google.com",
                        CivilId = "http://google.com",
                        RealEstateContract = "http://google.com",
                    }
                };

                await userManager.CreateAsync(realtor, "P@ssw0rd");
                await userManager.AddToRoleAsync(realtor, UserRole.Realtor.ToString());
            }
        }

        private static async Task InitializeStudentsAsync(this UserManager<ApplicationUser> userManager)
        {
            for (int i = 1; i <= 20; i++)
            {
                var student = new ApplicationUser()
                {
                    UserName = $"student{i}",
                    Email = $"student{i}@sakany.com",
                    EmailConfirmed = true,
                    PhoneNumber = "+201234567890",
                    FirstName = $"Student{i}",
                    LastName = $"Student{i}",
                    BirthDate = DateOnly.FromDateTime(DateTime.UtcNow).AddYears(-20),
                    UserProfile = new StudentProfile()
                    {
                        Bio = "Loading...",
                        ImageUrl = "http://google.com",
                        CivilId = "http://google.com",
                        UniversityId = "http://google.com",
                        Unviersity = "Sohag",
                        College = "Computers and AI",
                        Level = 4
                    }
                };

                await userManager.CreateAsync(student, "P@ssw0rd");
                await userManager.AddToRoleAsync(student, UserRole.Student.ToString());
            }
        }

        public static async Task InitializeUsersDataSeedingAsync(this UserManager<ApplicationUser> userManager)
        {
            await userManager.InitializeAdminsAsync();
            await userManager.InitializeSuperAdminsAsync();
            await userManager.InitializeRealtorsAsync();
            await userManager.InitializeStudentsAsync();
        }
    }
}