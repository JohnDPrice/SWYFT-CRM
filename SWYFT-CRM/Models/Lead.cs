using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Tabloid.Models
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
        public DateTime DateOfBirth { get; set; }
        public int leadStatusId { get; set; }

        [Required]
        public int userId { get; set; }

        [Required]
        public bool client { get; set; }

        public int coverageTypeId { get; set; }

        public int insuranceCompanyId { get; set; }

        [Required]
        public DateTime CreateDateTime { get; set; }

        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}