using System;
using System.Collections.Generic;

namespace aduaba.data.Entities.ApplicationEntity
{
    public class Category
    {
        public string CategoryId { get; set; } = Guid.NewGuid().ToString();
        public string CategoryImage { get; set; }
        public string CategoryName { get; set; }
        public IList<Product> Products { get; set; }
    }
}