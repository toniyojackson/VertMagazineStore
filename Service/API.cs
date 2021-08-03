using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using VertMagazineStore.Models;

namespace VertMagazineStore.Service
{
    public class API : IAPI
    {
        private readonly HttpClient httpClient;
        private readonly IMemoryCache memoryCache;

        public API(HttpClient httpClient, IMemoryCache memoryCache)
        {
            this.httpClient = httpClient;
        }

        public async Task<Categories> GetCurrentCategories(string token)
        {
            try
            {
                var response = await httpClient.GetAsync(httpClient.BaseAddress + "categories/" + token);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    var categories = JsonConvert.DeserializeObject<Categories>(responseString);

                    return categories;
                }

                throw new Exception($"Error occured while retriving current categories: {response.ReasonPhrase}");

            }
            catch (Exception ex)
            {
                throw new Exception($"Error occured while retriving current categories: {ex.Message}");
            }
        }

        public async Task<Subscribers> GetCurrentSubscribers(string token)
        {
            try
            {
                var response = await httpClient.GetAsync(httpClient.BaseAddress + "subscribers/" + token);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    var subscribers = JsonConvert.DeserializeObject<Subscribers>(responseString);

                    return subscribers;
                }

                throw new Exception($"Error occured while retriving current subscribers: {response.ReasonPhrase}");

            }
            catch (Exception ex)
            {
                throw new Exception($"Error occured while retriving current subscribers: {ex.Message}");
            }
        }

        public async Task<MagazinesCategory> GetMagazinesByCategory(string token, string category)
        {
            try
            {
                var response = await httpClient.GetAsync(httpClient.BaseAddress + "magazines/" + token + "/" + category);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    var magazinesCategory = JsonConvert.DeserializeObject<MagazinesCategory>(responseString);

                    return magazinesCategory;
                }

                throw new Exception($"Error occured while retriving current subscribers: {response.ReasonPhrase}");

            }
            catch (Exception ex)
            {
                throw new Exception($"Error occured while retriving current subscribers: {ex.Message}");
            }
        }

        public async Task<Token> GetToken()
        {
            try
            {
                var response = await httpClient.GetAsync(httpClient.BaseAddress + "token");

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    var token = JsonConvert.DeserializeObject<Token>(responseString);

                    return token;
                }

                throw new Exception($"Error occured while getting token: {response.ReasonPhrase}");

            }
            catch (Exception ex)
            {
                throw new Exception($"Error occured while getting token: {ex.Message}");
            }

        }

        public async Task<AnswerResponse> PostAnswers(string token, SubscribersAnswer subscribers)
        {
            try
            {
                JsonContent content = JsonContent.Create(subscribers);

                var response = await httpClient.PostAsync(httpClient.BaseAddress + "answer/" + token, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    var answerResponse = JsonConvert.DeserializeObject<AnswerResponse>(responseString);

                    return answerResponse;
                }

                throw new Exception($"Error occured while posting to answers: {response.ReasonPhrase}");

            }
            catch (Exception ex)
            {
                throw new Exception($"Error occured while posting to answers: {ex.Message}");
            }
        }

    }
}
