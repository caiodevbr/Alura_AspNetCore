﻿using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo
{
    public class DataService : IDataService
    {
        private readonly Contexto _contexto;
        private readonly IHttpContextAccessor _contextAccessor;
        public DataService(Contexto contexto, IHttpContextAccessor contextAccessor)
        {
            this._contexto = contexto;
            this._contextAccessor = contextAccessor;
        }

        public void AddItemPedido(int produtoId)
        {
            var produto =
                _contexto.Produtos
                .Where(p => p.Id == produtoId)
                .SingleOrDefault();

            if (produto != null)
            {
                //var pedido=_contexto.Pedidos.FirstOrDefault();
                int? pedidoId = GetSessionPedidoId();

                Pedido pedido = null;
                if (pedidoId.HasValue)
                {
                     pedido = _contexto.Pedidos.Where(p => p.Id == pedidoId.Value).SingleOrDefault();
                }


                if (pedido == null)
                    pedido = new Pedido();

                if (!_contexto.ItensPedido
                    .Where(i =>
                    i.Pedido.Id == pedido.Id &&
                    i.Produto.Id == produtoId)
                    .Any())
                {
                    _contexto.ItensPedido.Add(
                        new ItemPedido(pedido, produto, 1));

                    _contexto.SaveChanges();

                    //salva o id do pedido na sessão
                    SetSessionPedidoId(pedido);
                }
            }
        }

        private void SetSessionPedidoId(Pedido pedido)
        {
            //salva o id do pedido na sessão
            _contextAccessor.HttpContext.Session.SetInt32("pedidoId", pedido.Id);
        }

        private int? GetSessionPedidoId()
        {
            var retorno= _contextAccessor.HttpContext.Session.GetInt32("pedidoId");
            return retorno;
        }

        public List<ItemPedido> GetItensPedido()
        {
            var pedidoId = GetSessionPedidoId();

            var pedido = _contexto.Pedidos.Where(p => p.Id==pedidoId).Single();

            return this._contexto.ItensPedido.Where(x => x.Pedido.Id == pedido.Id).ToList();
        }

        public List<Produto> GetProdutos()
        {

            return this._contexto.Produtos.ToList();
        }

        public void InicializaDB()
        {
            this._contexto.Database.EnsureCreated();
            if (this._contexto.Produtos.Count() == 0)
            {
                List<Produto> produtos = new List<Produto>
                {
                    new Produto("Sleep not found", 59.90m),
                    new Produto("May the code be with you", 59.90m),
                    new Produto("Rollback", 59.90m),
                    new Produto("REST", 69.90m),
                    new Produto("Design Patterns com Java", 69.90m),
                    new Produto("Vire o jogo com Spring Framework", 69.90m),
                    new Produto("Test-Driven Development", 69.90m),
                    new Produto("iOS: Programe para iPhone e iPad", 69.90m),
                    new Produto("Desenvolvimento de Jogos para Android", 69.90m)
                };

                foreach (var produto in produtos)
                {
                    this._contexto.Produtos
                        .Add(produto);

                    //this._contexto.ItensPedido
                    //    .Add(new ItemPedido(produto, 1));
                }

                this._contexto.SaveChanges();
            }
        }



        public UpdateItemPedidoResponse UpdateItemPedido(ItemPedido itemPedido)
        {
            var itemPedidoDB =
            _contexto.ItensPedido
                .Where(i => i.Id == itemPedido.Id)
                .SingleOrDefault();

            if (itemPedidoDB != null)
            {
                itemPedidoDB.AtualizaQuantidade(itemPedido.Quantidade);

                if (itemPedidoDB.Quantidade == 0)
                    _contexto.ItensPedido.Remove(itemPedidoDB);

                _contexto.SaveChanges();
            }

            var itensPedido = _contexto.ItensPedido.ToList();

            var carrinhoViewModel = new CarrinhoViewModel(itensPedido);

            return new UpdateItemPedidoResponse(itemPedidoDB, carrinhoViewModel);
        }
    }
}



//using CasaDoCodigo.Models;
//using CasaDoCodigo.Models.ViewModel;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CasaDoCodigo
//{
//    public class DataService : IDataService
//    {
//        private readonly Contexto _contexto;
//        public DataService(Contexto contexto)
//        {
//            this._contexto = contexto;
//        }

//        public void AddItemPedido(int produtoId)
//        {
//            var produto =
//                _contexto.Produtos
//                .Where(p => p.Id == produtoId)
//                .SingleOrDefault();

//            if (produto != null)
//            {
//                if (!_contexto.ItensPedido
//                    .Where(i => i.Produto.Id == produtoId)
//                    .Any())
//                {
//                    _contexto.ItensPedido.Add(
//                        new ItemPedido(produto, 1));

//                    _contexto.SaveChanges();
//                }
//            }
//        }

//        public List<ItemPedido> GetItensPedido()
//        {
//            return this._contexto.ItensPedido.ToList();
//        }

//        public List<Produto> GetProdutos()
//        {
//            return this._contexto.Produtos.ToList();
//        }

//        public void InicialzaDB()
//        {
//            this._contexto.Database.EnsureCreated();
//            if (!_contexto.Produtos.Any())
//            {
//                List<Produto> produtos = new List<Produto>
//        {
//            new Produto("Sleep not found", 59.90m),
//            new Produto("May the code be with you", 59.90m),
//            new Produto("Rollback", 59.90m),
//            new Produto("REST", 69.90m),
//            new Produto("Design Patterns com Java", 69.90m),
//            new Produto("Vire o jogo com Spring Framework", 69.90m),
//            new Produto("Test-Driven Development", 69.90m),
//            new Produto("iOS: Programe para iPhone e iPad", 69.90m),
//            new Produto("Desenvolvimento de Jogos para Android", 69.90m)
//        };
//                //insere os produtos
//                foreach (var produto in produtos)
//                {
//                    _contexto.Produtos.Add(produto);


//                    _contexto.ItensPedido.Add(new ItemPedido(produto, 1));
//                }
//            }

//            _contexto.SaveChanges();
//        }

//        public UpdateItemPedidoResponse UpdateItemPedido(ItemPedido itemPedido)
//        {
//            var itemPedidoDB=_contexto.ItensPedido.Where(i => i.Id == itemPedido.Id).SingleOrDefault();

//            if(itemPedidoDB!=null)
//            {
//                itemPedidoDB.AtualizaQuantidade(itemPedido.Quantidade);
//                _contexto.SaveChanges();
//            }
//            if (itemPedidoDB.Quantidade == 0)
//                _contexto.ItensPedido.Remove(itemPedidoDB);

//            var itensPedidos = _contexto.ItensPedido.ToList();
//            var carrinhoViewModel = new CarrinhoViewModel(itensPedidos);

//            return new UpdateItemPedidoResponse(itemPedidoDB, carrinhoViewModel);

//        }
//    }
//}
