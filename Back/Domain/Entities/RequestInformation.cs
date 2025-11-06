using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Domain.Entities
{
    public class RequestInformation
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public RequestStatus requestStatus { get; set; } = default!;
        public DateTime date { get; set; } = default!;
        public IFormFile? requestFile { get; set; } = default!;
    }
}
