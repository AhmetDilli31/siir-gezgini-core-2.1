using System;

namespace SiirGezgini.Shared.Constants
{
    public static class MailTemplateConstant
    {
        public static string GetTemplate(MailTemplateEnum.MailTemplate template)
        {
            string index;

            switch (template)
            {
                case MailTemplateEnum.MailTemplate.Poet:
                    index = @"\Sayfalar\sairler\BaseTemplate\poet.html";
                    break;

                case MailTemplateEnum.MailTemplate.Poem:
                    index = @"\Sayfalar\siir\BaseTemplate\poem.html";
                    break;

                case MailTemplateEnum.MailTemplate.PoemsOfPoet:
                    index = @"\Sayfalar\sair\BaseTemplate\poemofpeot.html";
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(template), template, null);
            }

            return index;
        }


    }
}
