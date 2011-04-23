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
using System.Text.RegularExpressions;
using System.Web;

namespace mobile_twitter
{
    public static class StatusFormattingHelper
    {
        public static readonly Regex LinkRegex =
            new Regex(@"\b(([\w-]+://?|www[.])[^\s()<>]+(?:\([\w\d]+\)|([^[:punct:]\s]|/)))", RegexOptions.Compiled);

        public static readonly Regex MentionRegex = new Regex(@"(?<!\w)(@(\w+))", RegexOptions.Compiled);
        public static readonly Regex HashtagRegex = new Regex(@"(?<!\w)(#(\w+))", RegexOptions.Compiled);

        public static string FormatLinksAndMentions(string statusText)
        {
            statusText = LinkRegex.Replace(statusText, Linker);
            statusText = MentionRegex.Replace(statusText, "<a href=\"/user/$2\">$1</a>");

            statusText = HashtagRegex.Replace(statusText,
                                              m =>
                                              "<a href=\"/search/?q=" + HttpUtility.UrlEncode(m.Captures[0].Value) +
                                              "\">" + m.Captures[0] + "</a>");

            return statusText;
        }

        private static string Linker(Match match)
        {
            if (match.Groups[1].Value.Contains("://"))
                return "<a target=\"_blank\" href=\"" + match.Groups[1].Value + "\">" + match.Groups[1].Value + "</a>";

            return "<a target=\"_blank\" href=\"http://" + match.Groups[1].Value + "\">" + match.Groups[1].Value +
                   "</a>";
        }

        public static string FormatPrettyDate(DateTime date)
        {
            var now = DateTime.UtcNow;

            var span = now - date;

            if (span.TotalMinutes < 1)
            {
                return (int) span.TotalSeconds + " seconds ago";
            }

            if (span.TotalHours < 1)
            {
                return (int) span.TotalMinutes + " minutes ago";
            }

            if (span.TotalDays < 1)
            {
                return (int) span.TotalHours + " hours ago";
            }

            if (span.TotalDays < 30)
            {
                return (int) span.TotalDays + " days ago";
            }

            if (span.TotalDays < 90)
            {
                return (int) span.TotalDays/7 + " weeks ago";
            }

            return date.ToLocalTime().ToString("h:mm tt MMM d, yyyy");
        }
    }
}