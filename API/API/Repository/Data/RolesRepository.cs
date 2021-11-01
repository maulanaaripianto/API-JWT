using API.Context;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class RolesRepository : GeneralRepository<MyContext, Roles, int>
    {
        public RolesRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
