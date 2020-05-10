using System;
using System.Threading;

namespace produtor_consumidor
{
    class Program
    {
        public static Object @lock = new Object();
        static void Main(string[] args)
        {
            int tamanhoMaximo = 100;  
            int tempoDeEsperaProdutor = 200;  
            int tempoDeEsperaConsumidor = 200;  

            Dados dados = new Dados(tamanhoMaximo);
            Produtor produtor = new Produtor(dados, tempoDeEsperaProdutor);
            Consumidor consumidor = new Consumidor(dados, tempoDeEsperaConsumidor);

            Thread threadProdutora = new Thread(new ThreadStart(produtor.Produzir));
            Thread threadConsumidora = new Thread(new ThreadStart(consumidor.Consumir));

            threadProdutora.IsBackground = true;
            threadConsumidora.IsBackground = true;

            threadProdutora.Start();
            threadConsumidora.Start();

            Console.ReadKey();
        }
    }
}
