﻿using System;
using Reinforced.Typings.Ast;
using Reinforced.Typings.Attributes;

namespace Reinforced.Typings.Generators
{
    /// <summary>
    ///     Default code generator for interfaces. Derived from class generator since interfaces are very similar to classes in
    ///     TypeScript
    /// </summary>
    public class InterfaceCodeGenerator : ClassAndInterfaceGeneratorBase<RtInterface>
    {
        /// <summary>
        ///     Main code generator method. This method should write corresponding TypeScript code for element (1st argument) to
        ///     WriterWrapper (3rd argument) using TypeResolver if necessary
        /// </summary>
        /// <param name="element">Element code to be generated to output</param>
        /// <param name="result">Resulting node</param>
        /// <param name="resolver">Type resolver</param>
        public override RtInterface GenerateNode(Type element, RtInterface result, TypeResolver resolver)
        {
            var tc = Context.Project.Blueprint(element).Attr<TsInterfaceAttribute>();
            if (tc == null) throw new ArgumentException("TsInterfaceAttribute is not present", "element");
            Export(result, element, resolver, tc);

            result.OriginalIsAbstractClass = element.IsClass && element.IsAbstract;

            return result;
        }
    }
}