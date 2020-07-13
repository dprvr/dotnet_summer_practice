using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FilesStorage.Entities.DTOs;

namespace FilesStorage.BLL.Interfaces
{
    public interface ISearchQueryParser
    {
        SearchOptionsDto ParseQuery(FilesSearchDto searchDto);
    }
}
