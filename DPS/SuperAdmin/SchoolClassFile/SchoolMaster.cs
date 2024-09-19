using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DPS.SuperAdmin.SchoolClassFile
{
    public class SchoolMaster
    {
        public int Id { get; set; } = default;
        public string Name { get; set; }=string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int IdState { get; set; }=default;
        public string Country { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Pincode { get; set; } = string.Empty;
        public string EmailId { get; set; } = string.Empty;
        public string Logo { get; set; } = string.Empty;
        public string IdDatabase { get; set; } = string.Empty;
        public bool IsActive { get; set; } = default;
        public bool IsSyncronized { get; set; } = default;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = default;
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedOn { get; set; } = default;
        public string DeletedBy { get; set; } = string.Empty;
        public DateTime? DeletedOn { get; set; } = default;
        public bool IsDeleted { get; set; } = default;
    }
}