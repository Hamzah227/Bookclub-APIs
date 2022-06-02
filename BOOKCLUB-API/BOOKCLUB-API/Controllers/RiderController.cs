using BOOKCLUB_API.IRepository;
using BOOKCLUB_API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BOOKCLUB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RiderController : ControllerBase
    {
        private readonly IRider rider_;
        public RiderController(IRider rider)
        {
            rider_ = rider;
        }

        [HttpPost("SignUp")]
        public async Task<string> signUpAsync([FromBody] JObject json)
        {
            try
            {      
                APIModel apiModel = new APIModel();
                if (1 == 1)
                {
                    var userData = JsonConvert.DeserializeObject<Rider>(json.ToString());
                    Rider user = new Rider();

                    user.FirstName = userData.FirstName;
                    user.ProfileImageUrl = userData.ProfileImageUrl;
                    user.Email = userData.Email;
                    user.Phone = userData.Phone;
                    user.RoleId = userData.RoleId;
                    user.IsActive = true;
                    user.UserName = userData.UserName;
                    user.Password = userData.Password;
                    user.LastName = userData.LastName;
                   

                    var response = rider_.SignUpRider(user);
                    var output = JsonConvert.SerializeObject(response);
                    return output;

                }
                else
                {
                    apiModel.data = false;
                    apiModel.status = "400";
                    apiModel.success = false;
                    apiModel.timeStamp = DateTime.Now.ToString();
                    apiModel.message = "Token Expired";
                    var output = JsonConvert.SerializeObject(apiModel);
                    return output;
                }

            }
            catch (Exception ex)
            {
                APIModel apiModel = new APIModel();
                apiModel.data = false;
                apiModel.status = "500";
                apiModel.success = false;
                apiModel.timeStamp = DateTime.Now.ToString();
                apiModel.message = ex.Message;
                var output = JsonConvert.SerializeObject(apiModel);
                return output;
            }
        }
        [HttpGet("Login")]
        public async Task<string> loginAsync(string username, string password)
        {
            var apiModel = new APIModel();
            try
            {
                //var riderData = JsonConvert.DeserializeObject<Rider>(json.ToString());
                var response = rider_.LoginRider(username, password);
                var output = JsonConvert.SerializeObject(response);
                return output;
            }
            catch (Exception ex)
            {
                apiModel.data = false;
                apiModel.status = "500";
                apiModel.success = false;
                apiModel.timeStamp = DateTime.Now.ToString();
                apiModel.message = ex.Message;
                var output = JsonConvert.SerializeObject(apiModel);
                return output;
            }
        }
        [HttpGet("GetAllRiders")]
        public APIModel GetAll()
        {
            var apiModel = new APIModel();
            try
            {
                apiModel = rider_.GetAllRiders();
                return apiModel;
            }
            catch (Exception e)
            {
                apiModel.data = false;
                apiModel.status = "500";
                apiModel.success = false;
                apiModel.timeStamp = DateTime.Now.ToString();
                apiModel.message = e.Message;
                return apiModel;
            }
        }
        [HttpGet("GetRequestDeliveries")]
        public APIModel GetRequestDeliveries(int status)
        {
            APIModel apiModel = new APIModel();
            try
            {
                apiModel = rider_.GetAllRequestDelivery(status);
                return apiModel;
            }
            catch (Exception e)
            {
                apiModel.data = e.Message;
                apiModel.message = "Exception!";
                apiModel.status = "500";
                apiModel.success = true;
                apiModel.timeStamp = DateTime.Now.ToString();
                return apiModel;
            }
        }
        [HttpPost("AcceptDeliveryRequest")]
        public APIModel AcceptRequest([FromBody] JObject json)
        {
            APIModel api = new APIModel();
            try
            {
                if (json != null)
                {
                    var request = JsonConvert.DeserializeObject<RiderAcceptRequest>(json.ToString());
                    api = rider_.AcceptDeliveryRequest(request);
                }
                return api;
            }
            catch (Exception e)
            {
                api.data = e.Message;
                api.message = "Exception!";
                api.status = "500";
                api.success = true;
                api.timeStamp = DateTime.Now.ToString();
                return api;
            }
        }

        [HttpPost("StartJob")]
        public APIModel StartJob([FromBody] JObject json)
        {
            APIModel api = new APIModel();
            string temp = "{\r\n  \"RiderId\": 0,\r\n  \"RequestDeliveryId\": 0,\r\n  \"RiderLocationName\": null,\r\n  \"RiderLocation\": " +
                "{\r\n  \"Latitude\": 0.0,\r\n  \"Longitude\": 0.0\r\n}}\r\n";
            json = JObject.Parse(temp);
            try
            {
                if (json != null)
                {
                    StartJobBody body = json.ToObject<StartJobBody>();
                    LocationCoordinates riderCooridnates = new LocationCoordinates();
                    riderCooridnates.Latitude = (double)body.RiderLocation.Latitude;
                    riderCooridnates.Longitude = (double)body.RiderLocation.Longitude;
                    int reqDeliveryId = (int)body.RequestDeliveryId;

                    var returnRequest = rider_.StartJob(body);

                    return api;
                }
                api.success = false;
                api.status = 400.ToString();
                api.message = "Invalid Request";
                api.data = null;
                api.timeStamp = DateTime.Now.ToString();
                return api;
            }
            catch (Exception e)
            {
                api.success = false;
                api.status = 500.ToString();
                api.message = e.Message;
                api.data = e.Data;
                api.timeStamp = DateTime.Now.ToString();
                return api;
            }
        }

    }
}
