<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SICAR_POSnoFinancieras.aspx.vb" Inherits="SisCajas.SICAR_POSnoFinancieras" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>SICAR_POSnoFinancieras</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="styleSheet" type="text/css" href="../estilos/est_General.css">
		<script type="text/javascript" src="../librerias/Lib_FuncGenerales.js"></script>
		<!--PROY-27440 INI-->
		<script language="JavaScript" src="../librerias/msrsclient.js"></script>
		<script language="JavaScript" src="../Scripts/jquery-1.1.js"></script>
		<script language="JavaScript" src="../Scripts/form.js"></script>
		<script language="JavaScript" src="../Scripts/xml2json.js"></script>
		<script language="JavaScript" src="../Scripts/operacionPOS.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<!--PROY-27440 FIN-->
		<script language="javascript">
    //PROY-27440 INI
var varArrayEstTrans;
var varMoneda = '1';
//var serverURL =  '../ProcesoPOS.aspx';
var serverURL = '../Pos/ProcesoPOS.aspx';
var webServiceURL = '<%= System.Configuration.ConfigurationSettings.AppSettings("consWsLocal_Url") %>';
var timeOutWsLocal = '<%= System.Configuration.ConfigurationSettings.AppSettings("consWsLocal_Url_TimeOut") %>';
var tiposError = 'EX_';
var rptTransaccion = true;


var sCodRespTarj;
//Mastercard
var varTransMC;
var varMonedaMC='';
var varApliMC;
var varNroRefMC;
var varPwdComercioMC = '';
//VisaNet
var varMonedaVisa;
//var varCont1 = 0;
var varTramaUpdateAux;
var varTramaAuditAux;
var varIdTransAux;
var arrTipoPOS;
var objEntityPOS = new Object(null);
var tipoPOS = '';
var NameDoc = '';
var varMontoAnular;
var strTipoTransANU = '';
var strTipoTransaccion = '';
var strTipoTransAP = '';
var varContLoop = 0;
var varBolWsLocal;
var strReferenciaVoucher = '';
var strTipoTransRIM = '';
var varArrayTipoOpera;
var varNroRef;
var varNomEquipoPOS;
var strTipoPOS = '';
/******************************/
//Parametros Envio TB Transacciones
var varTipTarjeta;
var varTransMonto = '';
var varTipoPos;
var varTipoTrans;
var varNroTarjeta;
var varNroTelefono;
var varTipoOpeFi;
var varCodOpe;
var varDescriOpe;
var varMoneda;
var varNroPedido;
var varTipoPago; //DEVOLUCIONES
var varCodOper = '';
var montoOperacion = '';
var varNroRegistro;
var varNroTienda;
var varCodEstablec;
var varCodigoCaja;
var varNomPcPos;
var varCodTerminal;
var varIpPos;
var varTipoOpe;
var varNomCliente = '';
var varNroTelefono = '';
var varNroPedido = '';
var varValueTar = '';
var varNroRefVisa = '';
var VarToday = '';
var varArrayDatoPosVisa = '';
var varArrayDatoPosMC = '';
var varTipPagONF='';
var varValidaCBO="--Seleccione--";
/******************************/
function f_ValidaSwitch(){

	alert("PAGINA NO DISPONIBLE");
	
}
function f_Enviar() {

    return f_ValidaDatos();
}

function f_ValidaDatos() {
    varArrayDatoPosVisa = document.getElementById("HidDatoPosVisa").value.split("|");
    varArrayDatoPosMC = document.getElementById("HidDatoPosMC").value.split("|");
    varTipPagONF=getValue('HidTipPagONF');
    VarToday = new Date();
    var bresult = true;
    strReferenciaVoucher = '';
    strTipoTransRIM = '';
    strTipoTransaccion = getValue('cboTipoTransaccion');
    strReferenciaVoucher = getValue('txtRefVoucher')
    strTipoPOS = getValue('cboTipPOS');
    document.getElementById("HidIdCabez").value = '';
    varNroRef = strReferenciaVoucher;
    strTipoTransANU = getValue('HidTipoTransAnu');
    strTipoTransRIM = getValue('HidTipoTransRIM');
    strTipoTransAP = getValue('HidTipoTransAP');
	
	if(strTipoPOS ==varValidaCBO ){
		alert("Seleccione Tipo POS.");
        return;
    }
	if (strTipoTransaccion == varValidaCBO ) {
        alert("Seleccione Tipo Transaccion.");
        return;
    }
    
    if (strTipoTransaccion == strTipoTransAP && strTipoPOS == '02') {
        alert("OPERACIÓN NO VÁLIDA.");
        return;
    }

    if (strTipoTransaccion == strTipoTransANU || (strTipoTransaccion == strTipoTransRIM && strTipoPOS == '02')) {
        if (strReferenciaVoucher == "" || strReferenciaVoucher == null) {
            document.getElementById("txtRefVoucher").disabled = false;
            alert("No ha ingresado el número de la factura/boleta...!!");
            bresult = false;
        } else {
            f_CargarTransacciones();
        }
    } else {
        f_EnvioPOS();
    }

    return bresult;
}
function f_Cerrar()
{
		f_limpiarForm();
	
}
function f_CambiarTipoOperacion() {
		strTipoPOS = getValue('cboTipPOS');
    var arrTipoOperacion = getValue("HidOperacionVoucher").split("|");
    var strTipoTrans = getValue("cboTipoTransaccion");
    if (arrTipoOperacion[0] == strTipoTrans || (arrTipoOperacion[1] == strTipoTrans && strTipoPOS == '02')) {
        document.getElementById("txtRefVoucher").disabled = false;

    } else {
        document.getElementById("txtRefVoucher").disabled = true;
        document.getElementById("txtRefVoucher").value = '';
    }
}

function f_EnvioPOS() {
    var varTramaInsert = '';
    var strTipoTrans = strTipoTransaccion;
    varCodOpe = '';
     varMonedaMC = '';
    varTransMonto = '';
    var varDescriOpe = '';
    varTipoTrans = '';
    varTipTarjeta = '';
    var varCodPtaWS = '';
    varArrayTipoOpera = document.getElementById("HidTipoOpera").value.split("|");
    varTipoOpe = ((strTipoTransaccion == strTipoTransANU) ? varArrayTipoOpera[0] : varArrayTipoOpera[1])
    var varArrayCodOpe = document.getElementById("HidCodOpera").value.split("|");
    var varArrayDesOpe = document.getElementById("HidDesOpera").value.split("|");
	varNroRefMC='';
    varNroRegistro = '';
    varNroTienda = '';
    varCodigoCaja = '';
    varCodEstablec = '';
    varNomPcPos = '';
    varCodTerminal = '';
    varIpPos = '';
    varArrayEstTrans = document.getElementById("HidEstTrans").value.split("|");
    f_bloquearBotones();
    document.getElementById("lblEnvioPos").innerHTML = "Enviando al POS...";
    arrTipoPOS = getValue("HidTipoPOS").split("|");
    switch (strTipoTrans) {
        case "2":
            varCodOpe = varArrayCodOpe[2]; //Anulacion
            varDescriOpe = varArrayDesOpe[2];
            varTipoTrans = strTipoTrans; //ANULACION DE PAGO
            varTransMonto = varMontoAnular;
            if (strTipoPOS == arrTipoPOS[0]) {
                var varArrayMonedaVisa = document.getElementById("HidMonedaVisa").value.split("|");
                varMonedaVisa = varArrayMonedaVisa[0]; //SOLES VISA


                if (document.getElementById("txtRefVoucher").value == '') {
                    alert('No tiene numero de referencia para eliminar la transaccion');
                    return;
                }

            } else {
                var varArrayTranMC = document.getElementById("HidTransMC").value.split("|");
                varTransMC = varArrayTranMC[1]; //06
                varPwdComercioMC = '';					
					varPwdComercioMC = varArrayTranMC[6]
               
				var varArrayMonedaMC = document.getElementById("HidMonedaMC").value.split("|");					
					varMonedaMC = varArrayMonedaMC[0];//SOLES MC
                if (document.getElementById("txtRefVoucher").value == '') {
                    alert('No tiene numero de referencia para eliminar la transaccion');
                    return;
                } else {

                    varNroRefMC = document.getElementById("txtRefVoucher").value;
                }
            }
            break;
        case "3":
            varCodOpe = ''
            varDescriOpe = '';
            varTipoTrans = strTipoTrans; //REEIMPRESION DE VOUCHER

            if (strTipoPOS == arrTipoPOS[0]) {
                var varArrayMonedaVisa = document.getElementById("HidMonedaVisa").value.split("|");
                varMonedaVisa = varArrayMonedaVisa[0]; //SOLES VISA
                varMoneda = varMonedaVisa;

            } else {
                var varArrayTranMC = document.getElementById("HidTransMC").value.split("|");
                varTransMC = varArrayTranMC[4]; //11
                varMonedaMC = '';

                if (document.getElementById("txtRefVoucher").value == '') {
                    alert('No tiene numero de referencia para re-imprimir la transaccion');
                    return;
                } else {

                    varNroRefMC = getValue("txtRefVoucher");
                }
            }
            break;
        case "4":
            varCodOpe = ''
            varDescriOpe = '';
            varTipoTrans = strTipoTrans; //REEIMPRESION DE VOUCHER

            if (strTipoPOS == arrTipoPOS[1]) {
                var varArrayTranMC = document.getElementById("HidTransMC").value.split("|");
                varTransMC = varArrayTranMC[2]; //09//reporte detallado
                varMonedaMC = '';

            }
            break;
        case "5":
            varCodOpe = ''
            varDescriOpe = '';
            varTipoTrans = strTipoTrans;

            if (strTipoPOS == arrTipoPOS[1]) {
                var varArrayTranMC = document.getElementById("HidTransMC").value.split("|");
                varTransMC = varArrayTranMC[3]; //10//reporte total
                varMonedaMC = '';

            }
            break;
        case "7":
            varCodOpe = ''
            varDescriOpe = '';
            varTipoTrans = strTipoTrans; //CIERRE DE CAJA
			
            if (strTipoPOS == arrTipoPOS[1]) {
                var varArrayTranMC = document.getElementById("HidTransMC").value.split("|");
                varTransMC = varArrayTranMC[5]; //12//cierre de caja mc
                varMonedaMC = '';

            }
            break;
    }

    varTipoTrans = strTipoTransaccion; //CIERRE
    var varNameTipoDoc;
    var varNameMonto;
    var varDesTipTarjeta = '';
    varNameTipoPOS = '';
    varNameDoc = ''
    varNameTipoDoc = '';
    varNameMonto = '';

    var EntitySaveTransac;
    var EntityUpdateTransac;
    var soapMSG
    varTipTarjeta = strTipoPOS;
    if (strTipoPOS == '01') {
        varDesTipTarjeta = "VISA"
        varValueTar = 'VIS';
        varNroRegistro = varArrayDatoPosVisa[0].substr(varArrayDatoPosVisa[0].indexOf("=") + 1);
        varNroTienda = varArrayDatoPosVisa[1].substr(varArrayDatoPosVisa[1].indexOf("=") + 1);
        varCodigoCaja = varArrayDatoPosVisa[2].substr(varArrayDatoPosVisa[2].indexOf("=") + 1);
        varCodEstablec = varArrayDatoPosVisa[3].substr(varArrayDatoPosVisa[3].indexOf("=") + 1);
        varNomPcPos = varArrayDatoPosVisa[4].substr(varArrayDatoPosVisa[4].indexOf("=") + 1);
        varCodTerminal = varArrayDatoPosVisa[6].substr(varArrayDatoPosVisa[6].indexOf("=") + 1);
        varIpPos = varArrayDatoPosVisa[7].substr(varArrayDatoPosVisa[7].indexOf("=") + 1);
    } else {
        varDesTipTarjeta = "MASTERCARD";
        varValueTar = 'MCD';
        //varTipoPos= varArrayTipoPOS[1];
        //varTipTarjeta = varArrayCodTarjeta[1];//MASTERCARD				

        varNroRegistro = varArrayDatoPosMC[0].substr(varArrayDatoPosMC[0].indexOf("=") + 1);
        varNroTienda = varArrayDatoPosMC[1].substr(varArrayDatoPosMC[1].indexOf("=") + 1);
        varCodigoCaja = varArrayDatoPosMC[2].substr(varArrayDatoPosMC[2].indexOf("=") + 1);
        varCodEstablec = varArrayDatoPosMC[3].substr(varArrayDatoPosMC[3].indexOf("=") + 1);
        varNomPcPos = varArrayDatoPosMC[4].substr(varArrayDatoPosMC[4].indexOf("=") + 1);
        varCodTerminal = varArrayDatoPosMC[6].substr(varArrayDatoPosMC[6].indexOf("=") + 1);
        varIpPos = varArrayDatoPosMC[7].substr(varArrayDatoPosMC[7].indexOf("=") + 1);

    }
    //VISA Y MASTER



    varTipoPos = varDesTipTarjeta;
    var varTipoOpeNoFi = varTipoOpe;
    var varNroPedido = '';
    varNroTelefono = '';
    varNroPedido = '';
    varTipoPago = varTipPagONF; //OPERACIONES NO FINANCIERAS
    var varEstadoTrans = varArrayEstTrans[0]; //PENDIENTE

    varNroTarjeta = '';
    varTramaInsert = '';
    varMoneda = getValue('HidTipoMoneda');
    var varIdCabecera = document.getElementById("HidIdCabez").value;
    varTramaInsert =
        'codOperacion=' + varCodOpe +
        '|desOperacion=' + varDescriOpe +
        '|tipoOperacion=' + varTipoOpe +
        '|montoOperacion=' + varTransMonto +
        '|monedaOperacion=' + varMoneda +
        '|tipoTarjeta=' + varTipTarjeta +
        '|tipoPago=' + varTipoPago +
        '|estadoTransaccion=' + varEstadoTrans +
        '|tipoPos=' + varTipoPos + '|tipoTransaccion=' + varTipoTrans + '|ipCaja=' + varIpPos +
        '|NroRegistro=' + varNroRegistro + '|NroTienda=' + varNroTienda + '|CodigoCaja=' + varCodigoCaja +
        '|CodEstablec=' + varCodEstablec + '|NomPcPos=' + varNomPcPos + '|CodTerminal=' + varCodTerminal +
        '|IdCabecera=' + varIdCabecera +
        '|nroTarjeta=' + varNroTarjeta +
        '|nroRef=' + varNroRef;


    //1 - PENDIENTE	
    RSExecute(serverURL, "GuardarTransaction", varTramaInsert, varNroTelefono, varNroPedido, CallBack_GuardarTransaction, GuardarTransactionError, "X");

}

function CallBack_GuardarTransaction(response) {

    arrTipoPOS = getValue("HidTipoPOS").split("|");


    var varTramaUpdate = '';
    var varRpta = response.return_value;
    var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n", "");
    varRpta = res;

    if (varRpta.substr(0, 1) == '0') {

        //alert(varRpta);

        var varArrayRpta = varRpta.split("|");
        var varIdTran = varArrayRpta[2];
        var varIdCabez = varArrayRpta[3];
        var varFlagPago = '1';

        document.getElementById("HidIdCabez").value = varIdCabez;

        //var varIdTran = varRpta.substr(varRpta.lastIndexOf("|")+1);

        //2 - EN PROCESO
        var varNumVoucher = '';
        var varNumAutTransaccion = '';
        var varCodRespTransaccion = '';
        var varDescTransaccion = '';
        var varCodAprobTransaccion = '';
        var varNroTarjeta = '';
        var varFechaExpiracion = '';
        var varNomCliente = '';
        var varImpVoucher = '';
        var varSeriePOS = varCodTerminal;
        var nomEquipoPOS = varNomPcPos;
        var varNroPedido = varNroRef;
        var varIdUnicoTrans = '';
        var varIdRefAnu = '';
        var tipoPago=varTipPagONF;
        varEstadoTrans = '';
        varEstadoTrans = varArrayEstTrans[1]; //EN PROCESO


        varTramaUpdate = '';
        varTramaUpdate = 'monedaOperacion=' + varMoneda + '|montoOperacion=' + varTransMonto +
            '|nroRegistro=' + varNroRegistro + '|numVoucher=' + varNumVoucher +
            '|numAutTransaccion=' + varNumAutTransaccion + '|codRespTransaccion=' + varCodRespTransaccion +
            '|descTransaccion=' + varDescTransaccion + '|codAprobTransaccion=' + varCodAprobTransaccion +
            '|tipoPos=' + varTipoPos + '|varNroTarjeta=' + varNroTarjeta +
            '|fechaExpiracion=' + varFechaExpiracion + '|nomCliente=' + varNomCliente +
            '|impVoucher=' + varImpVoucher + '|seriePOS=' + varSeriePOS +
            '|nomEquipoPOS=' + nomEquipoPOS + '|estadoTransaccion=' + varEstadoTrans +
            '|NroTienda=' + varNroTienda + '|CodigoCaja=' + varCodigoCaja +
            '|CodEstablec=' + varCodEstablec + '|IpPos=' + varIpPos + '|IdCabez=' + varIdCabez +
            '|FlagPago=' + varFlagPago + '|Pedido=' + varNroPedido + '|IdUnico=' + varIdUnicoTrans +
            '|TipoTrans=' + varTipoTrans +  '|varIdRefAnu=' + varIdRefAnu +  '|TipoPago=' + tipoPago +
            '|ResTarjetaPos=';//Proy-31949-Inicio;

        //alert(varTramaUpdate);


        RSExecute(serverURL, "ActualizarTransaction", varTramaUpdate,
            varIdTran, CallBack_ActualizarTransaction1, GuardarTransactionError, "X");


        objEntityPOS = {
            monedaOperacion: '',
            montoOperacion: varTransMonto,
            CodigoTienda: varNroTienda,
            NroPedido: '',
            ipAplicacion: '',
            nombreAplicacion: '',
            usuarioAplicacion: '',
            TrnsnId: varIdTran,
            tipoPos: '',
            CodigoCaja: varCodigoCaja
        };

        if (varValueTar == 'VIS') { //VISA
            CallService(varTipTarjeta, objEntityPOS);
        } else if (varValueTar == 'AMX' || varValueTar == 'MCD' || varValueTar == 'DIN') { //MASTERCARD
            CallService(varTipTarjeta, objEntityPOS);
        }
    } else {
        alert('Error al registrar la transaccion en estado PENDIENTE');
        //f_limpiarForm();
        document.getElementById("lblEnvioPos").innerHTML = "";
        f_ActivarBotones();
        return;
    }
}

function GuardarTransactionError(co) {
    if (co.message) {
        alert("Context:" + co.context + "\nError: " + co.message);
    }
	f_ActivarBotones();
}


function CallService(tipoPOS, objEntityPOS) {
    //alert('CallService');
    var varArrayEstTrans = document.getElementById("HidEstTrans").value.split("|");
    var varArrayAudi = document.getElementById("HidDatoAuditPos").value.split("|");
    strTipoTransaccion = strTipoTransaccion;
    strReferenciaVoucher = getValue('txtRefVoucher')
    strTipoTransANU = getValue('HidTipoTransAnu');
    strTipoTransRIM = getValue('HidTipoTransRIM');
    var varNroPedido = varNroRef;
    VarToday = new Date();
    var varIdTransaccion = varNroPedido + '_' + formatDate(VarToday);
    varBolWsLocal = false;
    var entityOpe;
    var soapMSG;
    var soapDataUpdate;
    var varNroTarjeta;
    var varMontoOperacion;
    var varEstTran;
    var varFechaExpiracion;
    var varSeriePOS;
    var varDesTipTarjeta = '';
    var VarTrnsnId = '';
    var VarToday = '';
    var varNroReferencia = '';
    var varFechaExpiracion;
    var varSeriePOS;
    var varIpApplicacion = varArrayAudi[0];
    var varNombreAplicacion = varArrayAudi[1];
    var varUsuarioAplicacion = varArrayAudi[2];

    varTipOpePOS = '';
    varRutaIni = '';
    // VALIDAR NO FINANCIERA/FINANCIERA ANULACION
    var varTipoOpeFiVisa = ((strTipoTransaccion == strTipoTransANU) ? varArrayTipoOpera[0] : varArrayTipoOpera[1])
    varTipoTrans = strTipoTransaccion;
    varMonedaVisa = '0';
    varMoneda;

    //varMonedaMC = '604';
    varNroRefVisa = '';
    var varMontoVisa = '';
    /*
    Financiera, No Financiera, 
    POS servicio, abrir puerto serial y cerrar puerto serial
    */

    switch (tipoPOS) {
        case "01": //VISANET        
            //alert('VISANET');

            entityOpe = {
                TipoOperacion: varTipoOpeFiVisa,
                SalidaMensaje: '',
                RutaArchivoINI: '',
                TipoMoneda: varMonedaVisa,
                Monto: objEntityPOS.montoOperacion,
                CodigoTienda: objEntityPOS.CodigoTienda,
                CodigoCaja: objEntityPOS.CodigoCaja,
                Empresa: '',
                Funcion: '',
                TipoPS: '',
                CapturaTarjeta: '',
                Cuotas: '',
                Diferido: '',
                Nombre: '',
                Valor: '',
                IdTransaccion: varIdTransaccion,
                IpApplicacion: varIpApplicacion,
                NombreAplicacion: varNombreAplicacion,
                UsuarioAplicacion: varUsuarioAplicacion
            };
            soapMSG = f_data_VisaNet(entityOpe);
            //alert(soapMSG);

            $.ajax({
                url: webServiceURL + '?op=peticionOperacionVisaNet',
                type: "POST",
                dataType: "text",
                data: soapMSG,
                processData: false,
                contentType: "text/xml; charset=\"utf-8\"",
                async: true,
                cache: false,
                success: function(objResponse, status) { /*Inicio success*/
                    SuccessVisaNet(objResponse);
                    /*Fin success*/
                },
                error: function(request, status) {
                    /*Inicio Error*/
                    alert('Sin respuesta del POS, tiempo de espera superado.');
                    ErrorVisaNet(request, objEntityPOS);
                    /*Fin Error*/
                },
                timeout: Number(timeOutWsLocal)
            });
            return true;
            break
        case "02": //MASTER CARD

            if (varTipoTrans == '3') {
                varMonedaMC = '';
                varMontoMC = '';
            } else {
                varMontoMC = objEntityPOS.montoOperacion;
            }
            varApliMC = document.getElementById("HidApliPOS").value;
            entityOpe = {
                IdTransaccion: varIdTransaccion,
                IpApplicacion: varIpApplicacion,
                NombreAplicacion: varNombreAplicacion,
                UsuarioAplicacion: varUsuarioAplicacion,
                Aplicacion: varApliMC, //varNombreAplicacion, 
                Transaccion: varTransMC,
                Monto: varMontoMC,
                TipoMoneda: varMonedaMC,
                DataAdicional: varNroRefMC,
                CodigoServicio: '',
                ClaveComercio: varPwdComercioMC,
                Dni: '',
                Ruc: '',
                Producto: '',
                OpeMonto: '',
                Nombre: '',
                Valor: ''
            };

            soapMSG = f_data_MC(entityOpe);
           // alert(soapMSG);

            $.ajax({
                url: webServiceURL + '?op=peticionOperacionMC',
                type: "POST",
                dataType: "text",
                data: soapMSG,
                timeout: timeOutWsLocal,
                processData: false,
                contentType: "text/xml; charset=\"utf-8\"",
                async: true,
                cache: false,
                success: function(objResponse, status) {
                   // alert('ok');
                    /*Inicio success*/
                    SuccessMasterCard(objResponse);

                },
                error: function(request, status) {
                    /*Inicio Error*/
                    varBolWsLocal = true;
                    alert('Sin respuesta del POS, tiempo de espera superado.');
                    ErrorMasterCard(request, objEntityPOS);
                    /*Fin Error*/
                },
                timeout: Number(timeOutWsLocal)
            });
            return true;
            break;
    }



    //varMontoOperacion OJO DUMMY
    varMontoOperacion = '';
    varNroTelefono = '';
    var varNroPedido = '';


    var varTramaAudit = '';
    varTramaAudit = 'NomCliente=' + varNomCliente + '|NroTelefono=' + varNroTelefono +
        '|NroPedido=' + varNroPedido + '|IdTransaccion=' + objEntityPOS.TrnsnId + '|nMonto=' + varMontoOperacion;

    varIdTransAux = objEntityPOS.TrnsnId;
    varTipoTrans = strTipoTransaccion;

   /* if (varTipoTrans == "2") {
        varCont1 = varCont1 + 1;
        if (varCont1 > 3) {

            varTramaAuditAux = varTramaAudit;
            RSExecute(serverURL, "GuardarAutorizacion", varTramaAudit, CallBack_GuardarAutorizacion, GuardarAutorizacionError, "X");

            return;
        }
    }*/


}

function ErrorVisaNet(request, objEntityPOS) {

    try {

        varBolWsLocal = true;

        var varClienteVisa = '';
        var varNumAutTransaccion = '';
        var varCodOperVisa = '';
        var varImpVoucher = '';
        varMontoOperacion = objEntityPOS.montoOperacion;
        varNomEquipoPOS = varNomPcPos;
        var varNroPedido = '';
        var varIdUnicoTrans = '';
        var varIdCabez = document.getElementById("HidIdCabez").value;
        var varFlagPago = '1';
        var varIdRefAnuVisa = '';
        var tipoPago=varTipPagONF;
        
        //CNH-INI-2018-03-19
        var varDesRptaWs='';
        //CNH-FIN-2018-03-19
        
        varNroTarjeta = varNroTarjeta;
        varNroReferencia = '';
        varNroReferencia = varNroRef;
        varNumAutTransaccion = '';
        varFechaExpiracion = '';
        varCodOperVisa = '';
        varSeriePOS = '';
        varImpVoucher = '';
        sCodRespTarj = '';
        varEstTran = varArrayEstTrans[3]; //RECHAZADO
        varDesRpta = 'ERROR';

        varTramaUpdate = '';
        varTramaUpdate = 'monedaOperacion=' + varMoneda + '|montoOperacion=' + varMontoOperacion +
            '|nroRegistro=' + varNroRegistro +
            '|numVoucher=' + varNroRef +
            '|numAutTransaccion=' + varNumAutTransaccion +
            '|codRespTransaccion=' + sCodRespTarj +
            '|descTransaccion=' + varDesRpta +
            '|codAprobTransaccion=' + varCodOperVisa +
            '|tipoPos=' + varTipoPos + '|varNroTarjeta=' + varNroTarjeta +
            '|fechaExpiracion=' + varFechaExpiracion +
            '|nomCliente=' + varClienteVisa +
            '|impVoucher=' + varImpVoucher +
            '|seriePOS=' + varSeriePOS + '|nomEquipoPOS=' + varNomEquipoPOS +
            '|estadoTransaccion=' + varEstTran +
            '|NroTienda=' + varNroTienda + '|CodigoCaja=' + varCodigoCaja +
            '|CodEstablec=' + varCodEstablec + '|IpPos=' + varIpPos + '|IdCabez=' + varIdCabez +
            '|FlagPago=' + varFlagPago + '|Pedido=' + varNroPedido + '|IdUnico=' + varIdUnicoTrans +
            '|TipoTrans=' + varTipoTrans +  '|varIdRefAnu=' + varIdRefAnuVisa + '|TipoPago=' + tipoPago+
            '|ResTarjetaPos=';//Proy-31949-Inicio; ;


        RSExecute(serverURL, "ActualizarTransaction", varTramaUpdate,
            objEntityPOS.TrnsnId, CallBack_ActualizarTransaction2, GuardarTransactionError, "X");
        document.getElementById("lblEnvioPos").innerHTML = "";

        var varTramaAudit = '';
        varTramaAudit = 'NomCliente=' + varNomCliente +
            '|NroTelefono=' + varNroTelefono +
            '|NroPedido=' + varNroPedido + '|IdTransaccion=' +
            objEntityPOS.TrnsnId + '|nMonto=' + varMontoOperacion;
        varIdTransAux = objEntityPOS.TrnsnId;


        /*if (varTipoTrans == "2") {
            varCont1 = varCont1 + 1;
            if (varCont1 > 3) {
                varTramaAuditAux = varTramaAudit;
                RSExecute(serverURL, "GuardarAutorizacion", varTramaAudit, CallBack_GuardarAutorizacion, GuardarAutorizacionError, "X");
                varCont1 = 0;
                return;
            }
        }*/
        // INSERTAR APERTURA CIERRE
        var varTramaInsertApeCiePOS = '';
        if ((varTipoTrans == '6' || varTipoTrans == '7') && varTipoPos == 'VISA') {
            var varTipoOperacion = ((varTipoTrans == '6') ? '1' : '2');

            varTramaInsertApeCiePOS = 'tipoOperacion=' + varTipoOperacion +
                '|estadoTransaccion=' + varEstTran +
                '|descTransaccion=' + varDesRptaWs +
                '|nroRegistro=' + varNroRegistro;
            RSExecute(serverURL, "GuardarTransactionApeCiePOS", varTramaInsertApeCiePOS, CallBack_GuardarTransacAperturaCierre, GuardarTransactionError, "X");

        }
         document.getElementById("lblEnvioPos").innerHTML = "";
		f_ActivarBotones();
    } catch (err) {
        alert(err.description);
        document.getElementById("lblEnvioPos").innerHTML = "";
        f_ActivarBotones();
    }
}

function SuccessVisaNet(objResponse) {
    try {
        var x2js = new X2JS();
        var jsonObj = x2js.xml_str2json(objResponse);
        var varImpVoucher = '';
        var varDesRpta = '';
        var varClienteVisa = '';
        var varNumAutTransaccion = '';
        var varCodOperVisa = '';
        var varTramaUpdate = '';
        var varCodRptaAudit = '';
        var varMsjRptaAudit = '';
        var varCodRptaWs = '';
        var varMsgAlert = '';
        var varPrintData = '';
        var varFechaExpiracion = '';
        var varSeriePOS = '';
        var varNroPedido = varNroRef;
        var varIdUnicoTrans = '';
        var varFlagPago = '1';
        var varIdCabez = document.getElementById("HidIdCabez").value;
        var varDesRptaWs = '';
        var varIdRefAnuVisa = '';
        var tipoPago=varTipPagONF;
        varCodRptaAudit = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.AuditResponse.CodigoRespuesta;
        varMsjRptaAudit = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.AuditResponse.mensajeRespuesta;
        varCodRptaWs = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.CodigoRespuesta;
        varDesRptaWs = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.DescripcionRespuesta;
        
        
        

        /*VALIDAR OPCION RE-IMPRESION*/
        if (varTipoTrans == '3' || varTipoTrans == '4' || varTipoTrans == '5' || varTipoTrans == '6' || varTipoTrans == '7') {
            varNroTarjeta = '';
            varNroReferencia = '';
            varNroReferencia = varNroPedido;
            varNumAutTransaccion = '';
            varFechaExpiracion = '';
            varCodOperVisa = '';
            varSeriePOS = '';
            varImpVoucher = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.ImprimirData;

            if ( varImpVoucher != null )
			{
				if (String(varImpVoucher).length > 50)
					varImpVoucher = String(varImpVoucher).substring(1, 200);
				else
					varImpVoucher = String(varImpVoucher);
			}
			else
			{
				varImpVoucher = '';
			}

            sCodRespTarj = '';

            if (varCodRptaWs == '1') {
                varEstTran = varArrayEstTrans[3]; //RECHAZADO
                varMsgAlert = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.ImprimirData;
                alert(varMsgAlert);
            } else {
                varEstTran = varArrayEstTrans[2]; //ACEPTADO								
            }


            if (varImpVoucher.indexOf('CANCELADA') > -1) {
                varDesRpta = varImpVoucher;
            } else {
                varDesRpta = '';
            }
            // INSERTAR APERTURA CIERRE
            var varTramaInsertApeCiePOS = '';
            if ((varTipoTrans == '6' || varTipoTrans == '7') && varTipoPos == 'VISA') {
                var varTipoOperacion = ((varTipoTrans == '6') ? '1' : '2');

                varTramaInsertApeCiePOS = 'tipoOperacion=' + varTipoOperacion +
                    '|estadoTransaccion=' + varEstTran +
                    '|descTransaccion=' + varDesRptaWs +
                    '|nroRegistro=' + varNroRegistro;
                RSExecute(serverURL, "GuardarTransactionApeCiePOS", varTramaInsertApeCiePOS, CallBack_GuardarTransacAperturaCierre, GuardarTransactionError, "X");

            }
        } else {
            if ((varCodRptaAudit != '0' && typeof varCodRptaWs == 'undefined') || varCodRptaWs == '1') {
                varDesRpta = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.ImprimirData;
                varNroTarjeta = '';
                varNroReferencia = '';
                varNroReferencia = varNroRefVisa;
                varNumAutTransaccion = '';
                varFechaExpiracion = '';
                sCodRespTarj = '';
                varCodOperVisa = '';
                varSeriePOS = '';

                if (typeof jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.ImprimirData == 'undefined') {
                    varImpVoucher = '';
                    varMsgAlert = varMsjRptaAudit;
                } else {
                    varImpVoucher = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.ImprimirData;
                    varMsgAlert = varImpVoucher;
                }
                varEstTran = varArrayEstTrans[3]; //RECHAZADO
            } else {
                /*Ouput Visa Ini(Venta&Anulacion)*/
                sCodRespTarj = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.CodigoRespuesta);
                varDesRpta = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.DescripcionRespuesta);
                varClienteVisa = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.NombreCliente);
                varNumAutTransaccion = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.NumeroAutorizacion);
                varNroRefVisa = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.NumeroReferencia);
                varNroTarjeta = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.NumeroTarjeta);
                varFechaExpiracion = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.FechaExpiracion);
                varCodOperVisa = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.CodigoOperacion);
                varSeriePOS = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.Terminal);
                varImpVoucher = '';
                varImpVoucher = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.ImprimirData);
                varNroReferencia = varNroRefVisa;

                if (typeof jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.IdUnico == 'undefined')
                    varIdUnicoTrans = '';
                else
                    varIdUnicoTrans = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.IdUnico);


                if ( varImpVoucher != null )
				{
					if (String(varImpVoucher).length > 50)
						varImpVoucher = String(varImpVoucher).substring(1, 200);
					else
						varImpVoucher = String(varImpVoucher);
				}
				else
				{
					varImpVoucher = '';
				}



                /*Codigo de Rpta VISA*/
                if (sCodRespTarj == '00' || sCodRespTarj == '11') {
                    varEstTran = varArrayEstTrans[2]; //ACEPTADO
                } else {
                    varEstTran = varArrayEstTrans[3]; //RECHAZADO						
                    varMsgAlert = varDesRpta;
                    varNroTarjeta = '';
                }

                /*Ouput Visa Fin (Venta&Anulacion)*/
            }
        }
        varMontoOperacion = '';
        varMontoOperacion = objEntityPOS.montoOperacion;
        varNomEquipoPOS = '';
        varNomEquipoPOS = varNomPcPos;

        //alert('Contador: ' + varCont1.toString());

        //VALIDACION CUARTA ANULACION ESTADO INCONPLETO
        /*if (varCont1 >= 3) {
            varEstTran = varArrayEstTrans[4]; //INCONPLETO	
        }*/

        varTramaUpdate = '';
        varTramaUpdate = 'monedaOperacion=' + varMoneda + '|montoOperacion=' + varMontoOperacion +
            '|nroRegistro=' + varNroRegistro +
            '|numVoucher=' + varNroRefVisa +
            '|numAutTransaccion=' + varNumAutTransaccion +
            '|codRespTransaccion=' + sCodRespTarj +
            '|descTransaccion=' + varDesRpta +
            '|codAprobTransaccion=' + varCodOperVisa +
            '|tipoPos=' + varTipoPos + '|varNroTarjeta=' + varNroTarjeta +
            '|fechaExpiracion=' + varFechaExpiracion +
            '|nomCliente=' + varClienteVisa +
            '|impVoucher=' + varImpVoucher +
            '|seriePOS=' + varSeriePOS + '|nomEquipoPOS=' + varNomEquipoPOS +
            '|estadoTransaccion=' + varEstTran +
            '|NroTienda=' + varNroTienda + '|CodigoCaja=' + varCodigoCaja +
            '|CodEstablec=' + varCodEstablec + '|IpPos=' + varIpPos + '|IdCabez=' + varIdCabez +
            '|FlagPago=' + varFlagPago + '|Pedido=' + varNroPedido + '|IdUnico=' + varIdUnicoTrans +
            '|TipoTrans=' + varTipoTrans +  '|varIdRefAnu=' + varIdRefAnuVisa + '|TipoPago=' + tipoPago+
            '|ResTarjetaPos=';//Proy-31949-Inicio; ;

        //alert(varTramaUpdate);

        var varRpta = RSExecute(serverURL, "ActualizarTransaction", varTramaUpdate,
            objEntityPOS.TrnsnId);

        var varTramaAudit = '';

        varTramaAudit = 'NomCliente=' + varNomCliente +
            '|NroTelefono=' + "" +
            '|NroPedido=' + varNroPedido +
            '|IdTransaccion=' + objEntityPOS.TrnsnId +
            '|nMonto=' + varTransMonto;

        varIdTransAux = objEntityPOS.TrnsnId;

        /*if (varTipoTrans == 2 && varCont1 >= 2 && varEstTran != varArrayEstTrans[2] && varEstTran != varArrayEstTrans[4]) {
            alert('ENVIANDO SOLICITUD DE ANULACION');
            varTramaAuditAux = varTramaAudit;
            varCont1 = parseInt(varCont1) + 1;
            RSExecute(serverURL, "GuardarAutorizacion", varTramaAudit, CallBack_GuardarAutorizacion, GuardarAutorizacionError, "X");
        }*/

        //ANULACION MASTERCARD Y VISANET
        if (varCodOpe == '04' && (varEstTran == '3' || varEstTran == '5') && varTipoTrans == '2') {
            alert('ANULACION CORRECTA');

        }

        //ANULACION CANCELADA
        else if (varCodOpe == '04' && varEstTran == '4' && varTipoTrans == '2') {
            alert('ANULACION CANCELADA');
        }
        
        if(varTipoTrans != '2' && varEstTran == '3' ){
			alert('OPERACION EXITOSA');
        }
        f_limpiarForm();
        document.getElementById("lblEnvioPos").innerHTML = "";
        f_ActivarBotones();
        //alert('OPERACION EXITOSA.')
    } catch (err) {
        alert(err.description);
        //f_limpiarForm();
        document.getElementById("lblEnvioPos").innerHTML = "";
         f_ActivarBotones();
    }

}

function CallBack_ActualizarTransaction(response) {
    var varRpta = response.return_value;

    var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n", "");

    varRpta = res;

    //alert(varRpta);
}

function CallBack_GuardarTransacAperturaCierre(response) {
    var varRpta = response.return_value;

    var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n", "");

    varRpta = "CallBack_GuardarTransacAperturaCierre: " + res;

    //alert(varRpta);
}

function CallBack_GuardarAutorizacion(response) {

    var varRpta = response.return_value;
    var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n", "");

    varRpta = res;

    if (varRpta == '0') {

        alert('Ud. no está autorizado a realizar esta operación. Comuniquese con el administrador');
        //RSExecute(serverURL, "GuardarAutorizacion", varTramaAuditAux, CallBack_GuardarAutorizacion, GuardarAutorizacionError, "X");
        return;
    }
    else
    {
      f_EnvioPOS();
    }
}

function GuardarAutorizacionError(co) {
    
    if (co.message) {
        alert("Context:" + co.context + "\nError: " + co.message);
    }
    f_ActivarBotones();
}

function CallBack_ActualizarTransaction1(response) {
    var varRpta = response.return_value;

    var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n", "");

    varRpta = res;
}

function CallBack_ActualizarTransaction2(response) {
    var varRpta = response.return_value;

    var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n", "");

    varRpta = res;
}


function SuccessMasterCard(objResponse) {
    try {
        var x2js = new X2JS();
        var jsonObj = x2js.xml_str2json(objResponse);

        var varImpVoucher = '';
        var varDesRpta = '';
        var varClienteVisa = '';
        var varNumAutTransaccion = '';
        var varCodOperVisa = '';
        var varTramaUpdate = '';
        var varCodRptaAudit = '';
        var varMsjRptaAudit = '';
        var varCodRptaWs = '';
        var varMsgAlert = '';
        var varPrintData = '';
        var varIdRefAnuMC = '';
        var varNroPedido = '';
        var varIdUnicoTrans = '';
        var varFlagPago = '1';
        var varIdCabez = document.getElementById("HidIdCabez").value;
		var varFechaExpiracion='';
		var varNroReferencia ='';
		varClienteVisa='';
		var varSeriePOS='';
		var tipoPago=varTipPagONF;
        varCodRptaAudit = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.AuditResponse.CodigoRespuesta;
        varMsjRptaAudit = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.AuditResponse.mensajeRespuesta;
        varCodRptaWs = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoRespuesta;

        /*VALIDAR OPCION RE-IMPRESION*/
        if (varTipoTrans == '3' || varTipoTrans == '4' || varTipoTrans == '5' ||  varTipoTrans == '7') {

            sCodRespTarj = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoRespuesta);
            varDesRpta = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.DescripcionRespuesta);
            varNroTarjeta = '';
            varNroReferencia = '';
            varNroReferencia = varNroRefVisa;
            varNumAutTransaccion = ''; //NumeroAutorizacion
            varNumAutTransaccion = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.NumeroAutorizacion);
            varFechaExpiracion = '';
            varCodOperVisa = '';
            varSeriePOS = '';
            varCodOperVisa='';
            varImpVoucher = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.ImprimirData);

            if (varDesRpta == ('RESERVADO')) {
                varImpVoucher = ''
            }

            if ( varImpVoucher != null )
			{
				if (String(varImpVoucher).length > 50)
					varImpVoucher = String(varImpVoucher).substring(1, 20);
				else
					varImpVoucher = String(varImpVoucher);
			}
			else
			{
				varImpVoucher = '';
			}


            if (varCodRptaWs == '1') {
                varEstTran = varArrayEstTrans[3]; //RECHAZADO
                varMsgAlert = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.ImprimirData;
                alert(varMsgAlert);
            } else {
                varEstTran = varArrayEstTrans[2]; //ACEPTADO								
            }
        } else {
            if (((varCodRptaAudit == '0' || varCodRptaAudit == '-2') && varCodRptaWs == '77') || varCodRptaWs == '1') {
                varDesRpta = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.DescripcionRespuesta;
                varNroTarjeta = '';
                varNroReferencia = '';
                varNroReferencia = varNroRefVisa;
                varNumAutTransaccion = '';
                varFechaExpiracion = '';
                sCodRespTarj = (sCodRespTarj == null) ? '' : String(sCodRespTarj);
                varCodOperVisa = '';
                varSeriePOS = '';

                varImpVoucher = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.DescripcionRespuesta;
                varMsgAlert = varImpVoucher;
                varEstTran = varArrayEstTrans[3]; //RECHAZADO
            } else {
                if (typeof jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoAdquiriente == 'undefined')
                    varIdUnicoTrans = '';
                else
                    varIdUnicoTrans = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoAdquiriente);

                /*Ouput Visa Ini(Venta&Anulacion)*/
                /*CodigoAprobacion*/
                sCodRespTarj =isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoRespuesta);
                varDesRpta = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.DescripcionRespuesta);
                varClienteVisa =isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.NombreCliente);
                varNumAutTransaccion =isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.NumeroAutorizacion);

                varNroRefVisa =isEmptyValue( jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.NumeroReferencia);
                varNroRefVisa = (varNroRefVisa == null) ? '' : String(varNroRefVisa).replace("REF","");
                varNroRefVisa =isEmptyValue( trim1(varNroRefVisa));
                varNroTarjeta =isEmptyValue( jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.NumeroTarjeta);
                varFechaExpiracion = '';
                varCodOperVisa =isEmptyValue( jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoAprobacion);
                varSeriePOS = '';
                varImpVoucher = '';
                varImpVoucher = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.ImprimirData);
                varIdRefAnuMC = varNroRefVisa;
				
				 if (varCodOpe == '04') {
                    varIdRefAnuMC = varNroRefVisa;
                    varNroRefVisa = '';
                    varNroRefVisa = getValue('txtRefVoucher');
                }
                
                if ( varImpVoucher != null )
				{
					if (String(varImpVoucher).length > 50)
						varImpVoucher = String(varImpVoucher).substring(1, 20);
					else
						varImpVoucher = String(varImpVoucher);
				}
				else
				{
					varImpVoucher = '';
				}

                /*Codigo de Rpta VISA*/
                if (sCodRespTarj == '00' || sCodRespTarj == '11') {
                    varEstTran = varArrayEstTrans[2]; //ACEPTADO
                } else {
                    varEstTran = varArrayEstTrans[3]; //RECHAZADO						
                    varMsgAlert = varDesRpta;
                    varNroTarjeta = '';
                }
                /*Ouput Visa Fin (Venta&Anulacion)*/
            }
        }

        varMontoOperacion = '';
        varMontoOperacion = objEntityPOS.montoOperacion;
        varNomEquipoPOS = '';
        varNomEquipoPOS = varNomPcPos;


        //VALIDACION CUARTA ANULACION ESTADO INCONPLETO
        /*if (varCont1 >= 3) {
            varEstTran = varArrayEstTrans[4]; //INCONPLETO	
        }*/
		 var varTramaInsertApeCiePOS = '';
            if ((varTipoTrans == '7') && varTipoPos == 'MASTERCARD') {
			var  varTipoOperacion='';
            varTipoOperacion ='2';

                varTramaInsertApeCiePOS = 'tipoOperacion=' + varTipoOperacion +
                    '|estadoTransaccion=' + varEstTran +
                    '|descTransaccion=' + varDesRpta +
                    '|nroRegistro=' + varNroRegistro;
                RSExecute(serverURL, "GuardarTransactionApeCiePOS", varTramaInsertApeCiePOS, CallBack_GuardarTransacAperturaCierre, GuardarTransactionError, "X");

            }
        varTramaUpdate = '';
        varTramaUpdate = 'monedaOperacion=' + varMoneda + '|montoOperacion=' + varMontoOperacion +
            '|nroRegistro=' + varNroRegistro +
            '|numVoucher=' + varNroRefVisa +
            '|numAutTransaccion=' + varNumAutTransaccion +
            '|codRespTransaccion=' + sCodRespTarj +
            '|descTransaccion=' + varDesRpta +
            '|codAprobTransaccion=' + varCodOperVisa +
            '|tipoPos=' + varTipoPos + '|varNroTarjeta=' + varNroTarjeta +
            '|fechaExpiracion=' + varFechaExpiracion +
            '|nomCliente=' + varClienteVisa +
            '|impVoucher=' + varImpVoucher +
            '|seriePOS=' + varSeriePOS + '|nomEquipoPOS=' + varNomEquipoPOS +
            '|estadoTransaccion=' + varEstTran +
            '|NroTienda=' + varNroTienda + '|CodigoCaja=' + varCodigoCaja +
            '|CodEstablec=' + varCodEstablec + '|IpPos=' + varIpPos + '|IdCabez=' + varIdCabez +
            '|FlagPago=' + varFlagPago + '|Pedido=' + varNroPedido + '|IdUnico=' + varIdUnicoTrans +
            '|TipoTrans=' + varTipoTrans +  '|varIdRefAnu=' + varIdRefAnuMC  + '|TipoPago=' + tipoPago+
            '|ResTarjetaPos=';//Proy-31949-Inicio;;



        var varRpta = RSExecute(serverURL, "ActualizarTransaction", varTramaUpdate,
            objEntityPOS.TrnsnId);

        var varTramaAudit = '';

        varTramaAudit = 'NomCliente=' + varNomCliente +
            '|NroTelefono=' + "" +
            '|NroPedido=' + varNroPedido +
            '|IdTransaccion=' + objEntityPOS.TrnsnId +
            '|nMonto=' + varTransMonto;

        varIdTransAux = objEntityPOS.TrnsnId;

        /*if (varTipoTrans == 2 && varCont1 >= 2 && varEstTran != varArrayEstTrans[2] && varEstTran != varArrayEstTrans[4]) {
            alert('ENVIANDO SOLICITUD DE ANULACION');
            varTramaAuditAux = varTramaAudit;
            varCont1 = parseInt(varCont1) + 1;
            RSExecute(serverURL, "GuardarAutorizacion", varTramaAudit, CallBack_GuardarAutorizacion, GuardarAutorizacionError, "X");
        }*/

        //ANULACION MASTERCARD Y VISANET
        if (varCodOpe == '04' && (varEstTran == '3' || varEstTran == '5') && varTipoTrans == '2') {
            alert('ANULACION CORRECTA');

        }

        //ANULACION CANCELADA
        else if (varCodOpe == '04' && varEstTran == '4' && varTipoTrans == '2') {
            alert('ANULACION CANCELADA');
        }
        
        if(varTipoTrans != '2' && varEstTran == '3' ){
			alert('OPERACION EXITOSA');
        }
        f_limpiarForm();
        document.getElementById("lblEnvioPos").innerHTML = "";
         f_ActivarBotones();
        // limpiar form
        //alert('OPERACION EXITOSA');

    } catch (err) {
        alert(err.description);
        document.getElementById("lblEnvioPos").innerHTML = "";
         f_ActivarBotones();
    }
}

function trim1 (string) {
		return string.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
	}

function ErrorMasterCard(request, objEntityPOS) {
    try {
        alert('Error: Rechazado');

        var varClienteVisa = '';
        var varNumAutTransaccion = '';
        var varCodOperVisa = '';
        var varImpVoucher = '';
        var varIdRefAnuMC = '';
        var varNroPedido = '';
        var varIdUnicoTrans = '';
        var varIdCabez = document.getElementById("HidIdCabez").value;
        var varFlagPago = '1';
		var varNroReferencia='';
		var varSeriePOS ='';
		var tipoPago=varTipPagONF;
        varMontoOperacion = objEntityPOS.montoOperacion;
        varNomEquipoPOS = varNomPcPos;
        varNroTarjeta = '';

        varNroReferencia = '';
        varNroReferencia = varNroRefVisa;
        varNumAutTransaccion = '';
        varFechaExpiracion = '';
        varCodOperVisa = '';
        varSeriePOS = '';
        varImpVoucher = '';
        sCodRespTarj = '';

        //VALIDACION CUARTA ANULACION ESTADO INCONPLETO
        /*if (varCont1 >= 3) {
            varEstTran = varArrayEstTrans[4]; //INCONPLETO
        } else {*/
            varEstTran = varArrayEstTrans[3]; //RECHAZADO
            varDesRpta = '';
        
			 var varTramaInsertApeCiePOS = '';
            if ((varTipoTrans == '7') && varTipoPos == 'VISA') {
                var varTipoOperacion ='2';

                varTramaInsertApeCiePOS = 'tipoOperacion=' + varTipoOperacion +
                    '|estadoTransaccion=' + varEstTran +
                    '|descTransaccion=' + varDesRpta +
                    '|nroRegistro=' + varNroRegistro;
                RSExecute(serverURL, "GuardarTransactionApeCiePOS", varTramaInsertApeCiePOS, CallBack_GuardarTransacAperturaCierre, GuardarTransactionError, "X");

            }
        varTramaUpdate = '';
        varTramaUpdate = 'monedaOperacion=' + varMoneda + '|montoOperacion=' + varMontoOperacion +
            '|nroRegistro=' + varNroRegistro +
            '|numVoucher=' + varNroRefVisa +
            '|numAutTransaccion=' + varNumAutTransaccion +
            '|codRespTransaccion=' + sCodRespTarj +
            '|descTransaccion=' + varDesRpta +
            '|codAprobTransaccion=' + varCodOperVisa +
            '|tipoPos=' + varTipoPos + '|varNroTarjeta=' + varNroTarjeta +
            '|fechaExpiracion=' + varFechaExpiracion +
            '|nomCliente=' + varClienteVisa +
            '|impVoucher=' + varImpVoucher +
            '|seriePOS=' + varSeriePOS + '|nomEquipoPOS=' + varNomEquipoPOS +
            '|estadoTransaccion=' + varEstTran +
            '|NroTienda=' + varNroTienda + '|CodigoCaja=' + varCodigoCaja +
            '|CodEstablec=' + varCodEstablec + '|IpPos=' + varIpPos + '|IdCabez=' + varIdCabez +
            '|FlagPago=' + varFlagPago + '|Pedido=' + varNroPedido + '|IdUnico=' + varIdUnicoTrans + 
            '|TipoTrans=' + varTipoTrans + '|varIdRefAnu=' + varIdRefAnuMC + '|TipoPago=' + tipoPago+
            '|ResTarjetaPos=';//Proy-31949-Inicio; ;




        RSExecute(serverURL, "ActualizarTransaction", varTramaUpdate,
            objEntityPOS.TrnsnId, CallBack_ActualizarTransaction2, GuardarTransactionError, "X");




        var varNomCliente = '';
        var varNroTelefono = '';
        var varNroPedido = '';

        var varTramaAudit = '';
        varTramaAudit = 'NomCliente=' + varNomCliente +
            '|NroTelefono=' + varNroTelefono +
            '|NroPedido=' + varNroPedido + '|IdTransaccion=' +
            objEntityPOS.TrnsnId + '|nMonto=' + varMontoOperacion;
        varIdTransAux = objEntityPOS.TrnsnId;

        /*if (varTipoTrans == "2") {
            varCont1 = varCont1 + 1;
            if (varCont1 > 3) {
                varTramaAuditAux = varTramaAudit;
                RSExecute(serverURL, "GuardarAutorizacion", varTramaAudit, CallBack_GuardarAutorizacion, GuardarAutorizacionError, "X");
                varCont1 = 0;
                return;
            }

        }*/
          document.getElementById("lblEnvioPos").innerHTML = "";
          f_ActivarBotones();
    } catch (err) {
        alert(err.description);
          document.getElementById("lblEnvioPos").innerHTML = "";
          f_ActivarBotones();
    }

}

function f_CargarTransacciones() {
		var varArrayTipoTran=document.getElementById("HidTipoTran").value.split("|");
		var varArrayCodOpe=document.getElementById("HidCodOpera").value.split("|");

    if (strTipoPOS == '01') {
        varDesTipTarjeta = "VISA"
        varValueTar = 'VIS';
        varNroRegistro = varArrayDatoPosVisa[0].substr(varArrayDatoPosVisa[0].indexOf("=") + 1);
        varNroTienda = varArrayDatoPosVisa[1].substr(varArrayDatoPosVisa[1].indexOf("=") + 1);
        varCodigoCaja = varArrayDatoPosVisa[2].substr(varArrayDatoPosVisa[2].indexOf("=") + 1);
        varCodEstablec = varArrayDatoPosVisa[3].substr(varArrayDatoPosVisa[3].indexOf("=") + 1);
        varNomPcPos = varArrayDatoPosVisa[4].substr(varArrayDatoPosVisa[4].indexOf("=") + 1);
        varCodTerminal = varArrayDatoPosVisa[6].substr(varArrayDatoPosVisa[6].indexOf("=") + 1);
        varIpPos = varArrayDatoPosVisa[7].substr(varArrayDatoPosVisa[7].indexOf("=") + 1);
    } else {
        varDesTipTarjeta = "MASTERCARD";
        varValueTar = 'MCD';
        //varTipoPos= varArrayTipoPOS[1];
        //varTipTarjeta = varArrayCodTarjeta[1];//MASTERCARD				

        varNroRegistro = varArrayDatoPosMC[0].substr(varArrayDatoPosMC[0].indexOf("=") + 1);
        varNroTienda = varArrayDatoPosMC[1].substr(varArrayDatoPosMC[1].indexOf("=") + 1);
        varCodigoCaja = varArrayDatoPosMC[2].substr(varArrayDatoPosMC[2].indexOf("=") + 1);
        varCodEstablec = varArrayDatoPosMC[3].substr(varArrayDatoPosMC[3].indexOf("=") + 1);
        varNomPcPos = varArrayDatoPosMC[4].substr(varArrayDatoPosMC[4].indexOf("=") + 1);
        varCodTerminal = varArrayDatoPosMC[6].substr(varArrayDatoPosMC[6].indexOf("=") + 1);
        varIpPos = varArrayDatoPosMC[7].substr(varArrayDatoPosMC[7].indexOf("=") + 1);

    }
    var PuntoVenta = getValue('HidPtoVenta');
    var TipoTarjetaPos = varDesTipTarjeta;
    var TipoTransaccion = varArrayTipoTran[0];
    var TipoOperacion = varArrayCodOpe[0];;
    var FechaInicial = FormatDateDdMmYyy(VarToday);
    var FechaFinal = FormatDateDdMmYyy(VarToday);
    var CodigoComercio ='';// varCodEstablec;
    var Cajero = getValue('hidUsuCodCaja');
    var EstadoTransaccion = '';
    var NumReferenciaVoucher = getValue('txtRefVoucher');

    var TramaCargarTrans =
        PuntoVenta + "|" +
        TipoTarjetaPos + "|" +
        TipoTransaccion + "|" +
        TipoOperacion + "|" +
        FechaInicial + "|" +
        FechaFinal + "|" +
        CodigoComercio + "|" +
        Cajero + "|" +
        EstadoTransaccion + "|" +
        NumReferenciaVoucher;


    RSExecute(serverURL, "CargarTransaccionesPOS", TramaCargarTrans, CallBack_CargarTransaccionesPOS, "X");

}

function CallBack_CargarTransaccionesPOS(response) {
	var msjCaja=getValue('hidMsjCajero');
	var NumReferenciaVoucher = getValue('txtRefVoucher');
    var varRpta = response.return_value;
    var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n","");
    var cresult = true;
    var filas = res.split(";");
    var celdas;
    var varUsuCodCaja = getValue('HidUsuCodCaja');
    var varCodCajaVoucher = "";
    var intentos = 1;
    var arrTipoOperacion = getValue("HidOperacionVoucher").split("|");
    var strTipoTrans = getValue("cboTipoTransaccion");
	if(filas!="" && filas[0].split("|")[0]!="Error"){
    for (var i = 0; i < filas.length; i++) {
        celdas = filas[i].split("|");

    }
    var idcab = celdas[0];
    varMontoAnular = celdas[8];
    varMontoAnular = Number(varMontoAnular).toFixed(2);
    varCodCajaVoucher = celdas.pop();
    document.getElementById("HidIdCabez").value = idcab;
    if (varCodCajaVoucher != varUsuCodCaja) {
        cresult = false;
        alert(msjCaja);

    } else {

        /**/
        if (strTipoTrans == 2)
        {
        
            var varTramaAudit = '';
			varTramaAudit = 'NomCliente=' + '' +
							 '|NroTelefono=' + '' +
							 '|NroPedido=' + celdas[3] +
							 '|IdTransaccion=' + idcab + 
							 '|nMonto=' + varMontoAnular;
        
            RSExecute(serverURL, "GuardarAutorizacion", varTramaAudit, CallBack_GuardarAutorizacion, GuardarAutorizacionError, "X");
							  
		}					  
        else
        {
        f_EnvioPOS();
    }
                
    }
	}else{
		if(filas[0].split("|")[0]=="Error"){
		var msjError="Ocurrió un error: "+isEmptyValue(filas[0].split("|")[1]);
		alert(msjError);
		}else{
			alert("Id Ref:" + NumReferenciaVoucher + ", NO tiene datos.");
		}
	}
    return cresult;
}

function f_limpiarForm() {
    setValue('txtRefVoucher', '');
    document.getElementById('cboTipoTransaccion').selectedIndex = 0;
    document.getElementById('cboTipPOS').selectedIndex = 0;

}

function isEmptyValue(val) {
    return (val == 'undefined' || val == null || val.length <= 0) ? '' : val;
}
function f_bloquearBotones(){
	document.getElementById("cmdEnviar").disabled = true;
	document.getElementById("cmdCerrar").disabled = true;
}
function f_ActivarBotones(){
	document.getElementById("cmdEnviar").disabled = false;
	document.getElementById("cmdCerrar").disabled = false;
}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:panel id="PnPosNoFinanciera" runat="server" Visible="True">
				<TABLE border="0" cellSpacing="0" cellPadding="0" width="450">
					<TR>
						<TD vAlign="top" width="10">&nbsp;</TD>
						<TD style="WIDTH: 832px" vAlign="top" width="480">
							<TABLE border="0" cellSpacing="0" cellPadding="0" width="100%" name="Contenedor">
								<TR>
									<TD height="10" align="center"></TD>
								</TR>
							</TABLE>
							<TABLE border="1" cellSpacing="0" borderColor="#336699" cellPadding="0" width="100%" align="center">
								<TR>
									<TD align="center">
										<TABLE border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
											<TR>
												<TD height="32" vAlign="top" width="10"></TD>
												<TD style="PADDING-TOP: 4px" class="TituloRConsulta" height="32" vAlign="top" width="98%"
													align="center">
													<asp:Label id="lblTitOperacionesNoFinan" Runat="server">ENVIO al POS de Operaciones no Financieras</asp:Label></TD>
											</TR>
										</TABLE>
										<TABLE border="0" cellSpacing="0" borderColor="#336699" cellPadding="4" width="350" align="center">
											<TR>
												<TD>
													<asp:Label id="lblOficina" runat="server" CssClass="Arial12b">Tipo POS :</asp:Label></TD>
												<TD style="WIDTH: 190px">
													<asp:dropdownlist id="cboTipPOS" tabIndex="6" runat="server" CssClass="clsSelectEnable" onchange="f_CambiarTipoOperacion()"></asp:dropdownlist>&nbsp;
												</TD>
											</TR>
											<TR>
												<TD>
													<asp:Label id="lblCodComercio" runat="server" CssClass="Arial12b">Codigo Comercio: </asp:Label></TD>
												<TD><INPUT style="WIDTH: 120px" id="txtCodComercio" class="clsInputDisable" disabled name="txtCodComercio"
														runat="server"></TD>
											</TR>
											<TR>
												<TD>
													<asp:Label id="lblCodTerminal" runat="server" CssClass="Arial12b">Codigo Terminal: </asp:Label></TD>
												<TD><INPUT style="WIDTH: 120px" id="txtCodTerminal" class="clsInputDisable" disabled name="txtCodTerminal"
														runat="server"></TD>
											</TR>
											<TR>
												<TD>
													<asp:Label id="lblTipoTransaccion" runat="server" CssClass="Arial12b">Tipo Transaccion: </asp:Label></TD>
												<TD style="WIDTH: 190px">
													<asp:dropdownlist id="cboTipoTransaccion" tabIndex="6" runat="server" CssClass="clsSelectEnable" onchange="f_CambiarTipoOperacion()"></asp:dropdownlist>&nbsp;
												</TD>
											</TR>
											<TR>
												<TD>
													<asp:Label id="lblRefVoucher" runat="server" CssClass="Arial12b">Referencia (Voucher)</asp:Label></TD>
												<TD><INPUT style="WIDTH: 100px" id="txtRefVoucher" class="clsInputEnable" disabled maxLength="40"
														size="25" name="txtRefVoucher" runat="server"></TD>
											</TR> <!--PROY-27440 INI-->
											<TR>
												<TD colSpan="7" align="center">
													<asp:label id="lblEnvioPos" runat="server" CssClass="TituloRConsulta"></asp:label></TD>
											</TR> <!--PROY-27440 FIN--></TABLE>
										<BR>
										<BR>
										<TABLE border="1" cellSpacing="0" borderColor="#336699" cellPadding="4" width="300" align="center">
											<TR>
												<TD>
													<TABLE class="Arial10B" border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
														<TR>
															<TD align="center"><INPUT style="WIDTH: 100px" id="cmdEnviar" class="BotonOptm" onclick="javascript:f_Enviar();"
																	value="ENVIO" type="button" name="btnEnviar" runat="server">&nbsp;&nbsp;</TD>
															<TD align="center"><INPUT style="WIDTH: 100px" id="cmdCerrar" class="BotonOptm" onclick="javascript:f_Cerrar();"
																	value="CERRAR" type="button" name="btnCerrar">&nbsp;&nbsp;
																<asp:Button style="DISPLAY: none" id="loadDataHandler" runat="server" Text="Button"></asp:Button></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
										<BR>
										<BR>
										<INPUT id="HidOperacionVoucher" type="hidden" name="HidOperacionVoucher" runat="server">
										<INPUT id="HidEstTrans" type="hidden" name="HidEstTrans" runat="server"> <INPUT id="HidTipoOpera" type="hidden" name="HidTipoOpera" runat="server">
										<INPUT id="HidTipoTransAnu" type="hidden" name="HidTipoTransAnu" runat="server"><INPUT id="HidTipoTransRIM" type="hidden" name="HidTipoTransRIM" runat="server">
										<INPUT id="HidTipoPOS" type="hidden" name="HidTipoPOS" runat="server"> <INPUT id="HidTransMC" type="hidden" name="HidTransMC" runat="server">
										<INPUT id="HidMonedaVisa" type="hidden" name="HidMonedaVisa" runat="server"> <INPUT id="HidMonedaMC" type="hidden" name="HidMonedaMC" runat="server">
										<INPUT id="hidUsuCodCaja" type="hidden" name="hidUsuCodCaja" runat="server"> <INPUT id="HidCodOpera" type="hidden" name="HidCodOpera" runat="server">
										<INPUT id="HidDesOpera" type="hidden" name="HidDesOpera" runat="server"><INPUT id="HidPtoVenta" type="hidden" name="HidPtoVenta" runat="server">
										<INPUT id="HidDatoPosVisa" type="hidden" name="HidDatoPosVisa" runat="server"><INPUT id="HidDatoPosMC" type="hidden" name="HidDatoPosMC" runat="server">
										<INPUT id="HidTipoMoneda" type="hidden" name="HidTipoMoneda" runat="server"><INPUT id="HidIdCabez" type="hidden" name="HidIdCabez" runat="server">
										<INPUT id="HidDatoAuditPos" type="hidden" name="HidDatoAuditPos" runat="server"><INPUT id="HidApliPOS" type="hidden" name="HidApliPOS" runat="server">
										<INPUT id="HidIntAutPos" type="hidden" name="HidIntAutPos" runat="server"><INPUT id="HidTipoTransAP" type="hidden" name="HidTipoTransAP" runat="server">
										<INPUT id="HidTipPagONF" type="hidden" name="HidTipPagONF" runat="server"><INPUT id="HidTipoTran" type="hidden" name="HidTipoTran" runat="server">
										<INPUT id="hidMsjCajero" type="hidden" name="hidMsjCajero" runat="server">
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</asp:panel></form>
	</body>
</HTML>
