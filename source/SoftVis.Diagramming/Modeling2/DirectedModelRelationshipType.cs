﻿using System;

namespace Codartis.SoftVis.Modeling2
{
    /// <summary>
    /// A relationship type with a direction.
    /// </summary>
    public struct DirectedModelRelationshipType : IEquatable<DirectedModelRelationshipType>
    {
        public ModelRelationshipStereotype Stereotype { get; }
        public RelationshipDirection Direction { get; }

        public DirectedModelRelationshipType(ModelRelationshipStereotype stereotype, RelationshipDirection direction)
        {
            Stereotype = stereotype ?? throw new ArgumentNullException(nameof(stereotype));
            Direction = direction;
        }

        public bool Equals(DirectedModelRelationshipType other)
        {
            return Stereotype.Equals(other.Stereotype) && Direction == other.Direction;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is DirectedModelRelationshipType && Equals((DirectedModelRelationshipType) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Stereotype.GetHashCode() * 397) ^ (int) Direction;
            }
        }

        public static bool operator ==(DirectedModelRelationshipType left, DirectedModelRelationshipType right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DirectedModelRelationshipType left, DirectedModelRelationshipType right)
        {
            return !left.Equals(right);
        }
    }
}
