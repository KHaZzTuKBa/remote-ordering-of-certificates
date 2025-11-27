using Application.Contracts;
using Application.DTOs.User.CreateRequest;
using Application.DTOs.User.GetRequest;
using Application.DTOs.User.GetRequestList;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Task<GetRequestListResponse> GetRequestList(GetRequestListDTO getRequestListDTO)
        {
            throw new NotImplementedException();
        }

        public Task<GetRequestResponse> GetRequest(GetRequestDTO getRequestDTO)
        {
            throw new NotImplementedException();
        }
    }
}
