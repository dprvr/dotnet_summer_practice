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
        public FilesAndTagsRepository(FilesStorageContext context, Action<Exception> commandFailure) : base(context, commandFailure)
        {

        }

        public IEnumerable<StorageTag> GetFileTags(int fileId)
        {
            return FindEntityById<StorageFile, int>(fileId).Tags.ToList();
            //return Query<FileAndTag>().Where(m => m.File.Id == fileId).Select(m => m.Tag).ToList();
        }

        public IEnumerable<StorageFile> GetTagFiles(int tagId)
        {
            return Query<FileAndTag>().Where(m => m.Tag.Id == tagId).Select(m => m.File).ToList();
        }

        public void AddTagsToFile(int fileId, params int[] tagsIds)
        {
            Command(c =>
            {
                var set = c.Set<FileAndTag>();
                var file = new StorageFile { Id = fileId };
                var tag = new StorageTag();
                foreach(var tagId in tagsIds)
                {
                    tag.Id = tagId;
                    set.Add(new FileAndTag
                    {
                        File = file,
                        Tag = tag,
                    });
                }
            });
        }

        public void DeleteTagsFromFile(int fileId, params int[] tagsIds)
        {
            Command(c =>
            {
                var set = c.Set<FileAndTag>();
                var found = set.Where(r => r.File.Id == fileId && tagsIds.Contains(r.Tag.Id));
                set.RemoveRange(found);
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

                if (!String.IsNullOrEmpty(searchDto.FileType))
                {
                    var getFileTypeAsString = Call(Property(enter, "FileType"), typeof(FileType).GetMethod("ToString"));
                    var CheckFileType = Equal(getFileTypeAsString, Constant(searchDto.FileType));

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
