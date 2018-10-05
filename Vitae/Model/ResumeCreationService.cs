namespace Vitae
{
    using Microsoft.Office.Interop.Word;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Media;

    public class ResumeCreationServiceXXX
    {
        private WdColor headerColor;

        private ResumeDataObject rdo;
        private ResumeFormatObject rfo;
        private Document document;

        public ResumeCreationService() 
        {
            setUpFormatting();
        }

        public void CreateResumeAsWordFile(ResumeDataObject resumeDataObject, string filePathAndName) 
        {
            rdo = resumeDataObject;

            Application app = new Application();
            document = app.Documents.Add();

            addFullName();
            addAddressPhoneEmail();
            addTagLine();
            addExpertise();
            addExperience();
            addEducation();
            addPublications();

            saveDocument(filePathAndName);

            document.Close();
            document = null;
            app.Quit();
            app = null;
        }

        private void addPublications() 
        {
            if (rdo.PublicationEntities.Count > 0)
            {
                // Publications label
                Paragraph pubsLabelPara = document.Content.Paragraphs.Add();
                Range r7label = pubsLabelPara.Range;
                r7label.Text = "Selected Publications";
                r7label.InsertParagraphAfter();
                r7label.Font.Name = "Segoe UI Light";
                r7label.Font.Color = headerColor;
                r7label.Font.Size = 16;

                // Publication items
                int numPub = rdo.PublicationEntities.Count;
                for (int i = 0; i < numPub; i++)
                {
                    Paragraph pubParagraph = document.Content.Paragraphs.Add();
                    Range r7 = pubParagraph.Range;
                    r7.Text = rdo.PublicationEntities[i].Publication;
                    r7.InsertParagraphAfter();
                    r7.Font.Name = "Segoe UI";
                    r7.Font.Size = 11;
                    r7.ParagraphFormat.SpaceAfter = 8;
                    r7.ParagraphFormat.SpaceBefore = 8;
                }
            }
        }

        private void addEducation() 
        {
            // Education label
            Paragraph educationLabelPara = document.Content.Paragraphs.Add();
            Range r6label = educationLabelPara.Range;
            r6label.Text = "Education";
            r6label.InsertParagraphAfter();
            r6label.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            r6label.Font.Name = "Segoe UI Light";
            r6label.Font.Color = headerColor;
            r6label.Font.Size = 16;

            // Education items
            int numEduc = rdo.EducationEntities.Count;
            for (int i = 0; i < numEduc; i++)
            {
                Paragraph educParagraph = document.Content.Paragraphs.Add();
                Range r6 = educParagraph.Range;
                r6.Text = rdo.EducationEntities[i].Credential + ", " + rdo.EducationEntities[i].Institution;
                r6.InsertParagraphAfter();
                r6.Font.Name = "Segoe UI";
                r6.Font.Size = 11;
                r6.ParagraphFormat.SpaceAfter = 8;
                r6.ParagraphFormat.SpaceBefore = 8;
            }
        }

        private void addExperience() 
        {
            // Experience label
            Paragraph experienceLabelPara = document.Content.Paragraphs.Add();
            Range r5label = experienceLabelPara.Range;
            r5label.Text = "Experience";
            r5label.InsertParagraphAfter();
            r5label.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            r5label.Font.Name = "Segoe UI Light";
            r5label.Font.Color = headerColor;
            r5label.Font.Size = 16;

            // Experience items
            int numJobs = rdo.ExperienceEntities.Count;
            for (int i = 0; i < numJobs; i++)
            {
                Paragraph experienceParagraph = document.Content.Paragraphs.Add();
                Range r5 = experienceParagraph.Range;
                r5.Text = rdo.ExperienceEntities[i].Employer + Environment.NewLine +
                          rdo.ExperienceEntities[i].Titles[0] + Environment.NewLine +
                          rdo.ExperienceEntities[i].StartDate + " - " +
                          rdo.ExperienceEntities[i].EndDate;
                r5.Font.Name = "Segoe UI Semibold";
                r5.Font.Color = WdColor.wdColorBlack;
                r5.Font.Size = 12;
                r5.ParagraphFormat.SpaceAfter = 0;
                r5.ParagraphFormat.SpaceBefore = 0;

                // Add experience details
                int numDetails = rdo.ExperienceEntities[i].Details.Count;
                Paragraph detPara = document.Content.Paragraphs.Add();
                Range r = detPara.Range;
                for (int j = 0; j < numDetails; j++)
                {
                    Paragraph expItemPara = document.Content.Paragraphs.Add();
                    Range r5Item = expItemPara.Range;
                    r5Item.Text = rdo.ExperienceEntities[i].Details[j];
                    r5Item.InsertParagraphAfter();
                    r5Item.Font.Name = "Segoe UI";
                    r5Item.Font.Size = 11;
                    r5Item.ParagraphFormat.LeftIndent = 20.0F;
                    r5Item.ParagraphFormat.SpaceAfter = 8;
                    r5Item.ParagraphFormat.SpaceBefore = 8;
                }
            }
        }

        private void addExpertise() 
        {
            if (rdo.ExpertiseEntities.Count > 0)
            {
                // Expertise label
                Paragraph tagLineLabelPara = document.Content.Paragraphs.Add();
                Range r4label = tagLineLabelPara.Range;
                r4label.Text = "Expertise";
                r4label.InsertParagraphAfter();
                r4label.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                r4label.Font.Name = "Segoe UI Light";
                r4label.Font.Color = headerColor;
                r4label.Font.Size = 16;

                // Expertise item-headers
                List<string> cats = rdo.ExpertiseEntities.Select(T => T.Category).Distinct().ToList();
                for (int i = 0; i < cats.Count; i++)
                {
                    Paragraph p = document.Content.Paragraphs.Add();
                    Range r4 = p.Range;
                    r4.Text = cats[i] + ": ";
                    int titleLength = r4.Text.Length;

                    // Insert expertise(s)
                    List<string> exps = rdo.ExpertiseEntities.Where(T => T.Category == cats[i]).Select(T => T.Expertise).ToList();
                    for (int j = 0; j < exps.Count; j++)
                    {
                        r4.Font.Name = "Segoe UI";
                        r4.Font.Size = 11;
                        r4.InsertAfter(exps[j] + " " + '\u2022' + " ");
                    }
                    r4.Text = r4.Text.Remove(r4.Text.Length - 2);

                    Range titleRange = r4.Duplicate;
                    titleRange.MoveEnd(WdUnits.wdCharacter, titleLength - 1 - titleRange.Characters.Count);
                    titleRange.MoveStart(WdUnits.wdCharacter, 0);
                    titleRange.Font.Name = "Segoe WP Semibold";

                    r4.InsertParagraphAfter();
                }
            }
        }

        private void addTagLine() 
        {
            if (rdo.TagLine != string.Empty)
            {
                Paragraph tagLinePara = document.Content.Paragraphs.Add();
                Range r3 = tagLinePara.Range;
                r3.Text = rdo.TagLine;
                r3.InsertParagraphAfter();
                r3.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                r3.Font.Name = "Segoe UI Light";
                r3.Font.Color = headerColor;
                r3.Font.Size = 16;
                r3.Font.Color = WdColor.wdColorBlack;
                r3.ParagraphFormat.SpaceAfter = 12;
                r3.ParagraphFormat.SpaceBefore = 12;
            }
        }

        private void addAddressPhoneEmail() 
        {
            Paragraph phoneAndEmailPara = document.Content.Paragraphs.Add();
            Table firstTable = document.Tables.Add(phoneAndEmailPara.Range, 2, 2);
            firstTable.Rows[1].Cells[1].Range.Text = rdo.AddressLine1;
            firstTable.Rows[2].Cells[1].Range.Text = rdo.AddressLine2;
            firstTable.Rows[1].Cells[2].Range.Text = rdo.PhoneNumber;
            firstTable.Rows[2].Cells[2].Range.Text = rdo.Email;
            firstTable.Rows[1].Cells[2].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
            firstTable.Rows[2].Cells[2].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
            firstTable.Range.Font.Name = "Segoe UI";
            firstTable.Range.Font.Size = 11;
            firstTable.Range.ParagraphFormat.SpaceAfter = 0;
            firstTable.Range.ParagraphFormat.SpaceBefore = 0;
            firstTable.Borders.Enable = 0;
        }

        private void addFullName() 
        {
            Paragraph fullNamePara = document.Content.Paragraphs.Add();
            Range r1 = fullNamePara.Range;
            r1.Text = rdo.FullName;
            r1.InsertParagraphAfter();
            r1.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            r1.Font.Name = "Segoe UI Light";
            r1.Font.Color = headerColor;
            r1.Font.Size = 24;
        }

        private void saveDocument(string filePathAndName) 
        {
            document.SaveAs2(FileName: filePathAndName);
        }

        private void setUpFormatting() 
        {
            Color niceBlue = Color.FromRgb(76, 136, 227);
            headerColor = (WdColor)(niceBlue.R + 0x100 * niceBlue.G + 0x10000 * niceBlue.B);
        }
    }

}