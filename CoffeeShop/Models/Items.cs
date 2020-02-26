using System;
using System.Collections.Generic;

namespace CoffeeShop.Models
{
    public partial class Items
    {
        public Items()
        {
            UsersItems = new HashSet<UsersItems>();
        }

        public int id { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }   

        public virtual ICollection<UsersItems> UsersItems { get; set; }
    }   
}
