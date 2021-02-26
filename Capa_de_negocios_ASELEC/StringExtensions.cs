using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_de_datosASELEC;

namespace Capa_de_negocios_ASELEC
{
    public static class StringExtensions
    {
        public static string GetFilePath(this string EnvironmentCurrentDirectory, string FolderAndFileName)
        {
            //Split on path characters
            var CurrentDirectory =
                EnvironmentCurrentDirectory
                .Split("\\".ToArray())
                .ToList();
            //Get rid of bin/debug (last two folders)
            var CurrentDirectoryNoBinDebugFolder =
                CurrentDirectory
                .Take(CurrentDirectory.Count() - 2)
                .ToList();
            //Convert list above to array for Join
            var JoinableStringArray =
                CurrentDirectoryNoBinDebugFolder.ToArray();
            //Join and add folder filename passed in
            var RejoinedString =
                string.Join("\\", JoinableStringArray) + "\\";
            var final = RejoinedString + FolderAndFileName;
            return final;
        }
    }
}
