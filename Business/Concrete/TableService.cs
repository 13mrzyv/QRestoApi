using Business.Abstract;
using Business.DTOs.Requests;
using Business.DTOs.Responses;
using Data.Abstract;
using Data.Concrete;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TableService : ITableService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TableService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Table>> GetAllTablesAsync()
        {
            var response = await _unitOfWork.TablesRepository.GetAllTablesAsync();

            return response.ToList();
        }
        public async Task<IEnumerable<TableResponse>> GetAllTableResponsesAsync()
        {
            var response = await _unitOfWork.TablesRepository.GetAllTablesDynamicAsync();

            return response.Select(t => new TableResponse
            {
                Id = t.Id,
                TableNumber = t.TableNumber,
                Status = t.Status,
                TotalAmount = t.TotalAmount
            }).ToList();
        }
        public async Task CreateTableAsync(InsertTableRequest insertTableRequest)
        {
            var newTable = new Table
            {
                TableNumber = insertTableRequest.TableNumber,
                Status = 0, // Yeni masalar varsayılan olarak boş (0) olarak başlatılır
                QRToken = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10).ToUpper()
            };

            await _unitOfWork.TablesRepository.InsertTableAsync(newTable);
        }
        public async Task UpdateTableStatusAsync(int id, int status)
        {
            // Repository-dəki metodu çağırırıq
            await _unitOfWork.TablesRepository.UpdateTableStatusAsync(id, status);
        }


    }
}
