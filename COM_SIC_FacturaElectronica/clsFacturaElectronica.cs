using System;
using Seguridad_NET;
using Claro.Datos.DAAB;
using System.Data;
using System.Configuration;


namespace COM_SIC_FacturaElectronica
{
	/// <summary>
	/// Summary description for clsFacturaElectronica.
	/// </summary>
	/// 
	public class clsFacturaElectronica
	{
		clsSeguridad objSeguridad = new clsSeguridad();
		string strCadenaConexion = String.Empty;
		string strCadenaEsquema = String.Empty;
		string MSSAP_PKG_PAGO= "PKG_PAGO_JRM";
		string MSSAP_SSAPSI_EVENTO_ERROR= ".SSAPSI_EVENTO_ERROR";



		public clsFacturaElectronica()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public bool RegistrarEventoError(Int64 pIdPago,string pComprobante,string pCodError, string pMensaje, string pMonto, string pAplicacion, string pUsuaCreacion,out string pCodRpta, out string pMsjRpta )
		{
			string sCodRpta="", sMsjRpta="";

			bool bResultado = false;
			try
			{
				
				strCadenaConexion = objSeguridad.FP_GetConnectionString("2", ConfigurationSettings.AppSettings["BD_MSSAP"]);
				strCadenaEsquema = ConfigurationSettings.AppSettings["ESQUEMA_SINERGIA"];

				DAABRequest objRequest = new DAABRequest(DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion);
				DAABRequest.Parameter[] arrParam = { new DAABRequest.Parameter("PI_PAGON_IDPAGO", DbType.Int64, pIdPago, ParameterDirection.Input),
													   new DAABRequest.Parameter("PI_SSAPV_COMPROBANTE", DbType.String, pComprobante, ParameterDirection.Input),
													   new DAABRequest.Parameter("PI_SSAPV_COD_ERROR", DbType.String, pCodError, ParameterDirection.Input),
													   new DAABRequest.Parameter("PI_SSAPV_MENSAJE", DbType.String, pMensaje, ParameterDirection.Input),
													   new DAABRequest.Parameter("PI_SSAPN_MONTO", DbType.Double, pMonto, ParameterDirection.Input),
													   new DAABRequest.Parameter("PI_SSAPV_APLICACION", DbType.String, pAplicacion, ParameterDirection.Input),
													   new DAABRequest.Parameter("PI_SSAPV_USU_CREA", DbType.String, pUsuaCreacion, ParameterDirection.Input),
													   new DAABRequest.Parameter("PO_COD_RPTA", DbType.String, sCodRpta, ParameterDirection.Output),
													   new DAABRequest.Parameter("PO_MSJ_RPTA", DbType.String, sMsjRpta, ParameterDirection.Output)};

				objRequest.CommandType = CommandType.StoredProcedure;
				objRequest.Command = strCadenaEsquema+ "." + MSSAP_PKG_PAGO + MSSAP_SSAPSI_EVENTO_ERROR;
				objRequest.Parameters.AddRange(arrParam);
//				objRequest.Parameters.Add(arrParam);
				objRequest.Factory.ExecuteNonQuery(ref objRequest);
//				dsPool = objRequest.Factory.ExecuteDataset(ref objRequest);

				IDataParameter parSalida1,parSalida2;

				parSalida1 = (IDataParameter)objRequest.Parameters[7];

				pCodRpta = parSalida1.Value.ToString();

				parSalida2 = (IDataParameter)objRequest.Parameters[8];

				pMsjRpta = parSalida2.Value.ToString();
				bResultado = true;

				objRequest.Parameters.Clear();

			}
			catch (Exception ex)
			{
				bResultado = false;
				pCodRpta = "-2";
				pMsjRpta = ex.Message;

			}
			return bResultado;
			
			
		}
/*
		public bool ActualizaEventoError(Int64 pIdPago,string pComprobante,string pCodError, string pMensaje, string pMonto, string pAplicacion, string pUsuaCreacion,out string pCodRpta, out string pMsjRpta )
		{
			string sCodRpta="", sMsjRpta="";

			bool bResultado = false;
			try
			{
				
				strCadenaConexion = objSeguridad.FP_GetConnectionString("2", ConfigurationSettings.AppSettings["BD_MSSAP"]);
				strCadenaEsquema = ConfigurationSettings.AppSettings["ESQUEMA_SINERGIA"];

				DAABRequest objRequest = new DAABRequest(DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion);
				DAABRequest.Parameter[] arrParam = { new DAABRequest.Parameter("PI_PAGON_IDPAGO", DbType.Int64, pIdPago, ParameterDirection.Input),
													   new DAABRequest.Parameter("PI_SSAPV_COMPROBANTE", DbType.String, pComprobante, ParameterDirection.Input),
													   new DAABRequest.Parameter("PI_SSAPV_COD_ERROR", DbType.String, pCodError, ParameterDirection.Input),
													   new DAABRequest.Parameter("PI_SSAPV_MENSAJE", DbType.String, pMensaje, ParameterDirection.Input),
													   new DAABRequest.Parameter("PI_SSAPN_MONTO", DbType.Double, pMonto, ParameterDirection.Input),
													   new DAABRequest.Parameter("PI_SSAPV_APLICACION", DbType.String, pAplicacion, ParameterDirection.Input),
													   new DAABRequest.Parameter("PI_SSAPV_USU_CREA", DbType.String, pUsuaCreacion, ParameterDirection.Input),
													   new DAABRequest.Parameter("PO_COD_RPTA", DbType.String, sCodRpta, ParameterDirection.Output),
													   new DAABRequest.Parameter("PO_MSJ_RPTA", DbType.String, sMsjRpta, ParameterDirection.Output)};

				objRequest.CommandType = CommandType.StoredProcedure;
				objRequest.Command = strCadenaEsquema+ "." + MSSAP_PKG_PAGO + MSSAP_SSAPSI_EVENTO_ERROR;
				objRequest.Parameters.AddRange(arrParam);
				//				objRequest.Parameters.Add(arrParam);
				objRequest.Factory.ExecuteNonQuery(ref objRequest);
				//				dsPool = objRequest.Factory.ExecuteDataset(ref objRequest);

				IDataParameter parSalida1,parSalida2;

				parSalida1 = (IDataParameter)objRequest.Parameters[7];

				pCodRpta = parSalida1.Value.ToString();

				parSalida2 = (IDataParameter)objRequest.Parameters[8];

				pMsjRpta = parSalida2.Value.ToString();
				bResultado = true;

				objRequest.Parameters.Clear();

			}
			catch (Exception ex)
			{
				bResultado = false;
				pCodRpta = "-2";
				pMsjRpta = ex.Message;

			}
			return bResultado;
			
			
		}
*/

	}
}
