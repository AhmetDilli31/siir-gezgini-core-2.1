using SairGezgini.Core.Entities;
using SiirGezgini.Business.Interfaces;

namespace SiirGezgini.Business.Generator
{
    public class BaseGenerator
    {
        private readonly IPoemBusiness _poemBusiness;

        public BaseGenerator(IPoemBusiness poemBusiness)
        { 
            _poemBusiness = poemBusiness;
        }

        public string GenerateRandomPoet(int poemId)
        {
            Poem poem = _poemBusiness.GetPoem(poemId);

            var strPoem = "<h4>" + poem.Title + "</h1>";
            strPoem += "<pre style=\'font-size:12px\'><p>" + poem.Content + "</p></pre>";
            strPoem += "<h2 class=\'poet-name\'>" + poem.PoetName + "</h3>";

            var html = "<table>" +
                       "<tbody>" +
                       "<tr>" +
                       "<td><pre>" +
                       strPoem +
                       "</pre></td><tr></table>";
            return html;
        }

    }
}
