using System;
using System.Collections.Generic;

namespace lego_indexor_api.Core.Models.Entities
{
    public partial class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
