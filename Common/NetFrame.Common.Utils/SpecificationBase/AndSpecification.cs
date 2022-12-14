namespace NetFrame.Common.Utils.SpecificationBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AndSpecification<T> : CompositeSpecification<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
            : base(left, right)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns></returns>
        public override bool IsSatisfiedBy(T candidate)
        {
            return Left.IsSatisfiedBy(candidate) && Right.IsSatisfiedBy(candidate);
        }
    }
}
