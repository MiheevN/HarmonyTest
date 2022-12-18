using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WpfHarmony.Model
{
    public class TestModel
    {
        const string Host = "http://localhost:5092";
        public string Name { get; set; }
        public string Value { get; set; }
        public TestModel()
        {

        }


        public async Task Add()
        {
            var http = new HttpClient();
            var Response = await http.PostAsync($"{Host}/AddItem", new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                { "name", Name },
                { "value", Value }
            }
            ));
            Console.WriteLine(await Response.Content.ReadAsStringAsync());
        }

        public async Task<string> Search(string text)
        {
            var http = new HttpClient();
            return await http.GetStringAsync($"{Host}/GetItem?name={text}");
        }

        public async Task Delete(string name)
        {
            var http = new HttpClient();
            await http.DeleteAsync($"{Host}/DeleteItem?name={name}");
        }
    }
}
