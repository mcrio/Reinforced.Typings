using System.Linq;
using Reinforced.Typings.Ast;
#pragma warning disable 1591
namespace Reinforced.Typings.Visitors.Dart
{
    partial class DartExportVisitor
    {
        public override void Visit(RtClass node)
        {
            if (node == null) return;
            Visit(node.Documentation);
            var prev = Context;
            Context = WriterContext.Class;
            AppendTabs();
            
            node.Decorators.Add(new RtDecorator("freezed"));
            
            Decorators(node);
            //if (node.Export) Write("export ");
            if (node.Abstract) Write("abstract ");
            Write("class ");
            Visit(node.Name);
            if (node.Extendee != null)
            {
                Write(" extends ");
                Visit(node.Extendee);
            }
            if (node.Implementees.Count > 0)
            {
                Write(" implements ");
                SequentialVisit(node.Implementees, ", ");
            }
            
            Write(" with _$" + node.Name.TypeName);
            
            Br(); AppendTabs();
            Write("{"); Br();
            Tab();
            var members = DoSortMembers(node.Members);
            foreach (var rtMember in members)
            {
                Visit(rtMember);
            }
            UnTab();
            AppendTabs(); WriteLine("}");
            Context = prev;
        }

    }
}
