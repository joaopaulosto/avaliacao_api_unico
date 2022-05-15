using FeiraSP.WEB.API.Model.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeiraSP.WEB.API.Model
{
    /// <summary>
    /// Classe que representa os dados de uma SubPrefeitura
    /// </summary>
    [Table(name: "TB_SUB_PREFEITURA")]
    public class SubPrefeitura
    {
        /// <summary>
        /// IDentificado da SubPrefeitura
        /// </summary>
        [Key()]
        public int Id { get; set; }

        /// <summary>
        /// Nome da SubPrefeitura
        /// </summary>
        [Column(name: "SUBPREFE")]
        public string Nome { get; set; }
        
    }

}
