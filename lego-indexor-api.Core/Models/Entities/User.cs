using System;
using System.Collections.Generic;

namespace lego_indexor_api.Core.Models.Entities
{
    public partial class User
    {
        public User()
        {
            Connections = new HashSet<Connection>();
            Pieces = new HashSet<Piece>();
            Raspberrypis = new HashSet<Raspberrypi>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public byte[]? Password { get; set; }

        public virtual ICollection<Connection> Connections { get; set; }
        public virtual ICollection<Piece> Pieces { get; set; }
        public virtual ICollection<Raspberrypi> Raspberrypis { get; set; }
    }
}
