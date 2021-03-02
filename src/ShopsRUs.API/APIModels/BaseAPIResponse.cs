using System;
namespace ShopsRUs.API.APIModels
{
    public class BaseAPIResponse<T>
    {
        public string ResponseMessage { get; set; }
        public string ResponseCode { get; set; }
        public T Result { get; set; }
    }

}
