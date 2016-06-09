using MockingExampleSolution.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UIApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IAccountService _accountService;
        public HomeController(IClientService clientService, IAccountService accountService)
        {
            _clientService = clientService;
            _accountService = accountService;
        }

        // GET: Home
        public ActionResult Index()
        {
            var interest = _accountService.CapitalizeInterestForClientAccount(1,1);
            return View();
        }
    }
}