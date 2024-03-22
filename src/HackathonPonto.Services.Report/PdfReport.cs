using System.ComponentModel;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Xml.Linq;
using HackathonPonto.Application.Interfaces;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using PdfSharp.Quality;
using PdfSharp.Snippets.Font;
using PdfSharp.UniversalAccessibility.Drawing;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HackathonPonto.Services.Report
{
    public class PdfReport:IReportService
    {
        public PdfReport() { }

        public PdfReport(PdfDocument document) { 
        }

        public void GerarDocumento(string arquivo, dynamic dados)
        {
            string[] DiaSemana = { "DOM", "SEG", "TER", "QUA", "QUI", "SEX", "SAB" };

            // NET6FIX - will be removed
            if (Capabilities.Build.IsCoreBuild && GlobalFontSettings.FontResolver is null)
                GlobalFontSettings.FontResolver = new FailsafeFontResolver();

            // Create a new PDF document.
            var document = new PdfDocument();
            document.Info.Title = "Hackathon FIAP Postech Software Architecture";
            document.Info.Subject = "Grupo 7 SOAT2 - Relatório de Ponto";
            
            // Create an empty page in this document.
            var page = document.AddPage();
            page.Size = PageSize.A4;

            // Get an XGraphics object for drawing on this page.
            var gfx = XGraphics.FromPdfPage(page);

            // Draw two lines with a red default pen.
            var width = page.Width;
            var height = page.Height;
            gfx.DrawLine(XPens.DarkBlue, 50, 45, width-50, 45);                        

            // Create a font.
            var font = new XFont("Times New Roman", 12, XFontStyleEx.Bold);

            // Draw the text.            
            XTextFormatter tf = new XTextFormatter(gfx);

            gfx.DrawString(document.Info.Title, font, XBrushes.DarkBlue, 50, 20);
            gfx.DrawString(document.Info.Subject, font, XBrushes.Blue, 50, 40);

            
            font = new XFont("Times New Roman", 10, XFontStyleEx.Bold);

            var linha = dados[0];
            var registros = linha as IDictionary<string, object>;

            gfx.DrawString($"Funcionário: {(string)registros["nome"]}", font, XBrushes.Black, 50 ,70);

            string cpf = (string)registros["cpf"];
            cpf = cpf.Insert(9, "-");
            cpf = cpf.Insert(6, ".");
            cpf = cpf.Insert(3, ".");

            gfx.DrawString($"CPF: {cpf}", font, XBrushes.Black,width-250, 70);
            gfx.DrawLine(XPens.Gray, 50, 75, width - 50, 75);

            gfx.DrawString("DATA", font, XBrushes.Black, 50, 90);
            gfx.DrawString("DIA", font, XBrushes.Black, 150, 90);
            gfx.DrawString("E1", font, XBrushes.Black, 200, 90);
            gfx.DrawString("S1", font, XBrushes.Black, 250, 90);
            gfx.DrawString("E2", font, XBrushes.Black, 300, 90);
            gfx.DrawString("S2", font, XBrushes.Black, 350, 90);
            gfx.DrawString("HORAS", font, XBrushes.Black, 420, 90);
            gfx.DrawString("INTERVALO", font, XBrushes.Black, 480, 90);


            font = new XFont("Times New Roman", 10, XFontStyleEx.Regular);            

            var currentLine = 105;

            foreach (var rows in dados)
            {
                var fields = rows as IDictionary<string, object>;

                //string reg = DateTime.Parse(fields["dia"].ToString()).ToShortDateString();
                string reg = (string)fields["data"];
                gfx.DrawString(reg, font, XBrushes.Black, 50, currentLine);

                reg = DiaSemana[(int)DateTime.Parse(fields["data"].ToString()).DayOfWeek];
                gfx.DrawString(reg, font, XBrushes.Black, 150, currentLine);

                reg = fields["entrada1"] is null? "" : DateTime.Parse(fields["entrada1"].ToString()).ToString("hh:mm:ss");
                gfx.DrawString(reg, font, XBrushes.Black, 200, currentLine);

                reg = fields["saida1"] is null ? "" : DateTime.Parse(fields["saida1"].ToString()).ToString("hh:mm:ss");
                gfx.DrawString(reg, font, XBrushes.Black, 250, currentLine);

                reg = fields["entrada2"] is null ? "" : DateTime.Parse(fields["entrada2"].ToString()).ToString("hh:mm:ss");
                gfx.DrawString(reg, font, XBrushes.Black, 300, currentLine);
                
                reg = fields["saida2"] is null ? "" : DateTime.Parse(fields["saida2"].ToString()).ToString("hh:mm:ss");
                gfx.DrawString(reg, font, XBrushes.Black, 350, currentLine);

                reg = fields["trabalhadas"] is null ? "" : DateTime.Parse(fields["trabalhadas"].ToString()).ToString("hh:mm:ss");
                gfx.DrawString(reg, font, XBrushes.Black, 420, currentLine);

                reg = fields["intervalo"] is null ? "" : DateTime.Parse(fields["intervalo"].ToString()).ToString("hh:mm:ss");
                gfx.DrawString(reg, font, XBrushes.Black, 480, currentLine);
                                
                currentLine += 15;
            }

            

            // Save the document...
            document.Save(arquivo);
            document.Close();
            document.Dispose();
        }
    }
}
