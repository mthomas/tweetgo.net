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
using System.Security.Cryptography;
using System.Text;
using mobile_twitter.Model;

namespace mobile_twitter.Infrastucture
{
    public static class SignedString
    {
        public static string CreateSignedString(string message, string key)
        {
            return CreateSignature(message, key) + ":" + message;
        }

        private static string CreateSignature(string message, string key)
        {
            var messageBytes = Encoding.Default.GetBytes(message);
            var keyBytes = Encoding.Default.GetBytes(key);

            var hmac = HMAC.Create();
            hmac.Key = keyBytes;

            return Convert.ToBase64String(hmac.ComputeHash(messageBytes));
        }

        public static string ExtractAndVerifyMessage(string signedString, string key)
        {
            var parts = signedString.Split(new[] {':'}, 2);

            if (parts.Length != 2)
            {
                throw new InvalidSignatureExecption();
            }

            var signature = parts[0];
            var message = parts[1];

            if (!SecureEquals(CreateSignature(message, key), signature))
            {
                throw new InvalidSignatureExecption();
            }

            return message;
        }

        private static bool SecureEquals(string s1, string s2)
        {
            if (s1 == null) throw new ArgumentNullException("s1");
            if (s2 == null) throw new ArgumentNullException("s2");

            if (s1.Length != s2.Length) return false;

            var same = true;

            for (var i = 0; i < s1.Length; i++)
            {
                same = same && s1[i] == s2[i];
            }

            return same;
        }
    }
}