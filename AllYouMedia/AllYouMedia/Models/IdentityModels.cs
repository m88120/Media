using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
namespace AllYouMedia.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class AspNetUser : IdentityUser<long, AspNetUserLogin, AspNetUserRole, AspNetUserClaim>
    {
        [Required, MaxLength(255)]
        public override string UserName { get; set; }

        [Required, MaxLength(255)]
        public override string Email { get; set; }

        // TODO: Add your own custom properties
        [StringLength(200)]
        public string PasswordOrignal { get; set; }
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(50)]
        public string CurrentIP { get; set; }

        [StringLength(50)]
        public string LastIP { get; set; }
        public DateTime CurrentLoginTime { get; set; }
        public DateTime LastLoginTime { get; set; }
        public int LoginCount { get; set; }
        public DateTime LastWindowTime { get; set; }

        [StringLength(50)]
        public string ResetCode { get; set; }
        public DateTime ResetDate { get; set; }

        [StringLength(200)]
        public string RefferCode { get; set; }
        public int Status { get; set; }
        public int FailedPasswordAttemptCount { get; set; }
        public DateTime FailedPasswordAttemptWindowStart { get; set; }

        [StringLength(2000)]
        public string PhotoIMG { get; set; }

        public string BioDescription { get; set; }

        public decimal EarningAmount { get; set; }

        public long UserLevelID { get; set; }
        public virtual AllYouMedia.DataAccess.EntityLayer.DBEntity.UserLevel UserLevel { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(AspNetUserManager userManager)
        {
            var userIdentity = await userManager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual ICollection<AllYouMedia.DataAccess.EntityLayer.DBEntity.AspNetUserHierarchy> AspNetUser_AspNetUserHierarchy { get; set; }
        public virtual ICollection<AllYouMedia.DataAccess.EntityLayer.DBEntity.AspNetUserHierarchy> ParentAspNetUser_AspNetUserHierarchy { get; set; }

        public virtual ICollection<AllYouMedia.DataAccess.EntityLayer.DBEntity.AspNetUserConnection> AspNetUser_AspNetUserConnection { get; set; }
        public virtual ICollection<AllYouMedia.DataAccess.EntityLayer.DBEntity.AspNetUserConnection> ConnectedAspNetUser_AspNetUserConnection { get; set; }

        public virtual ICollection<AllYouMedia.DataAccess.EntityLayer.DBEntity.FanSharingUserRequest> AspNetUser_FanSharingRequests { get; set; }
        public virtual ICollection<AllYouMedia.DataAccess.EntityLayer.DBEntity.FanSharingUserRequest> RequestingAspNetUser_FanSharingRequests { get; set; }

        public virtual ICollection<AllYouMedia.DataAccess.EntityLayer.DBEntity.UserSpotlight> AspNetUser_UserSpotlights { get; set; }
        public virtual ICollection<AllYouMedia.DataAccess.EntityLayer.DBEntity.UserSpotlight> ReviewingAspNetUser_UserSpotlights { get; set; }

        public virtual ICollection<AllYouMedia.DataAccess.EntityLayer.DBEntity.FanSharingFanRequest> FanSharingFanRequests { get; set; }
        public virtual ICollection<AllYouMedia.DataAccess.EntityLayer.DBEntity.AspNetUserAddress> AspNetUserAddresses { get; set; }
        public virtual ICollection<AllYouMedia.DataAccess.EntityLayer.DBEntity.Item> Items { get; set; }
        public virtual ICollection<AllYouMedia.DataAccess.EntityLayer.DBEntity.FanUserMap> FanUserMaps { get; set; }
    }
    public class AspNetRole : IdentityRole<long, AspNetUserRole> { }
    public class AspNetUserRole : IdentityUserRole<long>
    {
        public long ID { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public virtual ICollection<AllYouMedia.DataAccess.EntityLayer.DBEntity.UserCategoryMap> UserCategoryMaps { get; set; }
    }
    public class AspNetUserLogin : IdentityUserLogin<long> { }
    public class AspNetUserClaim : IdentityUserClaim<long> { }
    public class AspNetUserManager : UserManager<AspNetUser, long>
    {
        public AspNetUserManager(IUserStore<AspNetUser, long> store)
            : base(store)
        {
            UserValidator = new UserValidator<AspNetUser, long>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
            };

            PasswordValidator = new PasswordValidator
            {

                // TODO: Add your own custom logic
            };

            //PasswordHasher = new TodoPasswordHasher();
        }

        public override Task<AspNetUser> FindAsync(UserLoginInfo login)
        {
            return base.FindAsync(login);
        }
    }
}