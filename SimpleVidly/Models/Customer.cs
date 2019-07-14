using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SimpleVidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required] //to overrride error message [Required(ErrorMessage = "Please enter name.")]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        [Display(Name = "Date of Birth")]
        [Min18YearsIfSubscribed]
        public DateTime? Birthdate { get; set; }
        public MembershipType Membertype { get; set; }
    }

}