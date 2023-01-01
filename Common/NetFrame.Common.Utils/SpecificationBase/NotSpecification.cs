namespace NetFrame.Common.Utils.SpecificationBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NotSpecification<T> : Specification<T>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ISpecification<T> _specification;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="specification"></param>
        public NotSpecification(ISpecification<T> specification)
        {
            _specification = specification;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns></returns>
        public override bool IsSatisfiedBy(T candidate)
        {
            return !_specification.IsSatisfiedBy(candidate);
        }
    }
}
