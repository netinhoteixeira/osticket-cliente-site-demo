using System;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;
using osticketclientesitedemo.DataTransferObject;
using osticketclientesitedemo.Dictionary;
using osticketclientesitedemo.Service;

namespace osticketclientesitedemo.Controller
{
    /// <summary>
    /// Controlador Principal.
    /// </summary>
    public class IndexController : NancyModule
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="osticketclientesitedemo.Controller.IndexController"/>.
        /// </summary>
        public IndexController()
            : base("/formularios")
        {
            Get["/"] = parameters => Response.AsFile("Content/index.html", "text/html");
            Get["/projetos"] = parameters => Response.AsFile("Content/index.html", "text/html");
            Get["/projetos/segurapreco"] = parameters => Response.AsFile("Content/projetos/segurapreco.html", "text/html");
            Post["/projetos/segurapreco"] = parameters => EnviarFormularioContatoSeguraPreco();
        }

        /// <summary>
        /// Envia o formulário de contato do projeto Segura Preço.
        /// </summary>
        /// <returns>O resultado do envio do formulário de contato.</returns>
        private Negotiator EnviarFormularioContatoSeguraPreco()
        {
            var formulario = this.Bind<RecebeFormularioContatoDataTransferObject>();
            var dicionario = new ProjetoSeguraPrecoDictionary();

            try
            {
                // Seleciona a categoria de acordo com o seu dicionário
                ProjetoSeguraPrecoDictionary.Categoria? categoria = dicionario.ObterCategoria(formulario.Assunto);

                // Envia os dados do formulário de contato
                int ticket = OsTicketService.Submeter(
                                 formulario.Nome,
                                 formulario.Cpf,
                                 formulario.Telefone,
                                 formulario.Email,
                                 dicionario.ObterValor(categoria),
                                 dicionario.ObterRotulo(categoria),
                                 formulario.Mensagem);

                Console.WriteLine("Ticket: " + ticket);

                this.ViewBag.ticket = ticket;
                return View["enviado"];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);

                this.ViewBag.mensagem = ex.Message;
                return View["erro"];
            }
        }
    }
}

