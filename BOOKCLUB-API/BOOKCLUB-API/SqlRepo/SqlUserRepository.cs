using BOOKCLUB_API.IRepository;
using BOOKCLUB_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BOOKCLUB_API.SqlRepo
{
    public class SqlUserRepository : IUserRepository
    {

        private readonly BOOKCLUBFYPContext _mContext;

        public SqlUserRepository(BOOKCLUBFYPContext mContext)
        {
            this._mContext = mContext;
        }

        public ApiWrapper GetAllProfileAttributes(string UUID)
        {
            throw new NotImplementedException();
        }

        public APIModel getAllUsers()
        {
            APIModel apiModel = new APIModel();
            try
            {

                var user = _mContext.AppUsers.ToList();
                if (user != null)
                {
                    apiModel.data = user;
                    apiModel.message = "User fetched successfully";
                    apiModel.status = "200";
                    apiModel.success = true;
                    apiModel.timeStamp = DateTime.Now.ToString();
                    return apiModel;
                }
                else
                {
                    apiModel.data = null;
                    apiModel.message = "Not found";
                    apiModel.status = "500";
                    apiModel.success = false;
                    apiModel.timeStamp = DateTime.Now.ToString();
                    return apiModel;

                }

            }
            catch (Exception ae)
            {
                apiModel.data = null;
                apiModel.message = ae.Message;
                apiModel.status = "500";
                apiModel.success = false;
                apiModel.timeStamp = DateTime.Now.ToString();
                return apiModel;
            }
        }

        public ApiWrapper getProfileAttribs(string UID)
        {
            throw new NotImplementedException();
        }

        public APIModel GetUser(string UUID)
        {
            APIModel apiModel = new APIModel();
            try
            {
               
                var user = _mContext.AppUsers.Where(user => user.Uid == UUID).SingleOrDefault();
                if (user != null)
                {
                    apiModel.data = user;
                    apiModel.message = "User saved successfully";
                    apiModel.status = "200";
                    apiModel.success = true;
                    apiModel.timeStamp = DateTime.Now.ToString();
                    return apiModel;
                }
                else
                {
                    apiModel.data = null;
                    apiModel.message = "Not found";
                    apiModel.status = "500";
                    apiModel.success = false;
                    apiModel.timeStamp = DateTime.Now.ToString();
                    return apiModel;

                }

            }
            catch (Exception ae)
            {
                apiModel.data = null;
                apiModel.message = ae.Message;
                apiModel.status = "500";
                apiModel.success = false;
                apiModel.timeStamp = DateTime.Now.ToString();
                return apiModel;
            }
        }

        public ApiWrapper SaveAllProfileAttribs(string UID, List<ProfileAttribute> profileAttributes)
        {
            throw new NotImplementedException();
        }

        public APIModel SignUpUser(AppUser appUser)
        {
            APIModel apiModel = new APIModel();

            try
            {
                var user = _mContext.AppUsers.Where(x => x.Uid == appUser.Uid).SingleOrDefault();
                if (user == null )
                {
                    _mContext.AddRange(appUser);
                    _mContext.SaveChanges();

                    apiModel.data = appUser;
                    apiModel.message = "User saved successfully";
                    apiModel.status = "200";
                    apiModel.success = true;
                    apiModel.timeStamp = DateTime.Now.ToString();

                    return apiModel;
                } else
                {
                    apiModel.data = null;
                    apiModel.message = "User already exist";
                    apiModel.status = "500";
                    apiModel.success = false;
                    apiModel.timeStamp = DateTime.Now.ToString();

                    return apiModel;
                }
              


            }
            catch (Exception ae)
            {
                apiModel.data = null;
                apiModel.message = ae.Message;
                apiModel.status = "500";
                apiModel.success = false;
                apiModel.timeStamp = DateTime.Now.ToString();
                return apiModel;
            }

        }

        public string UpdateUser(AppUser appUser)
        {
            try
            {

                var updateuser = _mContext.AppUsers.Attach(appUser);
                updateuser.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _mContext.SaveChanges();
                return "success";
            }
            catch (Exception ae)
            {
                return ae.Message;
            }
        }
    }

    public class ApiWrapper
    {
        public dynamic data { get; set; }
        public string message { get; set; }
    }
}
