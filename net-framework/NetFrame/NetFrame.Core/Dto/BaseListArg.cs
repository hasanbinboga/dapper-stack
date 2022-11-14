namespace NetFrame.Core.Dtos
{
    public class BaseListArg<T>
    {
        public T Filter { get; set; }
        public OrderArg[] Order { get; set; }
        public PageArgDto PageArg { get; set; }
    }
}
