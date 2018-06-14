﻿using DotVVM.Framework.Compilation.ControlTree.Resolved;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace DotvvmAcademy.Validation.Dothtml
{
    public class NodeExistenceVisitor : IControlVisitor
    {
        public const string MetadataKey = "NodeExistence";

        public static readonly ValidationDiagnosticDescriptor NodeDoesntExistError
                    = new ValidationDiagnosticDescriptor("TEMP", "Node does not exist", "Node with identifier '{0}' does no exist.", ValidationDiagnosticSeverity.Error);

        public static readonly ValidationDiagnosticDescriptor NodeExistsError
            = new ValidationDiagnosticDescriptor("TEMP", "Node must not exist", "Node with identifier '{0}' must not exist.",
                ValidationDiagnosticSeverity.Error);

        public List<ValidationDiagnostic> Diagnostics { get; set; }

        public IMetadataCollection<DothtmlIdentifier> Metadata { get; set; }

        public ImmutableArray<ValidationDiagnostic> GetDiagnostics() => Diagnostics.ToImmutableArray();

        public void Visit(DothtmlIdentifier identifier, ResolvedControl control)
        {
            var nodeExistence = Metadata.GetRequiredProperty<bool>(identifier, MetadataKey);
            if (nodeExistence && control == null)
            {
                Diagnostics.Add(ValidationDiagnostic.Create(NodeDoesntExistError, null, identifier));
            }
            else if (!nodeExistence && control != null)
            {
                Diagnostics.Add(ValidationDiagnostic.Create(NodeExistsError, null, identifier));
            }
        }
    }
}