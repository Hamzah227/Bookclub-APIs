using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BOOKCLUB_API.Models;
using BOOKCLUB_API.SqlRepo;

namespace BOOKCLUB_API.IRepository
{
   public interface IAppUtill
    {
        ApiWrapper getAllBookCategories();
    }
}
