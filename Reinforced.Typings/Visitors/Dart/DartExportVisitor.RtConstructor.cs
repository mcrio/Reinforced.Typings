using System;
using Reinforced.Typings.Ast;
#pragma warning disable 1591
namespace Reinforced.Typings.Visitors.Dart
{
    partial class DartExportVisitor
    {
        public override void Visit(RtConstructor node)
        {
            if (node == null) return;
            // if (Context == WriterContext.Interface) return;
            Visit(node.Documentation);
            AppendTabs();
            
            Write($"const factory {node.ClassName}(");
            SequentialVisit(node.Arguments, ", ");
            Write(")");
            if (node.NeedsSuperCall && node.Body == null)
            {
                string ncp = string.Empty;
                if (node.SuperCallParameters != null) ncp = string.Join(", ", node.SuperCallParameters);
                node.Body = new RtRaw(String.Format("super({0})", ncp));
            }
            
            Write($" = _{node.ClassName}; ");

            // if (node.Body != null && !string.IsNullOrEmpty(node.Body.RawContent))
            // {
            //     CodeBlock(node.Body);
            // }
            // else
            // {
            //     EmptyBody(null);
            // }

            if (!string.IsNullOrEmpty(node.LineAfter))
            {
                AppendTabs();
                Write(node.LineAfter);
                Br();
            }
        }
    }
}