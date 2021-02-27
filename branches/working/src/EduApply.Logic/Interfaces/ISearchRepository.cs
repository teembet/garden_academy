using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduApply.Data.Entities;

namespace EduApply.Logic.Interfaces
{
    public interface ISearchRepository
    {
        IEnumerable<SearchResult> GetSearchResult(SearchResultQuery query);

        IEnumerable<SearchResult> GetSearchResult(string regNum, string appNum);
    }
}
