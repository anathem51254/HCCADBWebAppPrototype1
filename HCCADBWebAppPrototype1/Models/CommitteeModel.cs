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
    public enum CurrentStatus { Active, InActive };

    public class CommitteeModel
    {
        public int CommitteeModelID { get; set; }
        
        [Display(Name="Committee Name")]
        public string CommitteeName { get; set; }

        [Display(Name="Status")]
        public CurrentStatus? CurrentStatus { get; set; }

        public virtual ICollection<CommitteeModel_CommitteeAreaOfHealthModel> CommitteeModel_CommitteeAreaOfHealthModels { get; set; }

        public virtual ICollection<ConsumerRepCommitteeHistoryModel> ConsumerRepsCommitteeHistoryModels { get; set; }
    }
}