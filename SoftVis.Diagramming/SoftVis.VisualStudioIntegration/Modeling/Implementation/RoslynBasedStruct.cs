﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Codartis.SoftVis.VisualStudioIntegration.Modeling.Implementation
{
    /// <summary>
    /// A model entity created from a Roslyn struct symbol.
    /// </summary>
    internal class RoslynBasedStruct : RoslynBasedModelEntity
    {
        internal RoslynBasedStruct(INamedTypeSymbol namedTypeSymbol)
            : base(namedTypeSymbol, TypeKind.Struct)
        {
        }

        public override int Priority => 3;

        public IEnumerable<RoslynBasedInterface> ImplementedInterfaces
        {
            get
            {
                return OutgoingRelationships.Where(i => i.IsInterfaceImplementation())
                    .Select(i => i.Target).OfType<RoslynBasedInterface>();
            }
        }
    }
}