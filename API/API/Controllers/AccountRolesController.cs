using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class AccountRolesController : BaseController<AccountRoles, AccountRolesRepository, int>
    {
        public AccountRolesController(AccountRolesRepository accountRolesRepository) : base(accountRolesRepository)
        {

        }
    }
}
