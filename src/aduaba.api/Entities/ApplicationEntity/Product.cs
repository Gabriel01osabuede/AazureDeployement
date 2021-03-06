using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace aduaba.api.Entities.ApplicationEntity
{
    public class Product
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string productImageUrlPath { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        [Column(TypeName = "Money")]
        public decimal productAmount { get; set; }
        public string ManufactureName { get; set; }
        public bool productAvailabilty { get; set; } = true;
        public DateTime createdDate { get; set; } = DateTime.Now;

        public Category Category { get; set; }
        public string CategoryId { get; set; }

    }
}