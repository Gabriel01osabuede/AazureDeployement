using System.ComponentModel.DataAnnotations;

namespace aduaba.api.Resource
{
    public class AddCategoryResource
    {
        [Required]
        [MaxLength(30)]
        public string categoryName { get; set; }
        public string categoryImageFilePath { get; set; }
    }
}