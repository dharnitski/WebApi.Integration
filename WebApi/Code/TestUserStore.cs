using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace WebApi.Code
{
    public class TestUserStore : IUserStore<TestIdentityUser>, IUserPasswordStore<TestIdentityUser>
    {
        public void Dispose()
        {
            
        }

        public Task CreateAsync(TestIdentityUser user)
        {
            return Task.Factory.StartNew(DoNothig);
        }

        public Task UpdateAsync(TestIdentityUser user)
        {
            return Task.Factory.StartNew(DoNothig);
        }

        public Task DeleteAsync(TestIdentityUser user)
        {
            return Task.Factory.StartNew(DoNothig);
        }

        public Task<TestIdentityUser> FindByIdAsync(string userId)
        {
            var result = new TestIdentityUser
            {
                Id = userId,
                UserName = "Dmitry"
            };

            var tcs = new TaskCompletionSource<TestIdentityUser>();
            tcs.SetResult(result);
            return tcs.Task;
        }

        public Task<TestIdentityUser> FindByNameAsync(string userName)
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

        private void DoNothig()
        {
            return;
        }

        public Task SetPasswordHashAsync(TestIdentityUser user, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(TestIdentityUser user)
        {
            var result = "hash";

            var tcs = new TaskCompletionSource<string>();
            tcs.SetResult(result);
            return tcs.Task;
        }

        public Task<bool> HasPasswordAsync(TestIdentityUser user)
        {
            throw new NotImplementedException();
        }
    }
}