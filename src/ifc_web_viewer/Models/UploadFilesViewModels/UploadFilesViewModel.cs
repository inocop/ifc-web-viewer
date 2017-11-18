using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ifc_web_viewer.Models.UploadFilesViewModels
{
    public class UploadFilesViewModel
    {
        [Required]
        [Display(Name = "Upload File")]
        [FileExtensions(Extensions = "jpg,png,gif,jpeg,bmp,svg,ifc")]
        public IFormFile UploadFile { get; set; }
    }
}
