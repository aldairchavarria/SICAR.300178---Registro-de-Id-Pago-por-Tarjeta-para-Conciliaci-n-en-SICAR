using System;
using System.Data;
using SAP_SIC_Recaudacion;

namespace COM_SIC_Procesa_Pagos
{
	/// <summary>
	/// Summary description for Extenciones.
	/// </summary>
 public class Extenciones
    {
        public static void BindReciboFromDataRow(ZST_RECIBO_RECAUDACION recibo, DataRow row)
        {
            recibo.Nro_Transaccion = row[0].ToString();
            recibo.Posicion = row[1].ToString();
            recibo.Tipo_Doc_Recaud = row[2].ToString();
            recibo.Nro_Doc_Recaud = row[3].ToString();
            recibo.Moneda_Docum = row[4].ToString();
            recibo.Importe_Recibo = Convert.ToDecimal(row[5].ToString());
            recibo.Importe_Pagado = Convert.ToDecimal(row[6].ToString());
            recibo.Nro_Cobranza = row[7].ToString();
            recibo.Nro_Ope_Acree = row[8].ToString();
            recibo.Fecha_Emision = SAPDataConnector.FormatoFecha(row[9].ToString());
            recibo.Fecha_Pago = SAPDataConnector.FormatoFecha(row[10].ToString());
            recibo.Nro_Trace_Anul = row[11].ToString();
            recibo.Nro_Trace_Pago = row[12].ToString();
            recibo.Desc_Servicio = row[13].ToString();
            recibo.Fecha_Hora = row[14].ToString();
            recibo.Servicio = row[15].ToString();
        }

        public static void BindDeudaFromDataRow(ZST_DEUDA_RECAUDACION deuda, DataRow row)
        {
            deuda.Nro_Transaccion = row[0].ToString();
            deuda.Nom_Deudor = row[1].ToString();
            deuda.Ruc_Deudor = row[2].ToString();///SOSPECHOSO
            deuda.Oficina_Venta = row[3].ToString();
            deuda.Nom_Of_Venta = row[4].ToString();
            deuda.Mon_Pago = row[5].ToString();
            deuda.Importe_Pago = Convert.ToDecimal(row[6]);
            deuda.Fecha_Transac = SAPDataConnector.FormatoFecha(row[7].ToString());
            deuda.Hora_Transac = row[8].ToString();
            deuda.Estado_Transac = row[9].ToString();
            deuda.Nro_Telefono = row[10].ToString();
            deuda.Cod_Cajero = row[11].ToString();
            deuda.Nom_Cajero = row[12].ToString();
            deuda.Nro_Trace_Cons = row[13].ToString();
            deuda.Tipo_Doc_Deudor = row[14].ToString();
            deuda.Nro_Doc_Deudor = row[15].ToString();
        }

        public static void BindPagoFromDataRow(ZST_PAGOS_RECAUDACION pagos, DataRow row)
        {
            pagos.Nro_Transaccion = row[0].ToString();
            pagos.Posicion = row[1].ToString();///NUMERICO
            pagos.Via_Pago = row[2].ToString();
            pagos.Importe_Pagado = Convert.ToDecimal(row[3].ToString());
            pagos.Nro_Cheque = row[4].ToString();
            pagos.Belnr = row[5].ToString();
            pagos.Desc_Via_Pago = row[6].ToString();
        }


    }
}
