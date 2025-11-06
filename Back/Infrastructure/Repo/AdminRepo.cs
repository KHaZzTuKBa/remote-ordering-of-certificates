using Application.Contracts;
using Application.DTOs.Admin.AdminGetRequest;
using Application.DTOs.Admin.AdminGetRequestList;
using Application.DTOs.Admin.SubmitTo1c;
using Application.DTOs.Admin.UpdateFrom1c;
using Infrastructure.Data;
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

        public Task<AdminGetRequestListResponse> AdminGetRequestList(AdminGetRequestListDTO adminGetRequestListDTO)
        {
            throw new NotImplementedException();
        }

        public Task<SubmitTo1cResponse> SubmitTo1C(SubmitTo1cDTO submitTo1cDTO)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateFrom1cResponse> UpdateFrom1C(UpdateFrom1cDTO updateFrom1cDTO)
        {
            throw new NotImplementedException();
        }
    }
}
