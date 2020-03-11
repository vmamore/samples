using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace simple_query_provider
{
    public class QueryProvider : IQueryProvider
    {
        public IQueryable CreateQuery(Expression expression)
        {
            Type elementType = expression.Type.GetElementType();
            
            try {
                return (IQueryable)Activator.CreateInstance(typeof(Query<>)
                .MakeGenericType(elementType), new object[] {this, expression});
            } catch (TargetInvocationException tie){
                throw tie.InnerException;
            }
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new Query<TElement>(this, expression);
        }

        public object Execute(Expression expression)
        {
            return Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return (TResult) Execute(expression);
        }

        public string GetQueryText(Expression expression) => Translate(expression);

        internal string Translate(Expression expression){
            return new QueryTranslator().Translate(expression);
        }
    }
}