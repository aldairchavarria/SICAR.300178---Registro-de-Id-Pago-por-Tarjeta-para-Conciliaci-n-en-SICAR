using System;
using System.Text;

namespace COM_SIC_Procesa_Pagos.BWCobroAnticipadoInstalacion
{
	public class PagoAnticipado
	{
		private Int64 _codigo;
		private Int64 _numeroSolicitud;
		private Int64 _numeroContrato;
		private Int64 _numeroSolicitudPlan;
		private string _tipoProducto;
		private double _montoTotalInstalacion;
		private double _montoInicialInstalacion;
		private double _porcentajeMontoInicial;
		private string _tipoInicial ;
		private string _numeroDocumento;
		private string _nombreCliente ;
		private string _fechaProgramacion1 ;
		private string _franjaHoraria1 ;
		private string _fechaProgramacion2 ;
		private string _franjaHoraria2 ;
		private string _fechaProgramacion3 ;
		private string _franjaHoraria3 ;
		private string _estado ;
		private string _fechaRegistro;
		private string _usuarioRegistro;
		private string _fechaActualizacion ;
		private string _usuarioActualizacion ;
		private double _transaccionPago ;
		private string _fechaTransaccionPago ;
		private string _medioPago ;
		private string _publicarNumero;
		private string _correoClienteClaro;

		public Int64 codigo
		{
			set{_codigo = value;}
			get{return _codigo;}
		}

		public Int64 numeroSolicitud
		{
			set{_numeroSolicitud = value;}
			get{return _numeroSolicitud;}
		}

		public Int64 numeroContrato
		{
			set{_numeroContrato = value;}
			get{return _numeroContrato;}
		}

		public Int64 numeroSolicitudPlan
		{
			set{_numeroSolicitudPlan = value;}
			get{return _numeroSolicitudPlan;}
		}

		public string tipoProducto
		{
			set{_tipoProducto = value;}
			get{return _tipoProducto;}
		}

		public double montoTotalInstalacion
		{
			set{_montoTotalInstalacion = value;}
			get{return _montoTotalInstalacion;}
		}

		public double montoInicialInstalacion
		{
			set{_montoInicialInstalacion = value;}
			get{return _montoInicialInstalacion;}
		}

		public double porcentajeMontoInicial
		{
			set{_porcentajeMontoInicial = value;}
			get{return _porcentajeMontoInicial;}
		}

		public string tipoInicial
		{
			set{_tipoInicial = value;}
			get{return _tipoInicial;}
		}

		public string numeroDocumento
		{
			set{_numeroDocumento = value;}
			get{return _numeroDocumento;}
		}

		public string nombreCliente
		{
			set{_nombreCliente = value;}
			get{return _nombreCliente;}
		}

		public string fechaProgramacion1
		{
			set{_fechaProgramacion1 = value;}
			get{return _fechaProgramacion1;}
		}

		public string franjaHoraria1
		{
			set{_franjaHoraria1 = value;}
			get{return _franjaHoraria1;}
		}

		public string fechaProgramacion2
		{
			set{_fechaProgramacion2 = value;}
			get{return _fechaProgramacion2;}
		}

		public string franjaHoraria2
		{
			set{_franjaHoraria2 = value;}
			get{return _franjaHoraria2;}
		}

		public string fechaProgramacion3
		{
			set{_fechaProgramacion3 = value;}
			get{return _fechaProgramacion3;}
		}

		public string franjaHoraria3
		{
			set{_franjaHoraria3 = value;}
			get{return _franjaHoraria3;}
		}

		public string estado
		{
			set{_estado = value;}
			get{return _estado;}
		}

		public string fechaRegistro
		{
			set{_fechaRegistro = value;}
			get{return _fechaRegistro;}
		}

		public string usuarioRegistro
		{
			set{_usuarioRegistro = value;}
			get{return _usuarioRegistro;}
		}

		public string fechaActualizacion
		{
			set{_fechaActualizacion = value;}
			get{return _fechaActualizacion;}
		}

		public string usuarioActualizacion
		{
			set{_usuarioActualizacion = value;}
			get{return _usuarioActualizacion;}
		}

		public double transaccionPago
		{
			set{_transaccionPago = value;}
			get{return _transaccionPago;}
		}

		public string fechaTransaccionPago
		{
			set{_fechaTransaccionPago = value;}
			get{return _fechaTransaccionPago;}
		}

		public string medioPago
		{
			set{_medioPago = value;}
			get{return _medioPago;}
		}

		public string publicarNumero
		{
			set{_publicarNumero = value;}
			get{return _publicarNumero;}
		}

		public string correoClienteClaro
		{
			set{_correoClienteClaro = value;}
			get{return _correoClienteClaro;}
		}
		

	}
}
