using System.Collections.Generic;
using System.Linq;
using Reinforced.Typings.Ast;

#pragma warning disable 1591
namespace Reinforced.Typings.Visitors.Dart
{
    partial class DartExportVisitor
    {
        public override void Visit(RtInterface node)
        {
            if (node == null) return;


            Visit(node.Documentation);
            WriterContext prev = Context;
            Context = WriterContext.Interface;
            AppendTabs();
            //if (node.Export) Write("export ");

            //WriteLine("@freezed");
            WriteLine("@JsonSerializable()");

            Write("class ");
            Visit(node.Name);

            if (node.Implementees.Count > 0)
            {
                Write(" extends ");
                SequentialVisit(node.Implementees, ", ");
            }

            Br();
            AppendTabs();
            Write("{");
            Br();
            Tab();
            

            var constructors = node.Members.OfType<RtConstructor>().ToList();
            constructors.ForEach(item => node.Members.Remove(item));
            
            // custom constructor
            WriteLine(string.Empty);
            

            Write(node.Name.TypeName);
            Write("(");
            

            node.Members
                .OfType<RtField>()
                .ToList()
                .ForEach(field =>
                {
                    Write("this.");
                    Visit(field.Identifier);
                    Write(", ");
                });
            var implementeesMembers = new List<string>();
            if (node.Implementees.Count > 0)
            {
                node.Implementees.ForEach(impl =>
                {
                    impl.Members.OfType<RtField>().ToList().ForEach(mem =>
                    {
                        // there have been some problems with extracting nullable in the generator
                        // some String properties were not marked as nullable
                        // this is a workaround
                        mem.Type.IsNullable = mem.Identifier.IsNullable;
                        Visit(mem.Type);
                        Write(" ");
                        var memNam = mem.Identifier.IdentifierName;
                        Write(memNam);
                        Write(", ");
                        implementeesMembers.Add(memNam);
                    });
                });
            }
            Write(")");

            if (implementeesMembers.Count > 0)
            {
                Write(":super(");
                implementeesMembers.ForEach(item =>
                {
                    Write(item);
                    Write(", ");
                });
                Write(")");
            }
            
            WriteLine(";");

            foreach (var rtMember in DoSortMembers(node.Members))
            {
                Visit(rtMember);
            }

            WriteLine("");
            AppendTabs();
            WriteLine("factory " + node.Name.TypeName + ".fromJson(Map<String, Object?> json) => _$" +
                      node.Name.TypeName + "FromJson(json);");
            
            UnTab();
            AppendTabs();


            WriteLine("}");
            WriteLine("");
            Context = prev;
        }
        
        // public override void Visit(RtInterface node)
        // {
        //     if (node == null) return;
        //
        //
        //     Visit(node.Documentation);
        //     WriterContext prev = Context;
        //     Context = WriterContext.Interface;
        //     AppendTabs();
        //     //if (node.Export) Write("export ");
        //
        //     //WriteLine("@freezed");
        //     WriteLine("@JsonSerializable()");
        //
        //     Write("class ");
        //     Visit(node.Name);
        //
        //     Write(" with _$" + node.Name.TypeName + " ");
        //
        //     // if (node.Implementees.Count > 0)
        //     // {
        //     //     Write(" extends ");
        //     //     SequentialVisit(node.Implementees, ", ");
        //     // }
        //
        //     Br();
        //     AppendTabs();
        //     Write("{");
        //     Br();
        //     Tab();
        //
        //     if (node.Implementees.Count > 0)
        //     {
        //         WriteLine(string.Empty);
        //         node.Implementees.ForEach(impl =>
        //         {
        //             WriteLine("// extends " + impl.TypeName);
        //         });
        //         WriteLine(string.Empty);
        //     }
        //     
        //
        //     // freezed constructor
        //     var fConstructor = new RtConstructor
        //     {
        //         ClassName = node.Name.TypeName,
        //     };
        //
        //     var fieldNodes = node.Members
        //         .OfType<RtField>()
        //         .ToList();
        //     fConstructor.Arguments.AddRange(
        //         fieldNodes.Select(fieldNode =>
        //             {
        //                 var rtArg = new RtArgument
        //                 {
        //                     Identifier = fieldNode.Identifier,
        //                     Type = fieldNode.Type,
        //                 };
        //
        //                 return rtArg;
        //             }
        //         )
        //     );
        //
        //     if (node.Implementees.Count > 0)
        //     {
        //         node.Implementees.ForEach(impl =>
        //         {
        //             
        //             impl.Children
        //                 .OfType<RtField>()
        //                 .ToList()
        //                 .ForEach(implField =>
        //                 {
        //                     fConstructor.Arguments.Add(
        //                         new RtArgument
        //                         {
        //                             Identifier = implField.Identifier,
        //                             Type = implField.Type,
        //                         }
        //                     );
        //                 });
        //         });
        //     }
        //
        //     node.Members.Add(fConstructor);
        //
        //     fieldNodes.ForEach(fieldNode => node.Members.Remove(fieldNode));
        //
        //     foreach (var rtMember in DoSortMembers(node.Members))
        //     {
        //         Visit(rtMember);
        //     }
        //
        //
        //     WriteLine("");
        //     AppendTabs();
        //     WriteLine("factory " + node.Name.TypeName + ".fromJson(Map<String, Object?> json) => _$" +
        //               node.Name.TypeName + "FromJson(json);");
        //
        //     UnTab();
        //     AppendTabs();
        //
        //
        //     WriteLine("}");
        //     WriteLine("");
        //     Context = prev;
        // }
    }
}