using Business.Abstract.Print;
using Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Business.Concrete.Print
{
    public class PrintQueue : IPrintQueue
    {
        // Channel - çox sürətli və eyni anda bir neçə işin toqquşmasının qarşısını alan növbə növüdür
        private readonly Channel<CreateOrderRequest> _queue;

        public PrintQueue()
        {
            // Unbounded - yəni növbənin limiti yoxdur, nə qədər sifariş gəlsə qəbul edir
            _queue = Channel.CreateUnbounded<CreateOrderRequest>();
        }

        public async ValueTask EnqueuePrintJobAsync(CreateOrderRequest request)
        {
            await _queue.Writer.WriteAsync(request);
        }

        public async ValueTask<CreateOrderRequest> DequeuePrintJobAsync(CancellationToken cancellationToken)
        {
            // Növbədə bir şey olana qədər gözləyir (proqramı dondurmur)
            return await _queue.Reader.ReadAsync(cancellationToken);
        }
    }
}
