using BOOKCLUB_API.IRepository;
using BOOKCLUB_API.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace BOOKCLUB_API.SqlRepo
{
    public static class GoogleMapsRepo
    {
        private static string basicDomain = "https://maps.googleapis.com/maps/api";
        private static string key = "AIzaSyBuNIgucPhZ8mTSvya4r8G1Xp15ZRKM_BY";
        private static HttpWebResponse response = null;

        public static DistanceMatrix GetDistanceMatrix(List<LocationCoordinates> origins, List<LocationCoordinates> destinations)
        {
            var distanceMatrix = new DistanceMatrix();
            string strOrigin = "origins=";
            string strDestination = "destinations=";
            try
            {
                strOrigin += WebUtility.UrlEncode(origins[0].Latitude.ToString() + "," + origins[0].Longitude.ToString());
                foreach (var item in destinations)
                {
                    strDestination += WebUtility.UrlEncode(item.Latitude.ToString() + "," + item.Longitude.ToString());
                    strDestination += "|";
                }
                strDestination = strDestination.Substring(0, strDestination.Length - 1);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(basicDomain + "/distancematrix/json?" + strOrigin + "&" + strDestination + "&key=" + key);
                response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    string html = reader.ReadToEnd();
                    distanceMatrix = JsonConvert.DeserializeObject<DistanceMatrix>(html);

                    //string temp = "{\r\n    \"destination_addresses\": [\r\n        \"327 Beach 19th St, Far Rockaway, NY 11691, USA\",\r\n        \"1000 N Village Ave, Rockville Centre, NY 11570, USA\",\r\n        \"102-01 66th Rd, Queens, NY 11375, USA\",\r\n        \"585 Schenectady Ave, Brooklyn, NY 11203, USA\"\r\n    ],\r\n    \"origin_addresses\": [\r\n        \"P.O. Box 793, Brooklyn, NY 11207, USA\"\r\n    ],\r\n    \"rows\": [\r\n        {\r\n            \"elements\": [\r\n                {\r\n                    \"distance\": {\r\n                        \"text\": \"21.3 km\",\r\n                        \"value\": 21304\r\n                    },\r\n                    \"duration\": {\r\n                        \"text\": \"34 mins\",\r\n                        \"value\": 2058\r\n                    },\r\n                    \"status\": \"OK\"\r\n                },\r\n                {\r\n                    \"distance\": {\r\n                        \"text\": \"25.6 km\",\r\n                        \"value\": 25552\r\n                    },\r\n                    \"duration\": {\r\n                        \"text\": \"30 mins\",\r\n                        \"value\": 1781\r\n                    },\r\n                    \"status\": \"OK\"\r\n                },\r\n                {\r\n                    \"distance\": {\r\n                        \"text\": \"13.8 km\",\r\n                        \"value\": 13760\r\n                    },\r\n                    \"duration\": {\r\n                        \"text\": \"23 mins\",\r\n                        \"value\": 1373\r\n                    },\r\n                    \"status\": \"OK\"\r\n                },\r\n                {\r\n                    \"distance\": {\r\n                        \"text\": \"4.6 km\",\r\n                        \"value\": 4631\r\n                    },\r\n                    \"duration\": {\r\n                        \"text\": \"18 mins\",\r\n                        \"value\": 1101\r\n                    },\r\n                    \"status\": \"OK\"\r\n                }\r\n            ]\r\n        }\r\n    ],\r\n    \"status\": \"OK\"\r\n}";
                    //distanceMatrix = JsonConvert.DeserializeObject<DistanceMatrix>(temp);
                    return distanceMatrix;
                }
            }
            catch (Exception)
            {
                return distanceMatrix;
            }
        }

        public static DistanceMatrix GetDistanceMatrix(LocationCoordinates origin, List<LocationCoordinates> destinations)
        {
            var distanceMatrix = new DistanceMatrix();
            string strOrigin = "origins=";
            string strDestination = "destinations=";
            try
            {
                strOrigin += WebUtility.UrlEncode(origin.Latitude.ToString() + "," + origin.Longitude.ToString());
                foreach (var item in destinations)
                {
                    strDestination += WebUtility.UrlEncode(item.Latitude.ToString() + "," + item.Longitude.ToString());
                    strDestination += "|";
                }
                strDestination = strDestination.Substring(0, strDestination.Length - 1);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(basicDomain + "/distancematrix/json?" + strOrigin + "&" + strDestination + "&key=" + key);
                response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    string html = reader.ReadToEnd();
                    distanceMatrix = JsonConvert.DeserializeObject<DistanceMatrix>(html);

                    //string temp = "{\r\n    \"destination_addresses\": [\r\n        \"327 Beach 19th St, Far Rockaway, NY 11691, USA\",\r\n        \"1000 N Village Ave, Rockville Centre, NY 11570, USA\",\r\n        \"102-01 66th Rd, Queens, NY 11375, USA\",\r\n        \"585 Schenectady Ave, Brooklyn, NY 11203, USA\"\r\n    ],\r\n    \"origin_addresses\": [\r\n        \"P.O. Box 793, Brooklyn, NY 11207, USA\"\r\n    ],\r\n    \"rows\": [\r\n        {\r\n            \"elements\": [\r\n                {\r\n                    \"distance\": {\r\n                        \"text\": \"21.3 km\",\r\n                        \"value\": 21304\r\n                    },\r\n                    \"duration\": {\r\n                        \"text\": \"34 mins\",\r\n                        \"value\": 2058\r\n                    },\r\n                    \"status\": \"OK\"\r\n                },\r\n                {\r\n                    \"distance\": {\r\n                        \"text\": \"25.6 km\",\r\n                        \"value\": 25552\r\n                    },\r\n                    \"duration\": {\r\n                        \"text\": \"30 mins\",\r\n                        \"value\": 1781\r\n                    },\r\n                    \"status\": \"OK\"\r\n                },\r\n                {\r\n                    \"distance\": {\r\n                        \"text\": \"13.8 km\",\r\n                        \"value\": 13760\r\n                    },\r\n                    \"duration\": {\r\n                        \"text\": \"23 mins\",\r\n                        \"value\": 1373\r\n                    },\r\n                    \"status\": \"OK\"\r\n                },\r\n                {\r\n                    \"distance\": {\r\n                        \"text\": \"4.6 km\",\r\n                        \"value\": 4631\r\n                    },\r\n                    \"duration\": {\r\n                        \"text\": \"18 mins\",\r\n                        \"value\": 1101\r\n                    },\r\n                    \"status\": \"OK\"\r\n                }\r\n            ]\r\n        }\r\n    ],\r\n    \"status\": \"OK\"\r\n}";
                    //distanceMatrix = JsonConvert.DeserializeObject<DistanceMatrix>(temp);
                    return distanceMatrix;
                }
            }
            catch (Exception)
            {
                return distanceMatrix;
            }
        }

        public static DistanceMatrix GetDistanceMatrix(LocationCoordinates origins, LocationCoordinates destinations)
        {
            var distanceMatrix = new DistanceMatrix();
            string strOrigin = "origins=";
            string strDestination = "destinations=";
            try
            {
                strOrigin += WebUtility.UrlEncode(origins.Latitude.ToString() + "," + origins.Longitude.ToString());
                strDestination += WebUtility.UrlEncode(destinations.Latitude.ToString() + "," + destinations.Longitude.ToString());
                string url = basicDomain + "/distancematrix/json?" + strOrigin + "&" + strDestination + "&key=" + key;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    string html = reader.ReadToEnd();
                    distanceMatrix = JsonConvert.DeserializeObject<DistanceMatrix>(html);

                    return distanceMatrix;
                }
            }
            catch (Exception)
            {
                return distanceMatrix;
            }
        }


        public static DistanceMatrix GetDistanceMatrix(LocationCoordinates origins, Dictionary<string, LocationCoordinates> destination)
        {
            List<LocationCoordinates> locationCoordinatesList = new List<LocationCoordinates>();
            foreach (var item in destination)
            {
                locationCoordinatesList.Add(item.Value);
            }
            
            var distanceMatrix = new DistanceMatrix();
            string strOrigin = "origins=";
            string strDestination = "destinations=";
            try
            {
                strOrigin += WebUtility.UrlEncode(origins.Latitude.ToString() + "," + origins.Longitude.ToString());

                foreach (var item in locationCoordinatesList)
                {
                    strDestination += WebUtility.UrlEncode(item.Latitude.ToString() + "," + item.Longitude.ToString());
                    strDestination += "|";
                }
                strDestination = strDestination.Substring(0, strDestination.Length - 1);

                string url = basicDomain + "/distancematrix/json?" + strOrigin + "&" + strDestination + "&key=" + key;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    string html = reader.ReadToEnd();
                    distanceMatrix = JsonConvert.DeserializeObject<DistanceMatrix>(html);

                    return distanceMatrix;
                }
            }
            catch (Exception)
            {
                return distanceMatrix;
            }
        }

        //public APIModel GetDistance(BidderLocations destination)
        //{
        //    APIModel model = new APIModel();
        //    string str = string.Empty;
        //    try
        //    {
        //        str = WebUtility.UrlEncode(destination.long1 + ", " + destination.lat1);
        //        if (destination.long2 != 0.0 && destination.lat2 != 0.0)
        //        {
        //            str += "|";
        //            str += WebUtility.UrlEncode(destination.long2 + "," + destination.lat2);
        //            if (destination.long3 != 0.0 && destination.lat3 != 0.0)
        //            {
        //                str += "|";
        //                str += WebUtility.UrlEncode(destination.long3 + "," + destination.lat3);
        //            }
        //        }
        //        str += "&origins=" + WebUtility.UrlEncode(destination.origin.long_ + "," + destination.origin.lat_);

        //        string uri = basicDomain + "/distancematrix/json?" +
        //            $"destinations={str}&key=AIzaSyBuNIgucPhZ8mTSvya4r8G1Xp15ZRKM_BY";
        //        //24.95385133857125%2C+67.05842602574519|24.99125085831958%2C+67.06638598156387&origins=24.929122354799897%2C+67.11558816807181
        //        WebRequest request = (WebRequest)HttpWebRequest.Create(uri);
        //        request.Method = "GET";

        //        response = (HttpWebResponse)request.GetResponse();
        //        using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
        //        {
        //            if (response.StatusCode == HttpStatusCode.OK)
        //            {
        //                model.status = response.StatusCode.ToString();
        //                model.data = JObject.Parse(reader.ReadToEnd());
        //                model.message = response.StatusDescription;
        //                model.success = true;
        //                model.timeStamp = DateTime.Now.ToString();
        //            }
        //            else
        //            {
        //                model.status = response.StatusCode.ToString();
        //                model.data = reader.ReadToEnd();
        //                model.message = response.StatusDescription;
        //                model.success = false;
        //                model.timeStamp = DateTime.Now.ToString();
        //            }
        //        }
        //        return model;
        //    }
        //    catch (System.Exception)
        //    {
        //        throw;
        //    }
        //}

        //public APIModel GetDirections(BidderLocations destination)
        //{
        //    APIModel model = new APIModel();
        //    string originStr = string.Empty;
        //    string destStr = string.Empty;
        //    string str = string.Empty;
        //    try
        //    {
        //        str = WebUtility.UrlEncode(destination.long1 + ", " + destination.lat1);
        //        if (destination.long2 != 0.0 && destination.lat2 != 0.0)
        //        {
        //            str += "|";
        //            str += WebUtility.UrlEncode(destination.long2 + "," + destination.lat2);
        //            if (destination.long3 != 0.0 && destination.lat3 != 0.0)
        //            {
        //                str += "|";
        //                str += WebUtility.UrlEncode(destination.long3 + "," + destination.lat3);
        //            }
        //        }
        //        str += "&origins=" + WebUtility.UrlEncode(destination.origin.long_ + "," + destination.origin.lat_);

        //        //Origin origin = dest.origin;
        //        //originStr = "origin=" + origin.long_ + "," + origin.lat_;
        //        //destStr = "destination=" + dest.long1 + "," + dest.lat1;
        //        string uri = basicDomain + "/directions/json?" +
        //            $"destinations={str}&key=AIzaSyBuNIgucPhZ8mTSvya4r8G1Xp15ZRKM_BY";

        //        WebRequest request = (WebRequest)HttpWebRequest.Create(uri);

        //        //WebRequest request = (WebRequest)HttpWebRequest.Create(basicDomain + "/directions/json?" +
        //        //    originStr + "&" + destStr + "&key=AIzaSyBuNIgucPhZ8mTSvya4r8G1Xp15ZRKM_BY");

        //        response = (HttpWebResponse)request.GetResponse();
        //        using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
        //        {
        //            if (response.StatusCode == HttpStatusCode.OK)
        //            {
        //                model.status = response.StatusCode.ToString();
        //                model.data = JObject.Parse(reader.ReadToEnd());
        //                model.success = true;
        //                model.timeStamp = DateTime.Now.ToString();
        //                model.message = response.StatusDescription;
        //            }
        //            else
        //            {
        //                model.status = response.StatusCode.ToString();
        //                model.data = JObject.Parse(reader.ReadToEnd());
        //                model.success = false;
        //                model.timeStamp = DateTime.Now.ToString();
        //                model.message = response.StatusDescription;
        //            }
        //        }
        //        return model;
        //    }
        //    catch (Exception e)
        //    {
        //        model.status = "500";
        //        model.data = e.Message;
        //        model.success = false;
        //        model.timeStamp = DateTime.Now.ToString();
        //        model.message = "Request Failed";
        //        return model;
        //    }
        //    return null;
        //}
    }
}
