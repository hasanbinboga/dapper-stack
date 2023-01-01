namespace NetFrame.Infrastructure
{
    public interface IElasticsearchService
    {
        /// <summary>
        /// The Index.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="document">The document<see cref="T"/>.</param>
        /// <param name="indexName">The indexName<see cref="string"/>.</param>
        void Index<T>(T document, string indexName) where T : class;
    }
}
