using System;
using System.Collections.Generic;

namespace aduaba.api.Entities.ApplicationEntity
{
    public class Category
    {
        public string categoryId { get; set; } = Guid.NewGuid().ToString();
        public string categoryImage { get; set; }
        public string categoryName { get; set; }
        public IList<Product> products { get; set; }
    }
}