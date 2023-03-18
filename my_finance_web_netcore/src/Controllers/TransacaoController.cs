using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using myfinance_web_netcore.Domain.Services.Interfaces;
using myfinance_web_netcore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace myfinance_web_netcore.Controllers
{
    [Route("[controller]")]
    public class TransacaoController : Controller
    {
        private readonly ILogger<PlanoContaController> _logger;
        private readonly ITransacaoService _transacaoService;
        private readonly IPlanoContaService _planoContaService;

        public TransacaoController(ILogger<PlanoContaController> logger)
        public TransacaoController(
            ILogger<PlanoContaController> logger, 
            ITransacaoService transacaoService, 
            IPlanoContaService planoContaService)
        {
            _logger = logger;
            _transacaoService = transacaoService;
            _planoContaService = planoContaService;
        }
        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            ViewBag.Transacoes = _transacaoService.ListarRegistros();
            return View();
        }
        [HttpGet]
        [Route("Cadastro")]
        public IActionResult Cadastro()
        [Route("Cadastro/{id}")]
        public IActionResult Cadastro(int? id)
        {
            return View();
            var model = new TransacaoModel();

            if (id != null){
                model = _transacaoService.RetornarRegistro((int)id);
            }

            var lista = _planoContaService.ListarRegistros();
            model.PlanoContas = new SelectList(lista,"Id", "Descricao");
            return View(model);
        }

        [HttpPost]
        [Route("Cadastro")]
        [Route("Cadastro/{id}")]
        public IActionResult Cadastro(TransacaoModel model)
        {
            _transacaoService.Salvar(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Excluir/{id}")]
        public IActionResult excluir(int id)
        {
            _transacaoService.Excluir(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Relatorio")]
        public IActionResult Relatorio()
        {
            RelatorioTransacoesModel model = new RelatorioTransacoesModel();
            model.DataInicio = DateTime.Now;
            model.DataFim = DateTime.Now;
            return View(model);
        }

        [HttpPost]
        [Route("Relatorio")]
        public IActionResult Relatorio(RelatorioTransacoesModel model)
        {
            if (model.DataInicio != null || model.DataFim != null)
            {
                model = _transacaoService.PegarPorPeriodo(model.DataInicio, model.DataFim);
            }
            ViewBag.ReceitasBag = model.TransacaoReceitas.ToString();
            ViewBag.DespesasBag = model.TransacaoDespesas.ToString();
            return View(model);
        }
    }
}