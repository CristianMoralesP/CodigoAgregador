using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace parserTest
{
    [Serializable]
    public class BigCommerceOrderProducts
    {
        public List<BigCommerceOrderProduct> Data { get; set; }
    }

    [Serializable]
    public class BigCommerceOrderProduct
    {
        public int Id { get; set; }
        public int Order_id { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string res = " {\"id\": 888677,\"name\": \"Categoría Prueba 1\"}";
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            BigCommerceOrderProducts a = new BigCommerceOrderProducts();
            a.Data = new List<BigCommerceOrderProduct>();
            BigCommerceOrderProduct b = new BigCommerceOrderProduct();
            b.Id = 33;
            b.Order_id = 230025;

            a.Data.Add(b);

            b = new BigCommerceOrderProduct();
            b.Id = 34;
            b.Order_id = 230025;

            a.Data.Add(b);

            string x = jsSerializer.Serialize(a);
            Console.WriteLine(x);

            a = jsSerializer.Deserialize<BigCommerceOrderProducts>(x);

            Console.WriteLine(a.Data[0].Order_id);

            Console.ReadKey();
        }
    }
}
