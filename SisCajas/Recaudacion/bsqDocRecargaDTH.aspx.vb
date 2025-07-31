Public Class bsqDocRecargaDTH
    Inherits SICAR_WebBase

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents hdnMensaje As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnPuntoDeVenta As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnUsuario As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnBinAdquiriente As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnCodComercio As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents intCanal As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnRutaLog As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnDetalleLog As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents btnBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents ddlTipoDocumento As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtNroDocumento As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtIdentificador As System.Web.UI.WebControls.TextBox
    Protected WithEvents hidMensaje As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidTipoDoc As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidNumeroDoc As System.Web.UI.HtmlControls.HtmlInputHidden

    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

#End Region

    Dim objFileLog As New SICAR_Log
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRegistroDOL")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo("Log_RecargaDTH")

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Introducir aquí el código de usuario para inicializar la página
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")

        If Session("USUARIO") Is Nothing Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        End If

        ' Eventos Controles
        hidMensaje.Value = Funciones.CheckStr(Request.QueryString("strMensaje"))
        btnBuscar.Attributes.Add("OnClick", "return buscarDocRegargaDTH();")
        ddlTipoDocumento.Attributes.Add("onChange", "cambiarNroDoc(this.value);")
        txtIdentificador.Attributes.Add("onkeydown", "validarNumero(this.value);")
        txtNroDocumento.Attributes.Add("onkeydown", "validarNumero(this.value);")

        ' Numero de Documento x Defecto de Cliente Generico
        Dim dsOficina As DataSet
        'dsOficina = (New SAP_SIC_Pagos.clsPagos).Get_ParamGlobal(Session("ALMACEN"))
        Dim objOffline As New COM_SIC_OffLine.clsOffline

        'txtNroDocumento.Text = Trim(dsOficina.Tables(0).Rows(0).Item("COD_CLTE_VARIOS"))
        'dsOficina = Nothing

        dsOficina = objOffline.ParametrosVenta(Session("ALMACEN"))

        Dim isDbnull As Boolean = Convert.IsDBNull(dsOficina.Tables(0).Rows(0).Item("COD_CLTE_VARIOS"))
        Dim numDocumento$
        Dim leerClienteVarios As Boolean = (ConfigurationSettings.AppSettings("FLAG_COD_CLTE_VARIOS") = 1)

        If (leerClienteVarios) Then
            numDocumento$ = ConfigurationSettings.AppSettings("COD_CLTE_VARIOS")
        Else
            numDocumento$ = IIf(isDbnull, ConfigurationSettings.AppSettings("COD_CLTE_VARIOS"), _
                                Trim(Convert.ToString(dsOficina.Tables(0).Rows(0).Item("COD_CLTE_VARIOS"))))
        End If
        txtNroDocumento.Text = numDocumento
        ddlTipoDocumento.SelectedValue = "01"       'Asigna por defecto DNI

    End Sub

    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click

        Dim pagina As String
        Dim tipoIdentificador As String
        Dim textoIdentificador As String
        Dim prefijoDTH As String
        Dim nroRecarga As String
        Dim dsCliente As DataSet
        Dim clsConsultaPvu As COM_SIC_Activaciones.clsConsultaPvu
        Dim k_nrolog, k_deslog As String
        Dim vCliente As String = ""
        Dim posCliente As String = ""

        objFileLog.Log_WriteLog(pathFile, strArchivo, txtIdentificador.Text & " - " & "------------ INICIO BUSCAR DTH ------------")

        prefijoDTH = ConfigurationSettings.AppSettings("PrefijoDTH")
        tipoIdentificador = ConfigurationSettings.AppSettings("TipoIdentificadorDTH")
        nroRecarga = txtIdentificador.Text
        textoIdentificador = prefijoDTH & txtIdentificador.Text

        objFileLog.Log_WriteLog(pathFile, strArchivo, txtIdentificador.Text & " - " & " Prefijo DTH: " & prefijoDTH)
        objFileLog.Log_WriteLog(pathFile, strArchivo, txtIdentificador.Text & " - " & " Tipo Identificador: " & tipoIdentificador)
        objFileLog.Log_WriteLog(pathFile, strArchivo, txtIdentificador.Text & " - " & " Nro Recarga: " & nroRecarga)
        objFileLog.Log_WriteLog(pathFile, strArchivo, txtIdentificador.Text & " - " & " Identificador: " & textoIdentificador)

        Dim dsOficina As DataSet
        'dsOficina = (New SAP_SIC_Pagos.clsPagos).Get_ParamGlobal(Session("ALMACEN"))
        Dim objOffline As New COM_SIC_OffLine.clsOffline

        dsOficina = objOffline.ParametrosVenta(Session("ALMACEN"))

        Dim isDbnull As Boolean = Convert.IsDBNull(dsOficina.Tables(0).Rows(0).Item("COD_CLTE_VARIOS"))
        Dim numDocumento$
        Dim leerClienteVarios As Boolean = (ConfigurationSettings.AppSettings("FLAG_COD_CLTE_VARIOS") = 1)

        If (leerClienteVarios) Then
            numDocumento$ = ConfigurationSettings.AppSettings("COD_CLTE_VARIOS")
        Else
            numDocumento$ = IIf(isDbnull, ConfigurationSettings.AppSettings("COD_CLTE_VARIOS"), _
                                Trim(Convert.ToString(dsOficina.Tables(0).Rows(0).Item("COD_CLTE_VARIOS"))))
        End If

        '***Busqueda del Cliente en PVUDB***
        Session("datosCliente") = ""
        objFileLog.Log_WriteLog(pathFile, strArchivo, txtIdentificador.Text & " - " & " INICIO Consulta Cliente - Metodo: consultaDatosCliente - SP: ssapss_cliente")
        objFileLog.Log_WriteLog(pathFile, strArchivo, txtIdentificador.Text & " - " & "     IN [Tipo Doc: " & hidTipoDoc.Value & "], [Nro Doc: " & hidNumeroDoc.Value & "]")
        dsCliente = (New COM_SIC_Activaciones.clsConsultaPvu).consultaDatosCliente(hidTipoDoc.Value, hidNumeroDoc.Value, k_nrolog, k_deslog)
        If Not IsNothing(dsCliente) Then
            If dsCliente.Tables(0).Rows.Count > 0 Then
                hidNumeroDoc.Value = Funciones.CheckStr(dsCliente.Tables(0).Rows(0).Item("CLIEV_NRO_DOCUMENTO"))
                For i As Integer = 0 To dsCliente.Tables(0).Columns.Count - 1
                    vCliente = vCliente & ";" & Funciones.CheckStr(dsCliente.Tables(0).Rows(0).Item(i))
                Next
                Session("datosCliente") = vCliente
                objFileLog.Log_WriteLog(pathFile, strArchivo, txtIdentificador.Text & " - " & "     OUT [Cliente encontrado en PVUDB][" & k_nrolog & "][" & k_deslog & "]")
            Else
                If hidTipoDoc.Value = "06" Then
                    hidNumeroDoc.Value = ConfigurationSettings.AppSettings("COD_CLTE_VARIOS_RUC")
                ElseIf hidTipoDoc.Value = "04" Then
                    hidNumeroDoc.Value = ConfigurationSettings.AppSettings("COD_CLTE_VARIOS_CE")
                Else
                    hidNumeroDoc.Value = numDocumento
                End If
            End If
        Else
            If hidTipoDoc.Value = "06" Then
                hidNumeroDoc.Value = ConfigurationSettings.AppSettings("COD_CLTE_VARIOS_RUC")
            ElseIf hidTipoDoc.Value = "04" Then
                hidNumeroDoc.Value = ConfigurationSettings.AppSettings("COD_CLTE_VARIOS_CE")
            Else
                hidNumeroDoc.Value = numDocumento
            End If
        End If
        objFileLog.Log_WriteLog(pathFile, strArchivo, txtIdentificador.Text & " - " & "     OUT [ Nro Doc Cliente: " & hidNumeroDoc.Value & " ]")
        objFileLog.Log_WriteLog(pathFile, strArchivo, txtIdentificador.Text & " - " & " FIN Consulta Cliente - Metodo: consultaDatosCliente - SP: ssapss_cliente")

        pagina = "detDocRecargaDTH.aspx"
        pagina += "?tipoIdentificador=" + tipoIdentificador
        pagina += "&textoIdentificador=" + textoIdentificador
        pagina += "&tipoDocCliente=" + hidTipoDoc.Value
        pagina += "&nroDocCliente=" + hidNumeroDoc.Value
        pagina += "&nroRecarga=" + nroRecarga

        objFileLog.Log_WriteLog(pathFile, strArchivo, txtIdentificador.Text & " - " & " Redirecciona : " & pagina)
        objFileLog.Log_WriteLog(pathFile, strArchivo, txtIdentificador.Text & " - " & "------------ FIN BUSCAR DTH ------------")
        Response.Redirect(pagina)
    End Sub
End Class
