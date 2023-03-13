using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Azin.Shadi.Models;
using Azin.Shadi.Models.ViewModels;

namespace Azin.Shadi.App_Start
{
    public class AutoMapperConfig
    {
        public static IMapper mapper;

        public static void Configuration()
        {
            MapperConfiguration configuration = new MapperConfiguration(t =>
            {
                t.CreateMap<Admin, RegisterAdminViewModel>();
                t.CreateMap<RegisterAdminViewModel, Admin>();
            });
            mapper = configuration.CreateMapper();
        }

}
}