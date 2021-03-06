﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FleetApi1.Models;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace FleetApi1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CompaniesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompaniesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Clients
        [HttpPost]
        public JsonResult Names()
        {
           string[] companies= _context.Companies.Select(c => c.Name).ToArray();
            return Json(new { companies });
            
        }


        }
}
