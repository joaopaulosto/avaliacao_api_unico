using FeiraSP.WEB.API.Model.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeiraSP.WEB.API.Model
{

    /// <summary>
    /// Classe que representa um Distrito
    /// </summary>
    [Table(name: "TB_DISTRITO")]
    public class Distrito
    {
        /// <summary>
        /// Identificador de um Distrito
        /// </summary>
        [Column(name: "ID")]
        [Key()]
        public int Id { get; set; }
        /// <summary>
        /// Nome do Distrito
        /// </summary>        
        [Column(name: "DISTRITO")]
        public string Nome { get; set; }

        
    }
}
