namespace SELDictionary.Model
{
    /// <summary>
    /// Describe a database table
    /// </summary>
    public record Entities
    {
        #region Columns

        public int EntityNumber { get; set; }
        public string EntityName { get; set; } = "";
        public string? EntityDescription { get; set; }
        public string? EntityApps { get; set; }
        public string? EntityIdenAtts { get; set; }
        public string? PrimaryKey { get; set; }
        public int? UpdateCount { get; set; }
        public string? Marker { get; set; }
        public string? Display { get; set; }

        #endregion

        /// <summary>
        /// Database columns
        /// </summary>
        public ICollection<UniqueAtts>? EntityAttrs { get; set; }
    }
}
