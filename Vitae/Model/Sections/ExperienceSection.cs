namespace Vitae.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;
    using Win = System.Windows.Documents;
    using Word = Microsoft.Office.Interop.Word;

    public class ExperienceSection : IResumeSection, IExperienceSection
    {
        private Thickness bulletMargin = new Thickness(5, 5, 5, 5);

        public Action AddToFlowDocument(IResumeDataObject rdo, IResumeFormatObject rfo, Win.FlowDocument flowDoc) 
        {
            return () =>
            {
                var list = new List<Win.Block>();

                // "Experience" header
                Win.Paragraph expTitlePara = new Win.Paragraph();
                expTitlePara.Inlines.Add(new Win.Run("Experience"));

                expTitlePara.Margin = new System.Windows.Thickness(0, 20, 0, 20);
                expTitlePara.Foreground = rfo.HeaderColorBrush;
                expTitlePara.FontFamily = new FontFamily(rfo.HeaderFontName);
                expTitlePara.FontSize = rfo.HeaderFontSizeWindows;
                expTitlePara.Margin = rfo.HeaderMargin;

                list.Add(expTitlePara);

                for (int i = 0; i < rdo.ExperienceEntities.Count; i++)
                {
                    var expItem = rdo.ExperienceEntities[i];

                    // Employer Name
                    Win.Paragraph empName = new Win.Paragraph(new Win.Run(expItem.Employer));
                    empName.Foreground = rfo.JobInfoColorBrush;
                    empName.FontFamily = new FontFamily(rfo.JobInfoFontName);
                    empName.FontSize = rfo.JobInfoFontSizeWindows;
                    list.Add(empName);

                    // Title
                    Win.Paragraph title = new Win.Paragraph(new Win.Run(expItem.Titles.FirstOrDefault()));
                    title.Foreground = rfo.JobInfoColorBrush;
                    title.FontFamily = new FontFamily(rfo.JobInfoFontName);
                    title.FontSize = rfo.JobInfoFontSizeWindows;
                    list.Add(title);

                    // Dates
                    Win.Paragraph dates = new Win.Paragraph(new Win.Run(expItem.StartDate + " - " + expItem.EndDate));
                    dates.Foreground = rfo.JobInfoColorBrush;
                    dates.FontFamily = new FontFamily(rfo.JobInfoFontName);
                    dates.FontSize = rfo.JobInfoFontSizeWindows;
                    list.Add(dates);

                    // List of experience items
                    Win.List bullets = new Win.List();
                    for (int j = 0; j < rdo.ExperienceEntities[i].Details.Count; j++)
                    {
                        var item = new Win.ListItem(new Win.Paragraph(new Win.Run(rdo.ExperienceEntities[i].Details[j])));
                        bullets.ListItems.Add(item);
                        item.FontFamily = new FontFamily(rfo.BodyFontName);
                        item.FontSize = rfo.BodyFontSizeWindows;
                        bullets.Margin = bulletMargin;
                        list.Add(bullets);
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
                // Experience label
                Word.Paragraph experienceLabelPara = wordDoc.Content.Paragraphs.Add();
                Word.Range r5label = experienceLabelPara.Range;
                r5label.Text = "Experience";
                r5label.InsertParagraphAfter();
                r5label.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                r5label.Font.Name = rfo.HeaderFontName;
                r5label.Font.Color = rfo.HeaderColorWord;
                r5label.Font.Size = rfo.HeaderFontSize;

                // Experience items
                int numJobs = rdo.ExperienceEntities.Count;
                for (int i = 0; i < numJobs; i++)
                {
                    // Job metadata
                    Word.Paragraph experienceParagraph = wordDoc.Content.Paragraphs.Add();
                    Word.Range r5 = experienceParagraph.Range;
                    r5.Text = rdo.ExperienceEntities[i].Employer + Environment.NewLine +
                              rdo.ExperienceEntities[i].Titles[0] + Environment.NewLine +
                              rdo.ExperienceEntities[i].StartDate + " - " +
                              rdo.ExperienceEntities[i].EndDate;
                    r5.Font.Name = rfo.JobInfoFontName;
                    r5.Font.Color = rfo.JobInfoColorWord;
                    r5.Font.Size = rfo.JobInfoFontSize;
                    r5.ParagraphFormat.SpaceAfter = 0;
                    r5.ParagraphFormat.SpaceBefore = 0;

                    // Add experience details
                    int numDetails = rdo.ExperienceEntities[i].Details.Count;
                    Word.Paragraph detPara = wordDoc.Content.Paragraphs.Add();
                    Word.Range r = detPara.Range;
                    for (int j = 0; j < numDetails; j++)
                    {
                        Word.Paragraph expItemPara = wordDoc.Content.Paragraphs.Add();
                        Word.Range r5Item = expItemPara.Range;
                        r5Item.Text = rdo.ExperienceEntities[i].Details[j];
                        r5Item.InsertParagraphAfter();
                        r5Item.Font.Name = rfo.BodyFontName;
                        r5Item.Font.Size = rfo.BodyFontSize;
                        r5Item.ParagraphFormat.LeftIndent = 20.0F;
                        r5Item.ParagraphFormat.SpaceAfter = 8;
                        r5Item.ParagraphFormat.SpaceBefore = 8;
                    }
                }
            };
        }
    }
}