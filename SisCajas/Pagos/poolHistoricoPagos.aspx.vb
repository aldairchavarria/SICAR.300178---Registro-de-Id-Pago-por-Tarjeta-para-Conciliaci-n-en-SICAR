Imports System.Globalization


Public Class poolHistoricoPagos
    Inherits System.Web.UI.Page



#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtFecha As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFecha2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNumDocumento As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboTipDocumento As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dgPool As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim ds As DataSet

    Dim drFila As DataRow
    Dim objLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecarga")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Dim strArchivo As String = objLog.Log_CrearNombreArchivo(nameFile)
    Dim objFileLog As New SICAR_Log

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Historico Pagos " & "- " & "Inicia el Page_Load")

            Dim objPool As New SAP_SIC_Pagos.clsPagos
            Dim tipo_doc As String = ""
            Dim objConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu                   '** SINERGIA 6.0 **'
            Dim objConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap               '** SINERGIA 6.0 **'

            Dim dsTipo As DataSet
            Dim dsData As DataSet
            Dim i As Integer
            Dim fecha2 As Object

            'dsTipo = objPool.Get_LeeTipoDocCliente()
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Historico Pagos " & "- " & " Consulta tipo docuemnto cliente.")
            dsTipo = objConsultaPvu.ConsultaTipoDocumento("")

            If Not dsTipo Is Nothing Then
                cboTipDocumento.DataSource = dsTipo.Tables(0)
                'cboTipDocumento.DataValueField = "J_1ATODC"
                'cboTipDocumento.DataTextField = "TEXT30"
                cboTipDocumento.DataValueField = "TDOCC_CODIGO"
                cboTipDocumento.DataTextField = "TDOCV_DESCRIPCION"

                objFileLog.Log_WriteLog(pathFile, strArchivo, "Historico Pagos " & "- " & " Asigna el tipo de docuemento por defecto DNI.")
                If cboTipDocumento.SelectedIndex = -1 Then
                    cboTipDocumento.SelectedValue = "01" 'DNI por defecto 31636
                End If
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Historico Pagos " & "- " & " No se encontraron registros para el tipo de documento cliente.")
            End If

            txtNumDocumento.Attributes.Add("onKeyDown", "textCounter(this)")
            txtNumDocumento.Attributes.Add("onKeyUp", "textCounter(this)")
            cboTipDocumento.Attributes.Add("onChange", "f_CambiaTipo()")

            If Not Page.IsPostBack Then
                If Session("FechaPago") = "" Then
                    txtFecha.Text = Format(Day(Now), "00") & "/" & Format(Month(Now), "00") & "/" & Format(Year(Now), "0000") 'Format(Now, "d")
                Else
                    txtFecha.Text = Session("FechaPago")
                    Session("FechaPago") = ""
                End If
            Else
                cboTipDocumento.SelectedValue = Request.Item("cbotipdocumento")
            End If


            objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio Fecha2")
            Dim cultureName As String = "es-PE"
            Dim culture As CultureInfo = New CultureInfo(cultureName)
            Dim dateTimeValue As DateTime

            Try
                'ds = objPool.Get_ConsultaPoolFactura(Session("ALMACEN"), txtFecha.Text, "D", txtFecha2.Text, txtNumDocumento.Text, cboTipDocumento.SelectedValue, "20", "1")
                If txtFecha2.Text = "" Then
                    fecha2 = DBNull.Value
                Else
                    'fecha2 = DateTime.ParseExact(Funciones.CheckDate(fecha2), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Fecha2. " & IIf(txtFecha2.Text = "", DateTime.Now, Me.txtFecha2.Text))
                    fecha2 = IIf(txtFecha2.Text = "", DateTime.Now, Me.txtFecha2.Text)
                    dateTimeValue = Convert.ToDateTime(fecha2, culture)
                    fecha2 = dateTimeValue
                    'fecha2 = DateTime.ParseExact(Funciones.CheckDate(txtFecha2.Text), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                End If
            Catch ex As Exception
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Err. " & ex.Message)
            End Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin Fecha2")



            objFileLog.Log_WriteLog(pathFile, strArchivo, "Historico Pagos " & "- " & " Asigna tipo doc:")
            tipo_doc = cboTipDocumento.SelectedValue


            objFileLog.Log_WriteLog(pathFile, strArchivo, "Historico Pagos " & "- " & " Inicia la consulta del Historico.")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Historico Pagos " & "- " & " Paràmetros: ")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Historico Pagos " & "- " & " Paràm1 : " & Funciones.CheckStr(cboTipDocumento.SelectedValue))
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Historico Pagos " & "- " & " Paràm2 : " & Funciones.CheckStr(txtNumDocumento.Text))
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Historico Pagos " & "- " & " Paràm3 : " & Funciones.CheckStr(ConsultaPuntoVenta(Session("ALMACEN"))))
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Historico Pagos " & "- " & " Paràm4 : " & txtFecha.Text.Trim)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Historico Pagos " & "- " & " Paràm5 : " & txtFecha2.Text.Trim)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Historico Pagos " & "- " & " Fin pàrametros.")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Historico Pagos " & "- " & " Ejecura consulta:")

            Try
                'INICIATIVA-565 INI
                dsData = objConsultaMsSap.ConsultarPagoHist(cboTipDocumento.SelectedValue, _
                                                          IIf(txtNumDocumento.Text.ToString.Trim.Length = 0, "00000000", Funciones.CheckStr(txtNumDocumento.Text.ToString.Trim)), _
                                                          String.Empty, _
                                                          FormatoFecha(txtFecha.Text.Trim), _
                                                          FormatoFecha(txtFecha2.Text.Trim))
                'INICIATIVA-565 FIN




                objFileLog.Log_WriteLog(pathFile, strArchivo, "Historico Pagos " & "- " & " Fin de la consulta")

            Catch ex As Exception
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Historico Pagos " & "- " & " Error al consultar el histórico de pagos : ")
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Historico Pagos " & "- " & " Err. " & ex.Message.ToString)
            End Try

            'Dim i As Integer
            'For i = 0 To ds.Tables(0).Columns.Count - 1
            '    Response.Write(ds.Tables(0).Columns(i).ColumnName + "<br>")
            'Next
            'Response.End() 

            'If txtNumDocumento.Text = "" Then
            '    For i = 0 To dsData.Tables(0).Rows.Count - 1
            '        dsData.Tables(0).Rows.RemoveAt(0)
            '    Next
            'End If
            Try
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Historico Pagos " & "- " & " Asignaciòn de los registros a la Grilla")
                If Not dsData Is Nothing Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Historico Pagos " & "- " & " Entra asignar.")
                    'dgPool.DataSource = ds.Tables(0)
                    dgPool.DataSource = dsData.Tables(0)
                    Me.DataBind()
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Historico Pagos " & "- " & " Fin asignar.")
                Else
                    dgPool.DataSource = Nothing
                    Me.DataBind()
                End If

                'cboTipDocumento.Items.Remove(New ListItem("Sin.Documento", "-"))
                If Not dsTipo Is Nothing Then
                    If tipo_doc <> "" Then
                        cboTipDocumento.SelectedValue = tipo_doc
                    End If
                End If
            Catch ex As Exception
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Historico Pagos " & "- " & " Error en la asignaciòn da datos a la grilla.")
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Historico Pagos " & "- " & " Err." & ex.Message.ToString)
            End Try
        End If
    End Sub

    Shared Function FormatoFecha(ByVal Fecha As String) As String
        If (Fecha.Length > 0) Then
            If (Fecha.Length = 8) Then
                Return Fecha.Substring(6, 2) + "/" + Fecha.Substring(4, 2) + "/" + Fecha.Substring(0, 4)
            Else
                Return Fecha.Substring(0, 2) + "/" + Fecha.Substring(3, 2) + "/" + Fecha.Substring(6, 4)
            End If
            Return "0000/00/00"
        End If
    End Function

    '****************************************************'
    '****************************************************'
    '++++METODO PARA HACER CONSULTA DEL PUNTO DE VENTA+++'
    '****************************************************'
    '****************************************************'
    Private Function ConsultaPuntoVenta(ByVal P_OVENC_CODIGO As String) As String
        Try
            Dim obj As New COM_SIC_Activaciones.clsConsultaMsSap
            Dim dsReturn As DataSet
            dsReturn = obj.ConsultaPuntoVenta(P_OVENC_CODIGO)
            If dsReturn.Tables(0).Rows.Count > 0 Then
                Return dsReturn.Tables(0).Rows(0).Item("OVENV_CODIGO_SINERGIA")
            End If
            Return Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

End Class
