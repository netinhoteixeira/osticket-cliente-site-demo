using System;
using OsTicket.API;
using RestSharp.Extensions.MonoHttp;

namespace osticketclientedemo
{
    class MainClass
    {
        // Projeto Banco do Brasil
        enum Categoria
        {
            Elogio = 21,
            Informacao = 20,
            Reclamacao = 13,
            Sugestao = 22
        }

        public static void Main(string[] args)
        {
            const string url = "http://192.168.83.104/osticket";
            const string chave = "DCFB4EC0A3D6E6A93C92397DE72739E9";

            var ticket = new Ticket()
            {
                Name = "Nome da Pessoa",
                Email = "email",
                TopicId = (int)Categoria.Reclamacao,
                Subject = "Reclamação 2",
                Message = PrepararMensagem("Conteúdo de Reclamação 2"),
                IsMessageHtml = true
            };
            ticket.ExtraFields.Add("cpf", "Número do CPF");
            ticket.ExtraFields.Add("phone", "Número do Telefone");

            var proxy = new APIProxy(url, chave);
            int retornoEnvio = proxy.SubmitTicket(ticket);

            Console.WriteLine(retornoEnvio);
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
