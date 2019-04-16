﻿using Codartis.SoftVis.Modeling;
using Codartis.SoftVis.Modeling.Implementation;

namespace Codartis.SoftVis.Diagramming.UnitTests.TestSubjects
{
    internal class TestModelNode : ModelNode
    {
        public TestModelNode(string name = "dummy")
            : this(ModelNodeId.Create(), name, ModelOrigin.SourceCode)
        {
        }

        private TestModelNode(ModelNodeId id, string name, ModelOrigin origin)
            : base(id, name, TestModelNodeStereotypes.Class, origin)
        {
        }
    }
}
