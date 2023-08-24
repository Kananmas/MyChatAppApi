
using Microsoft.AspNetCore.Mvc;
using MyChatAppApi.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyChatAppApi.Repository.Interfaces
{
    public interface IGroupSubscribtionRepositoryService
    {
        Task<List<GroupSubscribtion>> GetAllUserSubs(Guid userId);
        Task<List<GroupSubscribtion>> GetAllSubsByGroupId(Guid groupId);
        Task RemoveGroupsForUser(Guid userId,List<Guid> groupIds);
        Task RemoveSubscribtionForUser(Guid groupId,Guid userId);
        Task AddNewSubscribtion(GroupSubscribtion newSubscribtion);

    }
}
