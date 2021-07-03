using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace aduaba.api.Entities.ApplicationEntity
{
    public class Product
    {
        public string ProductId { get; set; } = Guid.NewGuid().ToString();
        public string ProductImageUrlPath { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ProductAmount { get; set; }
        public bool ProductAvailabilty { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string CategoryId { get; set; }
        public Category Category { get; set; }
    }
}