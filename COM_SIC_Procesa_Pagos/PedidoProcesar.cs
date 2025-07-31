using System;
using System.Data;
using System.Collections;
using SAP_SIC_Ventas;
using System.IO;
using System.Text;
using System.Configuration;

namespace COM_SIC_Procesa_Pagos
{
	/// <summary>
	/// Summary description for PedidoProcesar.
	/// </summary>
	public class PedidoProcesar
	{
		PedidoFacturaProxy pedido;
		Common pedidoPagoContext;

		public PedidoProcesar()
		{
			pedido = new PedidoFacturaProxy(SAPDataConnector.ConnectionString);
			pedidoPagoContext = new Common();
		}


		public string MigrarPedidoPago(int idCodigoCabecera,ref string res)
		{
			int cont=0;
			int contReg=0;
			string descripcion="";
			string deserror="";
			string archivoerror="";
			string codigo, fecha, usuario,nombretabla,registrasap="";
			RespuestaLog respuesta;
                        //INI PROY-140126
			Common.WriteLog("*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*INICIANDO TRANSACCCION DE PEDIDO ¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*");
			Common.WriteLog("¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*RFC (Zsicar_Rfc_Crea_Ped_Fac)¨*¨*¨*¨*¨*¨*¨*¨*¨*¨**");

			Common.WriteLog("************************** ENVIANDO TRANSACCION DE PEDIDOS **************************************");
			Common.WriteLog("************************** INICIO: DATOS A ENVIAR DE CABECERA PEDIDO ****************************");

			DataSet dsDatos = pedidoPagoContext.GetPedidoPago(idCodigoCabecera);
				
			dsDatos.DataSetName="Pedido";
			dsDatos.Tables[0].TableName="Cabecera";
			dsDatos.Tables[1].TableName ="Detalle";
			dsDatos.Tables[2].TableName ="Pago";
				
			DataTable tbCabeceraPedido = dsDatos.Tables[0];
			DataTable tbDetallePedido = dsDatos.Tables[1];
			DataTable tbPagos = dsDatos.Tables[2];
			string codigoCabecera=Convert.ToString(idCodigoCabecera);

			ZTI_CAB_PEDIDO cabeceraPedido=new ZTI_CAB_PEDIDO();	

			DataSet dsDatosXML = new DataSet();
			dsDatosXML.DataSetName="Pedido";
			
				//if (tbCabeceraPedido.Rows.Count<=0)
				//{
				foreach(DataRow rowCabeceraPedido in tbCabeceraPedido.Select("ID_T_TRS_PEDIDO='" + codigoCabecera + "'"))
				{
				//	this.LogTransaction.Error("************************ ERROR NO SE ENCUENTRA REGISTROS *********************************\n");
				//}
				//else
				//{						


					DataTable tbCabecera = tbCabeceraPedido.Clone();
					tbCabecera.Clear();
					tbCabecera.Rows.Add(rowCabeceraPedido.ItemArray);
					dsDatosXML.Tables.Add(tbCabecera);
					dsDatosXML.Tables[0].TableName = "Cabecera";
					dsDatosXML.AcceptChanges();

					cabeceraPedido.Auart =Convert.ToString(rowCabeceraPedido["Auart"]);
					cabeceraPedido.Audat =Convert.ToString(rowCabeceraPedido["Audat"]);
					cabeceraPedido.Clase_Venta =Convert.ToString(rowCabeceraPedido["Clase_Venta"]);
					cabeceraPedido.Cliente =Convert.ToString(rowCabeceraPedido["Cliente"]);
					cabeceraPedido.Spart =Convert.ToString(rowCabeceraPedido["Spart"]);
					cabeceraPedido.Tipo_Doc_Cliente =Convert.ToString(rowCabeceraPedido["Tipo_Doc_Cliente"]);
					cabeceraPedido.Tipo_Venta =Convert.ToString(rowCabeceraPedido["Tipo_Venta"]);
					cabeceraPedido.Vendedor =Convert.ToString(rowCabeceraPedido["Vendedor"]);
					cabeceraPedido.Vkbur =Convert.ToString(rowCabeceraPedido["Vkbur"]);
					cabeceraPedido.Vkorg =Convert.ToString(rowCabeceraPedido["Vkorg"]);
					cabeceraPedido.Vtweg =Convert.ToString(rowCabeceraPedido["Vtweg"]);
					cabeceraPedido.Waerk =Convert.ToString(rowCabeceraPedido["Waerk"]);
					cabeceraPedido.Xblnr =Convert.ToString(rowCabeceraPedido["Xblnr"]);

					Common.WriteLog("Auart: {0}", cabeceraPedido.Auart);
					Common.WriteLog("Audat: {0}", cabeceraPedido.Audat);
					Common.WriteLog("Clase_Venta: {0}", cabeceraPedido.Clase_Venta);
					Common.WriteLog("Cliente: {0}", cabeceraPedido.Cliente );
					Common.WriteLog("Tipo_Doc_Cliente: {0}",cabeceraPedido.Tipo_Doc_Cliente );
					Common.WriteLog("Tipo_Venta: {0}", cabeceraPedido.Tipo_Venta );
					Common.WriteLog("Vkbur: {0}", cabeceraPedido.Vkbur);
					Common.WriteLog("Vendedor: {0}", cabeceraPedido.Vendedor);
					Common.WriteLog("Vkorg: {0}", cabeceraPedido.Vkorg);
					Common.WriteLog("Vtweg: {0}", cabeceraPedido.Vtweg);
					Common.WriteLog("Waerk: {0}", cabeceraPedido.Waerk);
					Common.WriteLog("Xblnr: {0}", cabeceraPedido.Xblnr);
				
					Common.WriteLog("************************** FIN: DATOS A ENVIAR DE CABECERA PEDIDO ****************************");

					ArrayList listaDetalle=new ArrayList();
					foreach(DataRow filaDetallePago in tbDetallePedido.Select("ID_T_TRS_PEDIDO='" + codigoCabecera + "'"))
					{

						Common.WriteLog("**************************  INICIO: DATOS A ENVIAR DETALLE PEDIDO ****************************");

						DataTable tbdetalle = tbDetallePedido.Clone();
						tbdetalle.Clear();
						
						Object[] itemArray = filaDetallePago.ItemArray;
						itemArray[8] = ConfigurationSettings.AppSettings["valVentaRecVirtual"];
						tbdetalle.Rows.Add(itemArray);

						dsDatosXML.Tables.Add(tbdetalle);
						dsDatosXML.Tables[1].TableName = "Detalle";
						dsDatosXML.AcceptChanges();
	
						ZTI_DET_PEDIDO itemDetalle = new ZTI_DET_PEDIDO();

						itemDetalle.Campana = Convert.ToString(filaDetallePago["Campana"]);

						if (filaDetallePago["Descuento"].ToString()!=null)
						{itemDetalle.Descuento= Convert.ToDecimal(filaDetallePago["Descuento"].ToString());}
						else{itemDetalle.Descuento= 0;}
						
						if (filaDetallePago["Igv"].ToString()!=null)
						{itemDetalle.Igv= Convert.ToDecimal(filaDetallePago["Igv"].ToString());}
						else{itemDetalle.Igv= 0;}
						
						if (filaDetallePago["Kwmeng"].ToString()!=null)
						{itemDetalle.Kwmeng= Convert.ToDecimal(filaDetallePago["Kwmeng"].ToString());}
						else{itemDetalle.Kwmeng= 0;}
						
						itemDetalle.Matnr = Convert.ToString(filaDetallePago["Matnr"]);
						itemDetalle.Nro_Rec_Switch = Convert.ToString(filaDetallePago["Nro_Rec_Switch"]);
						itemDetalle.Plan_Tarifario = Convert.ToString(filaDetallePago["Plan_Tarifario"]);
						itemDetalle.Posnr = Convert.ToString(filaDetallePago["Posnr"]);

						if (filaDetallePago["Rec_Efectiva"].ToString()!=null)
						{itemDetalle.Rec_Efectiva= Convert.ToDecimal(filaDetallePago["Rec_Efectiva"].ToString());}
						else{itemDetalle.Rec_Efectiva= 0;}
						
						if (filaDetallePago["Total_Pago"].ToString()!=null)
						{itemDetalle.Total_Pago= Convert.ToDecimal(filaDetallePago["Total_Pago"].ToString());}
						else{itemDetalle.Total_Pago= 0;}
						
						if (filaDetallePago["Val_Venta"].ToString()!=null)
						{itemDetalle.Val_Venta= Convert.ToDecimal(filaDetallePago["Val_Venta"].ToString());}
						else{itemDetalle.Val_Venta= 0;}
						
						itemDetalle.Vkaus = Convert.ToString(filaDetallePago["Vkaus"]);
						itemDetalle.Zznro_Telef = Convert.ToString(filaDetallePago["Zznro_Telef"]);

						//itemDetalle.Val_Venta = itemDetalle.Matnr=="SERECVILIM"? Convert.ToDecimal(ConfigurationSettings.AppSettings["valVentaRecVirtual"]):itemDetalle.Val_Venta;
						itemDetalle.Val_Venta = itemDetalle.Matnr==ConfigurationSettings.AppSettings["strCodArticuloDTH"]?itemDetalle.Val_Venta: Convert.ToDecimal(ConfigurationSettings.AppSettings["valVentaRecVirtual"]);

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
					ZTI_DET_PEDIDO[] tramaDetalle =(ZTI_DET_PEDIDO[])listaDetalle.ToArray(typeof(ZTI_DET_PEDIDO));

					ArrayList listaPagos = new ArrayList();

					//if (tbPagos.Rows.Count > 0)
					//{
						foreach(DataRow filaPago in tbPagos.Select("ID_T_TRS_PEDIDO='" + codigoCabecera + "'"))
						{
							Common.WriteLog("**************************  INICIO: DATOS A ENVIAR DE PAGOS ****************************");

							string f_pedido =Convert.ToString( filaPago["F_Pedido"]);
							//DD.MM/YYY PARA RFC
							f_pedido = string.Format("{0}.{1}.{2}", f_pedido.Substring(8,2), f_pedido.Substring(5, 2), f_pedido.Substring(0, 4));

							DataTable tbpago = tbPagos.Clone();
							tbpago.Clear();
							tbpago.Rows.Add(filaPago.ItemArray);
							dsDatosXML.Tables.Add(tbpago);
						
							dsDatosXML.Tables[2].TableName = "Pago";
							dsDatosXML.AcceptChanges();

							ZTI_PAGOS itemPago = new ZTI_PAGOS();
							itemPago.Conc_Bsqda =Convert.ToString( filaPago["Conc_sbqda"]);
							itemPago.Cond_Pago =Convert.ToString( filaPago["Cond_Pago"]);
							itemPago.F_Pedido =f_pedido;
							itemPago.Glosa =Convert.ToString( filaPago["Glosa"]);
							itemPago.Importe =Convert.ToString(filaPago["Importe"]);
							itemPago.Moneda =Convert.ToString( filaPago["Moneda"]);
							itemPago.Nro_Exactus =Convert.ToString( filaPago["Nro_Exactus"]);
							itemPago.Of_Vtas =Convert.ToString( filaPago["Of_Vtas"]);
							itemPago.Org_Vtas =Convert.ToString( filaPago["Org_Vtas"]);
							itemPago.Pos =Convert.ToString( filaPago["Pos"]);
							itemPago.Referencia =Convert.ToString( filaPago["Referencia"]);
							itemPago.Solicitante =Convert.ToString( filaPago["Solicitante"]);
							itemPago.T_Cambio =Convert.ToString( filaPago["T_Cambio"]);
							itemPago.Via_Pago =Convert.ToString( filaPago["Via_Pago"]);
							listaPagos.Add(itemPago);

							Common.WriteLog("Conc_Bsqda: {0}", itemPago.Conc_Bsqda);
							Common.WriteLog("Cond_Pago: {0}", itemPago.Cond_Pago );
							Common.WriteLog("F_Pedido: {0}", itemPago.F_Pedido);
							Common.WriteLog("Glosa: {0}", itemPago.Glosa);
							Common.WriteLog("Importe: {0}", itemPago.Importe );
							Common.WriteLog("Moneda: {0}", itemPago.Moneda);
							Common.WriteLog("Nro_Exactus: {0}", itemPago.Nro_Exactus);
							Common.WriteLog("Of_Vtas: {0}", itemPago.Of_Vtas);
							Common.WriteLog("Org_Vtas: {0}", itemPago.Org_Vtas);
							Common.WriteLog("Pos: {0}", itemPago.Pos);
							Common.WriteLog("Referencia: {0}", itemPago.Referencia );
							Common.WriteLog("Solicitante: {0}", itemPago.Solicitante );
							Common.WriteLog("T_Cambio: {0}", itemPago.T_Cambio);
							Common.WriteLog("Via_Pago: {0}", itemPago.Via_Pago);

							Common.WriteLog("**************************  FIN : DATOS A ENVIAR DE PAGOS ****************************");
						}
					//}

					cont++;
					string pedido="";
					string numerofactura="";
					try				
					{	
						DataTable dtrespuestaLog= null;
						
						ZTI_PAGOS[] tramaPagos =(ZTI_PAGOS[]) listaPagos.ToArray(typeof(ZTI_PAGOS));
						respuesta = this.pedido.CreaPedidoFactura(cabeceraPedido,tramaDetalle,tramaPagos,out dtrespuestaLog);
						numerofactura=respuesta.NumeroFactura;
						pedido=respuesta.NumeroPedido;	

						dsDatosXML.Tables.Add(dtrespuestaLog);
						dsDatosXML.Tables[3].TableName = "Mensajes";
						dsDatosXML.AcceptChanges();

						if (respuesta.IndicadorError != "E")
						{
							contReg++;							
							descripcion=respuesta.LogSap;
							deserror = "";						
							registrasap="S";
							
							deserror="";
							TextWriter tw = new StringWriter();
							dsDatosXML.WriteXml(tw);
							string xml=tw.ToString().Replace("\r\n","");
							archivoerror=xml;

							//deserror = string.Format("MensajeError: {0}" ,respuesta.MensajeError);
							Common.WriteLog("************************ INICIO: REGISTRO EN LA BASE DATOS ************************");						
							Common.WriteLog("************************ METODO : actualizarEstadoMigracion ************************");
							Common.WriteLog("idCodigoCabecera: {0}", idCodigoCabecera);
							Common.WriteLog("deserror: {0}", deserror);	
							Common.WriteLog("************************ FIN : REGISTRO EN LA BASE DATOS ************************\n");						
						}
						else
						{

						registrasap="E";
						descripcion="";
						deserror = respuesta.MensajeError;
						//deserror = respuesta.LogSap;
						Common.WriteLog("************************ ERROR AL ENVIAR (RFC Zsicar_Rfc_Crea_Ped_Fac) ************************");
						Common.WriteLog("MENSAJE SAP: {0}",deserror);
						TextWriter tw = new StringWriter();
						dsDatosXML.WriteXml(tw);
						string xml=tw.ToString().Replace("\r\n","");
						archivoerror=xml;
						}
						Common.WriteLog("**************************  FIN : DATOS A ENVIAR DE PEDIDOS ****************************\n");
						//pedidoPagoContext.actualizarEstadoMigracion(idCodigoCabecera, respuesta.LogSap,registrasap);
						pedidoPagoContext.actualizarEstadoMigracion(idCodigoCabecera, (registrasap == "E" ? respuesta.MensajeError : respuesta.LogSap), registrasap);
						pedidoPagoContext.RegistrarNroDocumento(idCodigoCabecera,pedido,numerofactura);
					}
					catch(Exception ex)
					{
						Common.WriteLog("************************ ERROR EN LA TRANSACCION DE PEDIDOS ************************\n"+ex);

						TextWriter tw = new StringWriter();
						dsDatosXML.WriteXml(tw);
						string xml=tw.ToString().Replace("\r\n","");
						archivoerror=xml;
						//INI PROY-140126
						string MaptPath = "";
						MaptPath = "( Class : COM_SIC_Procesa_Pagos PedidoProcesar.cs; Function: MigrarPedidoPago)";
						pedidoPagoContext.actualizarEstadoMigracion(idCodigoCabecera, ex.Message + MaptPath, "E"); 
						//INI PROY-140126
						
						res = "No se puede imprimir porque fallo envio a SAP. Contacte a Soporte";
						return res;
					}
					codigo=Convert.ToString(idCodigoCabecera);
					fecha = DateTime.Today.ToString("dd/MM/yyyy");
					usuario= Environment.UserDomainName.ToString();
					nombretabla="T_TRS_PEDIDO";

					if (pedido!=null || numerofactura!=null)
					{
						descripcion=string.Format("Pedido: {0} - Numero Factura: {1}",pedido,numerofactura);
					}
					RegistrarLog(descripcion,codigo, deserror, fecha, usuario,nombretabla,archivoerror);
					res=deserror;
				}	
				
				Common.WriteLog("**************************  FIN DE ENVIO DE TRANSACCION DE PEDIDOS ****************************\n");
			//}

			if(cont==0 & contReg == 0)
			{
				res = "La recarga ya se esta enviando a SAP, intente luego.";
			}
			Common.WriteLog(String.Format("Total de Registros Leidos: {0}", cont));
			Common.WriteLog(String.Format("Total de Registros Enviados: {0}", contReg));
			dsDatosXML= new DataSet();
			dsDatosXML.DataSetName="Pedido";
			Common.WriteLog("*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨* TRANSACCCION DE PEDIDOS FINALIZADA ¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨\n");
			return res;
		}
	

		/// <summary>
		/// Procesa el pago
		/// </summary>
		/// <param name="idCodigoCabecera"></param>
		/// <param name="res"></param>
		/// <param name="numeroFactura"></param>
		/// <returns></returns>
		public string MigrarPedidoPago(int idCodigoCabecera,ref string res, ref string numeroFactura)
		{

			int cont=0;
			int contReg=0;
			string descripcion="";
			string deserror="";
			string archivoerror="";
			string codigo, fecha, usuario,nombretabla,registrasap="";
			RespuestaLog respuesta;

			//DataTable datosCabeceraTable = pedidoPagoContext.listarPedidosPagoCabecera(codigo);

			Common.WriteLog("*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*INICIANDO TRANSACCCION DE PEDIDO ¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*");
			Common.WriteLog("¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*RFC (Zsicar_Rfc_Crea_Ped_Fac)¨*¨*¨*¨*¨*¨*¨*¨*¨*¨**");


			//foreach(DataRow filaCabecera in datosCabeceraTable.Rows)
			//{

			Common.WriteLog("************************** ENVIANDO TRANSACCION DE PEDIDOS **************************************");

			Common.WriteLog("************************** INICIO: DATOS A ENVIAR DE CABECERA PEDIDO ****************************");

			//int idCodigoCabecera = Convert.ToInt32(filaCabecera["ID_T_TRS_PEDIDO"]);
			DataSet dsDatos = pedidoPagoContext.GetPedidoPago(idCodigoCabecera);
			

			dsDatos.DataSetName="Pedido";
			dsDatos.Tables[0].TableName="Cabecera";
			dsDatos.Tables[1].TableName ="Detalle";
			dsDatos.Tables[2].TableName ="Pago";
				
			DataTable tbCabeceraPedido = dsDatos.Tables[0];
			DataTable tbDetallePedido = dsDatos.Tables[1];
			DataTable tbPagos = dsDatos.Tables[2];
			string codigoCabecera=Convert.ToString(idCodigoCabecera);
			ZTI_CAB_PEDIDO cabeceraPedido=new ZTI_CAB_PEDIDO();

			DataSet dsDatosXML = new DataSet();
			dsDatosXML.DataSetName="Pedido";
			
			//if (tbCabeceraPedido.Rows.Count<=0)
			//{
			foreach(DataRow rowCabeceraPedido in tbCabeceraPedido.Select("ID_T_TRS_PEDIDO='" + codigoCabecera + "'"))
			{
				//	this.LogTransaction.Error("************************ ERROR NO SE ENCUENTRA REGISTROS *********************************\n");
				//}
				//else
				//{						

				DataTable tbCabecera = tbCabeceraPedido.Clone();
				tbCabecera.Clear();
				tbCabecera.Rows.Add(rowCabeceraPedido.ItemArray);
				dsDatosXML.Tables.Add(tbCabecera);
				dsDatosXML.Tables[0].TableName = "Cabecera";
				dsDatosXML.AcceptChanges();

				cabeceraPedido.Auart =Convert.ToString(rowCabeceraPedido["Auart"]);
				cabeceraPedido.Audat =Convert.ToString(rowCabeceraPedido["Audat"]);
				cabeceraPedido.Clase_Venta =Convert.ToString(rowCabeceraPedido["Clase_Venta"]);
				cabeceraPedido.Cliente =Convert.ToString(rowCabeceraPedido["Cliente"]);
				cabeceraPedido.Spart =Convert.ToString(rowCabeceraPedido["Spart"]);
				cabeceraPedido.Tipo_Doc_Cliente =Convert.ToString(rowCabeceraPedido["Tipo_Doc_Cliente"]);
				cabeceraPedido.Tipo_Venta =Convert.ToString(rowCabeceraPedido["Tipo_Venta"]);
				cabeceraPedido.Vendedor =Convert.ToString(rowCabeceraPedido["Vendedor"]);
				cabeceraPedido.Vkbur =Convert.ToString(rowCabeceraPedido["Vkbur"]);
				cabeceraPedido.Vkorg =Convert.ToString(rowCabeceraPedido["Vkorg"]);
				cabeceraPedido.Vtweg =Convert.ToString(rowCabeceraPedido["Vtweg"]);
				cabeceraPedido.Waerk =Convert.ToString(rowCabeceraPedido["Waerk"]);
				cabeceraPedido.Xblnr =Convert.ToString(rowCabeceraPedido["Xblnr"]);

				Common.WriteLog("Auart: {0}", cabeceraPedido.Auart);
				Common.WriteLog("Audat: {0}", cabeceraPedido.Audat);
				Common.WriteLog("Clase_Venta: {0}", cabeceraPedido.Clase_Venta);
				Common.WriteLog("Cliente: {0}", cabeceraPedido.Cliente );
				Common.WriteLog("Tipo_Doc_Cliente: {0}",cabeceraPedido.Tipo_Doc_Cliente );
				Common.WriteLog("Tipo_Venta: {0}", cabeceraPedido.Tipo_Venta );
				Common.WriteLog("Vkbur: {0}", cabeceraPedido.Vkbur);
				Common.WriteLog("Vendedor: {0}", cabeceraPedido.Vendedor);
				Common.WriteLog("Vkorg: {0}", cabeceraPedido.Vkorg);
				Common.WriteLog("Vtweg: {0}", cabeceraPedido.Vtweg);
				Common.WriteLog("Waerk: {0}", cabeceraPedido.Waerk);
				Common.WriteLog("Xblnr: {0}", cabeceraPedido.Xblnr);
				
				Common.WriteLog("************************** FIN: DATOS A ENVIAR DE CABECERA PEDIDO ****************************");

				ArrayList listaDetalle=new ArrayList();
				foreach(DataRow filaDetallePago in tbDetallePedido.Select("ID_T_TRS_PEDIDO='" + codigoCabecera + "'"))
				{

					Common.WriteLog("**************************  INICIO: DATOS A ENVIAR DETALLE PEDIDO ****************************");
	
					
					DataTable tbdetalle = tbDetallePedido.Clone();
					tbdetalle.Clear();
						
					Object[] itemArray = filaDetallePago.ItemArray;
					itemArray[8] = ConfigurationSettings.AppSettings["valVentaRecVirtual"];
					tbdetalle.Rows.Add(itemArray);

					dsDatosXML.Tables.Add(tbdetalle);
					dsDatosXML.Tables[1].TableName = "Detalle";
					dsDatosXML.AcceptChanges();

					ZTI_DET_PEDIDO itemDetalle = new ZTI_DET_PEDIDO();

					

					itemDetalle.Campana = Convert.ToString(filaDetallePago["Campana"]);

					if (filaDetallePago["Descuento"].ToString()!=null)
					{itemDetalle.Descuento= Convert.ToDecimal(filaDetallePago["Descuento"].ToString());}
					else{itemDetalle.Descuento= 0;}
						
					if (filaDetallePago["Igv"].ToString()!=null)
					{itemDetalle.Igv= Convert.ToDecimal(filaDetallePago["Igv"].ToString());}
					else{itemDetalle.Igv= 0;}
						
					if (filaDetallePago["Kwmeng"].ToString()!=null)
					{itemDetalle.Kwmeng= Convert.ToDecimal(filaDetallePago["Kwmeng"].ToString());}
					else{itemDetalle.Kwmeng= 0;}
						
					itemDetalle.Matnr = Convert.ToString(filaDetallePago["Matnr"]);
					itemDetalle.Nro_Rec_Switch = Convert.ToString(filaDetallePago["Nro_Rec_Switch"]);
					itemDetalle.Plan_Tarifario = Convert.ToString(filaDetallePago["Plan_Tarifario"]);
					itemDetalle.Posnr = Convert.ToString(filaDetallePago["Posnr"]);

					if (filaDetallePago["Rec_Efectiva"].ToString()!=null)
					{itemDetalle.Rec_Efectiva= Convert.ToDecimal(filaDetallePago["Rec_Efectiva"].ToString());}
					else{itemDetalle.Rec_Efectiva= 0;}
						
					if (filaDetallePago["Total_Pago"].ToString()!=null)
					{itemDetalle.Total_Pago= Convert.ToDecimal(filaDetallePago["Total_Pago"].ToString());}
					else{itemDetalle.Total_Pago= 0;}
						
					if (filaDetallePago["Val_Venta"].ToString()!=null)
					{itemDetalle.Val_Venta= Convert.ToDecimal(filaDetallePago["Val_Venta"].ToString());}
					else{itemDetalle.Val_Venta= 0;}
						
					itemDetalle.Vkaus = Convert.ToString(filaDetallePago["Vkaus"]);
					itemDetalle.Zznro_Telef = Convert.ToString(filaDetallePago["Zznro_Telef"]);

					//itemDetalle.Val_Venta = itemDetalle.Matnr=="SERECVILIM"? Convert.ToDecimal(ConfigurationSettings.AppSettings["valVentaRecVirtual"]):itemDetalle.Val_Venta;
					itemDetalle.Val_Venta = itemDetalle.Matnr==ConfigurationSettings.AppSettings["strCodArticuloDTH"]?itemDetalle.Val_Venta: Convert.ToDecimal(ConfigurationSettings.AppSettings["valVentaRecVirtual"]);

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
				ZTI_DET_PEDIDO[] tramaDetalle =(ZTI_DET_PEDIDO[])listaDetalle.ToArray(typeof(ZTI_DET_PEDIDO));

				ArrayList listaPagos = new ArrayList();

				//if (tbPagos.Rows.Count > 0)
				//{
				foreach(DataRow filaPago in tbPagos.Select("ID_T_TRS_PEDIDO='" + codigoCabecera + "'"))
				{

					DataTable tbpago = tbPagos.Clone();
					tbpago.Clear();
					tbpago.Rows.Add(filaPago.ItemArray);
					dsDatosXML.Tables.Add(tbpago);
						
					dsDatosXML.Tables[2].TableName = "Pago";
					dsDatosXML.AcceptChanges();

					Common.WriteLog("**************************  INICIO: DATOS A ENVIAR DE PAGOS ****************************");
					
					string f_pedido =Convert.ToString( filaPago["F_Pedido"]);

					//DD.MM/YYY PARA RFC
					f_pedido = string.Format("{0}.{1}.{2}", f_pedido.Substring(8,2), f_pedido.Substring(5, 2), f_pedido.Substring(0, 4));

					ZTI_PAGOS itemPago = new ZTI_PAGOS();
					itemPago.Conc_Bsqda =Convert.ToString( filaPago["Conc_sbqda"]);
					itemPago.Cond_Pago =Convert.ToString( filaPago["Cond_Pago"]);
					itemPago.F_Pedido =f_pedido;
					itemPago.Glosa =Convert.ToString( filaPago["Glosa"]);
					itemPago.Importe =Convert.ToString(filaPago["Importe"]);
					itemPago.Moneda =Convert.ToString( filaPago["Moneda"]);
					itemPago.Nro_Exactus =Convert.ToString( filaPago["Nro_Exactus"]);
					itemPago.Of_Vtas =Convert.ToString( filaPago["Of_Vtas"]);
					itemPago.Org_Vtas =Convert.ToString( filaPago["Org_Vtas"]);
					itemPago.Pos =Convert.ToString( filaPago["Pos"]);
					itemPago.Referencia =Convert.ToString( filaPago["Referencia"]);
					itemPago.Solicitante =Convert.ToString( filaPago["Solicitante"]);
					itemPago.T_Cambio =Convert.ToString( filaPago["T_Cambio"]);
					itemPago.Via_Pago =Convert.ToString( filaPago["Via_Pago"]);
					listaPagos.Add(itemPago);

					Common.WriteLog("Conc_Bsqda: {0}", itemPago.Conc_Bsqda);
					Common.WriteLog("Cond_Pago: {0}", itemPago.Cond_Pago );
					Common.WriteLog("F_Pedido: {0}", itemPago.F_Pedido);
					Common.WriteLog("Glosa: {0}", itemPago.Glosa);
					Common.WriteLog("Importe: {0}", itemPago.Importe );
					Common.WriteLog("Moneda: {0}", itemPago.Moneda);
					Common.WriteLog("Nro_Exactus: {0}", itemPago.Nro_Exactus);
					Common.WriteLog("Of_Vtas: {0}", itemPago.Of_Vtas);
					Common.WriteLog("Org_Vtas: {0}", itemPago.Org_Vtas);
					Common.WriteLog("Pos: {0}", itemPago.Pos);
					Common.WriteLog("Referencia: {0}", itemPago.Referencia );
					Common.WriteLog("Solicitante: {0}", itemPago.Solicitante );
					Common.WriteLog("T_Cambio: {0}", itemPago.T_Cambio);
					Common.WriteLog("Via_Pago: {0}", itemPago.Via_Pago);

					Common.WriteLog("**************************  FIN : DATOS A ENVIAR DE PAGOS ****************************");
				}
				//}

				cont++;
				string pedido="";
				string numerofactura="";
				try				
				{	
					DataTable dtrespuestaLog= null;
						
					ZTI_PAGOS[] tramaPagos =(ZTI_PAGOS[]) listaPagos.ToArray(typeof(ZTI_PAGOS));
					respuesta = this.pedido.CreaPedidoFactura(cabeceraPedido,tramaDetalle,tramaPagos,out dtrespuestaLog);						
					numerofactura=respuesta.NumeroFactura;
					numeroFactura = respuesta.NumeroFactura;
					pedido=respuesta.NumeroPedido;

					dsDatosXML.Tables.Add(dtrespuestaLog);
					dsDatosXML.Tables[3].TableName = "Mensajes";
					dsDatosXML.AcceptChanges();

					if (respuesta.IndicadorError != "E")
					{
						contReg++;							
						descripcion=respuesta.LogSap;
						deserror = "";						
						registrasap="S";
							
						deserror="";
						TextWriter tw = new StringWriter();
						dsDatosXML.WriteXml(tw);
						string xml=tw.ToString().Replace("\r\n","");
						archivoerror=xml;

						//deserror = string.Format("MensajeError: {0}" ,respuesta.MensajeError);
						Common.WriteLog("************************ INICIO: REGISTRO EN LA BASE DATOS ************************");						
						Common.WriteLog("************************ METODO : actualizarEstadoMigracion ************************");
						Common.WriteLog("idCodigoCabecera: {0}", idCodigoCabecera);
						Common.WriteLog("deserror: {0}", deserror);	
						Common.WriteLog("************************ FIN : REGISTRO EN LA BASE DATOS ************************\n");						
					}
					else
					{
//						dsDatosXML.Tables.Add(dtrespuestaLog);
//						dsDatosXML.Tables[3].TableName = "Mensajes";

						registrasap="E";
						descripcion="";
						deserror = respuesta.MensajeError;
						//deserror = respuesta.LogSap;
						Common.WriteLog("************************ ERROR AL ENVIAR (RFC Zsicar_Rfc_Crea_Ped_Fac) ************************");
						Common.WriteLog("MENSAJE SAP: {0}",deserror);
						TextWriter tw = new StringWriter();
						dsDatosXML.WriteXml(tw);
						string xml=tw.ToString().Replace("\r\n","");
						archivoerror=xml;
					}
					Common.WriteLog("**************************  FIN : DATOS A ENVIAR DE PEDIDOS ****************************\n");
					pedidoPagoContext.actualizarEstadoMigracion(idCodigoCabecera, respuesta.LogSap,registrasap);
					pedidoPagoContext.RegistrarNroDocumento(idCodigoCabecera,pedido,numerofactura);
				}
				catch(Exception ex)
				{
					Common.WriteLog("************************ ERROR EN LA TRANSACCION DE PEDIDOS ************************\n"+ex);

					TextWriter tw = new StringWriter();
					dsDatosXML.WriteXml(tw);
					string xml=tw.ToString().Replace("\r\n","");
					archivoerror=xml;
					//INI PROY-140126
					string MaptPath = "";
					MaptPath = "( Class : COM_SIC_Procesa_Pagos PedidoProcesar.vb; Function: MigrarPedidoPago)";
					pedidoPagoContext.actualizarEstadoMigracion(idCodigoCabecera, ex.Message + MaptPath, "E" ); 
					//FIN PROY-140126
					
					res=ex.InnerException.Message.ToString();
					res = "No se puede imprimir porque fallo envio a SAP. Contacte a Soporte";
					return res;
				}
				codigo=Convert.ToString(idCodigoCabecera);
				fecha = DateTime.Today.ToString("dd/MM/yyyy");
				usuario= Environment.UserDomainName.ToString();
				nombretabla="T_TRS_PEDIDO";

				if (pedido!=null || numerofactura!=null)
				{
					descripcion=string.Format("Pedido: {0} - Numero Factura: {1}",pedido,numerofactura);
				}
				RegistrarLog(descripcion,codigo, deserror, fecha, usuario,nombretabla,archivoerror);
				dsDatosXML= new DataSet();
				dsDatosXML.DataSetName="Pedido";
				res=deserror;
			}	
				
			Common.WriteLog("**************************  FIN DE ENVIO DE TRANSACCION DE PEDIDOS ****************************\n");
			//}

			Common.WriteLog(String.Format("Total de Registros Leidos: {0}", cont));
			Common.WriteLog(String.Format("Total de Registros Enviados: {0}", contReg));

			Common.WriteLog("*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨* TRANSACCCION DE PEDIDOS FINALIZADA ¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨\n");
			return res;
                        //FIN PROY-140126
		}
	

		private void RegistrarLog(string descripcion,string codigo, string deserror, string fecha, string usuario,string nombretabla, string archivo)
		{
			Common obj = new Common();
			obj.RegistrarLog(descripcion,codigo, deserror, fecha, usuario,nombretabla,archivo);

		}


	}
}
