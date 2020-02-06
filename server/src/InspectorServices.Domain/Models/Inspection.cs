using System;
using System.Collections.Generic;
using System.Text;

namespace InspectorServices.Domain.Models
{
    public enum Status
    {
        New = 0,
        InProgress = 1,
        Done = 2,
    }
    
    public class Inspection
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Customer { get; set; }
        public string Address { get; set; }
        public string Observations { get; set; }
        public string Status { get; set; }
    }
}
