using System.Collections.Generic;

namespace SairGezgini.Core.Entities
{
  public  class PoemOfPoetItem
    {
        public int PoetId { get; set; }
        public string PoetName { get; set; }
        public List<PoemOfPoetInformaton> PoemOfPoetResponse { get; set; }
        public int TotalPageNumber { get; set; }
        public int TotalPoemNumber { get; set; }
    }
}
