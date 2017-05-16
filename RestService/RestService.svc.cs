using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DBC;
using System.Web.Script.Serialization;
using System.Web;

namespace RestService
{
    public class RestService : IRestService
    {
        public void GetOrder(int value)
        {
            var serializer = new JavaScriptSerializer();
            var order = Connector.GetOrder(value);
            HttpContext.Current.Response.ContentType = "text/HTML";
            HttpContext.Current.Response.Write(serializer.Serialize(order));

        }
        
        public void ResetDatabase()
        {
            var helper = new DatabaseSetupHelper();
            helper.ResetDatabase();
        }

        public void GetNextOrder(int id)
        {
            var serializer = new JavaScriptSerializer();
            HttpContext.Current.Response.ContentType = "text/HTML";
            HttpContext.Current.Response.Write(serializer.Serialize(Connector.GetNextOrder(id)));
        }

        public void ConfirmOrderLine(int orderId, int orderLineId)
        {
            Connector.ConfirmOrderLine(orderId, orderLineId);
        }

        public void ConfirmOrder(int id)
        {
            Connector.OrderSend(id);
        }
    }
}
