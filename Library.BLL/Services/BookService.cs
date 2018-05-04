using AutoMapper;
using Library.DAL.Entities;
using Library.DAL.Repositories;
using Library.ViewModels.Models;
using System.Collections.Generic;
using System.Linq;

namespace Library.BLL.Services
{
    public class BookService
    {

        private BookRepository _bookRepository;

        public BookService()
        {
            _bookRepository = new BookRepository();
        }

        public IEnumerable<BookViewModel> GetRange(int[] listId)
        {
            List<Book> books = _bookRepository.GetRange(listId).ToList();
            List<BookViewModel> result = Mapper.Map<List<Book>, List<BookViewModel>>(books);
            result.ForEach(x => x.PublicHouses.ForEach(y => y.Books = null));
            result.ForEach(x => x.Authors.ForEach(y => y.Books = null));
            return result;
        }

        public IEnumerable<BookViewModel> GetAllWithOutRelations()
        {
            List<BookViewModel> result = GetAll().ToList();
            result.ForEach(x => x.PublicHouses.ForEach(y => y.Books = null));
            result.ForEach(x => x.Authors.ForEach(y => y.Books = null));
            return result;
        }

        public IEnumerable<BookViewModel> GetAll()
        {
            List<Book> books = _bookRepository.GetWithInclude(p => p.PublicHouses, p => p.Authors).ToList();
            List<BookViewModel> result = Mapper.Map<List<Book>, List<BookViewModel>>(books);
            return result;
        }

        public BookViewModel Get(int id)
        {
            Book book = _bookRepository.Get(id);
            BookViewModel result = Mapper.Map<Book, BookViewModel>(book);
            return result;
        }

        public void Delete(int id)
        {
            _bookRepository.Delete(id);
        }


        public void Create(BookViewModel itemDto)
        {
            Book book = Mapper.Map<BookViewModel, Book>(itemDto);
            Book result = _bookRepository.Create(book);
            itemDto.BookId = result.BookId;
        }

        public void Update(BookViewModel itemDto)
        {
            Book book = Mapper.Map<BookViewModel, Book>(itemDto);
            _bookRepository.Update(book);
        }

    }
}
