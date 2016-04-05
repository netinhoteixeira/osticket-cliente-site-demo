using System;
using Mono.Unix;
using Mono.Unix.Native;
using Nancy.Hosting.Self;

namespace osticketclientesitedemo
{
    class Program
    {
        public static void Main(string[] args)
        {
            const string uri = "http://localhost:8080";
            Console.WriteLine("Iniciando o Nancy em " + uri);

            // Inicializando uma instância do NancyHost
            var host = new NancyHost(new Uri(uri));
            // Inicia a hospedagem
            host.Start();

            // Verifica se está rodando o Mono
            if (Type.GetType("Mono.Runtime") != null)
            {
                // No Mono, os processos irão geralmente executar como serviços
                // - isso permite escutar sinais de término (CTRL + C, shutdown, etc)
                // e finaliza corretamente
                UnixSignal.WaitAny(new[]
                    {
                        new UnixSignal(Signum.SIGINT),
                        new UnixSignal(Signum.SIGTERM),
                        new UnixSignal(Signum.SIGQUIT),
                        new UnixSignal(Signum.SIGHUP)
                    });
            }
            else
            {
                Console.ReadLine();
            }

            Console.WriteLine("Parando o Nancy");
            // Termina a hospedagem
            host.Stop();
        }
    }
}
