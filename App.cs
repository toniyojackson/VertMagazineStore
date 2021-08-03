using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VertMagazineStore.Models;
using VertMagazineStore.Service;

namespace VertMagazineStore
{
    public class App
    {
        private readonly IConfiguration configuration;
        private readonly IAPI iAPI;
        private readonly IMemoryCache memoryCache;

        public App(IConfiguration configuration, IAPI iAPI, IMemoryCache memoryCache)
        {
            this.configuration = configuration;
            this.iAPI = iAPI;
            this.memoryCache = memoryCache;
        }

        public async Task Execute()
        {
            try
            {
                var token = await iAPI.GetToken();

                var categories = await iAPI.GetCurrentCategories(token.token);

                //using (var entry = memoryCache.CreateEntry("categories"))
                //{
                //    entry.Value = categories;
                //    entry.AbsoluteExpiration = DateTime.UtcNow.AddSeconds(30);
                //}

                List<Magazine> magazines = new List<Magazine>();
                SubscribersAnswer answers = new SubscribersAnswer();

                //memoryCache.TryGetValue("categories", out categories);

                foreach (var category in categories.data)
                {
                    var magazinesCategory = await iAPI.GetMagazinesByCategory(token.token, category);

                    magazines.AddRange(magazinesCategory.data);
                }

                //using (var entry = memoryCache.CreateEntry("magazines"))
                //{
                //    entry.Value = magazines;
                //    entry.AbsoluteExpiration = DateTime.UtcNow.AddSeconds(30);
                //}

                var subscribers = await iAPI.GetCurrentSubscribers(token.token);

                //using (var entry = memoryCache.CreateEntry("subscribers"))
                //{
                //    entry.Value = subscribers;
                //    entry.AbsoluteExpiration = DateTime.UtcNow.AddSeconds(30);
                //}

                //memoryCache.TryGetValue("subscribers", out subscribers);
                //memoryCache.TryGetValue("magazines", out magazines);

                foreach (Subscriber sub in subscribers.data)
                {
                    var category = magazines.Where(a => sub.magazineIds.Contains(a.id)).GroupBy(x=>x.category, (key,g) => g.OrderBy(e=>e.id).First());
                    
                    if(category != null)
                    {
                        if(category.Count() == categories.data.Count)
                        {
                            answers.subscribers.Add(sub.id);
                        }
                    }
                }

                //using (var entry = memoryCache.CreateEntry("answers"))
                //{
                //    entry.Value = answers;
                //    entry.AbsoluteExpiration = DateTime.UtcNow.AddSeconds(30);
                //}

                if (answers.subscribers.Count > 0)
                {
                    //memoryCache.TryGetValue("answers", out answers);
                    var answerResponse = await iAPI.PostAnswers(token.token, answers);

                    Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(answerResponse, Formatting.Indented));
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
