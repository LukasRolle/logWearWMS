using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RestService
{
    [ServiceContract]
    public interface IRestService
    {

        [OperationContract]
        [WebGet(UriTemplate = "Order/?id={id}")]
        void GetOrder(int id);

        [OperationContract]
        [WebGet(UriTemplate = "NextOrder/?id={id}")]
        void GetNextOrder(int id);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "ResetDatabase")]
        void ResetDatabase();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ConfirmOrderLine/?orderId={orderId}&orderLineId={orderLineId}")]
        void ConfirmOrderLine(int orderId, int orderLineId);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ConfirmOrder/?orderId={id}")]
        void ConfirmOrder(int id);

    }
}
