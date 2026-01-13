using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement
{
    // Represents a Student entity
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("School")]
        public int SchoolId { get; set; }

        public School School { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Full name must be between 2 and 100 characters")]
        public string FullName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string StudentIdentifier { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [RegularExpression("^\\d{10,11}$", ErrorMessage = "Phone must be 10 or 11 digits")]
        public string Phone { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
