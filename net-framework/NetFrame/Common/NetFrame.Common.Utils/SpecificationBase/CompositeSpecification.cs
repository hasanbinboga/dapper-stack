namespace NetFrame.Common.Utils.SpecificationBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CompositeSpecification<T> : Specification<T>
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly ISpecification<T> Left;
        /// <summary>
        /// 
        /// </summary>
        protected readonly ISpecification<T> Right;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        protected CompositeSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            Left = left;
            Right = right;
        }
    }
}
