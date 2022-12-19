namespace NetFrame.Infrastructure
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class NotXssKontrolAttribute : Attribute
    {
        public NotXssKontrolAttribute()
        {
        }
    }
}
