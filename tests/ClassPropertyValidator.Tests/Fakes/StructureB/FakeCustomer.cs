﻿using System;
using System.Collections.Generic;

namespace ClassPropertyValidator.Tests.Fakes.StructureB
{
    public class FakeCustomer
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public FakeEnum FakeEnum { get; set; }

        public IEnumerable<int> Identifiers { get; set; }
    }
}