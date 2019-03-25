using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using SairGezgini.Core.Entities;
using SiirGezgini.Business.Interfaces;
using SiirGezgini.Shared;
using SiirGezgini.Shared.Constants;

namespace SiirGezgini.Business.Generator
{
    public class GeneratorPoem : BaseGenerator
    {
        private readonly IPoemBusiness _poemBusiness;
        private readonly IPoetBusiness _poetBusiness;
        private readonly IHostingEnvironment _environment;
        private readonly IPoetOfPoemsBusiness _ofPoemsBusiness;

        public GeneratorPoem(
            IHostingEnvironment environment,
            IPoemBusiness poemBusiness,
            IPoetOfPoemsBusiness ofPoemsBusiness,
            IPoetBusiness poetBusiness) : base(poemBusiness)
        {
            _environment = environment;
            _poemBusiness = poemBusiness;
            _poetBusiness = poetBusiness;
            _ofPoemsBusiness = ofPoemsBusiness;
        }

        public void SaveHtml()
        {
            foreach (char alphabet in Alphabet.TurkishAlphabets)
            {
                List<Poet> poets = _poetBusiness.GetPoet(alphabet.ToString(), 0, 5000);

                foreach (Poet poet in poets)
                {
                    if (poet.PoetId == 260)
                    {
                        continue;
                    }
                    List<PoemOfPoetInformaton> informations = _ofPoemsBusiness.GetPoetPoems(poet.PoetId, 0, 5000).ToList();

                    foreach (var poetInformaton in informations)
                    {
                        Poem poem = _poemBusiness.GetPoem(poetInformaton.PoemId);

                        string content = GenerateContent(poem);

                        string templateHtml = File.ReadAllText($"{_environment.WebRootPath}{ MailTemplateConstant.GetTemplate(MailTemplateEnum.MailTemplate.Poem)}");

                        int id = new Random().Next(-1, poets.Count);
                        string randomPoem = GenerateRandomPoet(id);

                        templateHtml = templateHtml.Replace("#RANDOMPOEM#", randomPoem);
                        templateHtml = templateHtml.Replace("#CONTENT#", content);

                        string filePath = $"{_environment.WebRootPath}/{BaseDirectoryPathName}/{poem.Title.ReplaceForUrl()}.html";

                       FileHelper.DeleteExistsFiles(filePath);

                        using (StreamWriter sWrite = new StreamWriter(filePath, true, Encoding.UTF8))
                        {
                            sWrite.WriteLine(templateHtml);
                        }

                    }

                }
            }
        }

        public string GenerateContent(Poem poem)
        {
            var html = "<h1>" + poem.Title + "</h1>";
            html += "<pre><p>" + poem.Content + "</p></pre>";
            html += "<h2 class=\'poet-name\'>" + poem.PoetName + "</h3>";

            return html;
        }

        public string BaseDirectoryPathName => @"\Sayfalar\siir\";
    }
}
