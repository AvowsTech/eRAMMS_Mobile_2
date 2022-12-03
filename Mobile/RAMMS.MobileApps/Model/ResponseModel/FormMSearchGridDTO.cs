using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.MobileApps.Model.ResponseModel
{
    public class FormMSearchGridDTO
    {
        public int RefNo { get; set; }
        public string RefID { get; set; }
        public DateTime? AuditDate { get; set; }
        public int? Year { get; set; }
        public string RMUCode { get; set; }
        public string RMUDesc { get; set; }
        public string SecCode { get; set; }
        public string SecName { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }
        public double? RoadId { get; set; }
        public string ActivityCode { get; set; }
        public string ByFromdate { get; set; }
        public string ByTodate { get; set; }
        public string ClosureType { get; set; }
        public string Method { get; set; }
        public string AuditedBy {get; set; }
        public string WitnessedBy { get; set; }

        //public string SmartInputValue { get; set; }
        //public string sortOrder { get; set; }
        //public string currentFilter { get; set; }
        //public string searchString { get; set; }
        public int? Page_No { get; set; }
        public int? pageSize { get; set; }
        public bool? Active { get; set; }
        public string Status { get; set; }
        public int SNo { get; set; }
    }
}
