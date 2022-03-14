﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPItwe.Models;

namespace WebAPItwe.InRepositories
{
    public interface InSessionRepository
    {
        Task CreateNewSession(NewSessionModel newSession);
        Task<object> LoadSession(string memberId, int pageIndex, int pageSize);
        Task<object> LoadRecommendSession(string memberId, int pageIndex, int pageSize);
        Task<object> LoadSessionDetail(string memberId, string sessionId);
        Task<object> LoadRequestMember(string sessionId);
        Task AcceptSessionByCafe(string cafeId, string sessionId);
        Task CancelSessionByCafe(string cafeId, string sessionId);
    }
}
