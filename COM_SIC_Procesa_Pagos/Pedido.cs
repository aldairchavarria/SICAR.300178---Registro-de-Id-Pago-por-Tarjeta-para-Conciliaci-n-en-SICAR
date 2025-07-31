using System;
using System.Data;
using System.Collections;
using SAP_SIC_Ventas;
using System.IO;
using System.Text;
using System.Xml;
using System.Configuration;

namespace COM_SIC_Procesa_Pagos
{
    /// <summary>
    /// Summary description for Pedido.
    /// </summary>
    public class Pedido 
    {

         private PedidoFacturaProxy pedido;
         private Common pedidoPagoContext;

        /// <summary>
        /// No modificar el valor, es una constante para la validacion del ambiente que se ejecuta la aplicacion es de desarrollo
        /// </summary>
        private readonly string TEST_MODE;

        /// <summary>
        /// El valor para mostrar la informacion de inputs en el log es TEST
        /// en el ambiente de produccion debe ser PROD
        /// </summary>
        private string LogMode;

        public Pedido()
        {
            pedido = new PedidoFacturaProxy(SAPDataConnector.ConnectionString);
            pedidoPagoContext = new Common();
            LogMode = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["LogMode"]);
            TEST_MODE = "TEST";
        }

        public bool MigrarPedidoPago(string fechaCuadre, string codigoCajero, string codigoTienda, ref string mensaje)
        {
            int cont = 0;
            int contReg = 0;
			string idProceso = string.Empty;

            decimal val_venta_xml = 0; //AGREGADO JTN

            Common.UsuarioCaja = mensaje;
            mensaje = "";

            Common.WriteLog("---------------------- INICIANDO TRANSACCCION DE PEDIDOS ----------------------");


			Common.WriteLog("-- Separando registros de la BD a migrar: PCK_SICAR_OFF_SAP.MIG_SEP_TRANSACCIONES_PEDIDO");
			idProceso = pedidoPagoContext.separarRegistrosMigrarcion(codigoCajero,codigoTienda,fechaCuadre);
			Common.WriteLog("-- Separando registros de la BD a migrar terminada.");
			Common.WriteLog("-- ID_PROCESO : {0} --",idProceso);

            Common.WriteLog("Importando registros desde la BD: PCK_SICAR_OFF_SAP.CUA_GET_TRANSACCIONES_PEDIDO");
            Common.WriteLog("Parametro fechaCuadre: {0}", fechaCuadre);
            Common.WriteLog("Parametro codigoCajero: {0}", codigoCajero);
            Common.WriteLog("Parametro codigoTienda: {0}", codigoTienda);

            DataSet dsPedidosEnviar = pedidoPagoContext.getPendientesMigrar(codigoCajero, codigoTienda,fechaCuadre,idProceso);


            DataRelation cabeceraRelation = new DataRelation("cabeceraRelation",
                    dsPedidosEnviar.Tables[0].Columns["ID_T_TRS_PEDIDO"], dsPedidosEnviar.Tables[1].Columns["ID_T_TRS_PEDIDO"]);
            DataRelation pedidoRelation = new DataRelation("pedidoRelation",
                dsPedidosEnviar.Tables[0].Columns["ID_T_TRS_PEDIDO"], dsPedidosEnviar.Tables[2].Columns["ID_T_TRS_PEDIDO"]);
            DataRelation pagosRelation = new DataRelation("pagosRelation",
                dsPedidosEnviar.Tables[0].Columns["ID_T_TRS_PEDIDO"], dsPedidosEnviar.Tables[3].Columns["ID_T_TRS_PEDIDO"]);

            dsPedidosEnviar.Relations.AddRange(new DataRelation[] { cabeceraRelation, pedidoRelation, pagosRelation });

            Common.WriteLog("Importacion completada");

            DataSet dsDatosXML = new DataSet();
            dsDatosXML.DataSetName = "Pedido";
            DataTable tbCabeceraPedido = dsPedidosEnviar.Tables[1];
            DataTable tbDetallePedido = dsPedidosEnviar.Tables[2];
            DataTable tbPagos = dsPedidosEnviar.Tables[3];

            int m_ID_T_TRS_PEDIDO = 0;

            foreach (DataRow rowTrsPedido in dsPedidosEnviar.Tables[0].Rows)
            {
                m_ID_T_TRS_PEDIDO = Convert.ToInt32(rowTrsPedido["ID_T_TRS_PEDIDO"]);
                RespuestaLog respuesta;
                string codigoCabecera = Convert.ToString(m_ID_T_TRS_PEDIDO);
                string descripcion = "";
                string deserror = "";
                string archivoerror = "";
                string codigo, fecha, usuario, nombretabla, registrasap = "";

                string cajero = string.Empty;
                string almacen = string.Empty;
                ZTI_CAB_PEDIDO cabeceraPedido = new ZTI_CAB_PEDIDO();
                foreach (DataRow rowCabeceraPedido in rowTrsPedido.GetChildRows("cabeceraRelation"))
                {
                    dsDatosXML.Tables.Clear();
                    DataTable tbCabecera = tbCabeceraPedido.Clone();
                    tbCabecera.Clear();
                    tbCabecera.Rows.Add(rowCabeceraPedido.ItemArray);
                    dsDatosXML.Tables.Add(tbCabecera);
                    dsDatosXML.Tables[0].TableName = "Cabecera";
                    dsDatosXML.AcceptChanges();

                    Common.WriteLog("------ ENVIANDO TRANSACCION DE PEDIDOS ------");

                    Common.WriteLog("CODIGO DE REGISTRO (ID_T_TRS_PEDIDO) : {0}", m_ID_T_TRS_PEDIDO);


                    cabeceraPedido.Auart = Convert.ToString(rowCabeceraPedido["Auart"]);
                    cabeceraPedido.Audat = Convert.ToString(rowCabeceraPedido["Audat"]);
                    cabeceraPedido.Clase_Venta = Convert.ToString(rowCabeceraPedido["Clase_Venta"]);
                    cabeceraPedido.Cliente = Convert.ToString(rowCabeceraPedido["Cliente"]);
                    cabeceraPedido.Spart = Convert.ToString(rowCabeceraPedido["Spart"]);
                    cabeceraPedido.Tipo_Doc_Cliente = Convert.ToString(rowCabeceraPedido["Tipo_Doc_Cliente"]);
                    cabeceraPedido.Tipo_Venta = Convert.ToString(rowCabeceraPedido["Tipo_Venta"]);
                    cabeceraPedido.Vendedor = Convert.ToString(rowCabeceraPedido["Vendedor"]);
                    cabeceraPedido.Vkbur = Convert.ToString(rowCabeceraPedido["Vkbur"]);
                    cabeceraPedido.Vkorg = Convert.ToString(rowCabeceraPedido["Vkorg"]);
                    cabeceraPedido.Vtweg = Convert.ToString(rowCabeceraPedido["Vtweg"]);
                    cabeceraPedido.Waerk = Convert.ToString(rowCabeceraPedido["Waerk"]);
                    cabeceraPedido.Xblnr = Convert.ToString(rowCabeceraPedido["Xblnr"]);

                    if (this.LogMode == TEST_MODE)
                    {
                        Common.WriteLog("Auart: {0}", cabeceraPedido.Auart);
                        Common.WriteLog("Audat: {0}", cabeceraPedido.Audat);
                        Common.WriteLog("Clase_Venta: {0}", cabeceraPedido.Clase_Venta);
                        Common.WriteLog("Cliente: {0}", cabeceraPedido.Cliente);
                        Common.WriteLog("Tipo_Doc_Cliente: {0}", cabeceraPedido.Tipo_Doc_Cliente);
                        Common.WriteLog("Tipo_Venta: {0}", cabeceraPedido.Tipo_Venta);
                        Common.WriteLog("Vkbur: {0}", cabeceraPedido.Vkbur);
                        Common.WriteLog("Vendedor: {0}", cabeceraPedido.Vendedor);
                        Common.WriteLog("Vkorg: {0}", cabeceraPedido.Vkorg);
                        Common.WriteLog("Vtweg: {0}", cabeceraPedido.Vtweg);
                        Common.WriteLog("Waerk: {0}", cabeceraPedido.Waerk);
                        Common.WriteLog("Xblnr: {0}", cabeceraPedido.Xblnr);
                    }
                }

                //DataTable tablaCabecera = new DataTable();
                ArrayList listaDetalle = new ArrayList();
                foreach (DataRow rowDetallePago in rowTrsPedido.GetChildRows("pedidoRelation"))
                {
                    DataTable tbdetalle = tbDetallePedido.Clone();
                    tbdetalle.Clear();

                    Object[] itemArray = rowDetallePago.ItemArray;
                    itemArray[8] = ConfigurationSettings.AppSettings["valVentaRecVirtual"];
                    tbdetalle.Rows.Add(itemArray);

                    dsDatosXML.Tables.Add(tbdetalle);
                    dsDatosXML.Tables[1].TableName = "Detalle";
                    dsDatosXML.AcceptChanges();

                    ZTI_DET_PEDIDO itemDetalle = new ZTI_DET_PEDIDO();

                    itemDetalle.Campana = Convert.ToString(rowDetallePago["Campana"]);

                    itemDetalle.Descuento = rowDetallePago["Descuento"].ToString() != null ? Convert.ToDecimal(rowDetallePago["Descuento"].ToString()) : 0;
                    itemDetalle.Igv = rowDetallePago["Igv"].ToString() != null ? Convert.ToDecimal(rowDetallePago["Igv"].ToString()) : 0;
                    itemDetalle.Kwmeng = rowDetallePago["Kwmeng"].ToString() != null ? Convert.ToDecimal(rowDetallePago["Kwmeng"].ToString()) : 0;
                    itemDetalle.Matnr = Convert.ToString(rowDetallePago["Matnr"]);
                    itemDetalle.Nro_Rec_Switch = Convert.ToString(rowDetallePago["Nro_Rec_Switch"]);
                    itemDetalle.Plan_Tarifario = Convert.ToString(rowDetallePago["Plan_Tarifario"]);
                    itemDetalle.Posnr = Convert.ToString(rowDetallePago["Posnr"]);

                    if (rowDetallePago["Rec_Efectiva"].ToString() != null)
                    { itemDetalle.Rec_Efectiva = Convert.ToDecimal(rowDetallePago["Rec_Efectiva"].ToString()); }
                    else { itemDetalle.Rec_Efectiva = 0; }

                    if (rowDetallePago["Total_Pago"].ToString() != null)
                    { itemDetalle.Total_Pago = Convert.ToDecimal(rowDetallePago["Total_Pago"].ToString()); }
                    else { itemDetalle.Total_Pago = 0; }

                    if (rowDetallePago["val_venta"].ToString() != null)
                    { itemDetalle.Val_Venta = Convert.ToDecimal(rowDetallePago["val_venta"].ToString()); }
                    else { itemDetalle.Val_Venta = 0; }

                    itemDetalle.Val_Venta = itemDetalle.Matnr == ConfigurationSettings.AppSettings["strCodArticuloDTH"] ? itemDetalle.Val_Venta : Convert.ToDecimal(ConfigurationSettings.AppSettings["valVentaRecVirtual"]);
                    //itemDetalle.Val_Venta = itemDetalle.Matnr==ConfigurationSettings.AppSettings["strCodArticuloRV"]?Convert.ToDecimal(ConfigurationSettings.AppSettings["valVentaRecVirtual"]):itemDetalle.Val_Venta;

                    //strCodArticuloRV
                    val_venta_xml = itemDetalle.Val_Venta;
                    itemDetalle.Vkaus = Convert.ToString(rowDetallePago["Vkaus"]);
                    itemDetalle.Zznro_Telef = Convert.ToString(rowDetallePago["Zznro_Telef"]);
                    listaDetalle.Add(itemDetalle);

                    if (this.LogMode == TEST_MODE)
                    {
                        Common.WriteLog("Campana: {0}", itemDetalle.Campana);
                        Common.WriteLog("Descuento: {0}", itemDetalle.Descuento);
                        Common.WriteLog("Igv: {0}", itemDetalle.Igv);
                        Common.WriteLog("Matnr: {0}", itemDetalle.Matnr);
                        Common.WriteLog("Nro_Rec_Switch: {0}", itemDetalle.Nro_Rec_Switch);
                        Common.WriteLog("Plan_Tarifario: {0}", itemDetalle.Plan_Tarifario);
                        Common.WriteLog("Posnr: {0}", itemDetalle.Posnr);
                        Common.WriteLog("Rec_Efectiva: {0}", itemDetalle.Rec_Efectiva);
                        Common.WriteLog("Total_Pago: {0}", itemDetalle.Total_Pago);
                        Common.WriteLog("Val_Venta: {0}", itemDetalle.Val_Venta);
                        Common.WriteLog("Vkaus: {0}", itemDetalle.Vkaus);
                        Common.WriteLog("Zznro_Telef: {0}", itemDetalle.Zznro_Telef);
                    }
                }
                ZTI_DET_PEDIDO[] tramaDetalle = (ZTI_DET_PEDIDO[])listaDetalle.ToArray(typeof(ZTI_DET_PEDIDO));

                ArrayList listaPagos = new ArrayList();
                foreach (DataRow filaPago in rowTrsPedido.GetChildRows("pagosRelation"))
                {
                    DataTable tbpago = tbPagos.Clone();
                    tbpago.Clear();
                    tbpago.Rows.Add(filaPago.ItemArray);
                    dsDatosXML.Tables.Add(tbpago);

                    dsDatosXML.Tables[2].TableName = "Pago";
                    dsDatosXML.AcceptChanges();

                    ZTI_PAGOS itemPago = new ZTI_PAGOS();
                    itemPago.Conc_Bsqda = Convert.ToString(filaPago["Conc_sbqda"]);
                    itemPago.Cond_Pago = Convert.ToString(filaPago["Cond_Pago"]);
                    itemPago.F_Pedido = Convert.ToString(filaPago["F_Pedido"]);
                    itemPago.Glosa = Convert.ToString(filaPago["Glosa"]);
                    double dbimporte;
                    dbimporte = Convert.ToDouble(filaPago["Importe"]);
                    itemPago.Importe = Convert.ToString(String.Format("{0:f2}", dbimporte));
                    itemPago.Moneda = Convert.ToString(filaPago["Moneda"]);
                    itemPago.Nro_Exactus = Convert.ToString(filaPago["Nro_Exactus"]);
                    itemPago.Of_Vtas = Convert.ToString(filaPago["Of_Vtas"]);
                    itemPago.Org_Vtas = Convert.ToString(filaPago["Org_Vtas"]);
                    itemPago.Pos = Convert.ToString(filaPago["Pos"]);
                    itemPago.Referencia = Convert.ToString(filaPago["Referencia"]);
                    itemPago.Solicitante = Convert.ToString(filaPago["Solicitante"]);
                    itemPago.T_Cambio = Convert.ToString(filaPago["T_Cambio"]);
                    itemPago.Via_Pago = Convert.ToString(filaPago["Via_Pago"]);
                    listaPagos.Add(itemPago);

                    if (this.LogMode == TEST_MODE)
                    {
                        Common.WriteLog("Conc_Bsqda: {0}", itemPago.Conc_Bsqda);
                        Common.WriteLog("Cond_Pago: {0}", itemPago.Cond_Pago);
                        Common.WriteLog("F_Pedido: {0}", itemPago.F_Pedido);
                        Common.WriteLog("Glosa: {0}", itemPago.Glosa);
                        Common.WriteLog("Importe: {0}", itemPago.Importe);
                        Common.WriteLog("Moneda: {0}", itemPago.Moneda);
                        Common.WriteLog("Nro_Exactus: {0}", itemPago.Nro_Exactus);
                        Common.WriteLog("Of_Vtas: {0}", itemPago.Of_Vtas);
                        Common.WriteLog("Org_Vtas: {0}", itemPago.Org_Vtas);
                        Common.WriteLog("Pos: {0}", itemPago.Pos);
                        Common.WriteLog("Referencia: {0}", itemPago.Referencia);
                        Common.WriteLog("Solicitante: {0}", itemPago.Solicitante);
                        Common.WriteLog("T_Cambio: {0}", itemPago.T_Cambio);
                        Common.WriteLog("Via_Pago: {0}", itemPago.Via_Pago);
                    }

                }
                cont++;
                string pedido = "";
                string numerofactura = "";
                try
                {
                    DataTable dtrespuestaLog = null;

                    ZTI_PAGOS[] tramaPagos = (ZTI_PAGOS[])listaPagos.ToArray(typeof(ZTI_PAGOS));
                    Common.WriteLog("Enviando informacion a SAP: RFC Zsicar_Rfc_Crea_Ped_Fac...");
                    respuesta = this.pedido.CreaPedidoFactura(cabeceraPedido, tramaDetalle, tramaPagos, out dtrespuestaLog);
                    Common.WriteLog("Envio de informacion a SAP: RFC Zsicar_Rfc_Crea_Ped_Fac terminada.");

                    numerofactura = respuesta.NumeroFactura;
                    pedido = respuesta.NumeroPedido;

                    dsDatosXML.Tables.Add(dtrespuestaLog);
                    dsDatosXML.Tables[3].TableName = "Mensajes";
                    dsDatosXML.AcceptChanges();

                    if (respuesta.IndicadorError != "E")
                    {
                        contReg++;
                        descripcion = respuesta.LogSap;
                        deserror = respuesta.MensajeError;
                        registrasap = "S";

                        deserror = "";
                        TextWriter tw = new StringWriter();
                        //							dsDatos.WriteXml(tw);
                        //INICIO JTN
                        dsDatosXML.Tables[1].Rows[0]["val_venta"] = val_venta_xml;
                        dsDatosXML.Tables[1].AcceptChanges();
                        // FIN JTN
                        dsDatosXML.WriteXml(tw);
                        string xml = tw.ToString().Replace("\r\n", "");
                        archivoerror = xml;

                        if (this.LogMode == TEST_MODE)
                        {
                            Common.WriteLog("************************ INICIO: REGISTRO EN LA BASE DATOS ************************");
                            Common.WriteLog("************************ METODO : actualizarEstadoMigracion ************************");
                            Common.WriteLog("idCodigoCabecera: {0}", m_ID_T_TRS_PEDIDO);
                            Common.WriteLog("deserror: {0}", deserror);
                            Common.WriteLog("************************ FIN : REGISTRO EN LA BASE DATOS ************************\n");
                        }
                        Common.WriteLog("Respuesta desde SAP RFC Zsicar_Rfc_Crea_Ped_Fac: {0}", descripcion);
                    }
                    else
                    {
                        registrasap = "E";
                        descripcion = "";
                        deserror = respuesta.MensajeError;

                        Common.WriteLog("ERROR Respuesta desde SAP RFC Zsicar_Rfc_Crea_Ped_Fac: {0}", deserror);

                        TextWriter tw = new StringWriter();
                        //							dsDatos.WriteXml(tw);
                        //INICIO JTN
                        dsDatosXML.Tables[1].Rows[0]["val_venta"] = val_venta_xml;
                        dsDatosXML.Tables[1].AcceptChanges();
                        // FIN JTN
                        dsDatosXML.WriteXml(tw);
                        string xml = tw.ToString().Replace("\r\n", "");
                        archivoerror = xml;
                    }
                    pedidoPagoContext.actualizarEstadoMigracion(m_ID_T_TRS_PEDIDO, (registrasap == "E" ? respuesta.MensajeError : respuesta.LogSap), registrasap);
                    pedidoPagoContext.RegistrarNroDocumento(m_ID_T_TRS_PEDIDO, pedido, numerofactura);
                }
                catch (SAP.Connector.RfcCommunicationException ex)
                {
					//INI PROY-140126
					string MaptPath = "";
					MaptPath = "( Class : COM_SIC_Procesa_Pagos Pedido.cs; Function: MigrarPedidoPago)";
					Common.WriteLog("ERROR DE COMUNICACION CON SAP {0}", ex.Message + MaptPath); 
					//FIN PROY-140126                 
                    descripcion = ex.Message;
                    codigo = ex.ErrorCode;
                    fecha = DateTime.Today.ToString("dd/MM/yyyy");
                    usuario = Environment.UserDomainName.ToString();
                    nombretabla = "TI_EST_RECAUDACION";
                    archivoerror = "0";
                    deserror = "Error de conexion con SAP";

                    pedidoPagoContext.actualizarEstadoMigracion(m_ID_T_TRS_PEDIDO, "", "N");
                    pedidoPagoContext.RegistrarNroDocumento(m_ID_T_TRS_PEDIDO, pedido, numerofactura);
                    mensaje = "No se proceso ningun Pago";
                    Common.WriteLog("------  FIN ANOMALO DE ENVIO DE TRANSACCION DE PEDIDOS ------\n");
                    return false;
                    ///FIN DEL PROCESO
                }
                catch (Exception ex)
                {
                    Common.WriteLog("************************ Excepción en la Transacción Zsicar_Rfc_Crea_Ped_Fac ************************\n " + ex.Message);
                    TextWriter tw = new StringWriter();
                    //INICIO JTN
                    dsDatosXML.Tables[1].Rows[0]["val_venta"] = val_venta_xml;
                    dsDatosXML.Tables[1].AcceptChanges();
                    // FIN JTN
                    dsDatosXML.WriteXml(tw);
                    string xml = tw.ToString().Replace("\r\n", "");
                    archivoerror = xml;
                    pedidoPagoContext.actualizarEstadoMigracion(m_ID_T_TRS_PEDIDO, ex.Message, "E");
                }

                codigo = Convert.ToString(m_ID_T_TRS_PEDIDO);
                fecha = DateTime.Today.ToString("dd/MM/yyyy");
                usuario = Environment.UserDomainName.ToString();
                nombretabla = "T_TRS_PEDIDO";

                if (pedido != null || numerofactura != null)
                {
                    descripcion = string.Format("Pedido: {0} - Numero Factura: {1}", pedido, numerofactura);
                }
                RegistrarLog(descripcion, codigo, deserror, fecha, usuario, nombretabla, archivoerror);
                dsDatosXML = new DataSet();
                dsDatosXML.DataSetName = "Pedido";


                Common.WriteLog("------  FIN DE ENVIO DE TRANSACCION DE PEDIDOS ------\n");				
            }

            if (cont == 0)
            {
                Common.WriteLog("ERROR Ningun registro ha sido leido", "");
            }


            int enviosConError = cont - contReg;
            if (enviosConError > 0)
            {
                if (enviosConError == cont)
                    mensaje = "No se proceso ningun Pago";
                else
                    mensaje = string.Format("Se procesaron {0} de {1} Pagos", contReg, cont);
            }
            else
            {
                mensaje = "Se procesaron correctamente los Pagos";
            }

            if (cont == 0)
                mensaje = "No se encontraron pedidos para procesar";


            Common.WriteLog(String.Format("Registros Leidos: {0}", cont));
            Common.WriteLog(String.Format("Registros Enviados: {0}", contReg));
            Common.WriteLog(String.Format("Registros No procesados: {0}", cont - contReg));
            Common.WriteLog("------  FIN DE ENVIO DE TRANSACCION DE PEDIDOS ------\n");
			Common.WriteLog("---------------------- FIN ENVIO DE TRANSACCIONES DE CUADRE DE CAJAS ----------------------");
            return (cont == contReg || cont == 0);
        }

        public void MigrarPedidoPago(string fechaActual)
        {
            int cont = 0;
            int contReg = 0;
            decimal val_venta_xml = 0; //AGREGADO JTN

            DataTable datosCabeceraTable = pedidoPagoContext.listarPedidosPagoCabecera(fechaActual);
            //INI PROY-140126
            Common.WriteLog("---------------------- INICIANDO TRANSACCCION DE PEDIDOS ----------------------");
            Common.WriteLog("Importando registros desde la BD: PCK_SICAR_OFF_SAP.MIG_EXTRAE_PEDIDO_FACTURA...");
            //			Common.WriteLog("Parametro fechaActual: {0}",fechaActual);
            DataSet dsDatos = pedidoPagoContext.listarPedidoPago(1, "", "");
            //			Common.WriteLog("Numero de deudas a migrar: {0}",datosCabeceraTable.Rows.Count);
            Common.WriteLog("Importacion completada");
            //FIN PROY-140126

            DataSet dsDatosXML = new DataSet();
            dsDatosXML.DataSetName = "Pedido";
            DataTable tbCabeceraPedido = dsDatos.Tables[0];
            DataTable tbDetallePedido = dsDatos.Tables[1];
            DataTable tbPagos = dsDatos.Tables[2];


            //			dsDatos.Tables[0].TableName="Cabecera";
            //			dsDatos.Tables[1].TableName ="Detalle";
            //			dsDatos.Tables[2].TableName ="Pago";				

            foreach (DataRow filaCabecera in datosCabeceraTable.Rows)
            {   //INI PROY-140126
                Common.WriteLog("************************** ENVIANDO TRANSACCION DE PEDIDOS **************************************");

                Common.WriteLog("************************** INICIO: DATOS A ENVIAR DE CABECERA PEDIDO ****************************");

                ZTI_CAB_PEDIDO cabeceraPedido = new ZTI_CAB_PEDIDO();

                int idCodigoCabecera = Convert.ToInt32(filaCabecera["ID_T_TRS_PEDIDO"]);
                string codigoCabecera = Convert.ToString(idCodigoCabecera);

                //if (tbCabeceraPedido.Rows.Count<=0)

                foreach (DataRow rowCabeceraPedido in tbCabeceraPedido.Select("ID_T_TRS_PEDIDO='" + codigoCabecera + "'"))
                {
                    DataTable tbCabecera = tbCabeceraPedido.Clone();
                    tbCabecera.Clear();
                    tbCabecera.Rows.Add(rowCabeceraPedido.ItemArray);
                    dsDatosXML.Tables.Add(tbCabecera);
                    dsDatosXML.Tables[0].TableName = "Cabecera";
                    dsDatosXML.AcceptChanges();


                    string descripcion = "";
                    string deserror = "";
                    string archivoerror = "";
                    string codigo, fecha, usuario, nombretabla, registrasap = "";
                    RespuestaLog respuesta;

                    //{
                    //	this.LogTransaction.Error("************************ ERROR NO SE ENCUENTRA REGISTROS *********************************\n");
                    //}
                    //else
                    //{						
                    cabeceraPedido.Auart = Convert.ToString(rowCabeceraPedido["Auart"]);
                    cabeceraPedido.Audat = Convert.ToString(rowCabeceraPedido["Audat"]);
                    cabeceraPedido.Clase_Venta = Convert.ToString(rowCabeceraPedido["Clase_Venta"]);
                    cabeceraPedido.Cliente = Convert.ToString(rowCabeceraPedido["Cliente"]);
                    cabeceraPedido.Spart = Convert.ToString(rowCabeceraPedido["Spart"]);
                    cabeceraPedido.Tipo_Doc_Cliente = Convert.ToString(rowCabeceraPedido["Tipo_Doc_Cliente"]);
                    cabeceraPedido.Tipo_Venta = Convert.ToString(rowCabeceraPedido["Tipo_Venta"]);
                    cabeceraPedido.Vendedor = Convert.ToString(rowCabeceraPedido["Vendedor"]);
                    cabeceraPedido.Vkbur = Convert.ToString(rowCabeceraPedido["Vkbur"]);
                    cabeceraPedido.Vkorg = Convert.ToString(rowCabeceraPedido["Vkorg"]);
                    cabeceraPedido.Vtweg = Convert.ToString(rowCabeceraPedido["Vtweg"]);
                    cabeceraPedido.Waerk = Convert.ToString(rowCabeceraPedido["Waerk"]);
                    cabeceraPedido.Xblnr = Convert.ToString(rowCabeceraPedido["Xblnr"]);

                    Common.WriteLog("Auart: {0}", cabeceraPedido.Auart);
                    Common.WriteLog("Audat: {0}", cabeceraPedido.Audat);
                    Common.WriteLog("Clase_Venta: {0}", cabeceraPedido.Clase_Venta);
                    Common.WriteLog("Cliente: {0}", cabeceraPedido.Cliente);
                    Common.WriteLog("Tipo_Doc_Cliente: {0}", cabeceraPedido.Tipo_Doc_Cliente);
                    Common.WriteLog("Tipo_Venta: {0}", cabeceraPedido.Tipo_Venta);
                    Common.WriteLog("Vkbur: {0}", cabeceraPedido.Vkbur);
                    Common.WriteLog("Vendedor: {0}", cabeceraPedido.Vendedor);
                    Common.WriteLog("Vkorg: {0}", cabeceraPedido.Vkorg);
                    Common.WriteLog("Vtweg: {0}", cabeceraPedido.Vtweg);
                    Common.WriteLog("Waerk: {0}", cabeceraPedido.Waerk);
                    Common.WriteLog("Xblnr: {0}", cabeceraPedido.Xblnr);

                    DataTable tablaCabecera = new DataTable();

                    Common.WriteLog("************************** FIN: DATOS A ENVIAR DE CABECERA PEDIDO ****************************");

                    ArrayList listaDetalle = new ArrayList();
                    foreach (DataRow filaDetallePago in tbDetallePedido.Select("ID_T_TRS_PEDIDO='" + codigoCabecera + "'"))
                    {

                        DataTable tbdetalle = tbDetallePedido.Clone();
                        tbdetalle.Clear();
                        tbdetalle.Rows.Add(filaDetallePago.ItemArray);
                        dsDatosXML.Tables.Add(tbdetalle);
                        dsDatosXML.Tables[1].TableName = "Detalle";
                        dsDatosXML.AcceptChanges();


                        Common.WriteLog("**************************  INICIO: DATOS A ENVIAR DETALLE PEDIDO ****************************");

                        ZTI_DET_PEDIDO itemDetalle = new ZTI_DET_PEDIDO();



                        itemDetalle.Campana = Convert.ToString(filaDetallePago["Campana"]);

                        if (filaDetallePago["Descuento"].ToString() != null)
                        { itemDetalle.Descuento = Convert.ToDecimal(filaDetallePago["Descuento"].ToString()); }
                        else { itemDetalle.Descuento = 0; }
                        //itemDetalle.Descuento = Convert.ToDecimal(filaDetallePago["Descuento"]);
                        if (filaDetallePago["Igv"].ToString() != null)
                        { itemDetalle.Igv = Convert.ToDecimal(filaDetallePago["Igv"].ToString()); }
                        else { itemDetalle.Igv = 0; }
                        //itemDetalle.Igv =Convert.ToDecimal( filaDetallePago["Igv"]);
                        if (filaDetallePago["Kwmeng"].ToString() != null)
                        { itemDetalle.Kwmeng = Convert.ToDecimal(filaDetallePago["Kwmeng"].ToString()); }
                        else { itemDetalle.Kwmeng = 0; }
                        //itemDetalle.Kwmeng =Convert.ToDecimal( filaDetallePago["Kwmeng"]);
                        itemDetalle.Matnr = Convert.ToString(filaDetallePago["Matnr"]);
                        itemDetalle.Nro_Rec_Switch = Convert.ToString(filaDetallePago["Nro_Rec_Switch"]);
                        itemDetalle.Plan_Tarifario = Convert.ToString(filaDetallePago["Plan_Tarifario"]);
                        itemDetalle.Posnr = Convert.ToString(filaDetallePago["Posnr"]);

                        if (filaDetallePago["Rec_Efectiva"].ToString() != null)
                        { itemDetalle.Rec_Efectiva = Convert.ToDecimal(filaDetallePago["Rec_Efectiva"].ToString()); }
                        else { itemDetalle.Rec_Efectiva = 0; }
                        //itemDetalle.Rec_Efectiva =Convert.ToDecimal( filaDetallePago["Rec_Efectiva"]);
                        if (filaDetallePago["Total_Pago"].ToString() != null)
                        { itemDetalle.Total_Pago = Convert.ToDecimal(filaDetallePago["Total_Pago"].ToString()); }
                        else { itemDetalle.Total_Pago = 0; }
                        //itemDetalle.Total_Pago =Convert.ToDecimal( filaDetallePago["Total_Pago"]);


                        if (filaDetallePago["val_venta"].ToString() != null)
                        { itemDetalle.Val_Venta = Convert.ToDecimal(filaDetallePago["val_venta"].ToString()); }
                        else { itemDetalle.Val_Venta = 0; }

                        //itemDetalle.Val_Venta = itemDetalle.Matnr==ConfigurationSettings.AppSettings["strCodArticuloRV"]?Convert.ToDecimal(ConfigurationSettings.AppSettings["valVentaRecVirtual"]):itemDetalle.Val_Venta;
                        //						itemDetalle.Val_Venta = itemDetalle.Matnr=="SERECVILIM"? Convert.ToDecimal(ConfigurationSettings.AppSettings["valVentaRecVirtual"]):itemDetalle.Val_Venta;
                        itemDetalle.Val_Venta = itemDetalle.Matnr == ConfigurationSettings.AppSettings["strCodArticuloDTH"] ? itemDetalle.Val_Venta : Convert.ToDecimal(ConfigurationSettings.AppSettings["valVentaRecVirtual"]);

                        val_venta_xml = itemDetalle.Val_Venta; //AGREGADO JTN

                        //itemDetalle.Val_Venta =Convert.ToDecimal( filaDetallePago["Val_Venta"]);
                        itemDetalle.Vkaus = Convert.ToString(filaDetallePago["Vkaus"]);
                        itemDetalle.Zznro_Telef = Convert.ToString(filaDetallePago["Zznro_Telef"]);
                        listaDetalle.Add(itemDetalle);

                        Common.WriteLog("Campana: {0}", itemDetalle.Campana);
                        Common.WriteLog("Descuento: {0}", itemDetalle.Descuento);
                        Common.WriteLog("Igv: {0}", itemDetalle.Igv);
                        Common.WriteLog("Matnr: {0}", itemDetalle.Matnr);
                        Common.WriteLog("Nro_Rec_Switch: {0}", itemDetalle.Nro_Rec_Switch);
                        Common.WriteLog("Plan_Tarifario: {0}", itemDetalle.Plan_Tarifario);
                        Common.WriteLog("Posnr: {0}", itemDetalle.Posnr);
                        Common.WriteLog("Rec_Efectiva: {0}", itemDetalle.Rec_Efectiva);
                        Common.WriteLog("Total_Pago: {0}", itemDetalle.Total_Pago);
                        Common.WriteLog("Val_Venta: {0}", itemDetalle.Val_Venta);
                        Common.WriteLog("Vkaus: {0}", itemDetalle.Vkaus);
                        Common.WriteLog("Zznro_Telef: {0}", itemDetalle.Zznro_Telef);

                        Common.WriteLog("**************************  FIN: DATOS A ENVIAR DETALLE PEDIDO ****************************");

                    }
                    ZTI_DET_PEDIDO[] tramaDetalle = (ZTI_DET_PEDIDO[])listaDetalle.ToArray(typeof(ZTI_DET_PEDIDO));

                    ArrayList listaPagos = new ArrayList();
                    foreach (DataRow filaPago in tbPagos.Select("ID_T_TRS_PEDIDO='" + codigoCabecera + "'"))
                    {
                        DataTable tbpago = tbPagos.Clone();
                        tbpago.Clear();
                        tbpago.Rows.Add(filaPago.ItemArray);
                        dsDatosXML.Tables.Add(tbpago);

                        dsDatosXML.Tables[2].TableName = "Pago";
                        dsDatosXML.AcceptChanges();

                        Common.WriteLog("**************************  INICIO: DATOS A ENVIAR DE PAGOS ****************************");

                        ZTI_PAGOS itemPago = new ZTI_PAGOS();
                        itemPago.Conc_Bsqda = Convert.ToString(filaPago["Conc_sbqda"]);
                        itemPago.Cond_Pago = Convert.ToString(filaPago["Cond_Pago"]);
                        itemPago.F_Pedido = Convert.ToString(filaPago["F_Pedido"]);
                        itemPago.Glosa = Convert.ToString(filaPago["Glosa"]);
                        double dbimporte;
                        dbimporte = Convert.ToDouble(filaPago["Importe"]);
                        itemPago.Importe = Convert.ToString(String.Format("{0:f2}", dbimporte));
                        itemPago.Moneda = Convert.ToString(filaPago["Moneda"]);
                        itemPago.Nro_Exactus = Convert.ToString(filaPago["Nro_Exactus"]);
                        itemPago.Of_Vtas = Convert.ToString(filaPago["Of_Vtas"]);
                        itemPago.Org_Vtas = Convert.ToString(filaPago["Org_Vtas"]);
                        itemPago.Pos = Convert.ToString(filaPago["Pos"]);
                        itemPago.Referencia = Convert.ToString(filaPago["Referencia"]);
                        itemPago.Solicitante = Convert.ToString(filaPago["Solicitante"]);
                        itemPago.T_Cambio = Convert.ToString(filaPago["T_Cambio"]);
                        itemPago.Via_Pago = Convert.ToString(filaPago["Via_Pago"]);
                        listaPagos.Add(itemPago);

                        Common.WriteLog("Conc_Bsqda: {0}", itemPago.Conc_Bsqda);
                        Common.WriteLog("Cond_Pago: {0}", itemPago.Cond_Pago);
                        Common.WriteLog("F_Pedido: {0}", itemPago.F_Pedido);
                        Common.WriteLog("Glosa: {0}", itemPago.Glosa);
                        Common.WriteLog("Importe: {0}", itemPago.Importe);
                        Common.WriteLog("Moneda: {0}", itemPago.Moneda);
                        Common.WriteLog("Nro_Exactus: {0}", itemPago.Nro_Exactus);
                        Common.WriteLog("Of_Vtas: {0}", itemPago.Of_Vtas);
                        Common.WriteLog("Org_Vtas: {0}", itemPago.Org_Vtas);
                        Common.WriteLog("Pos: {0}", itemPago.Pos);
                        Common.WriteLog("Referencia: {0}", itemPago.Referencia);
                        Common.WriteLog("Solicitante: {0}", itemPago.Solicitante);
                        Common.WriteLog("T_Cambio: {0}", itemPago.T_Cambio);
                        Common.WriteLog("Via_Pago: {0}", itemPago.Via_Pago);

                        Common.WriteLog("**************************  FIN : DATOS A ENVIAR DE PAGOS ****************************");
                    }
                    cont++;
                    string pedido = "";
                    string numerofactura = "";
                    try
                    {
                        DataTable dtrespuestaLog = null;

                        ZTI_PAGOS[] tramaPagos = (ZTI_PAGOS[])listaPagos.ToArray(typeof(ZTI_PAGOS));
                        respuesta = this.pedido.CreaPedidoFactura(cabeceraPedido, tramaDetalle, tramaPagos, out dtrespuestaLog);
                        numerofactura = respuesta.NumeroFactura;
                        pedido = respuesta.NumeroPedido;

                        dsDatosXML.Tables.Add(dtrespuestaLog);
                        dsDatosXML.Tables[3].TableName = "Mensajes";
                        dsDatosXML.AcceptChanges();

                        if (respuesta.IndicadorError != "E")
                        {
                            contReg++;
                            descripcion = respuesta.LogSap;
                            deserror = "";
                            registrasap = "S";

                            deserror = "";
                            TextWriter tw = new StringWriter();
                            //							dsDatos.WriteXml(tw);
                            dsDatosXML.WriteXml(tw);
                            string xml = tw.ToString().Replace("\r\n", "");
                            archivoerror = xml;
                            Common.WriteLog("************************ INICIO: REGISTRO EN LA BASE DATOS ************************");
                            Common.WriteLog("************************ METODO : actualizarEstadoMigracion ************************");
                            Common.WriteLog("idCodigoCabecera: {0}", idCodigoCabecera);
                            Common.WriteLog("deserror: {0}", deserror);
                            Common.WriteLog("************************ FIN : REGISTRO EN LA BASE DATOS ************************\n");
                        }
                        else
                        {
                            registrasap = "N";
                            descripcion = "";
                            deserror = respuesta.MensajeError;
                            Common.WriteLog("************************ ERROR AL ENVIAR (RFC Zsicar_Rfc_Crea_Ped_Fac) ************************");
                            Common.WriteLog("MENSAJE SAP: {0}", deserror);
                            TextWriter tw = new StringWriter();
                            //							dsDatos.WriteXml(tw);
                            //INICIO JTN
                            dsDatosXML.Tables[1].Rows[0]["val_venta"] = val_venta_xml;
                            dsDatosXML.Tables[1].AcceptChanges();
                            // FIN JTN
                            dsDatosXML.WriteXml(tw);
                            string xml = tw.ToString().Replace("\r\n", "");
                            archivoerror = xml;
                        }
                        Common.WriteLog("**************************  FIN : DATOS A ENVIAR DE PEDIDOS ****************************\n");
                        pedidoPagoContext.actualizarEstadoMigracion(idCodigoCabecera, respuesta.LogSap, registrasap);
                        pedidoPagoContext.RegistrarNroDocumento(idCodigoCabecera, pedido, numerofactura);
                    }
                    catch (Exception ex)
                    {
                       Common.WriteLog("************************ ERROR EN LA TRANSACCION DE PEDIDOS ************************\n"+ex);

                        TextWriter tw = new StringWriter();
                        //						dsDatos.WriteXml(tw);
                        //INICIO JTN
                        dsDatosXML.Tables[1].Rows[0]["val_venta"] = val_venta_xml;
                        dsDatosXML.Tables[1].AcceptChanges();
                        // FIN JTN
                        dsDatosXML.WriteXml(tw);
                        string xml = tw.ToString().Replace("\r\n", "");
                        archivoerror = xml;
                    }
                    codigo = Convert.ToString(idCodigoCabecera);
                    fecha = DateTime.Today.ToString("dd/MM/yyyy");
                    usuario = Environment.UserDomainName.ToString();
                    nombretabla = "T_TRS_PEDIDO";

                    if (pedido != null || numerofactura != null)
                    {
                        descripcion = string.Format("Pedido: {0} - Numero Factura: {1}", pedido, numerofactura);
                    }
                    RegistrarLog(descripcion, codigo, deserror, fecha, usuario, nombretabla, archivoerror);
                    dsDatosXML = new DataSet();
                    dsDatosXML.DataSetName = "Pedido";

                }

                Common.WriteLog("**************************  FIN DE ENVIO DE TRANSACCION DE PEDIDOS ****************************\n");
            }

            if (cont == 0)
            {
                Common.WriteLog("Ningun registro ha sido leido", "");
            }

            Common.WriteLog(String.Format("Total de Registros Leidos: {0}", cont));
            Common.WriteLog(String.Format("Total de Registros Enviados: {0}", contReg));

            Common.WriteLog("*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨* TRANSACCCION DE PEDIDOS FINALIZADA ¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨\n");
           //FIN PROY-140126
        }

        private void RegistrarLog(string descripcion, string codigo, string deserror, string fecha, string usuario, string nombretabla, string archivo)
        {
            Common obj = new Common();
            obj.RegistrarLog(descripcion, codigo, deserror, fecha, usuario, nombretabla, archivo);
        }

    }
}
