using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using SairGezgini.Models;
using SiirGezgini.Business.Interfaces;
using SiirGezgini.Shared;

namespace SiirGezgini.Business
{
    public class SearchBusiness : ISearchBusiness
    {
        private readonly IHostingEnvironment _environment;

        public SearchBusiness(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public List<SearchModel> Search(string value)
        {
            value = value.ReplaceForUrl();

            var poemList = FileHelper.GetFilesTitles($"{_environment.WebRootPath}\\Sayfalar\\sair");
            var poetList = FileHelper.GetFilesTitles($"{_environment.WebRootPath}\\Sayfalar\\siir");

            var model = new List<SearchModel>();

            List<string> listPoems = poemList.Where(x => x.Contains(value)).Select(Path.GetFileName).ToList();

            foreach (var _poem in listPoems)
            {
                model.Add(new SearchModel
                {
                    Title = _poem.Replace(".html", "").Replace("\\", "").Replace("-", " "),
                    Url = $"/sayfalar/sair/{_poem}",
                    PoemName = _poem.Replace(".html", "").Replace("\\", "").Replace("-", " ")
                });
            }

            List<string> listPoets = poetList.Where(x => x.Contains(value)).Select(Path.GetFileName).ToList();
            foreach (var _poet in listPoets)
            {
                model.Add(new SearchModel
                {
                    Title = _poet.Replace(".html", "").Replace("\\", "").Replace("-", " "),
                    Url = $"/sayfalar/siir/{_poet}",
                    PoemName = _poet.Replace(".html", "").Replace("\\", "").Replace("-", " ")
                });
            }

            return model.GroupBy(x => x.PoemName).Select(x => x.First()).OrderBy(x => x.Title).ToList();
        }
    }
}
