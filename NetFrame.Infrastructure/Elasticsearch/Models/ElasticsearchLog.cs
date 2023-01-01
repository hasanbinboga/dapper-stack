namespace NetFrame.Infrastructure
{
    /// <summary>
    /// Defines the <see cref="ElasticsearchLog" />.
    /// </summary>
    public class ElasticsearchLog
    {
        
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ControllerName.
        /// </summary>
        public string ControllerName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the StartTime.
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// Gets or sets the EndTime.
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// Gets or sets the ExecutionMs.
        /// </summary>
        public double? ExecutionMs { get; set; }

        /// <summary>
        /// Gets or sets the LogTime.
        /// </summary>
        public DateTime? LogTime { get; set; }

        /// <summary>
        /// Gets or sets the UserId.
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// Gets or sets the KurumKodu.
        /// </summary>
        public int? KurumKodu { get; set; }

        /// <summary>
        /// Gets or sets the HostName.
        /// </summary>
        public string HostName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the IpAddress.
        /// </summary>
        public string IpAddress { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ServerInfo.
        /// </summary>
        public string ServerInfo { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Parameters.
        /// </summary>
        public string Parameters { get; set; } = string.Empty;

        public string Response { get; set; } = string.Empty;
    }
}
