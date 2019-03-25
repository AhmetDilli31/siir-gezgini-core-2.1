using SairGezgini.Core.Entities;
using SiirGezgini.Business.Interfaces;
using SiirGezgini.Repository;

namespace SiirGezgini.Business
{
    public class PoemBusiness : IPoemBusiness
    {
        private readonly IPoemService _poemService;

        public PoemBusiness(IPoemService poemService)
        {
            _poemService = poemService;
        }

        public Poem GetPoem(int poemId)
        {
            return _poemService.GetPoem(poemId);
        }
    }
}
