using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User.GetRequestList
{
    public class GetRequestListDTO
    {
        [Required]
        public int StudentId { get; set; } = default!;
    }
}
