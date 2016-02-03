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
            Get["/projetos/caixamaisvantagens"] = parameters => Response.AsFile("Content/projetos/caixamaisvantagens.html", "text/html");
            Post["/projetos/caixamaisvantagens"] = parameters => EnviarFormularioContatoCaixaMaisVantagens();
            Get["/projetos/maisdesconto"] = parameters => Response.AsFile("Content/projetos/maisdesconto.html", "text/html");
            Post["/projetos/maisdesconto"] = parameters => EnviarFormularioContatoMaisDesconto();
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
                var categoria = dicionario.ObterCategoria(formulario.Assunto);

                // Envia os dados do formulário de contato
                var ticket = OsTicketService.Submeter(
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

        /// <summary>
        /// Envia o formulário de contato do projeto Caixa Mais Vantagens.
        /// </summary>
        /// <returns>O resultado do envio do formulário de contato.</returns>
        private Negotiator EnviarFormularioContatoCaixaMaisVantagens()
        {
            var formulario = this.Bind<RecebeFormularioContatoDataTransferObject>();
            var dicionario = new ProjetoCaixaMaisVantagensDictionary();

            try
            {
                // Seleciona a categoria de acordo com o seu dicionário
                var categoria = dicionario.ObterCategoria(formulario.Assunto);

                // Envia os dados do formulário de contato
                var ticket = OsTicketService.Submeter(
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

        /// <summary>
        /// Envia o formulário de contato do projeto Segura Preço.
        /// </summary>
        /// <returns>O resultado do envio do formulário de contato.</returns>
        private Negotiator EnviarFormularioContatoMaisDesconto()
        {
            var formulario = this.Bind<RecebeFormularioContatoDataTransferObject>();
            var dicionario = new ProjetoMaisDescontoDictionary();

            try
            {
                // Seleciona a categoria de acordo com o seu dicionário
                var categoria = dicionario.ObterCategoria(formulario.Assunto);

                // Envia os dados do formulário de contato
                var ticket = OsTicketService.Submeter(
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

