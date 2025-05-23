using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class AuthenticationManager : IAuthenticationService
    {
        HttpClient client = new HttpClient();

        private Token _token;
        private readonly ILogger<AuthenticationManager> _logger;

        public AuthenticationManager(Token token, ILogger<AuthenticationManager> logger)
        {
            _token = token;
            _logger = logger;
        }

        public async Task<string> GetToken()
        {
            try
            {
                User user = new User();
                user.username = "***";
                user.password = "***";
                user.key = "***";

                client.BaseAddress = new Uri("***");

                HttpResponseMessage response = await client.PostAsJsonAsync("api", user);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    AuthSuccess authSuccess = JsonConvert.DeserializeObject<AuthSuccess>(data);

                    _token.accessToken = authSuccess.result.accessToken;
                    _token.refreshToken = authSuccess.result.refreshToken;
                    return _token.accessToken;

                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception!" + ex.Message);
                throw;
            }

            return null;
        }

        public async Task<AuthRegion> GetRegion()
        {
            try
            {
                if (_token.accessToken == null)
                {
                    _token.accessToken = GetToken().Result;
                }

                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "API CONNECTION"))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token.accessToken);

                    var send = await client.SendAsync(requestMessage);
                    if (send.IsSuccessStatusCode)
                    {
                        var data = await send.Content.ReadAsStringAsync();

                        var regionSuccess = JsonConvert.DeserializeObject<RegionSuccess>(data);
                        AuthRegion region = regionSuccess.result[1];
                        var picture = regionSuccess.result[1].picture;
                        return region;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception!" + ex.Message);
                throw;
            }
            return null;

        }

        public Image byteArrayToImage()
        {
            try
            {
                byte[] result = GetRegion().Result.picture;
                MemoryStream ms = new MemoryStream(result);
                Image ret = Image.FromStream(ms);
                //ret.Save("output.png", ImageFormat.Png);
                return ret;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception!" + ex.Message);
                throw;
            }
        }

        public async Task<List<Tag>> GetTag()
        {
            try
            {
                if (_token.accessToken == null)
                {
                    _token.accessToken = GetToken().Result;
                }

                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "API CONNECTION"))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token.accessToken);

                    var send = await client.SendAsync(requestMessage);
                    if (send.IsSuccessStatusCode)
                    {
                        var data = await send.Content.ReadAsStringAsync();
                        //dynamic tag = JObject.Parse(data);

                        var tag = JsonConvert.DeserializeObject<TagSuccess>(data);

                        var tagList = new List<Tag>();

                        foreach (var item in tag.result)
                        {
                            if (item.region_ID == 3 && item.type == "S")
                            {
                                tagList.Add(item);
                            }
                        }
                        return tagList;

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception!" + ex.Message);
                throw;
            }
            return null;
        }

        public async Task<Tag> MobileTag()
        {
            try
            {
                if (_token.accessToken == null)
                {
                    _token.accessToken = GetToken().Result;
                }

                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "API CONNECTION"))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token.accessToken);

                    var send = await client.SendAsync(requestMessage);
                    if (send.IsSuccessStatusCode)
                    {
                        var data = await send.Content.ReadAsStringAsync();

                        var tag = JsonConvert.DeserializeObject<TagSuccess>(data);

                        return tag.result[0];
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception!" + ex.Message);
                throw;
            }
            return null;
        }
    }
}
