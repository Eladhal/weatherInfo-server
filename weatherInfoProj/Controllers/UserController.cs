using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using weatherInfoProj.Models;

namespace weatherInfoProj.Controllers
{
    public class UserController : Controller
    {
        [HttpPost("[action]")]
        public bool add(UserInfo userInfo)
        {
            // Add user to Db with empty cities
            return true;
        }
    }
}