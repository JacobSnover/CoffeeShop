using System;
using System.Collections.Generic;

namespace CoffeeShop.Models
{
    public partial class Users
    {
        public Users()
        {
            UsersItems = new HashSet<UsersItems>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UsersItems> UsersItems { get; set; }
    }
}
