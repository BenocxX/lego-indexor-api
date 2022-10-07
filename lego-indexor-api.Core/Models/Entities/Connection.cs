using System;
using System.Collections.Generic;

namespace lego_indexor_api.Core.Models.Entities
{
    public partial class Connection
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}
