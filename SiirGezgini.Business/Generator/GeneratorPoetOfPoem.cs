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
    public class GeneratorPoetOfPoem : BaseGenerator
    {
        private readonly IHostingEnvironment _environment;
        private readonly IPoetOfPoemsBusiness _ofPoemsBusiness;
        private readonly IPoetBusiness _poetBusiness;


        public string BaseDirectoryPathName => @"\Sayfalar\sair\";

        public GeneratorPoetOfPoem(
            IHostingEnvironment environment,
            IPoetOfPoemsBusiness ofPoemsBusiness,
            IPoetBusiness poetBusiness,
            IPoemBusiness poemBusiness) : base(poemBusiness)
        {
            _environment = environment;
            _ofPoemsBusiness = ofPoemsBusiness;
            _poetBusiness = poetBusiness;
        }

        public void SaveHtml()
        {
            foreach (char alphabet in Alphabet.TurkishAlphabets)
            {
                List<Poet> poets = _poetBusiness.GetPoet(alphabet.ToString(), 0, 5000);

                foreach (Poet poet in poets)
                {
                    List<PoemOfPoetInformaton> informations = _ofPoemsBusiness.GetPoetPoems(poet.PoetId, 0, 5000).ToList();

                    var content = GenerateContent(informations);
                    string templateHtml = File.ReadAllText($"{_environment.WebRootPath}{ MailTemplateConstant.GetTemplate(MailTemplateEnum.MailTemplate.PoemsOfPoet)}");

                    int id = new Random().Next(-1, poets.Count);
                    string randomPoem = GenerateRandomPoet(id);

                    templateHtml = templateHtml.Replace("#RANDOMPOEM#", randomPoem);
                    templateHtml = templateHtml.Replace("#CONTENT#", content);

                    string filePath = $"{_environment.WebRootPath}/{BaseDirectoryPathName}/{poet.PoetName.ReplaceForUrl()}.html";

                    DeleteExistsFiles(filePath);

                    using (StreamWriter sWrite = new StreamWriter(filePath, true, Encoding.UTF8))
                    {
                        sWrite.WriteLine(templateHtml);
                    }
                }
            }


        }

        public string GenerateContent(List<PoemOfPoetInformaton> poemInformations)
        {
            int classCounter = 1;

            var html = "<table class=\'table\'> <thead>" +
                       " <tr><th></th>" +
                       " <th>Şiir Adı</th>" +
                       " <th style='width:35%'>Okumak için Tıklayınız</th>" + "" +
                       "</tr></thead>" +
                       "<tbody>";

            for (var i = 0; i < poemInformations.Count; i++)
            {
                var peomsInfo = poemInformations[i];

                string classAttr = "info";

                if (classCounter % 2 == 0)
                {
                    classAttr = "danger";
                }

                string link = $"/sayfalar/siir/{peomsInfo.Title.ReplaceForUrl()}.html";


                html += "<tr class='" + classAttr + "'>" +
                        " <td>" + Convert.ToInt32(i + 1) + "</td>" +
                        " <td>" + peomsInfo.Title + "</td>" +
                        " <td style='text-align: center;'><a href='" + link + "'> Oku </a></td>" +
                        "</tr>";
            }

            html += " </tbody> </table>";

            return html;
        }

        public void DeleteExistsFiles(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
