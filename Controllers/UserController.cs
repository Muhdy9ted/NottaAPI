using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notta.Data;
using Notta.Dto;

namespace Notta.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
      private readonly IUserRepository _repo;
      private readonly IMapper _mapper;
      private readonly ApplicationDbContext _context;


      /// <summary>
      /// user controller constructor
      /// </summary>
      public UserController(IUserRepository repo, IMapper mapper, ApplicationDbContext context)
      {
        _repo = repo;
        _mapper = mapper;
        _context = context;
      }

      /// <summary>
      /// API endpoint for retrieving all registered users
      /// </summary>
      /// <returns></returns>
      [HttpGet]
      public async Task<IActionResult> GetUsers()
      {
        var users = await _repo.GetUsers();
        var usersToReturn = _mapper.Map<IEnumerable<UserForDetailedDto>>(users);
        return Ok(usersToReturn);
      }


      /// <summary>
      /// API endpoint for retrieving a particular user
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      [HttpGet("{id}", Name = "GetUser")]
      public async Task<IActionResult> GetUser(int id)
      {
        var user = await _repo.GetUser(id);
        var userToReturn = _mapper.Map<UserForDetailedDto>(user);
        return Ok(userToReturn);
      }
    }
}