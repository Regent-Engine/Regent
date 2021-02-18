using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Redux.Generators
{
    [Generator]
    public class ReduxPropertyGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new ReduxPropertyAnalyzer());
            Console.WriteLine("pene");
        }

        public void Execute(GeneratorExecutionContext context)
        {
            
        }
    }
}
