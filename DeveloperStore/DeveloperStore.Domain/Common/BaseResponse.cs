namespace DeveloperStore.Domain.Common
{
    public class BaseResponse<T> where T : class
    {
        private BaseResponse()
        {
            
        }
        public T? Data { get; protected set; }
        public bool IsSuccess { get; protected set; }
        public string Erro { get; protected set; } = string.Empty;
        public string TypeErro { get; protected set; } = string.Empty;
        public string Detail { get; protected set; } = string.Empty;

        public static BaseResponse<T> New(T data)
        {
            return new()
            {
                Data = data,
                IsSuccess = true,
            };
        }


        public static BaseResponse<T> NewErro(string erro, string typeErro, string detail)
        {
            return new()
            {
                IsSuccess = false,
                Erro = erro,
                TypeErro = typeErro,
                Detail = detail,
            };
        }

    }
}
