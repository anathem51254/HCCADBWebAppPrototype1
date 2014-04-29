using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCCADBWebAppPrototype1.Models
{
    public class ConsumerRepModel_ConsumerRepAreaOfInterestModel
    {
        public int ConsumerRepModel_ConsumerRepAreaOfInterestModelID { get; set; }

        public int ConsumerRepModelID { get; set; }

        public int ConsumerRepAreaOfInterestModelID { get; set; }

        public virtual ConsumerRepModel ConsumerRepModel { get; set; }

        public virtual ConsumerRepAreaOfInterestModel ConsumerRepAreaOfInterestModel { get; set; }

    }
}