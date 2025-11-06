using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User.CreateRequest
{
    public class CreateRequestDTO
    {
        [Required]
        public int StudentId { get; set; } = default!;
        [Required]
        public string FullName { get; set; } = default!;
        [Required]
        public int Course { get; set; } = default!;
    }
}
