using System.Text.RegularExpressions;
using Reinforced.Typings.Ast;
using Reinforced.Typings.Ast.TypeNames;

#pragma warning disable 1591
namespace Reinforced.Typings.Visitors.Dart
{
    partial class DartExportVisitor
    {
        public override void Visit(RtField node)
        {
            if (node == null) return;
            Visit(node.Documentation);
            AppendTabs();
            if (Context != WriterContext.Interface)
            {
                Decorators(node);
                Modifiers(node);
            }

            // there have been some problems with extracting nullable in the generator
            // some String properties were not marked as nullable
            // this is a workaround
            node.Type.IsNullable = node.Identifier.IsNullable;

            void printConverter(RtTypeName? typeName)
            {
                if (typeName is null || !typeName.OriginalTypeIsAbstractClass)
                {
                    return;
                }

                var rgx = new Regex("[^a-zA-Z0-9 -]");
                WriteLine("");
                WriteLine($"@_{rgx.Replace(typeName.ToString()!, "")}Converter()");
            }

            if (node.Type is RtArrayType arrayType)
            {
                printConverter(arrayType.ElementType);
            }
            else if (node.Type is RtSimpleTypeName simpleTypeName)
            {
                printConverter(simpleTypeName);
            }

            Visit(node.Type);
            Write(" ");
            Visit(node.Identifier);

            if (!string.IsNullOrEmpty(node.InitializationExpression))
            {
                Write(" = ");
                Write(node.InitializationExpression);
            }

            Write(";");
            Br();
            if (!string.IsNullOrEmpty(node.LineAfter))
            {
                AppendTabs();
                Write(node.LineAfter);
                Br();
            }
        }
    }
}