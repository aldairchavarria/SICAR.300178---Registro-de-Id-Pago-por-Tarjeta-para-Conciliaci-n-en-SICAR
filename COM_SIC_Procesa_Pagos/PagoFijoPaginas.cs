using SAP_SIC_Recaudacion;
using SAP.Connector;
using System;
using System.Data;
using System.IO;


namespace COM_SIC_Procesa_Pagos
{
    /// <summary>
    /// Summary description for PagoFijoPaginas.
    /// </summary>
    public class PagoFijoPaginas 
    {
         private int EJECUCION_OK = 0;
         private int EJECUCION_ERROR = 8;

        /// <summary>
        /// No modificar el valor, es una constante para la validacion del ambiente que se ejecuta la aplicacion es de desarrollo
        /// </summary>
        private readonly string TEST_MODE;

        /// <summary>
        /// El valor para mostrar la informacion de inputs en el log es TEST
        /// en el ambiente de produccion debe ser PROD
        /// </summary>
        private string LogMode;

        public PagoFijoPaginas()
        {
            LogMode = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["LogMode"]);
            TEST_MODE = "TEST";
        }


		public bool RegistrarTransaccionSAP(string fechaActual, string codigoCajero, string codigoTienda, ref string mensaje)
		{
			int registrosLeidos = 0;
			int registrosEnviados = 0;
			string descripcionTrs = "";
			string deserror = "";
			string logXML = string.Empty;
			string descripcion, codigo, fecha, usuario, nombretabla = "";
			Common.UsuarioCaja = mensaje;
			
			string idProceso = string.Empty;

			mensaje = "";

			Common.WriteLog("---------------------- INICIANDO TRANSACCCION DE PAGOS ----------------------");

			Common.WriteLog("-- Separando registros de la BD a migrar: PCK_SICAR_OFF_SAP.CUA_GET_TRANSACCIONES_RECAUDA");
			try
			{
				idProceso = separarRegistrosMigrarcion(codigoCajero,codigoTienda,fechaActual);
			}
			catch(Exception ex)
			{
				Common.WriteLog(ex.Message);
				mensaje = ex.Message;
				return false;
			}			
			idProceso = idProceso.Trim();
			Common.WriteLog("-- Separando registros de la BD a migrar terminada.");
			Common.WriteLog("-- ID_PROCESO : {0} --",idProceso);


			Common.WriteLog("Importando registros desde la BD: PCK_SICAR_OFF_SAP.CUA_GET_TRANSACCIONES_RECAUDA");
			DataSet dsRecaudaciones = null;
			try
			{
				dsRecaudaciones = cargarRecaudacionesNoMigradas(codigoCajero, codigoTienda, fechaActual,idProceso);
			}
			catch(Exception ex)
			{
				Common.WriteLog(ex.Message);
				mensaje = ex.Message;
				return false;
			}	
			dsRecaudaciones = cargarRecaudacionesNoMigradas(codigoCajero, codigoTienda, fechaActual,idProceso);
			Common.WriteLog("Importacion completada");


			// CREANDO LAS RELACIONES
			DataRelation deudaRelation = new DataRelation("deudaRelation",
				dsRecaudaciones.Tables[0].Columns["ID_T_TRS_REG_DEUDA"], dsRecaudaciones.Tables[1].Columns["ID_T_TRS_REG_DEUDA"]);
			DataRelation pedidoRelation = new DataRelation("reciboRelation",
				dsRecaudaciones.Tables[0].Columns["ID_T_TRS_REG_DEUDA"], dsRecaudaciones.Tables[2].Columns["ID_T_TRS_REG_DEUDA"]);
			DataRelation pagosRelation = new DataRelation("pagosRelation",
				dsRecaudaciones.Tables[0].Columns["ID_T_TRS_REG_DEUDA"], dsRecaudaciones.Tables[3].Columns["ID_T_TRS_REG_DEUDA"]);
			// FIN CREANDO LAS RELACIONES

			dsRecaudaciones.Relations.AddRange(new DataRelation[] { deudaRelation, pedidoRelation, pagosRelation });


			foreach (System.Data.DataRow filaTrsRegDeuda in dsRecaudaciones.Tables[0].Rows)
			{
				string mensajeOperacion = "";
				ZST_DEUDA_RECAUDACIONTable deudaSapTable = new ZST_DEUDA_RECAUDACIONTable();
				ZST_RECIBO_RECAUDACIONTable recibosSapTable = new ZST_RECIBO_RECAUDACIONTable();
				ZST_PAGOS_RECAUDACIONTable pagosSapTable = new ZST_PAGOS_RECAUDACIONTable();
				BAPIRET2Table logSapTable = new BAPIRET2Table();


				// ARMAND0 ESTRUCTURA ZST_DEUDA_RECAUDACION
				System.Data.DataRow[] filasTableDeuda = filaTrsRegDeuda.GetChildRows("deudaRelation");

				if (filasTableDeuda.Length > 0)
				{
					System.Data.DataRow filaTableDeuda = filasTableDeuda[0];
					ZST_DEUDA_RECAUDACION filaDeudaSapTable = new ZST_DEUDA_RECAUDACION();

					filaDeudaSapTable.Nro_Transaccion = filaTableDeuda[0].ToString();
					filaDeudaSapTable.Nom_Deudor = filaTableDeuda[1].ToString();
					filaDeudaSapTable.Ruc_Deudor = filaTableDeuda[2].ToString();///SOSPECHOSO
					filaDeudaSapTable.Oficina_Venta = filaTableDeuda[3].ToString();
					filaDeudaSapTable.Nom_Of_Venta = filaTableDeuda[4].ToString();
					filaDeudaSapTable.Mon_Pago = filaTableDeuda[5].ToString();
					filaDeudaSapTable.Importe_Pago = filaTableDeuda[6].ToString() != null ? Convert.ToDecimal(filaTableDeuda[6].ToString()) : 0;
					filaDeudaSapTable.Fecha_Transac = filaTableDeuda[7].ToString();
					filaDeudaSapTable.Hora_Transac = filaTableDeuda[8].ToString();
					filaDeudaSapTable.Estado_Transac = filaTableDeuda[9].ToString();
					filaDeudaSapTable.Nro_Telefono = filaTableDeuda[10].ToString();
					filaDeudaSapTable.Cod_Cajero = filaTableDeuda[11].ToString();
					filaDeudaSapTable.Nom_Cajero = filaTableDeuda[12].ToString();
					filaDeudaSapTable.Nro_Trace_Cons = filaTableDeuda[13].ToString();
					filaDeudaSapTable.Tipo_Doc_Deudor = filaTableDeuda[14].ToString();
					filaDeudaSapTable.Nro_Doc_Deudor = filaTableDeuda[15].ToString();
					deudaSapTable.Add(filaDeudaSapTable);
				}

				// ARMAND0 ESTRUCTURA ZST_RECIBO_RECAUDACION
				foreach (System.Data.DataRow filaTablaRecibos in filaTrsRegDeuda.GetChildRows("reciboRelation"))
				{
					ZST_RECIBO_RECAUDACION filaReciboSapTable = new ZST_RECIBO_RECAUDACION();

					filaReciboSapTable.Nro_Transaccion = filaTablaRecibos[0].ToString();
					filaReciboSapTable.Posicion = filaTablaRecibos[1].ToString();
					filaReciboSapTable.Tipo_Doc_Recaud = filaTablaRecibos[2].ToString();
					filaReciboSapTable.Nro_Doc_Recaud = filaTablaRecibos[3].ToString();
					filaReciboSapTable.Moneda_Docum = filaTablaRecibos[4].ToString();
					filaReciboSapTable.Importe_Recibo = filaTablaRecibos[5].ToString() != null ? Convert.ToDecimal(filaTablaRecibos[5].ToString()) : 0;
					filaReciboSapTable.Importe_Pagado = filaTablaRecibos[6].ToString() != null ? Convert.ToDecimal(filaTablaRecibos[6].ToString()) : 0;
					filaReciboSapTable.Nro_Cobranza = filaTablaRecibos[7].ToString();
					filaReciboSapTable.Nro_Ope_Acree = filaTablaRecibos[8].ToString();
					filaReciboSapTable.Fecha_Emision = filaTablaRecibos[9].ToString();
					filaReciboSapTable.Fecha_Pago = filaTablaRecibos[10].ToString();
					filaReciboSapTable.Nro_Trace_Anul = filaTablaRecibos[11].ToString();
					filaReciboSapTable.Nro_Trace_Pago = filaTablaRecibos[12].ToString();
					filaReciboSapTable.Desc_Servicio = filaTablaRecibos[13].ToString();
					filaReciboSapTable.Fecha_Hora = filaTablaRecibos[14].ToString();
					filaReciboSapTable.Servicio = filaTablaRecibos[15].ToString();



					recibosSapTable.Add(filaReciboSapTable);
				}

				// ARMAND0 ESTRUCTURA ZST_PAGOS_RECAUDACION
				foreach (System.Data.DataRow filaTablaPagos in filaTrsRegDeuda.GetChildRows("pagosRelation"))
				{
					ZST_PAGOS_RECAUDACION filaPagosSapTable = new ZST_PAGOS_RECAUDACION();

					filaPagosSapTable.Nro_Transaccion = filaTablaPagos[0].ToString();
					filaPagosSapTable.Posicion = filaTablaPagos[1].ToString();
					filaPagosSapTable.Via_Pago = filaTablaPagos[2].ToString();

					if (filaTablaPagos[3].ToString() != null)
					{ filaPagosSapTable.Importe_Pagado = Convert.ToDecimal(filaTablaPagos[3].ToString()); }
					else { filaPagosSapTable.Importe_Pagado = 0; }

					filaPagosSapTable.Nro_Cheque = filaTablaPagos[4].ToString();
					filaPagosSapTable.Belnr = filaTablaPagos[5].ToString();
					filaPagosSapTable.Desc_Via_Pago = filaTablaPagos[6].ToString();


					pagosSapTable.Add(filaPagosSapTable);
				}

				registrosLeidos++;

				SAP_SIC_Recaudacion.SAP_SIC_Recaudacion recaudacion = new SAP_SIC_Recaudacion.SAP_SIC_Recaudacion(SAPDataConnector.ConnectionString);
				string numeroTransaccion = "";

				try
				{
					Common.WriteLog("------ ENVIANDO TRANSACCION DE PAGOS ------");
					Common.WriteLog("CODIGO DE REGISTRO [Nro_Transaccion] : {0}; [Monto] : {1}", filaTrsRegDeuda["Nro_Transaccion"], deudaSapTable[0].Importe_Pago);
					Common.WriteLog("Enviando informacion a SAP: RFC Zpvu_Rfc_Trs_Reg_Deuda...");
					recaudacion.Zpvu_Rfc_Trs_Reg_Deuda(out numeroTransaccion, ref deudaSapTable, ref logSapTable, ref pagosSapTable, ref recibosSapTable);
					Common.WriteLog("Envio de informacion a SAP: RFC Zpvu_Rfc_Trs_Reg_Deuda terminada");

					System.Data.DataTable tbLogResult = logSapTable.ToADODataTable();
					int numeroResultadoOperacion = -1;
					foreach (System.Data.DataRow item in tbLogResult.Rows)
					{
						numeroResultadoOperacion = item[0].Equals("E") ? EJECUCION_ERROR : EJECUCION_OK;
						mensajeOperacion = string.Format("{0}@,{1};{2}", numeroResultadoOperacion, numeroTransaccion, item[3]);
						descripcionTrs = item[3].ToString();
					}
					if (numeroResultadoOperacion == EJECUCION_OK)
					{
						registrosEnviados++;
						string RegistraZAP = "S";
						deserror = "";
						this.registrarResultadoOperacion(Convert.ToInt32(filaTrsRegDeuda["TransaccionID"]), RegistraZAP, mensajeOperacion, numeroTransaccion);
						filaTrsRegDeuda[1] = true;
						if (this.LogMode == TEST_MODE)
						{
							Common.WriteLog("Inicio del Método PagoFijoPaginas.RegistrarTransaccionSAP.registrarResultadoOperacion:Claro.SiscajasBatch.Data");
							Common.WriteLog("Nro Transaccion: {0}", numeroTransaccion);
							Common.WriteLog("RegistraSAP: {0}", RegistraZAP);
						}
						Common.WriteLog("Respuesta desde SAP RFC Zpvu_Rfc_Trs_Reg_Deuda: {0}", mensajeOperacion);
					}
					else
					{
						string RegistraZAP = "E";
						numeroTransaccion = String.Empty;
						//numeroTransaccion = Convert.ToString(filaTrsRegDeuda["NRO_TRANSACCION"]).ToString();

						Common.WriteLog("ERROR Respuesta desde SAP RFC Zpvu_Rfc_Trs_Reg_Deuda: {0}", mensajeOperacion);
						deserror = "No se envio Correctamente la Transaccion";
						this.registrarResultadoOperacion(Convert.ToInt32(filaTrsRegDeuda["TransaccionID"]), RegistraZAP, mensajeOperacion, numeroTransaccion);
					}
					recaudacion.Connection.Close();

					// ARMADO LA ESTRUCTURA PARA LOG XML

					DataSet dsLogXML = new DataSet("Recaudacion");
					DataTable tbDeuda = deudaSapTable.ToADODataTable();
					DataTable tbRecibos = recibosSapTable.ToADODataTable();
					DataTable tbPagos = pagosSapTable.ToADODataTable();
					DataTable tbLogRespuesta = logSapTable.ToADODataTable();

					tbDeuda.TableName = "DEUDA";
					tbRecibos.TableName = "RECIBOS";
					tbPagos.TableName = "PAGOS";
					tbLogRespuesta.TableName = "BAPIRET2";

					dsLogXML.Tables.AddRange(new DataTable[] { tbDeuda, tbRecibos, tbPagos, tbLogRespuesta });
					TextWriter tw = new StringWriter();
					dsLogXML.WriteXml(tw);
					string xml = tw.ToString().Replace("\r\n", "");
					logXML = xml;

					// FIN ARMADO ESTRUCTURA

					Common.WriteLog("------ FIN TRANSACCION DE PAGOS  ------\n");
				}
				catch (SAP.Connector.RfcCommunicationException ex)
				{
				   //INI PROY-140126
					string MaptPath = "";
				    MaptPath = "( Class : COM_SIC_Procesa_Pagos PagoFijoPaginas.cs; Function: RegistrarTransaccionSAP)";
					Common.WriteLog("ERROR DE COMUNICACION CON SAP {0}", ex.Message + MaptPath); 
				   //FIN PROY-140126

					
					descripcion = ex.Message;
					codigo = ex.ErrorCode;
					fecha = DateTime.Today.ToString("dd/MM/yyyy");
					usuario = Environment.UserDomainName.ToString();
					nombretabla = "TI_EST_RECAUDACION";
					RegistrarLog(descripcion, codigo, deserror, fecha, usuario, nombretabla, 0);
					this.registrarResultadoOperacion(Convert.ToInt32(filaTrsRegDeuda["TransaccionID"]), "N", mensajeOperacion, numeroTransaccion);
				}
				catch (Exception ex)
				{
					deserror = "";
					deserror = ex.ToString();
					Common.WriteLog("************************ ERROR EN LA TRANSACCION DE PAGOS ************************\n");
					Common.WriteLog("{0}:{1}", "Excepción en la Transacción Zpvu_Rfc_Trs_Reg_Deuda", "Numero Transacción: " + numeroTransaccion, "Claro.SiscajasBatch.Data.PagoFijoPaginas.RegistrarTransaccionSAP");
					this.registrarResultadoOperacion(Convert.ToInt32(filaTrsRegDeuda["TransaccionID"]), "E", mensajeOperacion, numeroTransaccion);
				}
				descripcion = descripcionTrs;
				codigo = numeroTransaccion;
				fecha = DateTime.Today.ToString("dd/MM/yyyy");
				usuario = Environment.UserDomainName.ToString();
				nombretabla = "T_TRS_REG_DEUDA";
				RegistrarLog(descripcion, codigo, deserror, fecha, usuario, nombretabla, 0);
			}
			if (registrosLeidos == 0)
			{
				Common.WriteLog("Ningun registro ha sido leido");
			}

			int enviosConError = registrosLeidos - registrosEnviados;
			if (enviosConError > 0)
			{
				if (enviosConError == registrosLeidos)
				{
					mensaje = "No se proceso ninguna recaudaciones de fija y movil";
				}
				else
				{
					mensaje = string.Format("Se procesaron {0} de {1} Recaudaciones", registrosEnviados, registrosLeidos);
				}
			}
			else
			{
				mensaje = "Se procesaron correctamente las Recaudaciones";
			}

			if (registrosLeidos == 0)
			{
				mensaje = "No se encontraron recaudaciones para procesar";
			}

			Common.WriteLog("Registros leidos: {0}", registrosLeidos);
			Common.WriteLog("Registros enviados: {0}", registrosEnviados);
			Common.WriteLog("Registros no enviados: {0}", registrosLeidos - registrosEnviados);
			Common.WriteLog("---------------------- TRANSACCCION DE PAGOS FINALIZADA ----------------------\n");
			return (registrosLeidos == registrosEnviados || registrosLeidos == 0);
		}


		public void RegistrarTransaccionSAP(string fechaActual)
        {
            int cont = 0;
            int contReg = 0;
            string descripcionTrs = "";
            string deserror = "";
            long archivo = 0;
            string descripcion, codigo, fecha, usuario, nombretabla = "";


            Common.WriteLog("---------------------- INICIANDO TRANSACCCION DE PAGOS ----------------------");

            Common.WriteLog("Importando registros desde la BD: PCK_SICAR_OFF_SAP.MIG_ListarPagosFijoPagina...");

            //			Common.WriteLog("Parametro fechaActual: {0}",fechaActual);

            System.Data.DataTable tbNoMigradas = listarDeudasNoMigradas(fechaActual, "", "");
            //			Common.WriteLog("Numero de deudas a migrar: {0}",tbNoMigradas.Rows.Count);
            Common.WriteLog("Importacion completada");
            tbNoMigradas.Columns.Add("MigracionOK", typeof(bool));



            foreach (System.Data.DataRow filaPago in tbNoMigradas.Rows)
            {
                string mensajeOperacion = "";
                ZST_DEUDA_RECAUDACIONTable deudaSapTable = new ZST_DEUDA_RECAUDACIONTable();
                ZST_RECIBO_RECAUDACIONTable recibosSapTable = new ZST_RECIBO_RECAUDACIONTable();
                ZST_PAGOS_RECAUDACIONTable pagosSapTable = new ZST_PAGOS_RECAUDACIONTable();
                BAPIRET2Table logSapTable = new BAPIRET2Table();
                System.Data.DataSet deudasNoMigradas = listarDeudaReciboPago(Convert.ToInt32(filaPago["TransaccionID"]), "", "");
                System.Data.DataTable deudasTable = deudasNoMigradas.Tables[0];

                if (deudasTable.Rows.Count > 0)
                {
                    System.Data.DataRow filaTableDeuda = deudasTable.Rows[0];

                    ZST_DEUDA_RECAUDACION filaDeudaSapTable = new ZST_DEUDA_RECAUDACION();

                    filaDeudaSapTable.Nro_Transaccion = filaTableDeuda[0].ToString();
                    filaDeudaSapTable.Nom_Deudor = filaTableDeuda[1].ToString();
                    filaDeudaSapTable.Ruc_Deudor = filaTableDeuda[2].ToString();///SOSPECHOSO
                    filaDeudaSapTable.Oficina_Venta = filaTableDeuda[3].ToString();
                    filaDeudaSapTable.Nom_Of_Venta = filaTableDeuda[4].ToString();
                    filaDeudaSapTable.Mon_Pago = filaTableDeuda[5].ToString();
                    if (filaTableDeuda[6].ToString() != null)
                    { filaDeudaSapTable.Importe_Pago = Convert.ToDecimal(filaTableDeuda[6].ToString()); }
                    else { filaDeudaSapTable.Importe_Pago = 0; }
                    //filaDeudaSapTable.Importe_Pago = (filaTableDeuda[6]);
                    filaDeudaSapTable.Fecha_Transac = filaTableDeuda[7].ToString();
                    filaDeudaSapTable.Hora_Transac = filaTableDeuda[8].ToString();
                    filaDeudaSapTable.Estado_Transac = filaTableDeuda[9].ToString();
                    filaDeudaSapTable.Nro_Telefono = filaTableDeuda[10].ToString();
                    filaDeudaSapTable.Cod_Cajero = filaTableDeuda[11].ToString();
                    filaDeudaSapTable.Nom_Cajero = filaTableDeuda[12].ToString();
                    filaDeudaSapTable.Nro_Trace_Cons = filaTableDeuda[13].ToString();
                    filaDeudaSapTable.Tipo_Doc_Deudor = filaTableDeuda[14].ToString();
                    filaDeudaSapTable.Nro_Doc_Deudor = filaTableDeuda[15].ToString();

                    //filaDeudaSapTable.BindDeudaFromDataRow(filaTableDeuda);
                    deudaSapTable.Add(filaDeudaSapTable);
                }

                System.Data.DataTable recibosTable = deudasNoMigradas.Tables[1];
                foreach (System.Data.DataRow filaTablaRecibos in recibosTable.Rows)
                {
                    ZST_RECIBO_RECAUDACION filaReciboSapTable = new ZST_RECIBO_RECAUDACION();

                    filaReciboSapTable.Nro_Transaccion = filaTablaRecibos[0].ToString();
                    filaReciboSapTable.Posicion = filaTablaRecibos[1].ToString();
                    filaReciboSapTable.Tipo_Doc_Recaud = filaTablaRecibos[2].ToString();
                    filaReciboSapTable.Nro_Doc_Recaud = filaTablaRecibos[3].ToString();
                    filaReciboSapTable.Moneda_Docum = filaTablaRecibos[4].ToString();
                    //filaReciboSapTable.Importe_Recibo = Convert.ToDecimal(filaTablaRecibos[5]);
                    if (filaTablaRecibos[5].ToString() != null)
                    { filaReciboSapTable.Importe_Recibo = Convert.ToDecimal(filaTablaRecibos[5].ToString()); }
                    else { filaReciboSapTable.Importe_Recibo = 0; }

                    if (filaTablaRecibos[6].ToString() != null)
                    { filaReciboSapTable.Importe_Pagado = Convert.ToDecimal(filaTablaRecibos[6].ToString()); }
                    else { filaReciboSapTable.Importe_Pagado = 0; }//
                    //					filaReciboSapTable.Importe_Pagado = Convert.ToDecimal(filaTablaRecibos[6]);
                    //
                    //					if (filaTableDeuda[6].ToString()!=null)
                    //					{filaDeudaSapTable.Importe_Pago = Convert.ToDecimal(filaTableDeuda[6].ToString());}
                    //					else{filaDeudaSapTable.Importe_Pago =0;}

                    filaReciboSapTable.Nro_Cobranza = filaTablaRecibos[7].ToString();
                    filaReciboSapTable.Nro_Ope_Acree = filaTablaRecibos[8].ToString();
                    filaReciboSapTable.Fecha_Emision = filaTablaRecibos[9].ToString();
                    filaReciboSapTable.Fecha_Pago = filaTablaRecibos[10].ToString();
                    filaReciboSapTable.Nro_Trace_Anul = filaTablaRecibos[11].ToString();
                    filaReciboSapTable.Nro_Trace_Pago = filaTablaRecibos[12].ToString();
                    filaReciboSapTable.Desc_Servicio = filaTablaRecibos[13].ToString();
                    filaReciboSapTable.Fecha_Hora = filaTablaRecibos[14].ToString();
                    filaReciboSapTable.Servicio = filaTablaRecibos[15].ToString();

                    //filaReciboSapTable.BindReciboFromDataRow(filaTablaRecibos);

                    recibosSapTable.Add(filaReciboSapTable);
                }

                System.Data.DataTable pagosTable = deudasNoMigradas.Tables[2];
                foreach (System.Data.DataRow filaTablaPagos in pagosTable.Rows)
                {
                    ZST_PAGOS_RECAUDACION filaPagosSapTable = new ZST_PAGOS_RECAUDACION();

                    filaPagosSapTable.Nro_Transaccion = filaTablaPagos[0].ToString();
                    filaPagosSapTable.Posicion = filaTablaPagos[1].ToString();///NUMERICO
                    filaPagosSapTable.Via_Pago = filaTablaPagos[2].ToString();
                    //'filaPagosSapTable.Importe_Pagado = Convert.ToDecimal(filaTablaPagos[3]);
                    if (filaTablaPagos[3].ToString() != null)
                    { filaPagosSapTable.Importe_Pagado = Convert.ToDecimal(filaTablaPagos[3].ToString()); }
                    else { filaPagosSapTable.Importe_Pagado = 0; }

                    filaPagosSapTable.Nro_Cheque = filaTablaPagos[4].ToString();
                    filaPagosSapTable.Belnr = filaTablaPagos[5].ToString();
                    filaPagosSapTable.Desc_Via_Pago = filaTablaPagos[6].ToString();

                    //filaPagosSapTable.BindPagoFromDataRow(filaTablaPagos);
                    pagosSapTable.Add(filaPagosSapTable);
                }
                //CONTADOR REGISTROS LEIDOS
                cont++;

                SAP_SIC_Recaudacion.SAP_SIC_Recaudacion recaudacion = new SAP_SIC_Recaudacion.SAP_SIC_Recaudacion(SAPDataConnector.ConnectionString);
                string numeroTransaccion = "";

                try
                {
                    Common.WriteLog("------ ENVIANDO TRANSACCION DE PAGOS ------");
                    Common.WriteLog("CODIGO DE REGISTRO (Nro_Transaccion) : {0}", filaPago["Nro_Transaccion"]);

                    ///LLAMADO AL RFC CON LOS DATOS
                    Common.WriteLog("Enviando informacion a SAP: RFC Zpvu_Rfc_Trs_Reg_Deuda...");
                    recaudacion.Zpvu_Rfc_Trs_Reg_Deuda(out numeroTransaccion, ref deudaSapTable, ref logSapTable, ref pagosSapTable, ref recibosSapTable);
                    Common.WriteLog("Envio de informacion a SAP: RFC Zpvu_Rfc_Trs_Reg_Deuda terminada");

                    System.Data.DataTable tbLogResult = logSapTable.ToADODataTable();
                    int numeroResultadoOperacion = -1;
                    foreach (System.Data.DataRow item in tbLogResult.Rows)
                    {
                        numeroResultadoOperacion = item[0].Equals("E") ? EJECUCION_ERROR : EJECUCION_OK;
                        mensajeOperacion = string.Format("{0}@,{1};{2}", numeroResultadoOperacion, numeroTransaccion, item[3]);
                        descripcionTrs = item[3].ToString();
                    }

                    //					string str = "";					

                    if (numeroResultadoOperacion == EJECUCION_OK)
                    {
                        contReg++;
                        string RegistraZAP = "S";
                        deserror = "";

                        this.registrarResultadoOperacion(Convert.ToInt32(filaPago["TransaccionID"]), RegistraZAP, mensajeOperacion, numeroTransaccion);
                        filaPago[1] = true;

                        //						this.LogTransaction.InfoFormat("{0}:{1}", "Inicio del Método PagoFijoPaginas.RegistrarTransaccionSAP.registrarResultadoOperacion", "Claro.SiscajasBatch.Data");
                        //						this.LogTransaction.InfoFormat("Nro Transaccion: {0}", numeroTransaccion);
                        //						this.LogTransaction.InfoFormat("RegistraSAP: {0}", RegistraZAP);
                        //						this.LogTransaction.InfoFormat("Log Registro: {0}", mensajeOperacion);
                        if (this.LogMode == TEST_MODE)
                        {
                            Common.WriteLog("Inicio del Método PagoFijoPaginas.RegistrarTransaccionSAP.registrarResultadoOperacion:Claro.SiscajasBatch.Data");
                            Common.WriteLog("Nro Transaccion: {0}", numeroTransaccion);
                            Common.WriteLog("RegistraSAP: {0}", RegistraZAP);
                        }
                        Common.WriteLog("Respuesta desde SAP RFC Zpvu_Rfc_Trs_Reg_Deuda: {0}", mensajeOperacion);

                        //						str = string.Format("{0}:{1}", "Se realizó con exito la Transacción Zpvu_Rfc_Trs_Reg_Deuda", mensajeOperacion);

                        //						this.LogTransaction.Info(string.Format("Resultado: {0}", str));
                        //						this.LogTransaction.Info("Mensaje: SE ENVIO CORRECTAMENTE LA TRANSACCION");
                        //
                        //						Common.WriteLog(string.Format("Resultado: {0}", str));
                        //						Common.WriteLog("Mensaje: SE ENVIO CORRECTAMENTE LA TRANSACCION");
                    }
                    else
                    {
                        string RegistraZAP = "E";
                        numeroTransaccion = Convert.ToString(filaPago["NRO_TRANSACCION"]).ToString();
                        //						this.LogTransaction.Info(String.Format("NUMERO TRANSACCION: {0}",numeroTransaccion));
                        Common.WriteLog("ERROR Respuesta desde SAP RFC Zpvu_Rfc_Trs_Reg_Deuda: {0}", mensajeOperacion);
                        deserror = "No se envio Correctamente la Transaccion";
                        //						str = string.Format("{0}:{1}", "No Se realizó con exito la Transacción Zpvu_Rfc_Trs_Reg_Deuda", mensajeOperacion);

                        //						this.LogTransaction.Info(string.Format("Resultado: {0}", str));
                        //						this.LogTransaction.Info("Mensaje: NO SE ENVIO CORRECTAMENTE LA TRANSACCION");

                        //						Common.WriteLog("Resultado: {0}", str);
                        //						Common.WriteLog("Mensaje: NO SE ENVIO CORRECTAMENTE LA TRANSACCION");

                        this.registrarResultadoOperacion(Convert.ToInt32(filaPago["TransaccionID"]), RegistraZAP, mensajeOperacion, numeroTransaccion);
                    }
                    recaudacion.Connection.Close();

                    //					this.LogTransaction.Info("********************* FIN TRANSACCION DE PAGOS **********************\n");
                    Common.WriteLog("------ FIN TRANSACCION DE PAGOS ------\n");
                }
                catch (Exception ex)
                {
                    deserror = "";
                    deserror = ex.ToString();
                    //					this.LogTransaction.Error("************************ ERROR EN LA TRANSACCION DE PAGOS ************************\n",ex);
                    //					this.LogTransaction.Error(string.Format("{0}:{1}", "Excepción en la Transacción Zpvu_Rfc_Trs_Reg_Deuda", "Numero Transacción: " + numeroTransaccion, "Claro.SiscajasBatch.Data.PagoFijoPaginas.RegistrarTransaccionSAP"), ex);

                    Common.WriteLog("************************ ERROR EN LA TRANSACCION DE PAGOS ************************\n");
                    Common.WriteLog("{0}:{1}", "Excepción en la Transacción Zpvu_Rfc_Trs_Reg_Deuda", "Numero Transacción: " + numeroTransaccion, "Claro.SiscajasBatch.Data.PagoFijoPaginas.RegistrarTransaccionSAP");
                }
                descripcion = descripcionTrs;
                codigo = numeroTransaccion;
                //deserror=descripcionTrs;
                fecha = DateTime.Today.ToString("dd/MM/yyyy");
                usuario = Environment.UserDomainName.ToString();
                nombretabla = "T_TRS_REG_DEUDA";


                RegistrarLog(descripcion, codigo, deserror, fecha, usuario, nombretabla, 0);

                //this.LogTransaction.InfoFormat("\n");
            }
            if (cont == 0)
            {
                Common.WriteLog("Ningun registro ha sido leido");//PROY-140126
            }


            //			this.LogTransaction.Info(String.Format("Total de Registros Leidos: {0}", cont));
            //			this.LogTransaction.Info(String.Format("Total de Registros Enviados: {0}", contReg));
            //			this.LogTransaction.Info("*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨* TRANSACCCION DE PAGOS FINALIZADA ¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨\n");

            Common.WriteLog("Registros leidos: {0}", cont);
            Common.WriteLog("Registros enviados: {0}", contReg);
            Common.WriteLog("Registros no enviados: {0}", cont - contReg);
            Common.WriteLog("---------------------- TRANSACCCION DE PAGOS FINALIZADA ----------------------");

        }

        private System.Data.DataTable listarDeudasNoMigradas(string fecha, string codCajero, string codOficina)
        {
            System.Data.DataTable dt;
            Common obj = new Common();
            dt = obj.listarDeudasNoMigradas(fecha, codCajero, codOficina);
            return dt;
        }

        private System.Data.DataSet listarDeudaReciboPago(int codigoTransaccion, string codigoCajero, string codigoTienda)
        {
            System.Data.DataSet dsdeudas;
            Common obj = new Common();
            dsdeudas = obj.listarDeudaReciboPago(codigoTransaccion, codigoCajero, codigoTienda);
            return dsdeudas;
        }

        private void registrarResultadoOperacion(System.Int32 TransaccionID, System.String transaccionEjecutada, System.String resultadoOperacion, string nro_Operacion)
        {
            Common obj = new Common();
            obj.registrarResultadoOperacion(TransaccionID, transaccionEjecutada, resultadoOperacion, nro_Operacion);
        }

        
//		private void RegistrarLog(string descripcion, string codigo, string deserror, string fecha, string usuario, string nombretabla, string archivo)
//        {
//            Common obj = new Common();
//            obj.RegistrarLog(descripcion, codigo, deserror, fecha, usuario, nombretabla, archivo);
//        }

		private void RegistrarLog(string descripcion,string codigo, string deserror, string fecha, string usuario,string nombretabla,long archivo)
		{
			Common obj = new Common();
			obj.RegistrarLog(descripcion,codigo, deserror, fecha, usuario,nombretabla,archivo);
		}

        private DataSet cargarRecaudacionesNoMigradas(string codigoCajero, string codigoTienda, string fechaActual, string idProceso)
        {
            System.Data.DataSet ds;
            Common obj = new Common();
            ds = obj.cargarRecaudaciones(codigoCajero, codigoTienda, fechaActual,idProceso);
            return ds;
        }


		private string separarRegistrosMigrarcion(string vendedor, string codOficina, string fechaCuadre)
		{
			Common obj = new Common();
			return obj.separarRegistrosMigrarcionFP(vendedor,codOficina,fechaCuadre);
		}
    }


}
