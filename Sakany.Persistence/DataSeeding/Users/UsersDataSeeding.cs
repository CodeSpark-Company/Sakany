using Microsoft.AspNetCore.Identity;
using Sakany.Application.Interfaces.UnitOfWork;
using Sakany.Domain.Entities.Users;
using Sakany.Domain.Enumerations.Users;
using Sakany.Domain.IdentityEntities;

namespace Sakany.Persistence.DataSeeding.Security.Users
{
    public static class UsersDataSeeding
    {
        private static async Task InitializeSuperAdminsAsync(this UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            var superAdmin = new ApplicationUser()
            {
                Email = "superadmin@sakany.com",
                UserName = "superadmin",
                EmailConfirmed = true,
            };

            await userManager.CreateAsync(superAdmin, "P@ssw0rd");
            await unitOfWork.Repository<SuperAdminProfile>().AddAsync(new SuperAdminProfile() { UserId = superAdmin.Id, Bio = string.Empty });
            await userManager.AddToRoleAsync(superAdmin, UserRole.SuperAdmin.ToString());
        }

        private static async Task InitializeAdminsAsync(this UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            for (int i = 1; i <= 20; i++)
            {
                var admin = new ApplicationUser()
                {
                    UserName = $"admin{i}",
                    Email = $"admin{i}@sakany.com",
                    EmailConfirmed = true,
                };

                await userManager.CreateAsync(admin, "P@ssw0rd");
                await unitOfWork.Repository<AdminProfile>().AddAsync(new AdminProfile() { UserId = admin.Id, Bio = string.Empty });
                await userManager.AddToRoleAsync(admin, UserRole.Admin.ToString());
            }
        }

        private static async Task InitializeRealtorsAsync(this UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            for (int i = 1; i <= 20; i++)
            {
                var realtor = new ApplicationUser()
                {
                    UserName = $"realtor{i}",
                    Email = $"realtor{i}@sakany.com",
                    EmailConfirmed = true,
                };

                await userManager.CreateAsync(realtor, "P@ssw0rd");
                await unitOfWork.Repository<RealtorProfile>().AddAsync(new RealtorProfile() { UserId = realtor.Id, Bio = string.Empty });
                await userManager.AddToRoleAsync(realtor, UserRole.Realtor.ToString());
            }
        }

        private static async Task InitializeStudentsAsync(this UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            for (int i = 1; i <= 20; i++)
            {
                var student = new ApplicationUser()
                {
                    UserName = $"student{i}",
                    Email = $"student{i}@sakany.com",
                    EmailConfirmed = true,
                };

                await userManager.CreateAsync(student, "P@ssw0rd");
                await unitOfWork.Repository<StudentProfile>().AddAsync(new StudentProfile() { UserId = student.Id, Bio = string.Empty });
                await userManager.AddToRoleAsync(student, UserRole.Student.ToString());
            }
        }

        public static async Task InitializeUsersDataSeedingAsync(this UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            await userManager.InitializeAdminsAsync(unitOfWork);
            await userManager.InitializeSuperAdminsAsync(unitOfWork);
            await userManager.InitializeRealtorsAsync(unitOfWork);
            await userManager.InitializeStudentsAsync(unitOfWork);
        }
    }
}