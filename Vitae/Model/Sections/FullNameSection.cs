namespace Vitae.Model
{
    using Word = Microsoft.Office.Interop.Word;
    using System;
    using Win = System.Windows.Documents;
    using System.Windows.Media;
    using System.Windows;

    public class FullNameSection : IResumeSection, IFullNameSection
    {
        public Action AddToFlowDocument(IResumeDataObject rdo, IResumeFormatObject rfo, Win.FlowDocument flowDoc) 
        {
            return () =>
            {
                var para = new Win.Paragraph(new Win.Run(rdo.FullName));

                para.FontSize = rfo.NameFontSizeWindows;
                para.Foreground = rfo.NameColorBrush;
                para.TextAlignment = TextAlignment.Center;
                para.FontFamily = new FontFamily(rfo.NameFontName);
                para.Margin = rfo.HeaderMargin;

                flowDoc.Blocks.Add(para);
            };
        }

        public Action AddToWordDocument(IResumeDataObject rdo, IResumeFormatObject rfo, Word.Document wordDoc) 
        {
            return () =>
            {
                Word.Paragraph fullNamePara = wordDoc.Content.Paragraphs.Add();
                Word.Range r1 = fullNamePara.Range;
                r1.Text = rdo.FullName;
                r1.InsertParagraphAfter();
                r1.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                r1.Font.Name = rfo.NameFontName;
                r1.Font.Color = rfo.NameColorWord;
                r1.Font.Size = rfo.NameFontSize;
            };
        }
    }
}