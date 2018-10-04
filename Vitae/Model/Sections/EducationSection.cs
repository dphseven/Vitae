namespace Vitae.Model
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Media;
    using Win = System.Windows.Documents;
    using Word = Microsoft.Office.Interop.Word;

    public class EducationSection : IResumeSection, IEducationSection
    {
        public Action AddToFlowDocument(IResumeDataObject rdo, IResumeFormatObject rfo, Win.FlowDocument flowDoc) 
        {
            return () =>
            {
                var list = new List<Win.Block>();

                if (rdo.EducationEntities != null && rdo.EducationEntities.Count > 0)
                {
                    Win.Paragraph edTitle = new Win.Paragraph(new Win.Run("Education"));

                    edTitle.Foreground = rfo.HeaderColorBrush;
                    edTitle.FontFamily = new FontFamily(rfo.HeaderFontName);
                    edTitle.FontSize = rfo.HeaderFontSizeWindows;
                    edTitle.Margin = rfo.HeaderMargin;

                    list.Add(edTitle);

                    foreach (var item in rdo.EducationEntities)
                    {
                        Win.Paragraph pItem = new Win.Paragraph();
                        pItem.Inlines.Add(new Win.Run(item.Credential + ", " + item.Institution));

                        pItem.FontFamily = new FontFamily(rfo.BodyFontName);
                        pItem.FontSize = rfo.BodyFontSizeWindows;

                        list.Add(pItem);
                    }
                }

                foreach (var item in list)
                {
                    flowDoc.Blocks.Add(item);
                }
            };
        }

        public Action AddToWordDocument(IResumeDataObject rdo, IResumeFormatObject rfo, Word.Document wordDoc) 
        {
            return () =>
            {
                // Education label
                Word.Paragraph educationLabelPara = wordDoc.Content.Paragraphs.Add();
                Word.Range r6label = educationLabelPara.Range;
                r6label.Text = "Education";
                r6label.InsertParagraphAfter();
                r6label.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                r6label.Font.Name = rfo.HeaderFontName;
                r6label.Font.Color = rfo.HeaderColorWord;
                r6label.Font.Size = rfo.HeaderFontSize;

                // Education items
                int numEduc = rdo.EducationEntities.Count;
                for (int i = 0; i < numEduc; i++)
                {
                    Word.Paragraph educParagraph = wordDoc.Content.Paragraphs.Add();
                    Word.Range r6 = educParagraph.Range;
                    r6.Text = rdo.EducationEntities[i].Credential + ", " + rdo.EducationEntities[i].Institution;
                    r6.InsertParagraphAfter();
                    r6.Font.Name = rfo.BodyFontName;
                    r6.Font.Size = rfo.BodyFontSize;
                    r6.ParagraphFormat.SpaceAfter = 8;
                    r6.ParagraphFormat.SpaceBefore = 8;
                }
            };
        }
    }
}