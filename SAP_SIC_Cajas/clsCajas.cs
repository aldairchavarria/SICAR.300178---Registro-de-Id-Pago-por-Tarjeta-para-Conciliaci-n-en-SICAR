using System;
using System.Data;

namespace SAP_SIC_Cajas
{
	/// <summary>
	/// Summary description for clsCajas.
	/// </summary>
	public class clsCajas
	{
		public clsCajas()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public DataSet Set_PagosCajero(string Fecha, string strPagos, string Usuario, string NotaCredito )
		{

			DataSet dsReturn = new DataSet();
			string[] strTrama1;
			string[] strTrama2;

			try
			{
				ZES_PAGOSTable objPagos = new ZES_PAGOSTable();
				ZES_PAGOS objLinPagos = new ZES_PAGOS();
				ZST_PV_LOGTable objLog = new ZST_PV_LOGTable();

				strTrama1 = strPagos.Split('|');

				for (int i=0;i<strTrama1.Length;i++)
				{

                    strTrama2 = strTrama1[i].Split(';'); 

					objLinPagos.Org_Vtas = strTrama2[0];
					objLinPagos.Of_Vtas = strTrama2[1];
					objLinPagos.Via_Pago = strTrama2[2];
					objLinPagos.Conc_Bsqda = strTrama2[3];
					objLinPagos.Solicitante = strTrama2[4];
					objLinPagos.Importe = strTrama2[5];
					objLinPagos.Moneda = strTrama2[6];
					objLinPagos.T_Cambio = strTrama2[7];
					objLinPagos.Referencia = strTrama2[8];
					objLinPagos.Glosa = strTrama2[9];
					objLinPagos.F_Pedido = strTrama2[10];
					objLinPagos.Cond_Pago = strTrama2[11];
					objLinPagos.Nro_Exactus = strTrama2[12];
					objLinPagos.Pos = strTrama2[13];

					objPagos.Add(objLinPagos);

					objLinPagos = null;
					objLinPagos = new ZES_PAGOS();

				}
				
				SAP_SIC_Cajas proxy = ConectaSAP();

				proxy.Zpvu_Rfc_Trs_Pagos_Cajero(FormatoFecha(Fecha),NotaCredito,Usuario,ref objLog,ref objPagos);

				dsReturn.Tables.Add(objLog.ToADODataTable());
				proxy.Connection.Close();

			}
			catch (Exception ex)
			{
				dsReturn = null;
			}

			return dsReturn;

		}


		public DataSet Set_NroSunatCajero( string Nro_Documento,string Nro_Asignar, string Nro_Ref_Franquicia,string Oficina_Venta, 
			                              string Reasignar, string Fecha, string strPagos, string Nro_Ref_Corner, string Tipo_Doc_Corner, 
			                              string Usuario, string Caja, string Nota_Credito, out string Nro_Ref_Asignado)
		{
			DataSet dsReturn = new DataSet();
			string[] strTrama1;
			string[] strTrama2;

			try 
			{
				ZES_PAGOSTable objPagos = new ZES_PAGOSTable();
				ZES_PAGOS objLinPagos = new ZES_PAGOS();
				BAPIRET2Table objLog = new BAPIRET2Table();

			if (strPagos.Length>0)
			{
				strTrama1 = strPagos.Split('|');

				for (int i=0;i<strTrama1.Length;i++)
				{

					strTrama2 = strTrama1[i].Split(';'); 

					objLinPagos.Org_Vtas = strTrama2[0];
					objLinPagos.Of_Vtas = strTrama2[1];
					objLinPagos.Via_Pago = strTrama2[2];
					objLinPagos.Conc_Bsqda = strTrama2[3];
					objLinPagos.Solicitante = strTrama2[4];
					objLinPagos.Importe = strTrama2[5];
					objLinPagos.Moneda = strTrama2[6];
					objLinPagos.T_Cambio = strTrama2[7];
					objLinPagos.Referencia = strTrama2[8];
					objLinPagos.Glosa = strTrama2[9];
					objLinPagos.F_Pedido = strTrama2[10];
					objLinPagos.Cond_Pago = strTrama2[11];
					objLinPagos.Nro_Exactus = strTrama2[12];
					objLinPagos.Pos = strTrama2[13];

					objPagos.Add(objLinPagos);

					objLinPagos = null;
					objLinPagos = new ZES_PAGOS();

				}
			}
                SAP_SIC_Cajas proxy = ConectaSAP();

				proxy.Zpvu_Rfc_Trs_Caj_Set_Nro_Sunat(Caja,FormatoFecha(Fecha),
				    Nota_Credito,Nro_Asignar,Nro_Documento,Nro_Ref_Corner,Nro_Ref_Franquicia,
					Oficina_Venta, Reasignar,Tipo_Doc_Corner,Usuario,out Nro_Ref_Asignado, ref objPagos,ref objLog);

				dsReturn.Tables.Add(objLog.ToADODataTable());

                proxy.Connection.Close();
			}
			catch (Exception ex)
			{
                dsReturn = null;
				Nro_Ref_Asignado = "";
				throw ex;
			}

			return dsReturn;

		}


		public DataSet Set_AnulPagosCajero(string FechaContable, string Nro_Referencia, string Via_Pago, string Oficina_Venta, string Usuario)
		{
			DataSet dsReturn = new DataSet();

			//try
			//{
                ZST_PV_LOGTable objLog = new ZST_PV_LOGTable();

				SAP_SIC_Cajas proxy = ConectaSAP();
				proxy.Zpvu_Rfc_Trs_Pagos_Cajero_Anul(FormatoFecha(FechaContable),Nro_Referencia,Oficina_Venta,Usuario,Via_Pago,ref objLog);

				dsReturn.Tables.Add(objLog.ToADODataTable());
			    proxy.Connection.Close();

			//}
			//catch (Exception ex)
			//{
			//	dsReturn = null;
			//}

			return dsReturn;
		}

		public DataSet Get_AsigCajero(string Usuario,string Fecha, string Oficina)
		{

			DataSet dsReturn = new DataSet();

			try
			{
				ZST_CAJ_DIARIOTable objDiario = new ZST_CAJ_DIARIOTable();
				BAPIRET2Table objLog = new BAPIRET2Table();

				SAP_SIC_Cajas proxy = ConectaSAP();
				proxy.Zpvu_Rfc_Con_Asig_Cajero(FormatoFecha(Fecha),Usuario,Oficina, ref objDiario, ref objLog);

				dsReturn.Tables.Add(objDiario.ToADODataTable());
				dsReturn.Tables.Add(objLog.ToADODataTable());
				proxy.Connection.Close();

			}
			catch (Exception ex)
			{
				dsReturn = null;
			}

			return dsReturn;

		}


		public DataSet Get_CajaOficinas(string Oficina)
		{
			DataSet dsReturn = new DataSet();

			try
			{
				ZST_CAJASTable objCaja = new ZST_CAJASTable();
				BAPIRET2Table objLog = new BAPIRET2Table();

				SAP_SIC_Cajas proxy = ConectaSAP();
				proxy.Zpvu_Rfc_Con_Caja_Oficinas(Oficina,ref objCaja,ref objLog);

				dsReturn.Tables.Add(objCaja.ToADODataTable());
				dsReturn.Tables.Add(objLog.ToADODataTable());
				proxy.Connection.Close();

			}
			catch (Exception ex)
			{
				dsReturn = null;
			}

			return dsReturn;

		}

		public DataSet Get_CajeroCajaCerrada(string Oficina, string Fecha, string Usuario,out string Cierre_Realizado,out decimal Saldo_Inicial, out decimal Caja_Buzon)
		{
			DataSet dsReturn = new DataSet();

			try
			{
				BAPIRET2Table objLog = new BAPIRET2Table();

				SAP_SIC_Cajas proxy = ConectaSAP();
				proxy.Zpvu_Rfc_Con_Cajero_Caja_Cerra(FormatoFecha(Fecha),Oficina,Usuario,out Caja_Buzon,out Cierre_Realizado, out Saldo_Inicial, ref objLog);

				dsReturn.Tables.Add(objLog.ToADODataTable());
				proxy.Connection.Close();

			}
			catch (Exception ex)
			{
				dsReturn = null;
				Cierre_Realizado = "";
				Saldo_Inicial = 0;
                Caja_Buzon = 0;
			}

			return dsReturn;

		}


/*		public DataSet Set_ArqueoCajero(string Fecha,string Oficina, decimal Saldo_Inicial,decimal Ingreso_Efectivo_Dia, decimal Caja_Buzon, decimal Remesa,
			decimal Monto_Pndnte_Ingreso, decimal Monto_Sobrante, string Cerrar_El_Dia, string Usuario, out decimal Saldo_Inicial_R, out decimal Ingreso_Efectivo_Dia_R, 
			out decimal Caja_Buzon_R, out decimal Remesa_R, out decimal Monto_Pndnte_Ingreso_R, out decimal Monto_Sobrante_R)
		{

			DataSet dsReturn = new DataSet();

			try
			{
				ZST_PV_CUOTA_FACTURATable objCuota = new ZST_PV_CUOTA_FACTURATable();
				ZST_PV_MATER_FACTURATable objMaterial = new ZST_PV_MATER_FACTURATable();
				ZST_PV_MONTOS_CUADRETable objMontos = new ZST_PV_MONTOS_CUADRETable();
				BAPIRET2Table objLog = new BAPIRET2Table();

				SAP_SIC_Cajas proxy = ConectaSAP();
				//modificado JCR
				proxy.Zpvu_Rfc_Rep_Arqueo_Cajero(Caja_Buzon,Cerrar_El_Dia,FormatoFecha(Fecha),Ingreso_Efectivo_Dia,Monto_Pndnte_Ingreso,Monto_Sobrante,Oficina,
					Remesa,Saldo_Inicial,Usuario,out Caja_Buzon_R,out Ingreso_Efectivo_Dia_R,out Monto_Pndnte_Ingreso_R,out Monto_Sobrante_R, out Remesa_R, out Saldo_Inicial_R,
					ref objCuota,ref objMaterial, ref objMontos, ref objLog);
				                

				dsReturn.Tables.Add(objCuota.ToADODataTable());
				dsReturn.Tables.Add(objMaterial.ToADODataTable());
				dsReturn.Tables.Add(objMontos.ToADODataTable());
				dsReturn.Tables.Add(objLog.ToADODataTable());
				proxy.Connection.Close();

			}
			catch (Exception ex)
			{
				dsReturn = null;
				
                Saldo_Inicial_R = 0;
				Ingreso_Efectivo_Dia_R = 0;
                Caja_Buzon_R = 0;
				Remesa_R = 0;
				Monto_Pndnte_Ingreso_R=0;
				Monto_Sobrante_R=0;

			}

			return dsReturn;

		}
*/

		public DataSet Set_CuadreCajero(string Fecha,string Oficina, decimal Saldo_Inicial,decimal Ingreso_Efectivo_Dia, decimal Caja_Buzon, decimal Caja_Buzon_Cheque, decimal Remesa,
			decimal Monto_Pndnte_Ingreso, decimal Monto_Sobrante, string Cerrar_El_Dia, string Usuario)
		{

			DataSet dsReturn = new DataSet();

			try
			{
				BAPIRET2Table objLog = new BAPIRET2Table();

				SAP_SIC_Cajas proxy = ConectaSAP();
				proxy.Zpvu_Rfc_Rep_Cuadre_Cajero(Caja_Buzon,Caja_Buzon_Cheque,Cerrar_El_Dia,FormatoFecha(Fecha),Ingreso_Efectivo_Dia,Monto_Pndnte_Ingreso,Monto_Sobrante,Oficina,
				Remesa,Saldo_Inicial,Usuario,ref objLog);
				
				dsReturn.Tables.Add(objLog.ToADODataTable());
				proxy.Connection.Close();

			}
			catch (Exception ex)
			{
				dsReturn = null;
				throw ex;
		    }

			return dsReturn;

		}

		public DataSet Set_CajeroDiario(string Oficina, string Usuario, string Fecha, string Caja)
		{
			DataSet dsReturn = new DataSet();

			try
			{
				BAPIRET2Table objLog = new BAPIRET2Table();

				SAP_SIC_Cajas proxy = ConectaSAP();
				proxy.Zpvu_Rfc_Trs_Cajero_Diario(Caja,FormatoFecha(Fecha),Usuario,Oficina,ref objLog);

				dsReturn.Tables.Add(objLog.ToADODataTable());
				proxy.Connection.Close();

			}
			catch (Exception ex)
			{
				dsReturn = null;
			}

			return dsReturn;
		}


		private string FormatoFecha(string Fecha)
		{

			if (Fecha.Length > 0 )
				return Fecha.Substring(6,4) + "/" + Fecha.Substring(3,2) + "/" + Fecha.Substring(0,2);
			else
				return "";

		}

		private SAP_SIC_Cajas ConectaSAP()
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

			SAP_SIC_Cajas  proxy;
				
			if (strLoadBal == "0")
			{
				proxy = new SAP_SIC_Cajas("CLIENT=" + strCliente + " USER=" + strUsuario + " PASSWD=" + strPassword + " LANG=" + strLanguage +" ASHOST=" + strApplicationServer + " SYSNR=" + strSistema);
			}
			else
			{
                proxy = new SAP_SIC_Cajas("CLIENT=" + strCliente + " USER=" + strUsuario + " PASSWD=" + strPassword + " LANG=" + strLanguage +" MSHOST=" + strMessServ + " R3NAME=" + strR3Name + " GROUP=" + strGroup);
			}

			return proxy;

		}

		public Boolean Set_RegistroRemesa(string Num_Bolsa, string Detalle, out string strMensaje){
		
			DataSet dsReturn = new DataSet();
			string[] arrDetalle;
			string[] arrLineaDetalle;	
			strMensaje = "";

			SAP_SIC_Cajas proxy = ConectaSAP();
			BAPIRET2Table objLog = new BAPIRET2Table();
			ZPV_REMESATable Zremesa = new ZPV_REMESATable();
			
			try
			{
				arrDetalle = Detalle.Split('|');
				if(Detalle.Length>0)
				{
					for (int i=0;i<arrDetalle.Length;i++)
					{
						arrLineaDetalle = arrDetalle[i].Split(';');

						ZPV_REMESA objRemesa = new ZPV_REMESA();

						objRemesa.Mandt = arrLineaDetalle[0];
						objRemesa.Nro_Bolsa = arrLineaDetalle[1];
						objRemesa.Fecha_Envio = FormatoFecha(arrLineaDetalle[2]);
						objRemesa.Nro_Sobre = arrLineaDetalle[3];
						objRemesa.Fecha_Registro = FormatoFecha(arrLineaDetalle[4]);
						objRemesa.Tipo_Remesa = arrLineaDetalle[5];
						objRemesa.Importe = Convert.ToDecimal(arrLineaDetalle[6]);
						objRemesa.Oficina_Venta = arrLineaDetalle[7];
						objRemesa.Usuario = arrLineaDetalle[8];

						Zremesa.Add(objRemesa);	
						
					}						
				}
				
				proxy.Zpvu_Rfc_Registro_Remesa(Num_Bolsa,ref objLog, ref Zremesa);

				dsReturn.Tables.Add(objLog.ToADODataTable());		
				
				if (dsReturn.Tables[0].Rows.Count>0)
				{	
					foreach(DataRow dr in dsReturn.Tables[0].Rows)
					{
						string tipo = Convert.ToString(dr["TYPE"]);
						if (tipo == "E")
						{
							strMensaje = Convert.ToString(dr["MESSAGE"]);												
						}		
					}
				}

			}
			catch (Exception ex)
			{
				strMensaje = ex.Message;
				throw new Exception(ex.Message);
			}
			finally{
				proxy.Connection.Close();
				dsReturn = null;				
			}

			return true;
		}

	}
}
