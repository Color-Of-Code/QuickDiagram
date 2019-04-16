using Codartis.SoftVis.Graphs;
using Codartis.SoftVis.Graphs.Layered;

namespace Codartis.SoftVis.Diagramming.Layout.Incremental.Relative
{
    /// <summary>
    /// Provides a read-only view of a layered graph used for layout calculation.
    /// </summary>
    internal interface IReadOnlyLayeredLayoutGraph : IReadOnlyLayeredGraph<DiagramNodeLayoutVertex, LayoutPath>
    {
    }
}