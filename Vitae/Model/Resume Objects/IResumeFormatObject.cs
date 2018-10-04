using System.Windows;
using System.Windows.Media;
using Microsoft.Office.Interop.Word;

namespace Vitae.Model
{
    public interface IResumeFormatObject
    {
        WdColor NameColorWord { get; set; }
        Brush NameColorBrush { get; set; }
        string NameFontName { get; set; }
        float NameFontSize { get; set; }
        float NameFontSizeWindows { get; }

        WdColor TagLineColorWord { get; set; }
        Brush TagLineColorBrush { get; set; }
        string TagLineFontName { get; set; }
        float TagLineFontSize { get; set; }
        float TagLineFontSizeWindows { get; }

        WdColor HeaderColorWord { get; set; }
        Brush HeaderColorBrush { get; set; }
        string HeaderFontName { get; set; }
        float HeaderFontSize { get; set; }
        float HeaderFontSizeWindows { get; }
        Thickness HeaderMargin { get; set; }

        WdColor JobInfoColorWord { get; set; }
        Brush JobInfoColorBrush { get; set; }
        string JobInfoFontName { get; set; }
        float JobInfoFontSize { get; set; }
        float JobInfoFontSizeWindows { get; }

        WdColor CategoryColorWord { get; set; }
        Brush CategoryColorBrush { get; set; }
        string CategoryFontName { get; set; }
        float CategoryFontSize { get; set; }
        float CategoryFontSizeWindows { get; }

        string BodyFontName { get; set; }
        float BodyFontSize { get; set; }
        float BodyFontSizeWindows { get; }
    }
}