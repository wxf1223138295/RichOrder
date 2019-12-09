using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Rich.Order.Application.UserAppService;
using Rich.Order.Domain.Permissions;
using Rich.Order.Domain.User;

namespace Rich.Order.Application.MapProfile
{
    public class AccountProfile: Profile
    {
        public AccountProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<RegisterViewModel, RichOrderUser>()
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.userName))
                .ForMember(d => d.PasswordHash, opt => opt.MapFrom(s => s.passWord))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.email));

            CreateMap<RichOrderRole, RoleListOutView>()
                .ForMember(d => d.Key, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Introduction));

            //CreateMap<RichOrderRole, RoleListOutView>();


        }
    }
}
