using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using SairGezgini.Core.Entities;
using SiirGezgini.Business.Interfaces;
using SiirGezgini.Shared;
using SiirGezgini.Shared.Constants;

namespace SiirGezgini.Business.Generator
{
    public class GeneratorPoetHtml : BaseGenerator
    {
        private readonly IPoetBusiness _poetBusiness;
        private readonly IPoemBusiness _poemBusiness;
        private readonly IHostingEnvironment _environment;

        public GeneratorPoetHtml(IHostingEnvironment environment,
            IPoetBusiness poetBusiness,
            IPoemBusiness poemBusiness) : base(poemBusiness)
        {
            _poetBusiness = poetBusiness;
            _poemBusiness = poemBusiness;
            _environment = environment;
        }

        public string GenerateContent(List<Poet> poets, char alphabet)
        {
            var html = string.Empty;
            html += "<div class='total-poet'> " + alphabet.ToString().ToUpper() + " harfi ile başlayan <span class='total-poet-count'>" + poets.Count + "</span> şair bulundu </div>";

            html += "<table class=\'table table-hover\'>" +
                       "<thead>" +
                       " <tr>" +
                    " <th></th>" +
                          " <th>Adı Soyadı</th>" +
                       " <th style='width:25%'>Eser Sayısı</th>" + "" +
                       "</tr> " +
                       " </thead>" +
                       "<tbody>";

            for (int i = 0; i < poets.Count; i++)
            {
                Poet poet = poets[i];

                string link = $"/sayfalar/sair/{poet.PoetName.ReplaceForUrl()}.html";

                html += "<tr style='cursor:pointer'>" +
                        " <td>" + Convert.ToInt32(i + 1) + "</td>" +
                        " <td>" + poet.PoetName + "</td>" +
                        "<td><a href='" + link + "' <span class='total-poet-count'>" + poet.TotalPoem + " </span> Eser </a></td>" +
                        "</tr>";
            }

            html += " </tbody> </table>";

            return html;
        }

        public new string GenerateRandomPoet(int poemId)
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

        public void SaveHtml()
        {
            foreach (char alphabet in Alphabet.TurkishAlphabets)
            {
                List<Poet> poets = _poetBusiness.GetPoet(alphabet.ToString(), 0, 5000);

                var content = GenerateContent(poets, alphabet);

                string templateHtml = File.ReadAllText($"{_environment.WebRootPath}{ MailTemplateConstant.GetTemplate(MailTemplateEnum.MailTemplate.Poet)}");
                int id = new Random().Next(-1, poets.Count);
                string randomPoem = GenerateRandomPoet(id);

                templateHtml = templateHtml.Replace("#RANDOMPOEM#", randomPoem);
                templateHtml = templateHtml.Replace("#CONTENT#", content);

                string filePath = $"{_environment.WebRootPath}/Sayfalar/sairler/{alphabet.ToString().ToLower()}-harfi-ile-baslayan-sairler.html";

                FileHelper.DeleteExistsFiles(filePath);

                using (StreamWriter sWrite = new StreamWriter(filePath, true, Encoding.UTF8))
                {
                    sWrite.WriteLine(templateHtml);
                }
            }
        }
    }
}
