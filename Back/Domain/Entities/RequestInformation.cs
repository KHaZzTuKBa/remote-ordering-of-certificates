using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Domain.Entities
{
    public class RequestInformation
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public RequestStatus FullRequestStatus { get; set; } = default!;
        public DateTime Date { get; set; } = default!;
        public string FilePath { get; set; } = default!;
        public ReceivingFormat receivingFormat { get; set; }
    }
}
