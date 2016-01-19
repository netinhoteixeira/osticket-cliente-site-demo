
namespace osticketclientesitedemo.DataTransferObject
{
    /// <summary>
    /// Objeto de Transferência de Dados que Recebe do Formulário de Contato.
    /// </summary>
    public class RecebeFormularioContatoDataTransferObject
    {

        public string Nome { get; set; }

        public string Cpf { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public int? Assunto { get; set; }

        public string Mensagem { get; set; }
    }
}

