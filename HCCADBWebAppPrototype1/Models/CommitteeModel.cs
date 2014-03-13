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
    public class CommitteeModel
    {
        [Key]
        public int CommitteeId { get; set; }
        
        [Display(Name="Committee Name")]
        public string CommitteeName { get; set; }

        [Display(Name="Area Of Health")]
        public string HealthArea { get; set; }

        [Display(Name="Status")]
        public int Status { get; set; }
    }

    public class HCCADbContext : DbContext
    {
        public DbSet<CommitteeModel> Committees { get; set; }
    }
}