﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotvvmAcademy.Meta.Syntax
{
    public class ConstructedTypeNameNode : NameNode, IEquatable<ConstructedTypeNameNode>
    {
        public ConstructedTypeNameNode(
            NameNode unboundTypeName,
            ImmutableArray<NameNode> arguments,
            ImmutableArray<NameToken> commaTokens,
            NameToken openBracketToken,
            NameToken closeBracketToken)
        {
            UnboundTypeName = unboundTypeName;
            Arguments = arguments;
            CommaTokens = commaTokens;
            OpenBracketToken = openBracketToken;
            CloseBracketToken = closeBracketToken;
        }

        public ImmutableArray<NameNode> Arguments { get; }

        public NameToken CloseBracketToken { get; }

        public ImmutableArray<NameToken> CommaTokens { get; }

        public NameToken OpenBracketToken { get; }

        public NameNode UnboundTypeName { get; }

        public static bool operator !=(ConstructedTypeNameNode node1, ConstructedTypeNameNode node2)
        {
            return !(node1 == node2);
        }

        public static bool operator ==(ConstructedTypeNameNode node1, ConstructedTypeNameNode node2)
        {
            return EqualityComparer<ConstructedTypeNameNode>.Default.Equals(node1, node2);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ConstructedTypeNameNode);
        }

        public bool Equals(ConstructedTypeNameNode other)
        {
            return other != null
                && EqualityComparer<NameNode>.Default.Equals(UnboundTypeName, other.UnboundTypeName)
                && Arguments.Equals(other.Arguments)
                && CommaTokens.Equals(other.CommaTokens)
                && OpenBracketToken.Equals(other.OpenBracketToken)
                && CloseBracketToken.Equals(other.CloseBracketToken);
        }

        public override int GetHashCode()
        {
            var hashCode = -1125981776;
            hashCode = hashCode * -1521134295 + EqualityComparer<NameNode>.Default.GetHashCode(UnboundTypeName);
            hashCode = hashCode * -1521134295 + EqualityComparer<ImmutableArray<NameNode>>.Default.GetHashCode(Arguments);
            hashCode = hashCode * -1521134295 + EqualityComparer<ImmutableArray<NameToken>>.Default.GetHashCode(CommaTokens);
            hashCode = hashCode * -1521134295 + EqualityComparer<NameToken>.Default.GetHashCode(OpenBracketToken);
            hashCode = hashCode * -1521134295 + EqualityComparer<NameToken>.Default.GetHashCode(CloseBracketToken);
            return hashCode;
        }

        public override string ToString()
        {
            return $"{UnboundTypeName}[{string.Join(", ", Arguments)}]";
        }

        internal override TResult Accept<TResult>(NameNodeVisitor<TResult> visitor)
        {
            return visitor.VisitConstructedType(this);
        }
    }
}