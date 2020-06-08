namespace specification_and_notification
{
    public interface ISpecification<T>
    {
         bool IsSatisfiedBy(T entity);
    }
}