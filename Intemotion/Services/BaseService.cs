using Intemotion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.Services
{
    public class BaseService
    {
        public ServiceResult<T> Error<T>(string message) 
        {
            return ServiceResult<T>.Error(message);
        }
        public ServiceResult<T> Success<T>(T data =default)
        {
            return ServiceResult<T>.Success(data);
        }
        public ServiceResult Error(string message) 
        {
            var result = ServiceResult.Error(message);

            return new ServiceResult { Data = result.Data, ErrorMessage = result.ErrorMessage, IsSuccess = result.IsSuccess };
        }
        public ServiceResult Success(object data = null) 
        {
            var result =  ServiceResult.Success(data);

            return new ServiceResult { Data = result.Data, ErrorMessage = result.ErrorMessage, IsSuccess = result.IsSuccess };
        }
    }
}
