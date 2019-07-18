using System;
using System.IO;
using Tweetinvi;

namespace bottwitter
{
    public static class Tools
    {
		public static void CreateDirectory(String path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    Console.WriteLine("That path exists already.");
                    return;
                }
                DirectoryInfo di = Directory.CreateDirectory(path);
                Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
		}

		public static void Authentification(string consumerKey, string consumerSecret, string accessToken, string accesTokenSecret)
        {
            Auth.SetUserCredentials(consumerKey, consumerSecret, accessToken, accesTokenSecret);
        }
    }
}
