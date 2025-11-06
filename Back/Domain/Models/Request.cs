using Domain.Enums;

namespace Domain.Models
{
    public class Request
    {
        public Guid Id { get; set; }
        public RequestStatus requestStatus { get; set; }

        public DateTime date {  get; set; }
    }
}
