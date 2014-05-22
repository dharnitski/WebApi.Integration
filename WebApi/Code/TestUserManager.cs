using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace WebApi.Code
{
    public class TestUserManager : UserManager<TestIdentityUser>
    {
        public TestUserManager(IUserStore<TestIdentityUser> store) : base(store)
        {
        }


    }
}