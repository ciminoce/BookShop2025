using AutoMapper;
using BookShop2025.Data.Interfaces;
using BookShop2025.Service.DTOs.Publisher;
using BookShop2025.Web.ViewModels.Publisher;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;
using X.PagedList.Extensions;

namespace BookShop2025.Web.Controllers
{
    public class PublishersController : Controller
    {
        private readonly IPublisherService _publisherService;
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;
        public PublishersController(IPublisherService publisherService, IMapper mapper, ICountryService countryService)
        {
            _publisherService = publisherService;
            _mapper = mapper;
            _countryService = countryService;
        }

        public IActionResult Index(int? page, int? pageSize)
        {
            int pageNumber = page ?? 1;
            int registerPerPage = pageSize ?? 5;

            var publishersDto = _publisherService.GetAll();
            var pagedListDto = publishersDto.ToPagedList(pageNumber, registerPerPage);
            var viewModelList = _mapper.Map<List<PublisherListVm>>(pagedListDto); // Mapea en memoria

            var viewModelPagedList = new StaticPagedList<PublisherListVm>(
                viewModelList,
                pagedListDto.PageNumber,
                pagedListDto.PageSize,
                pagedListDto.TotalItemCount
            );

            return View(viewModelPagedList);
        }
        public IActionResult Create()
        {
            var viewModel = new PublisherEditVm()
            {
                Countries = GetCountries()
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PublisherEditVm publisherVm)
        {

            if (ModelState.IsValid)
            {
                PublisherEditDto publisherDto = _mapper.Map<PublisherEditDto>(publisherVm);

                try
                {
                    if (_publisherService.Save(publisherDto, out var errors))
                    {
                        TempData["success"] = "Register Successfully Added";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, errors.First());
                        publisherVm.Countries = GetCountries();

                    }
                    return View(publisherVm);
                }
                catch (Exception ex)
                {
                    publisherVm.Countries = GetCountries();

                    ModelState.AddModelError(string.Empty, "F!ck!! Something Happen" + ex.Message);
                }

            }
            publisherVm.Countries = GetCountries();

            return View(publisherVm);
        }
        public IActionResult Edit(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            try
            {
                PublisherEditDto? publisherDto = _publisherService.GetById(id.Value);
                if (publisherDto is null)
                {
                    return NotFound($"Publisher With Id {id} Not Found!!");
                }
                PublisherEditVm publisherVm = _mapper.Map<PublisherEditVm>(publisherDto);
                publisherVm.Countries = GetCountries();
                return View(publisherVm);
            }
            catch (Exception)
            {

                TempData["error"] = "Error while trying to get a publisher";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PublisherEditVm publisherVm)
        {
            if (ModelState.IsValid)
            {
                PublisherEditDto publisherDto = _mapper.Map<PublisherEditDto>(publisherVm);
                try
                {
                    if (_publisherService.Save(publisherDto, out var errors))
                    {
                        TempData["success"] = "Register Successfully Updated";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, errors.First());
                    }
                    publisherVm.Countries = GetCountries();
                    return View(publisherVm);
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, "F!ck!! Something Happen" + ex.Message);
                }

            }
            publisherVm.Countries = GetCountries();
            return View(publisherVm);
        }

        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            try
            {
                PublisherEditDto? publisherDto = _publisherService.GetById(id.Value);
                if (publisherDto is null)
                {
                    return NotFound($"Publisher With Id {id} Not Found!!");
                }
                PublisherListVm publisherVm = _mapper.Map<PublisherListVm>(publisherDto);
                var paisDto = _countryService.GetById(publisherDto.CountryId);
                publisherVm.CountryName = paisDto!.CountryName;
                return View(publisherVm);
            }
            catch (Exception)
            {

                TempData["error"] = "Error while trying to get a publisher";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            try
            {
                PublisherEditDto? publisherDto = _publisherService.GetById(id.Value);
                if (publisherDto is null)
                {
                    return NotFound($"Publisher With Id {id} Not Found!!");
                }
                if (_publisherService.Remove(id.Value, out var errors))
                {
                    TempData["success"] = "Publisher Succesfully Removed";
                    return RedirectToAction("Index");
                }
                else
                {
                    PublisherListVm publisherVm = _mapper.Map<PublisherListVm>(publisherDto);
                    var paisDto = _countryService.GetById(publisherDto.CountryId);
                    publisherVm.CountryName = paisDto!.CountryName;

                    ModelState.AddModelError(string.Empty, errors.First());
                    return View(publisherVm);

                }
            }
            catch (Exception)
            {

                TempData["error"] = "Error while trying to get a publisher";
                return RedirectToAction("Index");
            }

        }

        private List<SelectListItem> GetCountries()
        {
            return _countryService.GetAll()
                .Select(c => new SelectListItem
                {
                    Value = c.CountryId.ToString(),
                    Text = c.CountryName
                }).ToList();

        }
    }
}
