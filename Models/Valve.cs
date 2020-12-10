using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Preveld.Models
{
    [Table("valve_inspection_db")]
    public class Valve
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [DisplayName("Unique ID")]
        public string Unique_ID { get; set; }

        [Required]
        [DisplayName("Valve Tag")]
        public string Valve_tag { get; set; }

        [DisplayName("Description")]
        public string Valve_description { get; set; }

        [Required]
        public string Client { get; set; }

        [Required]
        public string Location { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        [Required]
        [DisplayName("Date of Last Inspection")]
        public DateTime Date_of_Inspection { get; set; }

        [Required]
        [DisplayName("COR No")]
        public string COR_No { get; set; }

        [Required]
        public string Function { get; set; }

        [Required]
        [DisplayName("Size")]
        public double Size_Inch { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        [DisplayName("PID No")]
        public string PID_No { get; set; }

        public string PID_Description { get; set; }

        [Required]
        [DisplayName("Pup")]
        public string Pressure_UP { get; set; }

        [Required]
        [DisplayName("Pdown")]
        public string Pressure_Down { get; set; }

        [Required]
        [DisplayName("Valve Condition")]
        public string LeaK_Condition { get; set; }

        [Required]
        [DisplayName("ASL (dB)")]
        public string ASL_dB { get; set; }

        [Required]
        [DisplayName("Leak Classification")]
        public string Leak_Classification { get; set; }

        [Required]
        [DisplayName("Leak Rage (kg/min)")]
        public string Leak_Rate_kgmin { get; set; }

        [Required]
        [DisplayName("Color Code")]
        public string Leak_Color_Code { get; set; }

        public string Comment { get; set; }

        [Required]
        [DisplayName("TLT Value (kg/s)")]
        public string Tolerable_Leak_Threshold_TLT { get; set; }

        [Required]
        [DisplayName("Warning TLT (kg/s)")]
        public string Warning_Limit_TLT { get; set; }

        [Required]
        [DisplayName("Recommended Action")]
        public string Recommended_Action { get; set; }
    }
}