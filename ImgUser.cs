using System;
using Tweetinvi;
using Tweetinvi.Models;
using System.Net;
using System.IO;

namespace bottwitter
{
	public class ImgUser
	{
		private string path;
		private string[] users;

		public ImgUser(string path, string[] users)
		{
			Path = path;
			Users = users;
		}

		private string Path { get => path; set => path = value; }
		private string[] Users { get => users; set => users = value; }


		private void DownloadImages(IUser user, String chemin)
        {
			Console.WriteLine(user.StatusesCount);
			var tls = user.GetUserTimeline(user.StatusesCount);
			string pathimg;
			foreach (var tl in tls)
			{
				foreach (var img in tl.Media)
				    {
					pathimg = System.IO.Path.Combine(chemin, user.ScreenName + "_" + tl.Id + ".jpg");
					using (WebClient client = new WebClient())
					{

						try{
							if (File.Exists(pathimg))
                            {
								//Console.WriteLine("That image exists already.");
								continue;
                            }
							client.DownloadFile(new Uri(img.MediaURL), pathimg);
							Console.WriteLine( user.ScreenName + " " + img.Id + " téléchargé!");
						}
						catch (Exception e)
                        {
                            Console.WriteLine("The process failed: {0}", e.ToString());
                        }

					}
				}
			}
        }

		private String PathUser(IUser user)
		{
			string chemin = System.IO.Path.Combine(Path, user.ScreenName);
			return chemin;
		}

		public void Run()
        {
			string chemin;
			foreach (var userz in Users)
            {
                var user = User.GetUserFromScreenName(userz);
				chemin = PathUser(user);
				Tools.CreateDirectory(chemin);
				DownloadImages(user, chemin);
            }
        }
	
	}
}
