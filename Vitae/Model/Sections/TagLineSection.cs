namespace Vitae.Model
{
    using Word = Microsoft.Office.Interop.Word;
    using System;
    using Win = System.Windows.Documents;
    using System.Windows.Media;
    using System.Windows;

    public class TagLineSection : IResumeSection, ITagLineSection
    {
        public Action AddToFlowDocument(IResumeDataObject rdo, IResumeFormatObject rfo, Win.FlowDocument flowDoc) 
        {
            return () =>
            {
                Win.Paragraph para = new Win.Paragraph(new Win.Run(rdo.TagLine));

                para.Foreground = rfo.TagLineColorBrush;
                para.FontFamily = new FontFamily(rfo.TagLineFontName);
                para.FontSize = rfo.TagLineFontSizeWindows;
                para.TextAlignment = TextAlignment.Center;
                para.Margin = rfo.HeaderMargin;

                flowDoc.Blocks.Add(para);
            };
        }

        public Action AddToWordDocument(IResumeDataObject rdo, IResumeFormatObject rfo, Word.Document wordDoc) 
        {
            return () =>
            {
                if (rdo.TagLine != string.Empty)
                {
                    Word.Paragraph tagLinePara = wordDoc.Content.Paragraphs.Add();
                    Word.Range r3 = tagLinePara.Range;
                    r3.Text = rdo.TagLine;
                    r3.InsertParagraphAfter();
                    r3.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    r3.Font.Name = rfo.TagLineFontName;
                    r3.Font.Color = rfo.TagLineColorWord;
                    r3.Font.Size = rfo.TagLineFontSize;
                    r3.ParagraphFormat.SpaceAfter = 12;
                    r3.ParagraphFormat.SpaceBefore = 12;
                }
            };
        }
    }
}