using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.IO;
using System.Configuration;

namespace Sincronizador
{
    class Program
    {
        static DataTable dtCuentas = new DataTable();
        static DataTable dtProductosEliminados = new DataTable();
        static DataTable dtProductosSinInventario = new DataTable();
        static DataTable dtProductosConInventario = new DataTable();
        static DataTable dtProductosOfertas = new DataTable();
        static BOCuentas objcuentas = new BOCuentas();
        static BOSitios objSitios = new BOSitios();
        static BOCategorias objCategorias = new BOCategorias();
        static BOProductos objProductos = new BOProductos();
        static BOOrdenes objOrdenes = new BOOrdenes();
        static BOEmpresas objEmpresas = new BOEmpresas();
        static JavaScriptSerializer js = new JavaScriptSerializer();
        static List<Sitio> sitios;
        static List<Categoria> categorias;
        static List<Producto> productos;
        static List<Orden> ordenes;
        static List<OrdenesPorTienda> ordenesTienda;
        static List<SubCategoria> subCategorias = new List<SubCategoria>();
        static Empresa empresas;
        static Cuenta cuenta;
              

        static void Main(string[] args)
        {
            if (validarSincronizacionMP())
                sincronizarMP();
            else
            {
                ejecutarTodo();
                borrarOfertas();
                sincronizarOfertas();
            }
        }

        static bool validarSincronizacionMP()
        {
            string opc = string.Empty;
            try
            {
                using (StreamReader sr = new StreamReader(ConfigurationManager.AppSettings["archivoValidacionEjecucion"].ToString(), false))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!string.IsNullOrEmpty(line))
                            opc = line;
                    }
                }
                if (opc == "*")
                    return false;
                else
                    return true;
            }
            catch (Exception)
            {
                Console.WriteLine("El archivo no se puede leer");
                return false;
            }
        }

        static void sincronizarMP()
        {
            string cuentamp = new BOCuentas().obtenerCuentaMP();
            string info = obtenerInfoCuenta(cuentamp);
            if (!string.IsNullOrEmpty(info))
            {
                DeserializarCuenta(info);
                if (guardarInfoBD())
                {
                    Console.WriteLine(string.Format("Cuenta {0} actualizada en BD", cuenta.name));
                    int idCuenta;
                    int.TryParse(cuenta.id, out idCuenta);
                    if (idCuenta > 0)
                    {
                        obtenerSitiosPorCuenta(idCuenta);
                        obtenerCategoriasPorCuenta(idCuenta);
                        obtenerProductosPorCuenta(idCuenta);
                        obtenerOrdenesPorCuenta(idCuenta);
                        obtenerEmpresasPorCuenta(idCuenta);
                    }
                }
                else
                    Console.WriteLine(string.Format("Cuenta {0} falló al actualizar en BD", cuenta.name));
            }
            else
            {
                Console.WriteLine(string.Format("No se encontró información para {0}", cuenta));
            }
            ProductosEliminados();
            ProductosSinInventario();
            ProductosConInventario();
            sincronizarInfo();
            sincronizarOfertas();
        }
        static void ejecutarTodo()
        {
            Console.WriteLine("Iniciando proceso de lectura...");
            listarCuentasDesdeBD();
            if (dtCuentas.Rows.Count > 0)
            {
                for (int i = 0; i < dtCuentas.Rows.Count; i++)
                {
                    string info = obtenerInfoCuenta(dtCuentas.Rows[i]["nombreCuentaCam"].ToString());
                    if (!string.IsNullOrEmpty(info) && !(info.ToLower().Contains("error")))
                    {
                        DeserializarCuenta(info);
                        if (guardarInfoBD())
                        {
                            Console.WriteLine(string.Format("Cuenta {0} actualizada en BD", cuenta.name));
                            int idCuenta;
                            int.TryParse(cuenta.id, out idCuenta);
                            if (idCuenta > 0)
                            {
                                obtenerSitiosPorCuenta(idCuenta);
                                obtenerCategoriasPorCuenta(idCuenta);
                                obtenerProductosPorCuenta(idCuenta);
                                obtenerOrdenesPorCuenta(idCuenta);
                                obtenerEmpresasPorCuenta(idCuenta);
                            }
                        }
                        else
                            Console.WriteLine(string.Format("Cuenta {0} falló al actualizar en BD", cuenta.name));
                    }
                    else
                    {
                        Console.WriteLine(string.Format("No se encontró información para {0}", dtCuentas.Rows[i]["nombreCuentaCam"].ToString()));
                    }
                }
            }
            ProductosEliminados();
            ProductosSinInventario();
            ProductosConInventario();
            sincronizarInfo();
            sincronizarOfertas();
        }

        #region InfoBD
        static void sincronizarInfo()
        {
            Console.WriteLine("\n************************\nSincronizando información");
            sincronizarCuentas();
            sincronizarSitios();
            sincronizarCategorias();
            sincronizarSubCategorias();
            sincronizarProductos();
            sincronizarOrdenes();
            sincronizarEmpresas();
        }
        static void sincronizarCuentas()
        {
            objcuentas.sincronizarCuentas();
        }

        static void sincronizarSitios()
        {
            objSitios.sincronizarSitios();
        }

        static void sincronizarCategorias()
        {
            objCategorias.sincronizarCategorias();
        }

        static void sincronizarSubCategorias()
        {
            objCategorias.sincronizarSubCategorias();
        }

        static void sincronizarProductos()
        {
            objProductos.sincronizarProductos();
        }

        static void sincronizarOrdenes()
        {
            objOrdenes.sincronizarOrdenes();
        }

        static void sincronizarEmpresas()
        {
            objEmpresas.sincronizarEmpresas();
        }

        static void sincronizarOfertas()
        {
            APIConsumer c = null;
            string discountLabelOrg = string.Empty, priceOrg = string.Empty, listPriceOrg = string.Empty, costPriceOrg = string.Empty;
            new BOCuentas().listarProductosMP_Ofertas(ref dtProductosOfertas);
            string idCuentaMp = new BOCuentas().obtenerIdCuentaMP();
            if (dtProductosOfertas.Rows.Count > 0)
                c = new APIConsumer();
            for (int i = 0; i < dtProductosOfertas.Rows.Count; i++)
            {
                discountLabelOrg = dtProductosOfertas.Rows[i]["dFte"].ToString();
                priceOrg = dtProductosOfertas.Rows[i]["priceFte"].ToString();
                listPriceOrg = dtProductosOfertas.Rows[i]["lPriceFte"].ToString();
                costPriceOrg = dtProductosOfertas.Rows[i]["cPriceFte"].ToString();

                string[] catsMp = dtProductosOfertas.Rows[i]["catMP"].ToString().Split(';');
                string[] catsOf = dtProductosOfertas.Rows[i]["catOf"].ToString().Split(';');
                //Actualizar MarketPlace producto y subcategoria
                for (int j = 0; j < catsMp.Length; j++)
                {
                    if (!string.IsNullOrEmpty(catsMp[j]))
                    {
                        c.crearRequestActualizacionProd(idCuentaMp, dtProductosOfertas.Rows[i]["nameMP"].ToString(), catsMp[j].ToString(), dtProductosOfertas.Rows[i]["idCamMP"].ToString(), listPriceOrg, priceOrg, costPriceOrg, discountLabelOrg);
                    }
                }
                //Actualizar oferta
                for (int j = 0; j < catsOf.Length; j++)
                {
                    if (!string.IsNullOrEmpty(catsOf[j]))
                    {
                        c.crearRequestActualizacionProd(idCuentaMp, dtProductosOfertas.Rows[i]["nameMP"].ToString(), catsOf[j].ToString(), dtProductosOfertas.Rows[i]["idCamOf"].ToString(), listPriceOrg, priceOrg, costPriceOrg, discountLabelOrg);
                    }
                }
            }
        }

        static void borrarOfertas()
        {
            DataTable dtBorrar = new DataTable();
            BOCuentas ctas = new BOCuentas();
            APIConsumer c = null;
            ctas.listarOfertasBorrar(ref dtBorrar);
            string idOferta = ctas.obtenerIdOfertas();
            if (dtBorrar.Rows.Count > 0)
            {
                c = new APIConsumer();
                for (int i = 0; i < dtBorrar.Rows.Count; i++)
                {
                    string idCam = string.Empty;
                    if (!string.IsNullOrEmpty(dtBorrar.Rows[i]["idsproductos"].ToString()))
                    {
                        idCam = dtBorrar.Rows[i]["idsproductos"].ToString().Split(';')[1].Split(',')[1].Replace("idGenerado:", string.Empty).Trim();
                        c.crearRequestEliminacionProd(ctas.obtenerIdCuentaMP(), idOferta, idCam);
                    }
                }
                ctas.inactivarOfertas("SyncAdmin", int.Parse(dtBorrar.Rows[0]["idOferta"].ToString()));
            }
        }
        static void listarCuentasDesdeBD()
        {
            new BOCuentas().listarCuentas(ref dtCuentas);
        }

        static bool guardarInfoBD()
        {
            return objcuentas.CamilyoguardarInfoCuenta(cuenta);
        }

        static void obtenerSitiosPorCuenta(int idCuenta)
        {
            string res = obtenerSitiosPorCuenta(idCuenta.ToString());
            DeserializarSitios(res);
            if (sitios != null)
            {
                for (int i = 0; i < sitios.Count; i++)
                {
                    new BOSitios().CamilyoguardarInfoSitio(sitios[i]);
                }
            }
            
        }

        static void obtenerCategoriasPorCuenta(int idCuenta)
        {
            string res = obtenerCategoriasPorCuenta(idCuenta.ToString());
            if (idCuenta.ToString() == new BOCuentas().obtenerIdCuentaMP())
            {
                DeserializarSubCategoriasMP(res);
                obtenerSubCategoriasPorCuenta(idCuenta);
            }
            DeserializarCategorias(res);
            if (categorias != null)
            {
                for (int i = 0; i < categorias.Count; i++)
                {
                    new BOCategorias().CamilyoguardarInfoCategorias(categorias[i], idCuenta.ToString());
                }
            }
        }

        static void obtenerSubCategoriasPorCuenta(int idCuenta)
        {
            //Guardando subcategorias
            if (subCategorias.Count > 0)
            {
                for (int i = 0; i < subCategorias.Count; i++)
                {
                    new BOCategorias().CamilyoguardarInfoSubCategorias(subCategorias[i]);
                }
            }
        }

        static void obtenerProductosPorCuenta(int idCuenta)
        {
            string res = obtenerProductosPorCuenta(idCuenta.ToString());
            DeserializarProductos(res);
            if (productos != null)
            {
                for (int i = 0; i < productos.Count; i++)
                {
                    new BOProductos().CamilyoguardarInfoProductos(productos[i], idCuenta.ToString());
                }
            }
        }

        static void obtenerOrdenesPorCuenta(int idCuenta)
        {
            string res = obtenerOrdenesPorCuenta(idCuenta.ToString());
            DeserializarOrdenes(res);
            if (ordenes != null)
            {
                for (int i = 0; i < ordenes.Count; i++)
                {
                    new BOOrdenes().CamilyoguardarInfoOrdenes(ordenes[i]);
                }
            }
        }

        static void obtenerEmpresasPorCuenta(int idCuenta)
        {
            string res = obtenerEmpresasPorCuenta(idCuenta.ToString());
            DeserializarEmpresas(res);
            new BOEmpresas().CamilyoguardarInfoEmpresas(empresas, idCuenta.ToString());
        }

        static void ProductosEliminados()
        {
            new BOProductos().ProductosEliminados(ref dtProductosEliminados);

            if (dtProductosEliminados.Rows.Count > 0)
            {
                for (int i = 0; i < dtProductosEliminados.Rows.Count; i++)
                {
                    //Verificar funcionalidad con la tienda que se creará para simulación. 
                    new APIConsumer().crearRequestEliminacionProd(string.Empty, string.Empty, string.Empty);
                }
            }
        }

        static void ProductosSinInventario()
        {
            new BOProductos().ProductosSinInventario(ref dtProductosSinInventario);

            if (dtProductosSinInventario.Rows.Count > 0)
            {
                for (int i = 0; i < dtProductosSinInventario.Rows.Count; i++)
                {
                    //Verificar funcionalidad con la tienda que se creará para simulación. 
                    new APIConsumer().crearRequestEliminacionProd(string.Empty, string.Empty, string.Empty);
                }
            }
        }

        static void ProductosConInventario()
        {
            new BOProductos().ProductosConInventario(ref dtProductosConInventario);

            if (dtProductosConInventario.Rows.Count > 0)
            {
                for (int i = 0; i < dtProductosConInventario.Rows.Count; i++)
                {
                    //Verificar funcionalidad con la tienda que se creará para simulación. 
                    new APIConsumer().crearRequestEliminacionProd(string.Empty, string.Empty, string.Empty);
                }
            }
        }

        #endregion

        #region API
        static string obtenerInfoCuenta(string nombreCuenta)
        {
            return new RestAPI("http://manage.camilyo.us/api/accounts/", HttpVerb.GET).MakeRequest("?userName=" + nombreCuenta);
        }

        static string obtenerSitiosPorCuenta(string idCuenta)
        {
            return new RestAPI("http://manage.camilyo.us/api/accounts/", HttpVerb.GET).MakeRequest(idCuenta + "/sites");
        }

        static string obtenerCategoriasPorCuenta(string idCuenta)
        {
            return new RestAPI("http://manage.camilyo.us/api/accounts/", HttpVerb.GET).MakeRequest(idCuenta + "/catalog/categories");
        }

        static string obtenerProductosPorCuenta(string idCuenta)
        {
            return new RestAPI("http://manage.camilyo.us/api/accounts/", HttpVerb.GET).MakeRequest(idCuenta + "/catalog/categories/0/products");
        }

        static string obtenerOrdenesPorCuenta(string idCuenta)
        {
            return new RestAPI("http://manage.camilyo.us/api/accounts/", HttpVerb.GET).MakeRequest(idCuenta + "/orders");
        }

        static string obtenerEmpresasPorCuenta(string idCuenta)
        {
            return new RestAPI("http://manage.camilyo.us/api/accounts/", HttpVerb.GET).MakeRequest(idCuenta + "/data");
        }
        #endregion

        #region jsonConverter
        static void DeserializarCuenta(string json)
        {
            cuenta = null;
            if (!json.Equals(string.Empty) && (!json.ToLower().Contains("error")))
                cuenta = js.Deserialize<Cuenta>(json);
        }

        static void DeserializarSitios(string json)
        {
            sitios = null;
            if (!json.Equals(string.Empty))
                sitios = js.Deserialize<List<Sitio>>(json);
        }

        static void DeserializarCategorias(string json)
        {
            categorias = null;
            if (!json.Equals(string.Empty))
            {
                DeserializarSubCategorias(json);
                categorias = js.Deserialize<List<Categoria>>(json);
            }
        }

        static void DeserializarSubCategoriasMP(string json)
        {
            if (!json.Equals(string.Empty))
            {
                DeserializarSubCategorias(json);
            }
        }

        static void DeserializarSubCategorias(string json)
        {
            subCategorias = null;
            subCategorias = new List<SubCategoria>();
            int _parentId = 0;
            string _id = string.Empty;
            string _name = string.Empty;
            string _isOnline = string.Empty;
            string valor = string.Empty;
            string textoOrigen = string.Empty;
            string[] res = null;
            string[] parseSubCategorias = json.Split(new string[] { "subCategories" }, StringSplitOptions.None);
            for (int i = 0; i < parseSubCategorias.Length; i++)
            {
                textoOrigen = parseSubCategorias[i];

                //Obtener parentId
                res = textoOrigen.Split(new string[] { "parentId" }, StringSplitOptions.None);
                if (res.Length > 0)
                {
                    valor = res[res.Length - 1].Replace(",", string.Empty).Replace("\"", string.Empty).Replace(":", string.Empty);
                    int.TryParse(valor, out _parentId);
                }
                else
                    _parentId = 0;

                if (_parentId != 0)
                {
                    //Obtener id
                    textoOrigen = parseSubCategorias[i].Replace("\"$id\"", string.Empty);
                    res = textoOrigen.Split(new string[] { "id" }, StringSplitOptions.None);
                    if (res.Length > 0)
                    {
                        _id = res[res.Length - 1].Split(',')[0].Replace(":", string.Empty).Replace("\"", string.Empty);
                    }
                    else
                        _id = string.Empty;

                    //Obtener nombre
                    res = textoOrigen.Split(new string[] { "name" }, StringSplitOptions.None);
                    if (res.Length > 0)
                    {
                        _name = res[res.Length - 1].Split(',')[0].Replace(":", string.Empty).Replace("\"", string.Empty);
                    }
                    else
                        _name = string.Empty;

                    //Obtener isOnline
                    res = textoOrigen.Split(new string[] { "isOnline" }, StringSplitOptions.None);
                    if (res.Length > 0)
                    {
                        _isOnline = res[res.Length - 1].Split(',')[0].Replace(":", string.Empty).Replace("\"", string.Empty);
                    }
                    else
                        _isOnline = string.Empty;
                }
                if (_parentId != 0 && !(string.IsNullOrEmpty(_parentId.ToString())))
                {
                    SubCategoria sc = new SubCategoria();
                    sc.id = _id;
                    sc.name = _name;
                    sc.parentId = _parentId.ToString();
                    sc.isOnline = _isOnline;
                    subCategorias.Add(sc);
                    sc = null;
                }
            }
        }

        static string obtenerValor(string llave, string textoOrigen)
        {
            string valor = string.Empty;
            textoOrigen = textoOrigen.Replace("\"$id\"", string.Empty);
            string[] res = textoOrigen.Split(new string[] { llave }, StringSplitOptions.None);
            if (res.Length > 0)
            {
                valor = res[res.Length - 1];
                valor = valor.Replace(",", string.Empty);
                valor = valor.Replace("\"", string.Empty);
                valor = valor.Replace(":", string.Empty);
                return valor;
            }
            else
                return string.Empty;
        }

        static void DeserializarProductos(string json)
        {
            productos = null;
            if (!json.Equals(string.Empty))
                productos = js.Deserialize<List<Producto>>(json);
        }

        static void DeserializarOrdenes(string json)
        {
            ordenesTienda = null;
            ordenesTienda = js.Deserialize<List<OrdenesPorTienda>>(json);
        }

        static void DeserializarEmpresas(string json)
        {
            empresas = null;
            empresas = js.Deserialize<Empresa>(json);
        }
        #endregion
    }
}
