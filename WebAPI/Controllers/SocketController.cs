using DataAccess.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocketController : ControllerBase
    {
        SocketManager _socketManager;

        public SocketController(SocketManager socketManager)
        {
            _socketManager = socketManager;
        }

        [HttpPost("SocketClient")]
        public string Client(string hostname, int port, Location[] location)
        {
            return _socketManager.StartClient(hostname, port, location);
        }
    }
}