using System;
using System.Linq;

namespace simple_query_provider
{
    class Program
    {
        static void Main(string[] args)
        {
            var provider = new QueryProvider();
            var customer = new Query<Customer>(provider);

            IQueryable<Customer> query = customer.Where(c => c.Id == 1);

            System.Console.WriteLine($"query: {query}");
            Console.ReadKey();
        }
    }
}
