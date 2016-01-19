using System;
using System.Configuration;
using OsTicket.API;
using RestSharp.Extensions.MonoHttp;

namespace osticketclientesitedemo.Service
{
    /// <summary>
    /// Serviço para integração com o Central de Atendimento (OsTicket).
    /// </summary>
    public static class OsTicketService
    {

        /// <summary>
        /// Submete os dados do formulário de contato para o Central de Atendimento (OsTicket).
        /// </summary>
        /// <param name="nome">Nome.</param>
        /// <param name="cpf">Código de Pessoa Física (CPF).</param>
        /// <param name="telefone">Telefone.</param>
        /// <param name="email">E-mail.</param>
        /// <param name="categoria">Categoria (a.k.a. Tópico de Ajuda).</param>
        /// <param name="assunto">Assunto.</param>
        /// <param name="mensagem">Mensagem.</param>
        /// <returns>O código do chamado.</returns>
        public static int Submeter(string nome, string cpf, string telefone, string email, int? categoria, string assunto, string mensagem)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new Exception("Central de Atendimento: O campo Nome precisa ser fornecido.");
            }

            if (string.IsNullOrEmpty(cpf))
            {
                throw new Exception("Central de Atendimento: O campo CPF precisa ser fornecido.");
            }

            if (string.IsNullOrEmpty(telefone))
            {
                throw new Exception("Central de Atendimento: O campo Telefone precisa ser fornecido.");
            }

            if (string.IsNullOrEmpty(email))
            {
                throw new Exception("Central de Atendimento: O campo E-mail precisa ser fornecido.");
            }

            if (!categoria.HasValue)
            {
                throw new Exception("Central de Atendimento: O campo Categoria precisa ser fornecido.");
            }

            if (string.IsNullOrEmpty(assunto))
            {
                throw new Exception("Central de Atendimento: O campo Assunto precisa ser fornecido.");
            }

            if (string.IsNullOrEmpty(mensagem))
            {
                throw new Exception("Central de Atendimento: O campo Mensagem precisa ser fornecido.");
            }

            // Cria o pedido
            var ticket = new Ticket()
            {
                Name = nome,
                Email = email,
                TopicId = categoria,
                Subject = assunto,
                Message = PrepararMensagem(mensagem),
                IsMessageHtml = true
            };
            ticket.ExtraFields.Add("cpf", cpf);
            ticket.ExtraFields.Add("phone", telefone);

            // Envia o pedido
            return (new APIProxy(
                ConfigurationManager.AppSettings["osticket_url"],
                ConfigurationManager.AppSettings["osticket_apikey"]
            )).SubmitTicket(ticket);
        }

        /// <summary>
        /// Prepara a mensagem.
        /// </summary>
        /// <param name="mensagem">Mensagem a ser preparada.</param>
        /// <returns>A mensagem.</returns>
        private static string PrepararMensagem(string mensagem)
        {
            mensagem = HttpUtility.HtmlEncode(mensagem);
            mensagem = mensagem.Replace("\r\n", "\r");
            mensagem = mensagem.Replace("\n", "\r");
            mensagem = mensagem.Replace("\r", "<br>\r\n");
            mensagem = mensagem.Replace("  ", " &nbsp;");

            return mensagem;
        }
    }
}

