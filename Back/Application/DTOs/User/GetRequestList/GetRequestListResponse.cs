using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User.GetRequestList
{
    public record GetRequestListResponse (List<Request> ListOfRequests);
}
