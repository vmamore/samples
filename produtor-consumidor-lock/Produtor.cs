using System;
using System.Threading;

namespace produtor_consumidor
{
    public class Produtor
    {
        private Dados _dados;
        private Random _random = new Random();
        private int _espera;

        public Produtor(Dados dados, int espera)
        {
            this._dados = dados;
            this._espera = espera;
        }

        public void Produzir()
        {
            while (true)
            {
                while (_dados.EstaCheio)
                {
                    Console.WriteLine("Produtor está aguardando a lista esvaziar.");
                    lock (Program.@lock)
                    {
                        Monitor.Wait(Program.@lock);
                    }
                    Console.WriteLine("Produtor acordou");
                }
                var tempoGeradoRandomicamente = _random.Next(0, _espera) * 5;

                Thread.Sleep(_espera + tempoGeradoRandomicamente);

                _dados.Escrever(_random.Next(0, 10000));

                lock (Program.@lock)
                {
                    Monitor.PulseAll(Program.@lock);
                }
            }
        }
    }
}