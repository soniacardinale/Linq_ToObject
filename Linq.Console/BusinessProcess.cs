using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Linq.ConsoleApp
{
    //1.Dichiarare il delegate
    public delegate void ProcessStarted();
    public delegate void ProcessCompleted(int duration);

    public class BusinessProcess
    {
        //2.Dichiarare l'evento
        public event ProcessStarted Started;
        public event ProcessCompleted Completed;
        public event EventHandler StartedCore; //qui utilizzo un evento gia presente, non devo dichiare il delegate sopra
        public event EventHandler<ProcessandEventArgs> CompletedCore;
        public void ProcessData()
        {
            Console.WriteLine("Starting process");
            Thread.Sleep(2000); //il codice si addormena per 2 secondi (nelle parentesi ho i millesecondi)
            Console.WriteLine("Process Started");
            //sollevo l'evento Started:
            if (Started != null)
                Started();
            if (StartedCore != null)
                StartedCore(this, EventArgs.Empty);
            Thread.Sleep(3000);
            Console.WriteLine("Process Completed");
            //3.Solleva l'evento
            if (Completed != null)
                Completed(5000);
            if (CompletedCore != null)
                CompletedCore(this, new ProcessandEventArgs { Duration = 2000 });

        }
    }

    public class ProcessandEventArgs : EventArgs
    {
        public int Duration { get; set; }
    }
}
