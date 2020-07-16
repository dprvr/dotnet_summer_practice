using System;
using System.Linq;
using FilesStorage.BLL.Interfaces;
using FilesStorage.Entities.DTOs;
using FilesStorage.Entities.Enums;

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
            try
            {
                var subItems = searchDto.SearchString.Split(queryOptionsSeps);
                foreach (var c in subItems)
                {
                    var strs = c.Split(commandValueSep);
                    var command = strs[0];
                    var value = strs[1];
                    SetLinkedProperties(command, value);
                }
            }
            catch
            {
                throw new ArgumentException("The search string format is incorrect"); 
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
                        {
                            if (Enum.TryParse(_value, out FileType fileType))
                                dto.FileType = fileType;
                            break;
                        }                        
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
