namespace aduaba.api.Models.Communication
{
    public abstract class BaseResponse
    {
        public bool success { get; set; }
        public string message { get; set; }

        public BaseResponse(bool success, string message)
        {
            this.success = success;
            this.message = message;
        }
    }
}