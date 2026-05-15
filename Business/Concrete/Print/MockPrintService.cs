using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Printing;
using Business.Abstract.Print;

namespace Business.Concrete.Print
{
    public class MockPrintService : IPrintService
    {
        public void PrintKitchenTicket(KitchenTicketModel ticket)
        {
            PrintDocument pd = new PrintDocument();

            // 1. Qovluq yolunu təyin edirik
            string folderPath = @"C:\Users\Polad\Desktop\pdQR\";

            // 2. Fayl adını unikal edirik (Məs: Sifaris_71_14052026.pdf)
            string fileName = $"Sifaris_{ticket.OrderNumber}_{DateTime.Now:HHmmss}.pdf";
            string fullPath = Path.Combine(folderPath, fileName);

            // Qovluq yoxdursa, yaradırıq
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            pd.PrintPage += (sender, ev) =>
            {
                // Sənin əvvəlki dizayn kodların burada olacaq
                Graphics g = ev.Graphics;
                Font font = new Font("Courier New", 12);
                float yPos = 10;

                g.DrawString($"MASA: {ticket.TableName}", new Font("Courier New", 14, FontStyle.Bold), Brushes.Black, 10, yPos);
                yPos += 30;
                g.DrawString($"Sifariş #{ticket.OrderNumber} - {ticket.OrderTime:HH:mm}", font, Brushes.Black, 10, yPos);
                yPos += 20;
                g.DrawString("----------------------------", font, Brushes.Black, 10, yPos);
                yPos += 20;

                foreach (var item in ticket.Items)
                {
                    g.DrawString($"{item.Quantity} x {item.Name}", font, Brushes.Black, 10, yPos);
                    yPos += 20;
                    if (!string.IsNullOrEmpty(item.Note))
                    {
                        g.DrawString($"  * {item.Note}", new Font("Courier New", 10, FontStyle.Italic), Brushes.Gray, 10, yPos);
                        yPos += 20;
                    }
                }
            };

            // 3. Əsas məqam: Printer pəncərəsini söndürürük və fayla çap edirik
            pd.PrinterSettings.PrinterName = "Microsoft Print to PDF";
            pd.PrinterSettings.PrintToFile = true;
            pd.PrinterSettings.PrintFileName = fullPath;

            pd.Print();
        }
    }
}
