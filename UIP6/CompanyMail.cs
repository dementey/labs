using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace UIP6
{
    [Serializable]
    public class CompanyMail : IMail
    {
        public string Address { get; set; }
        public int Index { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Name { get; set; }
        public event EventDelegate MyEvent = null;
        static XmlSerializer formatter = new XmlSerializer(typeof(List<CompanyMail>));
        public CompanyMail(string a, int i, string ct, string cn, string n)
        {
            Address = a;
            Index = i;
            City = ct;
            Country = cn;
            Name = n;
        }
        public CompanyMail() { }
        public void InvokeEvent()
        {
            if (MyEvent != null)
            {
                MyEvent.Invoke();
                MyEvent -= MyEvent; // убрать обработчик, который мы занесли
            }
        }
        public static void View(List<CompanyMail> obj)
        {
            foreach (var item in obj)
            {
                Console.WriteLine(item);
            }
        }
        public static List<CompanyMail> Add(List<CompanyMail> obj, string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                Console.WriteLine("Введите Адрес, индекс, город, страну, наименование");
                obj.Add(new CompanyMail(Console.ReadLine(),
                    Convert.ToInt32(Console.ReadLine()),
                    Console.ReadLine(),
                    Console.ReadLine(),
                    Console.ReadLine()));
                formatter.Serialize(fs, obj);
            }
            return obj;
        }
        public static List<CompanyMail> LastDelete(List<CompanyMail> obj, string path)
        {
            obj.RemoveAt(obj.Count - 1);

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                Console.WriteLine("Удаление (Последний элемент)");
                formatter.Serialize(fs, obj);
            }
            return obj;
        }
        public static List<CompanyMail> LastEdit(List<CompanyMail> obj, string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                Console.WriteLine("Выберите что хотите изменить\n" +
                "Адрес - 1\n" +
                "Индекс - 2\n" +
                "Город - 3\n" +
                "Страна - 4\n" +
                "Наименование - 5\n");
                int secondchoice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Вводите");
                switch (secondchoice)
                {

                    case 1:
                        {
                            obj[obj.Count - 1].Address = Console.ReadLine();
                            break;
                        }
                    case 2:
                        {
                            obj[obj.Count - 1].Index = Convert.ToInt32(Console.ReadLine());
                            break;
                        }
                    case 3:
                        {
                            obj[obj.Count - 1].City = Console.ReadLine();
                            break;
                        }
                    case 4:
                        {
                            obj[obj.Count - 1].Country = Console.ReadLine();
                            break;
                        }
                    case 5:
                        {
                            obj[obj.Count - 1].Name = Console.ReadLine();
                            break;
                        }
                }
                formatter.Serialize(fs, obj);
            }
            return obj;
        }
        public override string ToString() =>
          $"Почтовый адрес организации\n" +
          $"Адрес организации: {Address}\n" +
          $"Индекс: {Index}\n" +
          $"Город: {City}\n" +
          $"Страна: {Country}\n" +
          $"Наименование организации: {Name}\n";
    }
}