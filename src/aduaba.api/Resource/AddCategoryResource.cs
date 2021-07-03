using System.ComponentModel.DataAnnotations;

namespace aduaba.api.Resource
{
    public class AddCategoryResource
    {
        [Required]
        [MaxLength(30)]
        public string CategoryName { get; set; }
        public string CategoryImageFilePath { get; set; }
    }
}