using System.Linq;
using Reinforced.Typings.Ast;

#pragma warning disable 1591
namespace Reinforced.Typings.Visitors.Dart
{
    partial class DartExportVisitor
    {
        public override void Visit(RtEnum node)
        {
            // DART 2.17
            // enum Foo {
            //     one(1),
            //     two(2);
            //
            //     const Foo(this.value);
            //     final num value;
            // }

            if (node == null) return;
            Visit(node.Documentation);
            var prev = Context;
            Context = WriterContext.Enum;
            AppendTabs();
            //if (node.Export) Write("export ");
            //if (node.IsConst) Write("const ");
            Write("enum ");
            Visit(node.EnumName);
            WriteLine(" {");
            Tab();
            RtEnumValue[] arr = node.Values.ToArray();
            
            bool hasValues = node.Values.Any(val => !string.IsNullOrEmpty(val.EnumValue));
            
            
            for (int i = 0; i < arr.Length; i++)
            {
                Visit(arr[i]);

                if (i != arr.Length - 1)
                {
                    WriteLine(",");
                }
                else
                {
                    if (hasValues)
                    {
                        Write(";");    
                    }
                }

                if (!string.IsNullOrEmpty(arr[i].LineAfter))
                {
                    AppendTabs();
                    Write(arr[i].LineAfter);
                    Br();
                }
            }

            
            if (hasValues)
            {
                WriteLine(string.Empty);
                WriteLine("const " + node.EnumName + "(this.value);");
                WriteLine("final "
                          + (node.Values.First().EnumValue.StartsWith("\"")
                          || node.Values.First().EnumValue.StartsWith("'")
                              ? "String "
                              : "num ") 
                          + "value;");
            }

            WriteLine(string.Empty);
            UnTab();
            AppendTabs();
            WriteLine("}");
            Context = prev;
        }
    }
}