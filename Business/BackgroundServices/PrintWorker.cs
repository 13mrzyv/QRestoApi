using Business.Abstract.Print;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BackgroundServices
{
    public class PrintWorker : BackgroundService
    {
        private readonly IPrintQueue _printQueue;
        private readonly IServiceProvider _serviceProvider;

        public PrintWorker(IPrintQueue printQueue, IServiceProvider serviceProvider)
        {
            _printQueue = printQueue;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Proqram bağlanana qədər bu döngü dayanmır
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // 1. Növbədə sifariş varmı? Yoxdursa burada gözləyir (Bloklamır)
                    var request = await _printQueue.DequeuePrintJobAsync(stoppingToken);

                    // 2. Scoped servislərə (PrintPrepService) müraciət etmək üçün "Scope" yaradırıq
                    // Çünki BackgroundService "Singleton" kimidir, Scoped servisləri birbaşa qəbul edə bilməz
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var printPrepService = scope.ServiceProvider.GetRequiredService<IPrintPrepService>();

                        // 3. Çap prosesini başlat
                        await printPrepService.PrepareAndPrintKitchenTicketAsync(request);
                    }
                }
                catch (OperationCanceledException)
                {
                    // Proqram bağlanan zaman baş verir, normaldır.
                }
                catch (Exception ex)
                {
                    // Hər hansı çap xətası olsa, Worker-in dayanmaması üçün catch edirik
                    Console.WriteLine($"[PrintWorker] Xəta baş verdi: {ex.Message}");
                }
            }
        }
    }
}
