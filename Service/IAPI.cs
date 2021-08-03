using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VertMagazineStore.Models;

namespace VertMagazineStore.Service
{
    public interface IAPI
    {
        public Task<Token> GetToken();
        public Task<Categories> GetCurrentCategories(string token);
        public Task<MagazinesCategory> GetMagazinesByCategory(string token, string category);
        public Task<Subscribers> GetCurrentSubscribers(string token);
        public Task<AnswerResponse> PostAnswers(string token, SubscribersAnswer subscribers);
    }
}
