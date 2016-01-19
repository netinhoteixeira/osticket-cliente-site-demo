using System;
using System.IO;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Diagnostics;
using Nancy.TinyIoc;

namespace osticketclientesitedemo
{
    public class Bootstrapper: DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            StaticConfiguration.DisableErrorTraces = false;
        }

        protected override DiagnosticsConfiguration DiagnosticsConfiguration
        {
            get
            {
                return new DiagnosticsConfiguration
                {
                    Password = @"WwX5Ms3v"
                };
            }
        }

        protected override IRootPathProvider RootPathProvider
        {
            get
            {
                return new PathProvider();
            }
        }
    }

    public class PathProvider : IRootPathProvider
    {
        public string GetRootPath()
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var result = Path.GetFullPath(Path.Combine(currentDirectory, "..", ".."));

            return result;
        }
    }
}

