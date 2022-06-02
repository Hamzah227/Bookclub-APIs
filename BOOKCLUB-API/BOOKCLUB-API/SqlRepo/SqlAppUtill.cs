using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BOOKCLUB_API.IRepository;
using BOOKCLUB_API.Models;

namespace BOOKCLUB_API.SqlRepo
{
    public class SqlAppUtill : IAppUtill
    {
        private readonly BOOKCLUBFYPContext _mContext;

        public SqlAppUtill(BOOKCLUBFYPContext mContext)
        {
            this._mContext = mContext;
        }

        public ApiWrapper getAllBookCategories()
        {
            try
            {
                ApiWrapper apiWrapper = new ApiWrapper();
                var user = _mContext.Categories.Where(x => x.IsActive == true).ToList();
                if (user != null)
                {
                    apiWrapper.data = user;
                    apiWrapper.message = "";
                    return apiWrapper;
                }
                else
                {
                    apiWrapper.data = null;
                    apiWrapper.message = "Not found";
                    return apiWrapper;
                }
            }
            catch (Exception ae)
            {
                ApiWrapper api = new ApiWrapper();
                api.data = null;
                api.message = ae.Message;
                return api;
            }
        }
    }
}
