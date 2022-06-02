using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BOOKCLUB_API.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace BOOKCLUB_API.Controllers.Helper
{
    public class FirebaseValidator : ControllerBase , IFirebaseValidate
    {
        public async Task<FirebaseAPIModel> validateToken(string Incomingtoken)
        {
            try
            {
                var auth = FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance;
             //   string accessTokenWithBearerPrefix = Request.Headers[HeaderNames.Authorization];
                string accessTokenWithoutBearerPrefix = Incomingtoken.Substring("Bearer ".Length);
                var token = await auth.VerifyIdTokenAsync(accessTokenWithoutBearerPrefix);
               

                FirebaseAPIModel apiModel = new FirebaseAPIModel();

                if (token == null)
                {
                    apiModel.status = "400";
                    apiModel.message = "Token Expired";
                    apiModel.success = false;

                }
                else
                {
                    var userfirebase = await auth.GetUserByEmailAsync(token.Claims["email"].ToString());
                    apiModel.success = true;
                    apiModel.data = userfirebase;
                }

                return apiModel;
            }
            catch (Exception ae)
            {
                FirebaseAPIModel apiModel = new FirebaseAPIModel();
                apiModel.status = "400";
                apiModel.message = ae.Message;

                return apiModel;
            }

           
        }
    }
}
