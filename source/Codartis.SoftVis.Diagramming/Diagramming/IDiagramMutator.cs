using System;
using Codartis.SoftVis.Geometry;
using Codartis.SoftVis.Modeling;

namespace Codartis.SoftVis.Diagramming
{
    /// <summary>
    /// Keeps track of the latest diagram instance through mutated instances and publishes change events.
    /// </summary>
    public interface IDiagramMutator
    {
        IDiagram Diagram { get; }

        event Action<DiagramEventBase> DiagramChanged;

        void AddNode(IDiagramNode node, IContainerDiagramNode parentNode = null);
        void RemoveNode(ModelNodeId nodeId);
        void UpdateDiagramNodeModelNode(IDiagramNode diagramNode, IModelNode newModelNode);
        void UpdateDiagramNodeSize(IDiagramNode diagramNode, Size2D newSize);
        void UpdateDiagramNodeCenter(IDiagramNode diagramNode, Point2D newCenter);
        void AddConnector(IDiagramConnector connector);
        void RemoveConnector(ModelRelationshipId connectorId);
        void UpdateDiagramConnectorRoute(IDiagramConnector connector, Route newRoute);
        void ClearDiagram();
    }
}