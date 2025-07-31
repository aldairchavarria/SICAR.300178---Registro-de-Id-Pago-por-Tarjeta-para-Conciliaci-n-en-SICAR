Public Class mntSuboficina
    Inherits SICAR_WebBase

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

    Protected WithEvents cboPtoVenta As System.Web.UI.HtmlControls.HtmlSelect
    Protected WithEvents cboCondicionPago As System.Web.UI.HtmlControls.HtmlSelect
    Protected WithEvents cboControlCredito As System.Web.UI.HtmlControls.HtmlSelect
    Protected WithEvents cboEstado As System.Web.UI.HtmlControls.HtmlSelect
    Protected WithEvents txtSubOficina As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtComentario As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtCtaContable As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtCreaUsuario As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtCreaFecha As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtModUsuario As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtModFecha As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents hdnControlCredito As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnCondicionPago As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnUsuario As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnOpcion As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnID As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnOficina As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnDefaultCC As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnDefaultCP As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
#End Region

    Private ConstSubOfi_GrupoRecaudacionDAC As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("ConstSubOfi_GrupoRecaudacionDAC"))
    Private strCodOficina As String = String.Empty
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogPOS")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogPOS")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim strCodSubOfiAuditoria As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("ConstSubOfi_Auditoria"))

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not Page.IsPostBack Then
            Call Inicio()
        End If
    End Sub

#Region "Botones"
    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " btnGrabar_Click: Inicio")
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " btnGrabar_Click: Opción, " & Me.hdnOpcion.Value)
        Call Mantenimiento(Me.hdnOpcion.Value)
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " btnGrabar_Click: Fin")
    End Sub
#End Region
#Region "Metodos"
    Private Sub Inicio()
        Me.hdnControlCredito.Value = clsKeyDAC.strControlCredito
        Me.hdnCondicionPago.Value = clsKeyDAC.strCondicionPago
        Me.hdnOficina.Value = Funciones.CheckStr(Session("ALMACEN"))
        Me.hdnUsuario.Value = Funciones.CheckStr(Session("USUARIO"))
        Me.hdnDefaultCC.Value = Funciones.CheckStr(clsKeyDAC.strDefaultCC)
        Me.hdnDefaultCP.Value = Funciones.CheckStr(clsKeyDAC.strDefaultCP)
        Me.hdnOpcion.Value = Request.QueryString("strOption")
        Me.hdnID.Value = Request.QueryString("strID")

        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Page_Load: Inicio mntSuboficina.aspx")

        Call Limpiar(Me.hdnOpcion.Value)
    End Sub

    Private Sub LlenarCombo(ByVal cbo As System.Web.UI.HtmlControls.HtmlSelect, ByVal strKey As String, ByVal strValue As String)
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " LlenarCombo: Inicio")
        Dim oParametros As New COM_SIC_Activaciones.clsRecaudacionDAC
        Dim dtParametro As New DataTable
        Dim strRsptaCode As String = String.Empty
        Dim strRsptaMsg As String = String.Empty
        Dim intIndex As Integer = 0
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " LlenarCombo: Grupo, " & ConstSubOfi_GrupoRecaudacionDAC)
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " LlenarCombo: Llave, " & strKey)
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " LlenarCombo: Valor, ")
            dtParametro = oParametros.ConsultarParametros(ConstSubOfi_GrupoRecaudacionDAC, strKey, String.Empty, strRsptaCode, strRsptaMsg)
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " LlenarCombo: Código respuesta, " & strRsptaCode)
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " LlenarCombo: Mensaje respuesta, " & strRsptaMsg)

            If strRsptaCode = "0" Then
                cbo.DataSource = dtParametro
                cbo.DataValueField = "SPARV_VALUE"
                cbo.DataTextField = "SPARV_VALUE2"
                cbo.DataBind()

                If strValue <> String.Empty Then
                    intIndex = cbo.Items.IndexOf(cbo.Items().FindByValue(strValue))
                    cbo.SelectedIndex() = intIndex
                End If
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " LlenarCombo: " & strRsptaMsg)
                Response.Write("<script language=jscript> alert('" + "Llenar Combo: " + strRsptaMsg + "'); </script>")
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " LlenarCombo: Error, " & ex.Message)
            Response.Write("<script language=jscript> alert('" + "Llenar Combo: " + ex.Message + "'); </script>")
        End Try

        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " LlenarCombo: Fin")
    End Sub

    Private Sub LlenarOfina(ByVal cbo As System.Web.UI.HtmlControls.HtmlSelect, ByVal strValue As String)
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " LlenarOfina: Inicio")
        Dim oclsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas
        Dim dsResult As New DataSet
        Dim dtResult As New DataTable
        Dim intIndex As Integer = 0

        Try
            dsResult = oclsAdmCaja.GetOficinas(String.Empty)
            dtResult = dsResult.Tables(0)
            Session("dgListaOficina") = dtResult
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " LlenarOfina: data por consulta, GetOficinas")

            cbo.DataSource = dtResult
            cbo.DataValueField = "CODIGO"
            cbo.DataTextField = "DESCRIPCION"
            cbo.DataBind()

            If dtResult.Rows.Count <= 0 Then
                Response.Write("<script language=jscript> alert('No se encontró datos'); </script>")
                objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " LlenarOfina: no existen datos de oficinas")
            Else
                If strValue <> String.Empty Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " LlenarOfina: valor seleccionado, " & strValue)
                    intIndex = cbo.Items.IndexOf(cbo.Items().FindByValue(strValue))
                    If intIndex = -1 Then
                        intIndex = cbo.Items.IndexOf(cbo.Items().FindByValue(Me.hdnOficina.Value))
                    End If
                    cbo.SelectedIndex() = intIndex
                End If
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " LlenarOfina: " & ex.Message)
            Response.Write("<script language=jscript> alert('" + "Llenar Ofina: " + ex.Message + "'); </script>")
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " LlenarOfina: Fin")
        End Try
    End Sub

    Private Sub Mantenimiento(ByVal strOption As String)
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Mantenimiento: Inicio")
        Dim strValidar As String = String.Empty
        Dim blnValidar As Boolean = True
        Dim oMantenimiento As New COM_SIC_Activaciones.clsRecaudacionDAC
        Dim strID As String = String.Empty
        Dim strPuntoVenta As String = String.Empty
        Dim strSubOficina As String = String.Empty
        Dim strComentario As String = String.Empty
        Dim strEstado As String = String.Empty
        Dim strCP As String = String.Empty
        Dim strCC As String = String.Empty
        Dim strCtaContable As String = String.Empty
        Dim strRsptaCode As String = String.Empty
        Dim strRsptaMsg As String = String.Empty
        Dim strUsuario As String = String.Empty
        Dim intResult As Integer = 0
        Dim strMensajeAuditoria As String = String.Empty

        Try
            If Me.txtSubOficina.Value = String.Empty Then
                strValidar = " - Ingresa Sub Oficina \n"
                blnValidar = False
            End If

            If Me.txtCtaContable.Value = String.Empty Then
                strValidar += " - Ingresa Cuenta Contable"
                blnValidar = False
            End If

            If Me.cboEstado.SelectedIndex() = 0 Then
                strValidar += " - Selecciona Estado"
                blnValidar = False
            End If

            If blnValidar Then
                strID = Funciones.CheckStr(Me.hdnID.Value)
                strPuntoVenta = Funciones.CheckStr(Me.cboPtoVenta.Value)
                strSubOficina = Funciones.CheckStr(Me.txtSubOficina.Value)
                strComentario = Funciones.CheckStr(Me.txtComentario.Value)
                strCP = Funciones.CheckStr(Me.cboCondicionPago.Value)
                strCC = Funciones.CheckStr(Me.cboControlCredito.Value)
                strCtaContable = Funciones.CheckStr(Me.txtCtaContable.Value)
                strEstado = Funciones.CheckStr(Me.cboEstado.Value)
                strUsuario = Me.hdnUsuario.Value

                objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Mantenimiento: Input strOption," & strOption)
                objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Mantenimiento: Input strID," & strID)
                objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Mantenimiento: Input strPuntoVenta," & strPuntoVenta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Mantenimiento: Input strSubOficina," & strSubOficina)
                objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Mantenimiento: Input strComentario," & strComentario)
                objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Mantenimiento: Input strCP," & strCP)
                objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Mantenimiento: Input strCC," & strCC)
                objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Mantenimiento: Input strCtaContable," & strCtaContable)
                objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Mantenimiento: Input strEstado," & strEstado)
                objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Mantenimiento: Input strUsuario," & strUsuario)

                If strOption = "Insert" Then
                    strMensajeAuditoria = "InsertarSubOficina - Insertar Sub Oficina"
                    intResult = oMantenimiento.InsertarSubOficina(strPuntoVenta, strSubOficina, strComentario, strEstado, strCP, strCC, strCtaContable, strUsuario, strRsptaCode, strRsptaMsg)
                    If strRsptaCode = "0" Then
                        Response.Write("<script language=jscript> alert('Mantenimiento: Registro guardado correctamente'); </script>")
                        Response.Write("<script language=jscript> var oResponse = { blnRefresh:true }; window.returnValue = oResponse; window.close(); </script>")
                    Else
                        Response.Write("<script language=jscript> alert('Mantenimiento: " + strRsptaMsg.Replace("'", "") + "'); </script>")
                    End If

                Else
                    strMensajeAuditoria = "InsertarSubOficina - Modificar Sub Oficina"
                    intResult = oMantenimiento.ModificarSubOficina(strID, strPuntoVenta, strSubOficina, strComentario, strEstado, strCP, strCC, strCtaContable, strUsuario, strRsptaCode, strRsptaMsg)
                    If strRsptaCode = "0" Then
                        Response.Write("<script language=jscript> alert('Mantenimiento: Registro modificado correctamente'); </script>")
                        Response.Write("<script language=jscript> var oResponse = { blnRefresh:true }; window.returnValue = oResponse; window.close(); </script>")
                    Else
                        Response.Write("<script language=jscript> alert('Mantenimiento: " + strRsptaMsg.Replace(Chr(34), "") + "'); </script>")
                    End If
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Mantenimiento: Código Respuesta, " & strRsptaCode)
                objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Mantenimiento: Mensaje Respuesta, " & strRsptaMsg)
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Mantenimiento: Validación, " & strValidar)
                Response.Write("<script language=jscript> alert('Mantenimiento: \n" + strValidar + "'); </script>")
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Mantenimiento: Error, " & ex.Message)
            Response.Write("<script language=jscript> alert('Mantenimiento: " + ex.Message + "'); </script>")
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Mantenimiento: Fin")
            RegistrarAuditoria(strMensajeAuditoria, strCodSubOfiAuditoria)
        End Try
    End Sub

    Private Sub Limpiar(ByVal strOpcion As String)
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Limpiar: Inicio")
        Dim strOfDefault As String = String.Empty

        If strOpcion = "Insert" Then
            Me.hdnID.Value = ""
            Me.txtSubOficina.Value = ""
            Me.txtComentario.Value = ""
            Me.txtCreaFecha.Value = ""
            Me.txtCreaUsuario.Value = ""
            Me.txtModFecha.Value = ""
            Me.txtModUsuario.Value = ""
            Me.txtCtaContable.Value = ""
            LlenarCombo(Me.cboControlCredito, Me.hdnControlCredito.Value, Me.hdnDefaultCC.Value)
            LlenarCombo(Me.cboCondicionPago, Me.hdnCondicionPago.Value, Me.hdnDefaultCP.Value)

            strOfDefault = Funciones.CheckStr(clsKeyDAC.strOfDefault)
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Inicio: oficina por default: " & strOfDefault)
            If (strOfDefault <> String.Empty) Then
                LlenarOfina(Me.cboPtoVenta, strOfDefault)
            Else
                LlenarOfina(Me.cboPtoVenta, Me.hdnOficina.Value)
            End If
            Me.cboEstado.SelectedIndex() = 2
        Else
            Call Registro(Me.hdnID.Value)
        End If
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Limpiar: Fin")
    End Sub

    Private Sub Registro(ByVal strID As String)
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Registro: Inicio")
        Dim oRegistro As New COM_SIC_Activaciones.clsRecaudacionDAC
        Dim dtRegistro As New DataTable
        Dim strRsptaCode As String = String.Empty
        Dim strRsptaMsg As String = String.Empty
        Dim intIndex As Integer = 0
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Registro: Consulta método ConsultarSubOficina")
            dtRegistro = oRegistro.ConsultarSubOficina(strID, String.Empty, String.Empty, strRsptaCode, strRsptaMsg)
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Registro: Código respuesta" & strRsptaCode)
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Registro: Mensaje respuesta" & strRsptaMsg)
            If strRsptaCode = "0" Then
                Me.hdnID.Value = strID
                Me.txtSubOficina.Value = Funciones.CheckStr(dtRegistro.Rows(0).Item("SUOFV_SUB_OFICINA"))
                Me.txtComentario.Value = Funciones.CheckStr(dtRegistro.Rows(0).Item("SUOFV_COMENTARIO"))
                Me.txtCtaContable.Value = Funciones.CheckStr(dtRegistro.Rows(0).Item("SUOFV_CUENTACONTABLE"))
                Me.txtCreaFecha.Value = Funciones.CheckStr(dtRegistro.Rows(0).Item("SUOFD_FECHA_CREA"))
                Me.txtCreaUsuario.Value = Funciones.CheckStr(dtRegistro.Rows(0).Item("SUOFV_USER_CREA"))
                Me.txtModFecha.Value = Funciones.CheckStr(dtRegistro.Rows(0).Item("SUOFD_FECHA_MODI"))
                Me.txtModUsuario.Value = Funciones.CheckStr(dtRegistro.Rows(0).Item("SUOFV_USER_MODI"))
                LlenarCombo(Me.cboControlCredito, Me.hdnControlCredito.Value, Funciones.CheckStr(dtRegistro.Rows(0).Item("SUOFV_CONTROLCRE")))
                LlenarCombo(Me.cboCondicionPago, Me.hdnCondicionPago.Value, Funciones.CheckStr(dtRegistro.Rows(0).Item("SUOFV_CONDIPAGO")))
                LlenarOfina(Me.cboPtoVenta, Funciones.CheckStr(dtRegistro.Rows(0).Item("SUOFC_PUNTO_VENTA")))
                intIndex = Me.cboEstado.Items.IndexOf(Me.cboEstado.Items().FindByValue(Funciones.CheckStr(dtRegistro.Rows(0).Item("SUOFV_ESTADO"))))
                Me.cboEstado.SelectedIndex() = intIndex
            Else
                Response.Write("<script language=jscript> alert('Registro: " + strRsptaMsg.Replace("'", "") + "'); </script>")
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Registro: Error, " & ex.Message)
            Response.Write("<script language=jscript> alert('Registro: " + ex.Message + "'); </script>")
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Registro: Fin")
        End Try
    End Sub

    Private Sub RegistrarAuditoria(ByVal DesTrx As String, ByVal CodTrx As String)
        Try
            Dim user As String = Me.CurrentUser
            Dim ipHost As String = CurrentTerminal
            Dim nameHost As String = System.Net.Dns.GetHostName
            Dim nameServer As String = System.Net.Dns.GetHostName
            Dim ipServer As String = System.Net.Dns.GetHostByName(nameServer).AddressList(0).ToString
            Dim hostInfo As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(nameHost)

            Dim CadMensaje As String
            Dim CodServicio As String = ConfigurationSettings.AppSettings("CONS_COD_SICAR_SERV")
            Dim oAuditoria As New COM_SIC_Activaciones.clsAuditoriaWS

            oAuditoria.RegistrarAuditoria(CodTrx, _
                                            CodServicio, _
                                            ipHost, _
                                            nameHost, _
                                            ipServer, _
                                            nameServer, _
                                            user, _
                                            "", _
                                            "0", _
                                            DesTrx)

        Catch ex As Exception

        End Try

    End Sub
#End Region
End Class
