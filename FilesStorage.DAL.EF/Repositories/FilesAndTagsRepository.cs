using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using FilesStorage.DAL.Interfaces;
using FilesStorage.Entities.DTOs;
using FilesStorage.Entities.Entities;
using FilesStorage.Entities.Enums;

using static System.Linq.Expressions.Expression;

namespace FilesStorage.DAL.EF.Repositories
{
    public class FilesAndTagsRepository : BaseRepository, IFilesAndTagsRepository
    {
        public FilesAndTagsRepository(FilesStorageContext context,
            Action<Exception> commandFailure) : base(context, commandFailure)
        {

        }

        public IEnumerable<StorageTag> GetFileTags(int fileId)
        {
            return FindEntityById<StorageFile, int>(fileId).Tags.ToList();
        }

        public IEnumerable<StorageFile> GetTagFiles(int tagId)
        {
            return LoadWithProperties<StorageTag>(t => t.Files).FirstOrDefault(t => t.Id == tagId).Files.ToList();
        }

        public void AddTagsToFile(int fileId, params int[] tagsIds)
        {
            Command(c =>
            {
                var file = FindEntityById<StorageFile, int>(fileId, false);
                foreach(var tagId in tagsIds)
                {
                    file.Tags.Add(new StorageTag { Id = tagId });
                }
            });
        }

        public void DeleteTagsFromFile(int fileId, params int[] tagsIds)
        {
            Command(c =>
            {
                var file = FindEntityById<StorageFile, int>(fileId, false);
                var deletingTags = file.Tags.Where(t => tagsIds.Contains(t.Id)).ToList();
                foreach(var tag in deletingTags)
                {
                    file.Tags.Remove(tag);
                }
            });
        }

        public IEnumerable<StorageFile> Search(SearchOptionsDto searchDto)
        {
            var query = BuildQueryTree();
            return Query<StorageFile>().Where(query).ToList();
            
            Expression<Func<StorageFile, bool>> BuildQueryTree()
            {
                var enter = Parameter(typeof(StorageFile), "storage_file");
                var IsInTheUserStorage = Equal(Property(Property(enter, "Storage"), "Id"), Constant(searchDto.StorageId));

                var searchQuery = IsInTheUserStorage;

                if (searchDto.FileType.HasValue)
                {
                    //exception enum.toStr not supported by expressions 
                    //var getFileTypeAsString = Call(Property(enter, "FileType"),
                    //    typeof(FileType).GetMethod("ToString", System.Type.EmptyTypes));

                    var necessaryType = Constant(searchDto.FileType.Value);
                    var CheckFileType = Equal(Property(enter, "FileType"), necessaryType);
                    searchQuery = AndAlso(searchQuery, CheckFileType);
                }

                if (!String.IsNullOrEmpty(searchDto.FileName))
                {
                    var IsFileNameContainsStr = Call(Property(enter, "Name"),
                    typeof(string).GetMethod("Contains"), Constant(searchDto.FileName));
                    var CheckFileName = Equal(IsFileNameContainsStr, Constant(true));
                    searchQuery = AndAlso(searchQuery, CheckFileName);
                }

                var finalPredicate = Lambda<Func<StorageFile, bool>>(searchQuery, enter);
                return finalPredicate;
            }

        }

    }
}
