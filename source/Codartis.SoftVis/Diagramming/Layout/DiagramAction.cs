﻿namespace Codartis.SoftVis.Diagramming.Layout
{
    /// <summary>
    /// Abstract base class for all diagram actions.
    /// </summary>
    internal abstract class DiagramAction
    {
        public abstract void Accept(IDiagramActionVisitor visitor);
    }
}
