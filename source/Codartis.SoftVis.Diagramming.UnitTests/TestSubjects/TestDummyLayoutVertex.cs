﻿using Codartis.SoftVis.Diagramming.Layout.Incremental;

namespace Codartis.SoftVis.Diagramming.UnitTests.TestSubjects
{
    internal class TestDummyLayoutVertex : DummyLayoutVertex
    {
        public TestDummyLayoutVertex(int id)
            : base(id)
        {
        }

        public override string Name => $"*{Id}";
        public override string ToString() => Name;
    }
}
