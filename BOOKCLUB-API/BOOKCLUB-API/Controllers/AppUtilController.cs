using BOOKCLUB_API.IRepository;
using BOOKCLUB_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BOOKCLUB_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AppUtilController : ControllerBase
    {
        private readonly IAppUtill appUtill;
        private readonly IFirebaseValidate _ifirebase;
        public AppUtilController(IAppUtill user, IFirebaseValidate firebaseValidate)
        {
            appUtill = user;
            _ifirebase = firebaseValidate;
        }

        [HttpGet("getAllCategory")]
        public async Task<string> getAllCategory()
        {
            try
            {
                APIModel apiModel = new APIModel();
                var response = appUtill.getAllBookCategories();
                if (response.data != null)
                {
                    apiModel.data = response.data;
                    apiModel.status = "200";
                    apiModel.success = true;
                    apiModel.timeStamp = DateTime.Now.ToString();
                    apiModel.message = "success";
                    var output = JsonConvert.SerializeObject(apiModel);
                    return output;
                }
                else
                {
                    apiModel.data = null;
                    apiModel.status = "400";
                    apiModel.success = false;
                    apiModel.timeStamp = DateTime.Now.ToString();
                    apiModel.message = response.message;
                    var output = JsonConvert.SerializeObject(apiModel);
                    return output;
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
