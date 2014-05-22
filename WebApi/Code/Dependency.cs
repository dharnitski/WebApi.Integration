using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Code
{
    public interface IDependency
    {
        int GetSomething();
    }

    public class Dependency: IDependency
    {
        public int GetSomething()
        {
            return 100;
        }
    }
}