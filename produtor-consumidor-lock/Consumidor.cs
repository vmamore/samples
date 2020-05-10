using System;
using System.Threading;

namespace produtor_consumidor
{
    public class Consumidor
    {
        private readonly Dados _dados;
        private Random _random = new Random();
        private readonly int _espera;
        
        public Consumidor(Dados dados, int espera)
        {
            this._dados = dados;
            this._espera = espera;
        }

        public void Consumir()
        {
            while (true)
            {
                while (_dados.EstaVazio)
                {
                    Console.WriteLine("Consumidor aguardando (dados vazios).");
                    lock (Program.@lock)
                    {
                        Monitor.Wait(Program.@lock);
                    }
                    Console.WriteLine("Consumidor acordou.");
                }
                var tempoGeradoRandomicamente = _random.Next(0, _espera) * 5;

                Thread.Sleep(_espera + tempoGeradoRandomicamente);

                _dados.Ler();

                lock (Program.@lock)
                {
                    Monitor.PulseAll(Program.@lock);
                }
            }
        }
    }
}