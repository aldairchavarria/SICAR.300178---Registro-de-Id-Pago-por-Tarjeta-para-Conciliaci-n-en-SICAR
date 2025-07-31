Public Class mntCajeroxSubOf
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

    Protected WithEvents cboSubCodOficina As System.Web.UI.HtmlControls.HtmlSelect
    Protected WithEvents cboCodCajero As System.Web.UI.HtmlControls.HtmlSelect
    Protected WithEvents cboEstado As System.Web.UI.HtmlControls.HtmlSelect
    Protected WithEvents txtComentario As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtCreaUsuario As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtCreaFecha As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtModUsuario As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtModFecha As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents hdnCodOficina As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnDesOficina As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnOpcion As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnUsuario As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnID As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnCodSubOficina As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
#End Region

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
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " btnGrabar_Click: Inicio")
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " btnGrabar_Click: Iniciar método Mantenimiento")
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " btnGrabar_Click: Variable input strOpcion, " & Me.hdnOpcion.Value)
        Call Mantenimiento(Me.hdnOpcion.Value)
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " btnGrabar_Click: Fin")
    End Sub
#End Region
#Region "Metodos"
    Private Sub Inicio()
        Me.hdnOpcion.Value = Funciones.CheckStr(Request.QueryString("strOption"))
        Me.hdnID.Value = Funciones.CheckStr(Request.QueryString("strID"))
        Me.hdnCodOficina.Value = Funciones.CheckStr(Request.QueryString("strCodOficina"))
        Me.hdnCodSubOficina.Value = Funciones.CheckStr(Request.QueryString("strCodSubOficina"))
        Me.hdnDesOficina.Value = Funciones.CheckStr(Request.QueryString("strDesOficina"))
        Me.hdnUsuario.Value = Funciones.CheckStr(Session("USUARIO"))

        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Page_Load: Inicio mntCajeroxSubOf.aspx")
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Inicio: Inicio")
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Inicio: Iniciar método, Limpiar")
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Inicio: Variable Input strOpcion, " & Me.hdnOpcion.Value)
        Call Limpiar(Me.hdnOpcion.Value)
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Inicio: Fin")
    End Sub

    Private Sub Limpiar(ByVal strOpcion As String)
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Limpiar: Inicio")
        If strOpcion = "Insert" Then
            Me.cboSubCodOficina.Items.Insert(0, Me.hdnDesOficina.Value)
            Me.cboCodCajero.Disabled = False
            Call LlenarCajero(Me.cboCodCajero, Me.hdnCodOficina.Value, String.Empty)
            Me.txtComentario.Value = ""
            Me.txtCreaFecha.Value = ""
            Me.txtCreaUsuario.Value = ""
            Me.txtModFecha.Value = ""
            Me.txtModUsuario.Value = ""
        Else
            Me.cboCodCajero.Disabled = True
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Limpiar: Iniciar método Regitro")
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Limpiar: Variable Input strID, " & Me.hdnID.Value)
            Call Regitro(Me.hdnID.Value)
        End If
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Limpiar: Fin")
    End Sub

    Private Sub LlenarCajero(ByVal cbo As System.Web.UI.HtmlControls.HtmlSelect, ByVal strCodOficina As String, ByVal strValue As String)
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " LlenarCajero: Inicio")
        Dim oclsAdmCajas As New COM_SIC_Adm_Cajas.clsAdmCajas
        Dim dsCajero As New DataSet
        Dim dtCajero As New DataTable
        Dim strRsptaCode As String = String.Empty
        Dim strRsptaMsg As String = String.Empty
        Dim intIndex As Integer = 0

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " LlenarCajero: Iniciar método, GetVendedores")
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " LlenarCajero: Variable Input usuario, ")
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " LlenarCajero: Variable Input oficina, " & strCodOficina)
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " LlenarCajero: Variable Input rol, C")
            dsCajero = oclsAdmCajas.GetVendedores("", strCodOficina, "C")
            If Not dsCajero Is Nothing And dsCajero.Tables.Count > 0 Then
                dtCajero = dsCajero.Tables(0)
                cbo.DataSource = dtCajero
                cbo.DataTextField = "DESCRIPCION"
                cbo.DataValueField = "CODIGO"
                cbo.DataBind()
                cbo.Items.Insert(0, "Seleccionar")

                objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " LlenarCajero: Valor seleccionado del combo, " & strValue)
                If strValue <> String.Empty Then
                    intIndex = cbo.Items.IndexOf(cbo.Items().FindByValue(strValue))
                Else
                    intIndex = cbo.Items.IndexOf(cbo.Items().FindByValue("Seleccionar"))
                End If
                cbo.SelectedIndex() = intIndex
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " LlenarCajero: No existen registros de cajero")
                Me.RegisterStartupScript("", "<script language=javascript>alert('" & "LlenarCajero - " & "No existen registros de cajero" & "');</script>")
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " LlenarCajero: Error, " & ex.Message)
            Me.RegisterStartupScript("", "<script language=javascript>alert('" & "LlenarCajero - " & ex.Message.ToString().Replace("'", " ") & "');</script>")
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " LlenarCajero: Fin")
        End Try
    End Sub

    Private Sub Mantenimiento(ByVal strOpcion As String)
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Mantenimiento: Inicio")
        Dim oMantenimiento As New COM_SIC_Activaciones.clsRecaudacionDAC
        Dim strRsptaCode As String = String.Empty
        Dim strRsptaMsg As String = String.Empty
        Dim intResult As Integer = 0
        Dim strValidate As String = String.Empty
        Dim blnValidate As Boolean = True
        Dim strAuditoria As String = String.Empty

        Try
            If Me.hdnCodSubOficina.Value = String.Empty Then
                strValidate = " - Ingresar Sub Oficina \n"
                blnValidate = False
            End If

            If Me.cboCodCajero.SelectedIndex() = 0 Then
                strValidate += " - Seleccionar Cajero \n"
                blnValidate = False
            End If

            If Funciones.CheckStr(Me.cboEstado.Value) = String.Empty Then
                strValidate += " - Seleccionar Estado \n"
                blnValidate = False
            End If

            If blnValidate = True Then
                Dim strID As String = Funciones.CheckStr(Me.hdnID.Value)
                Dim strSubOficina As String = Me.hdnCodSubOficina.Value
                Dim strCajero As String = Funciones.CheckStr(Funciones.CheckInt(Me.cboCodCajero.Value))
                Dim strComentario As String = Funciones.CheckStr(Me.txtComentario.Value)
                Dim strEstado As String = Me.cboEstado.Value
                Dim strUsuario As String = Me.hdnUsuario.Value

                objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Mantenimiento: Variable input strOpcion, " & strOpcion)
                objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Mantenimiento: Variable input strID, " & strID)
                objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Mantenimiento: Variable input strSubOficina, " & strSubOficina)
                objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Mantenimiento: Variable input strCajero, " & strCajero)
                objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Mantenimiento: Variable input strComentario, " & strComentario)
                objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Mantenimiento: Variable input strEstado, " & strEstado)
                objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Mantenimiento: Variable input strUsuario, " & strUsuario)

                If strOpcion = "Insert" Then
                    strAuditoria = "InsertarCajeroDAC - Insertar cajero sub oficina"
                    objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Mantenimiento: Iniciar método, InsertarCajeroDAC")
                    intResult = oMantenimiento.InsertarCajeroDAC(strSubOficina, strCajero, strComentario, strEstado, strUsuario, strRsptaCode, strRsptaMsg)
                    If strRsptaCode = "0" Then
                        Response.Write("<script language=jscript> alert('Mantenimiento Cajero: Registro guardado correctamente'); </script>")
                        Response.Write("<script language=jscript> var oResponse = { blnRefresh:true }; window.returnValue = oResponse; window.close(); </script>")
                    Else
                        Response.Write("<script language=jscript> alert('" + "Mantenimiento Cajero: " + strRsptaMsg.Replace("'", "") + "'); </script>")
                    End If

                Else
                    strAuditoria = "InsertarCajeroDAC - Modificar cajero sub oficina"
                    objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Mantenimiento: Iniciar método, ModificarCajeroDAC")
                    intResult = oMantenimiento.ModificarCajeroDAC(strID, strSubOficina, strCajero, strComentario, strEstado, strUsuario, strRsptaCode, strRsptaMsg)
                    If strRsptaCode = "0" Then
                        Response.Write("<script language=jscript> alert('Mantenimiento Cajero: Registro modificado correctamente'); </script>")
                        Response.Write("<script language=jscript> var oResponse = { blnRefresh:true }; window.returnValue = oResponse; window.close(); </script>")
                    Else
                        Response.Write("<script language=jscript> alert('" + "Mantenimiento Cajero: " + strRsptaMsg.Replace("'", "") + "'); </script>")
                    End If
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Mantenimiento: Código respuesta, " & strRsptaCode)
                objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Mantenimiento: Mensaje respuesta, " & strRsptaMsg)
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Mantenimiento: Validar, " & strValidate)
                Response.Write("<script language=jscript> alert('" + "Mantenimiento Cajero: \n" + strValidate + "'); </script>")
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Mantenimiento: Error, " & ex.Message)
            Response.Write("<script language=jscript> alert('" + "Mantenimiento Cajero: " + ex.Message + "'); </script>")
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Mantenimiento: Fin")
            RegistrarAuditoria(strAuditoria, strCodSubOfiAuditoria)
        End Try
    End Sub

    Private Sub Regitro(ByVal strID As String)
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Regitro: Inicio")
        Dim oCajeroDAC As New COM_SIC_Activaciones.clsRecaudacionDAC
        Dim dtCajeroDAC As New DataTable
        Dim strRsptaCode As String = String.Empty
        Dim strRsptaMsg As String = String.Empty
        Dim strCajero As String = String.Empty
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Regitro: Iniciar método, ConsultarCajeroDAC")
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Regitro: Variable input str_ID, " & strID)
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Regitro: Variable input str_SubOficina, ")
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Regitro: Variable input str_Cajero, ")
            dtCajeroDAC = oCajeroDAC.ConsultarCajeroDAC(strID, String.Empty, String.Empty, strRsptaCode, strRsptaMsg)
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Regitro: Código de respuesta, " & strRsptaCode)
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Regitro: Mensaje de respuesta, " & strRsptaMsg)
            If strRsptaCode = "0" Then
                Me.hdnCodSubOficina.Value = Funciones.CheckStr(dtCajeroDAC.Rows(0).Item("CASOV_SUB_OFICINA"))
                Me.cboSubCodOficina.Items.Insert(0, Funciones.CheckStr(dtCajeroDAC.Rows(0).Item("SUB_OFICINA_DESC")))
                strCajero = Funciones.CheckStr(dtCajeroDAC.Rows(0).Item("CASOC_CAJERO")).PadLeft(10, CChar("0"))
                Call LlenarCajero(Me.cboCodCajero, Me.hdnCodOficina.Value, strCajero)
                Me.txtComentario.Value = Funciones.CheckStr(dtCajeroDAC.Rows(0).Item("CASOV_COMENTARIO"))
                Me.txtCreaFecha.Value = Funciones.CheckStr(dtCajeroDAC.Rows(0).Item("CASOD_FECHA_CREA"))
                Me.txtCreaUsuario.Value = Funciones.CheckStr(dtCajeroDAC.Rows(0).Item("CASOV_USER_CREA"))
                Me.txtModFecha.Value = Funciones.CheckStr(dtCajeroDAC.Rows(0).Item("CASOD_FECHA_MODI"))
                Me.txtModUsuario.Value = Funciones.CheckStr(dtCajeroDAC.Rows(0).Item("CASOV_USER_MODI"))
            Else
                Response.Write("<script language=jscript> alert('" + "Regitro Cajero: " + strRsptaMsg.Replace("'", "") + "'); </script>")
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Regitro: Error, " & ex.Message)
            Response.Write("<script language=jscript> alert('" + "Regitro Cajero: " + ex.Message + "'); </script>")
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnCodOficina.Value & " - " & Me.hdnUsuario.Value & " Regitro: Fin")
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
