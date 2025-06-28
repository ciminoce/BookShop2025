using AutoMapper;
using BookShop2025.Data;
using BookShop2025.Data.Interfaces;
using BookShop2025.Entities.Entities;
using BookShop2025.Service.DTOs.Country;

namespace BookShop2025.Service.Services
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CountryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool Remove(int id, out List<string> errors)
        {
            errors = new List<string>();
            Country? country = _unitOfWork.Countries.GetById(id);
            if (country == null)
            {
                errors.Add("Countries does not exist!!!");
            }
            _unitOfWork.Countries.Remove(id);
            var rowsAffected = _unitOfWork.Complete();
            return rowsAffected > 0;
        }
        public IQueryable<CountryListDto> GetAll()
        {
            var categories = _unitOfWork.Countries.GetAll();
            return _mapper.ProjectTo<CountryListDto>(categories);
        }

        public CountryEditDto GetById(int id)
        {
            var country = _unitOfWork.Countries.GetById(id);
            return _mapper.Map<CountryEditDto>(country);
        }

        public bool Save(CountryEditDto countryDto, out List<string> errors)
        {
            errors = new List<string>();
            Country country = _mapper.Map<Country>(countryDto);
            int rowsAffected;

            // Verificar si es una categoría existente (edición) o nueva (creación)
            if (country.CountryId == 0) // Asumiendo que 0 significa una nueva entidad
            {
                // Lógica para AGREGAR (Crear)
                if (!_unitOfWork.Countries.Exist(country)) // Verifica si ya existe una categoría con el mismo nombre, por ejemplo
                {
                    _unitOfWork.Countries.Add(country);
                    rowsAffected = _unitOfWork.Complete();
                    return rowsAffected > 0;
                }
                else
                {
                    errors.Add("Country with this name already exists."); // Mensaje más específico
                    return false;
                }
            }
            else
            {


                // Lógica para EDITAR (editar)
                if (!_unitOfWork.Countries.Exist(country)) // Verifica si ya existe una categoría con el mismo nombre, por ejemplo
                {
                    _unitOfWork.Countries.Update(country);
                    rowsAffected = _unitOfWork.Complete();
                    return rowsAffected > 0;
                }
                else
                {
                    errors.Add("Country with this name already exists."); // Mensaje más específico
                    return false;
                }
            }
        }
    }
}
