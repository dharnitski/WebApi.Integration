using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;

namespace WebApi.Code
{
    public class TestUserManager : UserManager<TestIdentityUser>
    {
        public TestUserManager(IUserStore<TestIdentityUser> store) : base(store)
        {
        }

        public override Task<TestIdentityUser> FindAsync(string userName, string password)
        {
            var result = new TestIdentityUser
            {
                Id = userName,
                UserName = "Dmitry"
            };

            var tcs = new TaskCompletionSource<TestIdentityUser>();
            tcs.SetResult(result);
            return tcs.Task;
        }
    }
}