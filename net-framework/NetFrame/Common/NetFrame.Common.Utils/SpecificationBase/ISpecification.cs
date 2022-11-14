namespace NetFrame.Common.Utils.SpecificationBase
{
    /// <summary>
    ///  Samples: https://www.codeproject.com/Articles/670115/Specification-pattern-in-Csharp
    ///  For Details: http://enterprisecraftsmanship.com/2016/02/08/specification-pattern-c-implementation/
    ///  Turkish resource: http://www.buraksenyurt.com/post/specification-tasarim-kalibina-gitmeye-calisirken-ben
    ///  Not only does this approach removes domain knowledge duplication, 
    ///  it also allows for combining multiple specifications. 
    ///  That, in turn, helps us easily set up quite complex search and validation criteria.
    ///  There are 3 main use cases for the Specification pattern:
    ///  1- Looking up data in the database. That is finding records that match the specification we have in hand.
    ///  2- Validating objects in the memory. In other words, checking that an object we retrieved or created fits the spec.
    ///  3- Creating a new instance that matches the criteria. This is useful in scenarios where you don’t care about 
    ///     the actual content of the instances, but still need it to have certain attributes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISpecification<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns></returns>
        bool IsSatisfiedBy(T candidate);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        ISpecification<T> And(ISpecification<T> other);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        ISpecification<T> Or(ISpecification<T> other);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ISpecification<T> Not();
    }
}
