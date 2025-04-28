using System.ComponentModel.DataAnnotations.Schema;

namespace AuthApi.Models
{
    [Table("tbl_user")]
    public class User
    {
        [Column("id")]
        public string Id { get; set; } = string.Empty;

        [Column("email")] // correção - gitO email é o login do usuário
        public string Email { get; set; } = string.Empty;

        [Column("password")]
        public string Password { get; set; } = string.Empty;

        [Column("role")]
        public string Role { get; set; } = string.Empty;

        // Tudo abaixo é ignorado no banco:

        [NotMapped]
        public bool Enabled { get; set; }

        [NotMapped]
        public string? FirstName { get; set; }

        [NotMapped]
        public string? JobTitle { get; set; }

        [NotMapped]
        public string? LastName { get; set; }

        [NotMapped]
        public string? MiddleName { get; set; }

        [NotMapped]
        public string? Telephone { get; set; }

        [NotMapped]
        public string? ProfileImageId { get; set; }

        [NotMapped]
        public string? LanguageId { get; set; }

        [NotMapped]
        public string? TenantId { get; set; }

        [NotMapped]
        public long? LegacyId { get; set; }

        [NotMapped]
        public bool? FirstAccess { get; set; }

        [NotMapped]
        public string CalendarView { get; set; } = string.Empty;

        [NotMapped]
        public string Name
        {
            get
            {
                var parts = new List<string>();
                if (!string.IsNullOrWhiteSpace(FirstName)) parts.Add(FirstName);
                if (!string.IsNullOrWhiteSpace(MiddleName)) parts.Add(MiddleName);
                if (!string.IsNullOrWhiteSpace(LastName)) parts.Add(LastName);
                return string.Join(" ", parts).Trim();
            }
        }
    }
}