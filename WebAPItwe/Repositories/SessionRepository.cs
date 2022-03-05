﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPItwe.Entities;
using WebAPItwe.InRepositories;
using WebAPItwe.Models;

namespace WebAPItwe.Repositories
{
    public class SessionRepository : InSessionRepository 
    {
        private readonly dbEWTContext context;

        public SessionRepository(dbEWTContext context)
        {
            this.context = context;
        }

        public async Task CreateNewSession(NewSessionModel newSession)
        {
            string sessionId = Guid.NewGuid().ToString();
            string dateCreated = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string subjectName = await context.Subjects.Where(x => x.Id == newSession.SubjectId).Select(x => x.Name).FirstOrDefaultAsync();
            Session session = new Session { Id = sessionId, Slot = newSession.Slot, Date = newSession.Date,
                                            DateCreated = dateCreated, MaxPerson = newSession.MaxPerson, Status = "WaitCafe",
                                            MemberId = newSession.MemberId, MajorId = newSession.MajorId, SubjectId = newSession.SubjectId, 
                                            SubjectName = subjectName, CafeId = newSession.CafeId};
            context.Add(session);

            var memberSession = new MemberSession {Id = Guid.NewGuid().ToString(), MemberId = newSession.MemberId, MemberName = newSession.MemberName,
                                                   MemberImage = newSession.MemberImage ,MentorVoting = 0, CafeVoting = 0, SessionId = sessionId, Status = true};
            context.Add(memberSession);
            foreach(var mentorId in newSession.ListMentor)
            {
                var mentor = new MentorSession {Id = Guid.NewGuid().ToString(), MentorId = mentorId, SessionId = sessionId, AcceptDate = dateCreated, Status = false };
                context.Add(mentor);
            }
            //Bo sua lai
            var payment = new Payment { Id = Guid.NewGuid().ToString(), Amount = newSession.Payments.Amount, Type = newSession.Payments.Type, SessionId = sessionId ,Status = "true" };
            context.Add(payment);
            await context.SaveChangesAsync();
        }
    }
}