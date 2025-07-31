using SAP_SIC_Recaudacion;
using System;
using System.Data;

namespace COM_SIC_Procesa_Pagos
{
	/// <summary>
	/// Summary description for Anulaciones.
	/// </summary>
	public class Anulaciones 
	{
		 private readonly int EJECUCION_OK = 0;
		 private readonly int EJECUCION_ERROR = 8;


		/// <summary>
		/// No modificar el valor, es una constante para la validacion del ambiente que se ejecuta la aplicacion es de desarrollo
		/// </summary>
		private readonly string TEST_MODE;
		
		/// <summary>
		/// El valor para mostrar la informacion de inputs en el log es TEST
		/// en el ambiente de produccion debe ser PROD
		/// </summary>
		private string LogMode;

		

		public Anulaciones()
		{
			TEST_MODE ="TEST";
			LogMode = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["LogMode"]);
		}


		[Obsolete("No usar esta funcion para cuadre de caja por cajero")]
		public void AnularTransaccionSAP(string fechaActual)
		{
			int cont=0;
			int contReg=0;
			string descripcionTrs="";
			string deserror="";
			string descripcion,codigo, fecha, usuario,nombretabla="";
			long archivo=0;
                        //INI PROY-140126
			Common.WriteLog("¨*¨*¨*¨*¨*¨*¨*¨*¨*¨* INICIANDO TRANSACCCION DE ANULACIONES DE PAGOS ¨*¨*¨*¨*¨*¨*¨*¨*¨*");
			Common.WriteLog("¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*RFC (Zpvu_Rfc_Trs_Act_Est_Recaud)¨*¨*¨*¨*¨*¨*¨*¨*¨*¨**");

			Common.WriteLog("Inicio consultar listarPagosAnuladosNoMigrados CONF_MIG_GetEstadoRecaudacion");
			Common.WriteLog("Parametro fechaActual: {0}",fechaActual);
			DataTable tbNoMigradas = listarPagosAnuladosNoMigrados(fechaActual,"","");
			Common.WriteLog("Numero de deudas a migrar: {0}",tbNoMigradas.Rows.Count);
			Common.WriteLog("Fin consultar listarPagosAnuladosNoMigrados CONF_MIG_GetEstadoRecaudacion");//FIN PROY-140126
			
			tbNoMigradas.Columns.Add("MigracionOK", typeof(bool));

			foreach (DataRow filaPago in tbNoMigradas.Rows)
			{
				BAPIRET2Table logSapTable = new BAPIRET2Table();
				String mensajeOperacion = String.Empty;

				SAP_SIC_Recaudacion.SAP_SIC_Recaudacion recaudacion = new SAP_SIC_Recaudacion.SAP_SIC_Recaudacion(SAPDataConnector.ConnectionString);
				string numeroTransaccion = "";

				try
				{
                                        //INI PROY-140126
					Common.WriteLog("********************* ENVIANDO TRANSACCION DE ANULACIONES ***********************");


					string EstadoTransaccion = filaPago["ESTADO"].ToString().Trim();
					string strNroTransaccion = filaPago["NRO_TRANSACCION"].ToString().Trim();
					string strPosicion = filaPago["POSTMP"].ToString().Trim();
					string strTraceAnulacionSAP = filaPago["TRACEANL"].ToString().Trim();

					Common.WriteLog(string.Format("{0}:{1}", "Inicio del Método Anulaciones.RegistrarTransaccionSAP.Zpvu_Rfc_Trs_Act_Est_Recaud", "Claro.SiscajasBatch.Data"));
					Common.WriteLog("EstadoTransaccion: {0} ", EstadoTransaccion);
					Common.WriteLog("NroTransaccion: {0}", strNroTransaccion);
					Common.WriteLog("Posicion: {0}",  strPosicion);
					Common.WriteLog("TraceAnulacionSAP: {0}", strTraceAnulacionSAP);//FIN PROY-140126
					
					//CONTADOR REGISTROS LEIDOS
					cont++;
					recaudacion.Zpvu_Rfc_Trs_Act_Est_Recaud(EstadoTransaccion, strNroTransaccion, strPosicion, strTraceAnulacionSAP, ref logSapTable);

					DataTable tbLogResult = logSapTable.ToADODataTable();
					int numeroResultadoOperacion = -1;
					foreach (DataRow item in tbLogResult.Rows)
					{
						numeroResultadoOperacion = item[0].Equals("E") ? EJECUCION_ERROR : EJECUCION_OK;
						mensajeOperacion = String.Format("{0}@,{1};{2}", numeroResultadoOperacion, numeroTransaccion, item[3]);
						descripcionTrs =item[3].ToString();
					}

					string str = "";

					if (numeroResultadoOperacion == EJECUCION_OK)
					{
						contReg++;
						deserror="";
						string RegistraZAP = "S";
						string recaudacionID = Convert.ToString(filaPago["RECAUDACIONID"]);


						this.registrarAnulacionesPago(recaudacionID, strPosicion, RegistraZAP, mensajeOperacion);
						filaPago[1] = true;
						string numOpera = Convert.ToString(strNroTransaccion);
                                                //INI PROY-140126
						Common.WriteLog(string.Format("{0}:{1}", "Inicio del Método Anulaciones.RegistrarTransaccionSAP.registrarResultadoOperacion", "Claro.SiscajasBatch.Data"));
						Common.WriteLog("RecaudacionID:{0}",recaudacionID);
						Common.WriteLog("Posicion:{0}", strPosicion);
						Common.WriteLog("RegistraZAP:{0}", RegistraZAP);
						Common.WriteLog("Log Registro:{0}", mensajeOperacion);

						str = String.Format("{0}:{1}", "Se realizó con exito la Transacción Zpvu_Rfc_Trs_Act_Est_Recaud", mensajeOperacion);
						
						Common.WriteLog("Mensaje: SE ENVIO CORRECTAMENTE LA TRANSACCION");//PROY-140126
					}
					else
					{
						numeroTransaccion=strNroTransaccion;						
						deserror="No se envio Correctamente la Transaccion";
						str = String.Format("{0}:{1}", "No Se realizó con exito la Transacción Zpvu_Rfc_Trs_Act_Est_Recaud", mensajeOperacion);
						
						Common.WriteLog("Mensaje: NO SE ENVIO CORRECTAMENTE LA TRANSACCION");
					}

					recaudacion.Connection.Close();

					Common.WriteLog(String.Format("RESULTADO DE ANULACION: {0}", str));
	

					Common.WriteLog("********************* FIN DE TRANSACCION DE ANULACIONES **********************\n");//FIN PROY-140126

				}
				catch (Exception ex)
				{
					//this.LogTransaction.Error(String.Format("{0}:{1}", "Excepción en la Transacción Zpvu_Rfc_Trs_Act_Est_Recaud", "Numero Transacción: " + numeroTransaccion, "Claro.SiscajasBatch.Data.Anulaciones.RegistrarTransaccionSAP"), ex);
					deserror="";
					deserror=ex.ToString();
					Common.WriteLog(string.Format("************************ ERROR EN LA TRANSACCION DE ANULACIONES  ************************ Numero de Transaccion: {0}",numeroTransaccion)+ex);//PROY-140126
				}
				
				//REGISTRO EN LOG (T_LOG_TRANSACCION)
				descripcion= descripcionTrs;
				codigo=numeroTransaccion;
				//deserror=descripcionTrs;
				fecha = DateTime.Today.ToString("dd/MM/yyyy");
				usuario= Environment.UserDomainName.ToString();
				nombretabla="TI_EST_RECAUDACION";

				RegistrarLog(descripcion,codigo, deserror, fecha, usuario,nombretabla,archivo);	

				//this.LogTransaction.InfoFormat("\n");
			}
			if(cont == 0)
			{
				Common.WriteLog("Ningun registro ha sido leido{0}","");//PROY-140126
			}
                        //INI PROY-140126
			Common.WriteLog(String.Format("Total de Registros Leidos: {0}", cont));
			Common.WriteLog(String.Format("Total de Registros Enviados: {0}", contReg));
			Common.WriteLog("*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨* TRANSACCCION DE ANULACIONES DE PAGO FINALIZADA ¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*");//FIN PROY-140126

		}


		public bool AnularTransaccionSAP(string fechaActual,string codCajero,string codOficinaVenta, ref string mensaje)
		{
			int cont=0;
			int contReg=0;
			string descripcionTrs="";
			string deserror="";
			string descripcion,codigo, fecha, usuario,nombretabla="";
			long archivo=0;
			Common.UsuarioCaja = mensaje;
			mensaje = "";

			Common.WriteLog("---------------------- INICIANDO TRANSACCCION DE ANULACIONES DE PAGOS ----------------------");

			Common.WriteLog("Importando registros desde la BD: PCK_SICAR_OFF_SAP.CONF_MIG_GetEstadoRecaudacion...");
			Common.WriteLog("Parametro fechaActual: {0}",fechaActual);
			Common.WriteLog("Parametro Cajero: {0}",codCajero);
			Common.WriteLog("Parametro OficinaVenta: {0}",codOficinaVenta);
			
			DataTable tbNoMigradas = listarPagosAnuladosNoMigrados(fechaActual,codCajero,codOficinaVenta);

			Common.WriteLog("Importacion completada");
			
			tbNoMigradas.Columns.Add("MigracionOK", typeof(bool));

			foreach (DataRow filaPago in tbNoMigradas.Rows)
			{
				BAPIRET2Table logSapTable = new BAPIRET2Table();
				String mensajeOperacion = String.Empty;

				SAP_SIC_Recaudacion.SAP_SIC_Recaudacion recaudacion = new SAP_SIC_Recaudacion.SAP_SIC_Recaudacion(SAPDataConnector.ConnectionString);
				string numeroTransaccion = "";

				try
				{
					Common.WriteLog("------ ENVIANDO TRANSACCION DE ANULACIONES ------");
					Common.WriteLog("CODIGO DE REGISTRO [RECAUDACIONID] : {0}",filaPago["RECAUDACIONID"].ToString().Trim());

					string EstadoTransaccion = filaPago["ESTADO"].ToString().Trim();
					string strNroTransaccion = filaPago["NRO_TRANSACCION"].ToString().Trim();
					string strPosicion = filaPago["POSTMP"].ToString().Trim();
					string strTraceAnulacionSAP = filaPago["TRACEANL"].ToString().Trim();

					if(this.LogMode == this.TEST_MODE)
					{
						Common.WriteLog("{0}:{1}", "Inicio del Método Anulaciones.RegistrarTransaccionSAP.Zpvu_Rfc_Trs_Act_Est_Recaud", "Claro.SiscajasBatch.Data");
						Common.WriteLog("EstadoTransaccion: {0} ", EstadoTransaccion);
						Common.WriteLog("NroTransaccion: {0}", strNroTransaccion);
						Common.WriteLog("Posicion: {0}",  strPosicion);
						Common.WriteLog("TraceAnulacionSAP: {0}", strTraceAnulacionSAP);
					}

					//CONTADOR REGISTROS LEIDOS
					cont++;
					Common.WriteLog("Enviando informacion a SAP: RFC Zpvu_Rfc_Trs_Act_Est_Recaud...");
					recaudacion.Zpvu_Rfc_Trs_Act_Est_Recaud(EstadoTransaccion, strNroTransaccion, strPosicion, strTraceAnulacionSAP, ref logSapTable);
					Common.WriteLog("Envio de informacion a SAP: RFC Zpvu_Rfc_Trs_Act_Est_Recaud terminada");

					DataTable tbLogResult = logSapTable.ToADODataTable();
					int numeroResultadoOperacion = -1;
					foreach (DataRow item in tbLogResult.Rows)
					{
						numeroResultadoOperacion = item[0].Equals("E") ? EJECUCION_ERROR : EJECUCION_OK;
						mensajeOperacion = String.Format("{0}@,{1};{2}", numeroResultadoOperacion, numeroTransaccion, item[3]);
						descripcionTrs =item[3].ToString();
					}

//					string str = "";

					if (numeroResultadoOperacion == EJECUCION_OK)
					{
						contReg++;
						deserror="";
						string RegistraZAP = "S";
						string recaudacionID = Convert.ToString(filaPago["RECAUDACIONID"]);


						this.registrarAnulacionesPago(recaudacionID, strPosicion, RegistraZAP, mensajeOperacion);
						filaPago[1] = true;
						string numOpera = Convert.ToString(strNroTransaccion);


						if(this.LogMode ==TEST_MODE)
						{
							Common.WriteLog("{0}:{1}", "Inicio del Método Anulaciones.RegistrarTransaccionSAP.registrarResultadoOperacion", "Claro.SiscajasBatch.Data");
							Common.WriteLog("RecaudacionID:{0}",recaudacionID);
							Common.WriteLog("Posicion:{0}", strPosicion);
							Common.WriteLog("RegistraZAP:{0}", RegistraZAP);
						}
						Common.WriteLog("Respuesta desde SAP RFC Zpvu_Rfc_Trs_Act_Est_Recaud: {0}",mensajeOperacion);

					}
					else
					{
						string RegistraZAP = "E";
						string recaudacionID = Convert.ToString(filaPago["RECAUDACIONID"]);
						numeroTransaccion=strNroTransaccion;
						this.registrarAnulacionesPago(recaudacionID, strPosicion, RegistraZAP, mensajeOperacion);
						deserror="No se envio Correctamente la Transaccion";
						Common.WriteLog("ERROR: Respuesta desde SAP RFC Zpvu_Rfc_Trs_Act_Est_Recaud: {0}",mensajeOperacion);
					}
					recaudacion.Connection.Close();

					Common.WriteLog("------ FIN DE TRANSACCION DE ANULACIONES ------\n");
				}
				catch (Exception ex)
				{
					//this.LogTransaction.Error(String.Format("{0}:{1}", "Excepción en la Transacción Zpvu_Rfc_Trs_Act_Est_Recaud", "Numero Transacción: " + numeroTransaccion, "Claro.SiscajasBatch.Data.Anulaciones.RegistrarTransaccionSAP"), ex);
					deserror="";
					deserror=ex.ToString();
//					this.LogTransaction.Error(string.Format("************************ ERROR EN LA TRANSACCION DE ANULACIONES  ************************ Numero de Transaccion: {0}",numeroTransaccion) ,ex);
					Common.WriteLog(string.Format("************************ ERROR EN LA TRANSACCION DE ANULACIONES  ************************ Numero de Transaccion: {0}",numeroTransaccion) ,ex);
				}
				
				//REGISTRO EN LOG (T_LOG_TRANSACCION)
				descripcion= descripcionTrs;
				codigo=numeroTransaccion;
				//deserror=descripcionTrs;
				fecha = DateTime.Today.ToString("dd/MM/yyyy");
				usuario= Environment.UserDomainName.ToString();
				nombretabla="TI_EST_RECAUDACION";
				RegistrarLog(descripcion,codigo, deserror, fecha, usuario,nombretabla,archivo);
			}
			if(cont == 0)
			{
				Common.WriteLog("Ningun registro ha sido leido","");
			}

			int enviosConError = cont - contReg;
			if(enviosConError>0)
			{
				if (enviosConError == cont)
				{
					mensaje = "No se proceso ninguna Anulacion";
				}
				else
				{
					mensaje = string.Format("Se procesaron {0} de {1} Anulaciones",contReg,cont);
				}
			}
			else
			{
				mensaje ="Se procesaron correctamente las Anulaciones";
			}

			if(cont==0)
			{
				mensaje ="No se encontraron anulaciones para procesar";
			}

			Common.WriteLog(String.Format("Total de Registros Leidos: {0}", cont));
			Common.WriteLog(String.Format("Total de Registros Enviados: {0}", contReg));
			Common.WriteLog("---------------------- TRANSACCCION DE ANULACIONES DE PAGO FINALIZADA ----------------------\n");
			return (cont==contReg || cont == 0);

		}


		private DataTable listarPagosAnuladosNoMigrados(string fecha,string cajero,string oficina)
		{
			DataTable dtPagosAnulados;
			Common obj = new Common();
			dtPagosAnulados=obj.listarPagosAnuladosNoMigrados(fecha,cajero,oficina); 
			return dtPagosAnulados;
		}


		private void registrarAnulacionesPago(String TransaccionID, String Posicion, String RegZAP, String LogRegistro)
		{			
			Common obj = new Common();
			obj.registrarAnulacionesPago(TransaccionID, Posicion, RegZAP, LogRegistro);            
		}


		private void RegistrarLog(string descripcion,string codigo, string deserror, string fecha, string usuario,string nombretabla,long archivo)
		{
			Common obj = new Common();
			obj.RegistrarLog(descripcion,codigo, deserror, fecha, usuario,nombretabla,archivo);
		}


	}

}
