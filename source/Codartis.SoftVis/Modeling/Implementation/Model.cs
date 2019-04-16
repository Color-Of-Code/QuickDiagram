﻿using System;
using System.Collections.Generic;

namespace Codartis.SoftVis.Modeling.Implementation
{
    /// <summary>
    /// Implements an immutable model.
    /// </summary>
    public class Model : IModel
    {
        protected readonly ModelGraph Graph;

        public Model()
            : this(new ModelGraph())
        {
        }

        protected Model(ModelGraph graph)
        {
            Graph = graph;
        }

        public IEnumerable<IModelNode> Nodes => Graph.Vertices;
        public IEnumerable<IModelRelationship> Relationships => Graph.Edges;

        // TODO: implement node hierarchy
        public IEnumerable<IModelNode> RootNodes => Nodes;

        public IModelNode GetNode(ModelNodeId nodeId) => Graph.GetVertex(nodeId);
        public bool TryGetNode(ModelNodeId nodeId, out IModelNode node) => Graph.TryGetVertex(nodeId, out node);

        public IModelRelationship GetRelationship(ModelRelationshipId relationshipId) => Graph.GetEdge(relationshipId);
        public bool TryGetRelationship(ModelRelationshipId relationshipId, out IModelRelationship relationship) 
            => Graph.TryGetEdge(relationshipId, out relationship);

        // TODO: implement node hierarchy
        public IEnumerable<IModelNode> GetChildNodes(ModelNodeId nodeId) => throw new NotImplementedException();

        public IEnumerable<IModelNode> GetRelatedNodes(ModelNodeId nodeId,
            DirectedModelRelationshipType directedModelRelationshipType, bool recursive = false)
        {
            return Graph.GetAdjacentVertices(nodeId, directedModelRelationshipType.Direction,
                i => i.Stereotype == directedModelRelationshipType.Stereotype,
                recursive);
        }

        public IEnumerable<IModelRelationship> GetRelationships(ModelNodeId nodeId) => Graph.GetAllEdges(nodeId);

        public IModel AddNode(IModelNode node) => CreateInstance(Graph.AddVertex(node));
        public IModel RemoveNode(ModelNodeId nodeId) => CreateInstance(Graph.RemoveVertex(nodeId));
        public IModel ReplaceNode(IModelNode newNode) => CreateInstance(Graph.UpdateVertex(newNode));
        public IModel AddRelationship(IModelRelationship relationship) => CreateInstance(Graph.AddEdge(relationship));
        public IModel RemoveRelationship(ModelRelationshipId relationshipId) => CreateInstance(Graph.RemoveEdge(relationshipId));
        public IModel Clear() => CreateInstance(new ModelGraph());

        protected virtual IModel CreateInstance(ModelGraph graph) => new Model(graph);
    }
}
