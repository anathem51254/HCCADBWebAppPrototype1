using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HCCADBWebAppPrototype1.Models
{
    public enum MemberStatus { Yes, No };

    public class ConsumerRepModel
    {
        public int ConsumerRepModelID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Member Status")] 
        public MemberStatus? MemberStatus { get; set; }

        [Display(Name = "Date Trained")] // month - year
        public DateTime DateTrained { get; set; }

        public virtual ICollection<ConsumerRepModel_ConsumerRepAreaOfInterestModel> ConsumerRepModel_ConsumerRepAreasOfInterestModels { get; set; }

        public virtual ICollection<ConsumerRepCommitteeHistoryModel> ConsumerRepsCommitteeHistoryModels { get; set; }

    }
}