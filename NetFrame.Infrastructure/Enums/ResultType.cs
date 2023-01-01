using System.ComponentModel.DataAnnotations;

namespace NetFrame.Infrastructure
{
    [Serializable]
    public enum ResultType
    {
        [Display(Name = "Success")]
        Success = 1,

        [Display(Name = "Error")]
        Error = 2,

        [Display(Name = "Warning")]
        Warning = 3
    }
}
