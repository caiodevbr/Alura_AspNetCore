using CasaDoCodigo.Models;
using System.Collections.Generic;

namespace CasaDoCodigo
{
    public interface IDataService
    {
        void InicialzaDB();

       List<Produto> GetProdutos();
    }
}