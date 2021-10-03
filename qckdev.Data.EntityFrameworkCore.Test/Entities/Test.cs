using System;
using System.Collections.Generic;
using System.Text;

namespace qckdev.Data.EntityFrameworkCore.Test.Entities
{
    sealed class Test
    {

        public Guid TestId { get; set; }
        public string Name { get; set; }
        public int Factor { get; set; }
        public string Spaced { get; set; }
        public string SpacedRaw { get; set; }
    }
}
