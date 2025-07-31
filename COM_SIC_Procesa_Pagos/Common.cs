
using System.Configuration;
using Claro.Datos.DAAB;
using Claro.Datos;
using System;
using System.Data;
using  COM_SIC_Log4Net; //PROY-140126
namespace COM_SIC_Procesa_Pagos
{
    /// <summary>
    /// Summary description for Common.
    /// </summary>
    ///

    public class Common
    {

         Seguridad_NET.clsSeguridad objSeg = new Seguridad_NET.clsSeguridad();
         public string pkgNameOffSAP = "PCK_SICAR_OFF_SAP";

        public Common()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public System.Data.DataTable listarDeudasNoMigradas(string fecha, string codCajero, string codOficina)
        {
            System.Data.DataSet ds;
            System.Data.DataTable dtdeudasNoMigradas;
            String strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA");  //"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas";			
            Claro.Datos.DAAB.DAABRequest objRequest = new Claro.Datos.DAAB.DAABRequest(Claro.Datos.DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion);
            Claro.Datos.DAAB.DAABRequest.Parameter[] arrParam = {new Claro.Datos.DAAB.DAABRequest.Parameter("P_FECHA",DbType.String,10,fecha, ParameterDirection.Input),
																 new Claro.Datos.DAAB.DAABRequest.Parameter("P_CAJERO",DbType.String,10,codCajero, ParameterDirection.Input),
																 new Claro.Datos.DAAB.DAABRequest.Parameter("P_OFICINA",DbType.String,4,codOficina, ParameterDirection.Input),
																 new Claro.Datos.DAAB.DAABRequest.Parameter("C_PAGOS",DbType.Object, ParameterDirection.Output)};

            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = pkgNameOffSAP + ".MIG_ListarPagosFijoPagina";
            objRequest.Parameters.AddRange(arrParam);
            //Get_ConsultaVend = objRequest.Factory.ExecuteDataset(ref objRequest);
            ds = objRequest.Factory.ExecuteDataset(ref objRequest);
            dtdeudasNoMigradas = ds.Tables[0];
            return dtdeudasNoMigradas;
        }

        public System.Data.DataSet listarDeudaReciboPago(int codigoTransaccion, string codigoCajero, string codigoTienda)
        {
            String strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA"); //"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas";			
            Claro.Datos.DAAB.DAABRequest objRequest = new Claro.Datos.DAAB.DAABRequest(Claro.Datos.DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion);
            Claro.Datos.DAAB.DAABRequest.Parameter[] arrParam = {
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_TRANSACCIONID",DbType.Int32,10, codigoTransaccion, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_Cajero",DbType.AnsiString,10, codigoCajero, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_Oficina",DbType.AnsiString,4, codigoTienda, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("C_DEUDA",DbType.Object, ParameterDirection.Output),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("C_RECIBO",DbType.Object, ParameterDirection.Output),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("C_PAGO",DbType.Object, ParameterDirection.Output)};

            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = pkgNameOffSAP + ".MIG_ListarPagoDocumentosFP";
            objRequest.Parameters.AddRange(arrParam);
            //Get_ConsultaVend = objRequest.Factory.ExecuteDataset(ref objRequest);
            return objRequest.Factory.ExecuteDataset(ref objRequest);
        }

        public Int32 registrarResultadoOperacion(Int32 TransaccionID, String transaccionEjecutada, System.String resultadoOperacion, string NroTransaccion)
        {
            String strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA"); //"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas";
            Claro.Datos.DAAB.DAABRequest objRequest = new Claro.Datos.DAAB.DAABRequest(Claro.Datos.DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion);
            Claro.Datos.DAAB.DAABRequest.Parameter[] arrParam = {new Claro.Datos.DAAB.DAABRequest.Parameter("P_TRANSACCIONID",DbType.Int32 ,10, TransaccionID, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_REGISTRASAP",DbType.String,4, transaccionEjecutada, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_LOGREGISTRO",DbType.String,250, resultadoOperacion, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_NUMEROTR_SAP",DbType.String,20, NroTransaccion, ParameterDirection.Input)};

            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = pkgNameOffSAP + ".MIG_RegistrarResultadoFP";
            objRequest.Parameters.AddRange(arrParam);
            return objRequest.Factory.ExecuteNonQuery(ref objRequest);
        }

        public System.Data.DataSet listarCajaDiarioNoMigradas(string fecha, string oficinaVenta)
        {
            String strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA"); //"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas";			
            Claro.Datos.DAAB.DAABRequest objRequest = new Claro.Datos.DAAB.DAABRequest(Claro.Datos.DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion);
            Claro.Datos.DAAB.DAABRequest.Parameter[] arrParam = {new Claro.Datos.DAAB.DAABRequest.Parameter("P_FECHA_CAJA",DbType.String ,10, fecha, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_OFICINA",DbType.String ,4, oficinaVenta, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("CURCAJADIARIO",DbType.Object, ParameterDirection.Output)};

            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = pkgNameOffSAP + ".MIG_TRS_CAJADIARIO";
            objRequest.Parameters.AddRange(arrParam);
            return objRequest.Factory.ExecuteDataset(ref objRequest);
        }

        public Int32 registrarResultadoCajas(Int32 IDCajaDiario, String RegistraZAP, String mensajeOperacion)
        {
            String strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA"); //"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas";
            Claro.Datos.DAAB.DAABRequest objRequest = new Claro.Datos.DAAB.DAABRequest(Claro.Datos.DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion);
            Claro.Datos.DAAB.DAABRequest.Parameter[] arrParam = {new Claro.Datos.DAAB.DAABRequest.Parameter("P_ID_T_TI_CAJA_DIARIO",DbType.Int32 ,38, IDCajaDiario,																			ParameterDirection.Input),
																new Claro.Datos.DAAB.DAABRequest.Parameter("P_REGISTRA_SAP",DbType.String,2, RegistraZAP, ParameterDirection.																Input),
																new Claro.Datos.DAAB.DAABRequest.Parameter("P_LOG_REGISTRO",DbType.String,250, mensajeOperacion,																			ParameterDirection.Input)};

            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = pkgNameOffSAP + ".MIG_REGISTRA_CAJA_DIARIO";
            objRequest.Parameters.AddRange(arrParam);
            return objRequest.Factory.ExecuteNonQuery(ref objRequest);
        }


        public void RegistrarLog(string descripcion, string codigo, string deserror, string fecha, string usuario, string nombretabla, string archivo)
        {

            String strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA"); //"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas";
            Claro.Datos.DAAB.DAABRequest objRequest = new Claro.Datos.DAAB.DAABRequest(Claro.Datos.DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion);
            Claro.Datos.DAAB.DAABRequest.Parameter[] arrParam = {new Claro.Datos.DAAB.DAABRequest.Parameter("P_DESC_TRANSACCION",DbType.String ,400, descripcion, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_CODIGO_TRANSACCION",DbType.String,15, codigo, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_DESC_ERROR",DbType.String,400, deserror, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_USUARIO_TRANSACCION",DbType.String ,20, usuario, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_NOMBRE_TABLA",DbType.String,40, nombretabla, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_ARCHIVO_LOG",DbType.String,10000, archivo == string.Empty? "0" : archivo, ParameterDirection.Input)};

            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = pkgNameOffSAP + ".MIG_Trs_Registra_Log_Trs";
            objRequest.Parameters.AddRange(arrParam);
            objRequest.Factory.ExecuteNonQuery(ref objRequest);

        }

        public void RegistrarLog(string descripcion, string codigo, string deserror, string fecha, string usuario, string nombretabla, long archivo)
        {

            String strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA"); //"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas";
            Claro.Datos.DAAB.DAABRequest objRequest = new Claro.Datos.DAAB.DAABRequest(Claro.Datos.DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion);
            Claro.Datos.DAAB.DAABRequest.Parameter[] arrParam = {new Claro.Datos.DAAB.DAABRequest.Parameter("P_DESC_TRANSACCION",DbType.String ,400, descripcion, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_CODIGO_TRANSACCION",DbType.String,15, codigo, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_DESC_ERROR",DbType.String,400, deserror, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_USUARIO_TRANSACCION",DbType.String ,20, usuario, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_NOMBRE_TABLA",DbType.String,40, nombretabla, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_ARCHIVO_LOG",DbType.String,10000, archivo, ParameterDirection.Input)};

            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = pkgNameOffSAP + ".MIG_Trs_Registra_Log_Trs";
            objRequest.Parameters.AddRange(arrParam);
            objRequest.Factory.ExecuteNonQuery(ref objRequest);

        }

        public DataTable listarPedidosPagoCabecera(string fecha)
        {
            String strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA"); //"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas";			
            Claro.Datos.DAAB.DAABRequest objRequest = new Claro.Datos.DAAB.DAABRequest(Claro.Datos.DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion);
            Claro.Datos.DAAB.DAABRequest.Parameter[] arrParam = {new Claro.Datos.DAAB.DAABRequest.Parameter("P_FECHA_PEDIDO",DbType.String ,10, fecha, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("C_PAGOS",DbType.Object, ParameterDirection.Output)};

            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = pkgNameOffSAP + ".MIG_ListarPedidoPago";
            objRequest.Parameters.AddRange(arrParam);
            return objRequest.Factory.ExecuteDataset(ref objRequest).Tables[0];
        }

        public DataSet listarPedidoPago(int codigoTransaccion, string codCajero, string codOficina)
        {

            String strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA"); //"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas";			
            Claro.Datos.DAAB.DAABRequest objRequest = new Claro.Datos.DAAB.DAABRequest(Claro.Datos.DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion);
            Claro.Datos.DAAB.DAABRequest.Parameter[] arrParam = {new Claro.Datos.DAAB.DAABRequest.Parameter("P_DIARIO",DbType.Int32 ,20, 1, ParameterDirection.Input),
																 new Claro.Datos.DAAB.DAABRequest.Parameter("P_COD_VENDEDOR",DbType.String ,10, codCajero, ParameterDirection.Input),
																 new Claro.Datos.DAAB.DAABRequest.Parameter("P_COD_OFICINA",DbType.String ,4, codOficina, ParameterDirection.Input),
																 new Claro.Datos.DAAB.DAABRequest.Parameter("C_CABECERA",DbType.Object, ParameterDirection.Output),
																 new Claro.Datos.DAAB.DAABRequest.Parameter("C_DETALLE",DbType.Object, ParameterDirection.Output),
																 new Claro.Datos.DAAB.DAABRequest.Parameter("C_PAGO",DbType.Object, ParameterDirection.Output)};

            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = pkgNameOffSAP + ".MIG_EXTRAE_PEDIDO_FACTURA";
            objRequest.Parameters.AddRange(arrParam);
            return objRequest.Factory.ExecuteDataset(ref objRequest);
        }

        public DataSet GetPedidoPago(int idPedido)
        {

            String strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA"); //"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas";			
            Claro.Datos.DAAB.DAABRequest objRequest = new Claro.Datos.DAAB.DAABRequest(Claro.Datos.DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion);
            Claro.Datos.DAAB.DAABRequest.Parameter[] arrParam = {new Claro.Datos.DAAB.DAABRequest.Parameter("P_TRANSACCIONID",DbType.Int32 ,20, idPedido, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("C_CABECERA",DbType.Object, ParameterDirection.Output),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("C_DETALLE",DbType.Object, ParameterDirection.Output),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("C_PAGO",DbType.Object, ParameterDirection.Output),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_FOR_PROCESS",DbType.Int32 ,20, 1, ParameterDirection.Input)};

            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = pkgNameOffSAP + ".CONF_CON_IMPRESION";
            objRequest.Parameters.AddRange(arrParam);
            return objRequest.Factory.ExecuteDataset(ref objRequest);
        }

        public void actualizarEstadoMigracion(int codigoCabecera, string LogSap, string registrasap)
        {

            String strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA"); //"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas";
            Claro.Datos.DAAB.DAABRequest objRequest = new Claro.Datos.DAAB.DAABRequest(Claro.Datos.DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion);
            Claro.Datos.DAAB.DAABRequest.Parameter[] arrParam = {new Claro.Datos.DAAB.DAABRequest.Parameter("P_Id",DbType.Int32 ,20, codigoCabecera, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_LOG",DbType.String,400, LogSap, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_Registra_Sap",DbType.String,2, registrasap, ParameterDirection.Input)};

            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = pkgNameOffSAP + ".Mig_Trs_Actualiza_Cabecera";
            objRequest.Parameters.AddRange(arrParam);
            objRequest.Factory.ExecuteNonQuery(ref objRequest);
        }


        public void RegistrarNroDocumento(int Codigo, string Pedido, string Factura)
        {

            String strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA"); //"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas";
            Claro.Datos.DAAB.DAABRequest objRequest = new Claro.Datos.DAAB.DAABRequest(Claro.Datos.DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion);
            Claro.Datos.DAAB.DAABRequest.Parameter[] arrParam = {new Claro.Datos.DAAB.DAABRequest.Parameter("P_Id_T_Trs_Pedido",DbType.Int32 ,20, Codigo, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_Pedido",DbType.String,20, Pedido, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_Factura",DbType.String,20, Factura, ParameterDirection.Input)};

            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = pkgNameOffSAP + ".Conf_Registrar_Pedido_Factura";
            objRequest.Parameters.AddRange(arrParam);
            objRequest.Factory.ExecuteNonQuery(ref objRequest);
        }

        public DataTable listarPagosAnuladosNoMigrados(string fecha, string cajero, string oficinaventa)
        {
            String strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA"); //"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas";			
            Claro.Datos.DAAB.DAABRequest objRequest = new Claro.Datos.DAAB.DAABRequest(Claro.Datos.DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion);
            Claro.Datos.DAAB.DAABRequest.Parameter[] arrParam = {
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_FECHA",DbType.String ,10, fecha, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_CAJERO",DbType.String ,5, cajero, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_OFICINA",DbType.String ,4, oficinaventa, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("C_ESTADORECAUDACION",DbType.Object, ParameterDirection.Output)
																};

            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = pkgNameOffSAP + ".CONF_MIG_GetEstadoRecaudacion";
            objRequest.Parameters.AddRange(arrParam);
            return objRequest.Factory.ExecuteDataset(ref objRequest).Tables[0];
        }

        public void registrarAnulacionesPago(String TransaccionID, String Posicion, String RegZAP, String LogRegistro)
        {
            String strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA"); //"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas";
            Claro.Datos.DAAB.DAABRequest objRequest = new Claro.Datos.DAAB.DAABRequest(Claro.Datos.DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion);
            Claro.Datos.DAAB.DAABRequest.Parameter[] arrParam = {new Claro.Datos.DAAB.DAABRequest.Parameter("P_TRANSACCIONID",DbType.String ,15, TransaccionID, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_POSTMP",DbType.String,6, Posicion, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_REGISTRASAP",DbType.String,1, RegZAP, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_LOGREGISTRO",DbType.String,250, LogRegistro, ParameterDirection.Input)};

            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = pkgNameOffSAP + ".MIG_RegistrarAnulacionesPagos";
            objRequest.Parameters.AddRange(arrParam);
            objRequest.Factory.ExecuteNonQuery(ref objRequest);
        }

        public static void WriteLog(string message)
        {
			String nombreArchivo = String.Format("{0}_{1}", ConfigurationSettings.AppSettings["constNameLogCuadreCajaEnvioSap"],DateTime.Now.ToString("yyyyMMdd"));
			String filePath = ConfigurationSettings.AppSettings["constRutaLogActivacionPrepago"];
			String file = String.Format(@"{0}\{1}.txt", filePath, nombreArchivo);
			String lineaLog = String.Format(" {0}{1} {2} -{3}", Environment.NewLine, DateTime.Now.ToString("yyyy-MM-dd-hh:mm:ss"), UsuarioCaja, message);
			Scripting.TextStream archivoLog;


			try
			{
                                //INI PROY-140126
				NetLogger.EscribirLog(NetLogger.NivelLog.Aplicacion, nombreArchivo, message);
				}
			catch(Exception ex)
			{
				NetLogger.EscribirLog(NetLogger.NivelLog.Aplicacion, nombreArchivo, message);
				throw ex;
			}

			}
                                 //FIN PROY-140126
        public static void WriteLog(string format, params object[] valores)
        {
            string mensaje = String.Format(format, valores);
            WriteLog(mensaje);
        }

        public static string UsuarioCaja;


        public DataSet getPendientesMigrar(string vendedor, string codOficina, string fechaCuadre, string idProceso)
        {

            String strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA"); //"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas";			
            Claro.Datos.DAAB.DAABRequest objRequest = new Claro.Datos.DAAB.DAABRequest(Claro.Datos.DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion);
            Claro.Datos.DAAB.DAABRequest.Parameter[] arrParam = {new Claro.Datos.DAAB.DAABRequest.Parameter("P_VENDEDOR",DbType.String ,10, vendedor, ParameterDirection.Input),
																 new Claro.Datos.DAAB.DAABRequest.Parameter("P_OFICINA",DbType.String ,4, codOficina, ParameterDirection.Input),
																 new Claro.Datos.DAAB.DAABRequest.Parameter("P_FECHA_REGISTRO",DbType.String ,10, fechaCuadre, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_ID_PROCESO",DbType.String ,17, idProceso, ParameterDirection.Input),
                                                                 new Claro.Datos.DAAB.DAABRequest.Parameter("C_TRS_PED",DbType.Object, ParameterDirection.Output),
																 new Claro.Datos.DAAB.DAABRequest.Parameter("C_CABECERA",DbType.Object, ParameterDirection.Output),
																 new Claro.Datos.DAAB.DAABRequest.Parameter("C_Detalle",DbType.Object, ParameterDirection.Output),
																 new Claro.Datos.DAAB.DAABRequest.Parameter("C_PAGO",DbType.Object, ParameterDirection.Output)};

            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = pkgNameOffSAP + ".CUA_GET_TRANSACCIONES_PEDIDO";
            objRequest.Parameters.AddRange(arrParam);
            return objRequest.Factory.ExecuteDataset(ref objRequest);
        }


        internal DataSet cargarRecaudaciones(string codigoCajero, string codigoTienda, string fechaActual, string idProceso)
        {
            String strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA"); //"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas";			
            Claro.Datos.DAAB.DAABRequest objRequest = new Claro.Datos.DAAB.DAABRequest(Claro.Datos.DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion);
            Claro.Datos.DAAB.DAABRequest.Parameter[] arrParam = {new Claro.Datos.DAAB.DAABRequest.Parameter("P_VENDEDOR",DbType.String ,5, codigoCajero, ParameterDirection.Input),
																 new Claro.Datos.DAAB.DAABRequest.Parameter("P_OFICINA",DbType.String ,4, codigoTienda, ParameterDirection.Input),
																 new Claro.Datos.DAAB.DAABRequest.Parameter("P_FECHA_REGISTRO",DbType.String ,10, fechaActual, ParameterDirection.Input),
																 new Claro.Datos.DAAB.DAABRequest.Parameter("P_ID_PROCESO",DbType.String ,17, idProceso, ParameterDirection.Input),
                                                                 new Claro.Datos.DAAB.DAABRequest.Parameter("C_TRS_PED",DbType.Object, ParameterDirection.Output),
																 new Claro.Datos.DAAB.DAABRequest.Parameter("C_DEUDA",DbType.Object, ParameterDirection.Output),
																 new Claro.Datos.DAAB.DAABRequest.Parameter("C_RECIBO",DbType.Object, ParameterDirection.Output),
																 new Claro.Datos.DAAB.DAABRequest.Parameter("C_PAGO",DbType.Object, ParameterDirection.Output)};

            objRequest.CommandType = CommandType.StoredProcedure;
            objRequest.Command = pkgNameOffSAP + ".CUA_GET_TRANSACCIONES_RECAUDA";
            objRequest.Parameters.AddRange(arrParam);
            return objRequest.Factory.ExecuteDataset(ref objRequest);
        }


		public string separarRegistrosMigrarcion(string vendedor, string codOficina, string fechaCuadre)
		{
			String idProceso = string.Empty;
			String strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA"); //"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas";			
			Claro.Datos.DAAB.DAABRequest objRequest = new Claro.Datos.DAAB.DAABRequest(Claro.Datos.DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion);
			Claro.Datos.DAAB.DAABRequest.Parameter[] arrParam = {new Claro.Datos.DAAB.DAABRequest.Parameter("P_FLAG_CUADRE",DbType.Int32,  1, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_VENDEDOR",DbType.String ,10, vendedor, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_OFICINA",DbType.String ,4, codOficina, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_FECHA_REGISTRO",DbType.String,10,fechaCuadre, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_ID_PROCESO",DbType.String, ParameterDirection.Output)};
			objRequest.CommandType = CommandType.StoredProcedure;
			objRequest.Command = pkgNameOffSAP + ".MIG_SEP_TRANSACCIONES_PEDIDO";
			objRequest.Parameters.AddRange(arrParam);
			objRequest.Factory.ExecuteNonQuery(ref objRequest);
			idProceso = Convert.ToString((objRequest.Parameters[4] as System.Data.IDataParameter).Value);
			return idProceso.Trim();
		}


		public string separarRegistrosMigrarcionFP(string vendedor, string codOficina, string fechaCuadre)
		{
			String idProceso = string.Empty;
			String strCadenaConexion = objSeg.FP_GetConnectionString("2", "SISCAJA"); //"user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas";			
			Claro.Datos.DAAB.DAABRequest objRequest = new Claro.Datos.DAAB.DAABRequest(Claro.Datos.DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion);
			Claro.Datos.DAAB.DAABRequest.Parameter[] arrParam = {new Claro.Datos.DAAB.DAABRequest.Parameter("P_FLAG_CUADRE",DbType.Int32,  1, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_VENDEDOR",DbType.String ,5, vendedor, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_OFICINA",DbType.String ,4, codOficina, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_FECHA_REGISTRO",DbType.String,10,fechaCuadre, ParameterDirection.Input),
																	new Claro.Datos.DAAB.DAABRequest.Parameter("P_ID_PROCESO",DbType.String, ParameterDirection.Output)};
			objRequest.CommandType = CommandType.StoredProcedure;
			objRequest.Command = pkgNameOffSAP + ".MIG_SEP_TRANSACCIONES_RECAUDA";
			objRequest.Parameters.AddRange(arrParam);
			objRequest.Factory.ExecuteNonQuery(ref objRequest);
			idProceso = Convert.ToString((objRequest.Parameters[4] as System.Data.IDataParameter).Value);
			return idProceso.Trim();
		}
    }
}
