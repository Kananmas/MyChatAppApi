using Microsoft.EntityFrameworkCore;
using MyChatAppApi.Context;
using MyChatAppApi.Models;
using MyChatAppApi.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyChatAppApi.Repository.Services
{
    public class GroupSubscribtionRepositoryService : IGroupSubscribtionRepositoryService
    {
        protected readonly ChatHubContext _chatHubContext;
        protected readonly DbSet<GroupSubscribtion> _subs;


        public GroupSubscribtionRepositoryService(ChatHubContext chatHubContext) {
            _chatHubContext = chatHubContext;
            _subs = chatHubContext.Subscribtions;
        }


        public async Task AddNewSubscribtion(GroupSubscribtion newSubscribtion)
        {
            await _subs.AddAsync(newSubscribtion);

            await _chatHubContext.SaveChangesAsync();
        }

        public async Task<List<GroupSubscribtion>> GetAllSubsByGroupId(Guid groupId)
        {
            var groupList = await _subs.Where(sub => sub.GroupId == groupId).ToListAsync(); 

            return groupList;
        }

        public async Task<List<GroupSubscribtion>> GetAllUserSubs(Guid userId)
        {
            var userGroups = await _subs.Where(sub => sub.SubscriberId == userId).ToListAsync();

            return userGroups;
        }

        public async Task RemoveGroupsForUser(Guid userId, List<Guid> groupIds)
        {
            var group = await _subs
                .Where(sub => sub.SubscriberId == userId && groupIds.Contains(sub.GroupId))
                .FirstOrDefaultAsync();

            if (group != null)
            {
                _subs.Remove(group);


                await _chatHubContext.SaveChangesAsync();
            }
        }

        public async Task RemoveSubscribtionForUser(Guid groupId, Guid userId)
        {
            var group = await _subs
                .Where(sub => sub.GroupId == groupId && sub.SubscriberId == userId)
                .FirstOrDefaultAsync();

            if(group != null)
            {
                _subs.Remove(group);


                await _chatHubContext.SaveChangesAsync();
            }
        }
    }
}
