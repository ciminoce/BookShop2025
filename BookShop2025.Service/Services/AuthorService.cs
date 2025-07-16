using AutoMapper;
using BookShop2025.Data;
using BookShop2025.Data.Interfaces;
using BookShop2025.Entities.Entities;
using BookShop2025.Service.DTOs.Author;
using System.Linq.Expressions;

namespace BookShop2025.Service.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IQueryable<AuthorListDto> GetAll(string? searchText, string? orderAuthors = "Author")
        {
            Expression<Func<Author, bool>>? filterAuthor = null;
            if (searchText != null)
            {
                filterAuthor = a => a.FirstName.Contains(searchText) ||
                    a.LastName.Contains(searchText) ||
                    a.Country!.CountryName.Contains(searchText);
            }
            Func<IQueryable<Author>, IOrderedQueryable<Author>>? orderBy = null;
            if (orderAuthors == "Author")
            {
                orderBy = a => a.OrderBy(a => a.FirstName).ThenBy(a => a.LastName)
                    .ThenBy(a => a.Country!.CountryName);
            }
            else
            {
                orderBy = a => a.OrderBy(a => a.Country!.CountryName).ThenBy(a => a.FirstName)
                .ThenBy(a => a.LastName);
            }
                var authors = _unitOfWork.Authors.GetAll(filter: filterAuthor, orderBy: orderBy);
            return _mapper.ProjectTo<AuthorListDto>(authors);
        }

        public AuthorEditDto? GetById(int id, bool tracked = false)
        {
            var author = _unitOfWork.Authors.Get(filter:a=>a.AuthorId==id,tracked:true);
            if (author is null) return null;
            return _mapper.Map<AuthorEditDto>(author);
        }

        public bool Remove(int id, out List<string> errors)
        {
            errors = new List<string>();
            var author = _unitOfWork.Authors.Get(filter: a => a.AuthorId == id, tracked: true);
            if (author is null)
            {
                errors.Add("Author does not exist");
                return false;
            }
            _unitOfWork.Authors.Remove(author);
            var rowsAffected = _unitOfWork.Complete();
            return rowsAffected > 0;

        }

        public bool Save(AuthorEditDto authorDto, out List<string> errors)
        {
            errors = new List<string>();
            Author author = _mapper.Map<Author>(authorDto);
            if (author.AuthorId == 0)
            {
                if (!_unitOfWork.Authors.Exist(author))
                {
                    _unitOfWork.Authors.Add(author);
                    int rowsAffected = _unitOfWork.Complete();
                    return rowsAffected > 0;

                }
                else
                {
                    errors.Add("Author Already Exist!!");
                    return false;
                }

            }
            else
            {
                if (!_unitOfWork.Authors.Exist(author))
                {
                    _unitOfWork.Authors.Update(author);
                    int rowsAffected = _unitOfWork.Complete();
                    return rowsAffected > 0;

                }
                else
                {
                    errors.Add("Author Already Exist!!");
                    return false;
                }

            }
        }

    }
}
