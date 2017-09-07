using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.Configuration;

namespace Agregador
{ 
    public class BOProductos
    {
        DAOProductos prods = new DAOProductos();
        
        public void listarProductos(ref DataTable dtProductos, int idEstado, int idCuenta, string termino)
        {
            prods.listarProductos(ref dtProductos, idEstado, idCuenta, termino);
        }

        public void listarDetalleProducto(ref DataTable dtProducto, int idProducto, bool marketplace)
        {
            prods.listarDetalleProducto(ref dtProducto, idProducto, marketplace);

        }
        public void listarEstados(ref DataTable dtEstados)
        {
            prods.listarEstados(ref dtEstados);
        }

        public void listarCategoriasProducto(ref DataTable dtCategorias, int idCategoriaProd)
        {
            prods.listarCategoriasProducto(ref dtCategorias, idCategoriaProd);
        }
        public bool actualizarEstadoProducto(int idProducto, int idEstado)
        {
            return prods.actualizarEstadoProducto(idProducto, idEstado);
        }

        public bool agregarProductoMP(string cuentaOrigen, string productoOrigen, string productoDestino, string usuario)
        {
            return prods.agregarProductoMP(cuentaOrigen, productoOrigen, prods.obtenerCuentaMP().ToString(), productoDestino, usuario);
        }

        public bool crearLoteOferta(string fechaInicio, string fechaFinal, string usuario, string idsproductos)
        {
            return prods.crearLoteOferta(fechaInicio, fechaFinal, usuario, idsproductos);
        }

        public void listarProductosXEstado(ref DataTable dtProductos, int idUsuario)
        {
            prods.listarProductosXEstado(ref dtProductos, idUsuario);
        }

        public void listarCategoriasMP(ref DataTable dtCategorias, string nombre, int codUsuario)
        {
            prods.listarCategoriasMP(ref dtCategorias, nombre, codUsuario);
        }

        public void listarSubCategoriasMP(ref DataTable dtSubCategorias, int parentId, int codUsuario)
        {
            prods.listarSubCategoriasMP(ref dtSubCategorias, parentId, codUsuario);
        }
        public string crearProductoMP(string categoria, string existentes, string fechaInicio, string fechaFin)
        {
            string resOff = string.Empty;
            try
            {
                int idCuentaMP = prods.obtenerIdMP();
                int idOferta = prods.obtenerIdOfertas();
                DataTable dtInfo = new DataTable();
                prods.listarDetalleProductoCategoria(ref dtInfo, categoria, idCuentaMP.ToString());
                if (dtInfo.Rows.Count > 0)
                {
                    for (int i = 0; i < dtInfo.Rows.Count; i++)
                    {
                        if (!existentes.Contains(dtInfo.Rows[i]["id"].ToString()))
                        {
                            resOff = crearRequestCreacionProd(idCuentaMP.ToString(), dtInfo.Rows[i]["id"].ToString(), idOferta.ToString(), dtInfo.Rows[i]["name"].ToString(), dtInfo.Rows[i]["description"].ToString(), dtInfo.Rows[i]["description"].ToString(), dtInfo.Rows[i]["isOnline"].ToString(), dtInfo.Rows[i]["inventory"].ToString(), dtInfo.Rows[i]["foreignId"].ToString(), dtInfo.Rows[i]["mainImageUrl"].ToString(), dtInfo.Rows[i]["price"].ToString(), dtInfo.Rows[i]["listPrice"].ToString(), dtInfo.Rows[i]["currencyName"].ToString(), "ADMINOFERTAS", dtInfo.Rows[i]["UrlPrdOriginal"].ToString(), dtInfo.Rows[i]["categoryIds"].ToString(), dtInfo.Rows[i]["discountLabel"].ToString(), true, dtInfo.Rows[i]["id"].ToString(), fechaInicio, fechaFin);
                            if (!string.IsNullOrEmpty(resOff) && !(resOff.Contains("500")))
                                existentes += string.Format("idAgregador:{0}, idGenerado:{1};", dtInfo.Rows[0]["id"].ToString(), resOff);
                        }
                    }
                }
                else
                    resOff = "Producto sin info";
            }
            catch (Exception e)
            {
                resOff = e.Message;
                prods.logErrorApp("creaProdPorCat", e.Message);
            }
            return existentes;
        }
        public string crearProductoMP(int idProducto, string nombreProducto, string desc, string idsCategorias, string usuario, string subcategorias, string discountLabel)
        {
            string res = string.Empty, resOff = string.Empty;
            try
            {
                int idCuentaMP = prods.obtenerIdMP();
                DataTable dtInfo = new DataTable();
                prods.listarDetalleProducto(ref dtInfo, idProducto, false);
                if (dtInfo.Rows.Count > 0)
                {
                    if (idCuentaMP != 0)
                    {
                        res = crearRequestCreacionProd(idCuentaMP.ToString(), idProducto.ToString(), idsCategorias, nombreProducto, desc, desc, dtInfo.Rows[0]["isOnline"].ToString(), dtInfo.Rows[0]["inventory"].ToString(), dtInfo.Rows[0]["foreignId"].ToString(), dtInfo.Rows[0]["mainImageUrl"].ToString(), dtInfo.Rows[0]["price"].ToString(), dtInfo.Rows[0]["listPrice"].ToString(), dtInfo.Rows[0]["currencyName"].ToString(), usuario, dtInfo.Rows[0]["UrlPrdOriginal"].ToString(), subcategorias, discountLabel, false, dtInfo.Rows[0]["id"].ToString(), string.Empty, string.Empty);
                        if (esOferta(discountLabel, dtInfo.Rows[0]["price"].ToString(), dtInfo.Rows[0]["listPrice"].ToString()))
                        {
                            int idOferta = prods.obtenerIdOfertas();
                            resOff = crearRequestCreacionProd(idCuentaMP.ToString(), idProducto.ToString(), idOferta.ToString(), nombreProducto, desc, desc, dtInfo.Rows[0]["isOnline"].ToString(), dtInfo.Rows[0]["inventory"].ToString(), dtInfo.Rows[0]["foreignId"].ToString(), dtInfo.Rows[0]["mainImageUrl"].ToString(), dtInfo.Rows[0]["price"].ToString(), dtInfo.Rows[0]["listPrice"].ToString(), dtInfo.Rows[0]["currencyName"].ToString(), usuario, dtInfo.Rows[0]["UrlPrdOriginal"].ToString(), subcategorias, discountLabel, true, dtInfo.Rows[0]["id"].ToString(), string.Empty, string.Empty);
                        }
                        //res = actualizarProductoMP(idProducto, nombreProducto, desc, idsCategorias);
                    }
                    else
                        res = "No se encontró cuenta principal";
                }
                else
                    res = "Producto sin info";
            }
            catch (Exception e)
            {
                res = e.Message;
                prods.logErrorApp("creaP", e.Message);
            }
            return res;
        }

        public string establecerUrlProducto(string idCategoria, string idProducto, string url)
        {
            string res = string.Empty;
            try
            {
                int idCuentaMP = prods.obtenerIdMP();
                if (idCuentaMP != 0)
                    res = crearRequestUrlProducto(idProducto, idCuentaMP.ToString(), idCategoria, url);
                else
                    res = "No se encontró cuenta principal";
            }
            catch (Exception e)
            {
                res = e.Message;
                prods.logErrorApp("EstUrl", e.Message);
            }
            return res;
        }
        private bool esOferta(string discountLabel, string precioFinal, string precioLista)
        {
            try
            {
                //Que en el formulario del producto se encuentre diligenciado el campo de descuento.
                //Que en el formulario del producto el producto este marcado como producto en descuento.
                if (discountLabel != "NA")
                    return true;
                //Que en el formulario del producto las fechas de inicio de oferta y fin de oferta estén diligenciados.?
                /*
                    Validar con Jairo lectura de esos campos
                */
                //Que en el formulario del producto, el precio final del producto es diferente del precio de lista del producto y además el precio de lista es diferente de 0.
                if ((precioFinal != precioLista) && precioLista != "0")
                    return true;
                //Si no cumple ninguna característica devuelve false
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private string crearRequestCreacionProd(string idCuenta, string idProducto, string categories, string name, string shortDesc, string desc, string isOnline, string inventory, string foreignId, string url, string price, string listPrice, string currency, string usuario, string urlOriginal, string subCategorias, string discountLabel, bool esOferta, string idAgregador, string fechaInicioOferta, string fechaFinOferta)
        {
            try
            {
                if (inventory == "-1")
                    inventory = "0";
                StringBuilder sb = new StringBuilder();
                sb.Append("{\"name\":\"");
                sb.Append(name);
                sb.Append("\",\"categoryIds\":\"");
                sb.Append(subCategorias);
                sb.Append("\",\"foreignId\":\"");
                sb.Append(foreignId);
                sb.Append("\",\"mainImageUrl\":\"");
                sb.Append(url);
                sb.Append("\",\"mainImageUrlExternal\":\"");
                sb.Append(url);
                sb.Append("\",\"pageImageUrl\":\"");
                sb.Append(url);
                sb.Append("\",\"pageImageUrlExternal\":\"");
                sb.Append(url);
                sb.Append("\",\"imageGallery\":");
                sb.Append("[]");
                sb.Append(",\"shortDescription\":\"");
                sb.Append(shortDesc);
                sb.Append("\", \"description\":\"");
                sb.Append(desc);
                sb.Append("\",\"catalogNumber\":\"");
                sb.Append(string.Empty);
                sb.Append("\",\"gtin\":\"");
                sb.Append(string.Empty);
                sb.Append("\",\"model\":\"");
                sb.Append(string.Empty);
                sb.Append("\",\"brandId\":\"");
                sb.Append(string.Empty);
                sb.Append("\",\"dealStart\":\"");
                if (string.IsNullOrEmpty(fechaInicioOferta))
                    sb.Append(string.Empty);
                else
                    sb.Append(fechaInicioOferta);
                sb.Append("\",\"dealEnd\":\"");
                if (string.IsNullOrEmpty(fechaFinOferta))
                    sb.Append(string.Empty);
                else
                    sb.Append(fechaFinOferta);
                sb.Append(string.Empty);
                sb.Append("\",\"serviceStart\":\"");
                sb.Append(string.Empty);
                sb.Append("\",\"serviceEnd\":\"");
                sb.Append(string.Empty);
                sb.Append("\",\"dealProduct\":\"");
                sb.Append("false");
                sb.Append("\",\"brandName\":\"");
                sb.Append(string.Empty);
                sb.Append("\",\"price\":\"");
                sb.Append(price);
                sb.Append("\",\"listPrice\":\"");
                sb.Append(listPrice);
                sb.Append("\",\"currencyName\":\"");
                sb.Append(currency);
                sb.Append("\",\"currencySymbol\":\"");
                sb.Append("$");
                sb.Append("\",\"isOnline\":\"");
                sb.Append(isOnline);
                sb.Append("\",\"inventory\":\"");
                sb.Append(inventory);
                sb.Append("\",\"deliveryTime\":\"");
                sb.Append(string.Empty);
                sb.Append("\",\"warranty\":\"");
                sb.Append(string.Empty);
                sb.Append("\",\"updated\":\"");
                sb.Append(DateTime.Now.Year + "-" + DateTime.Now.Month +  "-" + DateTime.Now.Day);
                sb.Append("\",\"seoData\":");
                sb.Append("{\"$id\":\"2\",");
                sb.Append("\"friendlyUrl\": \"\",");
                sb.Append("\"title\": \"\",");
                sb.Append("\"description\": \"\",");
                sb.Append("\"keywords\": \"\"");
                sb.Append("},");
                sb.Append("\"attributes\":[");
                sb.Append("{");
                sb.Append("\"id\":\"\",");
                sb.Append("\"searchId\":\"\",");
                sb.Append("\"name\":\"\",");
                sb.Append("\"value\":\"\"");
                sb.Append("},");
                sb.Append("{");
                sb.Append("\"id\":\"308112\",");
                sb.Append("\"searchId\":\"Url_Original\",");
                sb.Append("\"name\":\"Url Original\",");
                sb.Append("\"value\":\"");
                sb.Append(urlOriginal);
                sb.Append("\"}");
                sb.Append("],");
                sb.Append("\"width\":\"");
                sb.Append(string.Empty);
                sb.Append("\",\"height\":\"");
                sb.Append(string.Empty);
                sb.Append("\",\"weight\":\"");
                sb.Append(string.Empty);
                sb.Append("\",\"discountLabel\":\"");
                sb.Append(discountLabel);
                sb.Append("\",\"serviceArea\":\"");
                sb.Append(string.Empty);
                sb.Append("\",\"fragile\":\"");
                sb.Append("false");
                sb.Append("\",\"flagProduct\":");
                sb.Append("false");
                sb.Append("}");
                string res = new RestAPI(string.Format("{0}api/accounts/{1}/catalog/categories/{2}/products", ConfigurationManager.AppSettings["CamilyoAPIUrl"].ToString(), idCuenta, categories), HttpVerb.POST, sb.ToString()).MakeRequest();
                string idGenerado = obtenerNuevoIdProducto(res);
                string codeMsj = "Creation";
                if (esOferta)
                    new BOAgregador().logRequest("Offer" + codeMsj, sb.ToString(), res, usuario, idAgregador, idGenerado);
                else
                    new BOAgregador().logRequest(codeMsj, sb.ToString(), res, usuario, idAgregador, idGenerado);
                return idGenerado;
            }
            catch (Exception ex)
            {
                prods.logErrorApp("creaProdCam", ex.Message);
                return string.Empty;
            }
        }
        public string obtenerNuevoIdProducto(string res)
        {
            string id = string.Empty;
            string[] arrTemp = res.Split(',');
            for (int i = 0; i < arrTemp.Length; i++)
            {
                if (arrTemp[i].Replace("\"", string.Empty).Contains("id") && !(arrTemp[i].Replace("\"", string.Empty).Contains("$")))
                {
                    id = arrTemp[i].Split(':')[1];
                    break;
                }
            }
            return id;
        }
        public string actualizarProductoMP(int idProducto, string nombreProducto, string desc, string idsCategorias)
        {
            try
            {
                int idCuentaMP = prods.obtenerIdMP();
                DataTable dtInfo = new DataTable();
                prods.listarDetalleProducto(ref dtInfo, idProducto, true);
                if (dtInfo.Rows.Count > 0)
                {
                    if (idCuentaMP != 0)
                    {
                        return crearRequestActualizacionProd(idCuentaMP.ToString(), idsCategorias, dtInfo.Rows[0]["idCam"].ToString(), nombreProducto, dtInfo.Rows[0]["pageImageURLExternal"].ToString(), dtInfo.Rows[0]["shortDescription"].ToString(), desc, dtInfo.Rows[0]["listPrice"].ToString(), dtInfo.Rows[0]["price"].ToString(), dtInfo.Rows[0]["inventory"].ToString());
                    }
                    else
                        return "Falla en el API";
                }
                return "Producto sin info";
            }
            catch (Exception e)
            {
                prods.logErrorApp("actPMP", e.Message);
                return e.Message;
            }
        }

        private string crearRequestActualizacionProd(string idCuenta, string idCategoria, string idProducto, string nombreProducto, string url, string shortDes, string desc, string listPrice, string price, string inventory)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("{\"id\":\"");
                sb.Append(idProducto);
                sb.Append("\",\"categoryIds\":\"");
                sb.Append(idCategoria);
                sb.Append("\",\"categoryForeignIds\":\"");
                sb.Append(string.Empty);
                sb.Append("\",\"name\":\"");
                sb.Append(nombreProducto);
                sb.Append("\",\"foreignId\":\"");
                sb.Append(string.Empty);
                sb.Append("\", \"pageImageURLExternal\":\"");
                sb.Append(url);
                sb.Append("\", \"listPrice\":\"");
                sb.Append(listPrice);
                sb.Append("\", \"price\":\"");
                sb.Append(price);
                sb.Append("\", \"inventory\":\"");
                sb.Append(inventory);
                sb.Append("\", \"isOnline\":\"");
                sb.Append("true");
                sb.Append("\",\"shortDescription\":\"");
                sb.Append(shortDes);
                sb.Append("\",\"description\":\"");
                sb.Append(desc);
                sb.Append("\"");
                sb.Append("}");
                //string prov = "{\"id\": 3790361, \"categoryIds\": \"\", \"categoryForeignIds\": \"\", \"name\": \"Anillo Marania actu\", \"foreignId\": \"\",   \"mainImageUrlExternal\": \"http://www.aldeaviral.com/wp-content/uploads/2016/03/a-7.jpg\", \"shortDescription\": \"short desc\", \"description\": \"long desc\", \"listPrice\": 17.0, \"price\": 18.0, \"inventory\": 21, \"isOnline\": true}";
                //return new RestAPI("http://manage.dynamiapublicar.co/api/accounts/11157/catalog/categories/375735/products", HttpVerb.PUT, prov).MakeRequest();
                return new RestAPI(string.Format("{0}api/accounts/{1}/catalog/categories/{2}/products", ConfigurationManager.AppSettings["CamilyoAPIUrl"].ToString(), idCuenta, idCategoria), HttpVerb.PUT, sb.ToString()).MakeRequest();
            }
            catch (Exception e)
            {
                prods.logErrorApp("craP", e.Message);
                return string.Empty;
            }
        }

        private string crearRequestUrlProducto(string idProducto, string idCuenta, string idCategoria, string url)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                /*sb.Append("{\"id\":\"");
                sb.Append(idProducto);*/
                sb.Append("{\"attributeId\":\"");
                sb.Append("308112");
                /*sb.Append("\",\"name\":\"");
                sb.Append("Url Original");*/
                sb.Append("\",\"value\":\"");
                sb.Append(url);
                sb.Append("\"");
                sb.Append("}");
                return new RestAPI(string.Format("{0}api/accounts/{1}/catalog/categories/{2}/products/{3}/attributes", ConfigurationManager.AppSettings["CamilyoAPIUrl"].ToString(), idCuenta, idCategoria, idProducto), HttpVerb.PUT, sb.ToString()).MakeRequest();
            }
            catch (Exception e)
            {
                prods.logErrorApp("crurlP", e.Message);
                return string.Empty;
            }
        }
        public string borrarProductoMP(int idProducto, int categoryId)
        {
            try
            {
                int idCuentaMP = prods.obtenerIdMP();
                DataTable dtInfo = new DataTable();
                prods.listarDetalleProducto(ref dtInfo, idProducto, true);
                if (idCuentaMP != 0)
                {
                    return crearRequestEliminacionProd(idCuentaMP, categoryId, idProducto);
                }
                else
                    return "No se encuentra num cuenta MP";
            }
            catch (Exception e)
            {
                prods.logErrorApp("bpmp", e.Message);
                return e.Message;
            }
        }

        private string crearRequestEliminacionProd(int idCuenta, int categoryId, int productid)
        {
            try
            {
                //return new RestAPI(string.Format("http://manage.camilyo.us/api/accounts/{0}/catalog/categories/{1}/products/{2}", idCuenta, categoryId, productid), HttpVerb.DELETE).MakeRequest();
                return new RestAPI(string.Format("{0}api/accounts/{1}/catalog/categories/{2}/products/{3}", ConfigurationManager.AppSettings["CamilyoAPIUrl"].ToString(), idCuenta, categoryId, productid), HttpVerb.DELETE).MakeRequest();
            }
            catch (Exception e)
            {
                prods.logErrorApp("crep", e.Message);
                return string.Empty;
            }
        }
    }
}