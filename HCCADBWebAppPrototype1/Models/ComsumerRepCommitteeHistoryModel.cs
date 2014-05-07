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
    public enum EndorsementType { Organisational, Standard };

    public class ConsumerRepCommitteeHistoryModel
    {
        public int ConsumerRepCommitteeHistoryModelID { get; set; }

        public int CommitteeModelID { get; set; }

        public int ConsumerRepModelID { get; set; }

        public int PrepTime { get; set; }

        public int Meetingtime { get; set; }

        public DateTime ReportedDate { get; set;  }

        public DateTime EndorsementDate { get; set; }

        public DateTime? FinishedDate { get; set; }

        public EndorsementType? EndorsementType { get; set; }

        public virtual ConsumerRepModel ConsumerRepModel { get; set; }

        public virtual CommitteeModel CommitteeModel { get; set; }
    }
}