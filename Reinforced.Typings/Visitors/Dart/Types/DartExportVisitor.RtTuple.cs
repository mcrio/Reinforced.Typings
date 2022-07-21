using Reinforced.Typings.Ast.TypeNames;
#pragma warning disable 1591
namespace Reinforced.Typings.Visitors.Dart
{
    partial class DartExportVisitor
    {
        #region Types
        public override void Visit(RtTuple node)
        {
            if (node==null) return;
            Write("[");
            SequentialVisit(node.TupleTypes,",");
            Write("]");
            if (node.IsNullable)
            {
                Write("?");
            }
        }


        #endregion


    }
}