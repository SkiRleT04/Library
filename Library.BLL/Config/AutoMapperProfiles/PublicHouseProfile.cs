﻿using AutoMapper;
using Library.DAL.Entities;
using Library.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BLL.Config.AutoMapperProfiles
{
    public class PublicHouseProfile : Profile
    {
        public PublicHouseProfile()
        {
            CreateMap<PublicHouse, PublicHouseViewModel>();
            CreateMap<PublicHouseViewModel, PublicHouse>();
        }
    }
}
