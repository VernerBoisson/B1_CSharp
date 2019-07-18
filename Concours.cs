using System;
using Tweetinvi;
using Tweetinvi.Parameters;
using Tweetinvi.Models;
using System.Threading;

namespace bottwitter
{
	public class Concours
	{
		private string[] keywords;
		private Random random = new Random();
		public Concours(string[] keywords)
		{
			Keywords = keywords;
		}
        
		private string[] Keywords { get => keywords; set => keywords = value; }

		private SearchTweetsParameters searchTweetsParameters(String motcle)
		{
			var searchParameter = new SearchTweetsParameters(motcle)
			{
				MaximumNumberOfResults = 100,
				Until = DateTime.Today,
			};
			return searchParameter;
		}

		private void Participation(SearchTweetsParameters searchTweets)
		{
			var tweets = Search.SearchTweets(searchTweets);
			foreach (var tweet in tweets)
			{
				//test(tweet);
				followMentionned(tweet);

				if (tweet.IsRetweet)
				{
					tweetRT(tweet);
				}
				else
				{
					tweetOriginal(tweet);
				}
				attente();
				Console.WriteLine("effectué.");
			}
		}

		private void test(ITweet tweet)
		{
			String parts;
			if (tweet.Prefix != null)
			{
				parts = tweet.Prefix;
				Console.WriteLine(parts);                            ;
			}

			parts = tweet.Text;
			Console.WriteLine(parts);
			if (tweet.Suffix != null)
			{
				parts = tweet.Suffix;
				//StringComparison comparison = StringComparison.OrdinalIgnoreCase;
				Console.WriteLine(parts);
				String str = "friend,Friend,ami,amis,amie,amies,Ami,Amie,Amis,friends,Friends"; 
				foreach(string item in str.Split(','))
					if(tweet.Text.Contains(item))
					        Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!");
			}

		}

		private void followMentionned(ITweet tweet)
		{
			if (tweet.UserMentions != null)
            {
                foreach (var userMentionned in tweet.UserMentions)
                {
                    var usr = User.GetUserFromScreenName((userMentionned.ScreenName));

					if (!(usr.Following))
                    {
                        User.FollowUser(usr);
                    }
                }

            }
		}
        
		private void tweetRT(ITweet tweet)
		{
			var usr = tweet.RetweetedTweet.CreatedBy;
            if (usr.Following)
            {
                tweet.PublishRetweet();
                tweet.Favorite();
            }
            else
            {
				User.FollowUser(usr);
                tweet.PublishRetweet();
				tweet.Favorite();
            }
        }

		private void tweetOriginal(ITweet tweet)
		{
			if (tweet.CreatedBy.Following)
            {
                tweet.PublishRetweet();
                tweet.Favorite();
            }
            else
            {
				User.FollowUser(tweet.CreatedBy);
                tweet.PublishRetweet();
                tweet.Favorite();
            }
		}


		public void Run()
		{
			foreach (var keyword in Keywords)
			{
				Participation(searchTweetsParameters(keyword));
				Thread.Sleep(8000);
			}
		}

        private void attente()
		{
			Thread.Sleep(15000 + random.Next(1000, 8000));
		}
	

    }
}
