using System;
using Tweetinvi;
using Tweetinvi.Parameters;
using System.Net;
using System.IO;

namespace bottwitter
{
    public class ImgKeyWords
    {
        private string path;
        private string[] keywords;
        private int searchNumber;

        public ImgKeyWords(string path, string[] keywords, int number)
        {
            Path = path;
            Keywords = keywords;
            searchNumber = number;
        }

        private string[] Keywords { get => keywords; set => keywords = value; }
        private string Path { get => path; set => path = value; }
        private int SearchNumber { get => searchNumber; set => searchNumber = value; }

        public void Run()
        {
            Chemin();
            SearchTweetsParameters searchTweets;
            foreach (String mots in Keywords)
            {
                Console.WriteLine("Chargement");
                searchTweets = searchTweetsParameters(mots);
                DownloadImg(searchTweets);
            }
        }

        private SearchTweetsParameters searchTweetsParameters(String motcle)
        {
            var searchParameter = new SearchTweetsParameters(motcle)
            {
                MaximumNumberOfResults = SearchNumber,
                Filters = TweetSearchFilters.Images
            };

            return searchParameter;
        }

        private void Chemin()
        {
            String chemin = System.IO.Path.Combine(Path, "recherche");
            Tools.CreateDirectory(chemin);
        }

        private void DownloadImg(SearchTweetsParameters searchtweetsParameters)
        {
            var tweets = Search.SearchTweets(searchtweetsParameters);
            string pathimg;

            foreach (var tweet in tweets)
            {
                foreach (var media in tweet.Media)
                {
                    pathimg = System.IO.Path.Combine(Path, "recherche", media.Id + ".jpg");
                    using (WebClient client = new WebClient())
                    {

                        try
                        {
                            if (File.Exists(pathimg))
                            {
                                Console.WriteLine("That image exists already.");
                                continue;
                            }
                            client.DownloadFile(new Uri(media.MediaURL), pathimg);
                            Console.WriteLine(media.Id + " téléchargé!");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("The process failed: {0}", e.ToString());
                        }
      
                    }
                }
            }
        }
    }
}
