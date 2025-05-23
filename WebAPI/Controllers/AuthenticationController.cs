using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Net.Http.Headers;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        AuthenticationManager _authenticationManager;

        public AuthenticationController(AuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }


        [HttpPost]
        public async Task<string> AuthController()
        {
            return _authenticationManager.GetToken().Result;  
        }

        [HttpGet("Region")]
        public async Task<AuthRegion> Get()
        {
    
            return _authenticationManager.GetRegion().Result;

        }

        [HttpGet("Image")]
        public Image byteArrayToImage()
        {
            return _authenticationManager.byteArrayToImage();
        }

        [HttpGet("Tag")]
        public async Task<List<Tag>> getTag()
        {
            return _authenticationManager.GetTag().Result;
        }

        [HttpGet("MobileTag")]
        public async Task<Tag> mobileTag()
        {
            return _authenticationManager.MobileTag().Result;
        }

    }
}