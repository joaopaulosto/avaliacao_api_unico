using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeiraSP.WEB.API.Model
{
    /// <summary>
    /// Classe que presentada o dados de um determinada Feria da cidade de São Paulo
    /// </summary>

    [Table(name: "TB_FEIRA")]
    public class Feira
    {
        /// <summary>
        /// Identificado do Feira
        /// </summary>

        [Column(name: "ID")]
        [Key()]
        public int Id { get; set; }

        /// <summary>
        /// Logitude da Feira
        /// </summary>
        /// 
        [Column(name: "LONG")]
        public int Longitude { get; set; }

        /// <summary>
        /// Latitude de Feira
        /// </summary>
        [Column(name: "LAT")]
        public int Latitude { get; set; }

        /// <summary>
        /// SetCens da Feira
        /// </summary>
        [Column(name: "SETCENS")]
        public long SetCens { get; set; }

        /// <summary>
        /// Area definida pela prefeitura
        /// </summary>
        [Column(name: "AREAP")]
        public long AreaPrefeitura { get; set; }

        /// <summary>
        /// Identificado do Distrito onde a feria acontece
        /// </summary>
        
       
        [Column(name: "CODDIST")] 
        public int DistritoId { get; set; }


        /// <summary>
        /// Referencia ao Distrito da Feira
        /// </summary>
        [ForeignKey("DistritoId")]
        public Distrito? Distrito { get; set; }

       

        /// <summary>
        /// Identificado da Feira
        /// </summary>        
        [Column(name: "CODSUBPREF")]
        public int SubPrefeituraID { get; set; }


        /// <summary>
        /// Referencia da SubPrefeitura da Feira
        /// </summary>
        [ForeignKey("SubPrefeituraID")]
        public SubPrefeitura? SubPrefeitura { get; set; }

        
        /// <summary>
        /// Região 5 onde a feira acontece
        /// </summary>
        [Column(name: "REGIAO5")]
        public string Regiao5 { get; set; }

        /// <summary>
        /// Região 8 onde a feira acontece
        /// </summary>
        [Column(name: "REGIAO8")]
        public string Regiao8 { get; set; }

        /// <summary>
        /// Nome dado a feira que é de conhecimento dos cidadoes
        /// </summary>
        [Column(name: "NOME_FEIRA")]
        public string Nome { get; set; }
        /// <summary>
        /// Registro que comprova que a feira é regularizada na prefeitura
        /// </summary>
        [Column(name: "REGISTRO")]
        public string Registro { get; set; }

        /// <summary>
        /// Endereço físico onde a feira acontece
        /// </summary>
        [Column(name: "LOGRADOURO")]
        public string Logradouro { get; set; }

        /// <summary>
        /// Número do endereço onde a feira acontece
        /// </summary>
        [Column(name: "NUMERO")]
        public string Numero { get; set; }

        /// <summary>
        /// Bairro onde a feira acontece
        /// </summary>
        [Column(name: "BAIRRO")]
        public string Bairro { get; set; }

        /// <summary>
        /// Ponto de referencia da feira
        /// </summary>
        [Column(name: "REFERENCIA")]
        public string Referencia { get; set; }






    }
}
