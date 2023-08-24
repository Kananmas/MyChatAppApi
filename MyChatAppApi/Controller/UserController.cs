using Microsoft.AspNetCore.Mvc;
using MyChatAppApi.DTOs;
using MyChatAppApi.Models;
using MyChatAppApi.Repository.Interfaces;
using MyChatAppApi.Utilites;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace MyChatAppApi.Controller
{
    public class UserController:ControllerBase
    {
        protected readonly IUserRepositoryService _userRepositoryService;
        protected readonly CommonUtillites _commonUtillites;

        public UserController(IUserRepositoryService userRepositoryService, CommonUtillites commonUtillites)
        {
            _userRepositoryService = userRepositoryService;
            _commonUtillites = commonUtillites;
        }

        [HttpGet("GetUserByFormData")]
        public async Task<User_DTO> GetUserByFromData( string email , string password)
        {
            try
            {
                var result = new User_DTO();
                var User = await _userRepositoryService.GetUserByEmailAndPassword(email, password);

                if (User != null)
                {
                    result.UserName = User.UserName;
                    result.PhoneNumber = User.PhoneNumber;
                    result.Email = User.Email;
                    result.JWTToken = _commonUtillites.GenerateJWT(User);
                    return result;
                }

                throw new Exception("No such user is found");
            }
            catch(Exception e)
            {
                throw e; 
            }
        }

        [HttpPost("PostUser")]
        public async Task<IActionResult> PostUser([FromForm]string user)
        {
           try
            {
                var newClient = JsonConvert.DeserializeObject<Client>(user);

                newClient.Id = Guid.NewGuid();

                var _user = await _userRepositoryService.GetUserByEmailAndPassword(newClient.Email, newClient.Password) ;

                if (_user == null)
                {
                    var token = _commonUtillites.GenerateJWT(newClient);

                    await _userRepositoryService.AddNewUser(newClient);

                    return Ok(new User_DTO() { 
                        JWTToken = token,
                        UserName = newClient.UserName,
                        PhoneNumber = newClient.PhoneNumber,
                        Email = newClient.Email,    
                    });
                }

                throw new Exception("User is Already Registered");
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
