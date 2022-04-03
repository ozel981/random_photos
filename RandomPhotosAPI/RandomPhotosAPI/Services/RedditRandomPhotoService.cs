using Newtonsoft.Json;
using RandomPhotosAPI.ModelsDTO;
using RandomPhotosAPI.Services.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RandomPhotosAPI.Services
{
    public class RedditRandomPhotoService : IRandomPhotoService
    {
        Random random;
        HttpClient client;
        HttpContent content;
        string clientId = "I3-8TcfpMo0Gts30MV3juw";
        string secretKey = "jE6GBN74IYpDsKyOBQ3bZMRz8sKPxw";
        string subreddit = "meme";
        string redditAccessTokenUriStr = "https://www.reddit.com/api/v1/access_token";
        public RedditRandomPhotoService()
        {
            random = new Random();
            client = new HttpClient();
            Uri baseUri = new Uri("https://www.reddit.com");
            client.BaseAddress = baseUri;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.ConnectionClose = true;
            var values = new List<KeyValuePair<string, string>>();
            values.Add(new KeyValuePair<string, string>("grant_type", "password"));
            values.Add(new KeyValuePair<string, string>("username", "Longjumping_Song7753"));
            values.Add(new KeyValuePair<string, string>("password", "Qwerty13579"));
            content = new FormUrlEncodedContent(values);
        }
        public async virtual Task<PhotoDTO> GetRandomPhoto()
        {
            string url = await GetRandomPhotoFromReddit();
            DateTime date = DateTime.Now;
            return new PhotoDTO
            {
                Url = url,
                DownloadDate = date
            };
        }
        private string GetRedditAccessToken()
        {
            var authenticationString = $"{clientId}:{secretKey}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, redditAccessTokenUriStr);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            requestMessage.Content = content;

            var task = client.SendAsync(requestMessage);
            var response = task.Result;
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;
            var obiekt = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseBody);
            return obiekt["access_token"];
        }
        private async Task<string> GetRandomPhotoFromReddit()
        {
            string accessToken = GetRedditAccessToken();
            var option = new RestClientOptions($"https://oauth.reddit.com/r/{subreddit}/hot")
            {
                Timeout = -1
            };
            var client = new RestClient(option);
            var request = new RestRequest();
            request.AddHeader("Authorization", $"Bearer {accessToken}");
            var response = await client.ExecuteGetAsync(request);
            string output = response.Content;
            var obiekt = JsonConvert.DeserializeObject<Subreddit>(output);
            List<string> photoUrls = new List<string>();
            foreach (var children in obiekt.Data.Children)
            {
                if (children.Data.Url.Contains(".png") || children.Data.Url.Contains(".jpg"))
                {
                    photoUrls.Add(children.Data.Url);
                }
            }
            return photoUrls[random.Next(0, photoUrls.Count - 1)];
        }
    }

    [Serializable]
    class Subreddit
    {
        public string Kind { get; set; }
        public SubredditData Data { get; set; }
    }

    [Serializable]
    class SubredditData
    {
        public string After { get; set; }
        public int Kind { get; set; }
        public Object Modhash { get; set; }
        public Object Qeo_filter { get; set; }
        public List<SubredditDataChildren> Children { get; set; }
    }

    [Serializable]
    class SubredditDataChildren
    {
        public string Kind { get; set; }
        public SubredditDataChildrenData Data { get; set; }
    }

    [Serializable]
    class SubredditDataChildrenData
    {
        public string Url { get; set; }
    }
}
