using System;
using System.Web.Security;
using System.Web.Services;
using SampleEntities;
using SerializationUtil;

namespace WebServiceSample {
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class ServiceSample : System.Web.Services.WebService {

        [WebMethod]
        public bool Login(string name, string password) {
            if (name == SampleConstants.ValidUser
                && password == SampleConstants.ValidPassword) {
                FormsAuthentication.SetAuthCookie(name, false);
                return true;
            }
            return false;
        }

        [WebMethod]
        public void Logout() {
            if(Context.User.Identity.IsAuthenticated)
                FormsAuthentication.SignOut();
        }

        [WebMethod]
        public string HelloWorld() {
            return "Hello World";
        }

        [WebMethod]
        public decimal Add(decimal val1, decimal val2){
            return val1 + val2;
        }

        [WebMethod]
        public byte[] GetCustomerByArray(){
            return  Serializer.SerializeBinary(new Customer()).ToArray();
        }

        [WebMethod]
        public Customer GetCustomer(){
            return  new Customer();
        }

        [WebMethod]
        public byte[] GetCustomerByCustomSoap(){
            var soap = Serializer.SerializeSOAP(new Customer());
            Console.Out.WriteLine(soap);
            return  soap.ToArray();
        }

        [WebMethod]
        public Box GetBox(int x, int y, int z){
            return new Box(x, y, z);
        }
    }
}
