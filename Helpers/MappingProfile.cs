using AutoMapper;
using Notta.Dto;
using Notta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notta.Helpers
{
  internal class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<UserForRegisterDto, User>();
      CreateMap<User, UserForDetailedDto>();
      CreateMap<User, UserForListDto>();
      CreateMap<PostForCreateDto, Post>();
      CreateMap<CommentForCreateDto, Comment>();
    }
  }
}
