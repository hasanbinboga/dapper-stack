using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFrame.Common.Utils.Search
{
    /// <summary>
    /// Arama Filtresi sınıfı
    /// </summary>
    public class SearchFilter
    {
        /// <summary>
        /// Property ismi
        /// </summary>
        public string PropertyName { get; set; } = string.Empty;

        /// <summary>
        /// Filtre Operasyonu
        /// </summary>
        public ConditionType Operation { get; set; }

        /// <summary>
        /// değer bilgisi
        /// </summary>
        public object Value { get; set; } = string.Empty;
    }
}
