using Reinforced.Typings.Ast.Dependency;
using Reinforced.Typings.Ast.TypeNames;

#pragma warning disable 1591

namespace Reinforced.Typings.Visitors.Dart
{
    partial class DartExportVisitor
    {
        public override void Visit(RtImport node)
        {
            Write("import ");
            if (node.Target != null)
            {
                Write(node.Target);
                Write(" ");
                if (node.IsRequire)
                {
                    WriteLine(string.Format("= require('{0}');", node.From));
                }
                else
                {
                    WriteLine(string.Format("from '{0}';", node.From));
                }
            }
            else
            {
                WriteLine(string.Format("'{0}';", node.From));
            }
            
        }

        public override void Visit(RtReference node)
        {
            WriteLine(string.Format("///<reference path=\"{0}\"/>", node.Path));
        }
    }
}