using System;
using System.Data;

//using System.EnterpriseServices;
//using System.Runtime.InteropServices;

namespace SAP_SIC_Ventas
{
	/// <summary>
	/// Summary description for clsVentas.
	/// </summary>
	//[Serializable(),JustInTimeActivation(true),Transaction(TransactionOption.Required),Synchronization(SynchronizationOption.Required),ClassInterface(ClassInterfaceType.AutoDual)]
	public class clsVentas
	{
		public clsVentas()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		//[AutoComplete(true)]
		public DataSet Get_ConsultaVendedor(string Oficina)
		{
			DataSet dsReturn = new DataSet();
			try
			{
				SAP_SIC_Ventas proxy = ConectaSAP();
				ZST_PV_VENDEDORTable objVendedor = new ZST_PV_VENDEDORTable();
				proxy.Zpvu_Rfc_Mae_Vendedor(Oficina,ref objVendedor);
				dsReturn.Tables.Add(objVendedor.ToADODataTable());
				proxy.Connection.Close();
			}
			catch (Exception ex)
			{
				dsReturn = null;
			}
            return dsReturn;
		}

		//[AutoComplete(true)]
		public DataSet Get_VerificaVendedor(string Oficina, string Vendedor)
		{
			DataSet dsReturn = new DataSet();
			try
			{
				SAP_SIC_Ventas proxy = ConectaSAP();
				BAPIRET2Table objLog = new BAPIRET2Table();
				proxy.Zpvu_Rfc_Con_Verif_Vendedor(Oficina,Vendedor,ref objLog);
				dsReturn.Tables.Add(objLog.ToADODataTable());
				proxy.Connection.Close();
			}
			catch (Exception ex)
			{
				dsReturn = null;
			}
			return dsReturn;
		}

		//[AutoComplete(true)]
		public DataSet Get_ConsultaCentroCostos(string MotivoPedido, string Oficina, string CentroCosto)
		{
			DataSet dsReturn = new DataSet();
			try
			{
				SAP_SIC_Ventas proxy = ConectaSAP();
				ZST_PV_CENTRO_COSTOTable objCentroCosto = new ZST_PV_CENTRO_COSTOTable();
				proxy.Zpvu_Rfc_Mae_Centro_Costo(CentroCosto,MotivoPedido,Oficina,ref objCentroCosto);
				dsReturn.Tables.Add(objCentroCosto.ToADODataTable());
				proxy.Connection.Close();
			}
			catch (Exception ex)
			{
				dsReturn = null;
			}
			return dsReturn;
		}

		//[AutoComplete(true)]
		public DataSet Get_ConsultaCampana(string FechaVenta,string TipoVenta)
		{
			DataSet dsReturn = new DataSet();
			try
			{
				SAP_SIC_Ventas proxy = ConectaSAP();
				ZST_PV_CAMPANATable objCampana = new ZST_PV_CAMPANATable();
				proxy.Zpvu_Rfc_Mae_Campana(FormatoFecha(FechaVenta),TipoVenta,ref objCampana);
				dsReturn.Tables.Add(objCampana.ToADODataTable());
				proxy.Connection.Close();
			}
			catch (Exception ex)
			{
				dsReturn = null;
			}
			return dsReturn;
		}

		//[AutoComplete(true)]
		public DataSet Get_ConsultaUtilizacion(string FechaVenta, string Campana, string TipoVenta, string Articulo, string TipoOperacion)
		{
			DataSet dsReturn = new DataSet();
			try
			{
				SAP_SIC_Ventas proxy = ConectaSAP();
				ZST_PV_UTILIZACIONTable objUtilizacion = new ZST_PV_UTILIZACIONTable();
				proxy.Zpvu_Rfc_Mae_Utilizacion(Articulo,Campana,FormatoFecha(FechaVenta),TipoOperacion,TipoVenta, ref objUtilizacion);
				dsReturn.Tables.Add(objUtilizacion.ToADODataTable());
				proxy.Connection.Close();
			}
			catch (Exception ex)
			{
				dsReturn = null;
			}
			return dsReturn;
		}

		//[AutoComplete(true)]
		public DataSet Get_ConsultaPlanTarifario(string Utilizacion, string TipoVenta)
		{
			DataSet dsReturn = new DataSet();
			try
			{
				SAP_SIC_Ventas proxy = ConectaSAP();
				ZST_PV_PLAN_TARIFARIOTable objPlanTar = new ZST_PV_PLAN_TARIFARIOTable();
				proxy.Zpvu_Rfc_Mae_Plan_Tarifario("02",TipoVenta,Utilizacion,ref objPlanTar);
				dsReturn.Tables.Add(objPlanTar.ToADODataTable());
				proxy.Connection.Close();
			}
			catch (Exception ex)
			{
				dsReturn = null;
			}
			return dsReturn;
		}

		//[AutoComplete(true)]
		public DataSet Get_ConsultaArticulo(string FechaVenta, string Material, string TipoVenta, string Oficina, string ClaseVenta)
		{
			DataSet dsReturn = new DataSet();
			try
			{
				SAP_SIC_Ventas proxy = ConectaSAP();
				ZST_PV_ARTICULOTable objArticulos = new ZST_PV_ARTICULOTable();
				proxy.Zpvu_Rfc_Mae_Articulo(ClaseVenta,FormatoFecha(FechaVenta),Material,Oficina,TipoVenta, ref objArticulos);
				dsReturn.Tables.Add(objArticulos.ToADODataTable());
				proxy.Connection.Close();
			}
			catch (Exception ex)
			{
				dsReturn = null;
			}
			return dsReturn;
		}

		//[AutoComplete(true)]
		public DataSet Get_LeeTipoDocCliente()
		{
			DataSet dsReturn = new DataSet();
			try
			{
				SAP_SIC_Ventas proxy = ConectaSAP();
				ZST_PV_TIP_DOC_CLIENTETable objTipoDoc = new ZST_PV_TIP_DOC_CLIENTETable();
				proxy.Zpvu_Rfc_Mae_Tip_Doc_Clte(ref objTipoDoc);
				dsReturn.Tables.Add(objTipoDoc.ToADODataTable());
				proxy.Connection.Close();
			}
			catch (Exception ex)
			{
				dsReturn = null;
			}
			return dsReturn;
		}

		//[AutoComplete(true)]
		public DataSet Get_ConsultaMotivoPedido(string Oficina, string Motivo)
		{
			DataSet dsReturn = new DataSet();
			try
			{
				SAP_SIC_Ventas proxy = ConectaSAP();
				ZST_PV_MOTIVO_PEDIDOTable objMotPed = new ZST_PV_MOTIVO_PEDIDOTable();
				proxy.Zpvu_Rfc_Mae_Motivo_Pedido(Motivo,Oficina,ref objMotPed);
				dsReturn.Tables.Add(objMotPed.ToADODataTable());
				proxy.Connection.Close();
			}
			catch (Exception ex)
			{
				dsReturn = null;
			}
			return dsReturn;
		}

		//[AutoComplete(true)]
		public DataSet Get_LeeCuotas()
		{
			DataSet dsReturn = new DataSet();
			try
			{
				SAP_SIC_Ventas proxy = ConectaSAP();
				ZST_PV_CUOTASTable objCuotas = new ZST_PV_CUOTASTable();
				proxy.Zpvu_Rfc_Mae_Cuotas(ref objCuotas);
				dsReturn.Tables.Add(objCuotas.ToADODataTable());
				proxy.Connection.Close();
			}
			catch (Exception ex)
			{
				dsReturn = null;
			}
			return dsReturn;
		}

		//[AutoComplete(true)]
		public DataSet Get_ConsultaClasePedido(string Oficina)
		{
			DataSet dsReturn = new DataSet();
			try
			{
				SAP_SIC_Ventas proxy = ConectaSAP();
				ZST_PV_CLASE_PEDIDOTable objClasePed = new ZST_PV_CLASE_PEDIDOTable();
				proxy.Zpvu_Rfc_Mae_Clase_Pedido(Oficina, ref objClasePed);
				dsReturn.Tables.Add(objClasePed.ToADODataTable());
				proxy.Connection.Close();
			}
			catch (Exception ex)
			{
				dsReturn = null;
			}
			return dsReturn;
		}

		//[AutoComplete(true)]
		public DataSet Get_ConsultaPrecio(string Oficina, string DocumentoOrigen, string Consecutivo, 
			                              string Material, string Utilizacion, decimal Cantidad,
			                              string Fecha, string Serie, string NroTelefono, string TipDocVenta,
			                              string strCadenaSeries, string Canal, string OrgVnt, out decimal Descuento,out decimal PrecIncIGV,
			                              out decimal Precio, out decimal SubTotal)
		{
			DataSet dsReturn = new DataSet();
			string DispIMEI;
	        decimal ValorIGV;
			string[] strTrama;
			int intMax;
            DispIMEI = "";
			try
			{				
				SAP_SIC_Ventas proxy = ConectaSAP();
				ZST_PV_LIN_DOC_IMEITable objLinDocIMEI = new ZST_PV_LIN_DOC_IMEITable();
                ZST_PV_LIN_DOC_IMEI objLineaLinDocIMEI = new ZST_PV_LIN_DOC_IMEI();

				BAPIRET2Table objLog = new BAPIRET2Table();

			    intMax = 4;
                strTrama = strCadenaSeries.Split(';',(char)intMax);
				
				if (strCadenaSeries.Length > 0)
				{
					objLineaLinDocIMEI.Documento = strTrama[0];
					objLineaLinDocIMEI.Consecutivo = strTrama[1];
					objLineaLinDocIMEI.Serie_Inicial = strTrama[2];
					objLineaLinDocIMEI.Serie_Final = strTrama[3];

					objLinDocIMEI.Add(objLineaLinDocIMEI);
				}

				proxy.Zpvu_Rfc_Val_Ser_Tlf_Pre(Canal, Cantidad,Consecutivo,DocumentoOrigen,FormatoFecha(Fecha),Material,
					                           NroTelefono, Oficina,OrgVnt,Serie,TipDocVenta,Utilizacion,DispIMEI,out Descuento,
					                           out PrecIncIGV, out Precio, out SubTotal, out ValorIGV, ref objLinDocIMEI, ref objLog);
					                           
				dsReturn.Tables.Add(objLog.ToADODataTable());
				proxy.Connection.Close();
			}
			catch (Exception ex)
			{
				dsReturn = null;
				Descuento = 0;
				PrecIncIGV = 0;
				Precio = 0;
				SubTotal = 0;
			}
			return dsReturn;
		}

		//[AutoComplete(true)]
		public DataSet Get_ConsultaCierreCaja(string Oficina, string Fecha, string Usuario, out string strRealizado)
		{
			DataSet dsReturn = new DataSet();
			
			try
			{
				SAP_SIC_Ventas proxy = ConectaSAP();
				BAPIRET2Table objLog = new BAPIRET2Table();
				proxy.Zpvu_Rfc_Con_Caja_Cerrada(FormatoFecha(Fecha),Oficina,Usuario,out strRealizado,ref objLog);
				dsReturn.Tables.Add(objLog.ToADODataTable());
				proxy.Connection.Close();
			}
			catch (Exception ex)
			{
				dsReturn = null;
				strRealizado = "";
			}
			return dsReturn;
		}

		//[AutoComplete(true)]
		public DataSet Set_CreaPedidoA(string CadenaCabecera,string CadenaDetalle, string CadenaSeries,
			                           string CadenaServAdic, string[] arrAcuerdo, 
			out string strEntrega,
		    out string strFactura,
		    out string strNroContrato,
			out string strNroDocCliente,
			out string strNroPedido,
			out string strRefHistorico,
			out string strTipDocCliente,
			out decimal dblValorDescuento)
		{
			DataSet dsReturn = new DataSet();
			int intMax1;
			int intMax2;
			int intMax3;
			int intMax4;
			int intMax5;

			string[] strTrama1;
            string[] strTrama2;

						
		//	try
		//	{
				SAP_SIC_Ventas proxy = ConectaSAP();

                ZST_PV_DOCUMENTOTable objDocumento = new ZST_PV_DOCUMENTOTable();
				ZST_PV_LINEA_DOC_VENTATable objLineaDocumento = new ZST_PV_LINEA_DOC_VENTATable();
				ZST_PV_LIN_DOC_IMEITable objLinDocIMEI = new ZST_PV_LIN_DOC_IMEITable();
				ZST_PV_SERVI_CONTRATable objServicios = new ZST_PV_SERVI_CONTRATable();
				ZST_PV_CONTRATOTable objContrato = new ZST_PV_CONTRATOTable();
				BAPIRET2Table objLog = new BAPIRET2Table();

                ZST_PV_DOCUMENTO objFilaDocumento = new ZST_PV_DOCUMENTO();
				ZST_PV_LINEA_DOC_VENTA objFilaLineaDocumento = new ZST_PV_LINEA_DOC_VENTA();
				ZST_PV_LIN_DOC_IMEI objFilaLinDocIMEI = new ZST_PV_LIN_DOC_IMEI();
				ZST_PV_SERVI_CONTRA objFilaServicios = new ZST_PV_SERVI_CONTRA();
				ZST_PV_CONTRATO objFilaContrato = new ZST_PV_CONTRATO();

                intMax1 = 41;
				intMax2 = 29;
				intMax3 = 4;
				intMax4 = 4;
				intMax5 = 62;

				strTrama1 = CadenaCabecera.Split(';',(char)intMax1);

				if (CadenaCabecera.Length > 0)
				{
					objFilaDocumento.Documento = strTrama1[0];
					objFilaDocumento.Tipo_Documento = strTrama1[1];
					objFilaDocumento.Oficina_Venta = strTrama1[2];
					objFilaDocumento.Fecha_Documento = FormatoFecha(strTrama1[3]);
					objFilaDocumento.Tipo_Doc_Cliente = strTrama1[4];
					objFilaDocumento.Cliente = strTrama1[5];
					objFilaDocumento.Augru = strTrama1[6];
					objFilaDocumento.Moneda = strTrama1[7];
					objFilaDocumento.Tipo_Operacion = strTrama1[8];
					objFilaDocumento.Total_Mercaderia = FormatoDec(strTrama1[9]);
					objFilaDocumento.Total_Impuesto = FormatoDec(strTrama1[10]);
					objFilaDocumento.Total_Documento = FormatoDec(strTrama1[11]);
				    objFilaDocumento.Fecha_Registro = FormatoFecha(strTrama1[12]);
					objFilaDocumento.Impreso = strTrama1[13];
					objFilaDocumento.Observacion1 = strTrama1[14];
					objFilaDocumento.Observacion2 = strTrama1[15];
					objFilaDocumento.Tipo_Venta = strTrama1[16];
					objFilaDocumento.Numero_Contrato = strTrama1[17];
					objFilaDocumento.Nro_Referencia = strTrama1[18];
					objFilaDocumento.Usuario_Registro = strTrama1[19];
					objFilaDocumento.Anulado = strTrama1[20];
					objFilaDocumento.Documento_Origen = strTrama1[21];
					objFilaDocumento.Fecha_Vta_Origen = FormatoFecha(strTrama1[22]);
					objFilaDocumento.Nro_Refer_Origen = strTrama1[23];
					objFilaDocumento.Nro_Cuotas = strTrama1[24];
					objFilaDocumento.Nro_Clarify = strTrama1[25];
					objFilaDocumento.Estado = strTrama1[26];
					objFilaDocumento.Vendedor = strTrama1[27];
					objFilaDocumento.Mala_Venta = strTrama1[28];
					objFilaDocumento.Clase_Venta = strTrama1[29];
					objFilaDocumento.Des_Clase_Venta = strTrama1[30];
					objFilaDocumento.Mot_Mala_Venta = strTrama1[31];
					objFilaDocumento.Telefono = strTrama1[32];
					objFilaDocumento.Referencia = strTrama1[33];
					objFilaDocumento.Historico = strTrama1[34];
					objFilaDocumento.Numero_Hdc = strTrama1[35];
					objFilaDocumento.Nro_Pcs_Asociado = strTrama1[36];
					objFilaDocumento.Nro_Ped_Tg = strTrama1[37];
					objFilaDocumento.Nro_Acuer_Alqu = strTrama1[38];
					objFilaDocumento.Trans_Gratuita = strTrama1[39];
					objFilaDocumento.Fidelizacion = strTrama1[40];
//					objFilaDocumento.Vendedor_Dni = strTrama1[41];
//					objFilaDocumento.Nro_Solicitud = strTrama1[42];
//					objFilaDocumento.Serie_Recibida = strTrama1[43];
//					objFilaDocumento.Operador = strTrama1[44];
//					objFilaDocumento.Tipo_Prod_Operad = strTrama1[45];
//					objFilaDocumento.Clase_Ped_Devol = strTrama1[46];
//					objFilaDocumento.Nro_Factura = strTrama1[47];
					objFilaDocumento.Orgvnt = strTrama1[48];
					objFilaDocumento.Canal = strTrama1[49];
					
					objDocumento.Add(objFilaDocumento);
				}


				if (CadenaDetalle.Length > 0 )
				{
					strTrama1 = CadenaDetalle.Split('|');
					for (int i=0;i<strTrama1.Length;i++)
					{
						strTrama2 = strTrama1[i].Split(';',(char)intMax2);

                        objFilaLineaDocumento.Documento = strTrama2[0];
						objFilaLineaDocumento.Consecutivo = strTrama2[1];
						objFilaLineaDocumento.Articulo = strTrama2[2];
						objFilaLineaDocumento.Des_Articulo = strTrama2[3];
						objFilaLineaDocumento.Utilizacion = strTrama2[4];
						objFilaLineaDocumento.Des_Utilizacion = strTrama2[5];
						objFilaLineaDocumento.Campana = strTrama2[6];
						objFilaLineaDocumento.Des_Campana = strTrama2[7];
						objFilaLineaDocumento.Serie = strTrama2[8];
						objFilaLineaDocumento.Cantidad = Convert.ToDecimal(strTrama2[9]);
						objFilaLineaDocumento.Precio = Convert.ToDecimal(strTrama2[10]);
						objFilaLineaDocumento.Precio_Total = Convert.ToDecimal(strTrama2[11]);
						objFilaLineaDocumento.Descuento = Convert.ToDecimal(strTrama2[12]);
						objFilaLineaDocumento.Porc_Descuento = Convert.ToDecimal(strTrama2[13]);
						objFilaLineaDocumento.Descuento_Adic = Convert.ToDecimal(strTrama2[14]);
						objFilaLineaDocumento.Subtotal = Convert.ToDecimal(strTrama2[15]);
						objFilaLineaDocumento.Impuesto1 = Convert.ToDecimal(strTrama2[16]);
						objFilaLineaDocumento.Impuesto2 = FormatoDec(strTrama2[17]);
						objFilaLineaDocumento.Plan_Tarifario = strTrama2[18];
						objFilaLineaDocumento.Des_Plan_Tarifar = strTrama2[19];
						objFilaLineaDocumento.Centro_Costo = strTrama2[20];
						objFilaLineaDocumento.Motivo_Devolucio = strTrama2[21];
						objFilaLineaDocumento.Asociado = strTrama2[22];
						objFilaLineaDocumento.Consecutivo_Padr = FormatoDecSTR(strTrama2[23]);
						objFilaLineaDocumento.Articulo_Asociac = strTrama2[24];
						objFilaLineaDocumento.Numero_Telefono = strTrama2[25];
						objFilaLineaDocumento.Nro_Clarify =FormatoDecSTR(strTrama2[26]);
						objFilaLineaDocumento.Dev_Componente = strTrama2[27];
						//objFilaLineaDocumento.Serie_Ant = strTrama2[28];
						objLineaDocumento.Add(objFilaLineaDocumento);
						objFilaLineaDocumento = null;
						objFilaLineaDocumento = new ZST_PV_LINEA_DOC_VENTA();
					}
				}


				if (CadenaSeries.Length > 0)
				{
                    strTrama1 = CadenaSeries.Split('|');
					for (int i=0;i<strTrama1.Length;i++)
					{
						strTrama2 = strTrama1[i].Split(';',(char)intMax3);

						objFilaLinDocIMEI.Documento = strTrama2[0];
						objFilaLinDocIMEI.Consecutivo = strTrama2[1];
						objFilaLinDocIMEI.Serie_Inicial = strTrama2[2];
						objFilaLinDocIMEI.Serie_Final = strTrama2[3];

						objLinDocIMEI.Add(objFilaLinDocIMEI);

						objFilaLinDocIMEI = null;
						objFilaLinDocIMEI = new ZST_PV_LIN_DOC_IMEI();
					}
				}

				if (CadenaServAdic.Length > 0)
				{
					strTrama1 = CadenaServAdic.Split('|');
					for (int i=0;i<strTrama1.Length;i++)
					{
						strTrama2 = strTrama1[i].Split(';',(char)intMax4);

						objFilaServicios.Documento  = strTrama2[0];
						objFilaServicios.Consecutivo = strTrama2[1];
						objFilaServicios.Servicio_Solicit = strTrama2[2];
						objFilaServicios.Valor_Selecciona = strTrama2[3];

						objServicios.Add(objFilaServicios);
					}
				}


				// La parte de generacion de acuerdo se deja de lado momentaneamente
				/*if (arrAcuerdo.Length>0)
				{
					DataTable dtAcuerdo = new DataTable();
					DataRow drFila;

					for (int i=0;i<intMax5;i++)
					{
						dtAcuerdo.Columns.Add(Convert.ToString(i));
					}

					drFila = dtAcuerdo.NewRow();
					for (int i=0;i<arrAcuerdo.Length;i++)
					{
						drFila[i] = arrAcuerdo[i];
					}
					dtAcuerdo.Rows.Add(drFila);

					objContrato.FromADODataTable(dtAcuerdo);
				}*/

				proxy.Zpvu_Rfc_Trs_Pedido("","","0000/00/00","","",out strEntrega,out strFactura,out strNroContrato,
					                      out strNroDocCliente,out strNroPedido,out strRefHistorico,
					                      out strTipDocCliente,out dblValorDescuento,ref objContrato,
					                      ref objDocumento,ref objLinDocIMEI,ref objLineaDocumento,
					                      ref objLog,ref objServicios);

				dsReturn.Tables.Add(objDocumento.ToADODataTable());
				dsReturn.Tables.Add(objLog.ToADODataTable());
			    proxy.Connection.Close();
			/*}
			catch (Exception ex)
			{
				dsReturn = null;
				strEntrega = "";
		    strFactura = "";
		    strNroContrato = "";
			strNroDocCliente = "";
			strNroPedido = "";
			strRefHistorico = "";
			strTipDocCliente = "";
			dblValorDescuento  = 0;
			}*/
			return dsReturn;
		}

        //[AutoComplete(true)]
		public DataSet Set_ActualizaEstadoPedido(string NroPedido,
												 string OficinaVenta,
												 string Almacenero,
												 string Despachador)
		{
			DataSet dsReturn = new DataSet();
			
			try
			{
				SAP_SIC_Ventas proxy = ConectaSAP();
				BAPIRET2Table objLog = new BAPIRET2Table();
				proxy.Zpvu_Rfc_Trs_Act_Estado_Pedido(Almacenero,Despachador,NroPedido,OficinaVenta,ref objLog);
				dsReturn.Tables.Add(objLog.ToADODataTable());
				proxy.Connection.Close();
			}
			catch (Exception ex)
			{
				dsReturn = null;
			}
			return dsReturn;
		}

		//[AutoComplete(true)]
		public DataSet Set_ActualizaCreaCliente(string OficinaVta, string[] arrCliente)
		{
			DataSet dsReturn = new DataSet();
			DataTable dtCliente = new DataTable();
			DataRow drCliente;
			
			//try
			//{
				SAP_SIC_Ventas proxy = ConectaSAP();
				BAPIRET2Table objLog = new BAPIRET2Table();
				ZST_PV_CLIENTETable objCliente = new ZST_PV_CLIENTETable();
				ZST_PV_CLIENTE objLinCliente;
                

				dtCliente = objCliente.ToADODataTable();

				drCliente = dtCliente.NewRow();

				for (int i=0;i<arrCliente.Length;i++)
				{
					drCliente[i] = arrCliente[i];
				}

				dtCliente.Rows.Add(drCliente);

				objCliente.FromADODataTable(dtCliente);

				objLinCliente = objCliente[0];
								
				objLinCliente.Fecha_Nacimiento = FormatoFecha(objLinCliente.Fecha_Nacimiento);

				objCliente.RemoveAt(0);
				objCliente.Add(objLinCliente);

				proxy.Zpvu_Rfc_Trs_Cliente(OficinaVta,ref objLog, ref objCliente);
				dsReturn.Tables.Add(objCliente.ToADODataTable());
				dsReturn.Tables.Add(objLog.ToADODataTable());
				proxy.Connection.Close();
			//}
			/*catch (Exception ex)
			{
				dsReturn = null;
			}*/
			return dsReturn;

		}

		//[AutoComplete(true)]
		public DataSet Get_ConsultaIMEITelf(string OficinaVenta, string MATERIAL,
											string NroResultados, string NroTelefono)
		{
			DataSet dsReturn = new DataSet();			
			//Console.Write("Metodo Get_ConsultaIMEITelf");
			//Response.write("Get_ConsultaIMEITelf");
			//Response.end();
			try
			{
				SAP_SIC_Ventas proxy = ConectaSAP();
				ZST_PVU_IMEISTable objIMEIS = new ZST_PVU_IMEISTable();
				BAPIRET2Table objLog = new BAPIRET2Table();
				proxy.Zpvu_Rfc_Con_Imeis(MATERIAL,NroResultados,NroTelefono,OficinaVenta,ref objIMEIS, ref objLog);
				dsReturn.Tables.Add(objIMEIS.ToADODataTable());
				dsReturn.Tables.Add(objLog.ToADODataTable());
				proxy.Connection.Close();
			}
			catch (Exception ex)
			{
				dsReturn = null;
			}
			return dsReturn;
		}

		//[AutoComplete(true)]
		public DataSet Get_GroupArt()
		{
			DataSet dsReturn = new DataSet();
			DataTable dtGrupo = new DataTable();
			
			//try
			//{
				SAP_SIC_Ventas proxy = ConectaSAP();
				ZST_PV_GRUPO_ARTICULOTable objChips = new ZST_PV_GRUPO_ARTICULOTable();
				ZST_PV_GRUPO_ARTICULOTable objPacks = new ZST_PV_GRUPO_ARTICULOTable();
				ZST_PV_GRUPO_ARTICULOTable objServ = new ZST_PV_GRUPO_ARTICULOTable();
				proxy.Zpvu_Rfc_Con_Gpos_Articulo(ref objChips, ref objPacks,ref objServ);

				dtGrupo = objChips.ToADODataTable();
                dtGrupo.TableName = "CHIPS";
				dsReturn.Tables.Add(dtGrupo);

				dtGrupo = objPacks.ToADODataTable();
				dtGrupo.TableName = "PACKS";
				dsReturn.Tables.Add(dtGrupo);

				dtGrupo = objServ.ToADODataTable();
				dtGrupo.TableName = "SERV";
				dsReturn.Tables.Add(dtGrupo);
				proxy.Connection.Close();
		//	}
			/*catch (Exception ex)
			/{
				dsReturn = null;
			}*/
			return dsReturn;
		}

		//[AutoComplete(true)]
		public DataSet Get_ConsultaCampanaAsociada(string CampanaPadre, string CampanaHijo)
		{
			DataSet dsReturn = new DataSet();
			
			try
			{
				SAP_SIC_Ventas proxy = ConectaSAP();
				ZST_PV_CAMPANA_ASOCTable objCampanas = new ZST_PV_CAMPANA_ASOCTable();
				proxy.Zpvu_Rfc_Mae_Campana_Asociada(CampanaHijo,CampanaPadre,ref objCampanas);
				dsReturn.Tables.Add(objCampanas.ToADODataTable());
				proxy.Connection.Close();
			
			}
			catch (Exception ex)
			{
				dsReturn = null;
			}
			return dsReturn;
		}
		
		//[AutoComplete(true)]
		public string Get_ConsultaSaldoRecarga(string Oficina)
		{
			decimal decSaldo;
			string strSaldo;
			try
			{
				if (Oficina.Substring(0,1) != "S")
				{
					SAP_SIC_Ventas proxy = ConectaSAP();
					proxy.Zpvu_Rfc_Con_Sald_Recarga(Oficina,out decSaldo);
					strSaldo = decSaldo.ToString("0.00");
					proxy.Connection.Close();
				}
				else
				{
					strSaldo = "";
				}
			}
			catch (Exception ex)
			{
				strSaldo = "";
			}
			return strSaldo;

		}

		//[AutoComplete(true)]
		public string Get_ConsultaVendedorRecarga(string Oficina,string strCanal)
		{
			string strVendedor;
			try
			{
				if (Oficina.Substring(0,1) != "S")
				{
					SAP_SIC_Ventas proxy = ConectaSAP();
					proxy.Zpvu_Rfc_Con_Vend_Recarga(strCanal,out strVendedor);
					proxy.Connection.Close();
				}
				else
				{
					strVendedor = "";
				}
			}
			catch (Exception ex)
			{
				strVendedor = "";
			}
			return strVendedor;
		}

		public DataSet Get_LeeDepartamento()
		{
            DataSet dsReturn = new DataSet();
			try
			{
                ZST_PV_DEPARTAMENTOTable objDepartamento = new ZST_PV_DEPARTAMENTOTable();
				SAP_SIC_Ventas proxy = ConectaSAP();
                proxy.Zpvu_Rfc_Mae_Departamento(ref objDepartamento);
	            dsReturn.Tables.Add(objDepartamento.ToADODataTable());
				proxy.Connection.Close();
			}
			catch (Exception ex)
			{
              dsReturn = null;
			}
			return dsReturn;
		}

		public DataSet Get_LeeProvincia()
		{
			DataSet dsReturn = new DataSet();
			try
			{
				ZST_PV_PROVINCIATable objProvincia = new ZST_PV_PROVINCIATable();
				SAP_SIC_Ventas proxy = ConectaSAP();
				proxy.Zpvu_Rfc_Mae_Provincia(ref objProvincia);
				dsReturn.Tables.Add(objProvincia.ToADODataTable());
				proxy.Connection.Close();
			}
			catch (Exception ex)
			{
				dsReturn = null;
			}
			return dsReturn;
		}

		public DataSet Get_LeeDistrito()
		{
			DataSet dsReturn = new DataSet();
			try
			{
				ZST_PV_DISTRITOTable objDistrito = new ZST_PV_DISTRITOTable();
				SAP_SIC_Ventas proxy = ConectaSAP();
				proxy.Zpvu_Rfc_Mae_Distrito(ref objDistrito);
				dsReturn.Tables.Add(objDistrito.ToADODataTable());
				proxy.Connection.Close();
			}
			catch (Exception ex)
			{
				dsReturn = null;
			}
			return dsReturn;
		}

		public DataSet Get_LeeEstadoCivil()
		{
			DataSet dsReturn = new DataSet();
			try
			{
				ZST_PV_EST_CIVILTable objEstCiv = new ZST_PV_EST_CIVILTable();
				SAP_SIC_Ventas proxy = ConectaSAP();
				proxy.Zpvu_Rfc_Mae_Est_Civil(ref objEstCiv);
				dsReturn.Tables.Add(objEstCiv.ToADODataTable());
				proxy.Connection.Close();
			}
			catch (Exception ex)
			{
				dsReturn = null;
			}
			return dsReturn;
		}

		public bool Set_AnulaTelefonoPortable(string NroTelefono,ref string mensaje)
		{
			bool valor = false;
			try
			{				
				SAP_SIC_Ventas proxy = ConectaSAP();
				MFORM objFORM = new MFORM();
				proxy.Zpvu_Rfc_Trs_Del_Nro_Tel(NroTelefono,out objFORM);

				if (objFORM.Ltype == "0")
				{
					valor =true;
				}
				else
				{
					valor = false;
					mensaje = CheckStr(objFORM.Linda);
				}

				proxy.Connection.Close();
			}
			catch (Exception ex)
			{
				valor = false;
				mensaje = ex.Message;
			}
			return valor;
		}

		public DataSet Set_ActualizaCreaClienteSap(string OficinaVta, string[] arrCliente)
		{
			DataSet dsReturn = new DataSet();
			
			//try
			//{
			SAP_SIC_Ventas proxy = ConectaSAP();
			BAPIRET2Table objLog = new BAPIRET2Table();
			ZST_PV_CLIENTETable objCliente = new ZST_PV_CLIENTETable();
			ZST_PV_CLIENTE objLinCliente = new ZST_PV_CLIENTE();
              
			if (arrCliente[1] != "" && arrCliente[2] != "")
			{
				//objLinCliente = objCliente[0];
				objLinCliente.Cliente=arrCliente[0];
				objLinCliente.Tipo_Doc_Cliente=arrCliente[1];
				objLinCliente.Tipo_Cliente=arrCliente[2];
				objLinCliente.Nombre=arrCliente[3];
				objLinCliente.Apellido_Paterno=arrCliente[4];
				objLinCliente.Apellido_Materno=arrCliente[5];
				objLinCliente.Razon_Social=arrCliente[6];
				objLinCliente.Fecha_Nacimiento=arrCliente[7];
				objLinCliente.Telefono=arrCliente[8];
				objLinCliente.Fax=arrCliente[9];
				objLinCliente.E_Mail=arrCliente[10];
				objLinCliente.Nombre_Conyuge=arrCliente[11];
				objLinCliente.Carga_Familiar=arrCliente[12];
				objLinCliente.Sexo=arrCliente[13];
				objLinCliente.Calle_Legal=arrCliente[14];
				objLinCliente.Ubigeo_Legal=arrCliente[15];
				objLinCliente.Calle_Fact=arrCliente[16];
				objLinCliente.Ubigeo_Fact=arrCliente[17];
				objLinCliente.Replegal_Tip_Doc=arrCliente[18];
				objLinCliente.Replegal_Nro_Doc=arrCliente[19];
				objLinCliente.Replegal_Nombre=arrCliente[20];
				objLinCliente.Replegal_Ape_Pat=arrCliente[21];
				objLinCliente.Replegal_Ape_Mat=arrCliente[22];
				objLinCliente.Replegal_Telefon=arrCliente[23];
				objLinCliente.Contacto_Tip_Doc=arrCliente[24];
				objLinCliente.Contacto_Nro_Doc=arrCliente[25];
				objLinCliente.Contacto_Nombre=arrCliente[26];
				objLinCliente.Contacto_Ape_Pat=arrCliente[27];
				objLinCliente.Contacto_Ape_Mat=arrCliente[28];
				objLinCliente.Contacto_Telefon=arrCliente[29];
				objLinCliente.Cargo=arrCliente[30];
				objLinCliente.Dependiente=arrCliente[31];
				objLinCliente.Empresa_Labora=arrCliente[32];
				objLinCliente.Empresa_Cargo=arrCliente[33];
				objLinCliente.Empresa_Telefono=arrCliente[34];
				objLinCliente.Actividad=arrCliente[35];
				objLinCliente.Ing_Bruto=FormatoDec(arrCliente[36]); //decimal
				objLinCliente.Otros_Ingresos=FormatoDec(arrCliente[37]); //decimal
				objLinCliente.Tarjeta_Credito=arrCliente[38];
				objLinCliente.Num_Tarj_Credito=arrCliente[39];
				objLinCliente.Moneda_Tcred=arrCliente[40];
				objLinCliente.Linea_Credito=FormatoDec(arrCliente[41]); //decimal
				objLinCliente.Fecha_Venc_Tcred=arrCliente[42];
				objLinCliente.Clasificacion=arrCliente[43];
				objLinCliente.Clase_Cliente=arrCliente[44];
				objLinCliente.Ramo=arrCliente[45];
				objLinCliente.Observaciones=arrCliente[46];
				objLinCliente.Estado_Civil=arrCliente[47];
				objLinCliente.Tit_Cliente=arrCliente[48];
				objLinCliente.Replegal_Tit=arrCliente[49];
				objLinCliente.Replegal_Fnac=arrCliente[50];
				objLinCliente.Replegal_Sexo=arrCliente[51];
				objLinCliente.Replegal_Est_Civ=arrCliente[52];
				objLinCliente.Ktokd=arrCliente[53];
				objLinCliente.Refer_Direccion=arrCliente[54];
				objLinCliente.Telf_Pref=arrCliente[55];
				objLinCliente.Fax_Pref=arrCliente[56];
				objLinCliente.Telef_Legal=arrCliente[57];
				objLinCliente.Operador=arrCliente[58];
				objLinCliente.Denom_Operador=arrCliente[59];
				objLinCliente.Tipo_Prod_Operad=arrCliente[60];
				objLinCliente.Denom_Tpo_Prod_Op=arrCliente[61];
				objLinCliente.Telef_Legal_Pref=arrCliente[62];
				objLinCliente.Refer_Legal=arrCliente[63];
				objLinCliente.Kunnr=arrCliente[64];
			}
			objCliente.Add(objLinCliente);

			proxy.Zpvu_Rfc_Trs_Cliente(OficinaVta,ref objLog, ref objCliente);
			dsReturn.Tables.Add(objCliente.ToADODataTable());
			dsReturn.Tables.Add(objLog.ToADODataTable());
			proxy.Connection.Close();
			//}
			/*catch (Exception ex)
			{
				dsReturn = null;
			}*/
			return dsReturn;

		}

		private SAP_SIC_Ventas ConectaSAP()
		{
			Seguridad_NET.clsSeguridad objSeg = new Seguridad_NET.clsSeguridad();
				
			string strCliente= objSeg.BaseDatos;
			string strUsuario= objSeg.Usuario;
			string strPassword= objSeg.Password;
			string strLanguage= objSeg.Idioma;
			string strApplicationServer = objSeg.Servidor;
			string strSistema = objSeg.Sistema;
			string strLoadBal = objSeg.LoadBal;
			string strMessServ = objSeg.MessServ;
			string strR3Name = objSeg.R3Name;
			string strGroup = objSeg.Group;

			SAP_SIC_Ventas proxy;

			if (strLoadBal == "0")
			{
				proxy = new SAP_SIC_Ventas("CLIENT=" + strCliente + " USER=" + strUsuario + " PASSWD=" + strPassword + " LANG=" + strLanguage +" ASHOST=" + strApplicationServer + " SYSNR=" + strSistema);
			}
			else
			{
				proxy = new SAP_SIC_Ventas("CLIENT=" + strCliente + " USER=" + strUsuario + " PASSWD=" + strPassword + " LANG=" + strLanguage +" MSHOST=" + strMessServ + " R3NAME=" + strR3Name + " GROUP=" + strGroup);
			}

			return proxy;
		}


		private string FormatoFecha(string Fecha)
		{
			if (Fecha.Length > 0 )
				return Fecha.Substring(6,4) + "/" + Fecha.Substring(3,2) + "/" + Fecha.Substring(0,2);
			else
				return "0000/00/00";
		}

		private decimal FormatoDec(string Valor)
		{
			decimal res = 0;
			if (Valor.Trim() != "")
			{
				res = Convert.ToDecimal(Valor);
			}
			return res;
		}

		private string FormatoDecSTR(string Valor)
		{
			string res = "0";
			if (Valor.Trim() != "")
			{
				res = Valor;
			}
			return res;
		}

		private string CheckStr(object Valor) 
		{ 
			string salida="";
			if (Valor == null || Valor == System.DBNull.Value)
				salida = "";			
			else
				salida = Valor.ToString();			
			return salida.Trim();
		}

		public void ConsultarMaterial(string p_codigo_iccid_imei, ref string codMaterial, ref string descMaterial) 
		{
			string out_codigo, out_descripcion;			
			try
			{
				SAP_SIC_Ventas proxy = ConectaSAP();

				proxy.Zsisa_Rfc_Con_Material(p_codigo_iccid_imei, out out_codigo, out out_descripcion);

				codMaterial = out_codigo;
				descMaterial = out_descripcion;

				proxy.Connection.Close();
			}
			catch (Exception ex)
			{
				throw ex;
			}			
		}

	}
}
