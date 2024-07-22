namespace Sakany.Domain.Entities.Users
{
    public class StudentProfile : UserProfile
    {
        #region Properties

        public string? UniversityId { get; set; }
        public string? Unviersity { get; set; }
        public string? College { get; set; }
        public int? Level { get; set; }

        #endregion Properties
    }
}