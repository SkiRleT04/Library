using AutoMapper;
using Library.DAL.Entities;
using Library.DAL.Repositories;
using Library.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BLL.Services
{
    public class PublicHouseService
    {
        private PublicHouseRepository _publicHouseRepository;

        public PublicHouseService()
        {
            _publicHouseRepository = new PublicHouseRepository("DefualtConnection");
        }

        public IEnumerable<PublicHouseViewModel> GetAllWithOutRelations()
        {
            List<PublicHouseViewModel> publicHouseList = GetAll().ToList();
            publicHouseList.ForEach(x => x.Books.ForEach(y => y.PublicHouses = null));
            return publicHouseList;
        }

        public IEnumerable<PublicHouseViewModel> GetRange(int[] listId)
        {
            List<PublicHouse> publicHouses = _publicHouseRepository.GetRange(listId).ToList();
            List<PublicHouseViewModel> result = Mapper.Map<List<PublicHouse>, List<PublicHouseViewModel>>(publicHouses);
            result.ForEach(x => x.Books.ForEach(y => y.PublicHouses = null));
            return result;
        }

        public IEnumerable<PublicHouseViewModel> GetAll()
        {
            List<PublicHouse> publicHouses = _publicHouseRepository.GetWithInclude(p => p.Books).ToList();
            List<PublicHouseViewModel> result = Mapper.Map<List<PublicHouse>, List<PublicHouseViewModel>>(publicHouses);
            return result;
        }

        public PublicHouseViewModel Get(int id)
        {
            PublicHouse publisOffice = _publicHouseRepository.Get(id);
            PublicHouseViewModel result = Mapper.Map<PublicHouse, PublicHouseViewModel>(publisOffice);
            return result;
        }

        public void Delete(int id)
        {
            _publicHouseRepository.Delete(id);
        }


        public void Create(PublicHouseViewModel itemDto)
        {
            PublicHouse publicHouse = Mapper.Map<PublicHouseViewModel, PublicHouse>(itemDto);
            PublicHouse result = _publicHouseRepository.Create(publicHouse);
            itemDto.PublicHouseId = result.PublicHouseId;
        }

        public void Update(PublicHouseViewModel itemDto)
        {
            PublicHouse publicHouce = Mapper.Map<PublicHouseViewModel, PublicHouse>(itemDto);
            _publicHouseRepository.Update(publicHouce);
        }
    }
}
