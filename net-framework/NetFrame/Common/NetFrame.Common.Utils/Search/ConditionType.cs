using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFrame.Common.Utils.Search
{
    /// <summary>
    /// Kullanılabilecek koşul çeşitleri
    /// </summary>
    public enum ConditionType
    {
        [Description("Bilinmiyor")]
        Bilinmiyor=0,
        /// <summary>
        /// eşittir
        /// </summary>
        [Description("eşittir")]
        Equals=1,
        /// <summary>
        /// -den büyüktür
        /// </summary>
        [Description("-den büyüktür")]
        GreaterThan = 2,
        /// <summary>
        /// -den küçüktür
        /// </summary>
        [Description("-den küçüktür")]
        LessThan= 3,
        /// <summary>
        /// -den büyük veya eşittir
        /// </summary>
        [Description("-den büyük veya eşittir")]
        GreaterThanOrEqual = 4,
        /// <summary>
        /// -den küçük veya eşittir
        /// </summary>
        [Description("-den küçük veya eşittir")]
        LessThanOrEqual = 5,
        /// <summary>
        /// içerir
        /// </summary>
        [Description("içerir")]
        Contains=6,
        /// <summary>
        /// ile başlar
        /// </summary>
        [Description("ile başlar")]
        StartsWith = 7,
        /// <summary>
        /// ile biter
        /// </summary>
        [Description("ile biter")]
        EndsWith = 8
    }
}
