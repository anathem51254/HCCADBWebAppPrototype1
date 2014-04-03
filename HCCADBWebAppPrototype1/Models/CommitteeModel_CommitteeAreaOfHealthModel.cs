using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCCADBWebAppPrototype1.Models
{
    public class CommitteeModel_CommitteeAreaOfHealthModel
    {
        public int CommitteeModel_CommitteeAreaOfHealthModelID { get; set; }

        public int CommitteeModel_CommitteeModelID { get; set; }

        public virtual CommitteeModel CommitteeModel { get; set; }

        public virtual CommitteeAreaOfHealthModel CommitteeAreaOfHealthModel { get; set; }
    }
}