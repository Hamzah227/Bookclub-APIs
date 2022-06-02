using BOOKCLUB_API.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BOOKCLUB_API.WrapperClasses;
using BOOKCLUB_API.Models;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;

namespace BOOKCLUB_API.Controllers
{
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequest request_;
        private readonly IFirebaseValidate _ifirebase;
        public RequestController(IRequest request, IFirebaseValidate firebaseValidate)
        {
            request_ = request;
            _ifirebase = firebaseValidate;
        }

        [HttpPost("SaveRequest")]
        public async Task<string> SaveRequest([FromBody] JObject json)
        {
            APIModel apiModel = new APIModel();
            try
            {
                FirebaseAPIModel auth = null; ;
                if (_ifirebase is not null) {
                    auth = await _ifirebase.validateToken(Request.Headers[HeaderNames.Authorization]);
                }

                if (auth.success)
                {
                    var requestData = JsonConvert.DeserializeObject<RequestWrapperClass>(json.ToString());
                    requestData.Uid = auth.data.Uid;
                    var response = request_.SaveRequest(requestData);

                    if (response.data != null && response.success == true)
                    {
                        var output = JsonConvert.SerializeObject(response);
                        return output;
                    }
                    else
                    {
                        var output = JsonConvert.SerializeObject(response);
                        return output;
                    }
               
                }
                else {
                    apiModel.data = true;
                    apiModel.status = "400";
                    apiModel.success = false;
                    apiModel.timeStamp = DateTime.Now.ToString();
                    apiModel.message = "Token not verified";
                    var output = JsonConvert.SerializeObject(apiModel);
                    return output;
                } 
            }
            catch (Exception ex)
            {
                apiModel.data = false;
                apiModel.status = "400";
                apiModel.success = false;
                apiModel.timeStamp = DateTime.Now.ToString();
                apiModel.message = ex.Message;
                var output = JsonConvert.SerializeObject(apiModel);
                return output;
            }
        }

        [HttpPost("SaveBid")]
        public async Task<string> BidRequest([FromBody] JObject json)
        {
            APIModel apiModel = new APIModel();
            try
            {
                var auth = await _ifirebase.validateToken(Request.Headers[HeaderNames.Authorization]);
                if (auth.success)
                {
                    var requestData = JsonConvert.DeserializeObject<BidsWrapperClass>(json.ToString());
                    requestData.Uid = auth.data.Uid;

                    var response = request_.SaveBid(requestData);
                    var output = JsonConvert.SerializeObject(response);
                    return output;

                } else
                {
                    apiModel.data = true;
                    apiModel.status = "401";
                    apiModel.success = false;
                    apiModel.timeStamp = DateTime.Now.ToString();
                    apiModel.message = "Token not verified";
                    var output = JsonConvert.SerializeObject(apiModel);
                    return output;
                }
               
            }
            catch (Exception ex)
            {
               
                apiModel.data = false;
                apiModel.status = "400";
                apiModel.success = false;
                apiModel.timeStamp = DateTime.Now.ToString();
                apiModel.message = ex.Message;
                var output = JsonConvert.SerializeObject(apiModel);
                return output;
            }
        }

        [HttpGet("GetRequest")]
        public async Task<string> GetRequest(bool myAds)
        {
            //  var auth = FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance;
            APIModel apiModel = new APIModel();
            try
            {
                var auth = await _ifirebase.validateToken(Request.Headers[HeaderNames.Authorization]);
                if (auth.success)
                {
                    var response = request_.GetAllRequest(auth.data.Uid, myAds);

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
                else
                {
                    apiModel.data = true;
                    apiModel.status = "401";
                    apiModel.success = false;
                    apiModel.timeStamp = DateTime.Now.ToString();
                    apiModel.message = "Token not verified";
                    var output = JsonConvert.SerializeObject(apiModel);
                    return output;
                }
            }
            catch (Exception ex)
            {
                
                apiModel.data = false;
                apiModel.status = "400";
                apiModel.success = false;
                apiModel.timeStamp = DateTime.Now.ToString();
                apiModel.message = ex.Message;
                var output = JsonConvert.SerializeObject(apiModel);
                return output;
            }
        }

        [HttpPost("ApproveBid")]
        public async Task<string> ApproveBid(string Id)
        {
            APIModel apiModel = new APIModel();
            try
            {
                var auth = await _ifirebase.validateToken(Request.Headers[HeaderNames.Authorization]);
                if (auth.success)
                {
                    var response = request_.ApproveBid(Convert.ToInt32(Id));
                    return JsonConvert.SerializeObject(response);
                    #region Old
                    //if (response.data != null)
                    //{
                    //    //apiModel.data = response.data;
                    //    //apiModel.status = "200";
                    //    //apiModel.success = true;
                    //    //apiModel.timeStamp = DateTime.Now.ToString();
                    //    //apiModel.message = "success";
                    //    //var output = JsonConvert.SerializeObject(apiModel);
                    //    return JsonConvert.SerializeObject(response);
                    //}
                    //else
                    //{
                    //    apiModel.data = null;
                    //    apiModel.status = "400";
                    //    apiModel.success = false;
                    //    apiModel.timeStamp = DateTime.Now.ToString();
                    //    apiModel.message = response.message;
                    //    var output = JsonConvert.SerializeObject(apiModel);
                    //    return output;
                    //}
                    #endregion
                }
                else
                {
                    apiModel.data = true;
                    apiModel.status = "401";
                    apiModel.success = false;
                    apiModel.timeStamp = DateTime.Now.ToString();
                    apiModel.message = "Token not verified";
                    return JsonConvert.SerializeObject(apiModel);
                }
            }
            catch (Exception ex)
            {
                apiModel.data = false;
                apiModel.status = "400";
                apiModel.success = false;
                apiModel.timeStamp = DateTime.Now.ToString();
                apiModel.message = ex.Message;
                return JsonConvert.SerializeObject(apiModel);
            }
        }

        [HttpPost("RejectBid")]
        public async Task<string> RejectBid(string Id)
        {
            APIModel apiModel = new APIModel();
            try
            {
                var auth = await _ifirebase.validateToken(Request.Headers[HeaderNames.Authorization]);
                if (auth.success)
                {
                    var response = request_.RejectBid(Convert.ToInt32(Id));

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
                else
                {
                    apiModel.data = true;
                    apiModel.status = "401";
                    apiModel.success = false;
                    apiModel.timeStamp = DateTime.Now.ToString();
                    apiModel.message = "Token not verified";
                    var output = JsonConvert.SerializeObject(apiModel);
                    return output;
                }
            }
            catch (Exception ex)
            {
                apiModel.data = false;
                apiModel.status = "400";
                apiModel.success = false;
                apiModel.timeStamp = DateTime.Now.ToString();
                apiModel.message = ex.Message;
                var output = JsonConvert.SerializeObject(apiModel);
                return output;
            }
        }

        [HttpGet("GetBids")]
        public async Task<string> GetBids()
        {
            APIModel apiModel = new APIModel();
            try
            {
                var auth = await _ifirebase.validateToken(Request.Headers[HeaderNames.Authorization]);
                if (auth.success)
                {
                    var response = request_.GetBids(auth.data.Uid);
                    
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
                else
                {
                    apiModel.data = true;
                    apiModel.status = "401";
                    apiModel.success = false;
                    apiModel.timeStamp = DateTime.Now.ToString();
                    apiModel.message = "Token not verified";
                    var output = JsonConvert.SerializeObject(apiModel);
                    return output;
                }
            }
            catch (Exception ex)
            {
                apiModel.data = false;
                apiModel.status = "400";
                apiModel.success = false;
                apiModel.timeStamp = DateTime.Now.ToString();
                apiModel.message = ex.Message;
                var output = JsonConvert.SerializeObject(apiModel);
                return output;
            }
        }

        [HttpGet("GetAllRequests")]
        public async Task<string> GetAdminRequest()
        {
            //  var auth = FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance;
            APIModel apiModel = new APIModel();
            try
            {
                var auth = await _ifirebase.validateToken(Request.Headers[HeaderNames.Authorization]);
                if (auth.success)
                {
                    var response = request_.GetAllRequestForAdmin();

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
                else
                {
                    apiModel.data = true;
                    apiModel.status = "401";
                    apiModel.success = false;
                    apiModel.timeStamp = DateTime.Now.ToString();
                    apiModel.message = "Token not verified";
                    var output = JsonConvert.SerializeObject(apiModel);
                    return output;
                }
            }
            catch (Exception ex)
            {

                apiModel.data = false;
                apiModel.status = "400";
                apiModel.success = false;
                apiModel.timeStamp = DateTime.Now.ToString();
                apiModel.message = ex.Message;
                var output = JsonConvert.SerializeObject(apiModel);
                return output;
            }
        }
    }
}
