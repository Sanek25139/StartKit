using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.WPF.Tool
{
    public static class FileTool
    {
        public static FileInfo? SaveFile(string filter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = "C:\\";
            saveFileDialog.Filter = filter;
            saveFileDialog.Title = "Сохранить файл";

            if (saveFileDialog.ShowDialog() == true)
            {
                // Возвращаем объект FileInfo для выбранного файла
                return new FileInfo(saveFileDialog.FileName);
            }
            else
            {
                // Если пользователь не выбрал файл, возвращаем null или выбрасываем исключение
                return null; // Или можно выбросить исключение, если это необходимо
            }
        }
    }
}
