using Entities.Concrete;
using Microsoft.Extensions.Logging;
using NLog;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DataAccess.Concrete
{
    public class CoordinateManager
    {
        AuthenticationManager _authenticationManager;
        List<MobileTagWithCoordinates> result = new List<MobileTagWithCoordinates>();
        List<DistancesFromStabilTags> distances = new List<DistancesFromStabilTags>();
        List<MessageType44FWModel> messages = new List<MessageType44FWModel>();
        public int speed = 10;
        private object message;
        private readonly ILogger<CoordinateManager> _logger;

        public CoordinateManager(AuthenticationManager authenticationManager, ILogger<CoordinateManager> logger)
        {
            _authenticationManager = authenticationManager;
            _logger = logger;
        }

        public async Task<List<Location>> LocationGenerator(Location[] location)
        {
            List<Location> list = new List<Location>();
            int i;

            try
            {
                for (i = 0; i < (location.Length - 1); i++)
                {
                    Location firstLocation = new Location();
                    firstLocation.Id = list.Count + 1;
                    firstLocation.Lat = location[i].Lat;
                    firstLocation.Lng = location[i].Lng;
                    list.Add(firstLocation);

                    double differenceLat = location[i + 1].Lat - location[i].Lat;
                    double differenceLng = location[i + 1].Lng - location[i].Lng;
                    double hypotenuse = Math.Sqrt(Math.Pow(differenceLng, 2) + Math.Pow(differenceLat, 2));

                    double increaseLat = (differenceLat * speed) / hypotenuse;
                    double increaseLng = (differenceLng * speed) / hypotenuse;
                    Location tempLocation = new Location();
                    tempLocation = location[i];
                    for (int j = 0; j < hypotenuse; j += speed)
                    {
                        Location newLocation = new Location();
                        newLocation.Id = list.Count + 1;
                        newLocation.Lat = tempLocation.Lat + increaseLat;
                        newLocation.Lng = tempLocation.Lng + increaseLng;

                        tempLocation.Lat = newLocation.Lat;
                        tempLocation.Lng = newLocation.Lng;

                        list.Add(newLocation);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception!" + ex.Message);
                throw;
            }

            Location lastLocation = new Location();
            lastLocation.Id = list.Count + 1;
            lastLocation.Lat = location[i].Lat;
            lastLocation.Lng = location[i].Lng;
            list.Add(lastLocation);
            return list;
        }

        public async Task<List<DistancesFromStabilTags>> Distance(Location[] location)
        {
            try
            {
                var locations = await LocationGenerator(location);
                var mobileTags = await _authenticationManager.MobileTag();
                var tag = await _authenticationManager.GetTag();
                foreach (var item in locations)
                {
                    foreach (var stabilTag in tag)
                    {
                        MobileTagWithCoordinates coordinates = new MobileTagWithCoordinates
                        {
                            Id = item.Id,
                            InterpolatedId = item.Id,
                            Longitude = item.Lng,
                            Latitude = item.Lat,
                            Altitude = mobileTags.altitude
                        };
                        result.Add(coordinates);

                        double hypotenuse = Math.Sqrt((Math.Pow(item.Lng - Convert.ToDouble(stabilTag.longitude), 2)) +
                            (Math.Pow(item.Lat - Convert.ToDouble(stabilTag.latitude), 2)));
                        double tagAltitude = stabilTag.altitude;
                        double altitude = tagAltitude - coordinates.Altitude;
                        double distance = Math.Sqrt((altitude * altitude) + (hypotenuse * hypotenuse));

                        DistancesFromStabilTags distancesFrom = new DistancesFromStabilTags
                        {
                            Id = item.Id,
                            InterpolatedId = item.Id,
                            Longitude = Convert.ToDouble(item.Lng),
                            Latitude = Convert.ToDouble(item.Lat),
                            Altitude = Convert.ToDouble(mobileTags.altitude),
                            Distance = distance
                        };
                        distances.Add(distancesFrom);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception!" + ex.Message);
                throw;
            }
            return distances;
        }

        public async Task<List<MessageType44FWModel>> MessageFormat(Location[] location)
        {
            try
            {
                _logger.LogInformation("Info");
                var locations = await LocationGenerator(location);
                var mobileTags = await _authenticationManager.MobileTag();
                var tag = await _authenticationManager.GetTag();
                var twrPeriod = 5;
                var k = 0;
                foreach (var item in locations)
                {
                    foreach (var stabilTag in tag)
                    {
                        MobileTagWithCoordinates coordinates = new MobileTagWithCoordinates
                        {
                            Id = item.Id,
                            InterpolatedId = item.Id,
                            Longitude = item.Lng,
                            Latitude = item.Lat,
                            Altitude = mobileTags.altitude
                        };
                        result.Add(coordinates);

                        double hypotenuse = Math.Sqrt((Math.Pow(item.Lng - Convert.ToDouble(stabilTag.longitude), 2)) +
                            (Math.Pow(item.Lat - Convert.ToDouble(stabilTag.latitude), 2)));
                        double tagAltitude = stabilTag.altitude;
                        double altitude = tagAltitude - coordinates.Altitude;
                        double distance = Math.Sqrt((altitude * altitude) + (hypotenuse * hypotenuse));
                        DistancesFromStabilTags distancesFrom = new DistancesFromStabilTags
                        {
                            Id = item.Id,
                            InterpolatedId = item.Id,
                            Longitude = Convert.ToDouble(item.Lng),
                            Latitude = Convert.ToDouble(item.Lat),
                            Altitude = Convert.ToDouble(mobileTags.altitude),
                            Distance = distance
                        };
                        distances.Add(distancesFrom);

                        MobilePacketV2FWModel[] mobilePacketV2FWModels = new MobilePacketV2FWModel[1];

                        mobilePacketV2FWModels[0] = new MobilePacketV2FWModel
                        {
                            MobileNodeCode = Convert.ToUInt16(mobileTags.code),
                            DataCountNo = Convert.ToUInt16(item.Id),
                            Distance = Convert.ToUInt16(distances[k]),
                            DeviceState = 10,
                            DataType = 11,
                            DataValue = Convert.ToUInt16(twrPeriod)
                        };
                        k += 1;

                        MessageType44FWModel messageType44FWModel = new MessageType44FWModel
                        {
                            MessageType = 44,
                            DataSize = 1,
                            AnchorCode = Convert.ToUInt16(stabilTag.code),
                            MobilePackets = mobilePacketV2FWModels
                        };
                        messages.Add(messageType44FWModel);
                    }
                    //Thread.Sleep(twrPeriod * 100);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception!" + ex.Message);
                throw;
            }

            return messages;
        }
    }
}