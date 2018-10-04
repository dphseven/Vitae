using System.Windows;
using System.Windows.Media;
using Microsoft.Office.Interop.Word;

namespace Vitae.Model
{
    public class ResumeFormatObject : IResumeFormatObject
    {
        public WdColor NameColorWord { get; set; }
        public Brush NameColorBrush { get; set; }
        public string NameFontName { get; set; }
        public float NameFontSize { get; set; }
        public float NameFontSizeWindows 
        {
            get { return NameFontSize * 1.333F; }
        }

        public WdColor TagLineColorWord { get; set; }
        public Brush TagLineColorBrush { get; set; }
        public string TagLineFontName { get; set; }
        public float TagLineFontSize { get; set; }
        public float TagLineFontSizeWindows 
        {
            get { return TagLineFontSize * 1.333F; }
        }

        public WdColor HeaderColorWord { get; set; }
        public Brush HeaderColorBrush { get; set; }
        public string HeaderFontName { get; set; }
        public float HeaderFontSize { get; set; }
        public float HeaderFontSizeWindows 
        {
            get { return HeaderFontSize * 1.333F; }
        }
        public Thickness HeaderMargin { get; set; }

        public WdColor JobInfoColorWord { get; set; }
        public Brush JobInfoColorBrush { get; set; }
        public string JobInfoFontName { get; set; }
        public float JobInfoFontSize { get; set; }
        public float JobInfoFontSizeWindows 
        {
            get { return JobInfoFontSize * 1.333F; }
        }

        public WdColor CategoryColorWord { get; set; }
        public Brush CategoryColorBrush { get; set; }
        public string CategoryFontName { get; set; }
        public float CategoryFontSize { get; set; }
        public float CategoryFontSizeWindows 
        {
            get { return CategoryFontSize * 1.333F; }
        }

        public string BodyFontName { get; set; }
        public float BodyFontSize { get; set; }
        public float BodyFontSizeWindows 
        {
            get { return BodyFontSize * 1.333F; }
        }

        public ResumeFormatObject() 
        {
            WdColor niceBlueWord = (WdColor)14911564; // RGB is 76, 136, 227
            Brush niceBlueBrush = new SolidColorBrush(Color.FromRgb(76, 136, 227));

            NameColorWord = niceBlueWord;
            NameColorBrush = niceBlueBrush;
            NameFontName = "Segoe UI Light";
            NameFontSize = 24;

            TagLineColorWord = WdColor.wdColorBlack;
            TagLineColorBrush = Brushes.Black;
            TagLineFontName = "Segoe UI Light";
            TagLineFontSize = 16;

            HeaderColorWord = niceBlueWord;
            HeaderColorBrush = niceBlueBrush;
            HeaderFontName = "Segoe UI Light";
            HeaderFontSize = 16;
            HeaderMargin = new Thickness(0, 10, 0, 10);

            CategoryColorWord = WdColor.wdColorBlack;
            CategoryColorBrush = Brushes.Black;
            CategoryFontName = "Segoe UI Semibold";
            CategoryFontSize = 11;

            JobInfoColorWord = WdColor.wdColorBlack;
            JobInfoColorBrush = Brushes.Black;
            JobInfoFontName = "Segoe UI Semibold";
            JobInfoFontSize = 12;

            BodyFontName = "Segoe UI";
            BodyFontSize = 11;
        }
        
    }
}