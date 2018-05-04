using AutoMapper;
using Library.DAL.Entities;
using Library.DAL.Repositories;
using Library.ViewModels.Models;
using System.Collections.Generic;
using System.Linq;

namespace Library.BLL.Services
{
    public class BrochureService
    {
        private BrochureRepository _brochureRepository;

        public BrochureService()
        {
            _brochureRepository = new BrochureRepository();
        }

        public IEnumerable<BrochureViewModel> GetRange(int[] listId)
        {
            List<Brochure> brochures = _brochureRepository.GetRange(listId).ToList();
            List<BrochureViewModel> result = Mapper.Map<List<Brochure>, List<BrochureViewModel>>(brochures);
            return result;
        }

        public IEnumerable<BrochureViewModel> GetAll()
        {
            List<Brochure> brochures = _brochureRepository.GetAll().ToList();
            List<BrochureViewModel> result = Mapper.Map<List<Brochure>, List<BrochureViewModel>>(brochures);
            return result;
        }

        public BrochureViewModel Get(int id)
        {
            Brochure brochure = _brochureRepository.Get(id);
            BrochureViewModel result = Mapper.Map<Brochure, BrochureViewModel>(brochure);
            return result;
        }

        public void Delete(int id)
        {
            _brochureRepository.Delete(id);
        }


        public void Create(BrochureViewModel itemDto)
        {
            Brochure brochure = Mapper.Map<BrochureViewModel, Brochure>(itemDto);
            Brochure result = _brochureRepository.Create(brochure);
            itemDto.BrochureId = result.BrochureId;
        }

        public void Update(BrochureViewModel itemDto)
        {
            Brochure brochure = Mapper.Map<BrochureViewModel, Brochure>(itemDto);
            _brochureRepository.Update(brochure);
        }
    }
}
