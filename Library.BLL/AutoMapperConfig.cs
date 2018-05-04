using AutoMapper;
using Library.DLL.Entities;
using Library.Entities;
using Library.ViewModels.Enums;
using Library.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BLL
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Book, BookViewModel>();
                cfg.CreateMap<BookViewModel, Book>();

                cfg.CreateMap<Magazine, MagazineViewModel>();
                cfg.CreateMap<MagazineViewModel, Magazine>();

                cfg.CreateMap<Brochure, BrochureViewModel>();
                cfg.CreateMap<BrochureViewModel, Brochure>();

                cfg.CreateMap<PublicHouse, PublicHouseViewModel>();
                cfg.CreateMap<PublicHouseViewModel, PublicHouse>();

                cfg.CreateMap<Book, LibraryViewModel>()
               .ForMember(dest => dest.Name, opt => opt.ResolveUsing(src => src.Name))
               .ForMember(dest => dest.Type, opt => opt.ResolveUsing(src => (LibraryType)src.Type));

                cfg.CreateMap<Brochure, LibraryViewModel>()
              .ForMember(dest => dest.Name, opt => opt.ResolveUsing(src => src.Name))
              .ForMember(dest => dest.Type, opt => opt.ResolveUsing(src => (LibraryType)src.Type));

                cfg.CreateMap<Magazine, LibraryViewModel>()
              .ForMember(dest => dest.Name, opt => opt.ResolveUsing(src => src.Name))
              .ForMember(dest => dest.Type, opt => opt.ResolveUsing(src => (LibraryType)src.Type));
            });
        }
    }
}
