using System;
using System.Collections.Generic;

namespace produtor_consumidor
{
    public class Dados
    {
        private List<int> lista = new List<int>(); 
        private int _tamanhoMaximo;  

        public bool EstaCheio { get; private set; } = false;
        public bool EstaVazio { get; private set; } = true;
        public Dados(int tamanhoMaximo)
        {
            this._tamanhoMaximo = tamanhoMaximo;
        }

        public void Escrever(int numero)
        {
            lock (Program.@lock)
            {
                if (lista.Count >= ObterTamanhoMaximo()) return;

                lista.Add(numero); 
                Console.WriteLine($"Produziu => [{numero}]");

                if (lista.Count >= ObterTamanhoAtual()) EstaCheio = true;
                else EstaCheio = false;

                if (EstaVazio) EstaVazio = false;
            }
        }
        public void Ler()
        {
            lock (Program.@lock)
            {
                if (lista.Count <= 0) return;

                int numero = lista[0];

                lista.RemoveAt(0);

                Console.WriteLine($"Consumiu => [{numero}]");

                if (lista.Count <= 0) EstaVazio = true;
                else EstaVazio = false;

                if (EstaCheio) EstaCheio = false;
            }
        }

        private int ObterTamanhoMaximo()
        {
            return _tamanhoMaximo;
        }
        private int ObterTamanhoAtual()
        {
            return lista.Count;
        }
    }
}