namespace NetFrame.Core.Dtos
{
    public class BaseListArg<T>
    {
        public T? Filter { get; set; }
        public OrderArg[] Order { get; set; } = new OrderArg[0];
        public PageArgDto PageArg { get; set; } = new PageArgDto();
    }
}
