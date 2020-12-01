using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Linq.ToObject
{
    public class Esercizitazione
    {
        public static void CityByName()
        {
            var lst_City = new List<Citta>
            {
                new Citta {Nome = "Milano" },
                new Citta {Nome= "Palermo"},
                new Citta {Nome = "Matera"}

            };

            //Query
            IEnumerable<Citta> lst_CityByName = lst_City
                .Where(c => c.Nome.StartsWith("M")); //nella parentesi ho la Lambda Expression

            foreach (var item in lst_CityByName)
            {
                Console.WriteLine(item.Nome);
            }

        }

        public static void PersonByName()
        {
            var lst_Person = new List<Person>
            {
                new Person {FirstName= "Sara", LastName= "Ferrante"},
                new Person {FirstName = "Anna", LastName = "Mancini"},
                new Person { FirstName = "Fabrizio", LastName="Rossi"},
                new Person {FirstName= "Anna", LastName="Bianchi"}
            };

            //Query Syntax
            //var lst_ByName =
            //  from p in lst_Person
            //  where (p.FirstName == "Anna") || (p.FirstName == "Fabrizio")
            //  select p;

            //MethodSyntax
            var lst_ByName = lst_Person
                .Where(p => p.FirstName == "Anna" || p.FirstName == "Fabrizio");


            //Faccio una esecuzione differita

            Console.WriteLine("Persone di nome Anna e persone di nome Fabrizio: ");
            foreach (var item in lst_ByName)
            {
                Console.WriteLine("{0} {1}", item.FirstName, item.LastName);
            }



        }
    }
}
