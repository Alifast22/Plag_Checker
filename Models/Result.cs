using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PlagChecker.Models
{
    public class Result
    {
        [Key]
        public int ResultID { get; set; }
        public string Studen_RollNo { get; set; }
        public string Plag_RollNo { get; set; }
        public int PlagPercentage { get; set; }
    }
}