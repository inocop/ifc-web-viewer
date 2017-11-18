using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ifc_web_viewer.Models
{
    public class UploadFile
    {
        public long Id { get; set; }

        [Required]
        [Display(Name = "ファイルパス")]
        public string FilePath { get; set; }

        [Required]
        [Display(Name = "ファイル名")]
        public string FileName { get; set; }

        [Required]
        [Display(Name = "ファイルサイズ")]
        public long FileSize { get; set; }

        [Required]
        [Display(Name = "アップロード日時")]
        public DateTime UploadDate { get; set; }

        [Required]
        [Display(Name = "アップロードユーザー")]
        [Column("UploadAspNetUsersId")]
        [ForeignKey("Id")]
        public string AspNetUsersId { get; set; }
        public ApplicationUser AspNetUsers { get; set; }

        [Required]
        [Display(Name = "アップロード日時")]
        [DefaultValue(false)]
        public bool DeleteFlg { get; set; }

    }
}
