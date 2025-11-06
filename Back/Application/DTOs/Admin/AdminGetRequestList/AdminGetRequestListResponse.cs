using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Admin.AdminGetRequestList
{
    public record AdminGetRequestListResponse (List<Request> ListOfRequests);
}
