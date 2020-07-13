using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using Bogus;

using FilesStorage.Entities.Entities;
using FilesStorage.Entities.Enums;

namespace FilesStorage.DAL.EF.Helpers
{
    public class EntitiesGenerator
    {
        

        public EntitiesGenerator()
        {
            
        }

        public IEnumerable<StorageFile> CreateFiles(Storage storage)
        {
            return  new List<StorageFile>
            {
                new StorageFile
                {
                    Name = "skdjsd",
                    CreationDate = DateTime.Now,
                    FileType = FileType.doc,
                    Storage = storage,
                    Tags = new List<StorageTag>{ new StorageTag { Id = 1, Name="books"}, new StorageTag {Id = 3, Name = "Film"} }
                },

                new StorageFile
                {
                    Name = "apoow",
                    CreationDate = DateTime.Now,
                    FileType = FileType.doc,
                    Storage = storage,
                },
                new StorageFile
                {
                    Name = "popweo",
                    CreationDate = DateTime.Now,
                    FileType = FileType.doc,
                    Storage = storage,
                },
                new StorageFile
                {
                    Name = "dopd",
                    CreationDate = DateTime.Now,
                    FileType = FileType.doc,
                    Storage = storage,
                },
            };
        }


        public IEnumerable<FileAndTag> CreateTagsFilesRelation(IList<StorageFile> files, IList<StorageTag> tags)
        {
            return new List<FileAndTag>()
            {
                new FileAndTag
                {
                    File = files[1],
                    Tag = tags[3],
                },
                new FileAndTag
                {
                    File = files[3],
                    Tag = tags[3],
                },
                new FileAndTag
                {
                    File = files[1],
                    Tag = tags[2],
                },
                new FileAndTag
                {
                    File = files[1],
                    Tag = tags[3],
                },
                new FileAndTag
                {
                    File = files[3],
                    Tag = tags[2],
                },
                new FileAndTag
                {
                    File = files[2],
                    Tag = tags[1],
                },
            };
        }

        public IEnumerable<StorageTag> CreateTags(Storage storage)
        {
            var list = new List<StorageTag>()
            {
                new StorageTag()
                {
                    Id = 1,
                    Name = "fun",
                    Storage = storage,
                },

                new StorageTag()
                {
                    Id = 2,
                    Name = "work",
                    Storage = storage,
                },

                new StorageTag()
                {
                    Id = 3,
                    Name = "sport",
                    Storage = storage,
                },

                new StorageTag()
                {
                    Id = 4,
                    Name = "study",
                    Storage = storage,
                },
            };
            return list.AsEnumerable();
        }



        public IEnumerable<StorageFile> CreateFakeFiles(int count, Storage storage)
        {
            var faker = new Faker();
            var date = new DateTime(2000, 03, 14);
            var files = new List<StorageFile>();
            for(int i = 0; i < count; i++)
            {
                files.Add(new StorageFile
                {
                    Name = faker.Random.Word(),
                    Description = string.Join(" ", faker.Random.Words(10)),
                    FileType = faker.PickRandom<FileType>(),
                    CreationDate = faker.Date.Past(),
                    Storage = storage,
                });
            }
            return files;
        }

        public IEnumerable<StorageTag> GenerateTags(int count, Storage storage)
        {
            var faker = new Faker();
            var tags = new List<StorageTag>();
            for(int i = 0; i < count; i++)
            {
                tags.Add(new StorageTag
                {
                    Name = faker.Random.Word(),
                    Storage = storage,
                });                
            }
            return tags;
        }

        public Account CreateAccount()
        {
            var faker = new Faker();
            var account = GenerateAccount("admin", "admin");
            return account;

            Account GenerateAccount(string login, string password)
            {
                return new Account()
                {
                    Login = login,
                    HashedPassword = Hash(password),
                    CreationDate = DateTime.Now,
                };
            }

            // crack(-> BL)
            //REMOVE
            string Hash(string str)
            {
                HashAlgorithm algorithm = new SHA256CryptoServiceProvider();
                var hash = Convert.ToBase64String(algorithm.ComputeHash(Encoding.UTF8.GetBytes(str)));
                return hash;
            }

        }

        public User GenerateUser()
        {
            var faker = new Faker();
            return new User()
            {
                FirstName = faker.Name.FirstName(),
                LastName = faker.Name.LastName(),
                Birthday = faker.Date.Past(),
                Email = faker.Internet.Email(),
                Gender = faker.PickRandom<Gender>(),
            };
        }

    }
}
