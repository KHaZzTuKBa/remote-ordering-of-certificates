using Application.Contracts;
using Application.DTOs.User.CreateRequest;
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
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repo
{
    internal class UserRepo : IUser
    {
        private readonly AppDbContext appDbContext;
        private readonly IConfiguration configuration;

        public UserRepo(AppDbContext appDbContext, IConfiguration configuration)
        {
            this.appDbContext = appDbContext;
            this.configuration = configuration;
        }

        public async Task<CreateRequestResponse> CreateRequest(CreateRequestDTO createRequestDTO)
        {
            var requestInformation = new RequestInformation()
            {
                Name = createRequestDTO.FullName,
                FullRequestStatus = RequestStatus.New,
                Date = DateTime.UtcNow,
                FilePath = null,
                receivingFormat = createRequestDTO.receivingFormat
            };

            await appDbContext.RequestsInfo.AddAsync(requestInformation);
            await appDbContext.SaveChangesAsync();

            var strudentRequst = new StudentRequest()
            {
                StudentId = createRequestDTO.StudentId,
                RequestId = requestInformation.Id
            };

            await appDbContext.StudentRequests.AddAsync(strudentRequst);
            await appDbContext.SaveChangesAsync();

            return new CreateRequestResponse("Заявка передана в работу");
        }

        public async Task<GetRequestListResponse> GetRequestList(GetRequestListDTO getRequestListDTO)
        {
            var requestsIds = await FindRequestIdsByStudentId(getRequestListDTO.StudentId);

            var requests = new List<Request>(0);

            if (requestsIds.Count == 0 || requestsIds == null) 
            {
                return new GetRequestListResponse(requests);
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

            return new GetRequestListResponse(requests);
        }

        public async Task<GetRequestResponse> GetRequest(GetRequestDTO getRequestDTO)
        {
            var requestInfo = await FindRequestById(getRequestDTO.RequestID);

            if (requestInfo == null) return new GetRequestResponse(null);

            return new GetRequestResponse(requestInfo);
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
