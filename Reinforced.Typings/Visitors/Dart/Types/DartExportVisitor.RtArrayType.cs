using Reinforced.Typings.Ast.TypeNames;
#pragma warning disable 1591
namespace Reinforced.Typings.Visitors.Dart
{
    partial class DartExportVisitor
    {
        #region Types

        public override void Visit(RtArrayType node)
        {
            if (node == null) return;
            Write("List<");
            Visit(node.ElementType);
            Write(">");
            
            if (node.IsNullable)
            {
                Write("?");
            }
        }

        #endregion
    }
}