using AutoMapper;
using Library.DAL.Entities;
using Library.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BLL.Config.AutoMapperProfiles
{
    public class CommonItemProfile:Profile
    {
        public CommonItemProfile()
        {
            CreateMap<Book, CommonItemViewModel>().ForMember("Type", opt => opt.MapFrom(c => "Book"));
            CreateMap<Brochure, CommonItemViewModel>().ForMember("Type", opt => opt.MapFrom(c => "Brochure"));
            CreateMap<Magazine, CommonItemViewModel>().ForMember("Type", opt => opt.MapFrom(c => "Magazine"));
        }
    }
}
