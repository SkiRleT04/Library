using AutoMapper;
using Library.DAL.Entities;
using Library.DAL.Repositories;
using Library.ViewModels.Models;
using System.Collections.Generic;
using System.Linq;

namespace Library.BLL.Services
{
    public class MagazineService
    {
        private MagazineRepository _magazineRepository;

        public MagazineService()
        {
            _magazineRepository = new MagazineRepository();
        }

        public IEnumerable<MagazineViewModel> GetRange(int[] listId)
        {
            List<Magazine> magazines = _magazineRepository.GetRange(listId).ToList();
            List<MagazineViewModel> result = Mapper.Map<List<Magazine>, List<MagazineViewModel>>(magazines);
            return result;
        }

        public IEnumerable<MagazineViewModel> GetAll()
        {
            List<Magazine> magazines = _magazineRepository.GetAll().ToList();
            List<MagazineViewModel> result = Mapper.Map<List<Magazine>, List<MagazineViewModel>>(magazines);
            return result;
        }

        public MagazineViewModel Get(int id)
        {
            Magazine magazine = _magazineRepository.Get(id);
            MagazineViewModel result = Mapper.Map<Magazine, MagazineViewModel>(magazine);
            return result;
        }

        public void Delete(int id)
        {
            _magazineRepository.Delete(id);
        }


        public void Create(MagazineViewModel itemDto)
        {
            Magazine magazine = Mapper.Map<MagazineViewModel, Magazine>(itemDto);
            Magazine result = _magazineRepository.Create(magazine);
            itemDto.MagazineId = result.MagazineId;
        }

        public void Update(MagazineViewModel itemDto)
        {
            Magazine magazine = Mapper.Map<MagazineViewModel, Magazine>(itemDto);
            _magazineRepository.Update(magazine);
        }
    }
}
