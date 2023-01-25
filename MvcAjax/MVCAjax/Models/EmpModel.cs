using System;
using System.ComponentModel.DataAnnotations;

namespace EmpPayRollMVCAjax.Models
{
    public class EmpModel
    {
        [Key]
        public int EmpId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Department { get; set; }
        public string Profileimage { get; set; }
        public DateTime StartDate { get; set; }
        public double Salary { get; set; }
        public string Notes { get; set; }
    }
}
