Public Class grdCajeroxSubOf
    Inherits SICAR_WebBase

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents gridDetalle As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

    Protected WithEvents hdnCodOficina As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnDesOficina As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnCodSubOficina As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnDesSubOficina As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnUsuario As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnOficina As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtCodOficina As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtDesOficina As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtCodSubOficina As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtDesSubOficina As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents btnloadDataHandler As System.Web.UI.WebControls.Button

#End Region

    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogPOS")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogPOS")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim strCodSubOfiAuditoria As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("ConstSubOfi_Auditoria"))

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not Page.IsPostBack Then
            Me.hdnCodOficina.Value = Funciones.CheckStr(Session("ALMACEN"))
            Me.hdnUsuario.Value = Funciones.CheckStr(Session("USUARIO"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Page_Load: Inicio grdCajeroxSubOf.aspx")
            Call Inicio()
        End If
    End Sub
#Region "Botones"
    Private Sub btnloadDataHandler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnloadDataHandler.Click
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " btnloadDataHandler_Click: Inicio")
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " btnloadDataHandler_Click: Variable Input, " & Me.hdnCodSubOficina.Value)
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " btnloadDataHandler_Click: Iniciar método, ListarCajero")
        Call ListarCajero(Me.hdnCodSubOficina.Value)
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " btnloadDataHandler_Click: Fin")
    End Sub
#End Region
#Region "Metodos"
    Private Sub Inicio()
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Inicio: Inicio")
        Me.hdnCodSubOficina.Value = Funciones.CheckStr(Request.QueryString("CodSubOficina"))
        Me.hdnDesSubOficina.Value = Funciones.CheckStr(Request.QueryString("DesSubOficina"))
        Me.hdnCodOficina.Value = Funciones.CheckStr(Request.QueryString("CodOficina"))
        Me.hdnDesOficina.Value = Funciones.CheckStr(Request.QueryString("DesOficina"))

        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Inicio: Oficina código, " & Me.hdnCodOficina.Value)
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Inicio: Oficina descripción, " & Me.hdnDesOficina.Value)
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Inicio: Sub Oficina código, " & Me.hdnCodSubOficina.Value)
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Inicio: Sub Oficina descripción, " & Me.hdnDesSubOficina.Value)

        Me.txtCodOficina.Value = Me.hdnCodOficina.Value
        Me.txtDesOficina.Value = Me.hdnDesOficina.Value
        Me.txtCodSubOficina.Value = Me.hdnCodSubOficina.Value
        Me.txtDesSubOficina.Value = Me.hdnDesSubOficina.Value

        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Inicio: Iniciar método, ListarCajero")

        Call ListarCajero(Me.hdnCodSubOficina.Value)
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " Inicio: Fin")
    End Sub

    Private Sub ListarCajero(ByVal strCodOficina As String)
        objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " ListarCajero: Inicio")
        Dim oCajeroDAC As New COM_SIC_Activaciones.clsRecaudacionDAC
        Dim dtCajeroDAC As New DataTable
        Dim strRsptaCode As String = String.Empty
        Dim strRsptaMsg As String = String.Empty
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " ListarCajero: Iniciar método, ConsultarCajeroDAC")
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " ListarCajero: Variable Input ID, ")
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " ListarCajero: Variable Input Sub Oficina, " & strCodOficina)
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " ListarCajero: Variable Input Cajero, ")
            dtCajeroDAC = oCajeroDAC.ConsultarCajeroDAC(String.Empty, strCodOficina, String.Empty, strRsptaCode, strRsptaMsg)
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " ListarCajero: Código respuesta, " & strRsptaCode)
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " ListarCajero: Código respuesta, " & strRsptaMsg)
            If strRsptaCode = "0" Then
                Me.gridDetalle.DataSource = dtCajeroDAC
                Me.gridDetalle.DataBind()
            Else
                Response.Write("<script language=jscript> alert('" + "Listar Cajero: " + strRsptaMsg.Replace("'", "") + "'); </script>")
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " ListarCajero: Error, " & ex.Message)
            Response.Write("<script language=jscript> alert('" + "Listar Cajero: " + ex.Message + "'); </script>")
        Finally
            RegistrarAuditoria("ConsultarCajeroDAC - Consultar Cajero DAC de Sub Oficina", strCodSubOfiAuditoria)
            objFileLog.Log_WriteLog(pathFile, strArchivo, Me.hdnOficina.Value & " - " & Me.hdnUsuario.Value & " ListarCajero: Fin")
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
