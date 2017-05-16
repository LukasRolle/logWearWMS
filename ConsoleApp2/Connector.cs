using System;  // C# , ADO.NET  
    using DT = System.Data;            // System.Data.dll  
    using QC = System.Data.SqlClient;  // System.Data.dll  
    using CS = System.Configuration;
using System.Data.Linq;
using System.Linq;

namespace DBC
{
    public class Connector
    {
        static public void Main()
        {
            var helper = new DatabaseSetupHelper();
            helper.ResetDatabase();
            
        }

        public static Order GetNextOrder(int workerNumber)
        {
            WMSEntities db = new WMSEntities();
            var query =
                from order in db.Orders
                where order.Workers.Where(p => p.WorkerNumber == workerNumber).Count() != 0 && order.OrderPacked != true
                select order;
            return query.FirstOrDefault();
        }

        public static Order GetOrder(int orderNumber)
        {
            WMSEntities db = new WMSEntities();
            var orders =
                from order in db.Orders
                where order.OrderNumber == orderNumber
                select order;
            return orders.FirstOrDefault();
        }

        public static void ConfirmOrderLine(int orderNumber, int orderLineNumber)
        {
            WMSEntities db = new WMSEntities();
            var orders =
                from o in db.Orders
                where o.OrderNumber == orderNumber
                select o;
            Order order = orders.FirstOrDefault();
            if (order != null)
            {
                var orderLines = order.OrderLines.Where(p => p.OrderLineNumber == orderLineNumber);
                foreach (OrderLine orderLine in orderLines)
                {
                    orderLine.Acknowledgement = true;
                }
                db.SaveChanges();            
                    
            }

        }

        public static void OrderSend(int orderNumber)
        {
            WMSEntities db = new WMSEntities();
            var orders =
                from o in db.Orders
                where o.OrderNumber == orderNumber
                select o;
            Order order = orders.FirstOrDefault();
            if (order != null)
            {
                order.OrderPacked = true;
                db.SaveChanges();
            }
        }
    }
}