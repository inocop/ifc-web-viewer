using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ifc_web_viewer.Models
{
    public class UploadFile
    {
        public long Id { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "ファイル名")]
        public string FileName { get; set; }

        [Required]
        [Display(Name = "アップロード日")]
        public DateTime UploadDate { get; set; }

        [Required]
        [Column("UploadAspNetUsersId")]
        public string AspNetUsersId { get; set; }
        public ApplicationUser AspNetUsers { get; set; }
    }
}
