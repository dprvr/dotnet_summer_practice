using System.Linq;
using FilesStorage.BLL.Interfaces;
using FilesStorage.Entities.DTOs;

namespace FilesStorage.BLL
{
    public class SearchQueryParser : ISearchQueryParser
    {
        private static string str = "n:filename t:pdf";
        
        private char[] queryOptionsSeps = { ' ', ',' };
        private char[] commandValueSep = { ':' };

        public SearchOptionsDto ParseQuery(FilesSearchDto searchDto)
        {
            var dto = new SearchOptionsDto();
            var subItems = searchDto.SearchString.Split(queryOptionsSeps);
            foreach(var c in subItems)
            {
                var strs = c.Split(commandValueSep);
                var command = strs[0];
                var value = strs[1];
                SetLinkedProperties(command, value);
            }
            return dto;

            void SetLinkedProperties(string _command, string _value)
            {
                switch (_command)
                {
                    case "n":
                        dto.FileName = _value;
                        break;
                    case "t":
                        dto.FileType = _value;
                        break;
                    case "#":
                        dto.TagsNames.Append(_value);
                        break;
                    default:
                        break;
                }                
            }
            
        }
    }
}
