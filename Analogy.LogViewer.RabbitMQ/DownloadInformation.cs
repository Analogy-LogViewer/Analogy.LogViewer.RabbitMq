using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using Analogy.LogViewer.Template;

namespace Analogy.LogViewer.RabbitMq
{
    public class DownloadInformation : Analogy.LogViewer.Template.AnalogyDownloadInformation
    {
        protected override string RepositoryURL { get; set; } = "https://api.github.com/repos/Analogy-LogViewer/Analogy.LogViewer.RabbitMq";
        public override TargetFrameworkAttribute CurrentFrameworkAttribute { get; set; } = (TargetFrameworkAttribute)Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(TargetFrameworkAttribute));

        public override Guid FactoryId { get; set; } = RabbitMqFactory.Id;
        public override string Name { get; set; } = "RabbitMq Data Provider";

        private string? _installedVersionNumber;
        public override string InstalledVersionNumber
        {
            get
            {
                if (_installedVersionNumber != null)
                {
                    return _installedVersionNumber;
                }
                Assembly assembly = Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                _installedVersionNumber = fvi.FileVersion;
                return _installedVersionNumber;
            }
        }
    }
}
