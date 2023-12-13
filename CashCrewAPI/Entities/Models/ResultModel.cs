using System;
namespace Entities.Models
{
    public class ResultModel<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        

        public ResultModel(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }

}

