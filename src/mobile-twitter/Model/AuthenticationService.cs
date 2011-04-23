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
using System.Web;
using System.Web.Script.Serialization;
using mobile_twitter.Infrastucture;
using TweetSharp.Model;
using TweetSharp.Twitter.Extensions;
using TweetSharp.Twitter.Fluent;

namespace mobile_twitter.Model
{
    public class AuthenticationService
    {
        public const string CookieName = "oauth_token";

        private const string SigningKey = "A";
        private const string EncryptingKey = "A";

        public string GetAuthenticationUrl()
        {
            var twitter = FluentTwitter.CreateRequest().Authentication.GetRequestToken(OAuth.GetOAuthSecrets().Key,
                                                                                       OAuth.GetOAuthSecrets().Secret);

            var response = twitter.Request();
            var token = response.AsToken();

            var error = response.AsError();

            if (error != null)
            {
                throw new Exception(error.ErrorMessage);
            }

            if (token == null)
            {
                if (response.Exception != null)
                {
                    throw new Exception("Error getting authentication request token", response.Exception);
                }

                throw new Exception("error getting authentication request token error code " +
                                    response.ResponseHttpStatusCode);
            }

            return FluentTwitter.CreateRequest().Authentication.GetAuthorizationUrl(token.Token).Replace("/authorize",
                                                                                                         "/authenticate");
        }

        public string CreateAndSignAccessToken(string oauth_token)
        {
            var accessToken = FluentTwitter.CreateRequest()
                .Authentication.GetAccessToken(OAuth.GetOAuthSecrets().Key, OAuth.GetOAuthSecrets().Secret, oauth_token);

            var response = accessToken.Request();
            var result = response.AsToken();

            if (result == null)
            {
                var error = response.AsError();
                if (error != null)
                {
                    throw new Exception(error.ErrorMessage);
                }

                throw new Exception();
            }

            if (result.Token != null && result.TokenSecret != null && result.ScreenName != null)
            {
                return SignedString.CreateSignedString(Encryptor.EncryptString(SerializeToken(result), EncryptingKey),
                                                       SigningKey);
            }
            else
            {
                return null;
            }
        }

        public OAuthToken GetTokenFromCooke(HttpCookie cookie)
        {
            if (cookie == null)
            {
                return null;
            }

            if (cookie.Value == null)
            {
                return null;
            }

            try
            {
                var serialized = Encryptor.DescryptString(
                    SignedString.ExtractAndVerifyMessage(cookie.Value, SigningKey),
                    EncryptingKey);

                return DeserializeToken(serialized);
            }
            catch (CryptographicException)
            {
                //a cryptographic exception could be thrown if we changed keys...
                //beware though, this can disguise bad crypto code too
                return null;
            }
            catch (InvalidSignatureExecption)
            {
                return null;
            }
        }

        private static OAuthToken DeserializeToken(string serialized)
        {
            return new JavaScriptSerializer().Deserialize<OAuthToken>(serialized);
        }

        private static string SerializeToken(OAuthToken result)
        {
            return new JavaScriptSerializer().Serialize(result);
        }
    }
}