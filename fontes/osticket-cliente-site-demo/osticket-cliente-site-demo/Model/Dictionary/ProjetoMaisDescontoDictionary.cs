using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace osticketclientesitedemo.Dictionary
{
    /// <summary>
    /// Dicionário do Projeto Segura Preço.
    /// </summary>
    public class ProjetoMaisDescontoDictionary
    {
        /// <summary>
        /// Categorias do Formulário de Contato associados com a identificação
        /// do Tópico de Ajuda relativo no Central de Atendimento (OsTicket).
        /// </summary>
        public enum Categoria
        {
            [Description("Dúvida/Informação")]
            DuvidaInformacao = 278,
            [Description("Elogio")]
            Elogio = 278,
            [Description("Reclamação")]
            Reclamacao = 278,
            [Description("Sugestão")]
            Sugestao = 278
        }

        Dictionary<int, Categoria> dicionarioDe = new Dictionary<int, Categoria>();

        /// <summary>
        /// Inicializa uma nova instância da classe
        /// <see cref="ProjetoMaisDescontoDictionary"/>.
        /// </summary>
        public ProjetoMaisDescontoDictionary()
        {
            // Dicionário com os códigos recebidos do Formulário de Contato
            // associados à Categoria relativa ao Tópico de Ajuda.
            dicionarioDe.Add(1, Categoria.DuvidaInformacao);
            dicionarioDe.Add(2, Categoria.Elogio);
            dicionarioDe.Add(3, Categoria.Reclamacao);
            dicionarioDe.Add(4, Categoria.Sugestao);
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

