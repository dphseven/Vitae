namespace Vitae.Model
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using Win = System.Windows.Documents;
    using Word = Microsoft.Office.Interop.Word;

    public class BasicInfoSection : IResumeSection, IBasicInfoSection
    {
        public Action AddToFlowDocument(IResumeDataObject rdo, IResumeFormatObject rfo, Win.FlowDocument flowDoc) 
        {
            return () =>
            {
                var table = new Win.Table();
                table.RowGroups.Add(new Win.TableRowGroup());
                var rg = table.RowGroups[0];
                rg.Rows.Add(new Win.TableRow());
                rg.Rows.Add(new Win.TableRow());

                Win.TableCell add1Cell = new Win.TableCell(new Win.Paragraph(new Win.Run(rdo.AddressLine1)));
                Win.TableCell add2Cell = new Win.TableCell(new Win.Paragraph(new Win.Run(rdo.AddressLine2)));
                Win.TableCell phoneCell = new Win.TableCell(new Win.Paragraph(new Win.Run(rdo.Email)));
                Win.TableCell emailCell = new Win.TableCell(new Win.Paragraph(new Win.Run(rdo.PhoneNumber)));

                rg.Rows[0].Cells.Add(add1Cell);
                rg.Rows[1].Cells.Add(add2Cell);
                rg.Rows[0].Cells.Add(emailCell);
                rg.Rows[1].Cells.Add(phoneCell);

                add1Cell.TextAlignment = TextAlignment.Left;
                add2Cell.TextAlignment = TextAlignment.Left;
                phoneCell.TextAlignment = TextAlignment.Right;
                emailCell.TextAlignment = TextAlignment.Right;

                foreach (var row in rg.Rows) 
                {
                    foreach (var cell in row.Cells)
                    {
                        cell.FontFamily = new FontFamily(rfo.BodyFontName);
                        cell.FontSize = rfo.BodyFontSizeWindows;
                    }
                }

                flowDoc.Blocks.Add(table);
            };
        }

        public Action AddToWordDocument(IResumeDataObject rdo, IResumeFormatObject rfo, Word.Document wordDoc) 
        {
            return () =>
            {
                Word.Paragraph basicInfoPara = wordDoc.Content.Paragraphs.Add();
                Word.Table firstTable = wordDoc.Tables.Add(basicInfoPara.Range, 2, 2);
                firstTable.Rows[1].Cells[1].Range.Text = rdo.AddressLine1;
                firstTable.Rows[2].Cells[1].Range.Text = rdo.AddressLine2;
                firstTable.Rows[1].Cells[2].Range.Text = rdo.PhoneNumber;
                firstTable.Rows[2].Cells[2].Range.Text = rdo.Email;
                firstTable.Rows[1].Cells[2].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                firstTable.Rows[2].Cells[2].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                firstTable.Range.Font.Name = rfo.BodyFontName;
                firstTable.Range.Font.Size = rfo.BodyFontSize;
                firstTable.Range.ParagraphFormat.SpaceAfter = 0;
                firstTable.Range.ParagraphFormat.SpaceBefore = 0;
                firstTable.Borders.Enable = 0;
            };
        }

    }
}