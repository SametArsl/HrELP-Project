using AutoMapper;
using HrELP.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Application.Services.AdvanceRequestService
{
    public class AdvanceRequestService:IAdvanceRequestService
    {
        private readonly IAdvanceRequestRepository _advanceRequestRepository;

        public AdvanceRequestService(IAdvanceRequestRepository advanceRequestRepository)
        {
            _advanceRequestRepository = advanceRequestRepository;
        }
    }
}
