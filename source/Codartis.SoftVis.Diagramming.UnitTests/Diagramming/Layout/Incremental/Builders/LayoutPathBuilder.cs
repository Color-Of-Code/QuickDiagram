﻿using System.Linq;
using Codartis.SoftVis.Diagramming.Layout.Incremental;
using Codartis.SoftVis.Diagramming.UnitTests.Diagramming.Layout.Incremental.Helpers;

namespace Codartis.SoftVis.Diagramming.UnitTests.Diagramming.Layout.Incremental.Builders
{
    internal sealed class LayoutPathBuilder : BuilderBase
    {
        public LayoutPath CreateLayoutPath(string pathString)
        {
            var edgeSpecifications = PathSpecification.Parse(pathString).ToEdgeSpecifications();
            return new LayoutPath(edgeSpecifications.Select(CreateLayoutEdge));
        }

        public GeneralLayoutEdge CreateLayoutEdge(string edgeString)
        {
            var edgeSpecification = EdgeSpecification.Parse(edgeString);
            return CreateLayoutEdge(edgeSpecification);
        }
    }
}
