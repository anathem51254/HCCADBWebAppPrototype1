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
    public class ConsumerRepAreaOfInterestModel
    {
        public int ConsumerRepAreaOfInterestModelID { get; set; }

        public string AreaOfInterestName { get; set; }

        public virtual ConsumerRepModel ConsumerRepModel { get; set; }
    }
}