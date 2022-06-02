using System;
using System.Collections.Generic;

#nullable disable

namespace BOOKCLUB_API.Models
{
    public partial class AppUser
    {
        public long Id { get; set; }
        public string Uid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Provider { get; set; }
        public string ProfileImageUrl { get; set; }
        public string RoleId { get; set; }
        public bool? IsActive { get; set; }
        public string PaymentProviderCustomerId { get; set; }
        public string AuthUserId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
