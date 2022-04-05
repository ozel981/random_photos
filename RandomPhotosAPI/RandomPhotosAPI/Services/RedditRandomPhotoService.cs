using Newtonsoft.Json;
using RandomPhotosAPI.ModelsDTO;
using RandomPhotosAPI.Services.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
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
        RedditConnectionData _connectionData;
        public RedditRandomPhotoService(RedditConnectionData connectionData)
        {
            _connectionData = connectionData;
            InitialService();
        }
        private void InitialService()
        {
            random = new Random();
            client = new HttpClient();
            Uri baseUri = new Uri("https://www.reddit.com");
            client.BaseAddress = baseUri;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.ConnectionClose = true;

            // access token configure options grant_type
            var values = new List<KeyValuePair<string, string>>();
            values.Add(new KeyValuePair<string, string>("grant_type", "password"));
            values.Add(new KeyValuePair<string, string>("username", _connectionData.UserName));
            values.Add(new KeyValuePair<string, string>("password", _connectionData.Password));
            content = new FormUrlEncodedContent(values);
        }
        public async virtual Task<PhotoDTO> GetRandomPhotoAsync()
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
            // access token configure options auth str
            var authenticationString = $"{ _connectionData.ClientID}:{_connectionData.SecretKey}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

            // set headers
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, _connectionData.RedditAccessTokenUriStr);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            requestMessage.Content = content;

            // send request
            var task = client.SendAsync(requestMessage);

            // get access_token from result
            var response = task.Result;
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;
            var obiekt = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseBody);
            return obiekt["access_token"];
        }
        private async Task<string> GetRandomPhotoFromReddit()
        {
            // set request data
            string accessToken = GetRedditAccessToken();
            var option = new RestClientOptions($"https://oauth.reddit.com/r/{_connectionData.Subreddit}/hot");
            var client = new RestClient(option);
            var request = new RestRequest();
            request.AddHeader("Authorization", $"Bearer {accessToken}");

            // send request
            var response = await client.ExecuteGetAsync(request);

            // get result
            string output = response.Content;
            var obiekt = JsonConvert.DeserializeObject<Subreddit>(output);

            // filter photos
            List<string> photoUrls = new List<string>();
            foreach (var children in obiekt.Data.Children)
            {
                if (children.Data.Url.Contains(".png") || children.Data.Url.Contains(".jpg"))
                {
                    photoUrls.Add(children.Data.Url);
                }
            }
            if (photoUrls.Count == 0)
            {
                throw new Exception($"No photos in subreddit ${_connectionData.Subreddit}");
            }
            return photoUrls[random.Next(0, photoUrls.Count - 1)];
        }
    }

    public class RedditConnectionData
    {
        public string ClientID { get; set; } 
        public string SecretKey { get; set; }
        public string Subreddit { get; set; }
        public string RedditAccessTokenUriStr { get; set; } 
        public string UserName { get; set; } 
        public string Password { get; set; } 
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
