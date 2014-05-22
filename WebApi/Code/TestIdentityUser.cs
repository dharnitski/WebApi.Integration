using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace WebApi.Code
{
    public class TestIdentityUser: IUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
    }
}