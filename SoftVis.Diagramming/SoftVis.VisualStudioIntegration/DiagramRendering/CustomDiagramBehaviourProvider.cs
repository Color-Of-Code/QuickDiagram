﻿using System.Collections.Generic;
using Codartis.SoftVis.Geometry;
using Codartis.SoftVis.Rendering.Extensibility;
using Codartis.SoftVis.VisualStudioIntegration.Diagramming;
using Codartis.SoftVis.VisualStudioIntegration.Modeling;

namespace Codartis.SoftVis.VisualStudioIntegration.DiagramRendering
{
    /// <summary>
    /// Extends the built-in diagram behaviour for the roslyn-based model elements.
    /// </summary>
    internal class CustomDiagramBehaviourProvider : DefaultDiagramBehaviourProvider
    {
        private static readonly RelatedEntityMiniButtonDescriptor ImplementedInterfacesDescriptor =
            new RelatedEntityMiniButtonDescriptor(
                CustomRelationshipSpecifications.ImplementedInterfaces, CustomConnectorTypes.Implementation,
                new RectRelativeLocation(RectReferencePoint.TopCenter, new Point2D(MiniButtonRadius * 1.2, MiniButtonOverlapParentBy)));

        private static readonly RelatedEntityMiniButtonDescriptor ImplementerTypesDescriptor =
            new RelatedEntityMiniButtonDescriptor(
                CustomRelationshipSpecifications.ImplementerTypes, CustomConnectorTypes.Implementation,
                new RectRelativeLocation(RectReferencePoint.BottomCenter, new Point2D(MiniButtonRadius * 1.2, -MiniButtonOverlapParentBy)));

        public override IEnumerable<RelatedEntityMiniButtonDescriptor> GetRelatedEntityMiniButtonDescriptors()
        {
            yield return BaseTypesDescriptor.WithRelativeLocationTranslate(new Point2D(-MiniButtonRadius * 1.2, MiniButtonOverlapParentBy));
            yield return SubtypesDescriptor.WithRelativeLocationTranslate(new Point2D(-MiniButtonRadius * 1.2, -MiniButtonOverlapParentBy));
            yield return ImplementedInterfacesDescriptor;
            yield return ImplementerTypesDescriptor;
        }
    }
}
