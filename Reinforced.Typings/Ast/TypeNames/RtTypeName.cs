using System.Collections.Generic;

namespace Reinforced.Typings.Ast.TypeNames
{
    /// <summary>
    /// Abstract AST node for type name
    /// </summary>
    public abstract class RtTypeName : RtNode
    {
        /// <summary>
        /// indicates if type is nullable
        /// </summary>
        public bool IsNullable { get; set; } = false;
        
        /// <summary>
        ///  used to get implemented interface properties
        /// </summary>
        public List<RtNode> Members { get; private set; } = new List<RtNode>();

        public bool OriginalTypeIsAbstractClass { get; set; } = false;
    }
}
