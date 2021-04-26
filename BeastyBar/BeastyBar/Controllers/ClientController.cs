using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using BeastyBarGameLogic.NetworkMessaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BeastyBar.Authentication;

namespace BeastyBar.Controllers
{

    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;

        private List<(string,string)> userManager = new List<(string, string)>();

        public ClientController(IJwtAuthenticationManager jwtAuthenticationManager)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [HttpPost]
        [Route("api/users/login")]
        public ActionResult<ApiResponse> CheckLogin([FromBody] User user)
        {
            if (!userManager.Contains((user.Username,user.Password)))
            {
                return NotFound();
            }

            var token = this.jwtAuthenticationManager.Authenticate(user.Username, user.Password);
            return Ok(new ApiResponse(true, "", userManager.IndexOf((user.Username, user.Password)), token));
        }

        [HttpPost]
        [Route("api/users/add")]
        public ActionResult<ApiResponse> AddPlayer([FromBody] User user)
        {
            userManager.Add((user.Username,user.Password));
            var token = this.jwtAuthenticationManager.Authenticate(user.Username, user.Password);
            return Ok(new ApiResponse(true, "", userManager.IndexOf((user.Username, user.Password)), token));
        }
    }
}
