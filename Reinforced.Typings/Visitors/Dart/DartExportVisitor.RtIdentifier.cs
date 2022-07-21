using Reinforced.Typings.Ast;
#pragma warning disable 1591
namespace Reinforced.Typings.Visitors.Dart
{
    partial class DartExportVisitor
    {
        public override void Visit(RtIdentifier node)
        {
            if (node == null) return;
            Write(node.IdentifierName);
        }
    }
}
