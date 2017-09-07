using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Sincronizador
{
    class DAOOrdenes
    {
        SqlConnection conBd = null;
        #region BD
        Conexion objCon = new Conexion();

        private bool conectar()
        {
            this.conBd = objCon.conectar();
            if (this.conBd != null)
            {
                if (this.conBd.State == ConnectionState.Open)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        private void desconectar()
        {
            this.objCon.desconectar();
        }

        #endregion
        public void sincronizarOrdenes()
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("scAgregador.sincronizarOrdenes");
                    objCon.cmdApp.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                logErrorApp("syncOrd", e.Message);
            }
            finally
            { desconectar(); }
        }
        public bool CamilyoguardarInfoOrden(Orden orden)
        {
            try
            {
                if (this.conectar())
                {
                    objCon.configurarComando("Camilyo.GuardarInfoOrden");
                    objCon.cmdApp.Parameters.AddWithValue("@id", orden.id);
                    objCon.cmdApp.Parameters.AddWithValue("@AccountId", orden.AccountId);
                    objCon.cmdApp.Parameters.AddWithValue("@Type", orden.Type);
                    objCon.cmdApp.Parameters.AddWithValue("@Date", orden.Date);
                    objCon.cmdApp.Parameters.AddWithValue("@User_Id", orden.User_Id);
                    objCon.cmdApp.Parameters.AddWithValue("@Name", orden.Name);
                    objCon.cmdApp.Parameters.AddWithValue("@Email", orden.Email);
                    objCon.cmdApp.Parameters.AddWithValue("@Company_Id", orden.Company_Id);
                    objCon.cmdApp.Parameters.AddWithValue("@Company_Name", orden.Company_Name);
                    objCon.cmdApp.Parameters.AddWithValue("@Phone", orden.Phone);
                    objCon.cmdApp.Parameters.AddWithValue("@Mobile", orden.Mobile);
                    objCon.cmdApp.Parameters.AddWithValue("@Address", orden.Address);
                    objCon.cmdApp.Parameters.AddWithValue("@City", orden.City);
                    objCon.cmdApp.Parameters.AddWithValue("@State", orden.State);
                    objCon.cmdApp.Parameters.AddWithValue("@Country", orden.Country);
                    objCon.cmdApp.Parameters.AddWithValue("@BillingAddress", orden.BillingAddress);
                    objCon.cmdApp.Parameters.AddWithValue("@BillingCity", orden.BillingCity);
                    objCon.cmdApp.Parameters.AddWithValue("@BillingState", orden.BillingState);
                    objCon.cmdApp.Parameters.AddWithValue("@BillingCountry", orden.BillingCountry);
                    objCon.cmdApp.Parameters.AddWithValue("@ShippingName", orden.ShippingName);
                    objCon.cmdApp.Parameters.AddWithValue("@ShippingCompany_Id", orden.ShippingCompany_Id);
                    objCon.cmdApp.Parameters.AddWithValue("@ShippingCompany_Name", orden.ShippingCompany_Name);
                    objCon.cmdApp.Parameters.AddWithValue("@ShippingPhone", orden.ShippingPhone);
                    objCon.cmdApp.Parameters.AddWithValue("@ShippingMobile", orden.ShippingMobile);
                    objCon.cmdApp.Parameters.AddWithValue("@Comments", orden.Comments);
                    objCon.cmdApp.Parameters.AddWithValue("@IsGift", orden.IsGift);
                    objCon.cmdApp.Parameters.AddWithValue("@GiftWrapping", orden.GiftWrapping);
                    objCon.cmdApp.Parameters.AddWithValue("@GiftWrappingPrice", orden.GiftWrappingPrice);
                    objCon.cmdApp.Parameters.AddWithValue("@Delivery_Method", orden.Delivery_Method);
                    objCon.cmdApp.Parameters.AddWithValue("@Delivery_Price", orden.Delivery_Price);
                    objCon.cmdApp.Parameters.AddWithValue("@Tax", orden.Tax);
                    objCon.cmdApp.Parameters.AddWithValue("@Tax_Included", orden.Tax_Included);
                    objCon.cmdApp.Parameters.AddWithValue("@Discount", orden.Discount);
                    objCon.cmdApp.Parameters.AddWithValue("@Status", orden.Status);
                    objCon.cmdApp.Parameters.AddWithValue("@Billing", orden.Billing);
                    objCon.cmdApp.Parameters.AddWithValue("@Invoice_Id", orden.Invoice_Id);
                    objCon.cmdApp.Parameters.AddWithValue("@Approval", orden.Approval);
                    objCon.cmdApp.Parameters.AddWithValue("@Order_Total_Products", orden.Order_Total_Products);
                    objCon.cmdApp.Parameters.AddWithValue("@Order_Total_No_Discount", orden.Order_Total_No_Discount);
                    objCon.cmdApp.Parameters.AddWithValue("@Order_Total", orden.Order_Total);
                    objCon.cmdApp.ExecuteNonQuery();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                logErrorApp("StOrd", e.Message);
                return false;
            }
            finally
            { desconectar(); }
        }

        public void logErrorApp(string codigo, string msj)
        {
            this.conectar();
            objCon.configurarComando("Logs.GuardarError");
            objCon.cmdApp.Parameters.AddWithValue("@codigo", codigo);
            objCon.cmdApp.Parameters.AddWithValue("@mensaje", msj);
            objCon.cmdApp.ExecuteNonQuery();
        }
    }
}
