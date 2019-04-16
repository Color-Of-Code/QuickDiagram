﻿namespace Codartis.SoftVis.Modeling.Events
{
    public class ModelRelationshipRemovedEvent: ModelEventBase
    {
        public IModelRelationship RemovedRelationship { get; }

        public ModelRelationshipRemovedEvent(IModel newModel, IModelRelationship removedRelationship) 
            : base(newModel)
        {
            RemovedRelationship = removedRelationship;
        }
    }
}