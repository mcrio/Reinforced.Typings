using Reinforced.Typings.Ast;
#pragma warning disable 1591
namespace Reinforced.Typings.Visitors.Dart
{
    partial class DartExportVisitor
    {
        public override void Visit(RtArgument node)
        {
            if (node == null) return;
            Decorators(node);
            if (node.IsVariableParameters) Write("...");
            
            
            // there have been some problems with extracting nullable in the generator
            // some String properties were not marked as nullable
            // this is a workaround
            node.Type.IsNullable = node.Identifier.IsNullable;
            Visit(node.Type);
            Write(" ");
            Visit(node.Identifier);
            
            // if a default value is used, then the parameter may be omitted as the default value is in place.
            // if (Context == WriterContext.Interface && !node.Identifier.IsNullable && !string.IsNullOrEmpty(node.DefaultValue))
            // {
            //     Write("?");
            // }

            if (Context != WriterContext.Interface && !string.IsNullOrEmpty(node.DefaultValue))
            {
                Write(" = ");
                Write(node.DefaultValue);
            }
        }

    }
}
