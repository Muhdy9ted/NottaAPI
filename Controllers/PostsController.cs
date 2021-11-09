using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notta.Data;
using Notta.Dto;
using Notta.Models;

namespace Notta.Controllers
  {
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
      private readonly IPostRepository _repo;
      private readonly IMapper _mapper;
      private readonly IUserRepository _Urepo;


    /// <summary>
    /// the courses controller constructor
    /// </summary>
    /// <param name="repo"></param>
    /// <param name="mapper"></param>
    /// <param name="Urepo"></param>
    public PostsController(IPostRepository repo, IMapper mapper, IUserRepository Urepo)
      {
        _repo = repo;
        _mapper = mapper;
        _Urepo = Urepo;
      }

      // GET: api/Posts
      /// <summary>
      /// API endpoint for retrieving all the available posts
      /// </summary>
      /// <returns></returns>
      [HttpGet]
      public async Task<IActionResult> GetPosts()
      {
        var posts = await _repo.GetPosts();
        //var postsToSend = _mapper.Map<PostForListDto>(posts);
      return Ok(posts);
      }

      // GET: api/posts/5
      /// <summary>
      /// API endpoint for retrieving a particular post 
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      [HttpGet("{id}")]
      public async Task<IActionResult> GetPost(int id)
      {
        var post = await _repo.GetPost(id);
        return Ok(post);
      }


      // POST: api/posts
      /// <summary>
      /// API endpoint for creating a post
      /// </summary>
      /// <param name="postForCreateDto"></param>
      /// <returns></returns>
      [HttpPost]
      public async Task<IActionResult> CreatePost(PostForCreateDto postForCreateDto)
      {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        postForCreateDto.User = await _Urepo.GetUser(userId);
        //postForCreateDto.UserId = userId;
        var postToCreate = _mapper.Map<Post>(postForCreateDto);
        _repo.Add(postToCreate);
        if (await _repo.SaveAll())
        {
          return CreatedAtAction("GetPost", new { id = postToCreate.Id }, postToCreate);
        }
        throw new Exception($"creating post {postToCreate.Content} failed on save");
      }

      // Delete: api/posts
      /// <summary>
      /// API endpoint for creating a post
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      [HttpDelete("{id}")]
      public async Task<IActionResult> DeletePost(int id)
      {
        var post = await _repo.GetPost(id);
        if (post.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
        {
          return Unauthorized();
        }
        _repo.Delete(post);
        if (await _repo.SaveAll())
        {
          return Ok();
        }
        throw new Exception($"deleting post {post.Content} failed on delete");
      }
    } 
}