Public Class sicar_popup_MantRecargaVirtual
    Inherits SICAR_WebBase

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtCodigo As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboEstado As System.Web.UI.HtmlControls.HtmlSelect
    Protected WithEvents cmdAceptar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents btnCancelar As System.Web.UI.WebControls.Button
    Protected WithEvents txtUsuarioCreacion As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFechaCreacion As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtUsuarioModificacion As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFechaModificacion As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtComentario As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlOficinaVenta As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCajero As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCaja As System.Web.UI.WebControls.DropDownList
    Protected WithEvents chkActivo As System.Web.UI.WebControls.CheckBox
    Protected WithEvents hidRpta As System.Web.UI.HtmlControls.HtmlInputHidden

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogPOS")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogPOS")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Public strLlamado As String = String.Empty
    Public idPCajVir As String = String.Empty
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not Page.IsPostBack Then


            strLlamado = Funciones.CheckStr(Request.QueryString("accion"))
            If strLlamado = "Editar" Then
                idPCajVir = Funciones.CheckStr(Request.QueryString("id"))
                CargarInformacionCajeroVirtual(idPCajVir)



            ElseIf strLlamado = "Nuevo" Or strLlamado = String.Empty Then
                CargarNuevoCajeroVirtual()
            End If
            'cargarInformacionTarjeta()

        End If

    End Sub
    Private Sub CargarInformacionCajeroVirtual(ByVal id As String)

        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Dim dsCajeroVirtual As New System.Data.DataSet
        Dim dtCajeroVirtual As New System.Data.DataTable
        Dim CodRpta As String = String.Empty
        Dim MsjRpta As String = String.Empty

        Dim OficinaVenta As String
        Dim Cajero As String
        Dim Caja As String
        Dim UsuaCrea As String
        Dim FechaCrea As String
        Dim UsuaMod As String
        Dim FechaMod As String
        Dim Estado As String


        If Funciones.CheckStr(id) = String.Empty Then
            Response.Write("<script>alert('Ingrese datos para la busqueda.')</script>")
        End If

        dsCajeroVirtual = objOffline.Get_MantCajeroVirtualPorID(id, CodRpta, MsjRpta)
        If Not dsCajeroVirtual.Tables Is Nothing And dsCajeroVirtual.Tables.Count > 0 Then

            dtCajeroVirtual = dsCajeroVirtual.Tables(0)
            OficinaVenta = Funciones.CheckStr(dtCajeroVirtual.Rows(0).Item("CODPDV"))
            LlenaComboOficinaVenta(OficinaVenta)
            Cajero = Funciones.CheckStr(dtCajeroVirtual.Rows(0).Item("CODCAJERO"))
            Caja = Funciones.CheckStr(dtCajeroVirtual.Rows(0).Item("IDCAJA"))
            UsuaCrea = Funciones.CheckStr(dtCajeroVirtual.Rows(0).Item("USUARIOREG"))
            FechaCrea = Funciones.CheckStr(dtCajeroVirtual.Rows(0).Item("FECHAREG"))
            UsuaMod = Funciones.CheckStr(dtCajeroVirtual.Rows(0).Item("USUARIOMOD"))
            FechaMod = Funciones.CheckStr(dtCajeroVirtual.Rows(0).Item("FECHAMOD"))
            Estado = Funciones.CheckStr(dtCajeroVirtual.Rows(0).Item("ESTADO"))

            ddlOficinaVenta_SelectedIndexChanged(Nothing, Nothing)

            ddlCajero.SelectedValue = Cajero
            ddlCaja.SelectedValue = Caja

            chkActivo.Checked = IIf(Estado = "1", True, False)
            txtUsuarioCreacion.Text = UsuaCrea
            txtFechaCreacion.Text = FechaCrea
            txtUsuarioModificacion.Text = UsuaMod
            txtFechaModificacion.Text = FechaMod


        Else

            Response.Write("<script>alert('No existen registros para la consulta.')</script>")
        End If

    End Sub

    Private Sub CargarNuevoCajeroVirtual()
        ddlCajero.Enabled = False
        ddlCaja.Enabled = False
        LlenaComboOficinaVenta("")


    End Sub
    Private Sub LlenaComboOficinaVenta(ByVal codOficina As String)

        Dim dtEstado As New System.Data.DataTable
        Dim objOficinas As New COM_SIC_OffLine.clsOffline
        Dim dsOficinas As DataSet

        dsOficinas = objOficinas.Get_ConsultaOficinaVenta("", "")
        dtEstado = dsOficinas.Tables(0)


        ddlOficinaVenta.DataSource = dtEstado
        ddlOficinaVenta.DataValueField = "VKBUR"
        ddlOficinaVenta.DataTextField = "BEZEI"
        ddlOficinaVenta.DataBind()
        ddlOficinaVenta.Items.Insert(0, "SELECCIONE")

        If codOficina <> String.Empty Then
            ddlOficinaVenta.SelectedValue = codOficina
        End If

    End Sub



    Private Sub cmdAceptar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAceptar.ServerClick
        Dim sMensaje As String = String.Empty
        Dim strCodErr As String = String.Empty
        Dim strMsjErr As String = String.Empty

        Dim OficinaVenta As String = String.Empty
        Dim Cajero As String = String.Empty
        Dim Caja As String = String.Empty
        Dim Usuario As String = String.Empty
        Dim idCajeroVirtual As String = String.Empty
        Dim Estado As String = String.Empty

        Try

            OficinaVenta = Funciones.CheckStr(ddlOficinaVenta.SelectedValue)
            Cajero = Funciones.CheckStr(ddlCajero.SelectedValue)
            Caja = Funciones.CheckStr(ddlCaja.SelectedValue)
            Estado = IIf(chkActivo.Checked, "1", "0")
            Usuario = Funciones.CheckStr(Session("USUARIO"))


            OficinaVenta = IIf(OficinaVenta = "SELECCIONE", String.Empty, OficinaVenta)
            Cajero = IIf(Cajero = "SELECCIONE", String.Empty, Cajero)
            Caja = IIf(Caja = "SELECCIONE", String.Empty, Caja)

            strLlamado = Funciones.CheckStr(Request.QueryString("accion"))
            idPCajVir = Funciones.CheckStr(Request.QueryString("id"))

            If strLlamado = "Editar" Then
                idCajeroVirtual = idPCajVir
            Else
                idCajeroVirtual = "0"
            End If

            If OficinaVenta.Trim = "" Or Cajero.Trim = "" Or Caja.Trim = "" Then
                Response.Write("<script>alert('Verifique los campos ingresados');</script>")
                Return
            End If

            Dim objOffline As New COM_SIC_OffLine.clsOffline

            objFileLog.Log_WriteLog(pathFile, strArchivo, "btnGrabar_Click")

            objFileLog.Log_WriteLog(pathFile, strArchivo, "btnGrabar_Click : " & "IdCajeroVirtual : " & idCajeroVirtual)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "btnGrabar_Click : " & "Cajero : " & ddlCajero.SelectedValue & " - " & ddlCajero.SelectedItem.ToString)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "btnGrabar_Click : " & "Caja : " & ddlCaja.SelectedValue & " - " & ddlCaja.SelectedItem.ToString)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "btnGrabar_Click : " & "OficinaVenta : " & ddlOficinaVenta.SelectedValue & " - " & ddlOficinaVenta.SelectedItem.ToString)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "btnGrabar_Click : " & "Usuario: " & Session("USUARIO"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, "btnGrabar_Click : " & "Estado: " & Estado)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "btnGrabar_Click : " & "flagOperacion: " & strLlamado)

            Dim bValor As Boolean = objOffline.Get_Registro_MantCajeroVirtual(idCajeroVirtual, OficinaVenta, Cajero, Caja, Usuario, Estado, strCodErr, strMsjErr)

            objFileLog.Log_WriteLog(pathFile, strArchivo, "btnGrabar_Click : " & "strCodErr: " & strCodErr)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "btnGrabar_Click : " & "strMsjErr: " & strMsjErr)

            If strCodErr = "-2" Then
                Response.Write("<script>alert(' " & strMsjErr & "');</script>")
                Exit Sub
            Else
                If strCodErr <> "0" Then
                    Response.Write("<script>alert('Error al grabar');</script>")
                    Exit Sub
                Else
                    hidRpta.Value = "OK"
                    'Response.Write("<script>window.opener.f_respuesta('Registro grabado correctamente');</script>")
                    'Response.Write("<script>window.close();</script>")
                End If
            End If

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "error btnGuardar_Click: " & ex.Message.ToString())
            Exit Sub
        Finally

            sMensaje = "RegistrarCajeroVirtual - Registro"

            'RegistrarAuditoria(sMensaje, ConfigurationSettings.AppSettings("ConsMantCodTar_codTrsAuditoria"))

            objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin btnGrabar_Click")
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
            ' Throw New Exception("Error Registrar Auditoria.")
        End Try
    End Sub

    Private Sub ddlOficinaVenta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOficinaVenta.SelectedIndexChanged
        LlenarCajero(ddlOficinaVenta.SelectedValue)
        LlenarCaja(ddlOficinaVenta.SelectedValue)
    End Sub
    Public Sub LlenarCajero(ByVal ValorCombo As String)
        If ValorCombo <> "0" Then
            ddlCajero.Enabled = True
            Dim objOffline As New COM_SIC_OffLine.clsOffline
            Dim dsCajeros As DataSet
            dsCajeros = objOffline.Get_ConsultaCajeros(ddlOficinaVenta.SelectedValue, "C")
            If Not dsCajeros Is Nothing Then
                ddlCajero.DataTextField = "NOMBRE"
                ddlCajero.DataValueField = "USUARIO"
                ddlCajero.DataSource = dsCajeros.Tables(0)
                ddlCajero.DataBind()
                ddlCajero.Items.Insert(0, "SELECCIONE")
            End If
        Else
            ddlCajero.Items.Insert(0, "SELECCIONE")
            ddlCajero.Enabled = False
        End If
        'dsCajas = objOffline.Get_CajaOficinas(Session("ALMACEN"))
    End Sub
    Public Sub LlenarCaja(ByVal ValorCombo As String)
        If ValorCombo <> "0" Then
            ddlCaja.Enabled = True
            Dim objOffline As New COM_SIC_OffLine.clsOffline
            Dim dsCaja As DataSet
            dsCaja = objOffline.Get_CajaOficinas(ValorCombo)
            If Not dsCaja Is Nothing Then
                ddlCaja.DataTextField = "BEZEI"
                ddlCaja.DataValueField = "CASNR"
                ddlCaja.DataSource = dsCaja.Tables(0)
                ddlCaja.DataBind()
                ddlCaja.Items.Insert(0, "SELECCIONE")
            End If
        Else
            ddlCaja.Items.Insert(0, "SELECCIONE")
            ddlCaja.Enabled = False
        End If
        'dsCajas = objOffline.Get_CajaOficinas(Session("ALMACEN"))
    End Sub
End Class
