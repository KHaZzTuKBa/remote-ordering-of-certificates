using Application.Contracts;
using Application.DTOs.Admin.AdminGetRequest;
using Application.DTOs.Admin.AdminGetRequestList;
using Application.DTOs.Admin.SubmitTo1c;
using Application.DTOs.Admin.UpdateFrom1c;
using Application.DTOs.User.GetRequest;
using Application.DTOs.User.GetRequestList;
using Domain.Entities;
using Domain.Enums;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
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

        public async Task<AdminGetRequestResponse> AdminGetRequest(AdminGetRequestDTO adminGetRequestDTO)
        {
            var requestInfo = await FindRequestById(adminGetRequestDTO.RequestID);

            if (requestInfo == null) return new AdminGetRequestResponse(null);

            return new AdminGetRequestResponse(requestInfo);
        }

        public async Task<AdminGetRequestListResponse> AdminGetRequestList(AdminGetRequestListDTO adminGetRequestListDTO)
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

        public async Task<RequestsResponse> SendRequestsTo1C(RequestsDTO requestsDTO)
        {
            var newRequests = await FindNewRequests();

            if (newRequests.Count == 0)
            {
                return new RequestsResponse(newRequests);
            }

            foreach (var request in newRequests)
            {
                request.FullRequestStatus = RequestStatus.Processing;
            }

            await appDbContext.SaveChangesAsync();

            return new RequestsResponse(newRequests);
        }

        public async Task<UpdateFrom1cResponse> UpdateFrom1C(UpdateFrom1cDTO updateFrom1cDTO)
        {
            var uploadRoot = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            Directory.CreateDirectory(uploadRoot);

            foreach (var oneCRequest in updateFrom1cDTO.Requests)
            {
                var requestInfo = await FindRequestById(oneCRequest.Id);

                if (requestInfo == null)
                {
                    continue;
                }

                if (oneCRequest.requestFile == null)
                {
                    requestInfo.FullRequestStatus = RequestStatus.Rejected;
                    continue;
                }

                var safeFileName = Path.GetFileName(oneCRequest.requestFile.FileName);
                var fileName = $"{oneCRequest.Id}_{DateTime.UtcNow:yyyyMMddHHmmssfff}_{safeFileName}";
                var filePath = Path.Combine(uploadRoot, fileName);

                using (var stream = File.Create(filePath))
                {
                    await oneCRequest.requestFile.CopyToAsync(stream);
                }

                requestInfo.FullRequestStatus = RequestStatus.Completed;
                requestInfo.FilePath = filePath;
            }

            await appDbContext.SaveChangesAsync();

            return new UpdateFrom1cResponse();
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

        private async Task<List<RequestInformation>> FindNewRequests() =>
            await appDbContext.RequestsInfo
                .Where(r => r.FullRequestStatus == RequestStatus.New)
                .ToListAsync();
    }
}
