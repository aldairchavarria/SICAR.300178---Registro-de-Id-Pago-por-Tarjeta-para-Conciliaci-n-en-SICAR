using System;
using System.Configuration;
using System.Data;
using System.Collections;
using COM_SIC_Activaciones;

namespace NEGOCIO_SIC_SANS
{
	/// <summary>
	/// Descripción breve de SansNegocio.
	/// </summary>
	public class SansNegocio
	{
		SimCardsWS.ebsSimcards objSimCards = new  SimCardsWS.ebsSimcards();
		string idAplicacion = ConfigurationSettings.AppSettings["CodAplicacion"].ToString();
		string nombreAplicacion = ConfigurationSettings.AppSettings["constAplicacion"].ToString();	
		string nameFile = ConfigurationSettings.AppSettings["constNameLogWS_Sans"].ToString() + "_" + string.Format("{0:dd-MM-yyyy}", DateTime.Now) + ".txt";
		string pathFile = ConfigurationSettings.AppSettings["constRutaLogWS_Sans"].ToString();
            

		public SansNegocio()
		{
			objSimCards.Url = ConfigurationSettings.AppSettings["consRutaWS_Sans"].ToString();
			objSimCards.Credentials= System.Net.CredentialCache.DefaultCredentials;
			objSimCards.Timeout = int.Parse(ConfigurationSettings.AppSettings["consRutaWS_Sans_Timeout"].ToString());
		}         

		public bool Set_AnulaTelefonoPortable(string nroTelef, string usuario)
		{     
			string result = string.Empty;
			bool vExito;

			//Log
			COM_SIC_Recaudacion.SICAR_Log objLog = new COM_SIC_Recaudacion.SICAR_Log();
			string strArchivo = objLog.Log_CrearNombreArchivo(nameFile);	

			try 
			{
				objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
				objLog.Log_WriteLog(pathFile, strArchivo, "------------ " + DateTime.Now.ToShortTimeString() + " | Metodo Set_AnulaTelefonoPortable: Inicia Invocacion WS. eliminarNroTelef ------------");
				
				objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
				objLog.Log_WriteLog(pathFile, strArchivo, "Parametros del metodo");
				objLog.Log_WriteLog(pathFile, strArchivo, "- Nro Telefono: " + nroTelef);
				objLog.Log_WriteLog(pathFile, strArchivo, "- Usuario: " + usuario);

				//Se invoca al WS

				string idTransaccion = string.Empty;
				string mensajeResultado = string.Empty;
				SimCardsWS.itReturnType[] itReturn;

				result = objSimCards.eliminarNroTelef(ref idTransaccion, idAplicacion, nombreAplicacion, usuario,
					nroTelef, usuario, out mensajeResultado, out itReturn);
			
				objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
				objLog.Log_WriteLog(pathFile, strArchivo, "Datos obtenidos del WS");
				objLog.Log_WriteLog(pathFile, strArchivo, "- Resultado WS: " + result);
      
				//Valida conexion al WS
				if (result.Equals("0"))
				{
					objLog.Log_WriteLog(pathFile, strArchivo, "- ItReturn Tipo: " + itReturn[0].tipo);
					objLog.Log_WriteLog(pathFile, strArchivo, "- ItReturn Mensaje: " + itReturn[0].mensaje);
					objLog.Log_WriteLog(pathFile, strArchivo, "- ItReturn Lineas: " + itReturn[0].lineas);

					//Valida resultado de la operacion
					if(itReturn[0].tipo.Equals("0"))
					{
						vExito = true;				
					}
					else 
					{
						vExito = false;	
					}
				}
				else
				{
					objLog.Log_WriteLog(pathFile, strArchivo, "- Mensaje Resultado: " + mensajeResultado);	
					vExito = false;	
				}			
                
			}
			catch (Exception ex)
			{
				vExito = false;						
				objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
				objLog.Log_WriteLog(pathFile, strArchivo, "Error WS. eliminarNroTelef: " + ex.Message);
				objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
				objLog.Log_WriteLog(pathFile, strArchivo, "Error WS. eliminarNroTelef: " + ex.StackTrace);
			}

			objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
			objLog.Log_WriteLog(pathFile, strArchivo, "------------ Metodo Set_AnulaTelefonoPortable: Finaliza Invocacion WS. eliminarNroTelef ------------");

			return vExito;
            
		}

		
		public DataSet Get_VentaxTelefono(string strMaterial, string strTelefono, string strSerie, string usuarioAplicacion)
		{
			DataSet dsReturn = null;
			//Log
			COM_SIC_Recaudacion.SICAR_Log objLog = new COM_SIC_Recaudacion.SICAR_Log();
			string strArchivo = objLog.Log_CrearNombreArchivo(nameFile);

			objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
			objLog.Log_WriteLog(pathFile, strArchivo, "------------ " + DateTime.Now.ToShortTimeString() + " | Metodo Get_VentaxTelefono: Inicia Invocacion WS. obtenerDatosNroTelef ------------");
            			
			objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
			objLog.Log_WriteLog(pathFile, strArchivo, "Parametros del metodo");
			objLog.Log_WriteLog(pathFile, strArchivo, "- Material: " + strMaterial);
			objLog.Log_WriteLog(pathFile, strArchivo, "- Telefono: " + strTelefono);
			objLog.Log_WriteLog(pathFile, strArchivo, "- Serie: " + strSerie);

			try
			{
				//Se invoca al WS
				string idTransaccion = string.Empty;
				string mensajeResultado = string.Empty;
				SimCardsWS.itReturnType[] itReturn;
				SimCardsWS.nroSimcardsDataType[] nroSimcardsDataType;							

				string ws_nroTelefono = string.Empty;
				string ws_material = string.Empty;
				string ws_serie = string.Empty;
				string ws_materialAntiguo = string.Empty;
				bool obtieneDatosWS = true;

				//Se valida el material
				if (strMaterial.StartsWith("PS"))
				{
					string result = objSimCards.obtenerDatosNroTelef(ref idTransaccion, idAplicacion, nombreAplicacion, usuarioAplicacion,
						strTelefono, strMaterial, strSerie, out mensajeResultado, out nroSimcardsDataType, out itReturn);

					int cantReg = nroSimcardsDataType.Length;

					objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
					objLog.Log_WriteLog(pathFile, strArchivo, "Datos obtenidos del WS");
					objLog.Log_WriteLog(pathFile, strArchivo, "- Cantidad de Registro: " + cantReg.ToString());

					if (cantReg > 0)
					{
						ws_nroTelefono = nroSimcardsDataType[0].nroTelef;
						ws_material = nroSimcardsDataType[0].matNr;
						ws_serie = nroSimcardsDataType[0].serNr;
						ws_materialAntiguo = nroSimcardsDataType[0].matNrAntig;					
						
						objLog.Log_WriteLog(pathFile, strArchivo, "- WS Nro telefono: " + ws_nroTelefono);
						objLog.Log_WriteLog(pathFile, strArchivo, "- WS Material: " + ws_material);
						objLog.Log_WriteLog(pathFile, strArchivo, "- WS Serie: " + ws_serie);
						objLog.Log_WriteLog(pathFile, strArchivo, "- WS Material Antiguo: " + ws_materialAntiguo);
					}
					else 
					{	
						dsReturn = null;
						obtieneDatosWS = false;
						objLog.Log_WriteLog(pathFile, strArchivo, "***** El WS no devuelve datos. Por lo cual no se consultará SAP.");
					}

					objLog.Log_WriteLog(pathFile, strArchivo, "- Mensaje Resultado: " + mensajeResultado);
					objLog.Log_WriteLog(pathFile, strArchivo, "- Resultado WS: " + result);
					objLog.Log_WriteLog(pathFile, strArchivo, "- Mensaje ItReturn:  " + itReturn[0].mensaje);				
					objLog.Log_WriteLog(pathFile, strArchivo, "- Tipo ItReturn:  " + itReturn[0].tipo);
				}
				else
				{
					ws_serie = strSerie;	

					objLog.Log_WriteLog(pathFile, strArchivo, "***** El material no es chip (PS). Por lo cual se consultará SAP con los parametros de entrada.");
					objLog.Log_WriteLog(pathFile, strArchivo, "** Serie: " + ws_serie);
				}

				if (obtieneDatosWS)
				{
					//Metodo SAP
					SAP_SIC_Recaudacion.clsRecaudacion objRecaudacion = new SAP_SIC_Recaudacion.clsRecaudacion();
					dsReturn =  objRecaudacion.Get_VentaxTelefonoZ(strMaterial, ws_materialAntiguo, strTelefono, ws_serie);
				}

				objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
				objLog.Log_WriteLog(pathFile, strArchivo, "Datos de salida del SAP");
				objLog.Log_WriteLog(pathFile, strArchivo, "- Cantidad de Registro: " + dsReturn.Tables[0].Rows.Count);

			}
			catch (Exception ex)
			{
				dsReturn = null;
				objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
				objLog.Log_WriteLog(pathFile, strArchivo, "Error WS. obtenerDatosNroTelef: " + ex.Message);
				objLog.Log_WriteLog(pathFile, strArchivo, "Error WS. obtenerDatosNroTelef: " + ex.StackTrace);
			}      
            
			objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
			objLog.Log_WriteLog(pathFile, strArchivo, "------------ Metodo Get_VentaxTelefono: Finaliza Invocacion WS. obtenerDatosNroTelef ------------");

			return dsReturn;      
            
		}

		
		public DataSet Get_DatosCuotas(string strNroTelf,out string strNombre,out string strNroDoc, out string strCodSAP, out decimal SubTot,out decimal IGV, out decimal Total, string strMaterial, string strSerie, string usuarioAplicacion)
		{

			DataSet dsReturn = null;
			//Log
			COM_SIC_Recaudacion.SICAR_Log objLog = new COM_SIC_Recaudacion.SICAR_Log();
			string strArchivo = objLog.Log_CrearNombreArchivo(nameFile);

			objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
			objLog.Log_WriteLog(pathFile, strArchivo, "------------ " + DateTime.Now.ToShortTimeString() + " | Metodo Get_DatosCuotas: Inicia Invocacion WS. obtenerDatosNroTelef ------------");
            
			objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
			objLog.Log_WriteLog(pathFile, strArchivo, "Parametros del metodo");
			objLog.Log_WriteLog(pathFile, strArchivo, "- Material: " + strMaterial);
			objLog.Log_WriteLog(pathFile, strArchivo, "- Telefono: " + strNroTelf);
			objLog.Log_WriteLog(pathFile, strArchivo, "- Serie: " + strSerie);

			string ws_nroTelefono = string.Empty;
			string ws_material = string.Empty;
			string ws_serie = string.Empty;
			string ws_materialAntiguo = string.Empty;
			bool obtieneDatosWS = true;

			try 
			{
				string idTransaccion = string.Empty;   
				string mensajeResultado = string.Empty;
				SimCardsWS.itReturnType[] itReturn;
				SimCardsWS.nroSimcardsDataType[] nroSimcardsDataType;

				//Se valida el material
				if (strMaterial.StartsWith("PS"))
				{
					string result = objSimCards.obtenerDatosNroTelef(ref idTransaccion, idAplicacion, nombreAplicacion, usuarioAplicacion, strNroTelf, strMaterial, strSerie, out mensajeResultado, out nroSimcardsDataType, out itReturn);
            
					int cantReg = nroSimcardsDataType.Length;

					objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
					objLog.Log_WriteLog(pathFile, strArchivo, "Datos obtenidos del WS");
					objLog.Log_WriteLog(pathFile, strArchivo, "- Cantidad de Registro: " + cantReg.ToString());

					if (cantReg > 0) 
					{
						ws_nroTelefono = nroSimcardsDataType[0].nroTelef;
						ws_material = nroSimcardsDataType[0].matNr;
						ws_serie = nroSimcardsDataType[0].serNr;
						ws_materialAntiguo = nroSimcardsDataType[0].matNrAntig;					
					
						objLog.Log_WriteLog(pathFile, strArchivo, "- WS Nro telefono: " + ws_nroTelefono);
						objLog.Log_WriteLog(pathFile, strArchivo, "- WS Material: " + ws_material);
						objLog.Log_WriteLog(pathFile, strArchivo, "- WS Serie: " + ws_serie);
						objLog.Log_WriteLog(pathFile, strArchivo, "- WS Material Antiguo: " + ws_materialAntiguo);
					}
					else 
					{	
						obtieneDatosWS = false;
						objLog.Log_WriteLog(pathFile, strArchivo, "***** El WS no devuelve datos. Por lo cual no se consultará SAP!!");
						
					}

					objLog.Log_WriteLog(pathFile, strArchivo, "- Mensaje Resultado: " + mensajeResultado);
					objLog.Log_WriteLog(pathFile, strArchivo, "- Resultado WS: " + result);
					objLog.Log_WriteLog(pathFile, strArchivo, "- Mensaje ItReturn:  " + itReturn[0].mensaje);
				}
				else
				{
					ws_serie = strSerie;	

					objLog.Log_WriteLog(pathFile, strArchivo, "***** El material no es chip (PS). Por lo cual se consultará SAP con los parametros de entrada.");
					objLog.Log_WriteLog(pathFile, strArchivo, "** Serie: " + ws_serie);
				}
                
			}
			catch (Exception ex)
			{
				obtieneDatosWS = false;

				objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
				objLog.Log_WriteLog(pathFile, strArchivo, "Error WS. obtenerDatosNroTelef: " + ex.Message);
				objLog.Log_WriteLog(pathFile, strArchivo, "Error WS. obtenerDatosNroTelef: " + ex.StackTrace);
			}                  
	
			SubTot = 0;
			IGV = 0;
			Total = 0;
			SubTot = 0;
			strNombre = string.Empty;
			strNroDoc = string.Empty;
			strCodSAP = string.Empty;

			if (obtieneDatosWS)
			{
				//Metodo SAP
				SAP_SIC_Recaudacion.clsRecaudacion objRecaudacion = new SAP_SIC_Recaudacion.clsRecaudacion();
				dsReturn = objRecaudacion.Get_DatosCuotas_ZS(strNroTelf, out strNombre,out strNroDoc, out strCodSAP, 
					out SubTot,out IGV, out Total, strMaterial, ws_materialAntiguo, ws_serie);

				objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
				objLog.Log_WriteLog(pathFile, strArchivo, "Datos de salida del SAP");
				objLog.Log_WriteLog(pathFile, strArchivo, "- Cantidad de Registro: " + dsReturn.Tables[0].Rows.Count);			
				objLog.Log_WriteLog(pathFile, strArchivo, "- Nombre OUT: " + strNombre);
				objLog.Log_WriteLog(pathFile, strArchivo, "- Nro. Doc OUT: " + strNroDoc);
				objLog.Log_WriteLog(pathFile, strArchivo, "- strCodSAP OUT: " + strCodSAP);
				objLog.Log_WriteLog(pathFile, strArchivo, "- SubTot OUT: " + SubTot.ToString());
				objLog.Log_WriteLog(pathFile, strArchivo, "- IGV OUT: " + IGV.ToString());
				objLog.Log_WriteLog(pathFile, strArchivo, "- Total OUT: " + Total.ToString());
			}

			objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
			objLog.Log_WriteLog(pathFile, strArchivo, "------------ Metodo Get_DatosCuotas: Finaliza Invocacion WS. obtenerDatosNroTelef ------------");

			return dsReturn;            
		}

		
		public DataSet Get_ConsultaPrecio(string Oficina, string DocumentoOrigen, string Consecutivo, 
			string Material, string Utilizacion, decimal Cantidad,
			string Fecha, string Serie, string NroTelefono, string TipDocVenta,
			string strCadenaSeries, string Canal, string OrgVnt, out decimal Descuento,out decimal PrecIncIGV,
			out decimal Precio, out decimal SubTotal, string usuarioAplicacion)
		{            
			//Log
			COM_SIC_Recaudacion.SICAR_Log objLog = new COM_SIC_Recaudacion.SICAR_Log();
			string strArchivo = objLog.Log_CrearNombreArchivo(nameFile);

			objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
			objLog.Log_WriteLog(pathFile, strArchivo, "------------ " + DateTime.Now.ToShortTimeString() + " | Metodo Get_ConsultaPrecio: Inicia Invocacion WS. obtenerDatosNroTelef ------------");
            
			objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
			objLog.Log_WriteLog(pathFile, strArchivo, "Parametros del metodo");
			objLog.Log_WriteLog(pathFile, strArchivo, "- Material: " + Material);
			objLog.Log_WriteLog(pathFile, strArchivo, "- Telefono: " + NroTelefono);
			objLog.Log_WriteLog(pathFile, strArchivo, "- Serie: " + Serie);
			objLog.Log_WriteLog(pathFile, strArchivo, "- Oficina: " + Oficina);
			objLog.Log_WriteLog(pathFile, strArchivo, "- DocumentoOrigen: " + DocumentoOrigen);
			objLog.Log_WriteLog(pathFile, strArchivo, "- Consecutivo: " + Consecutivo);
			objLog.Log_WriteLog(pathFile, strArchivo, "- Utilizacion: " + Utilizacion);
			objLog.Log_WriteLog(pathFile, strArchivo, "- Cantidad: " + Cantidad.ToString());
			objLog.Log_WriteLog(pathFile, strArchivo, "- Fecha: " + Fecha);
			objLog.Log_WriteLog(pathFile, strArchivo, "- Tipo Doc Venta: " + TipDocVenta);
			objLog.Log_WriteLog(pathFile, strArchivo, "- Cadena Series: " + strCadenaSeries);
			objLog.Log_WriteLog(pathFile, strArchivo, "- Canal: " + Canal);
			objLog.Log_WriteLog(pathFile, strArchivo, "- OrgVnt: " + OrgVnt);

			string ws_nroTelefono = string.Empty;
			string ws_material = string.Empty;
			string ws_serie = string.Empty;
			string ws_materialAntiguo = string.Empty;
			bool obtieneDatosWS = true;

			DataSet dsReturn = null;

			try 
			{
				//Se valida el material
				if (Material.StartsWith("PS"))
				{
					string idTransaccion = string.Empty;            

					string mensajeResultado = string.Empty;
					SimCardsWS.itReturnType[] itReturn;
					SimCardsWS.nroSimcardsDataType[] nroSimcardsDataType;

					string result = objSimCards.obtenerDatosNroTelef(ref idTransaccion, idAplicacion, nombreAplicacion, usuarioAplicacion,
						NroTelefono, Material, Serie, out mensajeResultado, out nroSimcardsDataType, out itReturn);

					int cantReg = nroSimcardsDataType.Length;

					objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
					objLog.Log_WriteLog(pathFile, strArchivo, "Datos obtenidos del WS");
					objLog.Log_WriteLog(pathFile, strArchivo, "- Cantidad de Registro: " + cantReg.ToString());

					if (cantReg > 0) 
					{
						ws_nroTelefono = nroSimcardsDataType[0].nroTelef;
						ws_material = nroSimcardsDataType[0].matNr;
						ws_serie = nroSimcardsDataType[0].serNr;
						ws_materialAntiguo = nroSimcardsDataType[0].matNrAntig;
					
						objLog.Log_WriteLog(pathFile, strArchivo, "- WS Nro telefono: " + ws_nroTelefono);
						objLog.Log_WriteLog(pathFile, strArchivo, "- WS Material: " + ws_material);
						objLog.Log_WriteLog(pathFile, strArchivo, "- WS Serie: " + ws_serie);
						objLog.Log_WriteLog(pathFile, strArchivo, "- WS Material Antiguo: " + ws_materialAntiguo);
					}
					else 
					{	
						dsReturn = null;
						obtieneDatosWS = false;
						objLog.Log_WriteLog(pathFile, strArchivo, "***** El WS no devuelve datos. Por lo cual no se consultará SAP.");
					}
					objLog.Log_WriteLog(pathFile, strArchivo, "- Mensaje Resultado: " + mensajeResultado);
					objLog.Log_WriteLog(pathFile, strArchivo, "- Resultado WS: " + result);
					objLog.Log_WriteLog(pathFile, strArchivo, "- Mensaje ItReturn:  " + itReturn[0].mensaje);
				}
				else
				{
					ws_serie = Serie;	

					objLog.Log_WriteLog(pathFile, strArchivo, "***** El material no es chip (PS). Por lo cual se consultará el precio con los parametros de entrada.");					
					objLog.Log_WriteLog(pathFile, strArchivo, "** Serie: " + ws_serie);
				}

			}
			catch (Exception ex)
			{
				dsReturn = null;
                obtieneDatosWS = false;

				objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
				objLog.Log_WriteLog(pathFile, strArchivo, "Error WS. obtenerDatosNroTelef: " + ex.Message);
				objLog.Log_WriteLog(pathFile, strArchivo, "Error WS. obtenerDatosNroTelef: " + ex.StackTrace);
			}      

			Descuento = 0;
			PrecIncIGV = 0;
			Precio = 0;
			SubTotal = 0;
            
			if (obtieneDatosWS) 
			{
				//Metodo SAP
				SAP_SIC_Ventas.clsVentas objVentas = new SAP_SIC_Ventas.clsVentas();
				dsReturn = objVentas.Get_ConsultaPrecio(Oficina, DocumentoOrigen, Consecutivo, 
					Material, Utilizacion, Cantidad, Fecha, ws_serie, NroTelefono, TipDocVenta,
					strCadenaSeries, Canal, OrgVnt, out Descuento,out PrecIncIGV, out Precio, out SubTotal);

				objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
				objLog.Log_WriteLog(pathFile, strArchivo, "Datos de salida del SAP");
				objLog.Log_WriteLog(pathFile, strArchivo, "- Cantidad de Registro: " + dsReturn.Tables[0].Rows.Count);			
				objLog.Log_WriteLog(pathFile, strArchivo, "- Descuento OUT: " + Descuento.ToString());
				objLog.Log_WriteLog(pathFile, strArchivo, "- PrecIncIGV OUT: " + PrecIncIGV.ToString());
				objLog.Log_WriteLog(pathFile, strArchivo, "- Precio OUT: " + Precio.ToString());
				objLog.Log_WriteLog(pathFile, strArchivo, "- SubTotal OUT: " + SubTotal.ToString());

			}

			objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
			objLog.Log_WriteLog(pathFile, strArchivo, "------------ Metodo Get_ConsultaPrecio: Finaliza Invocacion WS. obtenerDatosNroTelef ------------");

			return dsReturn;            
		}

		
		public string ConsultarIccid(string Clase_Venta, string Nro_Telefono, string Tipo_Venta, string Material, string Serie, string usuarioAplicacion, string claseMaterial)
		{
			//Log
			COM_SIC_Recaudacion.SICAR_Log objLog = new COM_SIC_Recaudacion.SICAR_Log();
			string strArchivo = objLog.Log_CrearNombreArchivo(nameFile);

			objLog.Log_WriteLog(pathFile, strArchivo,Nro_Telefono+": "+System.Environment.NewLine);
			objLog.Log_WriteLog(pathFile, strArchivo,Nro_Telefono+": "+"------------ " + DateTime.Now.ToShortTimeString() + " | Metodo ConsultarIccid: Inicia Invocacion WS. obtenerDatosNroTelef ------------");
            
			objLog.Log_WriteLog(pathFile, strArchivo,Nro_Telefono+": "+  System.Environment.NewLine);
			objLog.Log_WriteLog(pathFile, strArchivo,Nro_Telefono+": "+"Parametros del metodo");
			objLog.Log_WriteLog(pathFile, strArchivo,Nro_Telefono+": "+ "- Material: " + Material);
			objLog.Log_WriteLog(pathFile, strArchivo,Nro_Telefono+": "+ "- Telefono: " + Nro_Telefono);
			objLog.Log_WriteLog(pathFile, strArchivo,Nro_Telefono+": "+ "- Serie: " + Serie);
			objLog.Log_WriteLog(pathFile, strArchivo, Nro_Telefono+": "+"- Clase Venta: " + Clase_Venta);

			string ws_nroTelefono = string.Empty;
			string ws_material = string.Empty;
			string ws_serie = string.Empty;
			string ws_materialAntiguo = string.Empty;
			string ws_fechacambio = string.Empty;
			bool obtieneDatosWS = true;

			string resultSAP = string.Empty;
            
			try 
			{
				if (claseMaterial == "PS")
				{
					string idTransaccion = string.Empty;   
					string mensajeResultado = string.Empty;
					SimCardsWS.itReturnType[] itReturn;
					SimCardsWS.nroSimcardsDataType[] nroSimcardsDataType;
					string result = objSimCards.obtenerDatosNroTelef(ref idTransaccion, idAplicacion, nombreAplicacion, usuarioAplicacion,
						Nro_Telefono, Material, Serie, out mensajeResultado, out nroSimcardsDataType, out itReturn);

					int cantReg = nroSimcardsDataType.Length;

					objLog.Log_WriteLog(pathFile, strArchivo,Nro_Telefono+": "+ System.Environment.NewLine);
					objLog.Log_WriteLog(pathFile, strArchivo,Nro_Telefono+": "+ "Datos obtenidos del WS");
					objLog.Log_WriteLog(pathFile, strArchivo,Nro_Telefono+": "+ "- Cantidad de Registro: " + cantReg.ToString());

					if (cantReg > 0) 
					{
						ws_nroTelefono = nroSimcardsDataType[0].nroTelef;
						ws_material = nroSimcardsDataType[0].matNr;
						ws_serie = Funciones.CheckStr(nroSimcardsDataType[0].serNr);
						ws_materialAntiguo = nroSimcardsDataType[0].matNrAntig;
						ws_fechacambio = nroSimcardsDataType[0].fecCambio.ToShortDateString();

						resultSAP = ws_serie;

						objLog.Log_WriteLog(pathFile, strArchivo,Nro_Telefono+": "+ "- WS Nro telefono: " + ws_nroTelefono);
						objLog.Log_WriteLog(pathFile, strArchivo,Nro_Telefono+": "+ "- WS Material: " + ws_material);
						objLog.Log_WriteLog(pathFile, strArchivo,Nro_Telefono+": "+ "- WS Serie: " + ws_serie);
						objLog.Log_WriteLog(pathFile, strArchivo,Nro_Telefono+": "+ "- WS Material Antiguo: " + ws_materialAntiguo);
						objLog.Log_WriteLog(pathFile, strArchivo,Nro_Telefono+": "+ "- WS Fecha Cambio: " + ws_fechacambio);
					}
					else 
					{	
						obtieneDatosWS = false;
						//objLog.Log_WriteLog(pathFile, strArchivo, "***** El WS no devuelve datos. Por lo cual no se consultará SAP.");
						objLog.Log_WriteLog(pathFile, strArchivo,Nro_Telefono+": "+ "***** El WS no devuelve datos.");
					}

					objLog.Log_WriteLog(pathFile, strArchivo,Nro_Telefono+": "+ "- Mensaje Resultado: " + mensajeResultado);
					objLog.Log_WriteLog(pathFile, strArchivo,Nro_Telefono+": "+ "- Resultado WS: " + result);
					objLog.Log_WriteLog(pathFile, strArchivo,Nro_Telefono+": "+ "- ItReturn Mensaje:  " + itReturn[0].mensaje);
					objLog.Log_WriteLog(pathFile, strArchivo,Nro_Telefono+": "+ "- ItReturn Tipo:  " + itReturn[0].tipo);
				}
				else
				{
					ws_serie = Serie;	
					resultSAP = ws_serie;
					//objLog.Log_WriteLog(pathFile, strArchivo, "***** El material no es chip (PS). Por lo cual se consultará SAP con los parametros de entrada.");					
					objLog.Log_WriteLog(pathFile, strArchivo,Nro_Telefono+": "+ "***** EL valor del material esta vacio");	

					objLog.Log_WriteLog(pathFile, strArchivo,Nro_Telefono+": "+ "** Material: " + Material);
					objLog.Log_WriteLog(pathFile, strArchivo,Nro_Telefono+": "+ "** Serie: " + ws_serie);
					objLog.Log_WriteLog(pathFile, strArchivo,Nro_Telefono+": "+ "** resultSAP: " + resultSAP);
				}

				if (obtieneDatosWS)
				{
					//Metodo SAP
					//					SAP_SIC_Pagos.clsPagos objPagos = new SAP_SIC_Pagos.clsPagos();
					//					resultSAP = objPagos.ConsultarIccid_Zs(Clase_Venta, Nro_Telefono, Tipo_Venta, ws_fechacambio, Material, ws_materialAntiguo, ws_serie);

					//					objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
					//					objLog.Log_WriteLog(pathFile, strArchivo, "Datos de salida del SAP");
					//objLog.Log_WriteLog(pathFile, strArchivo, "- Imei: " + resultSAP);
				}
			}
			catch (Exception ex)
			{
				objLog.Log_WriteLog(pathFile, strArchivo,Nro_Telefono+": "+ System.Environment.NewLine);
				objLog.Log_WriteLog(pathFile, strArchivo,Nro_Telefono+": "+ "Error WS. obtenerDatosNroTelef: " + ex.Message);
				objLog.Log_WriteLog(pathFile, strArchivo,Nro_Telefono+": "+ "Error WS. obtenerDatosNroTelef: " + ex.StackTrace);
			}      
            
			objLog.Log_WriteLog(pathFile, strArchivo,Nro_Telefono+": "+ System.Environment.NewLine);
			objLog.Log_WriteLog(pathFile, strArchivo,Nro_Telefono+": "+ "** Variable de retorno (resultSAP) : " + resultSAP);
			objLog.Log_WriteLog(pathFile, strArchivo,Nro_Telefono+": "+ "------------ Metodo ConsultarIccid: Finaliza Invocacion WS. obtenerDatosNroTelef ------------");

			return resultSAP;
		}

		
		public DataSet Get_ConsultaIMEITelf(string OficinaVenta, string codMaterial,
			string NroResultados, string NroTelefono, string usuarioAplicacion)
		{
          
			//Log
			COM_SIC_Recaudacion.SICAR_Log objLog = new COM_SIC_Recaudacion.SICAR_Log();
			string strArchivo = objLog.Log_CrearNombreArchivo(nameFile);

			objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
			objLog.Log_WriteLog(pathFile, strArchivo, "------------ " + DateTime.Now.ToShortTimeString() + " | Metodo Get_ConsultaIMEITelf: Inicia Invocacion WS. obtenerNroTelef ------------");

			objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
			objLog.Log_WriteLog(pathFile, strArchivo, "Parametros del metodo");
			objLog.Log_WriteLog(pathFile, strArchivo, "- Oficina Venta: " + OficinaVenta);
			objLog.Log_WriteLog(pathFile, strArchivo, "- Material: " + codMaterial);
			objLog.Log_WriteLog(pathFile, strArchivo, "- NroResultados: " + NroResultados);
			objLog.Log_WriteLog(pathFile, strArchivo, "- NroTelefono: " + NroTelefono);

			//Metodo SAP
			//SAP_SIC_Ventas.clsVentas objVentas = new SAP_SIC_Ventas.clsVentas();
			//DataSet resultSAP = objVentas.Get_ConsultaIMEITelf(OficinaVenta, codMaterial, NroResultados, NroTelefono);
			
			
			//Metodo MSSAP6
			COM_SIC_Activaciones.clsConsultaMsSap objclsConsultaMsSap=  new COM_SIC_Activaciones.clsConsultaMsSap();
			DataSet resultSAP = objclsConsultaMsSap.ConsultaSerieXMaterial(OficinaVenta, "","","",codMaterial,"",ConfigurationSettings.AppSettings["tipo_oficina"]);

			DataTable dtReturn = new DataTable();		
	
            int cantRegSAP = resultSAP.Tables[0].Rows.Count;	
			
			objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
			objLog.Log_WriteLog(pathFile, strArchivo, "Resultado SAP: ");
			objLog.Log_WriteLog(pathFile, strArchivo, "- Cantidad registros: " + cantRegSAP);
            
			try 
			{
				if (cantRegSAP > 0) 
				{	
					string tipoMaterial =resultSAP.Tables[0].Rows[0]["MATEC_TIPOMATERIAL"].ToString();

					//if (codMaterial.StartsWith("PS"))
					//{
					//if (tipoMaterial.StartsWith(ConfigurationSettings.AppSettings["constChips"]))
					if (tipoMaterial.StartsWith("PSX"))
					{	
						SimCardsWS.itMatSerType[] itInputArray = new SimCardsWS.itMatSerType[cantRegSAP];
						int cont = 0;

						foreach(DataRow _item in resultSAP.Tables[0].Rows)
						{
							SimCardsWS.itMatSerType itInputItem = new SimCardsWS.itMatSerType();
							//itInputItem.material = _item["MATNR"].ToString();
							//itInputItem.nroSerie = _item["SERNR"].ToString();
							itInputItem.material = _item["MATEC_CODMATERIAL"].ToString();
							itInputItem.nroSerie = _item["SERIC_CODSERIE"].ToString();
							itInputArray[cont] = itInputItem;
							cont++;
						}

						string idTransaccion = string.Empty;				
						string mensajeResultado = string.Empty;
						SimCardsWS.itTelSerType[] itOutPut;
						SimCardsWS.itReturnType[] itReturn;

						string result = objSimCards.obtenerNroTelef(ref idTransaccion, idAplicacion, nombreAplicacion, usuarioAplicacion,
							itInputArray, out mensajeResultado, out itOutPut , out itReturn);

						objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
						objLog.Log_WriteLog(pathFile, strArchivo, "Datos obtenidos del WS");
						objLog.Log_WriteLog(pathFile, strArchivo, "- Mensaje Resultado: " + mensajeResultado);
						objLog.Log_WriteLog(pathFile, strArchivo, "- Resultado WS: " + result);

						if (itOutPut != null) 
						{							
							objLog.Log_WriteLog(pathFile, strArchivo, "- Cantidad registros WS: " + itOutPut.Length);							
							objLog.Log_WriteLog(pathFile, strArchivo, "- ItReturn Mensaje: " + itReturn[0].mensaje);
							objLog.Log_WriteLog(pathFile, strArchivo, "- ItReturn Tipo: " + itReturn[0].tipo);
							objLog.Log_WriteLog(pathFile, strArchivo, "- itOutPut.Length: " + itOutPut.Length);							

							if (itOutPut.Length > 0) 
							{
								//Se construye el dataset 					
								dtReturn = resultSAP.Tables[0].Clone();

								foreach(SimCardsWS.itTelSerType _itemOut in itOutPut)
								{
									DataRow dr = dtReturn.NewRow();
									//dr["MATNR"] = codMaterial;
									//dr["SERNR"] = _itemOut.nroSerie;
									//dr["NRO_TELEF"] = _itemOut.nroTelef;

									dr["MATEC_CODMATERIAL"] = codMaterial;
									dr["SERIC_CODSERIE"] = _itemOut.nroSerie;
									dr["NRO_TELEF"] = _itemOut.nroTelef;
									dtReturn.Rows.Add(dr);
								}
							}
							else
							{								
								//dtReturn = resultSAP.Tables[0].Clone(); //linea anterior
								dtReturn = resultSAP.Tables[0].Copy();	//pruebas
							}
						}
						else 
						{
							objLog.Log_WriteLog(pathFile, strArchivo, "No devuelve el arreglo de numeros de series con su telefono asociado.");
							dtReturn = resultSAP.Tables[0].Clone();
						}
					}
					else 
					{
						objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
						objLog.Log_WriteLog(pathFile, strArchivo, "El material no es un chip. Por lo cual, no se consulto al WS Sans. Se toman los datos devueltos por SAP");
						dtReturn = resultSAP.Tables[0].Copy();
					}
				} 
				else{			
                    dtReturn = resultSAP.Tables[0].Clone();
				}               
			}
			catch (Exception ex)
			{				
				dtReturn = resultSAP.Tables[0].Clone();
				objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
				objLog.Log_WriteLog(pathFile, strArchivo, "Error WS. obtenerNroTelef: " + ex.Message);
				objLog.Log_WriteLog(pathFile, strArchivo, "Error WS. obtenerNroTelef: " + ex.StackTrace);
			}      

			objLog.Log_WriteLog(pathFile, strArchivo, "- dtReturn filas: " + dtReturn.Rows.Count);	
            
			objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
			objLog.Log_WriteLog(pathFile, strArchivo, "------------ Metodo Get_ConsultaIMEITelf: Finaliza Invocacion WS. obtenerNroTelef ------------");

			//return ((dtReturn != null && dtReturn.Rows.Count > 0) ? dtReturn.DataSet : resultSAP) ;

			DataSet dsReturn = new DataSet();
			dsReturn.Tables.Add(dtReturn);

			return dsReturn;
			
		}
        

		//ADD_INI_PROY_24388_RESERVA_Y_ACTIVACIÓN_EN_LÍNEA_PREPAGO_CAC_Y_POSTPAGO
		public string desasociarNroTelefSerie(string strLinea, string strNroSerie, string strUsuario,out string strMsgResp,out string strItReturn)
		{
			string	strCodigoRetorno = String.Empty;
			string strCadenaItReturn = String.Empty;
			string idTransaccion = String.Empty;
			strItReturn=String.Empty;
			SimCardsWS.itReturnType[] listReturn;
			
			try
			{
				strCodigoRetorno = objSimCards.desasociarNroTelefSerie(ref idTransaccion, idAplicacion, nombreAplicacion, strUsuario,
					strLinea, strNroSerie,out strMsgResp, out listReturn);

				if (listReturn != null)
				{
					foreach (SimCardsWS.itReturnType oRespuesta in listReturn)
					{
						strItReturn = oRespuesta.campo + ";" + oRespuesta.claseMensaje + ";" + oRespuesta.codigo + ";" + oRespuesta.lineas + ";" + oRespuesta.mensaje + ";" + oRespuesta.mensajeV1 + ";" +
							oRespuesta.mensajeV1 + ";" + oRespuesta.mensajeV2 + ";" + oRespuesta.mensajeV3 + ";" + oRespuesta.mensajeV4 + ";" + oRespuesta.numeroConsecutivoInterno + ";" + oRespuesta.numeroLog + ";" +
							oRespuesta.numeroMensaje + ";" + oRespuesta.parametro + ";" + oRespuesta.sistema + ";" + oRespuesta.sistema + ";" + oRespuesta.tipo;
					}
				}
			}
			catch (Exception ex)
			{
				strItReturn = strCadenaItReturn;
				strCodigoRetorno = "1";
				strMsgResp = ex.Message;
			}
			return strCodigoRetorno;
		}
		//FIN|PROY-24388 - IDEA-31791- Reserva y Activación en línea prepago CAC y postpago
		//ADD_INI_PROY_24388_RESERVA_Y_ACTIVACIÓN_EN_LÍNEA_PREPAGO_CAC_Y_POSTPAGO
		public bool Cambiar_Status(string nroTelef, string Status, string usuario,out string strMsgResp,out string strItReturn)
		{
			bool blnOk = false;
			string codigoResultado = String.Empty;
			string idTransaccion = String.Empty;
			string strCadenaItReturn = String.Empty;
			strItReturn = String.Empty;

			try
			{
				SimCardsWS.itReturnType[] itReturn;

				codigoResultado = objSimCards.cambiarStatus(ref idTransaccion, idAplicacion, nombreAplicacion, usuario,
					nroTelef, Status, usuario,out strMsgResp, out itReturn);

				if (codigoResultado == "0")
				{
					blnOk = true;
				}

				if (itReturn != null)
				{
					foreach (SimCardsWS.itReturnType oRespuesta in itReturn)
					{
						strItReturn = oRespuesta.campo + ";" + oRespuesta.claseMensaje + ";" + oRespuesta.codigo + ";" + oRespuesta.lineas + ";" + oRespuesta.mensaje + ";" + oRespuesta.mensajeV1 + ";" +
							oRespuesta.mensajeV1 + ";" + oRespuesta.mensajeV2 + ";" + oRespuesta.mensajeV3 + ";" + oRespuesta.mensajeV4 + ";" + oRespuesta.numeroConsecutivoInterno + ";" + oRespuesta.numeroLog + ";" +
							oRespuesta.numeroMensaje + ";" + oRespuesta.parametro + ";" + oRespuesta.sistema + ";" + oRespuesta.sistema + ";" + oRespuesta.tipo;
					}
				}
			}
			catch (Exception ex)
			{
				strItReturn = strCadenaItReturn;
				strMsgResp = ex.Message;
			}
			return blnOk;
		}
		//FIN|PROY-24388 - IDEA-31791- Reserva y Activación en línea prepago CAC y postpago

		//INC000000961273 - INICIO
		public bool CrearTelefonoPortable(string CodigoRed, string CodOperadorCedente, string NroTelefono, string region, string serie, string tipoNumero,
			out string mensaje, string material, string usuario)        
		{
			COM_SIC_Recaudacion.SICAR_Log objLog = new COM_SIC_Recaudacion.SICAR_Log();
			string strArchivo = objLog.Log_CrearNombreArchivo(nameFile);

			objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
			objLog.Log_WriteLog(pathFile, strArchivo, "------------ " + DateTime.Now.ToShortTimeString() + " | Metodo CrearTelefonoPortable: Inicia Invocacion WS. registroIMSIPortabilidad ------------");
			objLog.Log_WriteLog(pathFile, strArchivo, "Parametros del metodo");
			objLog.Log_WriteLog(pathFile, strArchivo, "- Codigo Red: " + CodigoRed);
			objLog.Log_WriteLog(pathFile, strArchivo, "- Cod Operador Cedente: " + CodOperadorCedente);
			objLog.Log_WriteLog(pathFile, strArchivo, "- Nro Telefono: " + NroTelefono);
			objLog.Log_WriteLog(pathFile, strArchivo, "- Region: " + region);
			objLog.Log_WriteLog(pathFile, strArchivo, "- Serie: " + serie);
			objLog.Log_WriteLog(pathFile, strArchivo, "- Tipo Numero: " + tipoNumero);
			objLog.Log_WriteLog(pathFile, strArchivo, "- Material: " + material);
			objLog.Log_WriteLog(pathFile, strArchivo, "- Usuario: " + usuario);
			
        
			string result = string.Empty;
			bool vExito = false;
			mensaje = "";

			try
			{
				string idTransaccion = string.Empty;  
				string mensajeWS = string.Empty;
				SimCardsWS.itReturnType[] itReturn;

				result = objSimCards.registroIMSIPortabilidad(ref idTransaccion, idAplicacion, nombreAplicacion, 
					usuario, NroTelefono, CodigoRed, tipoNumero, serie, CodOperadorCedente,
					region, material, usuario, out mensajeWS, out itReturn);

				objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
				objLog.Log_WriteLog(pathFile, strArchivo, "Datos obtenidos del WS");
				objLog.Log_WriteLog(pathFile, strArchivo, "- Resultado WS: " + result);
				objLog.Log_WriteLog(pathFile, strArchivo, "- Mensaje Resultado: " + mensajeWS);

				//Valida conexion al WS
				objLog.Log_WriteLog(pathFile, strArchivo, "- ItReturn Tipo: " + itReturn[0].tipo);
				objLog.Log_WriteLog(pathFile, strArchivo, "- ItReturn Mensaje: " + itReturn[0].mensaje);
				objLog.Log_WriteLog(pathFile, strArchivo, "- ItReturn Lineas: " + itReturn[0].lineas);				

				//Valida resultado de la operacion
				if(itReturn[0].tipo.Equals("0"))
				{
					vExito = true;		
				}
				else 
				{
					vExito = false;	
					mensaje = itReturn[0].mensaje;
				}

			}
			catch(Exception ex)
			{					
				vExito = false;	
				mensaje = ex.Message;
				objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
				//INI PROY-140126
				string MaptPath = "";
				MaptPath = "( Class : NEGOCIO_SIC_SANS SansNegocio.cs; Function: CrearTelefonoPortable)";
				objLog.Log_WriteLog(pathFile, strArchivo, ex.Message + MaptPath); 
				//FIN PROY-140126

				
				objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
				objLog.Log_WriteLog(pathFile, strArchivo, ex.StackTrace);
			}

			objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
			objLog.Log_WriteLog(pathFile, strArchivo, "------------ Metodo CrearTelefonoPortable: Finaliza Invocacion WS. registroIMSIPortabilidad ------------");
			
			return vExito;
		}
		//INC000000961273 - FIN

	}
}
