using CasaDoCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo
{
    public class DataService : IDataService
    {
        private readonly Contexto _contexto;
        public DataService(Contexto contexto)
        {
            this._contexto = contexto;
        }

        public void InicialzaDB()
        {
            this._contexto.Database.EnsureCreated();
            if (!_contexto.Produtos.Any())
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
                //insere os produtos
                foreach (var produto in produtos)
                {
                    _contexto.Produtos.Add(produto);


                    _contexto.ItensPedido.Add(new ItemPedido(produto, 1));
                }
            }

            _contexto.SaveChanges();
        }

    }
}
