namespace ITeam.DotnetCore.Models
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string UserName { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
    }
}
