using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Notta.Data;
using Notta.Dto;
using Notta.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace Notta.Controllers
{
    /// <summary>
    /// authentication controller for the student access authentication
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
      private readonly IAuthRepository _repo;
      private readonly IMapper _mapper;
      private readonly IConfiguration _config;


      /// <summary>
      /// authcontroller constructor
      /// </summary>
      /// <param name="repo"></param>
      /// <param name="mapper"></param>
      /// <param name="config"></param>
      public AuthController(IAuthRepository repo, IMapper mapper, IConfiguration config)
      {
        _repo = repo;
        _mapper = mapper;
        _config = config;
      }

      /// <summary>
      /// API endpoint for user registration
      /// </summary>
      /// <param name="userForRegisterDto"></param>
      /// <returns></returns>
      [HttpPost("register")]
      public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
      {
        userForRegisterDto.Email = userForRegisterDto.Email.ToLower();
        if (await _repo.UserExist(userForRegisterDto.Email))
        {
          return BadRequest("This Email address already exist");
        }
        var userToCreate = _mapper.Map<User>(userForRegisterDto);
        var createUser = await _repo.Register(userToCreate, userForRegisterDto.Password);
        var userToReturn = _mapper.Map<UserForDetailedDto>(createUser);
        return CreatedAtRoute("GetUser", new { controller = "User", id = createUser.Id }, userToReturn);
      }

      /// <summary>
      /// API endpoint for user login
      /// </summary>
      /// <param name="userForLoginDto"></param>
      /// <returns></returns>
      [HttpPost("login")]
      public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
      {
        var userFromRepo = await _repo.Login(userForLoginDto.Email.ToLower(), userForLoginDto.Password);
        if (userFromRepo == null)
        {
          return Unauthorized();
        }
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
            new Claim(ClaimTypes.SerialNumber, userFromRepo.Email),
            new Claim(ClaimTypes.Name, userFromRepo.Name)
          };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
          Subject = new ClaimsIdentity(claims),
          Expires = DateTime.Now.AddDays(3),
          SigningCredentials = creds
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return Ok(new { token = tokenHandler.WriteToken(token) });
      }
    }
}