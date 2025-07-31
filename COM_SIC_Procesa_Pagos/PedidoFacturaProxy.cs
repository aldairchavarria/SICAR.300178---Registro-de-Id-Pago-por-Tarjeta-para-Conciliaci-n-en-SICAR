using System;
using SAP_SIC_Ventas;
using System.Data;

namespace COM_SIC_Procesa_Pagos
{
	/// <summary>
	/// Summary description for PedidoFacturaProxy.
	/// </summary>
	public class PedidoFacturaProxy
	{
		private string connectionString;
		public PedidoFacturaProxy()
		{			
		}

		public PedidoFacturaProxy(string ConnectionString)
		{		
			connectionString = ConnectionString;
		}

		private char Delimitador{get{return ';';}}

		private string[] TramaToArray(string trama)
		{
			return trama.Split(this.Delimitador);
		}

		private string[] TramaToArray(string trama,char delimitador)
		{
			return trama.Split(delimitador);
		}


		public RespuestaLog CreaPedidoFactura(ZTI_CAB_PEDIDO cabeceraPedido,ZTI_DET_PEDIDO[] tramaDetalle,ZTI_PAGOS[] tramaPagos,out DataTable respuestaTable)
		{
			string numeroFactura,numeroPedido;
			
			numeroFactura = String.Empty;
			numeroPedido = String.Empty;

			ZTI_CAB_PEDIDOTable cabeceraPedidoTable = new ZTI_CAB_PEDIDOTable();
			ZTI_DET_PEDIDOTable detallePedidoTable=new ZTI_DET_PEDIDOTable();
			ZTI_PAGOSTable pagosTable= new ZTI_PAGOSTable();
			BAPIRET2Table respuestaOperacionTable=new BAPIRET2Table();
	
			cabeceraPedidoTable.Add(cabeceraPedido);

			foreach(ZTI_DET_PEDIDO itemDetalle in tramaDetalle)			
				detallePedidoTable.Add(itemDetalle);
			
			foreach(ZTI_PAGOS itemPago in tramaPagos)			
				pagosTable.Add(itemPago);			

			PedidoFactura rfcPedidoFactura=new PedidoFactura(connectionString);
			RespuestaLog respuesta= new RespuestaLog();

			try
			{
				rfcPedidoFactura.Zsicar_Rfc_Crea_Ped_Fac(out numeroFactura
					,out numeroPedido
					,ref cabeceraPedidoTable
					,ref detallePedidoTable
					,ref pagosTable
					,ref respuestaOperacionTable);
				
				respuesta.NumeroFactura = numeroFactura;
				respuesta.NumeroPedido = numeroPedido;
				respuesta.LogSap = Convert.ToString(respuestaOperacionTable.ToADODataTable().Rows[0][3]);

				//respuestaTable= respuestaOperacionTable.ToADODataTable();
			
				DataTable dtrespuesta= new DataTable();
				dtrespuesta = respuestaOperacionTable.ToADODataTable().Clone();

				foreach(DataRow row in respuestaOperacionTable.ToADODataTable().Rows)
				{					
					//					respuesta.MensajeError +=row.ItemArray[3].ToString() + "||"; //"MESSAGE"
						
					DataRow reg= dtrespuesta.NewRow();
					//reg = row;
					//dtrespuesta.Rows.Add(reg);
					dtrespuesta.Rows.Add(row.ItemArray);

					if(row.ItemArray[0].ToString()=="E")
					{
						respuesta.IndicadorError =row.ItemArray[0].ToString();
						respuesta.MensajeError = row.ItemArray[3].ToString();
						//throw new Exception(row.ItemArray[3].ToString());						
						//respuesta.MensajeError =row.ItemArray[3].ToString(); //"MESSAGE"
					}				
				}
				respuestaTable = dtrespuesta;
			}
			catch(SAP.Connector.RfcCommunicationException ex)
			{
				throw ex;
			}
			catch(ApplicationException ex)
			{
				throw new ApplicationException("Ocurrio un error al Invocar el RFC Zsicar_Rfc_Crea_Ped_Fac, para más detalle consulte innerexeption",ex);
			}
			finally
			{
				rfcPedidoFactura.Connection.Close();
			}

			return respuesta;
		}
	}

	public struct RespuestaLog
	{
		public string NumeroFactura;
		public string NumeroPedido;
		public string LogSap;
		public string IndicadorError;
		public string MensajeError;
	}
}
