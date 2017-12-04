namespace SFBBot_UCWA.Utils
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DataStore")]
    public partial class DataStore
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string Intent { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string EntityType { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string DisplayMessage { get; set; }

        [Key]
        [Column(Order = 3)]
        public string Url { get; set; }
    }
}
