using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace simple_query_provider
{
    public class QueryTranslator
    {
        StringBuilder sb;

        internal string Translate(Expression expression){
            sb = new StringBuilder();
            Visit(expression);
            return sb.ToString();
        }

        protected Expression Visit(Expression expression)
        {
            switch(expression.NodeType) {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.Or:
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                    return VisitBinary((BinaryExpression)expression);
                case ExpressionType.Call:
                    return VisitMethodCall((MethodCallExpression)expression);
                case ExpressionType.Constant:
                    return VisitConstant((ConstantExpression)expression);
                case ExpressionType.MemberAccess:
                    return VisitMemberAccess((MemberExpression)expression);
                default:
                    throw new Exception(string.Format("Unhandled expression type: '{0}'", expression.NodeType));
            }
        }

        private static Expression StripQuotes(Expression e){
            while(e.NodeType == ExpressionType.Quote){
                e = ((UnaryExpression)e).Operand;
            }
            return e;
        }

        private Expression VisitMemberAccess(MemberExpression m)
        {
            if(m.Expression != null && m.Expression.NodeType == ExpressionType.Parameter){
                sb.Append(m.Member.Name);
                return m;
            }
            throw new NotSupportedException($"The member {m.Member.Name} is not supported");
        }

        private Expression VisitConstant(ConstantExpression c)
        {
            var q = c.Value as IQueryable;
            if(q != null){
                sb.Append("SELECT * FROM ");
                sb.Append(q.ElementType.Name);
            } else if (c.Value == null){
                sb.Append("NULL");
            } else {
                switch ( Type.GetTypeCode(c.Value.GetType())){
                    case TypeCode.Boolean:
                        sb.Append(((bool)c.Value ? 1 : 0));
                        break;
                    case TypeCode.String:
                        sb.Append("'").Append(c.Value).Append("'");
                        break;
                    case TypeCode.Object:
                        throw new NotSupportedException($"The const for {c.Value} it not supported yet");
                    default:
                        sb.Append(c.Value);
                        break;
                }
            }

            return c;
        }

        private Expression VisitMethodCall(MethodCallExpression m)
        {
            if(m.Method.DeclaringType == typeof(Queryable) && m.Method.Name == "Where"){
                sb.Append("SELECT * FROM (");

                Visit(m.Arguments[0]);

                sb.Append(") AS T WHERE");

                LambdaExpression lambda = (LambdaExpression)StripQuotes(m.Arguments[1]);

                Visit(lambda.Body);

                return m;
            }

            throw new NotSupportedException(string.Format($"The method {m.Method.Name} is not supported"));
        }

        private Expression VisitBinary(BinaryExpression b)
        {
            sb.Append("(");
            Visit(b.Left);

            switch(b.NodeType){
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    sb.Append(" AND ");
                    break;
                case ExpressionType.Or:
                    sb.Append(" OR ");
                    break;
                case ExpressionType.Equal:
                    sb.Append(" = ");
                    break;
                case ExpressionType.NotEqual:
                    sb.Append(" <> ");
                    break;
                case ExpressionType.LessThan:
                    sb.Append(" < ");
                    break;
                case ExpressionType.LessThanOrEqual:
                    sb.Append(" <= ");
                    break;
                case ExpressionType.GreaterThan:
                    sb.Append(" > ");
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    sb.Append(" >= ");
                    break;
                default:
                    throw new NotSupportedException($"The binary operator {b.NodeType} is not supported.");
            }

            Visit(b.Right);

            sb.Append(")");
            return b;
        }
    }
}