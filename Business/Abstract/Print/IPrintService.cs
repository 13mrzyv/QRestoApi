using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract.Print
{
    public interface IPrintService
    {
        // Bu metod həm PDF-ə, həm də real printerə çap edə biləcək
        void PrintKitchenTicket(KitchenTicketModel ticket);
    }
}
