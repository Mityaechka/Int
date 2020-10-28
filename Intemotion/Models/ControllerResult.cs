using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.Models
{
    public class ControllerResult<T>:IActionResult 
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public object Data { get; set; }
        public ControllerResult(ServiceResult<T> result)
        {
            IsSuccess = result.IsSuccess;
            ErrorMessage = result.ErrorMessage;
            Data = result.Data;
        }


        public async  Task ExecuteResultAsync(ActionContext context)
        {
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            var json =  JsonConvert.SerializeObject(new { IsSuccess,ErrorMessage,Data}, serializerSettings);

            await context.HttpContext.Response.WriteAsync(json);
        }
    }
    public class ObjectControllerResult : ControllerResult<object>
    {
        public ObjectControllerResult(ServiceResult<object> result) : base(result)
        {
        }
    }
}
