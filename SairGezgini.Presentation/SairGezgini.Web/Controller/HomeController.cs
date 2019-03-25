using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SairGezgini.Models;
using SiirGezgini.Business.Generator;
using SiirGezgini.Business.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SairGezgini.Web.Controller
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IPoetBusiness _poetBusiness;
        private readonly IHostingEnvironment _environment;
        private readonly IPoemBusiness _poemBusiness;
        private readonly IPoetOfPoemsBusiness _ofPoemsBusiness;
        private readonly ISearchBusiness _searchBusiness;


        public HomeController(IPoetBusiness poetBusiness, IHostingEnvironment environment, IPoetOfPoemsBusiness ofPoemsBusiness, IPoemBusiness poemBusiness, ISearchBusiness searchBusiness)
        {
            _poetBusiness = poetBusiness;
            _environment = environment;
            _ofPoemsBusiness = ofPoemsBusiness;
            _poemBusiness = poemBusiness;
            _searchBusiness = searchBusiness;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search()
        {
            string key = Request.Query["search"];
            List<SearchModel> searchModels = _searchBusiness.Search(key);
            SearchViewModel vm = new SearchViewModel
            {
                Key = key,
                SearchModels = searchModels,
                Count = searchModels.Count
            };

            if (searchModels.Count <= 0)
            {
                vm = new SearchViewModel { Key = key };
            }

            return View(vm);
        }

        public void RunBoat()
        {
            new GeneratorPoetHtml(_environment, _poetBusiness, _poemBusiness).SaveHtml();
            new GeneratorPoetOfPoem(_environment, _ofPoemsBusiness, _poetBusiness, _poemBusiness).SaveHtml();
            new GeneratorPoem(_environment, _poemBusiness, _ofPoemsBusiness, _poetBusiness).SaveHtml();
        }
    }

}
