using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BOOKCLUB_API.IRepository
{
   public interface IFirebaseValidate
    {
        Task<FirebaseAPIModel> validateToken(string token);
    }


    public class FirebaseAPIModel
    {
        public bool success { get { return _success; } set { _success = value; } }
        public string message { get; set; }
        public FirebaseAdmin.Auth.UserRecord data { get { return _data; } set { _data = value; } }
        public string status { get { return _status; } set { _status = value; } }
        public string timeStamp { get { return _timestamp; } set { _timestamp = value; } }

        string _status = "200";
        bool _success = false;
        dynamic _data = false;
        string _timestamp = DateTime.Now.ToString();
    }
}
