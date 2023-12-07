using System;
using System.Collections.Generic;

namespace codecamp_efcore_dbfirst.Entities
{
    public partial class User
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public int Role { get; set; }
        public string? PasswordHash { get; set; }
    }
}
