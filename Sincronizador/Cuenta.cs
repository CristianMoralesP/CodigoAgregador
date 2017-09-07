using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sincronizador
{
    public class Cuenta
    {
        public string id { get; set; }
        public string name { get; set; }
        public string foreignId { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string roleType { get; set; }
        public string creation_time { get; set; }
        public string last_update_time { get; set; }
        public string last_login_time { get; set; }
        public string is_active { get; set; }
        public string simplification_mode { get; set; }
        public string use_external_login { get; set; }
        public string has_terms_flag { get; set; }
        public string onboarding_complited { get; set; }
        public string must_change_pwd { get; set; }
        public string company_name { get; set; }
    }

    public class Sitio
    {
        public string id { get; set; }
        public string domain { get; set; }
        public string sitename { get; set; }
        public string is_active { get; set; }
        public string is_prod_active { get; set; }
        public string displaysiteurl { get; set; }
        public string last_update_time_iso_str { get; set; }
        public string creation_time_iso_str { get; set; }
        public string account_id { get; set; }
        public string account_name { get; set; }
        public string up_to_date { get; set; }
    }

    public class Categoria
    {
        public string id { get; set; }
        public string foreignId { get; set; }
        public string name { get; set; }
        public string imageUrl { get; set; }
        public string shortDescription { get; set; }
        public string isOnline { get; set; }
        public string parentId { get; set; }
        //public List<SubCategoria> subcategorias { get; set; }
        string subcategorias { get; set; }
    }

    public class SubCategoria
    {
        public string id { get; set; }
        public string name { get; set; }
        public string parentId { get; set; }
        public string isOnline { get; set; }
    }

    public class Producto
    {
        public string id { get; set; }
        public string categoryIds { get; set; }
        public string categoryForeignIds { get; set; }
        public string name { get; set; }
        public string foreignId { get; set; }
        public string mainImageUrl { get; set; }
        public string mainImageUrlExternal { get; set; }
        public string pageImageURL { get; set; }
        public string pageImageURLExternal { get; set; }
        public string shortDescription { get; set; }
        public string description { get; set; }
        public string catalogNumber { get; set; }
        public string gtin { get; set; }
        public string model { get; set; }
        public string brandId { get; set; }
        public string brandName { get; set; }
        public string listPrice { get; set; }
        public string costPrice { get; set; }
        public string price { get; set; }
        public string currencyName { get; set; }
        public string deliveryTime { get; set; }
        public string inventory { get; set; }
        public string warranty { get; set; }
        public string isOnline { get; set; }
        public string updated { get; set; }
        public string discountLabel { get; set; }
    }

    public class Orden
    {
        public string id { get; set; }
        public string AccountId { get; set; }
        public string Type { get; set; }
        public string Date { get; set; }
        public string User_Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Company_Id { get; set; }
        public string Company_Name { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string BillingAddress { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingCountry { get; set; }
        public string ShippingName { get; set; }
        public string ShippingCompany_Id { get; set; }
        public string ShippingCompany_Name { get; set; }
        public string ShippingPhone { get; set; }
        public string ShippingMobile { get; set; }
        public string Comments { get; set; }
        public string IsGift { get; set; }
        public string GiftWrapping { get; set; }
        public string GiftWrappingPrice { get; set; }
        public string Delivery_Method { get; set; }
        public string Delivery_Price { get; set; }
        public string Tax { get; set; }
        public string Tax_Included { get; set; }
        public string Discount { get; set; }
        public string Status { get; set; }
        public string Billing { get; set; }
        public string Invoice_Id { get; set; }
        public string Approval { get; set; }
        public string Order_Total_Products { get; set; }
        public string Order_Total_No_Discount { get; set; }
        public string Order_Total { get; set; }
    }

    public class OrdenesPorTienda
    {
        public string id { get; set; }
        public string AccountId { get; set; }
        /*public string Type { get; set; }
        public string Date { get; set; }
        public string User_Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Company_Id { get; set; }
        public string Company_Name { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string BillingAddress { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingCountry { get; set; }
        public string ShippingName { get; set; }
        public string ShippingCompany_Id { get; set; }
        public string ShippingCompany_Name { get; set; }
        public string ShippingPhone { get; set; }
        public string ShippingMobile { get; set; }
        public string Comments { get; set; }
        public string IsGift { get; set; }
        public string GiftWrapping { get; set; }
        public string GiftWrappingPrice { get; set; }
        public string Delivery_Method { get; set; }
        public string Delivery_Price { get; set; }
        public string Tax { get; set; }
        public string Tax_Included { get; set; }
        public string Discount { get; set; }
        public string Status { get; set; }
        public string Billing { get; set; }
        public string Invoice_Id { get; set; }
        public string Approval { get; set; }*/
        public string Order_Total_Products { get; set; }
        /*public string Order_Total_No_Discount { get; set; }
        public string Order_Total { get; set; }*/
    }

    public class Empresa
    {
        public string id { get; set; }
        public string companyname { get; set; }
        public string company_id { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string websitedomain { get; set; }
    }
}
