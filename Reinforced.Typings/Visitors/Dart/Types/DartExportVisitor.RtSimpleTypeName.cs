using Reinforced.Typings.Ast.TypeNames;
#pragma warning disable 1591
namespace Reinforced.Typings.Visitors.Dart
{
    partial class DartExportVisitor
    {
        #region Types

        public override void Visit(RtSimpleTypeName node)
        {
            if (node.HasPrefix)
            {
                Write(node.Prefix);
                Write(".");
            }
            Write(node.TypeName);
            if (node.GenericArguments.Length > 0)
            {
                Write("<");
                SequentialVisit(node.GenericArguments, ", ");
                Write(">");
            }
            if (node.IsNullable)
            {
                Write("?");
            }
        }

        #endregion
    }
}