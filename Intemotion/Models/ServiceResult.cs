using Intemotion.Entities.Rounds.SecondRound;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Intemotion.Models
{
    public class ServiceResult<T>
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
        public ServiceResult<U> Convert<U>(Func<T, U> converter) where U : class
        {
            U u = default;
            if (Data != null)
                u = converter(Data);
            return new ServiceResult<U> { Data = u, ErrorMessage = ErrorMessage, IsSuccess = IsSuccess };
        }
        public static ServiceResult<T> Error(string message)
        {
            return new ServiceResult<T> { Data = default, IsSuccess = false, ErrorMessage = message };
        }
        public static ServiceResult<T> Success(T data)
        {
            return new ServiceResult<T> { Data = data, IsSuccess = true, ErrorMessage = null };
        }
        public ServiceResult<object> ToObject()
        {
            return new ServiceResult<object>
            {
                IsSuccess = IsSuccess,
                Data = (object)Data,
                ErrorMessage = ErrorMessage
            };
        }
    }
    public class ServiceResult : ServiceResult<object>
    {
    }
}
