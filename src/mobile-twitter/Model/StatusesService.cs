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
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TweetSharp.Model;
using TweetSharp.Twitter.Model;

namespace mobile_twitter.Model
{
    public class StatusesService : Service
    {
        private readonly Dictionary<long, Status> _statusCache = new Dictionary<long, Status>();

        private readonly Dictionary<string, ICollection<Status>> _userTimelineCache =
            new Dictionary<string, ICollection<Status>>();

        private int calls;

        public StatusesService(OAuthToken currentUserToken) : base(currentUserToken)
        {
        }

        public ICollection<Status> FollowConversation(long tweetId)
        {
            var first = GetSingleStatus(tweetId);

            var conversation = new Dictionary<long, Status>();
            var participants = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);

            conversation.Add(first.Id, first);

            for (var i = 0; i < 10; i++)
            {
                var convoCount = conversation.Count;
                var participantCount = participants.Count;

                foreach (var participant in participants)
                {
                    var others = participants.Except(new[] {participant});

                    var participantTimeline = UserTimeline(participant);

                    foreach (var status in participantTimeline)
                    {
                        foreach (var other in others)
                        {
                            if (Regex.IsMatch(status.Text, other, RegexOptions.IgnoreCase))
                            {
                                if (!conversation.ContainsKey(status.Id))
                                {
                                    conversation.Add(status.Id, status);
                                }
                            }
                        }

                        if (status.InReplyToStatusId != null && conversation.ContainsKey(status.InReplyToStatusId.Value))
                        {
                            if (!conversation.ContainsKey(status.Id))
                            {
                                conversation.Add(status.Id, status);
                            }
                        }
                    }
                }

                foreach (var status in conversation.Values)
                {
                    participants.Add(status.UserScreenName);
                }

                foreach (var status in conversation.Values.ToArray())
                {
                    var matches =
                        StatusFormattingHelper.MentionRegex.Matches(status.Text).Cast<Match>().Select(m => m.Groups[1]).
                            ToList();
                    foreach (var match in matches)
                    {
                        participants.Add(match.ToString().TrimStart('@'));
                    }
                }


                foreach (var status in conversation.Values.ToArray())
                {
                    if (status.InReplyToStatusId != null)
                    {
                        if (!conversation.ContainsKey(status.InReplyToStatusId.Value))
                        {
                            conversation.Add(status.InReplyToStatusId.Value,
                                             GetSingleStatus(status.InReplyToStatusId.Value));
                        }
                    }
                }


                if (convoCount == conversation.Count && participantCount == participants.Count)
                    break;

                if (conversation.Count >= 50)
                    break;
            }


            var statuses = conversation.Values.ToList().OrderByDescending(s => s.CreateDateRaw).ToList();

            return statuses;
        }

        private ICollection<Status> UserTimeline(string participant)
        {
            ICollection<Status> statuses;

            if (_userTimelineCache.TryGetValue(participant, out statuses))
            {
                return statuses;
            }

            calls++;
            statuses = new UserService(_currentUserToken).Statuses(participant, null, null);

            _userTimelineCache.Add(participant, statuses);

            foreach (var status in statuses)
            {
                if (!_statusCache.ContainsKey(status.Id))
                {
                    _statusCache.Add(status.Id, status);
                }
            }

            return statuses;
        }

        public ICollection<Status> Favorites(int? page)
        {
            return _service.ListFavoriteTweets(page.AsPage())
                .Select(s => new Status(s))
                .ToList();
        }

        public ICollection<Status> Mentions(long? newerThan, long? olderThan)
        {
            if (newerThan != null)
            {
                return _service.ListTweetsMentioningMeSince(newerThan.Value).Select(s => new Status(s)).ToList();
                ;
            }

            if (olderThan != null)
            {
                return _service.ListTweetsMentioningMeBefore(olderThan.Value).Select(s => new Status(s)).ToList();
                ;
                ;
            }

            return _service.ListTweetsMentioningMe().Select(s => new Status(s)).ToList();
        }

        public ICollection<Status> OnHomeTimeline(long? newerThan, long? olderThan)
        {
            if (newerThan != null && olderThan != null)
            {
                olderThan = null;
            }

            var onHomeTimeline = GetOnHomeTimeline(newerThan, olderThan).Where(s => s != null);


            return onHomeTimeline.Select(s => new Status(s)).ToArray();

            if (!onHomeTimeline.Any() && newerThan == null && olderThan == null)
            {
                return new Status[] {};
            }

            DateTime oldestStatus;

            if (onHomeTimeline.Any())
            {
                oldestStatus = onHomeTimeline.Min(s => s.CreatedDate);
            }
            else
            {
                oldestStatus = new DateTime(1900, 1, 1);
            }

            var retweetedByMe = GetRetweetedByMe(newerThan, olderThan);

            if (newerThan == null)
            {
                retweetedByMe = retweetedByMe.Where(s => s.CreatedDate >= oldestStatus);
            }

            return
                onHomeTimeline.Select(s => new Status(s))
                    .Union(retweetedByMe.Select(s => new Status(s)))
                    .Distinct()
                    .OrderByDescending(s => s.Id)
                    .ToList();
        }

        private IEnumerable<TwitterStatus> GetOnHomeTimeline(long? newerThan, long? olderThan)
        {
            if (newerThan != null)
            {
                return _service.ListTweetsOnHomeTimelineSince(newerThan.Value);
            }

            if (olderThan != null)
            {
                return _service.ListTweetsOnHomeTimelineBefore(olderThan.Value);
            }

            return _service.ListTweetsOnHomeTimeline();
        }

        private IEnumerable<TwitterStatus> GetRetweetedByMe(long? newerThan, long? olderThan)
        {
            if (newerThan != null)
            {
                return _service.ListRetweetsByMeSince(newerThan.Value, 50);
            }

            if (olderThan != null)
            {
                return _service.ListRetweetsByMeBefore(olderThan.Value, 50);
            }

            return _service.ListRetweetsByMe();
        }

        /// <summary>
        ///     Will throw an exception of statusId is not a valid status.
        /// </summary>
        public Status GetSingleStatus(long statusId)
        {
            Status status;

            if (_statusCache.TryGetValue(statusId, out status))
            {
                return status;
            }

            calls++;
            var rawStatus = _service.GetTweet(statusId);
            ;

            if (rawStatus == null && _service.Error != null)
            {
                throw new Exception(_service.Error.ToString());
            }

            if (rawStatus == null)
            {
                status = null;
            }
            else
            {
                status = new Status(rawStatus);
            }


            _statusCache.Add(statusId, status);

            return status;
        }

        /// <summary>
        ///     All parameters are "untrusted" and can be accepted directly from user.
        /// 
        ///     Throws a StatusUpdateException if an "expected error" occures.  Handle and show the user the FriendlyErrorMessage.
        /// 
        ///     Any other exceptions should be considered critical and left unhandled.
        /// </summary>
        /// <returns>A copy of the new tweet.</returns>
        public TwitterStatus Update(string status, double? lat, double? lon, long? inReplyTo)
        {
            if (String.IsNullOrEmpty(status))
            {
                throw new StatusUpdateException("Status must not be blank.");
            }

            if (status.Length > 140)
            {
                throw new StatusUpdateException("Status cannot be longer than 140 charicters");
            }

            if (inReplyTo != null)
            {
                if (lat != null && lon != null)
                {
                    return _service.SendTweet(status, inReplyTo.Value, new TwitterGeoLocation(lat.Value, lon.Value));
                }
                else
                {
                    return _service.SendTweet(status, inReplyTo.Value);
                }
            }
            else
            {
                if (lat != null && lon != null)
                {
                    return _service.SendTweet(status, new TwitterGeoLocation(lat.Value, lon.Value));
                }
                else
                {
                    return _service.SendTweet(status);
                }
            }
        }

        public TwitterStatus Favorite(long statusId)
        {
            return _service.FavoriteTweet(statusId);
        }

        public TwitterStatus Unfavorite(long statusId)
        {
            return _service.UnfavoriteTweet(statusId);
        }

        public TwitterStatus ReTweet(long statusId)
        {
            return _service.SendRetweet(statusId);
        }
    }
}