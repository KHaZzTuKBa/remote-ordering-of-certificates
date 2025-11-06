using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Admin.UpdateFrom1c
{
    public class UpdateFrom1cDTO
    {
        [Required]
        public Guid RequestID { get; set; } = default!;
        [Required]
        public OneCStatus Status { get; set; } = default!;
        [Required]
        public IFormFile? requestFile { get; set; } = default!;
    }
}
