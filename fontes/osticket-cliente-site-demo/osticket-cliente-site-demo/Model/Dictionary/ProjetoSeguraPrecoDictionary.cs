using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace osticketclientesitedemo.Dictionary
{
    public class ProjetoSeguraPrecoDictionary
    {
        public enum Categoria
        {
            [Description("Dúvida/Informação")]
            DuvidaInformacao = 165,
            [Description("Elogio")]
            Elogio = 166,
            [Description("Reclamação")]
            Reclamacao = 168,
            [Description("Sugestão")]
            Sugestao = 169
        }

        Dictionary<int, Categoria> dicionarioDe = new Dictionary<int, Categoria>();

        public ProjetoSeguraPrecoDictionary()
        {
            dicionarioDe.Add(1, Categoria.DuvidaInformacao);
            dicionarioDe.Add(2, Categoria.Elogio);
            dicionarioDe.Add(3, Categoria.Reclamacao);
            dicionarioDe.Add(4, Categoria.Sugestao);
        }

        public Categoria? Converter(int? categoria)
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

