using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace osticketclientesitedemo.Dictionary
{
    /// <summary>
    /// Dicionário do Projeto Vantagens.
    /// </summary>
    public class ProjetoVantagensDictionary
    {
        /// <summary>
        /// Categorias do Formulário de Contato associados com a identificação
        /// do Tópico de Ajuda relativo no Central de Atendimento (OsTicket).
        /// </summary>
        public enum Categoria
        {
            [Description("Dúvidas sobre o programa")]
            DuvidasSobrePrograma = 488,
            [Description("Problemas de acesso, cadastro")]
            ProblemasAcessoCadastro = 488,
            [Description("Problemas com parceiros de acúmulo")]
            ProblemasParceirosAcumulo = 488,
            [Description("Problemas com parceiros de resgate")]
            ProblemasParceirosResgate = 488,
            [Description("Problemas com produtos")]
            ProblemasProdutos = 488,
            [Description("Dúvidas sobre saldo")]
            DuvidasSobreSaldo = 488,
            [Description("Contato comercial")]
            ContatoComercial = 488
        }

        Dictionary<int, Categoria> dicionarioDe = new Dictionary<int, Categoria>();

        /// <summary>
        /// Inicializa uma nova instância da classe
        /// <see cref="osticketclientesitedemo.Dictionary.ProjetoVantagensDictionary"/>.
        /// </summary>
        public ProjetoVantagensDictionary()
        {
            // Dicionário com os códigos recebidos do Formulário de Contato
            // associados à Categoria relativa ao Tópico de Ajuda.
            dicionarioDe.Add(1, Categoria.DuvidasSobrePrograma);
            dicionarioDe.Add(2, Categoria.ProblemasAcessoCadastro);
            dicionarioDe.Add(3, Categoria.ProblemasParceirosAcumulo);
            dicionarioDe.Add(4, Categoria.ProblemasParceirosResgate);
            dicionarioDe.Add(5, Categoria.ProblemasProdutos);
            dicionarioDe.Add(6, Categoria.DuvidasSobreSaldo);
            dicionarioDe.Add(7, Categoria.ContatoComercial);
        }

        /// <summary>
        /// Obtém a Categoria com a identificação fornecida.
        /// </summary>
        /// <param name="categoria">Identificação da Categoria.</param>
        public Categoria? ObterCategoria(int? categoria)
        {
            if ((categoria.HasValue) && (dicionarioDe.ContainsKey(categoria.Value)))
            {
                return dicionarioDe[categoria.Value];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Obtém o valor da Categoria (a ser utilizado no Central de Atendimento).
        /// </summary>
        /// <param name="categoria">Categoria.</param>
        /// <returns>O valor da Categoria (a ser utilizado no Central de Atendimento).</returns>
        public int? ObterValor(Categoria? categoria)
        {
            if (categoria.HasValue)
            {
                return (int)categoria;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Obtém o rótulo da Categoria (a ser utilizado no Central de Atendimento).
        /// </summary>
        /// <param name="categoria">Categoria.</param>
        /// <returns>O rótulo da Categoria (a ser utilizado no Central de Atendimento).</returns>
        public string ObterRotulo(Categoria? categoria)
        {
            var field = categoria.GetType().GetField(categoria.ToString());
            var customAttributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (customAttributes.Length > 0)
            {
                return (customAttributes[0] as DescriptionAttribute).Description;
            }
            else
            {
                return categoria.ToString();
            }
        }
    }
}

