Public Class ConsultaCajaInd
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lbOficina As System.Web.UI.WebControls.ListBox
    Protected WithEvents txtCajero As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCaja As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboEstado As System.Web.UI.WebControls.DropDownList
    Protected WithEvents loadDataHandler As System.Web.UI.WebControls.Button
    Protected WithEvents hidCodCajero As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidCodOficina As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtFechaIni As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtFechaFin As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents hidCodCaja As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lbCodOficina As System.Web.UI.WebControls.ListBox
    Protected WithEvents txtCodCajero As System.Web.UI.WebControls.TextBox
    Protected WithEvents loadOficinaHandler As System.Web.UI.WebControls.Button
    Protected WithEvents loadCajeroHandler As System.Web.UI.WebControls.Button
    Protected WithEvents chkTodosOf As System.Web.UI.WebControls.CheckBox
    Protected WithEvents btnBuscar As System.Web.UI.WebControls.Button

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
    Dim nameFile As String = "Log_ConsultaIndividual"
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
                txtFechaIni.Value = Format(Day(Now), "00") & "/" & Format(Month(Now), "00") & "/" & Format(Year(Now), "0000")
                txtFechaFin.Value = Format(Day(Now), "00") & "/" & Format(Month(Now), "00") & "/" & Format(Year(Now), "0000")
                txtCodCajero.Attributes.Add("onkeydown", "f_buscarCajero();")
                txtCodCajero.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
            End If
        End If
    End Sub

    Private Sub loadDataHandler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles loadDataHandler.Click
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio IND - loadDataHandler_Click ")
            If Not Session("dgListaOficina") Is Nothing Then
                Dim dt As DataTable = DirectCast(Session("dgListaOficina"), DataTable)
                If Not Session("ConCajTot_codOficina") Is Nothing Then
                    Dim codigoOfi As String = CStr(Session("ConCajTot_codOficina"))
                    Dim dv As New DataView
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
                    dv.Table = dt
                    dv.RowFilter = "CODIGO = '" & codigoCaj & "'"
                    Dim drvResultado As DataRowView = dv.Item(0)
                    If Not drvResultado Is Nothing Then
                        With drvResultado
                            txtCodCajero.Text = Trim(drvResultado("CODIGO"))
                            txtCajero.Text = Trim(drvResultado("DESCRIPCION"))
                        End With
                        hidCodCajero.Value = CStr(Session("ConCajTot_codCajero"))
                    End If
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
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin IND - loadDataHandler_Click")
        End Try
    End Sub

    'INI-936 - JH - Reemplazado metodo cmdBuscar_ServerClick por btnBuscar_Click
    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            Dim strOficinaVta As String = hidCodOficina.Value
            Dim strFechaIni As String = txtFechaIni.Value
            Dim strFechaFin As String = txtFechaFin.Value
            Dim strCodCajero As String = hidCodCajero.Value
            Dim strCaja As String = hidCodCaja.Value
            Dim strEstado As String = cboEstado.SelectedItem.Value
            Dim strUrl As String = "ConsultaCajaInd_cargardatos.aspx?"
            Dim strMsjErr As String = String.Empty

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio cmdBuscar_Click ")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   in oficina: " & strOficinaVta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   in Fecha INI: " & strFechaIni)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   in Fecha FIN: " & strFechaFin)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   in codigo Cajero: " & strCodCajero)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   in Caja: " & strCaja)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   in Estado: " & strEstado)

            If chkTodosOf.Checked Then
                'INICIATIVA-318 INI
                Dim strOficinas As String = String.Empty
                objClsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas
                Dim dsResult As DataSet = objClsAdmCaja.GetOficinas("I-318") 'RF33
                Dim dt As New DataTable
                dt = dsResult.Tables(0)
                For Each dr As DataRow In dt.Rows
                    strOficinas += dr("CODIGO") & ","
                Next
                strOficinaVta = strOficinas
                'INICIATIVA-318 FIN
            End If

            If strCodCajero.Equals(String.Empty) Then
                strCodCajero = "0"
            End If

            If strCaja.Equals(String.Empty) Then
                strCaja = "0" 'Todas las cajas de la oficina seleccionada
            End If

            Session("oficinas") = strOficinaVta 'INI-936 - JH
            strUrl = strUrl & "fi=" & strFechaIni & "&ff=" & strFechaFin & "&cc=" & strCodCajero & "&cj=" & strCaja & "&st=" & strEstado 'INI-936 - JH

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
        CargarCajeros(hidCodOficina.Value)
    End Sub

#End Region

#Region "Metodos"

    Private Sub CargarCajeros(ByVal oficinaVta As String)
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio  CargarCajeros")
            objClsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas
            Dim strCodCajero As String = txtCodCajero.Text
            If oficinaVta.Equals(String.Empty) Then
                oficinaVta = 0
            End If

            Dim dsCajeros As DataSet = objClsAdmCaja.GetVendedores(strCodCajero, oficinaVta, CAJERO)
            If Not dsCajeros.Tables(0) Is Nothing Then
                If dsCajeros.Tables(0).Rows.Count = 0 Then
                    Response.Write("<script language=jscript> alert('No se encontró cajero'); </script>")
                Else
                    Dim drCj As DataRow = dsCajeros.Tables(0).Rows(0)
                    txtCodCajero.Text = Trim(drCj("CODIGO"))
                    hidCodCajero.Value = Trim(drCj("CODIGO"))
                    txtCajero.Text = Trim(drCj("DESCRIPCION"))
                End If
            End If
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN  CargarCajeros")
            objClsAdmCaja = Nothing
        End Try
    End Sub

#End Region

End Class
