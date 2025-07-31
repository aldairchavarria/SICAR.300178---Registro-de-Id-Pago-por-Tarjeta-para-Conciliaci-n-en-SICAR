Imports COM_SIC_Adm_Cajas

Public Class ConsultaCajaInd_asignar
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTitNuevoAsignacion As System.Web.UI.WebControls.Label
    Protected WithEvents lblOficina As System.Web.UI.WebControls.Label
    Protected WithEvents lblCajero As System.Web.UI.WebControls.Label
    Protected WithEvents lblCaja As System.Web.UI.WebControls.Label
    Protected WithEvents cboCaja As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblFecha As System.Web.UI.WebControls.Label
    Protected WithEvents txtFecha As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents cmdGrabar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents txtOficina As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCajero As System.Web.UI.WebControls.TextBox
    Protected WithEvents loadDataHandler As System.Web.UI.WebControls.Button
    Protected WithEvents hidCodCajero As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidCodOficina As System.Web.UI.HtmlControls.HtmlInputHidden

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
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = "Log_AsignacionCajas"
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogConsultaCuadre")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim strUsuario As String
    Dim strIdentifyLog As String
    Private Const optTodos As String = "T"
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
            If Not IsPostBack Then
                strUsuario = Session("USUARIO")
                strIdentifyLog = Session("ALMACEN") & "|" & strUsuario
                CargarCajas(String.Empty)
            End If
        End If
    End Sub

    Private Sub loadDataHandler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles loadDataHandler.Click
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio IND - loadDataHandler_Click ")
            If Not Session("Asignar_dgListaOficina") Is Nothing Then
                Dim dt As DataTable = DirectCast(Session("Asignar_dgListaOficina"), DataTable)
                If Not Session("Asignar_codOficina") Is Nothing Then
                    Dim codigoOfi As String = CStr(Session("Asignar_codOficina"))
                    Dim dv As New DataView
                    dv.Table = dt
                    dv.RowFilter = "CODIGO = '" & codigoOfi & "'"
                    Dim drvResultado As DataRowView = dv.Item(0)
                    If Not drvResultado Is Nothing Then
                        With drvResultado
                            txtOficina.Text = Trim(drvResultado("CODIGO")) & " - " & Trim(drvResultado("DESCRIPCION"))
                        End With
                        hidCodOficina.Value = CStr(Session("Asignar_codOficina"))
                    End If
                    CargarCajas(codigoOfi)
                End If
            End If

            If Not Session("dgListaCajero") Is Nothing Then
                Dim dt As DataTable = DirectCast(Session("dgListaCajero"), DataTable)
                If Not Session("ConCajTot_codCajero") Is Nothing Then
                    Dim codigoCaj As String = CStr(Session("ConCajTot_codCajero"))
                    Dim dv As New DataView
                    dv.Table = dt
                    dv.RowFilter = "CODIGO = '" & codigoCaj & "'"
                    Dim drvResultado As DataRowView = dv.Item(0)
                    If Not drvResultado Is Nothing Then
                        With drvResultado
                            txtCajero.Text = Trim(drvResultado("CODIGO")) & " - " & Trim(drvResultado("DESCRIPCION"))
                        End With
                        hidCodCajero.Value = CStr(Session("ConCajTot_codCajero"))
                    End If
                End If
            End If

        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   error loadDataHandler_Click: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin IND - loadDataHandler_Click")
        End Try
    End Sub

    Private Sub cmdGrabar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGrabar.ServerClick
        Dim strOficina As String = Request.Item("hidCodOficina")
        Dim strCajero As String = Request.Item("hidCodCajero")
        Dim strCaja As String = cboCaja.SelectedValue
        Dim strFecha As String = txtFecha.Value
        Dim dsResult As DataSet
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Dim i As Integer = 0
        Dim blnError As Boolean = False

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INI  cmdGrabar_ServerClick")
            If strOficina.Equals(String.Empty) Then
                Response.Write("<script>alert('Para Realizar la Asignación Seleccionar una Oficina')</script>")
                Exit Sub
            End If

            If strCajero.Equals(String.Empty) Then
                Response.Write("<script>alert('Para Realizar la Asignación Seleccionar un Usuario')</script>")
                Exit Sub
            End If

            If cboCaja.SelectedIndex = 0 Then
                Response.Write("<script>alert('Para Realizar la Asignación Seleccionar una Caja')</script>")
                Exit Sub
            End If

            If strFecha.Equals(String.Empty) Then
                Response.Write("<script>alert('Para Realizar la Asignación Seleccionar una Fecha')</script>")
                Exit Sub
            End If

            dsResult = objOffline.Set_CajeroDiario(strOficina, strCajero, txtFecha.Value, strCaja)

            If Not IsNothing(dsResult) Then
                For i = 0 To dsResult.Tables(0).Rows.Count - 1
                    If dsResult.Tables(0).Rows(i).Item("TYPE") = "E" Then
                        Response.Write("<script>alert('" & dsResult.Tables(0).Rows(i).Item("MESSAGE") & "')</script>")
                        blnError = True
                    End If
                Next
            End If
            If Not blnError Then
                Response.Write("<script>alert('Asignación realizada con éxito')</script>")
                Response.Write("<script>window.opener.f_Refrescar();</script>")
                Response.Write("<script>window.close();</script>")
            End If

            cboCaja.SelectedIndex = 0
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: " & ex.Message)
        Finally
            objOffline = Nothing
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN  cmdGrabar_ServerClick")
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub CargarCajas(ByVal oficina As String)
        Dim objclsAdmCaja As New clsAdmCajas
        Dim dvResult As New DataView
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Asignar Cajero - INI  CargarCajas")
            Dim dsResult As DataSet = objclsAdmCaja.GetCajas(String.Empty, oficina, optTodos)
            dsResult.Tables(0).Columns.Add("DescricionLarga", GetType(System.String), "CAJA + ' - ' + DESCRIPCION")

            dvResult.Table = dsResult.Tables(0)
            dvResult.RowFilter = "ESTADO <> 'INACTIVO'"

            cboCaja.DataTextField = "DescricionLarga"
            cboCaja.DataValueField = "CAJA"
            cboCaja.DataSource = dvResult
            cboCaja.DataBind()
            cboCaja.Items.Insert(0, New ListItem("Seleccionar"))
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: " & ex.Message)
        Finally
            objclsAdmCaja = Nothing
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Asignar Cajero - FIN  CargarCajas")
        End Try
    End Sub

#End Region

End Class
