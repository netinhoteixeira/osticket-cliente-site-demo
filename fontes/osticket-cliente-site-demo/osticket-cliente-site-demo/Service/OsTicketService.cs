using System;
using System.Configuration;
using OsTicket.API;
using RestSharp.Extensions.MonoHttp;

namespace osticketclientesitedemo.Service
{
    public static class OsTicketService
    {

        public static int Submeter(string nome, string cpf, string telefone, string email, int? categoria, string assunto, string mensagem)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new Exception("OsTicket - O campo Nome precisa ser fornecido.");
            }

            if (string.IsNullOrEmpty(cpf))
            {
                throw new Exception("OsTicket - O campo CPF precisa ser fornecido.");
            }

            if (string.IsNullOrEmpty(telefone))
            {
                throw new Exception("OsTicket - O campo Telefone precisa ser fornecido.");
            }

            if (string.IsNullOrEmpty(email))
            {
                throw new Exception("OsTicket - O campo E-mail precisa ser fornecido.");
            }

            if (!categoria.HasValue)
            {
                throw new Exception("OsTicket - O campo Categoria precisa ser fornecido.");
            }

            if (string.IsNullOrEmpty(assunto))
            {
                throw new Exception("OsTicket - O campo Assunto precisa ser fornecido.");
            }

            if (string.IsNullOrEmpty(mensagem))
            {
                throw new Exception("OsTicket - O campo Mensagem precisa ser fornecido.");
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

        public static string PrepararMensagem(string mensagem)
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

