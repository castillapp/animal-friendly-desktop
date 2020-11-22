using System;
using System.Collections.Generic;
using System.Text;

namespace MockPersistencia.Data
{
    public static class MockDatabase
    {
        public static Treballadors Treballadors { get; set; } = new Treballadors();
    }
}
