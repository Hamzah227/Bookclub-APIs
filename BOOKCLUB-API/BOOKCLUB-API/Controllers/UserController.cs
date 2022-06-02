using BOOKCLUB_API.IRepository;
using BOOKCLUB_API.Models;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BOOKCLUB_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _iuserRepository;
        private readonly IFirebaseValidate _ifirebase;
        public UserController(IUserRepository user, IFirebaseValidate firebaseValidate)
        {
            _iuserRepository = user;
            _ifirebase = firebaseValidate;
        }

        [HttpPost("SignUp")]
        public async Task<string> signUpAsync([FromBody] JObject json)
        {
            try
            {

                var authsuccess = await _ifirebase.validateToken(Request.Headers[HeaderNames.Authorization]);
                APIModel apiModel = new APIModel();
                if (authsuccess.success)
                {
                    //var userData = JsonConvert.DeserializeObject<AppUser>(json.ToString());
                    AppUser user = new AppUser();

                    user.FirstName = authsuccess.data.DisplayName;
                    user.Uid = authsuccess.data.Uid;
                    user.ProfileImageUrl = authsuccess.data.PhotoUrl;
                    var providerData = authsuccess.data.ProviderData[0];
                    user.Provider = authsuccess.data.ProviderData.First().ProviderId;
                   
                    user.Email = authsuccess.data.Email;
                    user.Phone = authsuccess.data.PhoneNumber;
                    user.RoleId = "Ialv6ngV/r0t3R+DqOafdQ=="; //userData.RoleId;
                    user.IsActive = true;

                    var response = _iuserRepository.SignUpUser(user);
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
                apiModel.status = "400";
                apiModel.success = false;
                apiModel.timeStamp = DateTime.Now.ToString();
                apiModel.message = ex.Message;
                var output = JsonConvert.SerializeObject(apiModel);
                return output;
            }


        }

        [HttpGet("authenticate")]
        public async Task<string> authenticate()
        {
            try
            {
                APIModel apiModel = new APIModel();
                var authsuccess = await _ifirebase.validateToken(Request.Headers[HeaderNames.Authorization]);
                if (authsuccess.success)
                {
                    var response = _iuserRepository.GetUser(authsuccess.data.Uid);

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
                APIModel apiModel = new APIModel();
                apiModel.data = false;
                apiModel.status = "400";
                apiModel.success = false;
                apiModel.timeStamp = DateTime.Now.ToString();
                apiModel.message = ex.Message;
                var output = JsonConvert.SerializeObject(apiModel);
                return output;
            }


        }

        [HttpGet("getProfileAttribs")]
        public async Task<string> getProfileAttribs()
        {
            var auth = FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance;
            try
            {
                string accessTokenWithBearerPrefix = Request.Headers[HeaderNames.Authorization];
                string accessTokenWithoutBearerPrefix = accessTokenWithBearerPrefix.Substring("Bearer ".Length);
                var token = await auth.VerifyIdTokenAsync(accessTokenWithoutBearerPrefix);
                var userfirebase = await auth.GetUserByEmailAsync(token.Claims["email"].ToString());

                APIModel apiModel = new APIModel();
                if (token != null)
                {
                    var response = _iuserRepository.getProfileAttribs(userfirebase.Uid);

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
                    apiModel.status = "500";
                    apiModel.success = false;
                    apiModel.timeStamp = DateTime.Now.ToString();
                    apiModel.message = "Token not verified";
                    var output = JsonConvert.SerializeObject(apiModel);
                    return output;
                }
            }
            catch (Exception ex)
            {
                APIModel apiModel = new APIModel();
                apiModel.data = false;
                apiModel.status = "400";
                apiModel.success = false;
                apiModel.timeStamp = DateTime.Now.ToString();
                apiModel.message = ex.Message;
                var output = JsonConvert.SerializeObject(apiModel);
                return output;
            }
        }

        [HttpPost("Update")]
        public async Task<string> UpdateUser([FromBody] JObject json)
        {
            var auth = FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance;
            try
            {
                string accessTokenWithBearerPrefix = Request.Headers[HeaderNames.Authorization];
                string accessTokenWithoutBearerPrefix = accessTokenWithBearerPrefix.Substring("Bearer ".Length);
                var token = await auth.VerifyIdTokenAsync(accessTokenWithoutBearerPrefix);
                var userfirebase = await auth.GetUserByEmailAsync(token.Claims["email"].ToString());

                APIModel apiModel = new APIModel();
                if (token != null)
                {
                    string updateUser = json.ToString();
                    AppUser appUser = JsonConvert.DeserializeObject<AppUser>(updateUser);
                    var response = _iuserRepository.UpdateUser(appUser);

                    if (response == "success")
                    {
                        apiModel.data = true;
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
                        apiModel.message = response;
                        var output = JsonConvert.SerializeObject(apiModel);
                        return output;
                    }
                }
                else
                {
                    apiModel.data = true;
                    apiModel.status = "500";
                    apiModel.success = false;
                    apiModel.timeStamp = DateTime.Now.ToString();
                    apiModel.message = "Token not verified";
                    var output = JsonConvert.SerializeObject(apiModel);
                    return output;
                }
            }
            catch (Exception ex)
            {
                APIModel apiModel = new APIModel();
                apiModel.data = false;
                apiModel.status = "400";
                apiModel.success = false;
                apiModel.timeStamp = DateTime.Now.ToString();
                apiModel.message = ex.Message;
                var output = JsonConvert.SerializeObject(apiModel);
                return output;
            }

        }

        [HttpPost("SaveProfileAttribs")]
        public async Task<string> SaveProfileAttribs([FromBody] JArray json)
        {
            var auth = FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance;
            try
            {
                string accessTokenWithBearerPrefix = Request.Headers[HeaderNames.Authorization];
                string accessTokenWithoutBearerPrefix = accessTokenWithBearerPrefix.Substring("Bearer ".Length);
                var token = await auth.VerifyIdTokenAsync(accessTokenWithoutBearerPrefix);
                var userfirebase = await auth.GetUserByEmailAsync(token.Claims["email"].ToString());

                APIModel apiModel = new APIModel();

                if (token != null)
                {
                    string updateUser = json.ToString();
                    List<ProfileAttribute> atribs = JsonConvert.DeserializeObject<List<ProfileAttribute>>(updateUser);
                    var response = _iuserRepository.SaveAllProfileAttribs(userfirebase.Uid, atribs);

                    if (response.message == "success")
                    {
                        apiModel.data = true;
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
                    apiModel.data = false;
                    apiModel.status = "400";
                    apiModel.success = false;
                    apiModel.timeStamp = DateTime.Now.ToString();
                    apiModel.message = "Token Expired";
                    var output = JsonConvert.SerializeObject(apiModel);
                    return output;
                }

            }
            catch (Exception ae)
            {
                APIModel apiModel = new APIModel();
                apiModel.data = false;
                apiModel.status = "400";
                apiModel.success = false;
                apiModel.timeStamp = DateTime.Now.ToString();
                apiModel.message = ae.Message;
                var output = JsonConvert.SerializeObject(apiModel);
                return output;
            }

        }

        [HttpGet("GetAllUsers")]
        public async Task<string> getAllUsers()
        {
            try
            {
                APIModel apiModel = new APIModel();
                var authsuccess = await _ifirebase.validateToken(Request.Headers[HeaderNames.Authorization]);
                if (authsuccess.success)
                {
                    var response = _iuserRepository.getAllUsers();

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
                APIModel apiModel = new APIModel();
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
