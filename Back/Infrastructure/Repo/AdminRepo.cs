using Application.Contracts;
using Application.DTOs.Admin.AdminGetRequest;
using Application.DTOs.Admin.AdminGetRequestList;
using Application.DTOs.Admin.SubmitTo1c;
using Application.DTOs.Admin.UpdateFrom1c;
using Application.DTOs.User.GetRequest;
using Application.DTOs.User.GetRequestList;
using Domain.Entities;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repo
{
    internal class AdminRepo : IAdmin
    {
        private readonly AppDbContext appDbContext;
        private readonly IConfiguration configuration;

        public AdminRepo(AppDbContext appDbContext, IConfiguration configuration)
        {
            this.appDbContext = appDbContext;
            this.configuration = configuration;
        }

        public Task<AdminGetRequestResponse> AdminGetRequest(AdminGetRequestDTO adminGetRequestDTO)
        {
            throw new NotImplementedException();
        }

        {
            var requestsIds = await FindRequestIdsByStudentId(adminGetRequestListDTO.StudentId);

            var requests = new List<Request>(0);

            if (requestsIds.Count == 0 || requestsIds == null)
            {
                return new AdminGetRequestListResponse(requests);
            }

            foreach (var requestId in requestsIds)
            {
                var requestInfo = await FindRequestById(requestId);

                if (requestInfo != null)
                {

                    var requestModel = new Request()
                    {
                        Id = requestInfo.Id,
                        requestStatus = requestInfo.FullRequestStatus,
                        date = requestInfo.Date,
                        receivingFormat = requestInfo.receivingFormat
                    };

                    requests.Add(requestModel);
                }
            }

            return new AdminGetRequestListResponse(requests);
        }

        public Task<RequestsResponse> SendRequestsTo1C(RequestsDTO requestsDTO)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateFrom1cResponse> UpdateFrom1C(UpdateFrom1cDTO updateFrom1cDTO)
        {
            throw new NotImplementedException();
        }

        private async Task<List<Guid>> FindRequestIdsByStudentId(int studentId) =>
            await appDbContext.StudentRequests
            .Where(sr => sr.StudentId == studentId)
            .Select(sr => sr.RequestId)
            .ToListAsync();

        private async Task<List<Guid>> FindRequestsByRequestsIds(int studentId) =>
            await appDbContext.StudentRequests
            .Where(sr => sr.StudentId == studentId)
            .Select(sr => sr.RequestId)
            .ToListAsync();

        private async Task<RequestInformation?> FindRequestById(Guid Id) =>
            await appDbContext.RequestsInfo.FirstOrDefaultAsync(r => r.Id == Id);
    }
}
