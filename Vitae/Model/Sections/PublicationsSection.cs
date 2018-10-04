namespace Vitae.Model
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Media;
    using Win = System.Windows.Documents;
    using Word = Microsoft.Office.Interop.Word;

    public class PublicationsSection : IResumeSection, IPublicationsSection
    {
        public Action AddToFlowDocument(IResumeDataObject rdo, IResumeFormatObject rfo, Win.FlowDocument flowDoc) 
        {
            return () =>
            {
                var list = new List<Win.Block>();

                if (rdo.PublicationEntities != null && rdo.PublicationEntities.Count > 0)
                {
                    Win.Paragraph pubTitle = new Win.Paragraph(new Win.Run("Publications"));
                    pubTitle.Foreground = rfo.HeaderColorBrush;
                    pubTitle.FontFamily = new FontFamily(rfo.HeaderFontName);
                    pubTitle.FontSize = rfo.HeaderFontSizeWindows;
                    pubTitle.Margin = rfo.HeaderMargin;
                    list.Add(pubTitle);

                    foreach (var item in rdo.PublicationEntities)
                    {
                        Win.Paragraph pItem = new Win.Paragraph();
                        pItem.Inlines.Add(new Win.Run(item.Publication));
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
                if (rdo.PublicationEntities.Count > 0)
                {
                    // Publications label
                    Word.Paragraph pubsLabelPara = wordDoc.Content.Paragraphs.Add();
                    Word.Range r7label = pubsLabelPara.Range;
                    r7label.Text = "Selected Publications";
                    r7label.InsertParagraphAfter();
                    r7label.Font.Name = rfo.HeaderFontName;
                    r7label.Font.Color = rfo.HeaderColorWord;
                    r7label.Font.Size = rfo.HeaderFontSize;

                    // Publication items
                    int numPub = rdo.PublicationEntities.Count;
                    for (int i = 0; i < numPub; i++)
                    {
                        Word.Paragraph pubParagraph = wordDoc.Content.Paragraphs.Add();
                        Word.Range r7 = pubParagraph.Range;
                        r7.Text = rdo.PublicationEntities[i].Publication;
                        r7.InsertParagraphAfter();
                        r7.Font.Name = rfo.BodyFontName;
                        r7.Font.Size = rfo.BodyFontSize;
                        r7.ParagraphFormat.SpaceAfter = 8;
                        r7.ParagraphFormat.SpaceBefore = 8;
                    }
                }
            };
        }
    }
}