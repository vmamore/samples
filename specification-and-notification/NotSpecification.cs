namespace specification_and_notification
{
    public class NotSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T> _spec;

        public NotSpecification(ISpecification<T> spec){
            _spec = spec;
        }
        public bool IsSatisfiedBy(T entity)
        {
            return !_spec.IsSatisfiedBy(entity);
        }
    }
}