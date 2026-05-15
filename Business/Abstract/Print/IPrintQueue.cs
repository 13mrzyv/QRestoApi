using Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract.Print
{
    public interface IPrintQueue
    {
        // Sifarişi növbəyə əlavə edir
        ValueTask EnqueuePrintJobAsync(CreateOrderRequest request);

        // Növbədən sifarişi götürür (Arxa plan işçisi üçün)
        ValueTask<CreateOrderRequest> DequeuePrintJobAsync(CancellationToken cancellationToken);
    }
}
