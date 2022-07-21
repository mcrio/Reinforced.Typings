using Reinforced.Typings.Ast.TypeNames;
#pragma warning disable 1591
namespace Reinforced.Typings.Visitors.Dart
{
    partial class DartExportVisitor
    {
        #region Types

        public override void Visit(RtAsyncType node)
        {
            Write("Future<");

            if (node.TypeNameOfAsync != null) this.Visit(node.TypeNameOfAsync);
            else Write("void");

            Write(">");
            
            if (node.IsNullable)
            {
                Write("?");
            }
        }

        #endregion
    }
}