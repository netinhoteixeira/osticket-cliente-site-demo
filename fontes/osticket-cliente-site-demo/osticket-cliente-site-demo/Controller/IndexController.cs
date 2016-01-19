using System;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;
using osticketclientesitedemo.DataTransferObject;
using osticketclientesitedemo.Dictionary;
using osticketclientesitedemo.Service;

namespace osticketclientesitedemo.Controller
{
    public class IndexController : NancyModule
    {
        public IndexController()
            : base("/formularios")
        {
            Get["/"] = parameters => Response.AsFile("Content/index.html", "text/html");
            Get["/projetos"] = parameters => Response.AsFile("Content/index.html", "text/html");
            Get["/projetos/segurapreco"] = parameters => Response.AsFile("Content/projetos/segurapreco.html", "text/html");
            Post["/projetos/segurapreco"] = parameters => EnviarFormularioSeguraPreco();
        }

        private Negotiator EnviarFormularioSeguraPreco()
        {
            var formulario = this.Bind<RecebeFormularioDataTransferObject>();
            var dicionario = new ProjetoSeguraPrecoDictionary();

            try
            {
                ProjetoSeguraPrecoDictionary.Categoria? categoria = dicionario.Converter(formulario.Assunto);
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

