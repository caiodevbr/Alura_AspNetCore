using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models
{
    [Serializable]
    [DataContract]
    public class ItemPedido:BaseModel
    {
        [DataMember]
        [Required]
         public Pedido Pedido { get; private set; }
        [DataMember]
        [Required]
        public Produto Produto { get; private set; }
        [DataMember]
        public int Quantidade { get; private set; }
        [DataMember]
        public decimal PrecoUnitario { get; private set; }
        [DataMember]
        public decimal Subtotal
        {
            get
            {
                return Quantidade * PrecoUnitario;
            }
        }
        public ItemPedido()
        {

        }

        public ItemPedido(int id, Produto produto,Pedido pedido,
            int quantidade):this(pedido,produto,quantidade)
        {
            this.Id = id;
        }

        public ItemPedido(Pedido pedido ,Produto produto,
            int quantidade)
        {
            this.Produto = produto;
            this.Quantidade = quantidade;
            this.PrecoUnitario = produto.Preco;
            this.Pedido = pedido;
        }

        public void AtualizaQuantidade(int quantidade)
        {
            this.Quantidade = quantidade;
        }
    }
}
