using System;
using System.Collections.Generic;
using System.Text;

namespace InspectorServices.WebAPI.DTOs
{
    public class InspectionRequest
    {
        public string Date { get; set; }
        public string Customer { get; set; }
        public string Address { get; set; }
        public string Observations { get; set; }
        public int Status { get; set; }
        public int InspectorId { get; internal set; }
    }
}
