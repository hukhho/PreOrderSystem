﻿namespace PreOrderPlatform.Entity.Models
{
    public partial class Role
    {
        public Role()
        {
            Id = Guid.NewGuid();
            Users = new HashSet<User>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
