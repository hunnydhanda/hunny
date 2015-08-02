using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WebApplication1.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "Itesting" in both code and config file together.
    [ServiceContract]
    public interface Itesting
    {
        [OperationContract]
        [WebGet]
        void DoWork();

        [OperationContract]
        [WebGet]
         int Multiple(int a, int b);

        [OperationContract]
        [WebGet]
         int Addition(int a, int b);

        [OperationContract]
        [WebGet]
        string databasecall(string username);
        //object databasecall(string username);

    }
}
