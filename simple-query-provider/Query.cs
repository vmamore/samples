using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace simple_query_provider
{
    public class Query<T> : IQueryable<T>
    {
        private QueryProvider _provider;
        private Expression _expression;

        public Query(QueryProvider provider)
        {
            this._provider = provider;
            this._expression = Expression.Constant(this);
        }

        public Query(QueryProvider provider, Expression expression)
        {
            this._provider = provider;
            this._expression = expression;
        }



        public Type ElementType => typeof(T);

        public Expression Expression => _expression;

        public System.Linq.IQueryProvider Provider => _provider;

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_provider.Execute(_expression)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_provider.Execute(_expression)).GetEnumerator();
        }

        public override string ToString() => _provider.GetQueryText(_expression);
    }
}