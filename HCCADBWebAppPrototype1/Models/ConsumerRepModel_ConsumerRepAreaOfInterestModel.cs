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
    public class ConsumerRepModel_ConsumerRepAreaOfInterestModel
    {
        public int ConsumerRepModel_ConsumerRepAreaOfInterestModelID { get; set; }

        public int ConsumerRepModelID { get; set; }

        public virtual ConsumerRepModel ConsumerRepModel { get; set; }

        public virtual ConsumerRepAreaOfInterestModel ConsumerRepAreaOfInterestModel { get; set; }

    }
}