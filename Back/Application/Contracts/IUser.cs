using Application.DTOs.User.CreateRequest;
using Application.DTOs.User.GetRequest;
using Application.DTOs.User.GetRequestList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IUser
    {
        Task<CreateRequestResponse> CreateRequest(CreateRequestDTO createRequestDTO);
        Task<GetRequestListResponse> GetRequestList(GetRequestListDTO getRequestListDTO);
        Task<GetRequestResponse> GetRequest(GetRequestDTO getRequestDTO);
    }
}
