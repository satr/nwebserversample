using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using ConsoleApplication.localhost;
using SampleEntities;
using SerializationUtil;

namespace ConsoleApplication{
    internal class Program{
        private static void Main(string[] args){
            var program = new Program();
            DoInvalidLogon();
            Do();
            Console.In.ReadLine();
        }

        private static void DoInvalidLogon(){
            using(ServiceSample service = GetService(SampleConstants.ValidUser,
                                          SampleConstants.InvalidPassword)){
                if (service == null)
                    return;
                Debug.Fail("Authentication failed");
            }
        }

        private static void Do(){
            using (ServiceSample service = GetService(SampleConstants.ValidUser,
                                                 SampleConstants.ValidPassword)){
            if (service == null)
                    return;
            try{
                Console.Out.WriteLine(service.HelloWorld());
                Console.Out.WriteLine("{0}+{1}={2}", 1, 2, service.Add(1, 2));
                service.AddAsync(1, 2);
                service.AddCompleted += service_AddCompleted;
                localhost.Customer customer = service.GetCustomer();
                object customerByArray = Serializer.DeSerializeBinary(new MemoryStream(service.GetCustomerByArray()));
                object customerBySoap = Serializer.DeSerializeSOAP(new MemoryStream(service.GetCustomerByCustomSoap()));
                Box box = service.GetBox(10, 20, 30);
                Console.Out.WriteLine("A volume of a box of size {0}x{1}x{2} is {3}", box.X, box.Y, box.Z, box.Volume);
            }
            finally{
                service.Logout();
            }
        }
    }

        private static ServiceSample GetService(string userName, string password){
            var service = new ServiceSample();
            service.CookieContainer = new CookieContainer();
            Console.Out.Write("Logon for a user {0} password {1}... ", userName, password);
            bool accessAllowed = service.Login(userName, password);
            if (!accessAllowed){
                Console.Out.WriteLine("Access denied");
                return null;
            }
            CookieContainer container = service.CookieContainer;
            CookieCollection cookies = container.GetCookies(new Uri("http://localhost"));
            Console.Out.WriteLine("Success");
            return service;
        }

        private static void service_AddCompleted(object sender, AddCompletedEventArgs e){
            Console.Out.WriteLine("{0}+{1}={2}", 1, 2, e.Result);
        }
    }
}