using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Scrabble_Remix
{
    public class API
    {
        private string endpoint;
        public bool CheckWord(string word)
        {
            endpoint = $"https://api.dictionaryapi.dev/api/v2/entries/en/{word}";
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(endpoint).Result;
                if (response.IsSuccessStatusCode == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
