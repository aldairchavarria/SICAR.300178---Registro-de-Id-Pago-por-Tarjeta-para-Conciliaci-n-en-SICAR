using System;

namespace COM_SIC_Entidades.DataPowerRest.MethodsRest.GeneraFacturaElectronica
{
	/// <summary>
	/// Summary description for BodyResponse.
	/// </summary>
	public class BodyResponse
	{
		private string _codigo;
		private string _mensaje;


		public BodyResponse()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public string codigo
		{
			set{_codigo= value;}
			get{ return _codigo;}
		}
		public string mensaje
		{
			set{_mensaje= value;}
			get{ return _mensaje;}
		}
	}
}
