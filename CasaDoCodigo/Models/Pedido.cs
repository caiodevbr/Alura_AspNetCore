using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models
{
    [Serializable]
    [DataContract]
    public class Pedido:BaseModel
    {
        [DataMember]
        public List<ItemPedido> Itens { get; set; }

        public Pedido()
        {
            Itens = new List<ItemPedido>();
        }
    }
}
