using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case
{
    class Program
    { 
        static void Main(string[] args)
        {
            DatabaseHandler dbHandler = new DatabaseHandler();

            // Get sales history
            dbHandler.GetSalesHistory();

            // Add new sales history record
            dbHandler.AddNewHistoryRecord(2, 1, DateTime.Today.AddDays(-1), 4, 30);
            dbHandler.AddNewHistoryRecord(2, 1, DateTime.Today, 4, 30);

            // Delete sales history record
            dbHandler.DeleteSalesHistoryRecord(DateTime.Today);

            // Update sales history record
            dbHandler.UpdateSalesHistoryRecord(DateTime.Today.AddDays(-1));

            // Get profit for given store
            dbHandler.GetProfitForAGivenStore("Istiklal");

            // Get the most profitable store
            dbHandler.GetMostProfitableStore();

            // Get the best seller product by sales quantity
            dbHandler.GetBestSellerbySalesQuantity();

            Console.ReadKey(true);
        }
    }
}
