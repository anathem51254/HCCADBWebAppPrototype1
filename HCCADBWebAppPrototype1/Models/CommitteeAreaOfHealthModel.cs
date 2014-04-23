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
    public class CommitteeAreaOfHealthModel
    {
        public int CommitteeAreaOfHealthModelID { get; set; }

        public string AreaOfHealthName { get; set; }

        //public virtual ICollection<CommitteeModel_CommitteeAreaOfHealthModel> CommitteeAreaOfHealthModels { get; set; }
        public virtual CommitteeModel CommitteeModel { get; set; }

    }
}