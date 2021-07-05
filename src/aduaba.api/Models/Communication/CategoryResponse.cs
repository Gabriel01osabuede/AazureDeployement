using aduaba.api.Entities.ApplicationEntity;

namespace aduaba.api.Models.Communication
{
    public class CategoryResponse : BaseResponse
    {
        public Category category { get; private set; }

        private CategoryResponse(bool success, string message, Category category) : base(success, message)
        {
            this.category = category;
        }

        public CategoryResponse(Category category) : this(true, string.Empty, category) { }
        public CategoryResponse(string message): this(false,message,null){}
    }
}