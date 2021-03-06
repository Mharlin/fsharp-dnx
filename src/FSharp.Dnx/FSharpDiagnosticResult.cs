using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.FSharp.Compiler;
using Microsoft.Extensions.CompilationAbstractions;

namespace FSharp.Dnx
{
  class FSharpDiagnosticResult : DiagnosticResult
  {
    public FSharpDiagnosticResult(IEnumerable<FSharpDiagnosticMessage> messages)
      : this(!messages.Any(m => m.Severity == DiagnosticMessageSeverity.Error), messages)
    {
    }

    public FSharpDiagnosticResult(bool success, IEnumerable<FSharpDiagnosticMessage> messages)
      : base(success, messages)
    {
    }

    internal static FSharpDiagnosticResult Error(string projectPath, string errorMessage)
    {
      var message = FSharpDiagnosticMessage.Error(projectPath, errorMessage);
      return new FSharpDiagnosticResult(ImmutableList.Create(message));
    }

    internal static FSharpDiagnosticResult CompilationResult(int resultCode, IEnumerable<FSharpErrorInfo> errors)
    {
      var success = resultCode == 0;
      return new FSharpDiagnosticResult(
        success,
        errors.Select(FSharpDiagnosticMessage.CompilationMessage).ToImmutableList());
    }
  }
}
