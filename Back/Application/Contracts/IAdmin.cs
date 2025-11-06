using Application.DTOs.Admin.AdminGetRequest;
using Application.DTOs.Admin.AdminGetRequestList;
using Application.DTOs.Admin.SubmitTo1c;
using Application.DTOs.Admin.UpdateFrom1c;

namespace Application.Contracts
{
    public interface IAdmin
    {
        Task<AdminGetRequestResponse> AdminGetRequest(AdminGetRequestDTO adminGetRequestDTO);
        Task<AdminGetRequestListResponse> AdminGetRequestList(AdminGetRequestListDTO adminGetRequestListDTO);
        Task<SubmitTo1cResponse> SubmitTo1C(SubmitTo1cDTO submitTo1cDTO);
        Task<UpdateFrom1cResponse> UpdateFrom1C (UpdateFrom1cDTO updateFrom1cDTO);
    }
}
