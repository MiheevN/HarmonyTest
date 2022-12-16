using HarmonyWebTest.Entitys;
using LiteDB;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace HarmonyWebTest.Controllers
{
    //Пока без подтверждений, но понятно что надо возвращать bool = успешно
    [ApiController]
    [Route("/[action]")]
    public class HomeController : Controller
    {

        [HttpGet()]
        public string GetItem(string name)
        {
            Console.WriteLine($"Requested Get: {name}");
            if (string.IsNullOrWhiteSpace(name))
            {
                return string.Empty;
            }
            using var DB = new LiteDatabase("TestDB");
            var entitys = DB.GetCollection<ValueEntity>("entitys");
            var find = entitys.FindOne(E => E.Name == name);
            if (find != null)
            {
                return find.Value;
            }
            return string.Empty;
        }

        /// <summary>
        /// Добавление/обновление записи.
        /// Если запись с таким именем уже существует, она обновится
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        [HttpPost()]
        public void AddItem([FromForm] string name, [FromForm] string value)
        {

            using var DB = new LiteDatabase("TestDB");
            var entitys = DB.GetCollection<ValueEntity>("entitys");
            var find = entitys.FindOne(E => E.Name == name);

            if (find == null)
            {
                Console.WriteLine($"Requested Add: {name}");
                entitys.Insert(new ValueEntity()
                {
                    Name = name,
                    Value = value
                });
            }
            else
            {
                Console.WriteLine($"Requested Update: {name}");
                find.Value = value;
                entitys.Update(find);
            }
        }

        [HttpPost()]
        public void UpdateItem(string name, string value)
        {
            AddItem(name, value);//В добавлении всё равно обновляется 
        }

        [HttpDelete()]
        public void DeleteItem(string name)
        {
            Console.WriteLine($"Requested Delete: {name}");

            using var DB = new LiteDatabase("TestDB");
            var entitys = DB.GetCollection<ValueEntity>("entitys");
            var find = entitys.FindOne(E => E.Name == name);
            if (find != null)
            {
                entitys.Delete(find.id);
            }
        }
    }
}
