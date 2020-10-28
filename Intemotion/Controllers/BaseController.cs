using Intemotion.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.Controllers
{
    public class BaseController:Controller
    {
        [NonAction]
        public ControllerResult<T> Result<T>(ServiceResult<T> result) 
        {
            return new ControllerResult<T>(result);
        }
        [NonAction]
        public ObjectControllerResult Result(ServiceResult result) 
        {
            return new ObjectControllerResult(result);
        }
    }
}
