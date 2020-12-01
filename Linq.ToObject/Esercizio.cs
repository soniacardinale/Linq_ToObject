using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Linq.ToObject
{
    public class Esercizio
    {
        //Creazione liste
        public static List<Product> CreateProductList()
        {
            var lista = new List<Product> //qui i prodotti li abbiamo già creati nella lista
            {
                new Product {ID=1, Name="Telefono", UnitPrice = 300.99},
                new Product {ID=2, Name="Computer", UnitPrice = 800},
                new Product {ID=3, Name= "Tablet", UnitPrice= 550.99}
            };
            return lista;
        }

        public static List<Order> CreateOrderList()
        {
            var lista = new List<Order>(); //inizilaizzo una lista vuota

            //qui creiamo prima i prodotti e poi li aggiungiamo nella lista
            var order = new Order { ID = 1, ProductID = 1, Quantity = 4 };
            lista.Add(order);
            var order1 = new Order { ID = 2, ProductID = 2, Quantity = 1 };
            lista.Add(order1);
            var order2 = new Order { ID = 3, ProductID = 1, Quantity = 1 };
            lista.Add(order2);

            return lista;
        }

        //Esecuzione immediata e ritardata
        public static void DeferredExecution()
        {
            var productList = CreateProductList();
            var orderList = CreateOrderList();

            //Vediamo i risultati
            foreach (var item in productList)
            {
                Console.WriteLine("{0}-{1}-{2}", item.ID, item.Name, item.UnitPrice);
            }

            foreach (var item in orderList)
            {
                Console.WriteLine("{0}-{1}-{2}", item.ID, item.ProductID, item.Quantity);
            }

            //Esecuzione Differita
            //Creazione Query
            var list = productList
                .Where(product => product.UnitPrice >= 400) //Lambda Expression
                .Select(p => new { Nome = p.Name, Prezzo = p.UnitPrice }); //sto crendo un Anonymous Type

            //Aggiungo Prodotto
            productList.Add(new Product { ID = 4, Name = "Bici", UnitPrice = 500.99 });

            //Risultati
            Console.WriteLine("Esecuzione Differita: ");
            foreach (var item in list)
            {
                Console.WriteLine("{0}-{1}", item.Nome, item.Prezzo);
            }

            //Esecuzione Immediata
            var list1 = productList
                .Where(product => product.UnitPrice >= 400) //Lambda Expression
                .Select(p => new { Nome = p.Name, Prezzo = p.UnitPrice }) //sto crendo un Anonymous Type
                .ToList(); //forzo l'esecuzione

            //Non vedrò il prodotto che andrò ad aggiungere dopo perchè la query è già stata eseguita
            productList.Add(new Product { ID = 5, Name = "Divano", UnitPrice = 450.99 });

            //Risultati
            Console.WriteLine("Esecuzione Immediata: ");
            foreach (var item in list1)
            {
                Console.WriteLine("{0}-{1}", item.Nome, item.Prezzo);
            }

        }

        //Sintassi
        public static void Syntax()
        {
            var productList = CreateProductList();
            var orderList = CreateOrderList();

            //Method Syntax
            var methodList = productList
                .Where(p => p.UnitPrice <= 600)
                .Select(p => new { Nome = p.Name, Prezzo = p.UnitPrice })
                .ToList();

            //Query
            var queryList =
                from p in productList
                where p.UnitPrice <= 600
                select new { Nome = p.Name, Prezzo = p.UnitPrice };
            queryList.ToList();

            //Method Syntax e Query fanno esattamente la stessa cosa
        }

        //Operatori
        public static void Operators()
        {
            var productList = CreateProductList();
            var orderList = CreateOrderList();

            //Scrittura a schermo delle liste
            Console.WriteLine("Lista Prodotti: ");
            foreach (var item in productList)
            {
                Console.WriteLine("{0}-{1}-{2}", item.ID, item.Name, item.UnitPrice);
            }

            Console.WriteLine("Lista Ordini: ");
            foreach (var item in orderList)
            {
                Console.WriteLine("{0}-{1}-{2}", item.ID, item.ProductID, item.ProductID);
            }

            //Filtro OfType
            var list = new ArrayList();
            list.Add(productList);
            list.Add("Ciao");
            list.Add(123);

            var typeQuery =
                from item in list.OfType<int>() //sto selezionando solo gli elementi di tipo int
                select item;

            //var typeQuery1 =
            //   from item in list.OfType<List<Product>>() //sto selezionando solo gli elementi di tipo List<Product>
            //    select item;

            Console.WriteLine("OfType => Gli elementi di tipo int nella lista sono: ");
            foreach (var item in typeQuery)
            {
                Console.WriteLine(item);
            }

            //Element
            //Console.WriteLine("Elementi: ");
            //string[] empty = { };
            //var el1 = empty.First();  //questo mi da errore perchè la mia stringa è vuota
            //Console.WriteLine(el1);

            Console.WriteLine("Elementi: ");
            string[] empty = { };
            var el1 = empty.FirstOrDefault();  //questo non mi da errore perchè da' il valore di default
            Console.WriteLine(el1);

            var p1 = productList.ElementAt(0).Name; //seleziona l'elemento nella posizione 0 (sarebbe la prima)
            Console.WriteLine(p1);

            //Ordinamento

            productList.Add(new Product { ID = 4, Name = "Telefono", UnitPrice = 200.99 });

            Console.WriteLine("Ordinamento1: ");

            var orderedList =
            from item in productList
            orderby item.Name ascending, item.UnitPrice descending
            select new { Nome = item.Name, Prezzo = item.UnitPrice };

            foreach (var item in orderedList)
            {
                Console.WriteLine("{0}-{1}", item.Nome, item.Prezzo);
            }

            //equivale a

            var orderedList2 = productList
                .OrderBy(p => p.Name)
                .ThenByDescending(p => p.UnitPrice)
                .Select(p => new { Nome = p.Name, Prezzo = p.UnitPrice })
                .Reverse(); //solo che qui l'ho rigirata

            Console.WriteLine("Ordinamento2: ");

            foreach (var item in orderedList2)
            {
                Console.WriteLine("{0}-{1}", item.Nome, item.Prezzo);
            }

            //Quantificatori: restituiscono booleani
            var hasProductWithT = productList.Any(p => p.Name.StartsWith("T")); //se ce n'è almeno 1 mi da true
            var allProductsWithT = productList.All(p => p.Name.StartsWith("T")); //devono iniziare tutti con T
            Console.WriteLine("Quantificatori:\r\nAlmeno un prodotto inizia con T: {0}\r\nTutti i prodotti iniziano con T: {1}", hasProductWithT, allProductsWithT);

            //GroupBy
            Console.WriteLine();
            Console.WriteLine("GroupBy-QuerySintax: ");

            //QuerySyntax
            //raggruppiamo order per ProductID
            var groupByList =
                from o in orderList
                group o by o.ProductID into groupList
                select groupList;

            foreach (var item in groupByList)
            {
                Console.WriteLine(item.Key); //raggruppo per ProductID

                foreach (var i in item)
                {
                    Console.WriteLine($"\t {i.ProductID}-{i.Quantity}"); //per ogni gruppo, mi scrivo quello che voglio
                }
            }

            //MethodSyntax
            var groupByList2 =
                orderList
                .GroupBy(o => o.ProductID);
            Console.WriteLine("GroupBy-MethodSyntax: ");
            foreach (var item in groupByList2)
            {
                Console.WriteLine(item.Key);
                foreach (var o in item)
                {
                    Console.WriteLine("\t {0}-{1}", o.ProductID, o.Quantity);
                }
            }

            //GroupBy con funzione di aggregazione
            //Raggruppare gli ordini per Prodotto e ricavare la somma delle quantità
            //MethodSyntax
            var sumyByProduct = orderList
                .GroupBy(p => p.ProductID)
                .Select(lista => new { ID = lista.Key, Quantities = lista.Sum(p => p.Quantity) });

            Console.WriteLine("GroupBy con funzione di aggregazione-MethodSyntax: ");
            foreach (var item in sumyByProduct)
            {
                Console.WriteLine("{0}-{1}", item.ID, item.Quantities);
            }

            //QuerySyntax
            var sumByProduct2 =
                from o in orderList
                group o by o.ProductID into list3
                select new { ID = list3.Key, Quantities = list3.Sum(x => x.Quantity) };

            Console.WriteLine("GroupBy con funzione di aggregazione-QuerySyntax: ");
            foreach (var item in sumByProduct2)
            {
                Console.WriteLine("{0}-{1}", item.ID, item.Quantities);
            }

            //Join (in Linq intendiamo INNER JOIN)
            //Recuperiamo i Prodotti che hanno Ordini
            //Nome-ID dell'ordine-Quantità (Nome dalla Lista di Prodotti, ID dell'ordine e Quantità da Lista di ordini)
            //MethodSyntax
            Console.WriteLine("Join-MethodSyntax: ");
            var joinList = productList //primma lista.Join
                .Join(orderList, p => p.ID, o => o.ProductID, (p, o) => new { Nome = p.Name, OrderID = o.ID, Quantità = o.Quantity }); //Join(seconda lista, elemento di join nella prima lista, elemento di join della seconda lista

            foreach (var item in joinList)
            {
                Console.WriteLine("{0}-{1}-{2}", item.Nome, item.OrderID, item.Quantità);
            }

            //QuerySyntax
            Console.WriteLine("Join-QuerySyntax: ");
            var joinList2 =
                from p in productList
                join o in orderList
                on p.ID equals o.ProductID
                select new { Nome = p.Name, OrderID = p.ID, Quantità = o.Quantity };

            foreach (var item in joinList2)
            {
                Console.WriteLine("{0}-{1}-{2}", item.Nome, item.OrderID, item.Quantità);
            }

            //GroupJoin -> mette insieme le join con un groupby
            //Recuperare gli ordini per prodotto e fare una somma sulle quantità
            //Vogliamo nome Prodotto e Quantità totale
            orderList.Add(new Order { ID = 4, ProductID = 4, Quantity = 3 });
            Console.WriteLine("GroupJoin-MethodSyntax: ");
            var groupJoinList = productList
                .GroupJoin(orderList, p => p.ID, o => o.ProductID,  //qui faccio la join
                (p, o) => new { Nome = p.Name, Quantità = o.Sum(o => o.Quantity) }); //qui faccio il GroupBy con la somma

            foreach (var item in groupJoinList)
            {
                Console.WriteLine("{0}-n°{1}", item.Nome, item.Quantità);
            }

            //QuerySyntax:
            Console.WriteLine("GroupJoin-QuerySyntax: ");
            var groupList2 =
                from p in productList
                join o in orderList
                on p.ID equals o.ProductID
                into gr //gr vede solo la seconda tabella
                select new { Prodotto = p.Name, Quantità = gr.Sum(o => o.Quantity) };

            foreach (var item in groupList2)
            {
                Console.WriteLine("{0}-{1}", item.Prodotto, item.Quantità);

            }


            //InnerJoin effettiva
            var lista4 =
            from o in orderList
            group o by o.ProductID
            into gr
            select new { ProdottoId = gr.Key, Quantità = gr.Sum(o => o.Quantity) }
            into gr1
            join p in productList
            on gr1.ProdottoId equals p.ID
            select new { p.Name, gr1.Quantità };
            //qui stampo effettivamente solamente i prodotti che hanno un ordine perchè sono partita dalla lista ordini (in cui sono presenti solamente i ProductID di ordine effettivamente ordinati)

            foreach (var item in lista4)
            {
                Console.WriteLine("{0}-{1}", item.Name, item.Quantità);
            }

        }
    }
}
