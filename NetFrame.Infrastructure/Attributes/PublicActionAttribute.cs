namespace NetFrame.Infrastructure
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class PublicActionAttribute : Attribute
    {
        public PublicActionAttribute()
        {
        }
    }
}
