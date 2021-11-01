using API.Context;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class AccountRolesRepository : GeneralRepository<MyContext, AccountRoles, int>
    {
        public AccountRolesRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
