using Reinforced.Typings.Ast.TypeNames;
#pragma warning disable 1591
namespace Reinforced.Typings.Visitors.Dart
{
    partial class DartExportVisitor
    {
        #region Types

        public override void Visit(RtDictionaryType node)
        {
            if (node == null) return;
            // string keyTypeSpec = node.IsKeyEnum ? " in " : ":";
            // Write("{ [key");
            // Write(keyTypeSpec);
            // Visit(node.KeyType);
            // Write("]: ");
            // Visit(node.ValueType);
            // Write(" }");
            
            Write("Map<");
            Visit(node.KeyType);
            Write(", ");
            Visit(node.ValueType);
            Write(">");
            
            if (node.IsNullable)
            {
                Write("?");
            }
        }

        #endregion
    }
}