using NewWPF.UI.i18N;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewWPF.Data
{
    public class Setup
    {
        public Setup()
        {
            ResourceDirectories = new List<string>
            {
                @$"{Settings.CurrentDirectory}\Folder1",
                @$"{Settings.CurrentDirectory}\Folder2"
            };

            CheckResourcesFolder();
            SetupLanguage();
        }

        public List<string> ResourceDirectories { get; set; }

        public void CheckResourcesFolder()
        {
            if (ResourceDirectories.Count > 0)
            {
                foreach (var dir in ResourceDirectories)
                {
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                }
            }
        }

        public void SetupLanguage()
        {
            i18N.LoadCurrentLanguage();
            //i18N.SwitchCurrentLanguage("tr-TR");
        }
    }
}