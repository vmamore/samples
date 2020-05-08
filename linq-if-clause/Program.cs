using System;
using System.Linq;
using System.Collections.Generic;

namespace linq_if_clause
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new[]
            {
                new { user = "captain", data = "launch codes" },
                new { user = "doctor", data = "medicine" },
                new { user = "lawyer", data = "law" }
            };

            var administratorFlags = new[] { true, false };
            var user = "captain";

            foreach (var regularUser in administratorFlags)
            {
                var results =
                    data.If(regularUser,
                        q => q.Where(x => x.user == user)
                    )
                    .Select(x => x.data)
                    .ToList();

                Console.WriteLine(new string('.', 50));
                Console.WriteLine($"I am {(regularUser ? "regular" : "SUPER")} {user}");
                Console.WriteLine($"My data includes {string.Join(", ", results)}.");
            }
            Console.WriteLine(new string('.', 50));
        }
    }

    public static class EnumerableHelpers
    {
        public static IQueryable<T> If<T>(
            this IQueryable<T> query,
            bool should,
            params Func<IQueryable<T>, IQueryable<T>>[] transforms)
        {
            return should ? transforms.Aggregate(query,
                (current, transform) => transform.Invoke(current))
                : query;
        }

        public static IEnumerable<T> If<T>(
            this IEnumerable<T> query,
            bool should,
            params Func<IEnumerable<T>, IEnumerable<T>>[] transforms)
        {
            return should ?
                transforms.Aggregate(query,
                (current, transform) => transform.Invoke(current))
                : query;
        }

    }
}
