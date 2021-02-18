using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Redux.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redux.Generators
{
    public class ReduxPropertyAnalyzer : ISyntaxReceiver
    {
        public List<PropertyDeclarationSyntax> CandidateDirectProperties { get; } = new();
        public List<PropertyDeclarationSyntax> CandidateReadonlyProperties { get; } = new();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if(syntaxNode is PropertyDeclarationSyntax pds)
            {
                foreach(var attrib_list in pds.AttributeLists)
                {
                    foreach(var att in attrib_list.Attributes)
                    {
                        if(att.Name.ToString() == ReadonlyReduxPropertyAttribute.ToStringStatic())
                        {
                            CandidateReadonlyProperties.Add(pds);
                        }
                        else if(att.Name.ToString() == DirectReduxPropertyAttribute.ToStringStatic())
                        {
                            CandidateDirectProperties.Add(pds);    
                        }
                    }
                }
            }
        }
    }
}
