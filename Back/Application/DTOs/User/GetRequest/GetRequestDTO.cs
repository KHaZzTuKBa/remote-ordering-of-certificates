using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User.GetRequest
{
    public class GetRequestDTO
    {
        [Required]
        public Guid RequestID { get; set; } = default!;
    }
}
