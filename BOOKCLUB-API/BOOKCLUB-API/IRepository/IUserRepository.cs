using BOOKCLUB_API.Models;
using BOOKCLUB_API.SqlRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BOOKCLUB_API.IRepository
{
    public interface IUserRepository
    {
        APIModel GetUser(string UUID);

        APIModel getAllUsers();

        APIModel SignUpUser(AppUser appUser);

        ApiWrapper getProfileAttribs(string UID);

        string UpdateUser(AppUser appUser);

        ApiWrapper GetAllProfileAttributes(string UUID);

        ApiWrapper SaveAllProfileAttribs(string UID, List<ProfileAttribute> profileAttributes);
    }
}
