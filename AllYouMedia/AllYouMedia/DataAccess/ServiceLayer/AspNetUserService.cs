namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;
    using AllYouMedia.Models;
    using System.Data.Entity;

    public class AspNetUserService : IAspNetUserService
    {
        private IRepository<AspNetUser> entityRepository;
        private IRepository<Log> userLogRepository;
        private IRepository<AspNetUserHierarchy> userHierarchyRepository;
        private IRepository<AspNetUserRole> aspNetUserRoleRepository;
        private IRepository<AspNetRole> aspNetRoleRepository;
        private IRepository<UserCategoryMap> userCategoryMapRepository;

        public AspNetUserService(IRepository<AspNetUser> entityRepository, IRepository<Log> userLogRepository, IRepository<AspNetUserHierarchy> userHierarchyRepository, IRepository<AspNetUserRole> aspNetUserRoleRepository, IRepository<AspNetRole> aspNetRoleRepository, IRepository<UserCategoryMap> userCategoryMapRepository)
        {
            this.entityRepository = entityRepository;
            this.userLogRepository = userLogRepository;
            this.userHierarchyRepository = userHierarchyRepository;
            this.aspNetRoleRepository = aspNetRoleRepository;
            this.aspNetUserRoleRepository = aspNetUserRoleRepository;
            this.userCategoryMapRepository = userCategoryMapRepository;
        }

        public List<AspNetUser> GetAll()
        {
            return this.entityRepository.GetAll().ToList();
        }

        public AspNetUser GetById(long id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("entity");
            }

            return this.entityRepository.GetById(id);
        }

        public void Insert(AspNetUser model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Insert(model);
        }

        public void Update(AspNetUser model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Update(model);
        }

        public void Delete(AspNetUser model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.entityRepository.Delete(model);
        }

        public Tuple<string, AspNetUser> UserLoginChecknUpdate(string UserName, string CurrentIP)
        {
            var matchedRecords = this.entityRepository.GetByQuery(x => x.UserName == UserName && x.IsActive);
            if (matchedRecords.Count() > 0)
            {
                var user = matchedRecords.First();
                user.LastLoginTime = user.CurrentLoginTime;
                user.LastIP = user.CurrentIP;
                user.CurrentLoginTime = DateTime.Now;
                user.CurrentIP = CurrentIP;
                user.FailedPasswordAttemptCount = 0;
                this.entityRepository.Update(user);

                this.userLogRepository.Insert(new Log
                {
                    AspNetUserID = user.Id,
                    ActivityType = "Login",
                    IPAddress = CurrentIP,
                    Description = "User logged in successfully."
                });
                return new Tuple<string, AspNetUser>(null, user);
            }
            else
                return new Tuple<string, AspNetUser>("User not found or is inactive.", null); ;
        }

        public long GetAspNetUserIDByUserName(string UserName)
        {
            return this.entityRepository.GetByQuery(x => x.UserName == UserName).First().Id;
        }

        public long GetAspNetUserIDByRefferCode(string RefferCode)
        {
            long aspNetUserID = -1;
            var aspNetUser = this.entityRepository.GetByQuery(x => x.RefferCode == RefferCode).FirstOrDefault();
            if (aspNetUser != null) aspNetUserID = aspNetUser.Id;
            return aspNetUserID;
        }
        public void AddToAspNetUserHierarchy(long AspNetUserID, long ParentAspNetUserID)
        {
            this.userHierarchyRepository.Insert(new AspNetUserHierarchy { AspNetUserID = AspNetUserID, ParentAspNetUserID = ParentAspNetUserID });
        }
        public UserTree GetUserTree(long AspNetUserID)
        {
            var userTree = new UserTree();
            var selfUser = entityRepository.GetById(AspNetUserID);
            userTree.TreeNode.Name = selfUser.Name;
            userTree.TreeNode.UserName = selfUser.UserName;
            userTree.TreeNode.AspNetUserID = selfUser.Id;
            userTree.TreeNode.ChildNodes = EnumrateChildRecursivly(AspNetUserID);
            return userTree;
        }
        private List<UserNode> EnumrateChildRecursivly(long AspNetUserID)
        {
            List<AspNetUser> childUsers = userHierarchyRepository.GetByQuery(x => x.ParentAspNetUserID == AspNetUserID && x.ParentAspNetUserID != -1).Select(x => x.AspNetUser).ToList();
            var childNodeList = new List<UserNode>();
            for (var i = 0; i < childUsers.Count(); i++)
            {
                var childNode = new UserNode { Name = childUsers[i].Name, UserName = childUsers[i].UserName, AspNetUserID = childUsers[i].Id };
                childNode.ChildNodes = EnumrateChildRecursivly(childUsers[i].Id);
                childNodeList.Add(childNode);
            }
            return childNodeList;
        }

        public Tuple<List<AspNetUser>, int> GetTalentSearchForDT(long AspNetUserID, long CategoryTypeID, long CategoryID, long SubCategoryID, long AttributeID, string search, int start, int length)
        {
            var queriable = this.entityRepository.GetByAction(x => x.Include(y => y.AspNetUserAddresses).Include(y => y.AspNetUser_AspNetUserConnection)).Where(x => x.IsActive && x.EmailConfirmed && x.Id != AspNetUserID && (x.Name.Contains(search) || x.UserName.Contains(search)));
            if (CategoryTypeID != -1)
            {
                var aspNetUserIDs = userCategoryMapRepository.GetByQuery(x => x.CategoryTypeID == CategoryTypeID).Select(x => x.AspNetUserRole.UserId).ToList();
                queriable = queriable.Where(x => aspNetUserIDs.Contains(x.Id));
            }
            if (CategoryID != -1)
            {
                var aspNetUserIDs = userCategoryMapRepository.GetByQuery(x => x.CategoryTypeID == CategoryTypeID && x.CategoryID == CategoryID).Select(x => x.AspNetUserRole.UserId).ToList();
                queriable = queriable.Where(x => aspNetUserIDs.Contains(x.Id));
            }
            if (SubCategoryID != -1)
            {
                var aspNetUserIDs = userCategoryMapRepository.GetByQuery(x => x.CategoryTypeID == CategoryTypeID && x.CategoryID == CategoryID && x.SubCategoryID == SubCategoryID).Select(x => x.AspNetUserRole.UserId).ToList();
                queriable = queriable.Where(x => aspNetUserIDs.Contains(x.Id));
            }
            if (AttributeID != -1)
            {
                var aspNetUserIDs = userCategoryMapRepository.GetByQuery(x => x.CategoryTypeID == CategoryTypeID && x.CategoryID == CategoryID && x.SubCategoryID == SubCategoryID && x.AttributeID == AttributeID).Select(x => x.AspNetUserRole.UserId).ToList();
                queriable = queriable.Where(x => aspNetUserIDs.Contains(x.Id));
            }
            var userIDsInValidRoles = aspNetUserRoleRepository.GetByQuery(x => x.RoleId == 2 || x.RoleId == 3 || x.RoleId == 4).Select(x => x.UserId).ToList();
            queriable = queriable.Where(x => userIDsInValidRoles.Contains(x.Id));
            int totalRecord = queriable.Count();
            return new Tuple<List<AspNetUser>, int>(queriable.ToList(), totalRecord);
        }
        public long GetAspNetUserRoleIDByUserAndRole(long AspNetUserID, string Role)
        {
            var RoleID = aspNetRoleRepository.GetByQuery(x => x.Name == Role).First().Id;
            return aspNetUserRoleRepository.GetByQuery(x => x.UserId == AspNetUserID && x.RoleId == RoleID).First().ID;
        }
        public Dictionary<long, string> GetRolesDictionary()
        {
            return this.aspNetRoleRepository.GetByQuery(x => 1 == 1).ToDictionary(x => x.Id, x => x.Name);
        }

        public Dictionary<string, string> GetUserDetails(long AspNetUserID)
        {
            //LEVELNAME = "FANATIC",
            //USERNAME = user.UserName,
            //RECRUITER = "",
            //RECRUITERNAME = "",
            //TOTALONTEAM = "",
            //GROSSPROFITS = "",
            //NETPROFITTOYOU = ""
            var userDetails = entityRepository.GetById(AspNetUserID);
            var parentAspNetUser = userHierarchyRepository.GetByAction(x => x.Include(y => y.ParentAspNetUser)).Where(x => x.AspNetUserID == AspNetUserID).First().ParentAspNetUser;
            var lstUserIDs = GetTeamMemberUserIDsOfUser(GetUserTree(AspNetUserID).TreeNode, new List<long>());
            Dictionary<string, string> dicUserDetails = new Dictionary<string, string>();
            dicUserDetails.Add("LEVELNAME", userDetails.UserLevel.Name);
            dicUserDetails.Add("USERNAME", userDetails.UserName);
            dicUserDetails.Add("RECRUITER", parentAspNetUser.UserName);
            dicUserDetails.Add("RECRUITERNAME", parentAspNetUser.Name);
            dicUserDetails.Add("TOTALONTEAM", lstUserIDs.Count.ToString());
            dicUserDetails.Add("GROSSPROFITS", entityRepository.GetByQuery(x => lstUserIDs.Contains(x.Id)).Sum(x => x.EarningAmount).ToString("N2"));
            dicUserDetails.Add("NETPROFITTOYOU", userDetails.EarningAmount.ToString("N2"));
            return dicUserDetails;
        }
        private List<long> GetTeamMemberUserIDsOfUser(UserNode treeNode, List<long> lstUserIDs)
        {
            if (treeNode != null)
            {
                lstUserIDs.Add(treeNode.AspNetUserID);
                foreach (var node in treeNode.ChildNodes)
                {
                    lstUserIDs = GetTeamMemberUserIDsOfUser(node, lstUserIDs);
                }
            }
            return lstUserIDs;
        }
    }
}