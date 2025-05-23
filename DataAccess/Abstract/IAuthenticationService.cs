using Entities.Concrete;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IAuthenticationService
    {
        Task<string> GetToken();

        Task<AuthRegion> GetRegion();

        Image byteArrayToImage();

        Task<List<Tag>> GetTag();

        Task<Tag> MobileTag();
    }
}
