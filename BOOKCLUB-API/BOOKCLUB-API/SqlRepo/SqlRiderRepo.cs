using BOOKCLUB_API.IRepository;
using System;
using System.Linq;
using BOOKCLUB_API.Models;
using BOOKCLUB_API.WrapperClasses;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BOOKCLUB_API.SqlRepo
{
    public class SqlRiderRepo : IRider
    {
        private readonly BOOKCLUBFYPContext _mContext;
        private Dictionary<string, DistanceDuration> distanceDuration_ = new Dictionary<string, DistanceDuration>();
        public SqlRiderRepo(BOOKCLUBFYPContext mContext)
        {
            this._mContext = mContext;
        }

        public APIModel LoginRider(string username, string password)
        {
            APIModel apiModel = new APIModel();
            try
            {
                //var allRiders = _mContext.Riders.ToList();
                var riders = this._mContext.Riders.Where(x => x.UserName == username && x.Password == password).ToList();
                if (riders.Count > 0)
                {
                    apiModel.status = "200";
                    apiModel.success = true;
                    apiModel.message = "Login Successfull!";
                    apiModel.data = riders.FirstOrDefault();
                    apiModel.timeStamp = DateTime.Now.ToString();
                    return apiModel;
                }
                apiModel.status = "404";
                apiModel.success = false;
                apiModel.message = "Not Found";
                apiModel.data = null;
                apiModel.timeStamp = DateTime.Now.ToString();
                return apiModel;
            }
            catch (Exception e)
            {
                apiModel.status = "500";
                apiModel.success = false;
                apiModel.message = e.Message;
                apiModel.data = null;
                apiModel.timeStamp = DateTime.Now.ToString();
                return apiModel;
            }
        }

        public APIModel ReachedDestination(RequestWrapperClass request)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            throw new NotImplementedException();
        }

        public APIModel ReachedSource(RequestWrapperClass request)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            throw new NotImplementedException();
        }

        public APIModel SignUpRider(Rider rider)
        {
            APIModel apiModel = new APIModel();
            try
            {
                var riderExists = this._mContext.Riders.Where(x=>x.UserName == rider.UserName).FirstOrDefault();
                if (riderExists == null)
                {
                    this._mContext.Riders.Add(rider);
                    this._mContext.SaveChanges();

                    apiModel.data = rider;
                    apiModel.message = "Rider Saved Successfully";
                    apiModel.status = "200";
                    apiModel.success = true;
                    apiModel.timeStamp = DateTime.Now.ToString();
                    return apiModel;
                }
                apiModel.data = null;
                apiModel.message = "Rider Already Exists";
                apiModel.status = "500";
                apiModel.success = false;
                apiModel.timeStamp = DateTime.Now.ToString();
                return apiModel;

            }
            catch (Exception e)
            {
                apiModel.data = null;
                apiModel.message = e.Message;
                apiModel.status = "500";
                apiModel.success = false;
                apiModel.timeStamp = DateTime.Now.ToString();
                return apiModel;
            }
            throw new NotImplementedException();
        }

        public APIModel GetAllRiders()
        {
            APIModel apiModel = new APIModel();
            try
            {
                var riders = this._mContext.Riders.Where(x => x.IsActive == true).ToList();
                if (riders != null)
                {
                    apiModel.data = riders;
                    apiModel.message = "Riders Fetched!";
                    apiModel.status = "200";
                    apiModel.success = true;
                    apiModel.timeStamp = DateTime.Now.ToString();
                    return apiModel;
                }
                apiModel.data = riders;
                apiModel.message = "Not Found!";
                apiModel.status = "400";
                apiModel.success = true;
                apiModel.timeStamp = DateTime.Now.ToString();
                return apiModel;
            }
            catch (Exception e)
            {
                apiModel.data = e.Message;
                apiModel.message = "Exception!";
                apiModel.status = "500";
                apiModel.success = true;
                apiModel.timeStamp = DateTime.Now.ToString();
                return apiModel;
            }
        }

        public APIModel GetAllRequestDelivery(int status)
        {
            APIModel apiModel = new APIModel();
            try
            {
                List<RequestDelivery> requestDeliveries = new List<RequestDelivery>();
                List<RequestDeliveryWrapper> deliveryWrapper = new List<RequestDeliveryWrapper>();
                if (status == (int)RequestDeliveryStatus.CREATED)
                    requestDeliveries = this._mContext.RequestDeliveries.Where(x => x.Status == "CREATED").ToList();
                else if (status == (int)RequestDeliveryStatus.ACCEPTED)
                    requestDeliveries = this._mContext.RequestDeliveries.Where(x => x.Status == "ACCEPTED").ToList();
                else if (status == (int)RequestDeliveryStatus.INPROGRESS)
                    requestDeliveries = this._mContext.RequestDeliveries.Where(x => x.Status == "INPROGRESS").ToList();
                else if (status == (int)RequestDeliveryStatus.COMPLETED)
                    requestDeliveries = this._mContext.RequestDeliveries.Where(x => x.Status == "COMPLETED").ToList();

                //var requestDeliveries = this._mContext.RequestDeliveries.ToList();

                foreach (var item in requestDeliveries)
                {
                    var attributes = this._mContext.RequestDeliveryAttributes.Where(x=>x.RequestDeliveryId == item.Id).ToList();
                    deliveryWrapper.Add(new RequestDeliveryWrapper()
                    {
                        ID = item.Id,
                        //BidId = item.BidId,
                        RequestId = item.RequestId,
                        DeliveryDate = (DateTime)item.DeliveryDate,
                        Status = item.Status,
                        timestamp = (DateTime)item.Timestamp,
                        deliveryAttribs = attributes.Where(x => x.RequestDeliveryId == item.Id).ToList()
                    });
                }
                
                if (deliveryWrapper.Count > 0)
                {
                    apiModel.status = "200";
                    apiModel.success = true;
                    apiModel.message = "Request Deliveries Fetched!";
                    apiModel.timeStamp = DateTime.Now.ToString();
                    apiModel.data = deliveryWrapper;
                    return apiModel;
                }
                apiModel.status = "200";
                apiModel.success = true;
                apiModel.message = "Currently No Request Deliveries";
                apiModel.timeStamp = DateTime.Now.ToString();
                apiModel.data = requestDeliveries;
                return apiModel;
            }
            catch (Exception e)
            {
                apiModel.data = e.Message;
                apiModel.message = "Exception!";
                apiModel.status = "500";
                apiModel.success = true;
                apiModel.timeStamp = DateTime.Now.ToString();
                return apiModel;
            }
            
        }
        #region Old Accept Delivery Request
        //public APIModel AcceptDeliveryRequest_1(RiderAcceptRequest request)
        //{
        //    APIModel apiModel = new APIModel();
        //    try
        //    {
        //        LocationCoordinates riderLocation = new();
        //        riderLocation.Latitude = (double)request.RiderLatitude;
        //        riderLocation.Longitude = (double)request.RiderLongitude;
        //        if (riderLocation.Latitude == 0 || riderLocation.Latitude == 0.0)
        //        {
        //            apiModel.status = "400";
        //            apiModel.success = false;
        //            apiModel.message = "Rider Location Not Supplied";
        //            apiModel.data = null;
        //            apiModel.timeStamp = DateTime.Now.ToString();
        //            return apiModel;
        //        }

        //        using (var dbTransaction = _mContext.Database.BeginTransaction())
        //        {
        //            var requestDelivery = _mContext.RequestDeliveries.Where(x => x.Id == request.RequestDeliveryId).First();
        //            var attributes = _mContext.RequestDeliveryAttributes.Where(x => x.RequestDeliveryId == request.RequestDeliveryId).ToList();
        //            try
        //            {
        //                requestDelivery.Status = "ACCEPTED";
        //                _mContext.RiderMasters.Add(new RiderMaster()
        //                {
        //                    Rdid = request.RequestDeliveryId,
        //                    ReqId = requestDelivery.RequestId,
        //                    RiderId = request.RiderId,
        //                    RiderLatitude = request.RiderLatitude,
        //                    RiderLongitude = request.RiderLongitude,
        //                    RiderLocation = request.RiderLocation
        //                });
        //                _mContext.SaveChanges();

        //                long Rmid_ = _mContext.RiderMasters.Where(x => x.Rdid == request.RequestDeliveryId).First().Mid;
        //                foreach (var item in attributes)
        //                {
        //                    _mContext.RiderDeliveryAttributes.Add(new RiderDeliveryAttribute()
        //                    {
        //                        Rmid = Rmid_,
        //                        Destination = item.DropOffAddress,
        //                        Longitude = (decimal)item.DropOffLong,
        //                        Latitude = (decimal)item.DropOffLat,
        //                        Status = "CREATED",
        //                        Type = "DROPOFF",
        //                        Priority = null
        //                    });
        //                    _mContext.SaveChanges();
        //                    _mContext.RiderDeliveryAttributes.Add(new RiderDeliveryAttribute()
        //                    {
        //                        Rmid = Rmid_,
        //                        Destination = item.PickupAddress,
        //                        Longitude = (decimal)item.PickupLong,
        //                        Latitude = (decimal)item.PickupLat,
        //                        Status = "CREATED",
        //                        Type = "PICKUP",
        //                        Priority = null
        //                    });
        //                    _mContext.SaveChanges();
        //                }
        //                dbTransaction.Commit();

        //                apiModel.status = "200";
        //                apiModel.success = true;
        //                apiModel.message = "Request Accepted!";
        //                apiModel.timeStamp = DateTime.Now.ToString();
        //                apiModel.data = null;
        //                return apiModel;
        //            }
        //            catch (Exception e)
        //            {
        //                dbTransaction.Rollback();
        //                apiModel.status = "500";
        //                apiModel.success = false;
        //                apiModel.message = "Exception Occured!";
        //                apiModel.timeStamp = DateTime.Now.ToString();
        //                apiModel.data = e.Message;
        //                return apiModel;
        //            }

        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        apiModel.status = "500";
        //        apiModel.success = false;
        //        apiModel.message = "Exception Occured!";
        //        apiModel.timeStamp = DateTime.Now.ToString();
        //        apiModel.data = e.Message;
        //        return apiModel;
        //    }
        //}
        #endregion
        public APIModel AcceptDeliveryRequest(RiderAcceptRequest request)
        {
            APIModel apiModel = new APIModel();
            using (var dbTransaction = _mContext.Database.BeginTransaction())
            {
                try
                {
                    var requestDelivery = _mContext.RequestDeliveries.Where(x => x.Id == request.RequestDeliveryId).ToList().First();

                    requestDelivery.Status = "ACCEPTED";
                    _mContext.SaveChanges();
                    dbTransaction.Commit();

                    apiModel.success = true;
                    apiModel.timeStamp = DateTime.Now.ToString();
                    apiModel.message = "Delivery Request Accepted";
                    apiModel.status = 200.ToString();
                    return apiModel;

                }
                catch (Exception e)
                {
                    dbTransaction.Rollback();
                    apiModel.success = false;
                    apiModel.timeStamp = DateTime.Now.ToString();
                    apiModel.message = "Exception Occured";
                    apiModel.status = 500.ToString();
                    apiModel.data = e.Message;
                    return apiModel;
                    throw;
                }
            }
        }

        public APIModel StartJob(StartJobBody body)
        {
            APIModel apiModel = new APIModel();
            using (var dbTransaction = _mContext.Database.BeginTransaction())
            {
                try
                {
                    Dictionary<string, LocationCoordinates> keyValuePairs = new();
                    Dictionary<LocationCoordinates, DistanceDuration> distanceDurations = new();
                    var requestDelivery = _mContext.RequestDeliveries.Where(x => x.Id == body.RequestDeliveryId).ToList().First();
                    var requestDeliveryAttributes = _mContext.RequestDeliveryAttributes.Where(x => x.RequestDeliveryId == requestDelivery.Id).ToList();
                    var request = _mContext.Requests.Where(x => x.ReqId == requestDelivery.RequestId).ToList().First();
                    var requestType = _mContext.Requests.Where(x => x.ReqId == (_mContext.RequestDeliveries.Where(x => 
                                        x.Id == body.RequestDeliveryId)).First().RequestId).ToList().First().TypeId;
                    if (requestType == "SELL")
                    {
                        RiderMaster riderMaster = new RiderMaster();
                        riderMaster.RiderId = body.RiderId;
                        riderMaster.ReqId = requestDelivery.RequestId;
                        riderMaster.RequestDeliveryId = requestDelivery.Id;
                        riderMaster.RiderLocation = body.RiderLocationName;
                        riderMaster.RiderLatitude = (decimal)body.RiderLocation.Latitude;
                        riderMaster.RiderLongitude = (decimal)body.RiderLocation.Longitude;
                        _mContext.SaveChanges();

                        int i = 0;
                        foreach (var attribute in requestDeliveryAttributes)
                        {
                            if (requestDeliveryAttributes.Count == 1)
                            {
                                RiderDeliveryAttribute riderDeliveryAttribs = new RiderDeliveryAttribute();
                                riderDeliveryAttribs.RiderMasterId = riderMaster.Id;
                                riderDeliveryAttribs.Destination = attribute.PickupAddress;
                                riderDeliveryAttribs.Latitude = (decimal)attribute.PickupLat;
                                riderDeliveryAttribs.Longitude = (decimal)attribute.PickupLong;
                                riderDeliveryAttribs.Type = "PICKUP";
                                riderDeliveryAttribs.Status = "STARTED";
                                riderDeliveryAttribs.Priority = 1;
                                _mContext.SaveChanges();
                            }
                            else
                            {
                                LocationCoordinates coordinates = new LocationCoordinates();
                                if (i == 0)
                                { 
                                    coordinates.Latitude = (double)attribute.PickupLat;
                                    coordinates.Longitude = (double)attribute.PickupLong;
                                    keyValuePairs.Add("PICKUP", coordinates);
                                }
                                else
                                {
                                    coordinates.Latitude = (double)attribute.DropOffLat;
                                    coordinates.Longitude = (double)attribute.DropOffLong;
                                    keyValuePairs.Add("DROPOFF", coordinates);
                                }
                            }                            
                        }
                        distanceDurations = GetDistanceMatrix(body.RiderLocation, keyValuePairs);

                        int maxValue = 0;
                        foreach (var item in distanceDurations)
                        {
                            var current = item.Value.Distance;
                            if (Convert.ToInt16(item.Value.Distance) > maxValue)
                            {
                                maxValue = Convert.ToInt16(item.Value.Distance);
                                
                            }
                        }
                    }
                    else if (requestType == "EXCHANGE")
                    {
                        RiderMaster riderMaster = new RiderMaster();
                        riderMaster.RiderId = body.RiderId;
                        riderMaster.ReqId = requestDelivery.RequestId;
                        riderMaster.RequestDeliveryId = requestDelivery.Id;
                        riderMaster.RiderLocation = body.RiderLocationName;
                        riderMaster.RiderLatitude = (decimal)body.RiderLocation.Latitude;
                        riderMaster.RiderLongitude = (decimal)body.RiderLocation.Longitude;
                        _mContext.SaveChanges();

                        foreach (var attribute in requestDeliveryAttributes)
                        {
                            RiderDeliveryAttribute riderDeliveryAttribs = new RiderDeliveryAttribute();
                            riderDeliveryAttribs.RiderMasterId = riderMaster.Id;
                            riderDeliveryAttribs.Destination = attribute.PickupAddress;
                            riderDeliveryAttribs.Latitude = (decimal)attribute.PickupLat;
                            riderDeliveryAttribs.Longitude = (decimal)attribute.PickupLong;
                            riderDeliveryAttribs.Type = "PICKUP";
                            riderDeliveryAttribs.Status = "STARTED";
                            riderDeliveryAttribs.Priority = 1;
                            _mContext.SaveChanges();
                        }

                    }

                    apiModel.success = false;
                    apiModel.timeStamp = DateTime.Now.ToString();
                    apiModel.message = "Exception Occured";
                    apiModel.status = 500.ToString();
                    return apiModel;
                }
                catch (Exception e)
                {
                    dbTransaction.Rollback();
                    apiModel.success = false;
                    apiModel.timeStamp = DateTime.Now.ToString();
                    apiModel.message = "Exception Occured";
                    apiModel.status = 500.ToString();
                    apiModel.data = e.Message;
                    throw;
                }
            }
        }

        #region Old StartJob
        //public APIModel StartJob(int ReqDeliveryId, LocationCoordinates riderLatLong)
        //{
        //    LocationCoordinates locationCoordinates = new LocationCoordinates();
        //    APIModel apiModel = new APIModel();
        //    try
        //    {
        //        var requestDelivery = _mContext.RequestDeliveries.Where(x => x.Id == ReqDeliveryId).ToList();
        //        var requestDeliveryAttributes = _mContext.RequestDeliveryAttributes.Where(x => x.RequestDeliveryId == ReqDeliveryId).ToList();

        //        if (requestDelivery.Count != 0 && (requestDelivery.First().Status == "ACCEPTED"))
        //        {
        //            var request = _mContext.Requests.Where(x => x.ReqId == requestDelivery.First().RequestId).ToList();
        //            var requestAttributes = _mContext.RequestAttributes.Where(x => x.RequestId == request.First().ReqId.ToString()).ToList();
        //            string requestTypeId = request.First().TypeId;

        //            if (requestTypeId == "EXCHANGE")
        //            {
        //                Dictionary<LocationCoordinates, DistanceDuration> keyValuePairs = new Dictionary<LocationCoordinates, DistanceDuration>();
        //                List<LocationCoordinates> destinations = new List<LocationCoordinates>();

        //                var riderMaster = _mContext.RiderMasters.Where(x => x.Rdid == (long)ReqDeliveryId).ToList().First();
        //                var riderDeliveryAttributes = _mContext.RiderDeliveryAttributes.Where(x => x.Rmid == riderMaster.Mid).ToList();

        //                foreach (var item in riderDeliveryAttributes)
        //                {
        //                    //locationCoordinates.Latitude = (double)item.Latitude;
        //                    //locationCoordinates.Longitude = (double)item.Longitude;
        //                    //destinations.Add(locationCoordinates);
        //                    destinations.Add(new LocationCoordinates()
        //                    {
        //                        Latitude = (double)item.Latitude,
        //                        Longitude = (double)item.Longitude
        //                    });
        //                }
        //                var shortestDestination = start_job_exchange(riderLatLong, destinations, ref keyValuePairs);



        //            }
        //            else if (requestTypeId == "SELL")
        //            {
        //                locationCoordinates.Latitude = Convert.ToDouble(requestAttributes.Where(x => x.Name == "AdLatitute_").First().Value);
        //                locationCoordinates.Longitude = Convert.ToDouble(requestAttributes.Where(x => x.Name == "AdLongitude_").First().Value);

        //                start_job_sell(riderLatLong, locationCoordinates);
        //                string json = JsonConvert.SerializeObject(distanceDuration_);

        //                apiModel.data = json;
        //                apiModel.message = "Distance to Seller";
        //                apiModel.status = 200.ToString();
        //                apiModel.success = true;
        //                apiModel.timeStamp = DateTime.Now.ToString();
        //            }
        //        }

        //        //GetShortestPath(riderLocation, attributes);
        //        //var dictionary = CalculatePriority(attributes, riderLocation);
        //        //if (dictionary == null)
        //        //{
        //        //    apiModel.status = "400";
        //        //    apiModel.success = false;
        //        //    apiModel.message = "Priorities could not be set";
        //        //    apiModel.data = "Error Occured";
        //        //    apiModel.timeStamp = DateTime.Now.ToString();
        //        //    return apiModel;
        //        //}

        //        return apiModel;
        //    }
        //    catch (Exception e)
        //    {
        //        apiModel.status = "400";
        //        apiModel.success = false;
        //        apiModel.message = "Priorities could not be set";
        //        apiModel.data = "Error Occured";
        //        apiModel.timeStamp = DateTime.Now.ToString();
        //        return apiModel;
        //    }
        //}
        #endregion
        private void start_job_sell(LocationCoordinates riderLocation, LocationCoordinates requestorLocation)
        {
            try
            {
                DistanceDuration distanceDuration = new DistanceDuration();
                var DistanceMatrix = GetDistanceMatrix(riderLocation, requestorLocation);
                distanceDuration_.Add("RIDER TO SELLER", DistanceMatrix);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private DistanceDuration start_job_exchange(LocationCoordinates riderLocation, List<LocationCoordinates> destinationLocations, ref Dictionary<LocationCoordinates, DistanceDuration> keyValues)
        {
            try
            {

                double maxValue = int.MaxValue;
                List<DistanceDuration> distanceDuration = new List<DistanceDuration>();
                DistanceDuration shortestDistanceDuration = new DistanceDuration();
                var DistanceMatrix = GetDistanceMatrix(riderLocation, destinationLocations);

                for (int i = 0; i < destinationLocations.Count; i++)
                {
                    keyValues.Add(destinationLocations[i], DistanceMatrix[i]);
                }

                foreach (var item in keyValues)
                {
                    string distance = item.Value.Distance;
                    distance = distance.Replace("km", "");
                    if (Convert.ToDouble(distance) <= maxValue)
                    {
                        shortestDistanceDuration = item.Value;
                    }
                }

                var key = keyValues.Where(x => x.Value == shortestDistanceDuration);
                _mContext.RiderDeliveryAttributes.Where(x => x.Latitude == (decimal)key.First().Key.Latitude);

                return shortestDistanceDuration;


                //foreach (var item in DistanceMatrix)
                //{
                //    string dist = item.Distance.Trim(new char[] { 'k', 'm', ' ' });
                //    if (maxValue > Convert.ToDouble(dist))
                //    {
                //        distanceDuration.Add(new DistanceDuration() 
                //        { 
                //            Distance = item.Distance,
                //            Duration = item.Duration 
                //        });
                //    }
                //}


                //distanceDuration_.Add("RIDER TO SELLER", DistanceMatrix);
            }
            catch (Exception e)
            {
                return new DistanceDuration();
            }
        }



        private LocationCoordinates GetShortestPath(LocationCoordinates riderLocation, List<RequestDeliveryAttribute> deliveryAttributes)
        {
            long min_distance = int.MaxValue;
            try
            {
                LocationCoordinates shortestLocation = new();
                List<LocationCoordinates> destinationLocations = new();
                List<LocationCoordinates> riderLocationAsList = new();

                riderLocationAsList.Add(riderLocation);

                foreach (var item in deliveryAttributes)
                {
                    var destLocation = new LocationCoordinates();
                    
                    destLocation.Latitude = (double)item.PickupLat;
                    destLocation.Longitude = (double)item.PickupLong;
                    destinationLocations.Add(destLocation);

                    destLocation.Latitude = (double)item.DropOffLat;
                    destLocation.Longitude = (double)item.DropOffLong;
                    destinationLocations.Add(destLocation);
                }

                var data = GoogleMapsRepo.GetDistanceMatrix(riderLocationAsList, destinationLocations);

                var elements = data.Rows[0].Elements;
                for (int i = 0; i< elements.Count; i++)
                {
                    if (min_distance > elements[i].Distance.Value)
                    {
                        min_distance = elements[i].Distance.Value;
                        shortestLocation.Latitude = destinationLocations[i].Latitude;
                        shortestLocation.Longitude = destinationLocations[i].Longitude;
                    }
                }
                return shortestLocation;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
        Dictionary<int, LocationCoordinates> CalculatePriority(List<RequestDeliveryAttribute> attribs, LocationCoordinates riderOrigin)
        {
            Dictionary<DeliveryLocations, int> priorityDistance = new Dictionary<DeliveryLocations, int>();
            try
            {
                long minValue = int.MaxValue;
                List<long> distanceValues = new List<long>();
                List<LocationCoordinates> dropOffCoordinates = new();
                List<LocationCoordinates> pickUpCoordinates = new();
                List<LocationCoordinates> destinationCoordinates = new();
                pickUpCoordinates.Add(riderOrigin);

                for (int i = 0; i< attribs.Count;i++)
                {
                    destinationCoordinates.Add(new LocationCoordinates()
                    {
                        Latitude = (double)attribs[i].PickupLat,
                        Longitude = (double)attribs[i].PickupLong,
                    });
                    destinationCoordinates.Add(new LocationCoordinates()
                    {
                        Latitude = (double)attribs[i].DropOffLat,
                        Longitude = (double)attribs[i].DropOffLong,
                    });
                    #region OLD
                    //if (i == 0)
                    //{
                    //    pickUpCoordinates.Add(new LocationCoordinates()
                    //    {
                    //        Latitude = (double)attribs[i].PickupLat,
                    //        Longitude = (double)attribs[i].PickupLong
                    //    });
                    //}
                    //else
                    //{
                    //    dropOffCoordinates.Add(new LocationCoordinates()
                    //    {
                    //        Latitude = (double)attribs[i].DropOffLat,
                    //        Longitude = (double)attribs[i].DropOffLong
                    //    });
                    //}
                    #endregion
                }

                DistanceMatrix data = GoogleMapsRepo.GetDistanceMatrix(pickUpCoordinates, destinationCoordinates);
                
                if (data.Status != null)
                {
                    var dictionary = SetPriority(data, destinationCoordinates);
                    if (dictionary != null || dictionary.Count != 0)
                    {
                        return dictionary;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        Dictionary<int, LocationCoordinates> SetPriority(DistanceMatrix data, List<LocationCoordinates> dests) 
        {
            int priority = 1;
            List<long> distanceValues = new List<long>();
            Dictionary<int, LocationCoordinates> dict = new Dictionary<int, LocationCoordinates>();
            
            try
            {
                var elements = data.Rows[0].Elements;
                for (int i = 0; i < elements.Count; i++)
                {
                    distanceValues.Add(elements[i].Distance.Value);
                }
                distanceValues.Sort();
                foreach (long item in distanceValues)
                {
                    for (int i = 0; i < elements.Count; i++)
                    {
                        if (item == elements[i].Distance.Value)
                        {
                            dict.Add(priority, dests[i]);
                            priority++;
                        }
                    }
                }

                return dict;
            }
            catch (Exception)
            {
                return dict;
            }
        }
        private DistanceDuration GetDistanceMatrix(LocationCoordinates riderLocation, LocationCoordinates deliveryLocation)
        {
            List<DistanceDuration> distanceDuration = new();
            try
            {
                var data = GoogleMapsRepo.GetDistanceMatrix(riderLocation, deliveryLocation);
                foreach (var item in data.Rows[0].Elements)
                {
                    distanceDuration.Add(new() 
                    { 
                        Distance = item.Distance.Text,
                        Duration = item.Duration.Text 
                    });
                }
                return distanceDuration.First();
            }
            catch (Exception)
            {
                return new DistanceDuration();
            }
        }
        private Dictionary<LocationCoordinates, DistanceDuration> GetDistanceMatrix(LocationCoordinates riderLocation, Dictionary<string, LocationCoordinates> deliveryLocation)
        {
            int i = 0;
            DistanceDuration distanceDuration;// = new();
            Dictionary<LocationCoordinates, DistanceDuration> returnValues = new Dictionary<LocationCoordinates, DistanceDuration>();
            var deliveryList = deliveryLocation.Values.ToList();
            
            try
            {
                var data = GoogleMapsRepo.GetDistanceMatrix(riderLocation, deliveryLocation);
                
                foreach (var item in data.Rows[0].Elements)
                {
                    distanceDuration = new DistanceDuration();
                    distanceDuration.Distance = item.Distance.Text;
                    distanceDuration.Duration = item.Duration.Text;

                    returnValues.Add(deliveryList[i], distanceDuration);
                }
                return returnValues;
            }
            catch (Exception)
            {
                return returnValues;
            }
        }

        private List<DistanceDuration> GetDistanceMatrix(LocationCoordinates riderLocation, List<LocationCoordinates> deliveryLocation)
        {
            List<DistanceDuration> distanceDuration = new List<DistanceDuration>();
            try
            {
                var data = GoogleMapsRepo.GetDistanceMatrix(riderLocation, deliveryLocation);
                foreach (var item in data.Rows[0].Elements)
                {
                    distanceDuration.Add(new DistanceDuration()
                    {
                        Distance = item.Distance.Text,
                        Duration = item.Duration.Text
                    });
                }
                return distanceDuration;
            }
            catch (Exception)
            {
                return new List<DistanceDuration>();
            }
        }
        #region Old GetNextLocation
        //private void GetNextLocation(LocationCoordinates riderLocation, long Rmid)
        //{
        //    LocationCoordinates deliveryLocation = new();
        //    try
        //    {
        //        List<RiderDeliveryAttribute> riderAttributes = new List<RiderDeliveryAttribute>();
        //        riderAttributes = _mContext.RiderDeliveryAttributes.Where(x => x.Rmid == Rmid).OrderByDescending(x => x.Ratid).ToList();

        //        for (int i = 0; i < riderAttributes.Count; i++)
        //        {
        //            if (riderAttributes[i].Type == "PICKUP" && riderAttributes[i].Status == "CREATED")
        //            {
        //                var attribs = _mContext.RiderDeliveryAttributes.SingleOrDefault(x => x.Ratid == riderAttributes[i].Ratid);
        //                deliveryLocation.Latitude = (double)attribs.Latitude; // Select(x => (double)x.Latitude).First();
        //                deliveryLocation.Longitude = (double)attribs.Longitude; // Select(x => (double)x.Longitude).First();
        //                if (attribs != null)
        //                {
        //                    attribs.Status = "IN PROGRESS";
        //                    _mContext.SaveChanges();
        //                }
        //            }
        //        }
                
                



        //        var distanceAndDuration = GetDistanceMatrix(riderLocation, deliveryLocation);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        #endregion
        enum RequestDeliveryStatus
        {
            CREATED = 1,
            ACCEPTED = 2,
            INPROGRESS = 3,
            COMPLETED = 4
        }
    }

    class DistanceDuration
    {
        public string Distance { get; set; }
        public string Duration { get; set; }
    }
}
