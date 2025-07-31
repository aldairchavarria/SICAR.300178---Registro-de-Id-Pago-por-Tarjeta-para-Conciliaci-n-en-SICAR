using System;
using System.Data;
//using System.EnterpriseServices;
//using System.Runtime.InteropServices;

namespace SAP_SIC_Pagos
{
	/// <summary>
	/// Summary description for clsPagos.
	/// </summary>
	///
//	[Serializable(),JustInTimeActivation(true),Transaction(TransactionOption.Required),Synchronization(SynchronizationOption.Required),ClassInterface(ClassInterfaceType.AutoDual)]
	public class clsPagos 
	{
		public clsPagos()
		{
			//
			// TODO: Add constructor logic here
			//
		}

//        //[AutoComplete(true)]
		public DataSet Get_ConsultaPoolFactura(string OficinaVenta, string FechaVenta, 
                                               string TipoPool, string FechaHasta, 
											   string NroDocCliente, string TipoDocCliente,
    		                                   string NumRegistros, string MostrarPagina)
		{
         

			DataSet dsReturn = new DataSet();

			//try
			//{
				
				string strFecha;
				string nroTelefono;
				DataTable dt = new DataTable();

				ZST_PV_POOL_PAGOSTable  objPagos =new ZST_PV_POOL_PAGOSTable();
				BAPIRET2Table objReturn = new BAPIRET2Table();
 
				SAP_SIC_Pagos proxy = ConectaSAP();
				
				proxy.Zpvu_Rfc_Trs_Pool_Facturas(FormatoFecha(FechaHasta),FormatoFecha(FechaVenta),NroDocCliente,OficinaVenta,
					TipoDocCliente,TipoPool,ref objPagos,ref objReturn);
				
				dt = objPagos.ToADODataTable();

				for (int i=0;i<dt.Rows.Count;i++)
				{
					strFecha = (string)dt.Rows[i]["FKDAT"];
					strFecha = strFecha.Substring(6,2) + "/" + strFecha.Substring(4,2) + "/" + strFecha.Substring(0,4);
					dt.Rows[i]["FKDAT"] = strFecha;

					dt.Rows[i]["XBLNR"] = ((string)dt.Rows[i]["XBLNR"]).Substring(1);

					nroTelefono = (string)dt.Rows[i]["NUMBR"];
					nroTelefono = FormatoTelefono(nroTelefono);
					dt.Rows[i]["NUMBR"] = nroTelefono;
				}


				dsReturn.Tables.Add(dt);
				dsReturn.Tables.Add(objReturn.ToADODataTable());
			//}
			//catch(Exception ex)
			//{
			//	dsReturn=null;
			//}
            proxy.Connection.Close();
            return dsReturn;
		}

		//[AutoComplete(true)]
		public DataSet Get_ConsultaViasPago(string OficinaVenta)
		{
         

			DataSet dsReturn = new DataSet();

			try
			{

				ZST_PV_VIAS_PAGOTable objViasPago = new ZST_PV_VIAS_PAGOTable();
				 
				SAP_SIC_Pagos  proxy = ConectaSAP();

				proxy.Zpvu_Rfc_Mae_Vias_Pago(OficinaVenta,ref objViasPago);
				
				dsReturn.Tables.Add(objViasPago.ToADODataTable());
				proxy.Connection.Close();
			}
			catch(Exception ex)
			{
				dsReturn=null;
			}
            
			return dsReturn;
		}

        //[AutoComplete(true)]
		public DataSet Set_NumeroSUNAT(string NroDocumento, string NroAsignar,
									   string NroRefFranquicia, string OficinaVenta, 
									   string Reasignar,
			                           string FechaContabilidad, 
									   string CadenaPagos, 
									   string NroRefCorner,
									   string DocCadena,
                                       string Usuario, 
									   string ImprCaja, 
									   string NroNotaCredito,
			                           out string strNroRefAsignado)
		{
         
			DataSet dsReturn = new DataSet();
		//	try
		//	{
				BAPIRET2Table objLog = new BAPIRET2Table();
				ZES_PAGOSTable objPagos = new ZES_PAGOSTable();
				ZES_PAGOS objFilaPago = new ZES_PAGOS();

				string[] strTrama1;
				string[] strTrama2;
				int intMax = 14;
				 
				SAP_SIC_Pagos proxy = ConectaSAP();
				
				if (NroAsignar.Trim()  == "") 
                  NroAsignar = "0";
             
				strTrama1 = CadenaPagos.Split('|');
				for (int i=0;i<strTrama1.Length;i++)
				{
					strTrama2 = strTrama1[i].Split(';',(char)intMax);

					if (strTrama2.Length > 1)
					{
						objFilaPago.Org_Vtas = strTrama2[0];
						if (strTrama2.Length>1) 
						{
							objFilaPago.Of_Vtas = strTrama2[1];
						}
						if (strTrama2.Length>2) 
						{
							objFilaPago.Via_Pago = strTrama2[2];
						}
						if (strTrama2.Length>3) 
						{
							objFilaPago.Conc_Bsqda = strTrama2[3];
						}
						if (strTrama2.Length>4) 
						{ 
							objFilaPago.Solicitante = strTrama2[4];
						}
						if (strTrama2.Length>5) 
						{
							objFilaPago.Importe = strTrama2[5];
						}
						if (strTrama2.Length>6) 
						{
							objFilaPago.Moneda = strTrama2[6];
						}
						if (strTrama2.Length>7) objFilaPago.T_Cambio = strTrama2[7];
						if (strTrama2.Length>8) objFilaPago.Referencia = strTrama2[8];
						if (strTrama2.Length>9) objFilaPago.Glosa = strTrama2[9];
						if (strTrama2.Length>10) objFilaPago.F_Pedido = strTrama2[10];
						if (strTrama2.Length>11) objFilaPago.Cond_Pago = strTrama2[11];
						if (strTrama2.Length>12) objFilaPago.Nro_Exactus = strTrama2[12];
						if (strTrama2.Length>13) objFilaPago.Pos = strTrama2[13];
						objPagos.Add(objFilaPago);
						objFilaPago = null;
						objFilaPago = new ZES_PAGOS();
					}
				}

				


				proxy.Zpvu_Rfc_Trs_Set_Nro_Sunat(ImprCaja,FormatoFecha(FechaContabilidad),NroNotaCredito,NroAsignar,
					                             NroDocumento,NroRefCorner,NroRefFranquicia,OficinaVenta,Reasignar,
												 DocCadena,Usuario, out strNroRefAsignado, ref objPagos, ref objLog);  
					                             
				dsReturn.Tables.Add(objLog.ToADODataTable());
			//}
			//catch(Exception ex)
			//{
			//	strNroRefAsignado = "";
			//	dsReturn = null;
			//}
			proxy.Connection.Close();
			return dsReturn;
		}

        //[AutoComplete(true)]
		public DataSet Get_NumeroSUNAT(string OficinaVenta, 
			string ClaseFactura,
			string ImprCaja, 
			string Trae_Nro_Actual,
			out string strReferencia,
			out string strUltNumero)
		{
         

			DataSet dsReturn = new DataSet();

			try
			{
				BAPIRET2Table objLog = new BAPIRET2Table();
								 
				SAP_SIC_Pagos proxy = ConectaSAP();

				proxy.Zpvu_Rfc_Trs_Get_Nro_Sunat(ImprCaja,ClaseFactura,OficinaVenta,Trae_Nro_Actual, out strReferencia, out strUltNumero, ref objLog);
				
				dsReturn.Tables.Add(objLog.ToADODataTable());
				proxy.Connection.Close();
			}
			catch(Exception ex)
			{
				strReferencia = "";
				strUltNumero = "";
				dsReturn = null;
			}
            return dsReturn;
			
		}

        //[AutoComplete(true)]
		public DataSet Get_RegistroPagos(string FechaContabilidad, 
			string CadenaPagos,
			string Usuario, 
			string NotaCredito)
		{
         

			DataSet dsReturn = new DataSet();
			string[] strTrama1;
			string[] strTrama2;

		//	try
		//	{
				ZES_PAGOSTable objPagos = new ZES_PAGOSTable();
				ZST_PV_LOGTable objLog = new ZST_PV_LOGTable();
								 
				SAP_SIC_Pagos proxy = ConectaSAP();
                
				ZES_PAGOS objFilaPago = new ZES_PAGOS();
				int intMax = 14;

				strTrama1 = CadenaPagos.Split('|');
				for (int i=0;i<strTrama1.Length;i++)
				{
					strTrama2 = strTrama1[i].Split(';',(char)intMax);

					if (strTrama2.Length > 0)
					{
						objFilaPago.Org_Vtas = strTrama2[0];
						objFilaPago.Of_Vtas = strTrama2[1];
						objFilaPago.Via_Pago = strTrama2[2];
						objFilaPago.Conc_Bsqda = strTrama2[3];
						objFilaPago.Solicitante = strTrama2[4];
						objFilaPago.Importe = strTrama2[5];
						objFilaPago.Moneda = strTrama2[6];
						objFilaPago.T_Cambio = strTrama2[7];
						objFilaPago.Referencia = strTrama2[8];
						objFilaPago.Glosa = strTrama2[9];
						objFilaPago.F_Pedido = strTrama2[10];
						objFilaPago.Cond_Pago = strTrama2[11];
						objFilaPago.Nro_Exactus = strTrama2[12];
						objFilaPago.Pos = strTrama2[13];
						objPagos.Add(objFilaPago);
						objFilaPago = null;
						objFilaPago = new ZES_PAGOS();
					}
				}


				proxy.Zpvu_Rfc_Trs_Pagos(FormatoFecha(FechaContabilidad),NotaCredito,Usuario,ref objLog,ref objPagos);
					                             
				
				dsReturn.Tables.Add(objLog.ToADODataTable());
		//	}
		//	catch(Exception ex)
		//	{
		//		dsReturn = null;
		//	}
            proxy.Connection.Close();
			return dsReturn;

			
		}

        //[AutoComplete(true)]
		public DataSet Get_RegistroCompensacion(string FechaContabilidad, string Factura, string NotaCredito, string Usuario)
		{
			
			DataSet dsReturn = new DataSet();

			try
			{
				ZST_PV_LOGTable objLog = new ZST_PV_LOGTable();

				SAP_SIC_Pagos proxy = ConectaSAP();

				proxy.Zpvu_Rfc_Trs_Pagos_Con_Nc(Factura,FormatoFecha(FechaContabilidad),NotaCredito,Usuario,ref objLog);

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
		public DataSet Get_RegistroAnulCompensacion(string FechaContabilidad, string OficinaVenta, string NotaCredito, string Usuario)
		{
			
			DataSet dsReturn = new DataSet();

			try
			{
				ZST_PV_LOGTable objLog = new ZST_PV_LOGTable();

				SAP_SIC_Pagos proxy = ConectaSAP();

				proxy.Zpvu_Rfc_Trs_Anulac_Compens_Nc(FormatoFecha(FechaContabilidad),NotaCredito,OficinaVenta,Usuario,ref objLog);

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
		public DataSet Get_LeeTipoDocCliente()
		{
			
			DataSet dsReturn = new DataSet();

			try
			{
				ZST_PV_TIP_DOC_CLIENTETable objTipDocCliente = new ZST_PV_TIP_DOC_CLIENTETable();

				SAP_SIC_Pagos proxy = ConectaSAP();

				proxy.Zpvu_Rfc_Mae_Tip_Doc_Clte(ref objTipDocCliente);

				dsReturn.Tables.Add(objTipDocCliente.ToADODataTable());
				proxy.Connection.Close();

			}
			catch (Exception ex)
			{
				dsReturn = null;
			}

			return dsReturn;

		}

        //[AutoComplete(true)]
		public DataSet Get_ConsultaPedido(string Referencia, string Oficina, string NroSAP, string TipoDoc)
		{
			DataSet dsReturn = new DataSet();

			try
			{
                ZST_PV_DOCUMENTOTable objDocumento = new ZST_PV_DOCUMENTOTable();
				ZST_PV_LINEA_DOC_VENTATable objLineaDoc = new ZST_PV_LINEA_DOC_VENTATable();
				ZST_PV_LIN_DOC_IMEITable objLineaDocIMEI = new ZST_PV_LIN_DOC_IMEITable();
				ZST_PV_SERVI_CONTRATable objServicioCont = new ZST_PV_SERVI_CONTRATable();
				BAPIRET2Table objReturn = new BAPIRET2Table();

				SAP_SIC_Pagos proxy = ConectaSAP();

				proxy.Zpvu_Rfc_Con_Pedido(NroSAP,Oficina,Referencia,TipoDoc,ref objDocumento, ref objLineaDocIMEI, ref objLineaDoc, ref objReturn, ref objServicioCont);


				dsReturn.Tables.Add(objDocumento.ToADODataTable());
				dsReturn.Tables.Add(objLineaDoc.ToADODataTable());
				dsReturn.Tables.Add(objLineaDocIMEI.ToADODataTable());
				dsReturn.Tables.Add(objServicioCont.ToADODataTable());
				dsReturn.Tables.Add(objReturn.ToADODataTable());
                proxy.Connection.Close();

			}
			catch (Exception ex)
			{
				dsReturn = null;
			}

			return dsReturn;
		}

		//[AutoComplete(true)]
		public DataSet ConsultaTriacionPrePost(string pinrodoc, string pitlfprepago, string pioficinavta, string piEstado)
		{

			DataSet dsReturn = new DataSet();
			DataTable dt = new DataTable();
			string strFecha;
			try
			{
				ZST_PVU_TRIACIONES_CONSULTATable objTriacionesCons = new ZST_PVU_TRIACIONES_CONSULTATable();
				BAPIRET2Table objReturn = new BAPIRET2Table();

				SAP_SIC_Pagos proxy = ConectaSAP();

				proxy.Zpvu_Rfc_Con_Triacion_Pre_Post(pinrodoc,piEstado,pioficinavta,pitlfprepago,ref objReturn,ref objTriacionesCons);

				dt = objTriacionesCons.ToADODataTable();

				for (int i=0;i<dt.Rows.Count;i++)
				{
					strFecha = (string)dt.Rows[i]["FECHA_CREACION"];
					strFecha = strFecha.Substring(6,2) + "/" + strFecha.Substring(4,2) + "/" + strFecha.Substring(0,4);
					dt.Rows[i]["FECHA_CREACION"] = strFecha;
				}

				dsReturn.Tables.Add(dt);
				dsReturn.Tables.Add(objReturn.ToADODataTable());
                proxy.Connection.Close();

			}
			catch (Exception ex)
			{
				dsReturn = null;
			}

			return dsReturn;

		}

		//[AutoComplete(true)]
		public DataSet Set_TriacionPrePost(string CadenaTriacion)
		{

			DataSet dsReturn = new DataSet();
			string[] strTrama;
			try
			{
				ZST_PVU_TRIACIONESTable objTriaciones = new ZST_PVU_TRIACIONESTable();
				BAPIRET2Table objReturn = new BAPIRET2Table();
				ZST_PVU_TRIACIONES objFilaTriaciones = new ZST_PVU_TRIACIONES();

				SAP_SIC_Pagos proxy = ConectaSAP();
				
				int intMax = 9;

				strTrama = CadenaTriacion.Split(';',(char)intMax);

				if (CadenaTriacion.Length > 0)
				{
					objFilaTriaciones.Secuencial  = strTrama[0];
					objFilaTriaciones.Oficina_Venta = strTrama[1];
					objFilaTriaciones.Cliente = strTrama[2];
					objFilaTriaciones.Tipo_Doc_Cliente = strTrama[3];
					objFilaTriaciones.Nro_Tel_Prepago = strTrama[4];
					objFilaTriaciones.Nro_Doc_Prepago = strTrama[5];
					objFilaTriaciones.Nro_Tel_Postpago = strTrama[6];
					objFilaTriaciones.Nro_Doc_Postpago = strTrama[7];
					objFilaTriaciones.Estado  = strTrama[8];
					objTriaciones.Add(objFilaTriaciones);
				}
				

				proxy.Zpvu_Rfc_Trs_Triacion_Pre_Post(ref objReturn,ref objTriaciones);
				
    			dsReturn.Tables.Add(objReturn.ToADODataTable());
				proxy.Connection.Close();

			}
			catch (Exception ex)
			{
				dsReturn = null;
			}

			return dsReturn;

		}


		//[AutoComplete(true)]
		public DataSet SetGet_LogActivacionCHIP(string NRO_DOCUMENTO , string OFI_VENTA, string strLog, ref string NRO_SOLICITUD)
		{
			DataSet dsReturn = new DataSet();
			string[] strTrama;
			try
			{
				ZST_ACT_CHIPTable objActChip = new ZST_ACT_CHIPTable();
				BAPIRET2Table objReturn = new BAPIRET2Table();
                ZST_ACT_CHIP objFilaActChip = new ZST_ACT_CHIP();

				SAP_SIC_Pagos proxy = ConectaSAP();
				
				int intMax = 18;

				strTrama = strLog.Split(';',(char)intMax);

				if (strLog.Length > 0)
				{
					objFilaActChip.Nro_Solicitud   = strTrama[0];
					objFilaActChip.Nro_Telefono = strTrama[1];
					objFilaActChip.Serie_Actual = strTrama[2];
					objFilaActChip.Imsi_Actual = strTrama[3];
					objFilaActChip.Oficina_Venta = strTrama[4];
					objFilaActChip.Documento = strTrama[5];
					objFilaActChip.Motivo_Pedido = strTrama[6];
					objFilaActChip.Serie_Nueva  = strTrama[7];
					objFilaActChip.Imsi_Nuevo = strTrama[8];
					objFilaActChip.Vendedor = strTrama[9];
					objFilaActChip.Estado_Ini_Telef = strTrama[10];
					objFilaActChip.Fecha_Creacion = strTrama[11];
					objFilaActChip.Hora_Creacion = strTrama[12];
					objFilaActChip.Estado_Solicitud = strTrama[13];
					objFilaActChip.Enviado_Variacio = strTrama[14];
					objFilaActChip.Estado_Fin_Telef  = strTrama[15];
					objFilaActChip.Fecha_Actualiz = strTrama[16];
					objFilaActChip.Hora_Actualiz = strTrama[17];
					objActChip.Add(objFilaActChip);
				}
				
				proxy.Zpvu_Rfc_Trs_Activacion_Chip(NRO_DOCUMENTO,OFI_VENTA,out NRO_SOLICITUD,ref objActChip,ref objReturn);
				
				dsReturn.Tables.Add(objActChip.ToADODataTable());
                dsReturn.Tables.Add(objReturn.ToADODataTable());
				proxy.Connection.Close();

			}
			catch (Exception ex)
			{
				dsReturn = null;
			}

			return dsReturn;

		}

		//[AutoComplete(true)]
		public DataSet Set_LogVariacion(string strEntrada, string strNomSalida, string strValSalida)
		{
			DataSet dsReturn = new DataSet();
			string[] arrEntrada;

			string[] arrSAP1 = {"MSISDN","IMSI","NROTXSW","RETCODE","ERRORMSG"};
			string strLogVariacion;

			try
			{
				SAP_SIC_Pagos proxy = ConectaSAP();

				arrEntrada = strEntrada.Split(';');
				strEntrada = arrEntrada[6] + ';' + arrEntrada[5] + ';' + arrEntrada[0] + ';' + arrEntrada[1] + ';' + arrEntrada[3] + ';' + arrEntrada[4] + ';' + arrEntrada[2];
				strLogVariacion = OrdenaValores(strNomSalida,strValSalida,arrSAP1);

				strLogVariacion = strEntrada + ';' + strLogVariacion;

				ZST_PV_LOG_VARIACIO objFilaVariacion = new ZST_PV_LOG_VARIACIO();
                ZST_PV_LOG_VARIACIOTable objVariacion = new ZST_PV_LOG_VARIACIOTable();
                BAPIRET2Table objReturn = new BAPIRET2Table();

				int intMax = 12;
				arrEntrada = strLogVariacion.Split(';',(char)intMax);

				if (strLogVariacion.Length > 0)
				{
					objFilaVariacion.I_Iccidnew = arrEntrada[0];
					objFilaVariacion.I_Iccidold  = arrEntrada[1];
					objFilaVariacion.I_Msisdn = arrEntrada[2];
					objFilaVariacion.I_Nropedido  = arrEntrada[3];
					objFilaVariacion.I_Nroofventa = arrEntrada[4];
					objFilaVariacion.I_Nrotxsw = arrEntrada[5];
					objFilaVariacion.I_Codcausa = arrEntrada[6];
					objFilaVariacion.O_Msisdn = arrEntrada[7];
					objFilaVariacion.Imsi = arrEntrada[8];
					objFilaVariacion.Nrotxsw = arrEntrada[9];
					objFilaVariacion.Retcode = arrEntrada[10];
					objFilaVariacion.Errormsg = arrEntrada[11];

					objVariacion.Add(objFilaVariacion);

				}

				proxy.Zpvu_Rfc_Trs_Log_Variacion(ref objReturn,ref objVariacion);

				dsReturn.Tables.Add(objReturn.ToADODataTable());
				proxy.Connection.Close();

			}
			catch (Exception ex)
			{
				dsReturn = null;
			}

			return dsReturn;
		}

		//[AutoComplete(true)]
		public DataSet Get_ConsultaNroContrato(string OficVenta, string NroPedido, string NroFactura, out string NroContrato)
		{
			DataSet dsReturn = new DataSet();
			try
			{
				BAPIRET2Table objReturn = new BAPIRET2Table();

				SAP_SIC_Pagos proxy = ConectaSAP();
				
				proxy.Zpvu_Rfc_Con_Acuerdo_Pedido(NroFactura,NroPedido,OficVenta, out NroContrato,ref objReturn);
				
				dsReturn.Tables.Add(objReturn.ToADODataTable());
				proxy.Connection.Close();

			}
			catch (Exception ex)
			{
				dsReturn = null;
				NroContrato="";
			}

			return dsReturn;
		}
			

		//[AutoComplete(true)]
		public DataSet Get_ConsultaAcuerdoPCS(string NroContrato)
		{
			DataSet dsReturn = new DataSet();
			try
			{
				BAPIRET2Table objReturn = new BAPIRET2Table();
				ZST_PV_CONSULTA_CONTRATOTable objAcuerdo = new ZST_PV_CONSULTA_CONTRATOTable();
				ZST_PV_DET_ACUERDOTable objDetalle = new ZST_PV_DET_ACUERDOTable();
				ZST_PV_SERVICIOS_ACUERDOTable objServicios = new ZST_PV_SERVICIOS_ACUERDOTable();
				ZST_PV_CLIENTETable objCliente = new ZST_PV_CLIENTETable();

				SAP_SIC_Pagos proxy = ConectaSAP();

				proxy.Zpvu_Rfc_Con_Acuerdo(NroContrato,"",ref objAcuerdo,ref objCliente, ref objDetalle, ref objReturn,ref objServicios);
				
				dsReturn.Tables.Add(objAcuerdo.ToADODataTable());
				dsReturn.Tables.Add(objDetalle.ToADODataTable());
				dsReturn.Tables.Add(objServicios.ToADODataTable());
				dsReturn.Tables.Add(objCliente.ToADODataTable());
				dsReturn.Tables.Add(objReturn.ToADODataTable());
				proxy.Connection.Close();

			}
			catch (Exception ex)
			{
				dsReturn = null;
				throw new ApplicationException("Get_ConsultaAcuerdoPCS",ex);
			}

			return dsReturn;
		}

		//[AutoComplete(true)]
		public DataSet Get_ConsultaEstadoPCS(string NroContrato)
		{
			DataSet dsReturn = new DataSet();
			try
			{
				BAPIRET2Table objReturn = new BAPIRET2Table();
				ZST_PV_CON_ESTADOS_PCSTable objEstados = new ZST_PV_CON_ESTADOS_PCSTable();

				SAP_SIC_Pagos proxy = ConectaSAP();

				proxy.Zpvu_Rfc_Con_Estado_Pcs(NroContrato, ref objEstados,ref objReturn);
				
				dsReturn.Tables.Add(objEstados.ToADODataTable());
				dsReturn.Tables.Add(objReturn.ToADODataTable());
				proxy.Connection.Close();

			}
			catch (Exception ex)
			{
				dsReturn = null;
			}

			return dsReturn;
		}

		//[AutoComplete(true)]
		public DataSet Set_EstadoAcuerdoPCS(string NroContrato, string Estado, string Usuario,string AnalistaCredito, string CodigoAprobacion,string TipoRechazo, string Observaciones, string NumeroHDC)
		{
			DataSet dsReturn = new DataSet();
			try
			{
				BAPIRET2Table objReturn = new BAPIRET2Table();

				SAP_SIC_Pagos proxy = ConectaSAP();

				proxy.Zpvu_Rfc_Trs_Act_Estado_Pcs(AnalistaCredito, CodigoAprobacion,Estado,"",NroContrato,NumeroHDC,Observaciones,TipoRechazo,Usuario,ref objReturn);
				
				dsReturn.Tables.Add(objReturn.ToADODataTable());
				proxy.Connection.Close();

			}
			catch (Exception ex)
			{
				dsReturn = null;
			}

			return dsReturn;

		}

		//[AutoComplete(true)]
		public DataSet Get_ParamGlobal(string OficVta)
		{
			DataSet dsReturn = new DataSet();
			try
			{
				BAPIRET2Table objReturn = new BAPIRET2Table();
				ZST_PV_PARAM_OF_VTATable objParam = new ZST_PV_PARAM_OF_VTATable();

				SAP_SIC_Pagos proxy = ConectaSAP();

				proxy.Zpvu_Rfc_Adm_Param_Of_Vta(OficVta,"","", ref objParam,ref objReturn);
				
				dsReturn.Tables.Add(objParam.ToADODataTable());
				dsReturn.Tables.Add(objReturn.ToADODataTable());
				proxy.Connection.Close();

			}
			catch (Exception ex)
			{
				dsReturn = null;
				throw ex;
			}

			return dsReturn;
		}

		//[AutoComplete(true)]
		public DataSet Get_ConsultaCajeros(string OficinaVenta, string TipoUsuario)

		{
			DataSet dsReturn = new DataSet();
			string strFecha;

			try
			{
				ZST_USUARIOTable objCajero = new ZST_USUARIOTable();
				BAPIRET2Table objLog = new BAPIRET2Table();

				SAP_SIC_Pagos  proxy = ConectaSAP();

				//strFecha = DateTime.Now.ToString("d") ;
				strFecha = DateTime.Now.Day.ToString("00") + "/" +  DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString("0000");

				proxy.Zpvu_Rfc_Con_Usuario(FormatoFecha(strFecha),TipoUsuario,"",OficinaVenta,ref objLog,ref objCajero);

				dsReturn.Tables.Add(objCajero.ToADODataTable());
				proxy.Connection.Close();
				
			}
			catch(Exception ex)
			{
				dsReturn=null;
				throw ex;
			}
			return dsReturn;
		}

		//[AutoComplete(true)]
		public DataSet Get_DepositoGarantia(string P_NumDepGaran)
		{
            DataSet dsReturn = new DataSet();

			try
			{
                ZST_DEP_GARANTIATable objDepGaran = new ZST_DEP_GARANTIATable();
			    BAPIRET2Table objReturn = new BAPIRET2Table();

     		    SAP_SIC_Pagos  proxy = ConectaSAP();

				DataTable dt = new DataTable();
				string strFecha;

                proxy.Zpvu_Rfc_Con_Dep_Garan(P_NumDepGaran,"", ref objDepGaran,ref objReturn);

				dt = objDepGaran.ToADODataTable();
				
				for (int i=0;i<dt.Rows.Count;i++)
				{
					strFecha = (string)dt.Rows[i]["FECHA_DEPOSITO"];
					strFecha = strFecha.Substring(6,2) + "/" + strFecha.Substring(4,2) + "/" + strFecha.Substring(0,4);
					dt.Rows[i]["FECHA_DEPOSITO"] = strFecha;

					strFecha = (string)dt.Rows[i]["FECHA_VENCIMIENT"];
					strFecha = strFecha.Substring(6,2) + "/" + strFecha.Substring(4,2) + "/" + strFecha.Substring(0,4);
					dt.Rows[i]["FECHA_VENCIMIENT"] = strFecha;

					strFecha = (string)dt.Rows[i]["FEC_CREACION"];
					strFecha = strFecha.Substring(6,2) + "/" + strFecha.Substring(4,2) + "/" + strFecha.Substring(0,4);
					dt.Rows[i]["FEC_CREACION"] = strFecha;
				}

				dsReturn.Tables.Add(dt);				
				dsReturn.Tables.Add(objReturn.ToADODataTable());
				proxy.Connection.Close();

			}
			catch(Exception ex)
			{
				dsReturn = null;
			}
			return dsReturn;
		}

		//[AutoComplete(true)]
		public DataSet Get_NroCorrGarantia(string OficinaVenta, string clDoc, out string UltimoNumero)
		{
			DataSet dsReturn = new DataSet();

			try
			{
				BAPIRET2Table objReturn = new BAPIRET2Table();

				SAP_SIC_Pagos  proxy = ConectaSAP();

				proxy.Zpvu_Rfc_Trs_Get_Nro_Cor_Garan(clDoc,OficinaVenta,out UltimoNumero,ref objReturn); 

				dsReturn.Tables.Add(objReturn.ToADODataTable());
				proxy.Connection.Close();
			}
			catch(Exception ex)
			{
				dsReturn=null;
				UltimoNumero="";
			}
			return dsReturn;
		}

		//[AutoComplete(true)]
		public DataSet Set_DepositoGarantia(string P_KUNNR, string P_STCDT, string P_STCDT1, string P_VKBUR, string P_FECREG, string P_FECVEN, string P_IMPORT, string P_WAERS, string P_VIAPAG, string P_CORRE, string Usuario, string ClDoc, string NroCargos, string NroOperacion,string strNumDepGaran ,out string strBELNR, out string strXBLNR)
		{
			DataSet dsReturn = new DataSet();

			try
			{
				BAPIRET2Table objReturn = new BAPIRET2Table();

				SAP_SIC_Pagos  proxy = ConectaSAP();

				proxy.Zpvu_Rfc_Trs_Depos_Garantia(ClDoc,NroCargos,NroOperacion,P_CORRE,FormatoFecha(P_FECREG),FormatoFecha(P_FECVEN),Convert.ToDecimal(P_IMPORT),P_KUNNR,strNumDepGaran,P_STCDT1,P_STCDT,P_VIAPAG,P_VKBUR,P_WAERS,Usuario,out strBELNR,out strXBLNR,ref objReturn);
				
				dsReturn.Tables.Add(objReturn.ToADODataTable());
				proxy.Connection.Close();
			}
			catch(Exception ex)
			{
				dsReturn=null;
				strBELNR="";
				strXBLNR="";
			}
			return dsReturn;
		}

		//[AutoComplete(true)]
		public DataSet Set_ModificaDepositoGarantia(string P_CadenaDepGaran)
		{
			DataSet dsReturn = new DataSet();
            string[] strTrama;
			int intMax = 21;
			try
			{
				BAPIRET2Table objReturn = new BAPIRET2Table();
				ZST_DEP_GARANTIATable objDepGar = new ZST_DEP_GARANTIATable();
                ZST_DEP_GARANTIA objFilaDepGar = new ZST_DEP_GARANTIA();

				SAP_SIC_Pagos  proxy = ConectaSAP();
				
				strTrama = P_CadenaDepGaran.Split(';',(char)intMax);

				if (P_CadenaDepGaran.Length > 0)
				{
					objFilaDepGar.Nro_Dep_Garantia = strTrama[0];
					objFilaDepGar.Tipo_Doc_Cliente = strTrama[1];
					objFilaDepGar.Cliente = strTrama[2];
					objFilaDepGar.Fecha_Deposito = FormatoFecha(strTrama[3]);
					objFilaDepGar.Fecha_Vencimient = FormatoFecha(strTrama[4]);
					objFilaDepGar.Numero_Contrato = strTrama[5];
					objFilaDepGar.Documento = strTrama[6];
					objFilaDepGar.Belnr = strTrama[7];
					objFilaDepGar.Xblnr = strTrama[8];
					objFilaDepGar.Monto_Deposito = Convert.ToDecimal(strTrama[9]);
					objFilaDepGar.Oficina_Venta = strTrama[10];
					objFilaDepGar.Anulado = strTrama[11];
					objFilaDepGar.Usu_Creacion = strTrama[12];
					objFilaDepGar.Fec_Creacion = FormatoFecha(strTrama[13]);
					objFilaDepGar.Hor_Creacion = strTrama[14];
				    objFilaDepGar.Usu_Modifica = strTrama[15];
					objFilaDepGar.Fec_Modifica = FormatoFecha(strTrama[16]);
					objFilaDepGar.Hor_Modifica = strTrama[17];
					objFilaDepGar.Cldoc = strTrama[18];
					objFilaDepGar.Nro_Cargos = strTrama[19];
					objFilaDepGar.Nro_Operacion = strTrama[20];

					objDepGar.Add(objFilaDepGar);
				}

				proxy.Zpvu_Rfc_Trs_Mod_Dep_Garan(ref objDepGar,ref objReturn);

                dsReturn.Tables.Add(objDepGar.ToADODataTable());
				dsReturn.Tables.Add(objReturn.ToADODataTable());
				proxy.Connection.Close();
			}
			catch(Exception ex)
			{
				dsReturn=null;
			}
			return dsReturn;
		}

		//[AutoComplete(true)]
		public DataSet Get_CuadreCajaConsulta(string Fecha, string OficinaVenta, string Usuario, out decimal SaldoInicial, out decimal CajaBuzon)
		{
			DataSet dsReturn = new DataSet();
			try
			{
				BAPIRET2Table objReturn = new BAPIRET2Table();
				
				SAP_SIC_Pagos  proxy = ConectaSAP();

				proxy.Zpvu_Rfc_Con_Cierre_Ant(FormatoFecha(Fecha),OficinaVenta,Usuario,out CajaBuzon,out SaldoInicial,ref objReturn);

				dsReturn.Tables.Add(objReturn.ToADODataTable());
				proxy.Connection.Close();

			}
			catch(Exception ex)
			{
				dsReturn = null;
				SaldoInicial = 0;
				CajaBuzon = 0;
			}

			return dsReturn;

		}

        //[AutoComplete(true)]
		public DataSet Get_CuadreCajaProceso(string FechaDeArqueo, 
			string OficinaVenta,
			decimal SaldoInicial,
			decimal IngresoEfectivoDia, 
			decimal CajaBuzon, 
			decimal CajaBuzonCheque,
			decimal Remesa, 
			decimal MontoPendienteIngreso,
			decimal MontoSobrante,
			string CerrarElDia,
			string Usuario)
		{
			DataSet dsReturn = new DataSet();
			try
			{
				BAPIRET2Table objReturn = new BAPIRET2Table();
				
				SAP_SIC_Pagos  proxy = ConectaSAP();

				proxy.Zpvu_Rfc_Rep_Cuadre_Proceso(CajaBuzon,CajaBuzonCheque,CerrarElDia,FormatoFecha(FechaDeArqueo),IngresoEfectivoDia,MontoPendienteIngreso,MontoSobrante,OficinaVenta,Remesa,SaldoInicial,Usuario,ref objReturn);
				//proxy.Zpvu_Rfc_Rep_Cuadre_Proceso(CajaBuzon,CerrarElDia,FormatoFecha(FechaDeArqueo),IngresoEfectivoDia,MontoPendienteIngreso,MontoSobrante,OficinaVenta,Remesa,SaldoInicial,Usuario,ref objReturn);

				dsReturn.Tables.Add(objReturn.ToADODataTable());
				proxy.Connection.Close();

			}
			catch(Exception ex)
			{
				dsReturn = null;
				throw ex;
			}

			return dsReturn;

		}

		//[AutoComplete(true)]
		public DataSet Get_CuadreCajaResumDia(string FechaDeArqueo, 
			string OficinaVenta,
			string Usuario)
		{
			DataSet dsReturn = new DataSet();
			try
			{
				BAPIRET2Table objReturn = new BAPIRET2Table();
				ZST_PV_MONTOS_CUADRETable objMontos = new ZST_PV_MONTOS_CUADRETable();
				
				SAP_SIC_Pagos  proxy = ConectaSAP();

				proxy.Zpvu_Rfc_Rep_Cuadre_Resum_Dia(FormatoFecha(FechaDeArqueo),OficinaVenta,Usuario,ref objMontos,ref objReturn);

				dsReturn.Tables.Add(objMontos.ToADODataTable());
				dsReturn.Tables.Add(objReturn.ToADODataTable());
				proxy.Connection.Close();

			}
			catch(Exception ex)
			{
				throw ex;
			}

			return dsReturn;

		}

		//[AutoComplete(true)]
		public DataSet Get_ConsultaVend(string Vendedor)
		{
			DataSet dsReturn = new DataSet();
			//try
			//{
				ZST_PV_DATOS_VENDEDORTable objVendedor = new ZST_PV_DATOS_VENDEDORTable();
				SAP_SIC_Pagos  proxy = ConectaSAP();
				proxy.Zpvu_Rfc_Mae_Datos_Vendedor(Vendedor, ref objVendedor);
				dsReturn.Tables.Add(objVendedor.ToADODataTable());
			//}
			//catch(Exception ex)
		//	{
		//		dsReturn = null;
		//	}
			proxy.Connection.Close();
			return dsReturn;
		}

		private SAP_SIC_Pagos ConectaSAP()
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

            SAP_SIC_Pagos  proxy;

			if (strLoadBal == "0")
			{
			   proxy = new SAP_SIC_Pagos("CLIENT=" + strCliente + " USER=" + strUsuario + " PASSWD=" + strPassword + " LANG=" + strLanguage +" ASHOST=" + strApplicationServer + " SYSNR=" + strSistema);
			}
			else
			{
			   proxy = new SAP_SIC_Pagos("CLIENT=" + strCliente + " USER=" + strUsuario + " PASSWD=" + strPassword + " LANG=" + strLanguage +" MSHOST=" + strMessServ + " R3NAME=" + strR3Name + " GROUP=" + strGroup);
			}

			return proxy;

		}


		private string FormatoFecha(string Fecha)
		{

			if (Fecha.Length > 0 )
                return Fecha.Substring(6,4) + "/" + Fecha.Substring(3,2) + "/" + Fecha.Substring(0,2);
			else
				return "";

		}

		private string OrdenaValores(string strCampos, string strValores, string[] arrSAPAux)
		{
            string[] arrCampos;
            string[] arrValores;
            string[] arrAux;
		    int intIndiceA;
			int intIndiceB;
			string strResult;

			arrCampos = strCampos.Split(';');
			arrValores = strValores.Split(';');
			
		    arrAux = (string[])Array.CreateInstance(strCampos.GetType(),arrSAPAux.Length);

			for(intIndiceB = 0;intIndiceB < arrSAPAux.Length;intIndiceB++)
			{
				for(intIndiceA = 0;intIndiceA < arrCampos.Length;intIndiceA++)
				{
					if (arrSAPAux[intIndiceB].Trim() == arrCampos[intIndiceA].Trim())
  					 arrAux[intIndiceB] = arrValores[intIndiceA].Trim();
				}
			}
            strResult = "";
			for(intIndiceB = 0;intIndiceB < arrAux.Length;intIndiceB++)
			{
				strResult = strResult + arrAux[intIndiceB];
				if (intIndiceB<arrAux.Length-1)
                   strResult = strResult + ";";
			}
            
			strResult = strResult.Substring(0,strResult.Length-1);

			return strResult;
		}

		//anulaciones
		//[AutoComplete(true)]
		public DataSet Get_ConsultaAnulacion(string strNumFactSAP)
		{
			DataSet dsReturn;
			try
			{
				BAPIRET2Table objReturn = new BAPIRET2Table();
				SAP_SIC_Pagos proxy = ConectaSAP();
				proxy.Zpvu_Rfc_Con_Log_Anulac_Fact(strNumFactSAP,ref objReturn);

				dsReturn = new DataSet();
				dsReturn.Tables.Add(objReturn.ToADODataTable());
			    proxy.Connection.Close();
			}
			catch
			{
				dsReturn=null;
			}
			return dsReturn;
		}

		//[AutoComplete(true)]
		public DataSet Get_ConsultaPagos(string Oficina , string NroReferencia ,string TipoDocumento) 
		{
			DataSet dsReturn;
			try
			{
				ZES_PAGOSTable objPagos = new ZES_PAGOSTable();
				BAPIRET2Table objReturn = new BAPIRET2Table();
				SAP_SIC_Pagos proxy = ConectaSAP();
				proxy.Zpvu_Rfc_Con_Pagos(NroReferencia,Oficina, TipoDocumento,ref objPagos,ref objReturn);								
				foreach(ZES_PAGOS obFila in objPagos)
				{
					if (obFila.F_Pedido.Trim().Length>0)
					{
						string strFecha = obFila.F_Pedido;
						obFila.F_Pedido = strFecha.Substring(6,2) + "/" + strFecha.Substring(4,2) + "/" + strFecha.Substring(0,4);
					}
				}				
				dsReturn = new DataSet();
				dsReturn.Tables.Add(objPagos.ToADODataTable());				
				dsReturn.Tables.Add(objReturn.ToADODataTable());	
				proxy.Connection.Close();
			}
			catch
			{
				dsReturn=null;
			}
			return dsReturn;
		}

        //[AutoComplete(true)]
		public DataSet Get_RegistroAnulPagos( string FechaContabilidad , string NroReferencia ,  string ViaPago , string OficinaVenta ,string Usuario) 
		{
			DataSet dsReturn;
			try
			{				
				ZST_PV_LOGTable objReturn = new ZST_PV_LOGTable();
				SAP_SIC_Pagos proxy = ConectaSAP();
				proxy.Zpvu_Rfc_Trs_Pagos_Anulacion(FechaContabilidad,NroReferencia, OficinaVenta,Usuario,ViaPago,ref objReturn);								
				dsReturn = new DataSet();
				dsReturn.Tables.Add(objReturn.ToADODataTable());
				proxy.Connection.Close();
			}
			catch
			{
				dsReturn=null;
			}
			return dsReturn;	
		}

        //[AutoComplete(true)]
		public DataSet Set_AnularDocumentoJob(string CadenaDocumento, string Usuario) 
		{
			DataSet dsReturn;
			try
			{
				
				ZST_PV_LOGTable objReturn = new ZST_PV_LOGTable();
				ZST_PV_REPLICATable objReplica = new ZST_PV_REPLICATable();
				SAP_SIC_Pagos proxy = ConectaSAP();
				

				ZST_PV_REPLICA objReplicaFila = new ZST_PV_REPLICA();
				string[]  strTrama;
				
				strTrama = CadenaDocumento.Split(';',(char)43);
				
				if (CadenaDocumento.Length>0)
				{
					if (strTrama.Length>0) objReplicaFila.Borra = strTrama[0];
					if (strTrama.Length>1) objReplicaFila.Auart = strTrama[1];
					if (strTrama.Length>2) objReplicaFila.Vkorg = strTrama[2];
					if (strTrama.Length>3) objReplicaFila.Vtweg = strTrama[3];
					if (strTrama.Length>4) objReplicaFila.Spart = strTrama[4];
					if (strTrama.Length>5) objReplicaFila.Vkbur = strTrama[5];
					if (strTrama.Length>6) objReplicaFila.Vkgrp = strTrama[6];			
					if (strTrama.Length>7) objReplicaFila.Xblnr = strTrama[7];
					if (strTrama.Length>8) objReplicaFila.Kunnr = strTrama[8];
					if (strTrama.Length>9) objReplicaFila.Augru = strTrama[9];
					if (strTrama.Length>10) objReplicaFila.Mot_Repo = strTrama[10];				
					if (strTrama.Length>11) objReplicaFila.Mvgr4 = strTrama[11];
					if (strTrama.Length>12) objReplicaFila.Nro_Clarif = strTrama[12];
					if (strTrama.Length>13) objReplicaFila.Nro_Cuotas = strTrama[13];
					if (strTrama.Length>14) objReplicaFila.Tipo_Venta_Pvu = strTrama[14];
					if (strTrama.Length>15) objReplicaFila.Audat = strTrama[15];
					if (strTrama.Length>16) objReplicaFila.Prsdt = strTrama[16];
					if (strTrama.Length>17) objReplicaFila.Ihrez_E = strTrama[17];
					if (strTrama.Length>18) objReplicaFila.Interloc = strTrama[18];
					if (strTrama.Length>19) objReplicaFila.Dwerk = strTrama[19];
					if (strTrama.Length>20) objReplicaFila.Zterm = strTrama[20];
					if (strTrama.Length>21) objReplicaFila.Mabnr = strTrama[21];
					if (strTrama.Length>22) objReplicaFila.Kwmeng = strTrama[22];
					if (strTrama.Length>23) objReplicaFila.Pltyp = strTrama[23];
					if (strTrama.Length>24) objReplicaFila.Mvgr3 = strTrama[24];
					if (strTrama.Length>25) objReplicaFila.Vkaus = strTrama[25];
					if (strTrama.Length>26) objReplicaFila.Lgort = strTrama[26];
					if (strTrama.Length>27) objReplicaFila.Sernr = strTrama[27];
					if (strTrama.Length>28) objReplicaFila.Nro_Telef = strTrama[28];
					if (strTrama.Length>29) objReplicaFila.Campana = strTrama[29];
					if (strTrama.Length>30) objReplicaFila.Plan_Tarifario = strTrama[30];
					if (strTrama.Length>31) objReplicaFila.Nro_Clarify = strTrama[31];
					if (strTrama.Length>32) objReplicaFila.Vstel = strTrama[32];
					if (strTrama.Length>33) objReplicaFila.Kondm = strTrama[33];
					if (strTrama.Length>34) objReplicaFila.Clase_Venta = strTrama[34];

					if (strTrama.Length>35) objReplicaFila.Ref_Origen = strTrama[35];
					if (strTrama.Length>36) objReplicaFila.Nro_Pcs_Asociado = strTrama[36];
					if (strTrama.Length>37) objReplicaFila.Nro_Ped_Tg = strTrama[37];
					if (strTrama.Length>38) objReplicaFila.Nro_Acuer_Alqu = strTrama[38];
					if (strTrama.Length>39) objReplicaFila.Fidelizacion = strTrama[39];

					if (strTrama.Length>40) objReplicaFila.Nro_Solicitud = strTrama[40];
					if (strTrama.Length>41) objReplicaFila.Serie_Recibida = strTrama[41];
					if (strTrama.Length>42) objReplicaFila.Operador = strTrama[42];
					if (strTrama.Length>43) objReplicaFila.Tipo_Prod_Operad = strTrama[43];
					objReplica.Add(objReplicaFila);
				}

				proxy.Zpvu_Rfc_Trs_Anula_Pedido_Sap(Usuario,ref objReturn,ref objReplica);
				dsReturn = new DataSet();
				dsReturn.Tables.Add(objReplica.ToADODataTable());				
				dsReturn.Tables.Add(objReturn.ToADODataTable());
				proxy.Connection.Close();
			}
			catch
			{
				dsReturn=null;
			}
			return dsReturn;
			
		}//ya



		//anulacion depositos en garantia
		//[AutoComplete(true)]
		public DataSet Set_AnulaDepositoGarantia( string  P_FECHACONTAB , string P_OFVENTA , string P_BELNR, string P_USUARIO,string  P_XBLNR, out string P_BELNR_ANUL)
		{
		
			DataSet dsReturn;
			try
			{				
				ZST_PV_LOGTable objReturn = new ZST_PV_LOGTable();
				SAP_SIC_Pagos proxy = ConectaSAP();
				//proxy.Zpvu_Rfc_Trs_Anul_Dep_Garantia(P_BELNR,FormatoFecha(DateTime.Now.ToString("d")),P_OFVENTA,FormatoFecha(P_FECHACONTAB),P_USUARIO,P_XBLNR,out P_BELNR_ANUL,ref objReturn);
				proxy.Zpvu_Rfc_Trs_Anul_Dep_Garantia(P_BELNR,FormatoFecha(DateTime.Now.Day.ToString("00") + "/" +  DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString("0000")),P_OFVENTA,FormatoFecha(P_FECHACONTAB),P_USUARIO,P_XBLNR,out P_BELNR_ANUL,ref objReturn);
				dsReturn = new DataSet();
				dsReturn.Tables.Add(objReturn.ToADODataTable());
				proxy.Connection.Close();
			}
			catch
			{
				dsReturn=null;
				P_BELNR_ANUL="";
			}
			return dsReturn;
		}

        //[AutoComplete(true)]
		public string Get_ConsultaVendedorRecarga(string CSA, string codOficina) 
		{
			string strReturn;
			try
			{				
				if (codOficina.Trim().Substring(0,1)!="S")
				{				
					SAP_SIC_Pagos proxy = ConectaSAP();
					proxy.Zpvu_Rfc_Con_Vend_Recarga(CSA,out strReturn);
 					proxy.Connection.Close();
				}
				else
					strReturn="";
			}
			catch
			{
				strReturn="";
			}
			return strReturn;
		}

		//[AutoComplete(true)]
		public DataSet Get_ConsultaDetalleRVirtual(string CSA, string codOficina)
		{
			DataSet dsReturn;
			try
			{	
				if (codOficina.Trim().Substring(0,1)!="S")
				{
					ZPVU_DETALLE_RECARGATable objRecarga = new ZPVU_DETALLE_RECARGATable();
					BAPIRET2Table objReturn = new BAPIRET2Table();
					SAP_SIC_Pagos proxy = ConectaSAP();				
					proxy.Zpvu_Rfc_Con_Telef_Factura(CSA,codOficina, ref objRecarga, ref objReturn);
					dsReturn = new DataSet();
					dsReturn.Tables.Add(objRecarga.ToADODataTable());
					dsReturn.Tables.Add(objReturn.ToADODataTable());
					proxy.Connection.Close();
				}
				else
					dsReturn=null;
			}
			catch
			{
				dsReturn=null;
			}
			return dsReturn;			
		}

		//[AutoComplete(true)]
		public DataSet Set_VentaRecarga(string CadenaDetalle, string codOficina)
		{
			DataSet dsReturn;
			try
			{	
				if (codOficina.Trim().Substring(0,1)!="S")
				{
					ZPVU_DETALLE_RECARGATable objRecarga = new ZPVU_DETALLE_RECARGATable();
					BAPIRET2Table objReturn = new BAPIRET2Table();
					string[] arr = CadenaDetalle.Split(';');

					ZPVU_DETALLE_RECARGA obj = new ZPVU_DETALLE_RECARGA();
					if  (arr.Length>0) 	obj.Vbeln = arr[0];
					if  (arr.Length>1) 	obj.Posnr = arr[1];
					if  (arr.Length>2) 	obj.Vkbur = arr[2];
					if  (arr.Length>3) 	obj.Nro_Telefonico = arr[3];
					if  (arr.Length>4) 	obj.Rec_Efectiva = decimal.Parse(arr[4]);
					if  (arr.Length>5) 	obj.Val_Venta = decimal.Parse(arr[5]);
					if  (arr.Length>6) 	obj.Descuento = decimal.Parse(arr[6]);
					if  (arr.Length>7) 	obj.Igv = decimal.Parse(arr[7]);
					if  (arr.Length>8) 	obj.Total_Pago = decimal.Parse(arr[8]);
					if  (arr.Length>9) 	obj.Nro_Rec_Switch = arr[9];
					

					objRecarga.Add(obj);
					SAP_SIC_Pagos proxy = ConectaSAP();				
					proxy.Zpvu_Rfc_Trs_Act_Vta_Recarga(ref objRecarga, ref objReturn);
					dsReturn = new DataSet();
					dsReturn.Tables.Add(objRecarga.ToADODataTable());
					dsReturn.Tables.Add(objReturn.ToADODataTable());
					proxy.Connection.Close();
				}
				else
					dsReturn=null;
			}
			catch
			{
				dsReturn=null;
			}
			return dsReturn;			
		
		}

		// fin anulaciones
		//**********************************


		/// anulaciones de PREIMPRESOS
		//[AutoComplete(true)]
		public DataSet Set_SUNATAnulP( string OficinaVenta, string ClaseDocSunat, string SerieSunat , string CorrelativoSunat , string FechaContabilidad)
		{
			DataSet dsReturn;
			try
			{					
				BAPIRET2Table objReturn = new BAPIRET2Table();
				SAP_SIC_Pagos proxy = ConectaSAP();				

				SerieSunat = SerieSunat.Trim() == "" ? "0" : SerieSunat;
				CorrelativoSunat = CorrelativoSunat.Trim() == "" ? "0" : CorrelativoSunat;
				FechaContabilidad = FechaContabilidad.Substring(6,4)+"/"+FechaContabilidad.Substring(3,2)+"/"+FechaContabilidad.Substring(0,2);

				proxy.Zpvu_Rfc_Trs_Sunat_Anul_Prev(ClaseDocSunat, CorrelativoSunat, FechaContabilidad, OficinaVenta,SerieSunat,ref objReturn);
				dsReturn = new DataSet();					
				dsReturn.Tables.Add(objReturn.ToADODataTable());
				proxy.Connection.Close();
			}
			catch
			{
				dsReturn=null;
			}
			return dsReturn;			
		}
        
		//[AutoComplete(true)]
		public DataSet Set_SUNATAnulR(string OficinaVenta, string ClaseDocSunat , string SerieSunat, 
									string CorrelativoSunat ,string FechaContabilidad , string Motivo ) 
		{
			DataSet dsReturn;
			try
			{					
				BAPIRET2Table objReturn = new BAPIRET2Table();
				SAP_SIC_Pagos proxy = ConectaSAP();	

				SerieSunat = SerieSunat.Trim() == "" ? "0" : SerieSunat;
				CorrelativoSunat = CorrelativoSunat.Trim() == "" ? "0" : CorrelativoSunat;
				FechaContabilidad = FechaContabilidad.Substring(6,4)+"/"+FechaContabilidad.Substring(3,2)+"/"+FechaContabilidad.Substring(0,2);

				proxy.Zpvu_Rfc_Trs_Sunat_Anul_Reutil(ClaseDocSunat, CorrelativoSunat, FechaContabilidad, Motivo,OficinaVenta,SerieSunat,"",ref objReturn);
				dsReturn = new DataSet();					
				dsReturn.Tables.Add(objReturn.ToADODataTable());
				proxy.Connection.Close();
			}
			catch
			{
				dsReturn=null;
			}
			return dsReturn;					
		}
		/// FIN anulaciones de PREIMPRESOS
        //[AutoComplete(true)]
		public DataSet Get_ConsultaOficinaVenta(string Oficina, string Canal)

		{
			DataSet dsReturn = new DataSet();
			try
			{
				ZST_PV_OFICINA_VENTATable objOficina = new ZST_PV_OFICINA_VENTATable();
				SAP_SIC_Pagos  proxy = ConectaSAP();
				proxy.Zpvu_Rfc_Mae_Oficina_Venta(Canal,Oficina,ref objOficina);
				dsReturn.Tables.Add(objOficina.ToADODataTable());
				proxy.Connection.Close();
			}
			catch(Exception ex)
			{
				dsReturn = null;
			}
			return dsReturn;
		}
 
		
		//// IMPRESION
		///
		//[AutoComplete(true)]
		public DataSet Set_ImpresionFormulario(string Formulario, string Nro_Caja , string Nro_Documento, string  Visualizar ) 
		{
			DataSet dsReturn;
			try
			{					
				BAPIRET2Table objReturn = new BAPIRET2Table();
				SAP_SIC_Pagos proxy = ConectaSAP();	

				proxy.Zpvu_Rfc_Trs_Print_Formulario(Formulario,Nro_Caja,Nro_Documento,Visualizar, ref objReturn);

				dsReturn = new DataSet();					
				dsReturn.Tables.Add(objReturn.ToADODataTable());
				proxy.Connection.Close();
			}
			catch
			{
				dsReturn=null;
			}
			return dsReturn;	
		}

		

		//[AutoComplete(true)]
		public DataSet Get_ConsultaCliente(string Oficina,string TipDocCliente ,string Cliente ) 
		{
			string Multimarca="";
			DataSet dsReturn;
			try
			{
				ZST_PV_CLIENTETable objCliente = new ZST_PV_CLIENTETable();	
				BAPIRET2Table objReturn = new BAPIRET2Table();
				SAP_SIC_Pagos proxy = ConectaSAP();	

				proxy.Zpvu_Rfc_Con_Cliente(Cliente,Oficina, TipDocCliente, out Multimarca, ref objCliente, ref objReturn);

				dsReturn = new DataSet();					
				dsReturn.Tables.Add(objCliente.ToADODataTable());			
				dsReturn.Tables.Add(objReturn.ToADODataTable());
				proxy.Connection.Close();
			}
			catch
			{
				dsReturn=null;
			}
			
			return dsReturn;
		}

		//[AutoComplete(true)]
		public DataSet Set_PrintDocumentoSAP(string NroDocSap )
		{
			DataSet dsReturn;
			try
			{					
				BAPIRET2Table objReturn = new BAPIRET2Table();
				SAP_SIC_Pagos proxy = ConectaSAP();	

				proxy.Zpvu_Rfc_Trs_Print_Documento(NroDocSap, ref objReturn );

				dsReturn = new DataSet();					
				dsReturn.Tables.Add(objReturn.ToADODataTable());
				proxy.Connection.Close();
			}
			catch
			{
				dsReturn=null;
			}
			return dsReturn;			
		}

        //[AutoComplete(true)]
		public DataSet Get_ConsultaComprobante(string NroDocumento,string OficinaVenta)
		{
			DataSet dsReturn;
			DataTable dt = new DataTable();
			DataTable dt2 = new DataTable();
			string strFecha;
			try
			{					
				ZST_PV_COMPRO_CABECERATable		objCabecera	= new ZST_PV_COMPRO_CABECERATable();
				ZST_PV_COMPRO_CUOTASTable		objCuotas	= new ZST_PV_COMPRO_CUOTASTable();
				ZST_PV_COMPRO_DETALLETable		objDetalle	= new ZST_PV_COMPRO_DETALLETable();
				ZST_PV_COMPRO_PIETable			objPie		= new ZST_PV_COMPRO_PIETable();
				ZPVU_DETALLE_RECARGATable		objRecarga	= new ZPVU_DETALLE_RECARGATable();
				ZST_PV_LOGTable					objLog		= new ZST_PV_LOGTable();

				SAP_SIC_Pagos proxy = ConectaSAP();	

				proxy.Zpvu_Rfc_Trs_Comprobante(NroDocumento, OficinaVenta,ref objCabecera, ref objCuotas,ref objDetalle, ref objPie, ref objRecarga, ref objLog);

				dsReturn = new DataSet();					

				dt2 = objCabecera.ToADODataTable();
				
				for (int i=0;i<dt2.Rows.Count;i++)
				{
					strFecha = (string)dt2.Rows[i]["FEC_DOCUMENTO"];
					if (strFecha=="00000000")
						strFecha = "";
					else
						strFecha = strFecha.Substring(6,2) + "/" + strFecha.Substring(4,2) + "/" + strFecha.Substring(0,4);

					dt2.Rows[i]["FEC_DOCUMENTO"] = strFecha;
				}

				dsReturn.Tables.Add(dt2);

				dt = objCuotas.ToADODataTable();
				
				for (int i=0;i<dt.Rows.Count;i++)
				{
					strFecha = (string)dt.Rows[i]["F_VENCIMIENTO"];
					if (strFecha=="00000000")
                       strFecha = "";
                    else
					  strFecha = strFecha.Substring(6,2) + "/" + strFecha.Substring(4,2) + "/" + strFecha.Substring(0,4);

					dt.Rows[i]["F_VENCIMIENTO"] = strFecha;
				}
				
				dsReturn.Tables.Add(dt);
				dsReturn.Tables.Add(objDetalle.ToADODataTable());
				dsReturn.Tables.Add(objPie.ToADODataTable());
				dsReturn.Tables.Add(objRecarga.ToADODataTable());
				dsReturn.Tables.Add(objLog.ToADODataTable());
				proxy.Connection.Close();
			}
			catch
			{
				dsReturn=null;
			}
			return dsReturn;			
		}

        //[AutoComplete(true)]
		public DataSet Get_ConsultaDistritos()
		{
			DataSet dsReturn;
			try
			{					
				ZST_PV_DISTRITOTable objReturn = new ZST_PV_DISTRITOTable();
				SAP_SIC_Pagos proxy = ConectaSAP();	

				proxy.Zpvu_Rfc_Mae_Distrito(ref objReturn);

				dsReturn = new DataSet();					
				dsReturn.Tables.Add(objReturn.ToADODataTable());
				proxy.Connection.Close();
			}
			catch
			{
				dsReturn=null;
			}
			return dsReturn;			
		}


		//DEVOLUCUION  ZPVU_RFC_TRS_DEVOL_EFECTIVO
        //[AutoComplete(true)]
		public DataSet Set_DevolEfectivo(string P_VKBUR , string P_BUDDAT , string P_XBLNR, string P_REFFAC , 
			decimal P_WRBTR , string P_WAERS , string Usuario )
		{
			/*
			 * P_VKBUR : codigo de oficina de vta
			 * P_BUDDAT: Fecha de Contabilizacion
			 * P_XBLNR : numero de Nota de Credito
			 * P_REFFAC: número de boleta factura
			 * P_WRBTR : importe
			 * P_WAERS : momeda
			 * Usuario : USUARIO
			 * */
			DataSet dsReturn;
			try
			{					
				BAPIRET2Table objReturn = new BAPIRET2Table();
				SAP_SIC_Pagos proxy = ConectaSAP();	
				// formato de fecha yyyy/mm/dd
				P_BUDDAT = P_BUDDAT.Substring(6,4)+"/"+P_BUDDAT.Substring(3,2)+"/"+P_BUDDAT.Substring(0,2);

				proxy.Zpvu_Rfc_Trs_Devol_Efectivo(P_BUDDAT, P_REFFAC,P_VKBUR, P_WAERS, P_WRBTR ,P_XBLNR,Usuario, ref objReturn);

				dsReturn = new DataSet();					
				dsReturn.Tables.Add(objReturn.ToADODataTable());	
				proxy.Connection.Close();
			}
			catch
			{
				dsReturn=null;
			}
			return dsReturn;			
		}

        //[AutoComplete(true)]
		public DataSet Set_DevolDepositoGarantia(string P_VKBUR, string P_CORRE , string P_FECREG , string Usuario )
		{
			/*
			 * P_VKBUR : Oficina
			 * P_CORRE : Correlativo del DG
			 * P_FECREG : Fecha de registro
			 * Usuario  
			 * */
			DataSet dsReturn;
			try
			{					
				BAPIRET2Table objReturn = new BAPIRET2Table();
				SAP_SIC_Pagos proxy = ConectaSAP();	
				// formato de fecha yyyy/mm/dd
				P_FECREG = P_FECREG.Substring(6,4)+"/"+P_FECREG.Substring(3,2)+"/"+P_FECREG.Substring(0,2);

				proxy.Zpvu_Rfc_Trs_Devol_Depos_Garan(P_CORRE,P_FECREG,P_VKBUR, Usuario, ref objReturn);

				dsReturn = new DataSet();					
				dsReturn.Tables.Add(objReturn.ToADODataTable());	
				proxy.Connection.Close();
			}
			catch
			{
				dsReturn=null;
			}
			return dsReturn;	
		}


		//ZPVU_RFC_MAE_TARJETA_CREDITO
		//[AutoComplete(true)]
		public DataSet Get_Tarjeta_Credito()
		{
			DataSet dsReturn;
			try
			{					
				ZST_PV_TVCINTTable objReturn = new ZST_PV_TVCINTTable();
				SAP_SIC_Pagos proxy = ConectaSAP();	

				proxy.Zpvu_Rfc_Mae_Tarjeta_Credito(ref objReturn);

				dsReturn = new DataSet();					
				dsReturn.Tables.Add(objReturn.ToADODataTable());
				proxy.Connection.Close();
			}
			catch
			{
				dsReturn=null;
			}
			return dsReturn;			
		}

	// ZPVU_RFC_CON_TIPO_CAMBIO
		//[AutoComplete(true)]
		public decimal Get_TipoCambio(string strFecha)
		{
			decimal dsReturn;
			try
			{					
				SAP_SIC_Pagos proxy = ConectaSAP();	
				strFecha = strFecha.Substring(6,4)+"/"+strFecha.Substring(3,2)+"/"+strFecha.Substring(0,2);
				proxy.Zpvu_Rfc_Con_Tipo_Cambio(strFecha, out dsReturn);
				proxy.Connection.Close();
			}
			catch
			{
				dsReturn=0;
			}
			return dsReturn;			
		}

		//ZPVU_RFC_PRT_LISTA_IMPRESORAS
		public DataSet Get_ListaImpresoras(string Oficina)
		{
			DataSet dsReturn;
			try
			{					
				SAP_SIC_Pagos proxy = ConectaSAP();	
				ZPV_ST_LISTA_IMPRESORASTable objImp = new ZPV_ST_LISTA_IMPRESORASTable();
				BAPIRET2Table objReturn = new BAPIRET2Table();

				proxy.Zpvu_Rfc_Prt_Lista_Impresoras(Oficina, ref objImp, ref objReturn);

				dsReturn = new DataSet();					
				dsReturn.Tables.Add(objImp.ToADODataTable());	
				dsReturn.Tables.Add(objReturn.ToADODataTable());	
				proxy.Connection.Close();

			}
			catch
			{
				dsReturn=null;
			}
			return dsReturn;			
		}

		public DataSet Set_ActUsoImpresora(string Usuario , string Impresora , string Mensaje )		
		{
			DataSet dsReturn;
			try
			{					
				ZPV_ST_LISTA_IMPRESORASTable objImp = new ZPV_ST_LISTA_IMPRESORASTable();
				BAPIRET2Table objReturn = new BAPIRET2Table();
				SAP_SIC_Pagos proxy = ConectaSAP();	

				proxy.Zpvu_Rfc_Prt_Act_Uso_Impres(Impresora,Mensaje,Usuario, ref objImp, ref objReturn);

				dsReturn = new DataSet();					
				dsReturn.Tables.Add(objImp.ToADODataTable());	
				dsReturn.Tables.Add(objReturn.ToADODataTable());
				proxy.Connection.Close();
			}
			catch
			{
				dsReturn=null;
			}
			return dsReturn;	
		}

		public DataSet Get_CuadreCajaVtasFact(string FechaDeArqueo ,string OficinaVenta ,string Usuario)
		{
			DataSet dsReturn = new DataSet();
//			try
//			{					
				SAP_SIC_Pagos proxy = ConectaSAP();	
				ZST_PV_CUOTA_FACTURATable objFacturas = new ZST_PV_CUOTA_FACTURATable();
			    
				BAPIRET2Table objReturn = new BAPIRET2Table();

				proxy.Zpvu_Rfc_Rep_Cuadre_Vtas_Fact(FormatoFecha(FechaDeArqueo),OficinaVenta,Usuario,ref objFacturas, ref objReturn);

				dsReturn.Tables.Add(objFacturas.ToADODataTable());	
				dsReturn.Tables.Add(objReturn.ToADODataTable());	
				proxy.Connection.Close();

//			}
//			catch
//			{
//				dsReturn=null;
//			}
			return dsReturn;	
		}

		public DataSet Get_CuadreCajaMaterFact(string FechaDeArqueo, string OficinaVenta, string Usuario)
		{
			DataSet dsReturn = new DataSet();
			try
			{					
				SAP_SIC_Pagos proxy = ConectaSAP();	
				ZST_PV_MATER_FACTURATable objFacturas = new ZST_PV_MATER_FACTURATable();
				BAPIRET2Table objReturn = new BAPIRET2Table();

				proxy.Zpvu_Rfc_Rep_Cuadre_Mater_Fact(FormatoFecha(FechaDeArqueo),OficinaVenta,Usuario,ref objFacturas, ref objReturn);

				dsReturn.Tables.Add(objFacturas.ToADODataTable());	
				dsReturn.Tables.Add(objReturn.ToADODataTable());	
				proxy.Connection.Close();

			}
			catch
			{
				dsReturn=null;
			}
			return dsReturn;	
		}
		

		public DataSet Get_ConsultaPagosUsuario(string FechaIni,string FechaFin,string strSoloAnul, string strUsuario, string strOficina)
		{
			DataSet dsReturn = new DataSet();
			try
			{					
				SAP_SIC_Pagos proxy = ConectaSAP();	
				ZST_PV_POOL_PAGOSTable objPool = new ZST_PV_POOL_PAGOSTable();
				BAPIRET2Table objReturn = new BAPIRET2Table();

				proxy.Zpvu_Rfc_Con_Pagos_Usuario(FormatoFecha(FechaFin),FormatoFecha(FechaIni),strSoloAnul,strUsuario,strOficina,ref objPool,ref objReturn);

				DataTable dt = new DataTable();

				dt = objPool.ToADODataTable();
				string strFecha;

				for (int i=0;i<dt.Rows.Count;i++)
				{
					strFecha = (string)dt.Rows[i]["FKDAT"];
					strFecha = strFecha.Substring(6,2) + "/" + strFecha.Substring(4,2) + "/" + strFecha.Substring(0,4);
					dt.Rows[i]["FKDAT"] = strFecha;

					//dt.Rows[i]["XBLNR"] = ((string)dt.Rows[i]["XBLNR"]).Substring(1);

				}

				dsReturn.Tables.Add(dt);	
				dsReturn.Tables.Add(objReturn.ToADODataTable());	
				proxy.Connection.Close();

			}
			catch
			{
				dsReturn=null;
			}
		return dsReturn;	
		}

		public DataSet Get_ConsultaPedidoPreVenta(string strNumero)
		{
			DataSet dsReturn = new DataSet();
			try
			{					
				SAP_SIC_Pagos proxy = ConectaSAP();	
				ZPV_PRV_CABECERATable objCabecera = new ZPV_PRV_CABECERATable();
				ZST_PRV_DETALLE2Table objDetalle = new ZST_PRV_DETALLE2Table();
				BAPIRET2Table objReturn = new BAPIRET2Table();

				proxy.Zprvt_Con_Doc(strNumero,ref objDetalle,ref objCabecera,ref objReturn);

				dsReturn.Tables.Add(objCabecera.ToADODataTable());	
				dsReturn.Tables.Add(objDetalle.ToADODataTable());	
				dsReturn.Tables.Add(objReturn.ToADODataTable());	
				proxy.Connection.Close();

			}
			catch (Exception ex)
			{
				dsReturn=null;
				throw ex;
			}
			return dsReturn;	
		}

		public DataSet Set_ActPedidoPreVenta(string strNumero, string strContrato, string strCarpObs, string strCodSAP, string strOficina, string strAnul)
		{
			DataSet dsReturn = new DataSet();
			try
			{					
				SAP_SIC_Pagos proxy = ConectaSAP();	
				BAPIRET2Table objReturn = new BAPIRET2Table();

				proxy.Zprvt_Trs_Process(strAnul,strCarpObs,strNumero,strContrato,strCodSAP,strOficina,ref objReturn);

				dsReturn.Tables.Add(objReturn.ToADODataTable());	
				proxy.Connection.Close();

			}
			catch (Exception ex)
			{
				dsReturn=null;
				throw ex;
			}
			return dsReturn;	
		}

		public DataSet Set_ActualizaPCS(string strNumContrato, string strExisteBSCS, string strTipActiv, string strCodBSCS)
		{

			DataSet dsReturn = new DataSet();
			try
			{					
				SAP_SIC_Pagos proxy = ConectaSAP();	
				BAPIRET2Table objReturn = new BAPIRET2Table();

				proxy.Zpvu_Rfc_Trs_Act_Contrato_Ent(strCodBSCS,strExisteBSCS,strNumContrato,strTipActiv,ref objReturn);

				dsReturn.Tables.Add(objReturn.ToADODataTable());	
				proxy.Connection.Close();

			}
			catch (Exception ex)
			{
				dsReturn=null;
				throw ex;
			}
			return dsReturn;	

		}

		public string Get_GlosaCampaña(string strNroSap, string strOficina)
		{
			string strGlosa;
			try
			{
				SAP_SIC_Pagos proxy = ConectaSAP();	

				proxy.Zpvu_Rfc_Con_Camp_Etno(strNroSap, strOficina, out strGlosa);
				proxy.Connection.Close();

			}
			catch (Exception ex)
			{
                strGlosa = "";
				throw ex;
			}
			return strGlosa;
		}

		public string ConsultarIccid(string Clase_Venta, string Nro_Telefono, string Tipo_Venta)
		{
			string Matnr;
			string Imei = "";
			string Fecha_Venta;

			try
			{
				SAP_SIC_Pagos proxy = ConectaSAP();

				BAPIRET2Table objRetorno = new BAPIRET2Table();
				proxy.Zpvu_Rfc_Con_Telefono_Sans(Clase_Venta,  Nro_Telefono, Tipo_Venta, out Fecha_Venta, out Imei, out Matnr, ref objRetorno);
				proxy.Connection.Close();
			}
			catch (Exception ex)
			{
				Imei = "0000000000000000";
			}
			return Imei;
		}

		public string FormatoTelefono(string strTelefono)
		{
			long lngTelefono=0;
			if(strTelefono==null || strTelefono == "")
			{
				return "";
			}
			lngTelefono = Convert.ToInt64(strTelefono);
			strTelefono = Convert.ToString(lngTelefono);
			if(strTelefono.Substring(0,1)=="1")
			{//Si es lima adicionar 0 adelante
				strTelefono = "0" + strTelefono;
			}
			return strTelefono;               
		}


		/// TODO: OBTIENE EL ULTIMO CORRELATIVO Y DE SUNAT
		/// 19.08.2013 TS.JTN
		/// 
		public string ObtenerUltimoCorrelativoSunat(string oficinaVenta,string claseFactura)
		{
			SAP_SIC_Pagos pagosSap = ConectaSAP();
			String ultimoNumeroCorrelativo = string.Empty;
			string ultimaaReferencia=String.Empty;
			BAPIRET2Table logOperacionTable = new BAPIRET2Table();
			try
			{
				pagosSap.Zsicar_Trs_Act_Corr("00000",claseFactura,oficinaVenta,out ultimaaReferencia,out ultimoNumeroCorrelativo,ref logOperacionTable);
				DataTable tbLogOperacion = logOperacionTable.ToADODataTable();
				if(tbLogOperacion.Rows.Count>0)
					if(tbLogOperacion.Rows[0]["TYPE"].ToString().StartsWith("E"))				
						throw new ExecutionEngineException(tbLogOperacion.Rows[0]["MESSAGE"].ToString());
				

				pagosSap.Connection.Close();
				return ultimaaReferencia;
			}
			catch(ExecutionEngineException ex)
			{
				ultimaaReferencia="E|" + ex.Message;
				return ultimaaReferencia;
			}
			catch(Exception ex)
			{
				ultimaaReferencia="00-00000-0000000";
				return ultimaaReferencia;
			}
		}
		
		public string ObtenerUltimoCorrelativoSunat(string oficinaVenta,string claseFactura,string codigoCaja)
		{
			SAP_SIC_Pagos pagosSap = ConectaSAP();
			String ultimoNumeroCorrelativo = string.Empty;
			string ultimaaReferencia=String.Empty;
			BAPIRET2Table logOperacionTable = new BAPIRET2Table();
			try
			{
				pagosSap.Zsicar_Trs_Act_Corr(codigoCaja,claseFactura,oficinaVenta,out ultimaaReferencia,out ultimoNumeroCorrelativo,ref logOperacionTable);
				DataTable tbLogOperacion = logOperacionTable.ToADODataTable();
				if(tbLogOperacion.Rows.Count>0)
					if(tbLogOperacion.Rows[0]["TYPE"].ToString().StartsWith("E"))
						throw new ExecutionEngineException(tbLogOperacion.Rows[0]["MESSAGE"].ToString());
				pagosSap.Connection.Close();
				return ultimaaReferencia;
			}
			catch(ExecutionEngineException ex)
			{
				ultimaaReferencia="E|" + ex.Message;
				return ultimaaReferencia;
			}
			catch(Exception ex)
			{
				ultimaaReferencia="00-00000-0000000";
				return ultimaaReferencia;
			}
		}
		
		//SANS
		public string ConsultarIccid_Zs(string Clase_Venta, string Nro_Telefono, string Tipo_Venta, string Fecha_Cambio, string Material, string Material_Antig, string Serie)
		{
			string Matnr;
			string Imei = "";
			string Fecha_Venta;

			try
			{
				SAP_SIC_Pagos proxy = ConectaSAP();

				BAPIRET2Table objRetorno = new BAPIRET2Table();
				proxy.Zpvu_Rfc_Con_Telefono_Sans_Zs(Clase_Venta, Fecha_Cambio, Material, Material_Antig, Nro_Telefono, Serie, Tipo_Venta, out Fecha_Venta, out Imei, out Matnr, ref objRetorno);
				proxy.Connection.Close();
			}
			catch (Exception ex)
			{
				Imei = "0000000000000000";
			}
			return Imei;
		}
		//SANS
        		
	}
}
