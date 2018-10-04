namespace Vitae.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Media;
    using Win = System.Windows.Documents;
    using Word = Microsoft.Office.Interop.Word;

    public class ExpertiseSection : IResumeSection, IExpertiseSection
    {
        private char itemSeparator = '\u2022';

        public Action AddToFlowDocument(IResumeDataObject rdo, IResumeFormatObject rfo, Win.FlowDocument flowDoc) 
        {
            return () => 
            {
                var listOfCats = new List<Win.Paragraph>();

                // Add the "Expertise" legend
                if (rdo.ExpertiseEntities.Count > 0)
                {
                    Win.Paragraph header = new Win.Paragraph();
                    header.Inlines.Add("Expertise");

                    header.FontFamily = new FontFamily(rfo.HeaderFontName);
                    header.FontSize = rfo.HeaderFontSizeWindows;
                    header.Foreground = rfo.HeaderColorBrush;
                    header.Margin = rfo.HeaderMargin;

                    listOfCats.Add(header);
                }

                List<string> cats = rdo.ExpertiseEntities.Select(T => T.Category).Distinct().ToList();
                for (int i = 0; i < cats.Count; i++)
                {
                    Win.Paragraph p = new Win.Paragraph();
                    p.Inlines.Add(new Win.Bold(new Win.Run(cats[i] + ": ")));
                    List<string> expItems = rdo.ExpertiseEntities.Where(T => T.Category == cats[i]).Select(T => T.Expertise).ToList();
                    string s = string.Empty;
                    for (int j = 0; j < expItems.Count; j++)
                    {
                        s += (expItems[j] + " " + itemSeparator + " ");
                    }
                    s = s.Remove(s.Length - 3);
                    p.Inlines.Add(s);
                    p.FontFamily = new FontFamily(rfo.BodyFontName);
                    p.FontSize = rfo.BodyFontSizeWindows;
                    listOfCats.Add(p);
                }

                foreach (var item in listOfCats)
                {
                    flowDoc.Blocks.Add(item);
                }
            };
        }

        public Action AddToWordDocument(IResumeDataObject rdo, IResumeFormatObject rfo, Word.Document wordDoc) 
        {
            return () =>
            {
                if (rdo.ExpertiseEntities.Count > 0)
                {
                    // Expertise label
                    Word.Paragraph tagLineLabelPara = wordDoc.Content.Paragraphs.Add();
                    Word.Range r4label = tagLineLabelPara.Range;
                    r4label.Text = "Expertise";
                    r4label.InsertParagraphAfter();
                    r4label.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    r4label.Font.Name = rfo.HeaderFontName;
                    r4label.Font.Color = rfo.HeaderColorWord;
                    r4label.Font.Size = rfo.HeaderFontSize;

                    // Expertise item-headers
                    List<string> cats = rdo.ExpertiseEntities.Select(T => T.Category).Distinct().ToList();
                    for (int i = 0; i < cats.Count; i++)
                    {
                        Word.Paragraph p = wordDoc.Content.Paragraphs.Add();
                        Word.Range r4 = p.Range;
                        r4.Text = cats[i] + ": ";
                        int titleLength = r4.Text.Length;

                        // Insert expertise(s)
                        List<string> exps = rdo.ExpertiseEntities.Where(T => T.Category == cats[i]).Select(T => T.Expertise).ToList();
                        for (int j = 0; j < exps.Count; j++)
                        {
                            r4.Font.Name = rfo.BodyFontName;
                            r4.Font.Size = rfo.BodyFontSize;
                            r4.InsertAfter(exps[j] + " " + '\u2022' + " ");
                        }
                        r4.Text = r4.Text.Remove(r4.Text.Length - 2);

                        Word.Range titleRange = r4.Duplicate;
                        titleRange.MoveEnd(Word.WdUnits.wdCharacter, titleLength - 1 - titleRange.Characters.Count);
                        titleRange.MoveStart(Word.WdUnits.wdCharacter, 0);
                        titleRange.Font.Name = rfo.CategoryFontName;
                        titleRange.Font.Size = rfo.CategoryFontSize;

                        r4.InsertParagraphAfter();
                    }
                }
            };
        }
    }
}