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
    public delegate void EventDelegate();
    class Program
    {
        public static string path = @"file.xml"; // путь к файлу
        public static bool flag = false; // для бесконечного цикла
        static CompanyMail myevent = new CompanyMail(); // для событий
        static XmlSerializer formatter = new XmlSerializer(typeof(List<CompanyMail>));
        static void Main(string[] args)
        {
            //List<CompanyMail> adr = new List<CompanyMail>()
            //{
            //    new CompanyMail("ул. Академика 12-1", 112231, "Минск", "Беларусь","ИП Гефест"),
            //    new CompanyMail("ул. Гнездовское 15", 123123, "Минск", "Беларусь","ОАО Пенек")
            //};
            List<CompanyMail> adr;
            try
            {
                //using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                //{
                //    formatter.Serialize(fs,adr);
                //}
                
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    adr = (List<CompanyMail>)formatter.Deserialize(fs);
                }
                CompanyMail.View(adr);
                while (!flag)
                {
                    Console.WriteLine("Добавить - 1\n" +
                   "Удалить - 2\n" +
                   "Раздельное изменение - 3\n" +
                   "Выход - 0\n");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1: // добавить + вызов события
                            {
                                myevent.MyEvent += EventHandlerAdd;
                                myevent.InvokeEvent();

                                 CompanyMail.Add(adr, path);
                                break;
                            }
                        case 2: // удалить
                            {
                                 CompanyMail.LastDelete(adr, path);
                                break;
                            }
                        case 3: // редактировать
                            {
                                 CompanyMail.LastEdit(adr, path);
                                break;
                            }
                        case 0: // выход (вызов события)
                            {
                                flag = true;
                                myevent.MyEvent += EventHandlerExit;
                                myevent.InvokeEvent();
                                break;
                            }
                    }
                    CompanyMail.View(adr);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Возникла исключения!!!\n" + e.Message);
                Console.ReadLine();
            }
        }
        public static void EventHandlerAdd()
        {
            Console.WriteLine("Обработчик события!!!\n" +
                "Был вызван метод \"Add\" из класса CompanyMail\n");
            Console.Beep(250, 1000);
        }
        public static void EventHandlerExit()
        {
            Console.WriteLine("Обработчик события!!!\n" +
                "Вы вышли(\n");
            Console.Beep(200, 2000);
        }
    }
}