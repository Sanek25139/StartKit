using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace StarterKit.WPF.Interface
{
    public interface IPageControl
    {
        Task OpenPageAsync(Type pageType);
        Task OpenPage(Type pageType);
    }
}
