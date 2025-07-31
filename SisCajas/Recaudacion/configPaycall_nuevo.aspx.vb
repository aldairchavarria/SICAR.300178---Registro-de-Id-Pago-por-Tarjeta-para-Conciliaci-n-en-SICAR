Public Class configPaycall_nuevo
    Inherits SICAR_WebBase

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTitNuevo As System.Web.UI.WebControls.Label
    Protected WithEvents lblOficina As System.Web.UI.WebControls.Label
    Protected WithEvents cboOficina As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cmdGrabar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents lblPaycall As System.Web.UI.WebControls.Label
    Protected WithEvents txtPaycall As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents lblTipoRemesa As System.Web.UI.WebControls.Label
    Protected WithEvents cboTipoRemesa As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblCtaMayor As System.Web.UI.WebControls.Label
    Protected WithEvents txtCtaMayor As System.Web.UI.HtmlControls.HtmlInputText

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Variables"
    Dim objclsAdmCaja As COM_SIC_Adm_Cajas.clsAdmCajas
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogConfigPaycall")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogConfigPaycall")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim strUsuario As String
    Dim strIdentifyLog As String
    Private Const TIPO_REMESA As String = "TIPO_REMESA"
    Protected MensajeAudi As String
#End Region

#Region "Eventos"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            If Not Page.IsPostBack Then
                Dim dsOficinas As DataSet
                Dim strOficina As String = CStr(Session("strCPOficinaID"))
                objclsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas
                dsOficinas = objclsAdmCaja.GetOficinas(strOficina)

                cboOficina.DataSource = dsOficinas.Tables(0)
                cboOficina.DataTextField = "DESCRIPCION"
                cboOficina.DataValueField = "CODIGO"
                cboOficina.DataBind()

                txtPaycall.Attributes.Add("onkeydown", "validarNumero(this.value);")
                CargarTipoRemesa()

                If Not Request.QueryString("CodCtaRemesa") Is Nothing Then
                    If CInt(Request.QueryString("CodCtaRemesa")) = 0 Then 'Nuevo
                        txtCtaMayor.Value = String.Empty
                        cboTipoRemesa.SelectedIndex = 0
                        cboOficina.SelectedIndex = 0
                        cboOficina.Enabled = False
                        txtPaycall.Value = String.Empty
                    Else 'Editar
                        cboOficina.Enabled = False
                        CargarDatos(CLng(Request.QueryString("CodCtaRemesa")))
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub cmdGrabar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGrabar.ServerClick
        strUsuario = Session("USUARIO")
        strIdentifyLog = Session("ALMACEN") & "|" & strUsuario
        Dim strCodTrxAud As String
        Try
            Dim strMsjErr As String = String.Empty
            Dim strMsg As String = String.Empty

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Ini cmdGrabar_ServerClick")

            Dim lCodCtaRemesa As Integer = CInt(Request.QueryString("CodCtaRemesa"))
            objclsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas

            If Trim(cboOficina.SelectedValue).Equals(String.Empty) Then
                Response.Write("<script>alert('Seleccione oficina');</script>")
                Exit Sub
            End If

            If Trim(txtPaycall.Value) = "" Then
                Response.Write("<script>alert('Ingrese Paycall');</script>")
                Exit Sub
            End If

            If Trim(cboTipoRemesa.SelectedValue).Equals("0") Then
                Response.Write("<script>alert('Seleccione el tipo de remesa');</script>")
                Exit Sub
            End If

            If Trim(txtCtaMayor.Value) = "" Then
                Response.Write("<script>alert('Ingrese el número de cuenta');</script>")
                Exit Sub
            End If

            Dim strOfiVta As String = cboOficina.SelectedValue
            Dim bEditar As Boolean = False

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   In strOfiVta: " & strOfiVta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   In Paycall: " & CStr(txtPaycall.Value))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   In TipoRemesa: " & CStr(cboTipoRemesa.SelectedValue))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   In Cuenta: " & CStr(txtCtaMayor.Value))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   In Codigo Remesa: " & lCodCtaRemesa)
            If lCodCtaRemesa = 0 Then ' nuevo
                strMsjErr = objclsAdmCaja.SetCtaRemesa(strOfiVta, CStr(txtPaycall.Value), CStr(cboTipoRemesa.SelectedValue), CStr(txtCtaMayor.Value))
                bEditar = False
                strCodTrxAud = ConfigurationSettings.AppSettings("CodAuditGrabarConfigPaycall")
            Else 'editar
                strMsjErr = objclsAdmCaja.ActualizarCtaRemesa(lCodCtaRemesa, strOfiVta, CStr(txtPaycall.Value), CStr(cboTipoRemesa.SelectedValue), CStr(txtCtaMayor.Value))
                bEditar = True
                strCodTrxAud = ConfigurationSettings.AppSettings("CodAuditEditarConfigPaycall")
            End If

            If Not strMsjErr.Equals(String.Empty) Then
                Response.Write("<script>alert('" & strMsjErr & "');</script>")
                Exit Sub
            Else
                If bEditar Then
                    strMsg = "Se guardó los cambios correctamente."
                Else
                    strMsg = "Se registró correctamente."
                End If
            End If
            Response.Write("<script>window.opener.f_Refrescar();</script>")
            Response.Write("<script language=jscript> alert('" + strMsg + "'); </script>")
            Response.Write("<script>window.close();</script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Mensaje: " & strMsg)
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: " & ex.Message)
        Finally
            objclsAdmCaja = Nothing
            MensajeAudi = "Configuracion de Paycall. Grabar : " & strIdentifyLog
            RegistrarAuditoria(MensajeAudi, strCodTrxAud)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN  cmdGrabar_ServerClick")
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub CargarDatos(ByVal IdCtaRemesa As Long)
        objclsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas
        strUsuario = Session("USUARIO")
        strIdentifyLog = Session("ALMACEN") & "|" & strUsuario
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Ini CargarDatos")
            Dim dsDatos As DataSet
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Ini GetCtaRemesa")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP IDCtaRemesa :" & IdCtaRemesa.ToString())
            dsDatos = objclsAdmCaja.GetCtaRemesa(String.Empty, IdCtaRemesa)

            If Not dsDatos Is Nothing Then
                Dim drDatos As DataRow
                drDatos = dsDatos.Tables(0).Rows(0)
                If Not drDatos Is Nothing Then
                    If Not IsDBNull(drDatos("COD_OFICINA")) Then
                        cboOficina.SelectedValue = drDatos("COD_OFICINA")
                    End If
                    If Not IsDBNull(drDatos("PAYCALL")) Then
                        txtPaycall.Value = drDatos("PAYCALL")
                    End If

                    If Not IsDBNull(drDatos("TIPO_REMESA")) Then
                        cboTipoRemesa.SelectedValue = drDatos("TIPO_REMESA")
                    End If

                    If Not IsDBNull(drDatos("CTABANCO")) Then
                        txtCtaMayor.Value = drDatos("CTABANCO")
                    End If
                End If
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- ERROR:" & ex.Message)
        Finally
            objclsAdmCaja = Nothing
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin CargarDatos")
        End Try
    End Sub

    Private Sub CargarTipoRemesa()
        strUsuario = Session("USUARIO")
        strIdentifyLog = Session("ALMACEN") & "|" & strUsuario
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio  CargarTipoRemesa")
            objclsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas
            Dim dsTipos As DataSet = objclsAdmCaja.GetCodigos(TIPO_REMESA, String.Empty)
            Dim dtResult As New DataTable
            With dtResult
                .Columns.Add("CODIGO", GetType(String))
                .Columns.Add("DESCRIPCION", GetType(String))
            End With

            Dim dr As DataRow
            For Each item As DataRow In dsTipos.Tables(0).Rows
                dr = dtResult.NewRow
                dr("CODIGO") = Trim(item("codigo"))
                dr("DESCRIPCION") = Trim(item("codigo")) & " - " & Trim(item("descripcion"))
                dtResult.Rows.Add(dr)
            Next
            dtResult.AcceptChanges()

            cboTipoRemesa.DataSource = dtResult
            cboTipoRemesa.DataTextField = "DESCRIPCION"
            cboTipoRemesa.DataValueField = "CODIGO"
            cboTipoRemesa.DataBind()

            cboTipoRemesa.Items.Insert(0, New ListItem("Seleccionar", "0"))
            cboTipoRemesa.SelectedIndex = 0
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN  CargarTipoRemesa")
            objclsAdmCaja = Nothing
        End Try
    End Sub

    Private Sub RegistrarAuditoria(ByVal DesTrx As String, ByVal CodTrx As String)
        Dim oAuditoria As COM_SIC_Activaciones.clsAuditoriaWS
        Try
            Dim user As String = CurrentUser
            Dim ipHost As String = CurrentTerminal
            Dim nameHost As String = System.Net.Dns.GetHostName
            Dim nameServer As String = System.Net.Dns.GetHostName
            Dim ipServer As String = System.Net.Dns.GetHostByName(nameServer).AddressList(0).ToString
            Dim hostInfo As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(nameHost)

            Dim CodServicio As String = ConfigurationSettings.AppSettings("CONS_COD_SICAR_SERV")
            oAuditoria = New COM_SIC_Activaciones.clsAuditoriaWS

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio RegistrarAuditoria")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Nombre Host     : " & nameHost)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Nombre Server   : " & nameServer)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " ipServer        : " & ipServer)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " HostInfo        : " & hostInfo.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Usuario_id      : " & user)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " ipCliente       : " & ipHost)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " codServicio     : " & CodServicio)

            Dim auditoriaGrabado As Boolean = oAuditoria.RegistrarAuditoria(CodTrx, _
                                            CodServicio, _
                                            ipHost, _
                                            nameHost, _
                                            ipServer, _
                                            nameServer, _
                                            user, _
                                            "", _
                                            "0", _
                                            DesTrx)

            If Not auditoriaGrabado Then
                Throw New Exception("Error. No se registro en Auditoria la configuracion de Paycall.")
            End If
        Catch ex As Exception
            Response.Write("<script> alert('" + ex.Message + "');</script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: " & ex.Message)
        Finally
            oAuditoria = Nothing
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin RegistrarAuditoria")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
        End Try
    End Sub


#End Region

End Class
