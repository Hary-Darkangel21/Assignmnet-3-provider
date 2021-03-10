using System;
using System.Collections.Generic;

#nullable disable

namespace immunization.Models
{
    public partial class Provider
    {
        public Guid Id { get; set; }
        public DateTime CreationTime { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long LicenseNumber { get; set; }
        public string Address { get; set; }
    }
}
