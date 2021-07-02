using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SWYFT_CRM.Models
{
    public class Lead
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(255)]
        public string Email { get; set; }
        public int LeadStatusId { get; set; }
        public LeadStatus LeadStatus { get; set; }
        public DateTime DateCreated { get; set; }

        [Required]
        public int UserProfileId { get; set; }

        [Required]
        public bool Client { get; set; }
        public bool? Sold { get; set; }
        public DateTime? SoldDate { get; set; }

        public int CoverageTypeId { get; set; }
        public CoverageType CoverageType { get; set; }

        public int InsuranceCompanyId { get; set; }
        public InsuranceCompany InsuranceCompany { get; set; }

        public DateTime CreateDateTime { get; set; }
        public UserProfile UserProfile { get; set; }

        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}