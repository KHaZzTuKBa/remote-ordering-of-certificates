using Application.Contracts;
using Application.DTOs.User.CreateRequest;
using Application.DTOs.User.GetRequest;
using Application.DTOs.User.GetRequestList;
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

        public Task<CreateRequestResponse> CreateRequest(CreateRequestDTO createRequestDTO)
        {
            throw new NotImplementedException();
        }

        public Task<GetRequestListResponse> GetRequestList(GetRequestListDTO getRequestListDTO)
        {
            throw new NotImplementedException();
        }

        public Task<GetRequestResponse> GetResponseList(GetRequestListDTO getRequestListDTO)
        {
            throw new NotImplementedException();
        }
    }
}
