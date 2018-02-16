using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IDataService _dataService;
        public PedidoController(IDataService dataService)
        {
            _dataService = dataService;
        }
      

        public IActionResult Carrossel()
        {
            var produtos = _dataService.GetProdutos();
            return View(produtos);
        }

        public IActionResult Carrinho()
        {
            var produtos = _dataService.GetProdutos();

            CarrinhoViewModel viewModel = GetCarrinhoViewModel();
            return View(viewModel);
        }

        private CarrinhoViewModel GetCarrinhoViewModel()
        {
            var produtos = _dataService.GetProdutos();

            var itensCarrinho = this._dataService.GetItensPedido();


            CarrinhoViewModel viewModel = new CarrinhoViewModel(itensCarrinho);
            return viewModel;
        }

        public IActionResult Resumo()
        {
            CarrinhoViewModel viewModel = GetCarrinhoViewModel();

            return View(viewModel);
        }
    }
}
