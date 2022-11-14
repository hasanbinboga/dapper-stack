using System.Collections.Generic;
using Newtonsoft.Json;
using PagedList;

namespace NetFrame.Core.Dtos
{
    public class PaginationEntity<TEntity> where TEntity : class
    {
        public PaginationEntity(IPagedList<TEntity> items)
        {
            Items = items;
            MetaData = GetMetadata((PagedListMetaData)items.GetMetaData());
        }

        [JsonProperty(PropertyName = "items")]
        public IEnumerable<TEntity> Items { get; set; }

        [JsonProperty(PropertyName = "metadata")]
        public PaginationMetaData MetaData { get; set; }

        private PaginationMetaData GetMetadata(PagedListMetaData metaData)
        {
            return metaData != null
                ? new PaginationMetaData
                {
                    TotalItemCount = metaData.TotalItemCount,
                    HasNextPage = metaData.HasNextPage,
                    HasPreviousPage = metaData.HasPreviousPage,
                    IsFirstPage = metaData.IsFirstPage,
                    IsLastPage = metaData.IsLastPage,
                    LastItemOnPage = metaData.LastItemOnPage,
                    PageCount = metaData.PageCount,
                    PageNumber = metaData.PageNumber,
                    PageSize = metaData.PageSize
                }
                : null;
        }
    }

    public class PaginationMetaData
    {
        [JsonProperty(PropertyName = "hasNextPage")]
        public bool HasNextPage { get; set; }

        [JsonProperty(PropertyName = "hasPreviousPage")]
        public bool HasPreviousPage { get; set; }

        [JsonProperty(PropertyName = "isFirstPage")]
        public bool IsFirstPage { get; set; }

        [JsonProperty(PropertyName = "isLastPage")]
        public bool IsLastPage { get; set; }

        [JsonProperty(PropertyName = "lastItemOnPage")]
        public int LastItemOnPage { get; set; }

        [JsonProperty(PropertyName = "pageCount")]
        public int PageCount { get; set; }

        [JsonProperty(PropertyName = "pageNumber")]
        public int PageNumber { get; set; }

        [JsonProperty(PropertyName = "pageSize")]
        public int PageSize { get; set; }

        [JsonProperty(PropertyName = "totalItemCount")]
        public int TotalItemCount { get; set; }
    }
}
