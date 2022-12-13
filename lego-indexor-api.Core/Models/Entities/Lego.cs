using System;
using System.Collections.Generic;

namespace lego_indexor_api.Core.Models.Entities
{
    public partial class Lego
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Count { get; set; }
    }
}
