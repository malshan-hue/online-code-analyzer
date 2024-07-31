using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace online_code_editor.Controllers
{
    public class AnalyzerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CheckSyntax([FromBody] string code)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(code);
            var diagnostics = syntaxTree.GetDiagnostics();

            var errors = diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error)
                                    .Select(d => d.GetMessage());

            return Ok(new { errors = errors.ToArray() });
        }
    }
}
