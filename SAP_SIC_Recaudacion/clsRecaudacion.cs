using System;
using System.Data;
//using System.EnterpriseServices;
//using System.Runtime.InteropServices;

namespace SAP_SIC_Recaudacion
{
	/// <summary>
	/// Descripción breve de clsRecaudacion.
	/// </summary>
	/// 

//    [Serializable(),JustInTimeActivation(true),Transaction(TransactionOption.Required),Synchronization(SynchronizationOption.Required),ClassInterface(ClassInterfaceType.AutoDual)]
	public class clsRecaudacion
	{
		int gintCodErrMetodo = 8;
		int gintCodMetodoOK = 0;
		
		public clsRecaudacion()
		{
			//
			// TODO: agregar aquí la lógica del constructor
			//
		}
		

	/**********************************************************************
	*	METODO QUE SE ENCARGA DE ACTUALIZAR LA DEUDA DEL CLIENTE
	**********************************************************************/
		//[AutoComplete(true)]
		public string Set_RegistroDeuda(string strDeudaSAP, string strRecibosSAP, 
			string strPagosSAP, out string strValorResultado)
		{
			string strTransaccion;
			DataTable dtLog;
			string strResult;

			string[] strTrama;

			strValorResultado = "";
			strResult = "";
			
			try
			{
				ZST_DEUDA_RECAUDACIONTable objDeuda = new ZST_DEUDA_RECAUDACIONTable();
				ZST_RECIBO_RECAUDACIONTable objRecibos = new ZST_RECIBO_RECAUDACIONTable();
				ZST_PAGOS_RECAUDACIONTable objPagos = new ZST_PAGOS_RECAUDACIONTable();
				BAPIRET2Table objLog = new BAPIRET2Table();

				ZST_DEUDA_RECAUDACION objLinDeuda = new ZST_DEUDA_RECAUDACION();
				
				

				SAP_SIC_Recaudacion proxy = ConectaSAP();
			
				strTrama = strDeudaSAP.Split(';');
				if (strTrama.Length> 0 )
				{
					objLinDeuda.Nro_Transaccion = strTrama[0];
					objLinDeuda.Nom_Deudor = strTrama[1];
					objLinDeuda.Ruc_Deudor = strTrama[2];
					objLinDeuda.Oficina_Venta = strTrama[3];
					objLinDeuda.Nom_Of_Venta = strTrama[4];
					objLinDeuda.Mon_Pago = strTrama[5];
					objLinDeuda.Importe_Pago = Convert.ToDecimal(strTrama[6]);
					objLinDeuda.Fecha_Transac = FormatoFecha(strTrama[7]);
					objLinDeuda.Hora_Transac = strTrama[8];
					objLinDeuda.Estado_Transac = strTrama[9];
					objLinDeuda.Nro_Telefono = strTrama[10];
					objLinDeuda.Cod_Cajero = strTrama[11];
					objLinDeuda.Nom_Cajero = strTrama[12];
					objLinDeuda.Nro_Trace_Cons = strTrama[13];
					objLinDeuda.Tipo_Doc_Deudor = strTrama[14];
					objLinDeuda.Nro_Doc_Deudor = strTrama[15];

					objDeuda.Add(objLinDeuda);
				}

				string [] arrRecibos = strRecibosSAP.Split('|');
				for (int j=0; j< arrRecibos.Length;j++)
				{
					strTrama =arrRecibos[j].Split(';'); // strRecibosSAP.Split(';');
					if (arrRecibos[j]!="" &&  strTrama.Length> 0 )
					{
						ZST_RECIBO_RECAUDACION objLinRecibos = new ZST_RECIBO_RECAUDACION();
						objLinRecibos.Nro_Transaccion = strTrama[0];
						objLinRecibos.Posicion = strTrama[1];
						objLinRecibos.Tipo_Doc_Recaud = strTrama[2];
						objLinRecibos.Nro_Doc_Recaud = strTrama[3];
						objLinRecibos.Moneda_Docum = strTrama[4];
						objLinRecibos.Importe_Recibo = Convert.ToDecimal(strTrama[5]);
						objLinRecibos.Importe_Pagado = Convert.ToDecimal(strTrama[6]);
						objLinRecibos.Nro_Cobranza = strTrama[7];
						objLinRecibos.Nro_Ope_Acree = strTrama[8];
						objLinRecibos.Fecha_Emision = FormatoFecha(strTrama[9]);
						objLinRecibos.Fecha_Pago = FormatoFecha(strTrama[10]);
						objLinRecibos.Nro_Trace_Anul = strTrama[11];
						objLinRecibos.Nro_Trace_Pago = strTrama[12];
						objLinRecibos.Desc_Servicio  = strTrama[13];
						objLinRecibos.Fecha_Hora = strTrama[14];
						objLinRecibos.Servicio = strTrama[15];

						objRecibos.Add(objLinRecibos);
					}
				}

				string [] arrPagos = strPagosSAP.Split('|');
				for (int j=0; j< arrPagos.Length;j++)
				{
					strTrama =arrPagos[j].Split(';'); //strPagosSAP.Split(';');
					if (arrPagos[j]!="" && strTrama.Length> 0 )
					{
						ZST_PAGOS_RECAUDACION objLinPagos = new ZST_PAGOS_RECAUDACION();
						objLinPagos.Nro_Transaccion = strTrama[0];
						objLinPagos.Posicion = strTrama[1];
						objLinPagos.Via_Pago = strTrama[2];
						objLinPagos.Importe_Pagado = Convert.ToDecimal(strTrama[3]);
						objLinPagos.Nro_Cheque = strTrama[4];
						objLinPagos.Belnr = strTrama[5];
						objLinPagos.Desc_Via_Pago = strTrama[6];

						objPagos.Add(objLinPagos);
					}
				}
				proxy.Zpvu_Rfc_Trs_Reg_Deuda(out strTransaccion, ref objDeuda, ref objLog, ref objPagos, ref objRecibos);

				dtLog = objLog.ToADODataTable();
				
				for(int i=0;i<dtLog.Rows.Count;i++)
				{
					if ((string)dtLog.Rows[i].ItemArray[0] == "E")
					{
						strResult = gintCodErrMetodo.ToString() + "@" + strTransaccion + ";" + dtLog.Rows[i][3];
						strValorResultado = gintCodErrMetodo.ToString();
					}
					else
					{
						strResult = gintCodMetodoOK.ToString() + "@" + strTransaccion + ";" + dtLog.Rows[i][3];
						strValorResultado = gintCodMetodoOK.ToString();
					}
				}
				proxy.Connection.Close();
			}
			catch //(Exception ex)
			{
				strValorResultado = "";
				strResult = "";
			}
			return strResult;
		}

	/**********************************************************************
	*	METODO QUE SE ENCARGA DE MOSTRAR LA DEUDA DEL CLIENTE
	**********************************************************************/
		//[AutoComplete(true)]
		public DataSet Get_RegistroDeuda(string strNroTransaccion)
		{
			DataSet dsResult;
			
            try
			{
				ZST_DEUDA_RECAUDACIONTable objDeuda = new ZST_DEUDA_RECAUDACIONTable();
				ZST_RECIBO_RECAUDACIONTable objRecibos = new ZST_RECIBO_RECAUDACIONTable();
				ZST_PAGOS_RECAUDACIONTable objPagos = new ZST_PAGOS_RECAUDACIONTable();
				BAPIRET2Table objLog = new BAPIRET2Table();

			
				SAP_SIC_Recaudacion proxy = ConectaSAP();
			
				proxy.Zpvu_Rfc_Con_Deuda(strNroTransaccion,ref objDeuda, ref objLog,ref objPagos,ref objRecibos);

				foreach (ZST_DEUDA_RECAUDACION obFila in objDeuda)
				{
					obFila.Fecha_Transac=obFila.Fecha_Transac.Substring(6,2)+"/"+obFila.Fecha_Transac.Substring(4,2)+"/"+obFila.Fecha_Transac.Substring(0,4);
				}
				foreach (ZST_RECIBO_RECAUDACION obFila in objRecibos)
				{
					obFila.Fecha_Emision=obFila.Fecha_Emision.Substring(6,2)+"/"+obFila.Fecha_Emision.Substring(4,2)+"/"+obFila.Fecha_Emision.Substring(0,4);
					obFila.Fecha_Pago=obFila.Fecha_Pago.Substring(6,2)+"/"+obFila.Fecha_Pago.Substring(4,2)+"/"+obFila.Fecha_Pago.Substring(0,4);
				}

				dsResult= new DataSet();
				dsResult.Tables.Add(objDeuda.ToADODataTable());
				dsResult.Tables.Add(objRecibos.ToADODataTable());
				dsResult.Tables.Add(objPagos.ToADODataTable());
				dsResult.Tables.Add(objLog.ToADODataTable());
				proxy.Connection.Close();
			}
			catch //(Exception ex)
			{
				dsResult=null;
			}
			return dsResult;
		}




	/**********************************************************************
	*	METODO QUE SE ENCARGA DE LISTAR EL POOL DE RECAUDACION
	**********************************************************************/
		//[AutoComplete(true)]
		//nhuaringa auto strUsuario
		public DataSet Get_PoolRecaudacion(string strCodCajero, string strFechaTransaccion, string strOficinaVenta, 
										  string strNroTransaccion,  string strRucDeudor, 
										  string strNroTelefono, string strEstadoTransaccion)
		{
			
			DataSet dsResult;
			try
			{
				ZST_POOL_RECAUDACIONTable obRec = new ZST_POOL_RECAUDACIONTable();
				BAPIRET2Table objLog = new BAPIRET2Table();

				strFechaTransaccion = strFechaTransaccion.Substring(6,4)+"/"+strFechaTransaccion.Substring(3,2)+"/"+strFechaTransaccion.Substring(0,2);

				SAP_SIC_Recaudacion proxy = ConectaSAP();
				//nhuaringa auto strUsuario
				proxy.Zpvu_Rfc_Con_Pool_Recaudacion(strCodCajero,strEstadoTransaccion,strFechaTransaccion,
													strNroTelefono,strNroTransaccion,strOficinaVenta,
													strRucDeudor,ref objLog, ref obRec);
				
				foreach (ZST_POOL_RECAUDACION obRow in obRec )
				{
					obRow.Fecha_Transac=obRow.Fecha_Transac.Substring(6,2)+"/"+obRow.Fecha_Transac.Substring(4,2)+"/"+obRow.Fecha_Transac.Substring(0,4);
				}

				dsResult= new DataSet();
				dsResult.Tables.Add(obRec.ToADODataTable());
				dsResult.Tables.Add(objLog.ToADODataTable());
				proxy.Connection.Close();

			}
			catch //(Exception ex)
			{
				dsResult=null;
			}
		return dsResult;
		}



		/**********************************************************************
		*	METODO QUE SE ENCARGA DE ACTUALIZAR LA DEUDA DEL CLIENTE
		**********************************************************************/
		//[AutoComplete(true)]
		public DataSet Set_EstadoRecaudacion(string strNroTransaccion, string strEstadoTransaccion, 
			string strPosicion, string strTraceAnulacionSAP)
		{
			
			DataSet dsResult;
			
			try
			{
				BAPIRET2Table objLog = new BAPIRET2Table();
				SAP_SIC_Recaudacion proxy = ConectaSAP();
				proxy.Zpvu_Rfc_Trs_Act_Est_Recaud( strEstadoTransaccion , strNroTransaccion,strPosicion,strTraceAnulacionSAP,ref objLog);
                
				dsResult = new DataSet();
				dsResult.Tables.Add(objLog.ToADODataTable());
				proxy.Connection.Close();

			}
			catch //(Exception ex)
			{
				dsResult=null;
			}
			return dsResult;
		}

		private SAP_SIC_Recaudacion ConectaSAP()
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

//			strApplicationServer=strApplicationServer==null?"172.19.43.34":strApplicationServer;
//			strSistema = strSistema==null?"00":strSistema;
            SAP_SIC_Recaudacion proxy;

			if (strLoadBal == "0")
			{
				proxy = new SAP_SIC_Recaudacion("CLIENT=" + strCliente + " USER=" + strUsuario + " PASSWD=" + strPassword + " LANG=" + strLanguage +" ASHOST=" + strApplicationServer + " SYSNR=" + strSistema);
			}
			else
			{
                proxy = new SAP_SIC_Recaudacion("CLIENT=" + strCliente + " USER=" + strUsuario + " PASSWD=" + strPassword + " LANG=" + strLanguage +" MSHOST=" + strMessServ + " R3NAME=" + strR3Name + " GROUP=" + strGroup);
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

		//nuevo
        //[AutoComplete(true)]
		public string Set_LogRecaudacion(string strRecaudacion , string strDocum ) 
		{
		//ZPVU_RFC_TRS_LOG_RECAUDACION
			string trace;
			ZST_REC_LOG_DOCUM_POTable obLogDocum = new ZST_REC_LOG_DOCUM_POTable();			
			ZST_REC_LOG_RECAUDACIONTable obRecaudacion = new ZST_REC_LOG_RECAUDACIONTable();
			BAPIRET2Table obLog = new BAPIRET2Table();
			try
			{

				string [] arrRecreg = strRecaudacion.Split('|');
				for (int j=0; j<arrRecreg.Length; j++)
				{
					ZST_REC_LOG_RECAUDACION obRec = new ZST_REC_LOG_RECAUDACION();				
					string[] arrRecaudacion = arrRecreg[j].Split(';');  //strRecaudacion.Split(';');
					for (int i=0; i<arrRecaudacion.Length && i<31;i++)
					{
						if (arrRecaudacion[i]!="")
						{
							if (obRec[i] is int)						
								obRec[i]= int.Parse( arrRecaudacion[i]);						
							else
								if (obRec[i] is decimal)						
								obRec[i]= decimal.Parse(arrRecaudacion[i]);	
							else
								obRec[i]= (object) arrRecaudacion[i];	
						}
					}
					if (arrRecaudacion.Length >= 31)
					  obRec.Fecha_Transac = FormatoFecha(arrRecaudacion[22]);
					else
                      //obRec.Fecha_Transac = FormatoFecha(DateTime.Now.ToString("d"));
						obRec.Fecha_Transac = FormatoFecha(DateTime.Now.Day.ToString("00") + "/" +  DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString("0000"));

					obRecaudacion.Add(obRec);
					
				}

				
				string [] arrDocumreg = strDocum.Split('|');
				for (int j=0; j<arrDocumreg.Length; j++)
				{
					ZST_REC_LOG_DOCUM_PO obDoc = new ZST_REC_LOG_DOCUM_PO();
					string[] arrDocum = arrDocumreg[j].Split(';');	// strDocum.Split(';');
					for (int i=0; i<arrDocum.Length && i<9;i++)
					{
						if (obDoc[i] is int)						
							obDoc[i]= int.Parse(arrDocum[i]);						
						else
							if (obDoc[i] is decimal)						
							obDoc[i]= decimal.Parse(arrDocum[i]);	
						else
							obDoc[i]= (object) arrDocum[i];						

					}
					obLogDocum.Add(obDoc);
				}
				SAP_SIC_Recaudacion proxy = ConectaSAP();
				proxy.Zpvu_Rfc_Trs_Log_Recaudacion(out trace, ref obLogDocum, ref obLog, ref obRecaudacion);
				proxy.Connection.Close();
			}
			catch (Exception ex)
			{
				throw new ApplicationException(ex.Message,ex);
			}
			return trace;

		}
/*
		public DataSet Set_RecaudacionDAC(string strOficina, string strCliente, string strFecha, string strPagos, string strUsuario, out string strNroAt)
		{
			DataSet dsReturn = new DataSet();

			try
			{
				ZST_RFC_COMPTable objComp = new ZST_RFC_COMPTable();
				ZST_RFC_COMP objLinComp = new ZST_RFC_COMP();
				BAPIRET2Table objLog = new BAPIRET2Table();

				Random rnd=new Random(unchecked((int) DateTime.Now.Ticks));
				string strNroRan = Convert.ToString(rnd.Next());

				string[] strTrama = strPagos.Split('|');
				for (int i=0;i<strTrama.Length;i++)
				{
                    
					string[] strTrama2 = strTrama[i].Split(';');
					objLinComp.Cliente = LPad(strCliente,10,"0");
					objLinComp.Fecha = FormatoFecha(strFecha);
					objLinComp.Monto = Convert.ToDecimal(strTrama2[1]);
					objLinComp.Numero_Operacion = strNroRan; //strTrama2[0];
					objLinComp.Via_Pago = strTrama2[2];
					objLinComp.Documento = strTrama2[0];
					//objLinComp.Banco = strTrama2[3];
					
					objComp.Add(objLinComp);

					objLinComp = null;
					objLinComp = new ZST_RFC_COMP();
				}

				SAP_SIC_Recaudacion proxy = ConectaSAP();

				proxy.Zpvu_Rfc_Trs_Pago_Dealer(strOficina,strUsuario,out strNroAt,ref objComp,ref objLog);

				strNroAt = "";
				
				dsReturn.Tables.Add(objLog.ToADODataTable());
				proxy.Connection.Close();

			}
			catch (Exception ex)
			{
				dsReturn = null;
				throw new ApplicationException("Set_RecaudacionDAC",ex);
			}

			return dsReturn;


		}
*/
		public DataSet Set_RecaudacionDAC(string strOficina, string strCliente, string strFecha, string strPagos, string strUsuario, out string strNroAt)
		{
            DataSet dsReturn = new DataSet();
			try
			{
				ZST_RFC_COMP_MTable objComp = new ZST_RFC_COMP_MTable();
				ZST_RFC_COMP_M objLinComp = new ZST_RFC_COMP_M();
				BAPIRET2Table objLog = new BAPIRET2Table();

				Random rnd=new Random(unchecked((int) DateTime.Now.Ticks));
				string strNroRan = Convert.ToString(rnd.Next());

                string[] strTrama = strPagos.Split('|');
				for (int i=0;i<strTrama.Length;i++)
				{
                    string[] strTrama2 = strTrama[i].Split(';');
					objLinComp.Cliente = LPad(strCliente,10,"0");
					objLinComp.Fecha = FormatoFecha(strFecha);
					objLinComp.Monto = Convert.ToDecimal(strTrama2[1]);
					objLinComp.Numero_Operacion = strNroRan; //strTrama2[0];
					objLinComp.Via_Pago = strTrama2[2];
					objLinComp.Documento = strTrama2[0];
					objLinComp.Banco = strTrama2[3];
					
                    objComp.Add(objLinComp);

                    objLinComp = null;
					objLinComp = new ZST_RFC_COMP_M();
				}
                SAP_SIC_Recaudacion proxy = ConectaSAP();
				proxy.Zpvu_Rfc_Trs_Pago_Dealer_M("1",strOficina,strUsuario,out strNroAt,ref objComp,ref objLog);
				strNroAt = "";
				
				dsReturn.Tables.Add(objLog.ToADODataTable());
				proxy.Connection.Close();

			}
			catch (Exception ex)
			{
				dsReturn = null;
				throw new ApplicationException("Set_RecaudacionDAC",ex);
			}

			return dsReturn;
		}

		public DataSet Get_PoolDAC(string OfVta, string strUsuario, string strFecha)
		{
			DataSet dsReturn = new DataSet();

			try
			{
                ZST_POOL_PAGOS_RECAUDTable objPagos = new ZST_POOL_PAGOS_RECAUDTable();
                BAPIRET2Table objLog = new BAPIRET2Table();

                SAP_SIC_Recaudacion proxy = ConectaSAP();
                proxy.Zpvu_Rfc_Con_Pool_Pagos_Recaud(FormatoFecha(strFecha),strUsuario,OfVta, ref objPagos, ref objLog);

				dsReturn.Tables.Add(objPagos.ToADODataTable());
				dsReturn.Tables.Add(objLog.ToADODataTable());
				proxy.Connection.Close();
			}
			catch (Exception ex)
			{
				dsReturn = null;
				throw new ApplicationException("Get_PoolDAC",ex);
			}
			return dsReturn;
		}

		public DataSet Get_ConsultaPagoDAC(string strNroAt)
		{
			DataSet dsReturn = new DataSet();

			try
			{
				ZST_PAGO_RECAUDTable objPagos = new ZST_PAGO_RECAUDTable();
				BAPIRET2Table objLog = new BAPIRET2Table();

				SAP_SIC_Recaudacion proxy = ConectaSAP();
				proxy.Zpvu_Rfc_Con_Pago_Recaudacion(strNroAt,ref objPagos,ref objLog);

				dsReturn.Tables.Add(objPagos.ToADODataTable());
				dsReturn.Tables.Add(objLog.ToADODataTable());
				proxy.Connection.Close();
			}
			catch (Exception ex)
			{
				dsReturn = null;
				throw new ApplicationException("Get_ConsultaPagoDAC",ex);
			}
			return dsReturn;
		}

		public DataSet Get_ConsultaDeudaDAC(string strCliente, out string strName, out decimal decMonto, out string strDoc)
		{
			DataSet dsReturn = new DataSet();

			try
			{ 
				BAPIRET2Table objLog = new BAPIRET2Table();

				SAP_SIC_Recaudacion proxy = ConectaSAP();
				proxy.Zpvu_Rfc_Con_Dealer_Deuda_Tot(strCliente, out decMonto, out strName, out strDoc,ref objLog);
				
				dsReturn.Tables.Add(objLog.ToADODataTable());
				proxy.Connection.Close();
			}
			catch (Exception ex)
			{
				decMonto = 0;
				strName = "";
				strDoc = "";
				dsReturn = null;
				throw new ApplicationException("Get_ConsultaDeudaDAC",ex);
			}
			return dsReturn;
		}


		public DataSet Set_PagoCuotas(string Oficina, string Usuario, string NroTelef, string Fecha, string strPago,
			                          out string Name1,out string NroAt, out string Stcd1)
		{
			DataSet dsReturn = new DataSet();

//			try
//			{
				string[] strValores = strPago.Split('|');

				BAPIRET2Table objLog = new BAPIRET2Table();
				ZST_RFC_COMP_TELEFTable objPago = new ZST_RFC_COMP_TELEFTable();
				ZST_RFC_COMP_TELEF objLinPago = new ZST_RFC_COMP_TELEF();
			    
			    Random rnd=new Random(unchecked((int) DateTime.Now.Ticks));
			    string strNroRan = Convert.ToString(rnd.Next());

				for (int i=0;i<strValores.Length;i++)
				{
					string[] strLinValores = strValores[i].Split(';');

					objLinPago.Numero_Operacion = strNroRan; //Aleatorio
					objLinPago.Via_Pago = strLinValores[2];
					objLinPago.Monto = Convert.ToDecimal(strLinValores[1]);
					objLinPago.Documento = strLinValores[0];

					objPago.Add(objLinPago);
					objLinPago = null;
					objLinPago = new ZST_RFC_COMP_TELEF();
				}

				SAP_SIC_Recaudacion proxy = ConectaSAP();
				proxy.Zpvu_Rfc_Trs_Pago_Telef(FormatoFecha(Fecha),NroTelef,Oficina,Usuario,out Name1,out NroAt,out Stcd1,ref objPago,ref objLog);
				
				dsReturn.Tables.Add(objLog.ToADODataTable());
			    proxy.Connection.Close();
			//}
//			catch (Exception ex)
//			{
//				Stcd1 = "";
//				Name1 = "";
//				NroAt = "";
//				dsReturn = null;
//				throw new ApplicationException("Set_PagoCuotas",ex);
//			}
			return dsReturn;
		}

		public DataSet Get_DatosCuotas(string strNroTelf,out string strNombre,out string strNroDoc, out string strCodSAP, out decimal SubTot,out decimal IGV, out decimal Total)
		{

			DataSet dsReturn = new DataSet();
			
			try
			{
				ZST_RFC_TELF_DEUTable objCuotas = new ZST_RFC_TELF_DEUTable();
				BAPIRET2Table objLog = new BAPIRET2Table();

				SAP_SIC_Recaudacion proxy = ConectaSAP();

				proxy.Zpvu_Rfc_Con_Telef_Deuda("PE01","","",strNroTelf,"",out strCodSAP,out IGV,out strNombre,out SubTot, out strNroDoc,out Total,ref objCuotas,ref objLog);

                DataTable dt = new DataTable();

				dt = objCuotas.ToADODataTable();
				string strFecha;

				for (int i=0;i<dt.Rows.Count;i++)
				{
					strFecha = (string)dt.Rows[i]["FECHA"];
					strFecha = strFecha.Substring(6,2) + "/" + strFecha.Substring(4,2) + "/" + strFecha.Substring(0,4);
					dt.Rows[i]["FECHA"] = strFecha;

					strFecha = (string)dt.Rows[i]["FECHA_PAGO"];
					if (strFecha.Trim()!="00000000")
					{
						strFecha = strFecha.Substring(6,2) + "/" + strFecha.Substring(4,2) + "/" + strFecha.Substring(0,4);
						dt.Rows[i]["FECHA_PAGO"] = strFecha;
					}
					else
					{
                        dt.Rows[i]["FECHA_PAGO"] = "";
					}
					//dt.Rows[i]["MONTO"] = Convert.ToDecimal(dt.Rows[i]["MONTO"]) *-1;

				}

				dsReturn.Tables.Add(dt);
				dsReturn.Tables.Add(objLog.ToADODataTable());
				proxy.Connection.Close();

			}
			catch (Exception ex)
			{
				strNombre = "";
				strNroDoc = "";
				strCodSAP = "";
				SubTot = 0;
				IGV = 0;
				Total = 0;
				dsReturn = null;
                throw new ApplicationException("Get_DatosCuotas",ex);
			}
            return dsReturn;
		}

		public DataSet Get_VentaxTelefono(string strTelefono)
		{
           DataSet dsReturn = new DataSet();;
			try
			{
				BAPIRET2Table objLog = new BAPIRET2Table();
				ZST_DATA_VENTAS_TELEFTable objData = new ZST_DATA_VENTAS_TELEFTable();

				SAP_SIC_Recaudacion proxy = ConectaSAP();

				proxy.Zpvu_Rfc_Con_Venta_X_Telefono("","",strTelefono,"", ref objData,ref objLog);

				dsReturn.Tables.Add(objData.ToADODataTable());
				dsReturn.Tables.Add(objLog.ToADODataTable());
				proxy.Connection.Close();

			}
			catch (Exception ex)
			{ 
				dsReturn = null;
				throw new ApplicationException("Get_VentaxTelefono",ex);
			}

			return dsReturn;

		}
        
		private string LPad(string strCad, int intCant, string strChar)
		{
			string strResult=strCad;
			int Longitud = strResult.Length;

			for (int i=Longitud;i<intCant;i++)
			{
				strResult = strChar + strResult;
			}

			return strResult;

		}

		/**********************************************************************
			*	METODO QUE SE ENCARGA DE LISTAR EL POOL DE CAJAS CERRADAS
			**********************************************************************/
		//[AutoComplete(true)]
		//nhuaringa cajacerrada
		public DataSet Get_PoolCajaCerrada(string strCodCajero,string strOficinaVenta, string strFechaini, string strFechafin )
		{
			
			DataSet dsResult;
			try
			{
				ZST_CON_CUADRE_CAJEROTable obCua = new ZST_CON_CUADRE_CAJEROTable();
				//BAPIRET2Table objLog = new BAPIRET2Table();

				SAP_SIC_Recaudacion proxy = ConectaSAP();
				//nhuaringa auto strUsuario
				
				proxy.Zpvu_Rfc_Con_Cuadre_Cajero(strCodCajero,strFechafin,strFechaini,
					strOficinaVenta, ref obCua);
				//				proxy.Zpvu_Rfc_Con_Cuadre_Cajero("","20130515","20130515",
				//				"0006", ref obCua);

				foreach (ZST_CON_CUADRE_CAJERO obRow in obCua )
				{
					obRow.Fecha_Asignacion =obRow.Fecha_Asignacion.Substring(6,2)+"/"+obRow.Fecha_Asignacion.Substring(4,2)+"/"+obRow.Fecha_Asignacion.Substring(0,4);
					obRow.Hora_Asignacion =obRow.Hora_Asignacion.Substring(0,2)+":"+obRow.Hora_Asignacion.Substring(2,2)+":"+obRow.Hora_Asignacion.Substring(4,2);
					if (obRow.Fecha_Cierre == "00000000")
					{
						obRow.Fecha_Cierre = "-";
					}
					else
					{
						obRow.Fecha_Cierre =obRow.Fecha_Cierre.Substring(6,2)+"/"+obRow.Fecha_Cierre.Substring(4,2)+"/"+obRow.Fecha_Cierre.Substring(0,4);
					}
					
				}

				dsResult= new DataSet();
				dsResult.Tables.Add(obCua.ToADODataTable());
				proxy.Connection.Close();
		  
			}
			catch //(Exception ex)
			{
				dsResult=null;
			}
			return dsResult;
		} 

		//SANS
		public DataSet Get_VentaxTelefonoZ(string strMaterial, string strMaterialAntiguo, string strTelefono, string strSerie)
		{
			DataSet dsReturn = new DataSet();;
			try
			{
				BAPIRET2Table objLog = new BAPIRET2Table();
				ZST_DATA_VENTAS_TELEFTable objData = new ZST_DATA_VENTAS_TELEFTable();

				SAP_SIC_Recaudacion proxy = ConectaSAP();

				proxy.Zpvu_Rfc_Con_Venta_X_Telefonoz(strMaterial, strMaterialAntiguo, strTelefono, strSerie, ref objData,ref objLog);				

				dsReturn.Tables.Add(objData.ToADODataTable());
				dsReturn.Tables.Add(objLog.ToADODataTable());
				proxy.Connection.Close();

			}
			catch (Exception ex)
			{ 
				dsReturn = null;
				throw new ApplicationException("Get_VentaxTelefonoZ",ex);
			}

			return dsReturn;

		}

		public DataSet Get_DatosCuotas_ZS(string strNroTelf,out string strNombre,out string strNroDoc, out string strCodSAP, out decimal SubTot,out decimal IGV, out decimal Total, string strMaterial, string strMaterialAntiguo, string strSerie)
		{

			DataSet dsReturn = new DataSet();
			
			try
			{
				ZST_RFC_TELF_DEUTable objCuotas = new ZST_RFC_TELF_DEUTable();
				BAPIRET2Table objLog = new BAPIRET2Table();

				SAP_SIC_Recaudacion proxy = ConectaSAP();

				proxy.Zpvu_Rfc_Con_Telef_Deuda_Zs("PE01", strMaterial, strMaterialAntiguo, strNroTelf, strSerie, out strCodSAP,out IGV,out strNombre,out SubTot, out strNroDoc,out Total,ref objCuotas,ref objLog);

				DataTable dt = new DataTable();

				dt = objCuotas.ToADODataTable();
				string strFecha;

				for (int i=0;i<dt.Rows.Count;i++)
				{
					strFecha = (string)dt.Rows[i]["FECHA"];
					strFecha = strFecha.Substring(6,2) + "/" + strFecha.Substring(4,2) + "/" + strFecha.Substring(0,4);
					dt.Rows[i]["FECHA"] = strFecha;

					strFecha = (string)dt.Rows[i]["FECHA_PAGO"];
					if (strFecha.Trim()!="00000000")
					{
						strFecha = strFecha.Substring(6,2) + "/" + strFecha.Substring(4,2) + "/" + strFecha.Substring(0,4);
						dt.Rows[i]["FECHA_PAGO"] = strFecha;
					}
					else
					{
						dt.Rows[i]["FECHA_PAGO"] = "";
					}
					//dt.Rows[i]["MONTO"] = Convert.ToDecimal(dt.Rows[i]["MONTO"]) *-1;

				}

				dsReturn.Tables.Add(dt);
				dsReturn.Tables.Add(objLog.ToADODataTable());
				proxy.Connection.Close();

			}
			catch (Exception ex)
			{
				strNombre = "";
				strNroDoc = "";
				strCodSAP = "";
				SubTot = 0;
				IGV = 0;
				Total = 0;
				dsReturn = null;
				throw new ApplicationException("Get_DatosCuotas_ZS",ex);
			}
			return dsReturn;
		}

		//SANS

	}
}
