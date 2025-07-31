Public Class ConsultaCajaTot
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lbOficina As System.Web.UI.WebControls.ListBox
    Protected WithEvents cmdBuscar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents btnLimpiar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents txtFechaIni As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtFechaFin As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents chkTodos As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtCaja As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboEstado As System.Web.UI.WebControls.DropDownList
    Protected WithEvents loadDataHandler As System.Web.UI.WebControls.Button
    Protected WithEvents hidCodCajero As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidCodOficina As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidSwCajero As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidSwOficina As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidCodCaja As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lbCodOficina As System.Web.UI.WebControls.ListBox
    Protected WithEvents loadOficinaHandler As System.Web.UI.WebControls.Button
    Protected WithEvents loadCajeroHandler As System.Web.UI.WebControls.Button
    Protected WithEvents chkTodosOf As System.Web.UI.WebControls.CheckBox
    Protected WithEvents lbCodCajero As System.Web.UI.WebControls.ListBox
    Protected WithEvents lbCajero As System.Web.UI.WebControls.ListBox
    Protected WithEvents txtCntRegistros As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDescripcion As System.Web.UI.WebControls.TextBox

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
    Dim nameFile As String = "Log_ConsultaTotal"
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogConsultaCuadre")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim strUsuario As String = String.Empty
    Dim strIdentifyLog As String = String.Empty
    Private Const CAJERO As String = "C"
    Dim objClsAdmCaja As COM_SIC_Adm_Cajas.clsAdmCajas
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
                Session("ConCajTot_codOficina") = Nothing
                Session("ConCajTot_codCajero") = Nothing
                Session("ConCajTot_codCaja") = Nothing
                strUsuario = Session("USUARIO")
                strIdentifyLog = Session("ALMACEN") & "|" & strUsuario
                txtCaja.Attributes.Add("onkeypress", "javascript:ValidaNumero(this)")
                chkTodos.Attributes.Add("onclick", "checkEnabled(this.checked);")
                chkTodosOf.Attributes.Add("onclick", "checkEnabledOfi(this.checked);")
                txtCntRegistros.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtFechaIni.Value = Format(Day(Now), "00") & "/" & Format(Month(Now), "00") & "/" & Format(Year(Now), "0000")
                txtFechaFin.Value = Format(Day(Now), "00") & "/" & Format(Month(Now), "00") & "/" & Format(Year(Now), "0000")
            End If
        End If
    End Sub

    Private Sub loadDataHandler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles loadDataHandler.Click
        Try
            If Not Session("dgListaOficina") Is Nothing Then
                Dim dt As DataTable = DirectCast(Session("dgListaOficina"), DataTable)
                If Not Session("ConCajTot_codOficina") Is Nothing Then
                    Dim codigoOfi As String = CStr(Session("ConCajTot_codOficina"))
                    Dim dv As New DataView
                    Dim arrCodigos() As String = Split(codigoOfi, ",")
                    dv.Table = dt

                    lbOficina.Items.Clear()
                    lbCodOficina.Items.Clear()

                    If hidSwCajero.Value = "1" Then
                        lbCodCajero.Items.Clear()
                        lbCajero.Items.Clear()
                        Session("ConCajTot_codCajero") = ""
                    End If

                    For i As Int32 = 0 To arrCodigos.Length - 1
                        dv.RowFilter = "CODIGO = '" & arrCodigos(i) & "'"
                        Dim drvResultado As DataRowView = dv.Item(0)
                        If Not drvResultado Is Nothing Then
                            With drvResultado
                                lbCodOficina.Items.Add(New ListItem(Trim(drvResultado("CODIGO"))))
                                lbOficina.Items.Add(New ListItem(Trim(drvResultado("DESCRIPCION"))))
                            End With
                        End If
                    Next

                    hidCodOficina.Value = CStr(Session("ConCajTot_codOficina"))
                    chkTodosOf.Checked = False
                End If
            End If

            If Not Session("dgListaCajero") Is Nothing Then
                Dim dt As DataTable = DirectCast(Session("dgListaCajero"), DataTable)
                If Not Session("ConCajTot_codCajero") Is Nothing Then
                    Dim codigoCaj As String = CStr(Session("ConCajTot_codCajero"))
                    Dim dv As New DataView
                    Dim arrCodigos() As String = Split(codigoCaj, ",")
                    dv.Table = dt

                    lbCajero.Items.Clear()
                    lbCodCajero.Items.Clear()

                    If hidSwOficina.Value = "1" Then
                        lbCodOficina.Items.Clear()
                        lbOficina.Items.Clear()
                        Session("ConCajTot_codOficina") = ""
                    End If

                    For i As Int32 = 0 To arrCodigos.Length - 1
                        dv.RowFilter = "CODIGO = '" & arrCodigos(i) & "'"
                        Dim drvResultado As DataRowView = dv.Item(0)
                        If Not drvResultado Is Nothing Then
                            With drvResultado
                                lbCodCajero.Items.Add(New ListItem(Trim(drvResultado("CODIGO"))))
                                lbCajero.Items.Add(New ListItem(Trim(drvResultado("DESCRIPCION"))))
                            End With
                        End If
                    Next

                    hidCodCajero.Value = CStr(Session("ConCajTot_codCajero"))
                    chkTodos.Checked = False
                End If
            End If

            If Not Session("dgListaCaja") Is Nothing Then
                Dim dt As DataTable = DirectCast(Session("dgListaCaja"), DataTable)
                If Not Session("ConCajTot_codCaja") Is Nothing Then
                    Dim codigoCaja As String = CStr(Session("ConCajTot_codCaja"))
                    Dim dv As New DataView
                    dv.Table = dt
                    dv.RowFilter = "ID_CAJA_OFICINA = '" & codigoCaja & "'"
                    Dim drvResultado As DataRowView = dv.Item(0)
                    If Not drvResultado Is Nothing Then
                        With drvResultado
                            txtCaja.Text = Trim(drvResultado("CAJA")) & " - " & Trim(drvResultado("DESCRIPCION"))
                        End With
                        hidCodCaja.Value = drvResultado("CAJA")
                    End If
                End If
            End If

        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   error loadDataHandler_Click: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin TOT - loadDataHandler_Click")
        End Try
    End Sub

    Private Sub cmdBuscar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.ServerClick
        Try
            Dim strOficinaVta As String = hidCodOficina.Value
            Dim strFechaIni As String = txtFechaIni.Value
            Dim strFechaFin As String = txtFechaFin.Value
            Dim strCodCajero As String = hidCodCajero.Value
            Dim strCaja As String = hidCodCaja.Value
            Dim strEstado As String = cboEstado.SelectedItem.Value
            Dim strDescripcion As String = txtDescripcion.Text
            Dim strCntReg As String = txtCntRegistros.Text
            Dim strUrl As String = "ConsultaCajaTot_cargarDatos.aspx?"
            Dim strMsjErr As String = String.Empty

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio cmdBuscar_Click ")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   in oficina: " & strOficinaVta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   in Fecha INI: " & strFechaIni)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   in Fecha FIN: " & strFechaFin)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   in codigo Cajero: " & strCodCajero)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   in Caja: " & strCaja)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   in Estado: " & strEstado)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   in Descripcion: " & strDescripcion)

            If strOficinaVta.Equals(String.Empty) And Not chkTodosOf.Checked Then
                strMsjErr = "Seleccione oficina"
                Response.Write("<script>alert('" & strMsjErr & "');</script>")
                Exit Sub
            End If

            If chkTodosOf.Checked Then
                strOficinaVta = "0"
            End If

            If strFechaIni.Equals(String.Empty) Then
                strMsjErr = "Seleccione Fecha de inicio"
                Response.Write("<script>alert('" & strMsjErr & "');</script>")
                Exit Sub
            Else
                strFechaIni = CDate(strFechaIni).ToString("yyyyMMdd")
            End If

            If strFechaFin.Equals(String.Empty) Then
                strMsjErr = "Seleccione Fecha de fin"
                Response.Write("<script>alert('" & strMsjErr & "');</script>")
                Exit Sub
            Else
                strFechaFin = CDate(strFechaFin).ToString("yyyyMMdd")
            End If

            If chkTodos.Checked Then
                strCodCajero = "0"
            End If

            If strCaja.Equals(String.Empty) Then
                strCaja = "0" 'Todas las cajas de la oficina seleccionada
            End If

            If strDescripcion.Equals(String.Empty) Then
                strDescripcion = "0"
            End If

            Session("ConsultDatosCuadCajTot") = Nothing
            Session("ConsultDatosCuadCajTotFill") = Nothing

            strUrl = strUrl & "ov=" & strOficinaVta & "&fi=" & strFechaIni & "&ff=" & strFechaFin & "&cc=" & strCodCajero & "&cj=" & strCaja & "&st=" & strEstado & "&ds=" & strDescripcion & "&cntReg=" & strCntReg

            Response.Redirect(strUrl)
        Catch ex1 As Threading.ThreadAbortException
            'DO Nothing
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin cmdBuscar_Click ")
        End Try
    End Sub

    Private Sub loadCajeroHandler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles loadCajeroHandler.Click

    End Sub

    Private Sub btnLimpiar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLimpiar.ServerClick
        lbCajero.Items.Clear()
        lbCodCajero.Items.Clear()
        lbCodOficina.Items.Clear()
        lbOficina.Items.Clear()
    End Sub

#End Region

End Class