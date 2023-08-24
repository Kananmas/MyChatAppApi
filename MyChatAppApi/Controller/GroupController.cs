using Microsoft.AspNetCore.Mvc;
using MyChatAppApi.Repository.Interfaces;
using System.Threading.Tasks;

namespace MyChatAppApi.Controller
{
    public class GroupController:ControllerBase
    {
        private readonly IRoomRepositoryService _roomRepositoryService;

        public GroupController(IRoomRepositoryService roomRepositoryService)
        {
            _roomRepositoryService = roomRepositoryService;
        }

        [HttpGet("GetGroupsByName")]
        public async Task<IActionResult> GetGroupsByName(string name)
        {
            var groups = await _roomRepositoryService.GetRoomsByName(name);

            return Ok(groups);
        }

    }
}
