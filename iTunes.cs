using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace iTunes
{
    public class iTunesApi
    {
        private static string GetReques(string url, WebProxy webproxy=null)
        {
            string html = string.Empty;
            using (WebClient wc = new WebClient() { Proxy = webproxy })
            {
                html = wc.DownloadString(url);
            }
            return html;
        }
        private static async Task<string> GetRequesAsync(string url, WebProxy webproxy = null)
        {
            return await Task.Factory.StartNew(()=>
            {
               return GetReques(url, webproxy);
            });
        }

        public static SearchResponse Search(Dictionary<string, string> parameters, WebProxy webproxy = null)
        {
            var url = string.Format("https://itunes.apple.com/search?{0}",
                        string.Join("&",
                            parameters.Select(kvp =>
                                string.Format("{0}={1}", kvp.Key, kvp.Value))));
            return JsonConvert.DeserializeObject<SearchResponse>(GetReques(url, webproxy)); 
        }
        public static async Task<SearchResponse> SearchAsync(Dictionary<string, string> parameters, WebProxy webproxy = null)
        {
            return await Task.Factory.StartNew(()=>
            {
                return Search(parameters, webproxy);
            });
        }
    }

    public class SearchResult
    {
        public string wrapperType { get; set; }
        public string explicitness { get; set; }
        public string kind { get; set; }
        public int artistId { get; set; }
        public int collectionId { get; set; }
        public int trackId { get; set; }
        public string artistName { get; set; }
        public string collectionName { get; set; }
        public string trackName { get; set; }
        public string collectionCensoredName { get; set; }
        public string trackCensoredName { get; set; }
        public string artistViewUrl { get; set; }
        public string collectionViewUrl { get; set; }
        public string trackViewUrl { get; set; }
        public string previewUrl { get; set; }
        public string artworkUrl30 { get; set; }
        public string artworkUrl60 { get; set; }
        public string artworkUrl100 { get; set; }
        public double collectionPrice { get; set; }
        public double trackPrice { get; set; }
        public string releaseDate { get; set; }
        public string collectionExplicitness { get; set; }
        public string trackExplicitness { get; set; }
        public int discCount { get; set; }
        public int discNumber { get; set; }
        public int trackCount { get; set; }
        public int trackNumber { get; set; }
        public int trackTimeMillis { get; set; }
        public string country { get; set; }
        public string currency { get; set; }
        public string primaryGenreName { get; set; }
        public string radioStationUrl { get; set; }
        public bool isStreamable { get; set; }
        public string collectionArtistName { get; set; }
        public int? collectionArtistId { get; set; }
    }

    public class SearchResponse
    {
        public int resultCount { get; set; }
        public List<SearchResult> results { get; set; }
    }

}