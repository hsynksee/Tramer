namespace SharedKernel.Abstractions
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Messages = new List<string>();
            Messages.Add("İşlem Başarılı");
        }

        public BaseResponse(string message = null)
        {
            Messages = new List<string>();

            if (string.IsNullOrEmpty(message))
                message = "İşlem Başarılı";

            Messages.Add(message);
        }

        public List<string> Messages { get; set; }
    }

    public class BaseResponse<T> : BaseResponse
    {
        public BaseResponse() : base() { }

        public BaseResponse(T data, string message = null) : base(message)
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}
