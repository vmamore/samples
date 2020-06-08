namespace specification_and_notification
{
    public static class ExtensionMethods
    {
        public static ISpecification<T> And<T>(this ISpecification<T> spec1, ISpecification<T> spec2){
            return new AndSpecification<T>(spec1, spec2);
        }

        public static ISpecification<T> Or<T>(this ISpecification<T> spec1, ISpecification<T> spec2){
            return new OrSpecification<T>(spec1, spec2);
        }

        public static ISpecification<T> Not<T>(this ISpecification<T> spec){
            return new NotSpecification<T>(spec);
        }
    }

    
}