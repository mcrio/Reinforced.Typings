using System;
using System.Linq;
using Reinforced.Typings.Ast;
#pragma warning disable 1591
namespace Reinforced.Typings.Visitors.Dart
{
    partial class DartExportVisitor
    {
        public override void Visit(RtEnumValue node)
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
            AppendTabs();
            Write(
                node.EnumValueName[0].ToString().ToLowerInvariant() 
                + node.EnumValueName.Substring(1)
                );
            if (!string.IsNullOrEmpty(node.EnumValue))
            {
                Write("(");
                Write(node.EnumValue);
                Write(")");
            }
        }
    }
}