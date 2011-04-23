#region license

// Copyright (c) 2011 Michael Thomas
// 
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace mobile_twitter.Infrastucture
{
    public class Encryptor
    {
        #region IV

        private static readonly byte[] IV = new byte[]
                                                {
                                                    198, 232, 192, 188, 254, 220, 121, 3,
                                                    113, 218, 235, 4, 124, 143, 6, 86,
                                                    187, 29, 132, 255, 221, 50, 238, 173,
                                                    224, 55, 28, 197, 223, 188, 251, 173,
                                                    31, 68, 102, 150, 7, 254, 136, 33,
                                                    93, 243, 72, 211, 89, 202, 146, 171,
                                                    12, 154, 84, 168, 237, 3, 107, 135,
                                                    165, 203, 174, 145, 147, 14, 113, 144,
                                                    194, 60, 117, 255, 49, 83, 164, 23,
                                                    134, 203, 108, 207, 48, 141, 135, 195,
                                                    215, 23, 138, 0, 147, 236, 47, 169,
                                                    156, 238, 199, 214, 108, 65, 106, 71,
                                                    130, 142, 21, 68, 188, 193, 22, 16,
                                                    200, 163, 202, 109, 55, 230, 55, 10,
                                                    231, 221, 218, 32, 13, 242, 241, 90,
                                                    158, 138, 34, 110, 26, 180, 102, 51,
                                                    210, 201, 177, 170, 255, 249, 61, 144,
                                                    156, 216, 55, 188, 193, 68, 243, 17,
                                                    168, 105, 232, 133, 172, 59, 49, 199,
                                                    63, 190, 218, 24, 198, 129, 131, 68,
                                                    245, 32, 196, 32, 130, 125, 14, 204,
                                                    19, 149, 228, 177, 148, 165, 37, 12,
                                                    155, 119, 10, 88, 179, 229, 248, 131,
                                                    218, 85, 46, 182, 82, 24, 130, 238,
                                                    186, 83, 83, 201, 210, 111, 10, 36,
                                                    147, 35, 154, 7, 27, 127, 163, 214,
                                                    127, 15, 216, 12, 236, 21, 155, 169,
                                                    197, 169, 120, 69, 194, 200, 22, 32,
                                                    168, 212, 190, 95, 191, 19, 169, 226,
                                                    195, 46, 96, 242, 22, 65, 157, 136,
                                                    211, 164, 226, 176, 200, 167, 219, 167,
                                                    232, 84, 105, 228, 88, 66, 190, 28
                                                };

        #endregion

        #region DefaultSalt

        private static readonly byte[] DefaultSalt = new byte[]
                                                         {
                                                             164, 92, 89, 248, 245, 24, 105, 176,
                                                             15, 36, 110, 234, 35, 80, 38, 192,
                                                             171, 195, 227, 140, 207, 0, 83, 71,
                                                             42, 36, 151, 249, 2, 84, 66, 129,
                                                             92, 19, 241, 198, 188, 219, 34, 110,
                                                             231, 101, 247, 134, 145, 170, 72, 87,
                                                             145, 48, 117, 181, 120, 251, 135, 78,
                                                             240, 90, 87, 72, 178, 74, 103, 82,
                                                             205, 29, 153, 48, 194, 132, 21, 206,
                                                             137, 55, 92, 105, 149, 192, 100, 188,
                                                             80, 112, 93, 46, 65, 195, 172, 223,
                                                             38, 240, 71, 19, 56, 244, 205, 174,
                                                             60, 105, 163, 251, 0, 55, 33, 39,
                                                             81, 135, 59, 84, 162, 26, 230, 166,
                                                             19, 128, 170, 228, 104, 249, 137, 102,
                                                             67, 3, 1, 237, 105, 104, 119, 84,
                                                             248, 57, 167, 90, 157, 169, 230, 237,
                                                             159, 164, 15, 133, 155, 124, 175, 133,
                                                             179, 16, 40, 224, 234, 64, 220, 137,
                                                             105, 60, 88, 108, 218, 126, 206, 28,
                                                             155, 230, 192, 191, 139, 150, 63, 232,
                                                             131, 54, 135, 11, 22, 82, 196, 20,
                                                             127, 218, 67, 42, 203, 4, 30, 17,
                                                             198, 126, 71, 98, 227, 219, 244, 129,
                                                             125, 152, 179, 56, 182, 187, 142, 227,
                                                             32, 0, 248, 2, 5, 33, 30, 128,
                                                             28, 9, 118, 36, 50, 78, 138, 153,
                                                             136, 226, 110, 136, 213, 152, 240, 113,
                                                             198, 34, 190, 34, 107, 154, 190, 144,
                                                             94, 124, 181, 91, 228, 9, 193, 143,
                                                             199, 95, 179, 234, 49, 214, 207, 209,
                                                             49, 196, 58, 208, 67, 31, 29, 11,
                                                         };

        #endregion

        public static byte[] CreateEncryptionKeyFromPassword(int keyByteLength, string password)
        {
            var deriveBytes = new Rfc2898DeriveBytes(password, DefaultSalt);
            return deriveBytes.GetBytes(keyByteLength);
        }

        public static string EncryptString(string data, string password)
        {
            return Encrypt(Encoding.UTF8.GetBytes(data), password);
        }

        public static string DescryptString(string data, string password)
        {
            return Encoding.UTF8.GetString(Decrypt(data, password)).TrimEnd('\0');
        }

        private static string Encrypt(byte[] data, string password)
        {
            using (var ms = new MemoryStream())
            {
                var crypto = new RijndaelManaged();

                crypto.Key = CreateEncryptionKeyFromPassword(crypto.KeySize/8, password);
                crypto.IV = IV.Take(crypto.BlockSize/8).ToArray();
                crypto.Mode = CipherMode.CBC;

                using (var encryptor = crypto.CreateEncryptor())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.FlushFinalBlock();
                    }
                }

                return Convert.ToBase64String(ms.ToArray());
            }
        }

        private static byte[] Decrypt(string data, string password)
        {
            using (var ms = new MemoryStream(Convert.FromBase64String(data)))
            {
                var crypto = new RijndaelManaged();

                crypto.Key = CreateEncryptionKeyFromPassword(crypto.KeySize/8, password);
                crypto.IV = IV.Take(crypto.BlockSize/8).ToArray();
                crypto.Mode = CipherMode.CBC;

                var decrypted = new byte[ms.Length];

                using (var decryptor = crypto.CreateDecryptor())
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        cs.Read(decrypted, 0, decrypted.Length - 1);
                    }
                }

                return decrypted;
            }
        }
    }
}