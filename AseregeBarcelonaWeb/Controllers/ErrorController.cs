using AseregeBarcelonaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace AseregeBarcelonaWeb.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet("NotFound")]
        public IActionResult NotFound()
        {
            return View();
        }
    }
}
