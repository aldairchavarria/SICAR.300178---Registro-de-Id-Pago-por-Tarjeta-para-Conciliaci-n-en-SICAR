Imports System.Data
Imports System.Web

Public Class grdSubOficinas
    Inherits SICAR_WebBase

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtOculto As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnNuevo As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

    Protected WithEvents loadDataHandler As System.Web.UI.WebControls.Button
    Protected WithEvents btnLimpiar As System.Web.UI.WebControls.Button
    Protected WithEvents btnBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents hidCodOficina As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnCodUsuario As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnGCodOficina As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lbCodOficina As System.Web.UI.WebControls.ListBox
    Protected WithEvents lbOficina As System.Web.UI.WebControls.ListBox
    Protected WithEvents chkTodosOf As System.Web.UI.WebControls.CheckBox
    Protected WithEvents gridDetalle As System.Web.UI.WebControls.DataGrid
#End Region

    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogPOS")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogPOS")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim dsDatosBusqueda As New DataSet
    Dim strCodSubOfiAuditoria As String = ConfigurationSettings.AppSettings("ConstSubOfi_Auditoria")

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Session("USUARIO") Is Nothing Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            If Not Page.IsPostBack Then
                Me.hdnCodUsuario.Value = Funciones.CheckStr(Session("USUARIO"))
                Me.hdnGCodOficina.Value = Funciones.CheckStr(Session("ALMACEN"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnGCodOficina.Value & " - " & Me.hdnCodUsuario.Value & " Page_Load: Inicio grdSubOficinas")

                If CStr(Request.QueryString("back")) = "R" Then
                    hidCodOficina.Value = CStr(Session("ConCajTot_codOficina"))
                    If Not hidCodOficina.Value = "" Then
                        Call loadDataHandler_Click(sender, e)
                    Else
                        Me.chkTodosOf.Checked = True
                    End If
                    LLenarGrilla()
                End If

            End If
        End If
    End Sub

#Region "Buttons"

    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnGCodOficina.Value & " - " & Me.hdnCodUsuario.Value & " btnBuscar_Click : Inicio")

        Call LLenarGrilla()

        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnGCodOficina.Value & " - " & Me.hdnCodUsuario.Value & " btnBuscar_Click : Fin")
    End Sub

    Private Sub btnLimpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnGCodOficina.Value & " - " & Me.hdnCodUsuario.Value & " btnLimpiar_Click : Inicio")

        gridDetalle.DataSource = Nothing
        gridDetalle.DataBind()
        Me.lbCodOficina.Items.Clear()
        Me.lbOficina.Items.Clear()
        Me.chkTodosOf.Checked = False
        Me.hidCodOficina.Value = ""
        Session("ConCajTot_codOficina") = ""

        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnGCodOficina.Value & " - " & Me.hdnCodUsuario.Value & " btnLimpiar_Click : Fin")
    End Sub

    Private Sub loadDataHandler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles loadDataHandler.Click
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hidCodOficina.Value & " - " & Me.hdnCodUsuario.Value & " loadDataHandler_Click: Inicio")
            If Not Session("dgListaOficina") Is Nothing Then
                Dim dt As DataTable = DirectCast(Session("dgListaOficina"), DataTable)
                If Not Session("ConCajTot_codOficina") Is Nothing Then
                    Dim codigoOfi As String = CStr(Session("ConCajTot_codOficina"))
                    Dim dv As New DataView
                    Dim strCodigos As String = ""
                    Dim strOficinas As String = ""
                    Dim arrCodigos() As String = Split(codigoOfi, ",")
                    dv.Table = dt

                    lbOficina.Items.Clear()
                    lbCodOficina.Items.Clear()

                    For i As Int32 = 0 To arrCodigos.Length - 1
                        dv.RowFilter = "CODIGO = '" & arrCodigos(i) & "'"
                        Dim drvResultado As DataRowView = dv.Item(0)
                        If Not drvResultado Is Nothing Then
                            With drvResultado
                                lbCodOficina.Items.Add(New ListItem(Trim(drvResultado("CODIGO"))))
                                strCodigos = strCodigos + Trim(drvResultado("CODIGO")) + ";"
                                lbOficina.Items.Add(New ListItem(Trim(drvResultado("DESCRIPCION"))))
                                strOficinas = strOficinas + Trim(drvResultado("DESCRIPCION")) + ";"
                            End With
                        End If
                    Next

                    hidCodOficina.Value = CStr(Session("ConCajTot_codOficina"))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hidCodOficina.Value & " - " & Me.hdnCodUsuario.Value & " loadDataHandler_Click: " & "hidCodOficina : " & hidCodOficina.Value)
                    chkTodosOf.Checked = False
                End If
            End If
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hidCodOficina.Value & " - " & Me.hdnCodUsuario.Value & " loadDataHandler_Click: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hidCodOficina.Value & " - " & Me.hdnCodUsuario.Value & " loadDataHandler_Click: Fin")
        End Try
    End Sub
#End Region

#Region "Metodos"
    Private Sub LLenarGrilla()
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnGCodOficina.Value & " - " & Me.hdnCodUsuario.Value & " LLenarGrilla: Inicio")

        Dim objRecaudacionDAC As New COM_SIC_Activaciones.clsRecaudacionDAC
        Dim strResponseCode As String = String.Empty
        Dim strResponseMsg As String = String.Empty
        Dim strMensaje As String = String.Empty
        Dim strID As String = String.Empty
        Dim strPuntoVenta As String = String.Empty
        Dim strSubOficina As String = String.Empty
        Dim dtSubOficinas As New DataTable
        Dim blnTodos As Boolean = False

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnGCodOficina.Value & " - " & Me.hdnCodUsuario.Value & " LLenarGrilla Inicio")

            strID = String.Empty
            strPuntoVenta = String.Empty
            strSubOficina = String.Empty
            blnTodos = Me.chkTodosOf.Checked

            If blnTodos = False And Me.hidCodOficina.Value = String.Empty Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnGCodOficina.Value & " - " & Me.hdnCodUsuario.Value & " LLenarGrilla: Validar, selecciona oficina")
                Response.Write("<script language=jscript> alert('" + "Seleccione oficina" + "'); </script>")
            Else
                If blnTodos = True Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnGCodOficina.Value & " - " & Me.hdnCodUsuario.Value & " LLenarGrilla : ConsultarSubOficina, consultar todas las oficinas")
                    dtSubOficinas = objRecaudacionDAC.ConsultarSubOficina("", "", "", strResponseCode, strResponseMsg)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnGCodOficina.Value & " - " & Me.hdnCodUsuario.Value & " LLenarGrilla: Código de resultado, " & strResponseCode)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnGCodOficina.Value & " - " & Me.hdnCodUsuario.Value & " LLenarGrilla: Mensaje de resultado, " & strResponseMsg)
                    lbCodOficina.Items.Clear()
                    lbOficina.Items.Clear()
                    Session("ConCajTot_codOficina") = Nothing
                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnGCodOficina.Value & " - " & Me.hdnCodUsuario.Value & " LLenarGrilla: ConsultarSubOficina, oficinas seleccionadas")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnGCodOficina.Value & " - " & Me.hdnCodUsuario.Value & " LLenarGrilla: ConsultarSubOficina, código de oficina: " & Me.hidCodOficina.Value)
                    dtSubOficinas = objRecaudacionDAC.ConsultarSubOficina("", Me.hidCodOficina.Value, "", strResponseCode, strResponseMsg)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnGCodOficina.Value & " - " & Me.hdnCodUsuario.Value & " LLenarGrilla: ConsultarSubOficina, código resultado: " & strResponseCode)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnGCodOficina.Value & " - " & Me.hdnCodUsuario.Value & " LLenarGrilla: ConsultarSubOficina, código resultado: " & strResponseMsg)

                End If

                If strResponseCode = "0" Then
                    If dtSubOficinas.Rows.Count > 0 Then
                        Me.gridDetalle.DataSource = dtSubOficinas
                        Me.gridDetalle.DataBind()
                    End If
                Else
                    Me.gridDetalle.DataSource = Nothing
                    Me.gridDetalle.DataBind()

                    objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnGCodOficina.Value & " - " & Me.hdnCodUsuario.Value & " LLenarGrilla: " & strResponseMsg)
                    Response.Write("<script language=jscript> alert('" + strResponseMsg + "'); </script>")
                End If

            End If

        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnGCodOficina.Value & " - " & Me.hdnCodUsuario.Value & " LLenarGrilla: Error, " & ex.Message)
        Finally
            strMensaje = "ConsultarSubOficina - Consulta Sub Oficina"
            RegistrarAuditoria(strMensaje, strCodSubOfiAuditoria)
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnGCodOficina.Value & " - " & Me.hdnCodUsuario.Value & " LLenarGrilla: Fin")
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
