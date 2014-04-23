using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HCCADBWebAppPrototype1.Models;

namespace HCCADBWebAppPrototype1.ViewModels
{
    public class ConsumerRepAreaOfInterestIndexData
    {
        public IEnumerable<ConsumerRepModel> ConsumerReps { get; set; }
        public IEnumerable<ConsumerRepAreaOfInterestModel> ConsumerRepAreaOfInterests { get; set; }
    }
}