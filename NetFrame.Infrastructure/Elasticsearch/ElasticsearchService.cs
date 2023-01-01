using Nest;

namespace NetFrame.Infrastructure
{

    /// <summary>
    /// Defines the <see cref="ElasticService" />.
    /// </summary>
    public class ElasticsearchService : IElasticsearchService, IDisposable
    {
        

        private const string elasticHost = "http://localhost:9200/";
        private const string elasticUserName = "elk";
        private const string elasticPass = "123";


        /// <summary>
        /// Defines the ElasticConnection.
        /// </summary>
        private static readonly Lazy<ElasticClient> ElasticConnection = new Lazy<ElasticClient>(() =>
        {
            ConnectionSettings settings = new ConnectionSettings(
                new Uri(elasticHost)).
                BasicAuthentication(elasticUserName, elasticPass);

            return new ElasticClient(settings);
        });
        /// <summary>
        /// Defines the _elasticClient.
        /// </summary>
        public static ElasticClient _elasticClient = null!;

        /// <summary>
        /// Initializes a new instance of the <see cref="ElasticService"/> class.
        /// </summary>
        public ElasticsearchService()
        {
            _elasticClient = ElasticConnection.Value;

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this); // disposa çağrıldığında bu objenin yok edildiğini garbage collectora bilditiyoruz. 
        }

        /// <summary>
        /// The Index.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="document">The document<see cref="T"/>.</param>
        /// <param name="indexName">The indexName<see cref="string"/>.</param>
        public void Index<T>(T document, string indexName) where T : class
        {
            _elasticClient.IndexAsync<T>(document, i => i
                                            .Index(indexName)
                                            .Refresh(Elasticsearch.Net.Refresh.True));
        }
    }
}
