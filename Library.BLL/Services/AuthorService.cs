using AutoMapper;
using Library.DAL.Entities;
using Library.DAL.Repositories;
using Library.ViewModels.Models;
using System.Collections.Generic;
using System.Linq;

namespace Library.BLL.Services
{
    public class AuthorService
    {

        private AuthorRepository _authorRepository;

        public AuthorService()
        {
            _authorRepository = new AuthorRepository();
        }

        public IEnumerable<AuthorViewModel> GetRange(int[] listId)
        {
            List<Author> authors = _authorRepository.GetRange(listId).ToList();
            List<AuthorViewModel> result = Mapper.Map<List<Author>, List<AuthorViewModel>>(authors);
            result.ForEach(x => x.Books.ForEach(y => y.Authors = null));
            return result;
        }

        public IEnumerable<AuthorViewModel> GetAllWithOutRelations()
        {
            List<AuthorViewModel> authorsList = GetAll().ToList();
            authorsList.ForEach(x => x.Books.ForEach(y => y.Authors = null));
            return authorsList;
        }

        public IEnumerable<AuthorViewModel> GetAll()
        {
            List<Author> authors = _authorRepository.GetWithInclude(p => p.Books).ToList();
            List<AuthorViewModel> result = Mapper.Map<List<Author>, List<AuthorViewModel>>(authors);
            return result;
        }

        public AuthorViewModel Get(int id)
        {
            Author author = _authorRepository.Get(id);
            AuthorViewModel result = Mapper.Map<Author, AuthorViewModel>(author);
            return result;
        }

        public void Delete(int id)
        {
            _authorRepository.Delete(id);
        }


        public void Create(AuthorViewModel itemDto)
        {
            Author author = Mapper.Map<AuthorViewModel, Author>(itemDto);
            Author result = _authorRepository.Create(author);
            itemDto.AuthorId = result.AuthorId;
        }

        public void Update(AuthorViewModel itemDto)
        {
            Author author = Mapper.Map<AuthorViewModel, Author>(itemDto);
            _authorRepository.Update(author);
        }

    }
}
