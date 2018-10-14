﻿using System;
using System.Collections.Generic;
using Codartis.SoftVis.Diagramming;
using Codartis.SoftVis.Diagramming.Implementation;
using Codartis.SoftVis.Modeling;
using Codartis.SoftVis.TestHostApp.Modeling;

namespace Codartis.SoftVis.TestHostApp.Diagramming
{
    internal class TestDiagramService : DiagramService
    {
        private static readonly Dictionary<ModelRelationshipStereotype, ConnectorType> ModelRelationshipTypeToConnectorTypeMap =
            new Dictionary<ModelRelationshipStereotype, ConnectorType>
            {
                {ModelRelationshipStereotypes.Inheritance, TestConnectorTypes.Generalization},
                {ModelRelationshipStereotypes.Implementation, TestConnectorTypes.Implementation},
                {ModelRelationshipStereotypes.Association, TestConnectorTypes.Association},
            };

        public TestDiagramService(TestDiagram testDiagram, IModelService modelService, IDiagramShapeFactory diagramShapeFactory) 
            : base(testDiagram, modelService, diagramShapeFactory)
        {
        }

        public override ConnectorType GetConnectorType(ModelRelationshipStereotype stereotype)
        {
            if (!ModelRelationshipTypeToConnectorTypeMap.ContainsKey(stereotype))
                throw new Exception($"Unexpected model relationship type {stereotype}");

            return ModelRelationshipTypeToConnectorTypeMap[stereotype];
        }
    }
}
