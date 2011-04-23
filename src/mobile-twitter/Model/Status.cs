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
using TweetSharp.Twitter.Model;

namespace mobile_twitter.Model
{
    public class Status : IEquatable<Status>
    {
        public Status(TwitterStatus status)
        {
            Id = status.Id;
            IsFavorited = status.IsFavorited;

            if (status.User != null)
            {
                UserScreenName = status.User.ScreenName;
                UserProfileImageUrlNormal = status.User.ProfileImageUrl;
                UserProfileImageUrlBigger = status.User.ProfileImageUrl.Replace("_normal.", "_bigger.");
            }

            if (status.RetweetedStatus != null)
            {
                Text =
                    StatusFormattingHelper.FormatLinksAndMentions("RT " + "@" + status.RetweetedStatus.User.ScreenName +
                                                                  ": " + status.RetweetedStatus.Text);
            }
            else
            {
                Text = StatusFormattingHelper.FormatLinksAndMentions(status.Text);
            }

            CreateDateRaw = status.CreatedDate;
            CreatedDate = StatusFormattingHelper.FormatPrettyDate(status.CreatedDate);
            Source = status.Source;

            InReplyToStatusId = status.InReplyToStatusId == 0 ? null : status.InReplyToStatusId;
            InReplyToScreenName = status.InReplyToScreenName;

            IsTruncated = status.IsTruncated;
        }

        public long Id { get; set; }

        public bool IsFavorited { get; set; }

        public string UserScreenName { get; set; }
        public string UserProfileImageUrlNormal { get; set; }
        public string UserProfileImageUrlBigger { get; set; }

        public string Text { get; set; }
        public string CreatedDate { get; set; }
        public string Source { get; set; }

        public long? InReplyToStatusId { get; set; }
        public string InReplyToScreenName { get; set; }

        public bool IsTruncated { get; set; }

        public DateTime CreateDateRaw { get; set; }

        #region IEquatable<Status> Members

        public bool Equals(Status other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id == Id;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Status)) return false;
            return Equals((Status) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Status left, Status right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Status left, Status right)
        {
            return !Equals(left, right);
        }
    }
}