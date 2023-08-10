using FastReport.Data;
using FastReport.Export.PdfSimple;
using FastReport.Web;
using Microsoft.AspNetCore.Mvc;
using MVC_2022.Areas.Admin.Services;
using MVC_2022.Areas.FastReportUtils;

namespace MVC_2022.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminLanchesReportController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnv;
        private readonly RelatorioLanchesServices _relatorioLanchesService;

        public AdminLanchesReportController(IWebHostEnvironment webHostEnv,
            RelatorioLanchesServices relatorioLanchesService)
        {
            _webHostEnv = webHostEnv;
            _relatorioLanchesService = relatorioLanchesService;
        }
        public async Task<ActionResult> LanchesCategoriaReport()
        {
            //Cria a instancia do report web:
            var webReport = new WebReport();

            //Instancia a conexão com o sql
            var mssqlDataConnection = new MsSqlDataConnection();

            //registra a conexão no dicionario do webreport
            webReport.Report.Dictionary.AddChild(mssqlDataConnection);

            //Carrega o layout com o caminho físico
            webReport.Report.Load(Path.Combine(_webHostEnv.ContentRootPath, "wwwroot/reports", "lanchesCategorias.frx"));

            //Pega os dados e transforma em dataTable que é o tipo que o FastReport usa.
            var lanches = HelperFastReport.GetTable(await _relatorioLanchesService.GetLanchesReport(), "LanchesReport");
            var categorias = HelperFastReport.GetTable(await _relatorioLanchesService.GetCategoriasReport(), "CategoriasReport");

            //Registra os dados no report.
            webReport.Report.RegisterData(lanches, "LancheReport");
            webReport.Report.RegisterData(categorias, "CategoriasReport");

            //Retorna view com o tipo report.
            return View(webReport);
        }

        [Route("LanchesCategoriaPDF")]
        public async Task<ActionResult> LanchesCategoriaPDF()
        {
            var webReport = new WebReport();
            var mssqlDataConnection = new MsSqlDataConnection();

            webReport.Report.Dictionary.AddChild(mssqlDataConnection);

            webReport.Report.Load(Path.Combine(_webHostEnv.ContentRootPath, "wwwroot/reports",
                                               "lanchesCategorias.frx"));

            var lanches = HelperFastReport.GetTable(await _relatorioLanchesService.GetLanchesReport(), "LanchesReport");
            var categorias = HelperFastReport.GetTable(await _relatorioLanchesService.GetCategoriasReport(), "CategoriasReport");

            webReport.Report.RegisterData(lanches, "LancheReport");
            webReport.Report.RegisterData(categorias, "CategoriasReport");

            webReport.Report.Prepare();

            //Cria um objeto de memoria
            Stream stream = new MemoryStream();

            //Exporta o relatório para a memória
            webReport.Report.Export(new PDFSimpleExport(), stream);
            stream.Position = 0;

            //return File(stream, "application/zip", "LancheCategoria.pdf"); // Faz download
            return new FileStreamResult(stream, "application/pdf"); // Abre o pdf no navegador
        }
    }
}
