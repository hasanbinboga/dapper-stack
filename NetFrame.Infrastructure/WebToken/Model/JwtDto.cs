namespace NetFrame.Infrastructure.WebToken
{
    public class JwtDto
    {
        public long Id { get; set; }
        public long? PipeUserId { get; set; }
        public int? PipeUserType { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string IdentityNo { get; set; } = string.Empty;
        public int? Gender { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public List<string> RoleGroups { get; set; } = new List<string>();
        public string Language { get; set; } = "en";
        public int UserType { get; set; }
        public string RowGuid { get; set; } = Guid.NewGuid().ToString();
        public int AssistantType { get; set; }
    }
}
