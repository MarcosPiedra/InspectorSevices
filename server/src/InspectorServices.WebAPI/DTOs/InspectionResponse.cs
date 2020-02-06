using System;
using System.Collections.Generic;
using System.Text;

namespace InspectorServices.WebAPI.DTOs
{
    public class InspectionResponse
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Customer { get; set; }
        public string Address { get; set; }
        public string Observations { get; set; }
        public int Status { get; set; }
    }
}
