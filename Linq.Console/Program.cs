using System;
using System.Collections.Generic;
using System.IO;

namespace Linq.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("LINQ");
            //string firstname = "Sonia";
            //var lastname = "Cardinale"; //lastname è una stringa
            ////se assegno: 
            ////lastname = 2; UriHostNameType errore, lastname è una stringa

            ////using var file = new StreamWriter("null");

            //List<int> data = new List<int> { 1, 2, 3, 4 };
            //foreach (var item in data)
            //{
            //    Console.WriteLine("#" + item);
            //}

            //var person = new {firstName = "Luigi", lastName = "Rossi"};
            ////Non ho una classe person, se voglio definire una nuova persona, devo scrivere:
            //var person2 = new {firstName = "Marco", lastName = "Bianchi"};

            var process = new BusinessProcess();
            //sottoscrizione all'evento (posso avere più sottoscrizioni)
            process.Started += Process_Started; //mi sottoscrivo per ricevere una notifica quando viene generato l'evento
            process.Started += Process_Started1;
            process.Completed += Process_Completed; //il nome posso anche cambiarlo
            process.StartedCore += Process_StartedCore;
            process.ProcessData();


        }

        private static void Process_StartedCore(object sender, EventArgs e)
        {
            Console.WriteLine("Ricevuto StartedCore");
        }

        private static void Process_Completed(int duration)
        {
            Console.WriteLine($"Process Completed (duration:{duration})");
        }

        private static void Process_Started1()
        {
            Console.WriteLine("Altro Handler");
        }

        private static void Process_Started()
        {
            Console.WriteLine("Ricevuto - Processo Avviato");
        }
    }

    internal class Employee
    {
        //internal = visibile solo nell'assembly corrente.
        public string Name { get; set; }


    }

    internal class Employee<T> //ho definito una classe generica, a T devo passare un tipo (int, string,...)
    //internal è un accessor come public, private, ...
    //internal -> la classe è visibile solamente all'interno di questo assembly (progetto)
    //se aggiungo questo progetto come riferimento di altri progetti, io questa classse comunque non la vedo
    {
        public T ID { get; set; }
        public string FirstName { get; set; }
    }
    #region Generics

    internal class EmployeeInt
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    internal class EmployeeString
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    #endregion



}
