using System;
using System.Security.Cryptography;
using System.Text;
using FilesStorage.BLL.Interfaces;

namespace FilesStorage.BLL
{
    public class Hasher : IHasher
    {
        public Hasher()
        {

        }

        public string Hash(string str)
        {
            HashAlgorithm algorithm = new SHA256CryptoServiceProvider();
            var hash = Convert.ToBase64String(algorithm.ComputeHash(Encoding.UTF8.GetBytes(str)));
            return hash;
        }
    }
}
