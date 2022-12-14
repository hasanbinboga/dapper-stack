using System.ComponentModel.DataAnnotations.Schema;

namespace NetFrame.Core.Entities
{
    /// <summary>
    /// Log Class
    /// </summary>
    [Table("infolog")]
    public class InfoLogEntity:LogEntity
    {
        
    }

    public class InfoLogEntityValidator : LogEntityValidator<InfoLogEntity>
    {
        public InfoLogEntityValidator()
        {
             
        }
    }
}
