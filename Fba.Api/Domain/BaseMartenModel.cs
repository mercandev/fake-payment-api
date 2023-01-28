using System;
using Marten.Schema;

namespace Fba.Api.Domain
{
    public class BaseMartenModel
    {
        [Identity]
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
    }
}

