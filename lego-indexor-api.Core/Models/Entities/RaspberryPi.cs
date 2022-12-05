using System;
using System.Collections.Generic;

namespace lego_indexor_api.Core.Models.Entities
{
    public partial class RaspberryPi
    {
        public int Id { get; set; }
        public string MacAddress { get; set; } = null!;
        public string IpAddress { get; set; } = null!;
        public int? UserId { get; set; }
        public string? Code { get; set; }

        public virtual User? User { get; set; }
    }
}
