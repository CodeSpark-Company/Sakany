namespace Sakany.Application.Features.User.Profiles.Commands.UpdateStudentProfile.DTOs
{
    public class UpdateStudentProfileCommandDTO
    {
        #region Properties

        public string? Email { get; set; }
        public string? UserName { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }
        public int? Age { get; set; }

        public string? Bio { get; set; }
        public string? ImageUrl { get; set; }
        public string? CivilId { get; set; }

        public string? UniversityId { get; set; }
        public string? University { get; set; }
        public string? College { get; set; }
        public int? Level { get; set; }

        public DateTime CreatedAt { get; set; }

        #endregion Properties
    }
}