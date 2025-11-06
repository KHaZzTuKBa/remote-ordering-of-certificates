using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Admin.AdminGetRequestList
{
    public class AdminGetRequestListDTO
    {
        [Required]
        public int StudentId { get; set; } = default!;
    }
}
