
using System.Collections.Generic;

namespace Kong.OnlineStoreAPI.Model
{
    public class ApiResponse
    {
        public ApiResponse()
        {
            ErrorList = new List<Error>();
        }

        public bool Success { get; set; }

        public List<Error> ErrorList { get; set; }
    }

    public class Error
    {
        public string Message { get; set; }
        public string Code { get; set; }
        public string PropertyName { get; set; }
    }
}
