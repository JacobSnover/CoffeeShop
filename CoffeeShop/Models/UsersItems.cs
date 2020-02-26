using System;
using System.Collections.Generic;

namespace CoffeeShop.Models
{
    public partial class UsersItems
    {
        public int UserItemsId { get; set; }
        public int UsersId { get; set; }
        public int ItemsId { get; set; }

        public virtual Items Items { get; set; }
        public virtual Users Users { get; set; }
    }
}
