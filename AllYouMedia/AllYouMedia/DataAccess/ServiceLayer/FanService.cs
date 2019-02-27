namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;
    using System.Data.Entity;

    public class FanService : IFanService
    {
        private IRepository<Fan> entityRepository;
        private IRepository<FanUserMap> fanUserMapRepository;
        private IRepository<FanSharingUserRequest> fanSharingUserRequestRepository;
        private IRepository<FanSharingFanRequest> fanSharingFanRequestRepository;

        public FanService(IRepository<Fan> entityRepository, IRepository<FanUserMap> fanUserMapRepository, IRepository<FanSharingUserRequest> fanSharingUserRequestRepository, IRepository<FanSharingFanRequest> fanSharingFanRequestRepository)
        {
            this.entityRepository = entityRepository;
            this.fanUserMapRepository = fanUserMapRepository;
            this.fanSharingUserRequestRepository = fanSharingUserRequestRepository;
            this.fanSharingFanRequestRepository = fanSharingFanRequestRepository;
        }

        public List<Fan> GetAll()
        {
            return this.entityRepository.GetAll().ToList();
        }

        public Fan GetById(long id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("entity");
            }

            return this.entityRepository.GetById(id);
        }

        public void Insert(Fan model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Insert(model);
        }

        public void Update(Fan model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Update(model);
        }

        public void Delete(Fan model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Delete(model);
        }

        public int GetFanCount(long AspNetUserID)
        {
            return this.fanUserMapRepository.GetByQuery(x => x.AspNetUserID == AspNetUserID && x.IsActive).Count();
        }
        public Tuple<bool, string> BecomeFan(long ID, string Name, string Email, int Rating)
        {
            Email = Email.Trim();
            Name = Name.Trim();
            var fan = this.entityRepository.GetByQuery(x => x.Email.ToLower() == Email.ToLower()).FirstOrDefault();
            if (fan == null)
            {
                fan = new Fan() { Email = Email, IsActive = true, IsEmailConfirmed = false, Name = Name };
                fan = this.entityRepository.Insert(fan);
                this.fanUserMapRepository.Insert(new FanUserMap() { AspNetUserID = ID, FanID = fan.ID, IsActive = true, Rating = Rating });
                return new Tuple<bool, string>(true, "Added to Fan successfully.");
            }
            else
            {
                var fanUserMap = this.fanUserMapRepository.GetByQuery(x => x.FanID == fan.ID && x.AspNetUserID == ID).FirstOrDefault();
                if (fanUserMap == null)
                {
                    this.fanUserMapRepository.Insert(new FanUserMap() { AspNetUserID = ID, FanID = fan.ID, IsActive = true, Rating = Rating });
                    return new Tuple<bool, string>(true, "Added to Fan successfully.");
                }
                else
                {
                    fanUserMap.Rating = Rating;
                    this.fanUserMapRepository.Update(fanUserMap);
                    return new Tuple<bool, string>(true, "You are already a Fan, rating updated.");
                }
            }
        }

        public double GetFanRating(long ID)
        {
            return this.fanUserMapRepository.GetByQuery(x => x.AspNetUserID == ID && x.Rating > 0).Select(x => x.Rating).DefaultIfEmpty().Average();
            //var avg = this.fanUserMapRepository.GetByQuery(x => x.AspNetUserID == ID && x.Rating > 0).Select(x => x.Rating).DefaultIfEmpty().Average();
            //return Convert.ToInt32(Math.Ceiling(avg));
        }
        public Tuple<bool, string> GenerateFanShareRequestToUser(long RequestedFromAspNetUserID, long RequestedToAspNetUserID)
        {
            var request = this.fanSharingUserRequestRepository.GetByQuery(x => x.AspNetUserID == RequestedToAspNetUserID && x.RequestingAspNetUserID == RequestedFromAspNetUserID).FirstOrDefault();
            if (request == null)
            {
                request = new FanSharingUserRequest { AspNetUserID = RequestedToAspNetUserID, IsGranted = false, RequestingAspNetUserID = RequestedFromAspNetUserID };
                this.fanSharingUserRequestRepository.Insert(request);
                request = this.fanSharingUserRequestRepository.GetByAction(x => x.Include(y => y.RequestingAspNetUser).Include(y => y.AspNetUser)).Where(x => x.AspNetUserID == RequestedToAspNetUserID && x.RequestingAspNetUserID == RequestedFromAspNetUserID).First();
                new AllYouMedia.Mailers.AllYouMediaMailer().FanSharingRequestToUser(request.AspNetUser.UserName, request.AspNetUser.Name, request.RequestingAspNetUser.UserName, request.RequestingAspNetUser.Name).SendAsync();
                return new Tuple<bool, string>(true, "Your request is submitted to user. Please wait for acceptance.");
            }
            else
                return new Tuple<bool, string>(false, "Your request is already submitted to user. Please wait for acceptance.");
        }
        public int GetFanSharingRequestCount(long AspNetUserID)
        {
            return this.fanSharingUserRequestRepository.GetByQuery(x => x.AspNetUserID == AspNetUserID && x.GrantedOn == null).Count();
        }

        public Tuple<dynamic, int> GetFanSharingUserRequestForDT(long AspNetUserID, string search, int start, int length)
        {
            var queriable = this.fanSharingUserRequestRepository.GetByAction(x => x.Include(y => y.AspNetUser).Include(y => y.RequestingAspNetUser)).Where(x => (x.AspNetUser.Name.Contains(search) || x.AspNetUser.Email.Contains(search) || x.RequestingAspNetUser.Name.Contains(search) || x.RequestingAspNetUser.Email.Contains(search)) && x.ID != -1 && x.AspNetUserID == AspNetUserID && x.GrantedOn == null);
            int totalRecord = queriable.Count();
            return new Tuple<dynamic, int>(queriable.ToList().Select(x => new
            {
                DT_RowId = x.ID,
                RequestedBy = x.RequestingAspNetUser.Name + " (" + x.RequestingAspNetUser.Name + ")",
                RequestedOn = x.CreatedOn.ToString("dd-MM-yyyy")
            }).ToList(), totalRecord);
        }
        public void UpdateFanSharingUserRequest(long ID, bool IsAccepted)
        {
            var fanSharingReq = this.fanSharingUserRequestRepository.GetByAction(x => x.Include(y => y.RequestingAspNetUser)).FirstOrDefault(x => x.ID == ID);
            fanSharingReq.IsGranted = IsAccepted; fanSharingReq.GrantedOn = DateTime.Now;
            this.fanSharingUserRequestRepository.Update(fanSharingReq);
            if (IsAccepted)
            {
                var lstFans = this.fanUserMapRepository.GetByAction(x => x.Include(y => y.Fan)).Where(x => x.AspNetUserID == fanSharingReq.AspNetUserID).Select(x => x.Fan);
                var mailer = new AllYouMedia.Mailers.AllYouMediaMailer() { };
                foreach (var fan in lstFans)
                {
                    fanSharingFanRequestRepository.Insert(new FanSharingFanRequest { AspNetUserID = fanSharingReq.RequestingAspNetUserID, FanID = fan.ID, IsGranted = false });
                    mailer.FanSharingRequestToFan(fan.Email, fan.Name, fanSharingReq.RequestingAspNetUser.Name, string.Format("https://allyoumedia.com/bio/{0}", fanSharingReq.RequestingAspNetUserID)).SendAsync();
                }
            }
        }
    }
}