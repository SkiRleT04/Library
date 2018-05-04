using AutoMapper;
using Library.BLL.Config.AutoMapperProfiles;
using Library.ViewModels.Models;


namespace Library.BLL.Config
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfiles(new[] {
                        typeof(BookProfile),
                        typeof(BrochureProfile),
                        typeof(PublicHouseProfile),
                        typeof(MagazineProfile),
                        typeof(AuthorProfile)
                    });
            });
        }
    }
}

