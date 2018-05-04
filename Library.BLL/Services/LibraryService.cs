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
    public class LibraryService
    {
        private BookRepository _bookRepository;
        private MagazineRepository _magazineRepository;
        private BrochureRepository _brochureRepository;

        public LibraryService()
        {
            _bookRepository = new BookRepository();
            _magazineRepository = new MagazineRepository();
            _brochureRepository = new BrochureRepository();
        }

        public IEnumerable<CommonItemViewModel> GetAllWithOutRelations()
        {
            var result = new List<CommonItemViewModel>();

            IEnumerable<Book> books = _bookRepository.GetAll();
            IEnumerable<Brochure> brochures = _brochureRepository.GetAll();
            IEnumerable<Magazine> magazines = _magazineRepository.GetAll();

            List<CommonItemViewModel> booksCommonItemList = Mapper.Map<IEnumerable<Book>, List<CommonItemViewModel>>(books);
            List<CommonItemViewModel> magazinesCommonItemList = Mapper.Map<IEnumerable<Magazine>, List<CommonItemViewModel>>(magazines);
            List<CommonItemViewModel> brochuresCommonItemList = Mapper.Map<IEnumerable<Brochure>, List<CommonItemViewModel>>(brochures);

            result.AddRange(booksCommonItemList);
            result.AddRange(brochuresCommonItemList);
            result.AddRange(magazinesCommonItemList);
            return result;
        }
    }
}
