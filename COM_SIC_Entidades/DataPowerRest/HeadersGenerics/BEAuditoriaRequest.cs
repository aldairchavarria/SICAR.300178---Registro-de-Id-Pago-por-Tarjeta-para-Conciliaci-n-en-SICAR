using System;
using System.Text;

namespace COM_SIC_Entidades.DataPowerRest.HeadersGenerics
{
	[Serializable]
	public class BEAuditoriaRequest
	{
		public BEAuditoriaRequest()
		{
		}

		private string _canal;
		private string _idAplicacion;
		private string _usuarioAplicacion;
		private string _usuarioSesion;
		private string _idTransaccionESB;
		private string _idTransaccionNegocio;
		private string _fechaInicio;
		private string _nodoAdicional;
		private string _timestamp;
		private string _idTransaccion;
		private string _userId;
		private string _nameRegEdit;
		private string _applicationCodeWS;
		private string _applicationCode;
		private string _ipApplication;
		private string _accept;
private string _msgId;

		public string canal { get { return _canal; } set { _canal = value; } }
		public string idAplicacion { get { return _idAplicacion; } set { _idAplicacion = value; } }
		public string usuarioAplicacion { get { return _usuarioAplicacion; } set { _usuarioAplicacion = value; } }
		public string usuarioSesion { get { return _usuarioSesion; } set { _usuarioSesion = value; } }
		public string idTransaccionESB { get { return _idTransaccionESB; } set { _idTransaccionESB = value; } }
		public string idTransaccionNegocio { get { return _idTransaccionNegocio; } set { _idTransaccionNegocio = value; } }
		public string fechaInicio { get { return _fechaInicio; } set { _fechaInicio = value; } }
		public string nodoAdicional { get { return _nodoAdicional; } set { _nodoAdicional = value; } }
		public string timestamp { get { return _timestamp; } set { _timestamp = value; } }
		public string idTransaccion { get { return _idTransaccion; } set { _idTransaccion = value; } }
		public string userId { get { return _userId; } set { _userId = value; } }
		public string nameRegEdit { get { return _nameRegEdit; } set { _nameRegEdit = value; } }
		public string applicationCodeWS { get { return _applicationCodeWS; } set { _applicationCodeWS = value; } }
		public string applicationCode { get { return _applicationCode; } set { _applicationCode = value; } }
		public string ipApplication { get { return _ipApplication; } set { _ipApplication = value; } }
		public string accept { get { return _accept; } set { _accept = value; } }
		public string msgId { get { return _msgId; } set { _msgId = value; } }

	}
}
