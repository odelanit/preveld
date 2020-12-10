using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Preveld.Models
{
    [Table("wrap_inspection_db")]
    public class Wrap
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [DisplayName("Unique ID")]
        public string Unique_ID { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [DisplayName("Wrap No.")]
        public string Wrap_No { get; set; }

        [Required]
        public string Client { get; set; }

        [Required]
        [DisplayName("COR No")]
        public string COR_No { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Required]
        [DisplayName("Date of Last Inspection")]
        public DateTime Date_of_last_Inspection { get; set; }

        [Required]
        [DisplayName("Line No.")]
        public string Line_No { get; set; }

        [Required]
        public string Size { get; set; }

        [Required]
        [DisplayName("Preliminary Findings")]
        public string _Priliminary_findings_on_Site { get; set; }

        public string Status { get; set; }

        [Required]
        [DisplayName("Final Findings After Further Analysis")]
        public string Final_Findings { get; set; }

        public string Recommendation { get; set; }

    }
}