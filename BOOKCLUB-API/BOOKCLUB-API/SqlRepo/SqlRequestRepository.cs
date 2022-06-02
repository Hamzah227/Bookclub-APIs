using BOOKCLUB_API.IRepository;
using BOOKCLUB_API.Models;
using BOOKCLUB_API.WrapperClasses;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BOOKCLUB_API.SqlRepo
{
    public class SqlRequestRepository : IRequest
    {
        private readonly BOOKCLUBFYPContext _mContext;

        public SqlRequestRepository(BOOKCLUBFYPContext mContext)
        {
            this._mContext = mContext;
        }

        public APIModel ApproveBid(long BidId)
        {
            APIModel apiModel = new APIModel();
            try
            {
                var bid = _mContext.Bids.Where(x => x.Id == BidId).SingleOrDefault();
                var request = _mContext.Requests.Where(x => x.ReqId == Convert.ToInt32(bid.RequestId)).SingleOrDefault();
               

                if (bid != null)
                {
                    request.Status = "Accepted";
                    bid.Status = "Approved";
                    _mContext.SaveChanges();

                    if (CreateRequestDelivery(bid, request))
                    {
                        apiModel.data = "success";
                        apiModel.message = "Bid Approved";
                        apiModel.status = "200";
                        apiModel.success = true;
                        apiModel.timeStamp = DateTime.Now.ToString();
                        return apiModel;
                    }
                    else
                    {
                        apiModel.data = "failure";
                        apiModel.message = "Bid Approved but Request Delivery Failed!";
                        apiModel.status = "200";
                        apiModel.success = false;
                        apiModel.timeStamp = DateTime.Now.ToString();
                        return apiModel;
                    }
                } 
                else
                {
                    apiModel.data = string.Empty;
                    apiModel.message = "Bid Id cannot be found";
                    apiModel.status = "404";
                    apiModel.success = false;
                    apiModel.timeStamp = DateTime.Now.ToString();
                    return apiModel;
                }
            }
            catch (Exception ex)
            {
                apiModel.data = string.Empty;
                apiModel.message = ex.Message;
                apiModel.status = "500";
                apiModel.success = false;
                apiModel.timeStamp = DateTime.Now.ToString();
                return apiModel;
            }  
        }

        public APIModel RejectBid(long BidId)
        {
            APIModel apiModel = new APIModel();
            try
            {
                var bid = _mContext.Bids.Where(x => x.Id == BidId).SingleOrDefault();



                if (bid != null)
                {
                    bid.Status = "Rejected";
                    _mContext.SaveChanges();

                    apiModel.data = "success";
                    apiModel.message = "Bid Rejected";
                    apiModel.status = "200";
                    apiModel.success = true;
                    apiModel.timeStamp = DateTime.Now.ToString();

                    return apiModel;
                }
                else
                {
                    apiModel.data = string.Empty;
                    apiModel.message = "Bid Id cannot be found";
                    apiModel.status = "500";
                    apiModel.success = false;
                    apiModel.timeStamp = DateTime.Now.ToString();
                    return apiModel;
                }
            }
            catch (Exception ex)
            {

                apiModel.data = string.Empty;
                apiModel.message = ex.Message;
                apiModel.status = "500";
                apiModel.success = false;
                apiModel.timeStamp = DateTime.Now.ToString();
                return apiModel;
            }


        }

        public APIModel GetBids(string Uid)
        {
            APIModel apiModel = new APIModel();
            List<RequestWrapperClass> objbidsWrapperClasses = new List<RequestWrapperClass>();

            try
            {
                var bids = _mContext.Bids.Where(x => x.Uid == Uid).ToList();
                if (bids != null )
                {
                    foreach (var item in bids)
                    {
                        var requestBid = _mContext.Requests.Where(x => x.ReqId.ToString() == item.RequestId).SingleOrDefault();
                        var bidAttributesList = _mContext.BidAttributes.Where(x => x.BidId == item.Id.ToString()).ToList();
                        var requsestAttribs = _mContext.RequestAttributes.Where(x => x.RequestId == item.RequestId).ToList();
                        var bidAttributeBooks = _mContext.BidAttributeBooks.Where(x => x.BidId == item.Id).ToList();
                        List<BidsWrapperClass> bidsWrapperClasses = new List<BidsWrapperClass>();
                        RequestWrapperClass request = new RequestWrapperClass();

                        request.Uid = requestBid.Uid;
                        request.Longitude = requestBid.Longitude;
                        request.Latitude = requestBid.Latitude;
                        request.Status = requestBid.Status;
                        request.UpdateAt = requestBid.UpdateAt;
                        request.CreatedOn = requestBid.CreatedOn;
                        request.ExpiresOn = requestBid.ExpiresOn;
                        request.IsActive = requestBid.IsActive;
                        request.IsArchived = requestBid.IsArchived;
                        request.TypeId = requestBid.TypeId;
                        request.ReqId = requestBid.ReqId;
                        request.requestAttributes = requsestAttribs;

                        BidsWrapperClass bidsWrapper = new BidsWrapperClass();
                        bidsWrapper.Id = item.Id;
                        bidsWrapper.RequestId = item.RequestId;
                        bidsWrapper.Status = item.Status;
                        bidsWrapper.StatusProgress = item.StatusProgress;
                        bidsWrapper.Uid = item.Uid;
                        bidsWrapper.Comments = item.Comments;
                        bidsWrapper.Books = bidAttributeBooks;

                        bidsWrapper.bidAttributes = bidAttributesList;
                        bidsWrapperClasses.Add(bidsWrapper);
                        request.bids = bidsWrapperClasses;

                        objbidsWrapperClasses.Add(request);

                        
                    }

                    apiModel.data = objbidsWrapperClasses;
                    apiModel.message = "success";
                    apiModel.status = "200";
                    apiModel.success = true;
                    apiModel.timeStamp = DateTime.Now.ToString();
                    return apiModel;
                } else
                {
                    apiModel.data = null;
                    apiModel.message = "No record Found";
                    apiModel.status = "500";
                    apiModel.success = true;
                    apiModel.timeStamp = DateTime.Now.ToString();
                    return apiModel;
                }
               
            }
            catch (Exception ex)
            {
             
                apiModel.data = null;
                apiModel.status = "400";
                apiModel.success = false;
                apiModel.timeStamp = DateTime.Now.ToString();
                apiModel.message = ex.Message;
                return apiModel;
            }
        }

        public APIModel GetAllRequest(string UID,bool myAds)
        {
            APIModel apiWrapper = new APIModel();
            List<RequestWrapperClass> requestList = new List<RequestWrapperClass>();

            try
            {
              var request = _mContext.Requests.Where(x => x.Uid != UID).ToList();

                if (myAds)
                {
                    request = _mContext.Requests.Where(x => x.Uid == UID).ToList();
                } 
                if (request != null)
                {
                    foreach (var req in request)
                    {
                        RequestWrapperClass wrapper = new RequestWrapperClass();
                        wrapper.Uid = req.Uid;
                        wrapper.Longitude = req.Longitude;
                        wrapper.Latitude = req.Latitude;
                        wrapper.Status = req.Status;
                        wrapper.UpdateAt = req.UpdateAt;
                        wrapper.CreatedOn = req.CreatedOn;
                        wrapper.ExpiresOn = req.ExpiresOn;
                        wrapper.IsActive = req.IsActive;
                        wrapper.IsArchived = req.IsArchived;
                        wrapper.TypeId = req.TypeId;
                        wrapper.ReqId = req.ReqId;
                        


                        var requestAttribs = _mContext.RequestAttributes.Where(x => x.RequestId == req.ReqId.ToString()).ToList();

                        wrapper.requestAttributes = requestAttribs;

                        //foreach (var requestAttributes in requestAttribs)
                        //{
                        //    RequestAttribute requestAttributeobj = new RequestAttribute();
                        //    requestAttributeobj.Id = requestAttributes.Id;
                        //    requestAttributeobj.Name = requestAttributes.Name;
                        //    requestAttributeobj.Value = requestAttributes.Value;
                        //    requestAttributeobj.RequestId = requestAttributes.RequestId;


                        //    wrapper.requestAttributes.Add(requestAttributeobj);

                        //}


                        var bidWrappersList = new List<BidsWrapperClass>();
                         
                        var bids = _mContext.Bids.Where(x => x.RequestId == req.ReqId.ToString()).ToList();
                        if (bids.Count > 0)
                        {
                            foreach (var item in bids)
                            {
                                BidsWrapperClass wrapperClass = new BidsWrapperClass();
                                wrapperClass.Id = item.Id;
                                wrapperClass.RequestId = item.RequestId;
                                wrapperClass.Status = item.Status;
                                wrapperClass.StatusProgress = item.StatusProgress;
                                wrapperClass.Uid = item.Uid;
                                wrapperClass.Comments = item.Comments;
                                var bidAttributesList = _mContext.BidAttributes.Where(x => x.BidId == item.Id.ToString()).ToList();
                                wrapperClass.bidAttributes = bidAttributesList;
                                wrapperClass.Books = _mContext.BidAttributeBooks.Where(x => x.BidId == item.Id).ToList();
                                bidWrappersList.Add(wrapperClass);
                            }
                        }

                        wrapper.bids = bidWrappersList;
                        requestList.Add(wrapper);

                    }

                    apiWrapper.data = requestList;
                    apiWrapper.message = "success";
                    apiWrapper.status = "200";
                    apiWrapper.success = true;
                    apiWrapper.timeStamp = DateTime.Now.ToString();
                    return apiWrapper;
                }
                else
                {
                    apiWrapper.data = null;
                    apiWrapper.message = "No record found";
                    apiWrapper.status = "200";
                    apiWrapper.success = false;
                    apiWrapper.timeStamp = DateTime.Now.ToString();
                    return apiWrapper;

                }
            }
            catch (Exception ex)
            {

                APIModel apiModel = new APIModel();
                apiModel.data = null;
                apiModel.status = "400";
                apiModel.success = false;
                apiModel.timeStamp = DateTime.Now.ToString();
                apiModel.message = ex.Message;
                return apiModel;
            }
           
             
        }

        public APIModel GetUserRequest(string UID)
        {
            throw new NotImplementedException();
        }

        public APIModel SaveBid(BidsWrapperClass bidsWrapperClass)
        {
            APIModel apiModel = new APIModel();
            using (var dbContextTransaction = _mContext.Database.BeginTransaction())
            {
                try
                {
                    var bidRequest = _mContext.Requests.Where(x => x.ReqId.ToString() == bidsWrapperClass.RequestId).SingleOrDefault();
                    if (bidRequest != null)
                    {
                        BidAttributeBook books = new BidAttributeBook();

                        Bid bid = new Bid();
                        bid.RequestId = bidsWrapperClass.RequestId;
                        bid.Uid = bidsWrapperClass.Uid;
                        bid.StatusProgress = 20;
                        bid.Status = bidsWrapperClass.Status;
                        _mContext.Bids.Add(bid);
                        _mContext.SaveChanges();

                        foreach (var bidAttribute in bidsWrapperClass.bidAttributes)
                        {
                            BidAttribute bidAttribs = new BidAttribute();
                            bidAttribs.BidId = bid.Id.ToString();
                            bidAttribs.Name = bidAttribute.Name;
                            bidAttribs.UserId = bid.Uid;
                            bidAttribs.Value = bidAttribute.Value;

                            _mContext.BidAttributes.Add(bidAttribs);
                            _mContext.SaveChanges();
                        }

                        foreach (var book in bidsWrapperClass.Books)
                        {
                            books.BidId = bid.Id;
                            books.ImageUrl = book.ImageUrl;
                            books.Selected = book.Selected;
                            books.Isbn = book.Isbn;
                            books.BookTitle = book.BookTitle;
                            books.Author = book.Author;
                            books.Category = book.Category;
                            _mContext.BidAttributeBooks.Add(books);
                            _mContext.SaveChanges();
                        }


                        dbContextTransaction.Commit();
                        apiModel.data = "success";
                        apiModel.message = "Request saved successfully";
                        apiModel.status = "200";
                        apiModel.success = true;
                        apiModel.timeStamp = DateTime.Now.ToString();

                        return apiModel;

                    }
                    else
                    {
                        apiModel.data = null;
                        apiModel.status = "500";
                        apiModel.success = false;
                        apiModel.timeStamp = DateTime.Now.ToString();
                        apiModel.message = "Record Already exist";
                        return apiModel;
                    }
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    apiModel.data = null;
                    apiModel.status = "400";
                    apiModel.success = false;
                    apiModel.timeStamp = DateTime.Now.ToString();
                    apiModel.message = ex.Message;
                    return apiModel;

                }
            }
        }

        public APIModel SaveRequest(RequestWrapperClass requestWrapperClass)
        {
            APIModel apiWrapper = new APIModel();
            using (var dbContextTransaction = _mContext.Database.BeginTransaction())
            {
                try
                {
                    List<RequestWrapperClass> requestWrappers = new List<RequestWrapperClass>();
                    var request = _mContext.Requests.Where(x => x.ReqId == requestWrapperClass.ReqId).SingleOrDefault();
                    if (request == null)
                    {
                        Request saveReq = new Request();

                        saveReq.CreatedOn = DateTime.Now;
                        saveReq.ExpiresOn = DateTime.Now.AddDays(30);
                        saveReq.IsArchived = false; //requestWrapperClass.IsArchived;
                        saveReq.Latitude = requestWrapperClass.Latitude;
                        saveReq.Longitude = requestWrapperClass.Longitude;
                        saveReq.Uid = requestWrapperClass.Uid;
                        saveReq.UpdateAt = requestWrapperClass.UpdateAt;
                        saveReq.TypeId = requestWrapperClass.TypeId;
                        saveReq.Status = "Submitted";//requestWrapperClass.Status;
                        saveReq.IsActive = true; //requestWrapperClass.IsActive;

                        _mContext.Requests.Add(saveReq);
                        _mContext.SaveChanges();

                        foreach (var item in requestWrapperClass.requestAttributes)
                        {
                            RequestAttribute requestAttribute = new RequestAttribute();
                            requestAttribute.RequestId = saveReq.ReqId.ToString();
                            requestAttribute.Name = item.Name;
                            requestAttribute.Value = item.Value == null ? string.Empty : item.Value.Replace("\"", "");
                            _mContext.RequestAttributes.Add(requestAttribute);
                            _mContext.SaveChanges();
                        }

                        foreach (var sellBooks in requestWrapperClass.BooksForSell)
                        {
                            try
                            {
                                BookDatum requestBook = new BookDatum();
                                requestBook.Author = sellBooks.Author;
                                requestBook.BookTitle = sellBooks.BookTitle;
                                requestBook.Categories = sellBooks.Category;
                                requestBook.ImageUrl = sellBooks.ImageUrl == null ? string.Empty : sellBooks.ImageUrl.Replace("\"", ""); ;
                                requestBook.Isbn = sellBooks.ISBN;
                                requestBook.ReqId = saveReq.ReqId.ToString();
                                requestBook.TypeId = 1;
                                requestBook.Status = "Submitted";

                                _mContext.BookData.Add(requestBook);
                                _mContext.SaveChanges();
                            }
                            catch (Exception e)
                            {
                                dbContextTransaction.Rollback();
                                APIModel apiModel = new APIModel();
                                apiModel.data = null;
                                apiModel.status = "400";
                                apiModel.success = false;
                                apiModel.timeStamp = DateTime.Now.ToString();
                                apiModel.message = e.Message;
                                return apiModel;
                            }
                        }

                        foreach (var sellBooks in requestWrapperClass.BooksForExchange)
                        {
                            BookDatum requestBook = new BookDatum();
                            requestBook.Author = sellBooks.Author;
                            requestBook.BookTitle = sellBooks.BookTitle;
                            requestBook.Categories = sellBooks.Category;
                            requestBook.ImageUrl = sellBooks.ImageUrl.Replace("\"", ""); ;
                            requestBook.Isbn = sellBooks.ISBN;
                            requestBook.ReqId = saveReq.ReqId.ToString();
                            requestBook.TypeId = 2;
                            requestBook.Status = "Submitted";

                            requestWrappers.AddRange(GetSearchResult(sellBooks.ISBN, sellBooks.BookTitle, saveReq.ReqId.ToString()));

                            _mContext.BookData.Add(requestBook);
                            _mContext.SaveChanges();
                        }
                        
                        dbContextTransaction.Commit();
                        

                        apiWrapper.data = requestWrappers;
                        apiWrapper.message = "Request saved successfully";
                        apiWrapper.status = "200";
                        apiWrapper.success = true;
                        apiWrapper.timeStamp = DateTime.Now.ToString();

                        return apiWrapper;
                    }
                    else
                    {
                        apiWrapper.data = null;
                        apiWrapper.status = "500";
                        apiWrapper.success = false;
                        apiWrapper.timeStamp = DateTime.Now.ToString();
                        apiWrapper.message = "Record Already exist";
                        return apiWrapper;
                    }
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    APIModel apiModel = new APIModel();
                    apiModel.data = null;
                    apiModel.status = "400";
                    apiModel.success = false;
                    apiModel.timeStamp = DateTime.Now.ToString();
                    apiModel.message = ex.Message;
                    return apiModel;
                }
            }
        }

        public void CreateWorkOrders(Bid bid, Request request)
        {
            using (var dbContextTransaction = _mContext.Database.BeginTransaction()) {
                try
                {
                    WorkOrder workOrder = new WorkOrder();
                    workOrder.BidId = bid.Id;
                    workOrder.ReqId = request.ReqId;
                    workOrder.Status = "Created";
                    workOrder.Timestamp = DateTime.Now;

                    _mContext.WorkOrders.Add(workOrder);
                    _mContext.SaveChanges();

                    WorkOrderAttribute workOrderAttribute = new WorkOrderAttribute();
                    workOrderAttribute.Name = "DELIVERY REQUEST ACCEPTED";
                    workOrderAttribute.WorkOrderId = workOrder.Id;

                    _mContext.WorkOrderAttributes.Add(workOrderAttribute);
                    _mContext.SaveChanges();

                    workOrderAttribute = new WorkOrderAttribute();
                    workOrderAttribute.Name = "PICKED UP BY DELIVERY PERSON";
                    workOrderAttribute.WorkOrderId = workOrder.Id;

                    _mContext.WorkOrderAttributes.Add(workOrderAttribute);
                    _mContext.SaveChanges();

                    workOrderAttribute = new WorkOrderAttribute();
                    workOrderAttribute.Name = "PICKED UP BY DELIVERY PERSON";
                    workOrderAttribute.WorkOrderId = workOrder.Id;

                    _mContext.WorkOrderAttributes.Add(workOrderAttribute);
                    _mContext.SaveChanges();

                    workOrderAttribute = new WorkOrderAttribute();
                    workOrderAttribute.Name = "PAYMENT TRANSACTION COMPLETED";
                    workOrderAttribute.WorkOrderId = workOrder.Id;

                    _mContext.WorkOrderAttributes.Add(workOrderAttribute);
                    _mContext.SaveChanges();

                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    throw;
                }
            }
          
        }

        public List<RequestWrapperClass> GetSearchResult(string isbn, string bookTitle, string reqId)
        {
            try
            {
                List<RequestWrapperClass> requestWrapper = new List<RequestWrapperClass>();
                List<RequestAttribute> requestAttribute;
                APIModel apiModel = new APIModel();
                Request request;

                List<BookDatum> requestBook = _mContext.BookData.FromSqlRaw(
                    $"EXECUTE dbo.SearchBook '" + isbn + "','" + bookTitle + "','" + reqId + "'").ToList();
                List<string> reqIds = requestBook.Select(x => x.ReqId).Distinct().ToList();

                for (int i = 0; i < reqIds.Count; i++)
                {
                    request = _mContext.Requests.Where(x => Convert.ToString(x.ReqId) == reqIds[i]).FirstOrDefault();
                    requestAttribute = _mContext.RequestAttributes.Where(x => x.RequestId == reqIds[i]).ToList();

                    requestWrapper.Add(new RequestWrapperClass()
                    {

                        ReqId = request.ReqId,
                        Uid = request.Uid,
                        IsArchived = request.IsArchived,
                        TypeId = request.TypeId,
                        CreatedOn = request.CreatedOn,
                        UpdateAt = request.UpdateAt,
                        ExpiresOn = request.ExpiresOn,
                        Longitude = request.Longitude,
                        Latitude = request.Latitude,
                        Status = request.Status,
                        IsActive = request.IsActive,
                        bids = null,
                        requestAttributes = requestAttribute,
                        BooksForSell = null,
                        BooksForExchange = null

                    });
                }
                return requestWrapper;

                //return new List<RequestWrapperClass>();
            }
            catch (Exception)
            {
                return new List<RequestWrapperClass>();
            }
        }

        public APIModel GetAllRequestForAdmin()
        {
            APIModel apiWrapper = new APIModel();
            List<RequestWrapperClass> requestList = new List<RequestWrapperClass>();

            try
            {
                var request = _mContext.Requests.ToList();
                if (request != null)
                {
                    foreach (var req in request)
                    {
                        RequestWrapperClass wrapper = new RequestWrapperClass();
                        wrapper.Uid = req.Uid;
                        wrapper.Longitude = req.Longitude;
                        wrapper.Latitude = req.Latitude;
                        wrapper.Status = req.Status;
                        wrapper.UpdateAt = req.UpdateAt;
                        wrapper.CreatedOn = req.CreatedOn;
                        wrapper.ExpiresOn = req.ExpiresOn;
                        wrapper.IsActive = req.IsActive;
                        wrapper.IsArchived = req.IsArchived;
                        wrapper.TypeId = req.TypeId;
                        wrapper.ReqId = req.ReqId;

                        var requestAttribs = _mContext.RequestAttributes.Where(x => x.RequestId == req.ReqId.ToString()).ToList();

                        wrapper.requestAttributes = requestAttribs;

                        //foreach (var requestAttributes in requestAttribs)
                        //{
                        //    RequestAttribute requestAttributeobj = new RequestAttribute();
                        //    requestAttributeobj.Id = requestAttributes.Id;
                        //    requestAttributeobj.Name = requestAttributes.Name;
                        //    requestAttributeobj.Value = requestAttributes.Value;
                        //    requestAttributeobj.RequestId = requestAttributes.RequestId;


                        //    wrapper.requestAttributes.Add(requestAttributeobj);

                        //}


                        var bidWrappersList = new List<BidsWrapperClass>();
                        var bids = _mContext.Bids.Where(x => x.RequestId == req.ReqId.ToString()).ToList();
                        if (bids.Count > 0)
                        {
                            foreach (var item in bids)
                            {
                                BidsWrapperClass wrapperClass = new BidsWrapperClass();
                                wrapperClass.Id = item.Id;
                                wrapperClass.RequestId = item.RequestId;
                                wrapperClass.Status = item.Status;
                                wrapperClass.StatusProgress = item.StatusProgress;
                                wrapperClass.Uid = item.Uid;
                                wrapperClass.Comments = item.Comments;
                                var bidAttributesList = _mContext.BidAttributes.Where(x => x.BidId == item.Id.ToString()).ToList();
                                wrapperClass.bidAttributes = bidAttributesList;
                                bidWrappersList.Add(wrapperClass);
                            }
                        }

                        wrapper.bids = bidWrappersList;
                        requestList.Add(wrapper);

                    }

                    apiWrapper.data = requestList;
                    apiWrapper.message = "success";
                    apiWrapper.status = "200";
                    apiWrapper.success = true;
                    apiWrapper.timeStamp = DateTime.Now.ToString();
                    return apiWrapper;
                }
                else
                {
                    apiWrapper.data = null;
                    apiWrapper.message = "No record found";
                    apiWrapper.status = "200";
                    apiWrapper.success = false;
                    apiWrapper.timeStamp = DateTime.Now.ToString();
                    return apiWrapper;

                }
            }
            catch (Exception ex)
            {

                APIModel apiModel = new APIModel();
                apiModel.data = null;
                apiModel.status = "400";
                apiModel.success = false;
                apiModel.timeStamp = DateTime.Now.ToString();
                apiModel.message = ex.Message;
                return apiModel;
            }
        }

        private bool CreateRequestDelivery(Bid bid, Request request)
        {
            using (var dbContextTransaction = _mContext.Database.BeginTransaction())
            {
                try
                {
                    var reqDeliveryAttribs = new List<RequestDeliveryAttribute>();
                    var requestDelivery = new RequestDelivery();
                    if (request != null)
                    {
                        var requestDeliveryContext = _mContext.RequestDeliveries.Where(x => x.RequestId == request.ReqId).SingleOrDefault();

                        if (requestDeliveryContext == null)
                        {
                            requestDelivery.RequestId = Convert.ToInt16(request.ReqId);
                            //requestDelivery.BidId = bid.Id;
                            requestDelivery.Timestamp = DateTime.Now;
                            requestDelivery.DeliveryDate = DateTime.Now.AddDays(5);
                            requestDelivery.Status = RequestDeliveryStatus.CREATED.ToString();

                            _mContext.RequestDeliveries.Add(requestDelivery);
                            _mContext.SaveChanges();

                           
                        }

                        reqDeliveryAttribs.Add(new RequestDeliveryAttribute()
                        {
                            RequestDeliveryId = requestDelivery.Id,
                            BidId = bid.Id,
                            Status = requestDelivery.Status,
                            PickupAddress = _mContext.RequestAttributes.Where(x => x.RequestId == request.ReqId.ToString()).Where(x => x.Name == "AdLocation_").First().Value,
                            PickupLat = Convert.ToDouble(_mContext.RequestAttributes.Where(x => x.RequestId == request.ReqId.ToString()).Where(x => x.Name == "AdLatitute_").First().Value),
                            PickupLong = Convert.ToDouble(_mContext.RequestAttributes.Where(x => x.RequestId == request.ReqId.ToString()).Where(x => x.Name == "AdLongitude_").First().Value),
                            DropOffAddress = _mContext.BidAttributes.Where(x => x.BidId == bid.Id.ToString()).Where(x => x.Name == "AdLocation_").First().Value,
                            DropOffLat = Convert.ToDouble(_mContext.BidAttributes.Where(x => x.BidId == bid.Id.ToString()).Where(x => x.Name == "AdLatitute_").First().Value),
                            DropOffLong = Convert.ToDouble(_mContext.BidAttributes.Where(x => x.BidId == bid.Id.ToString()).Where(x => x.Name == "AdLongitude_").First().Value),
                        });



                        _mContext.RequestDeliveryAttributes.AddRange(reqDeliveryAttribs);
                        _mContext.SaveChanges();
                        dbContextTransaction.Commit();
                        return true;
                    }
                    dbContextTransaction.Rollback();
                    return false;
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    return false;
                }
            }
        }
    }

    enum RequestDeliveryStatus
    {
        CREATED,
        ASSIGNED,
        COMPLETED
    }
}
