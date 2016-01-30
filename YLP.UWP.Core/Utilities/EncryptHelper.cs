using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace YLP.UWP.Core
{
    public class EncryptHelper
    {
        public static string MD5Encrypt(string strSource)
        {
            //可以选择MD5 Sha1 Sha256 Sha384 Sha512
            string strAlgName = HashAlgorithmNames.Md5;

            // 创建一个 HashAlgorithmProvider 对象
            HashAlgorithmProvider objAlgProv = HashAlgorithmProvider.OpenAlgorithm(strAlgName);

            // 创建一个可重用的CryptographicHash对象           
            CryptographicHash objHash = objAlgProv.CreateHash();

            IBuffer buffMsg = CryptographicBuffer.ConvertStringToBinary(strSource, BinaryStringEncoding.Utf8);
            objHash.Append(buffMsg);
            IBuffer buffHash = objHash.GetValueAndReset();

            string strHash = CryptographicBuffer.EncodeToHexString(buffHash);

            return strHash;
        }
    }
}
