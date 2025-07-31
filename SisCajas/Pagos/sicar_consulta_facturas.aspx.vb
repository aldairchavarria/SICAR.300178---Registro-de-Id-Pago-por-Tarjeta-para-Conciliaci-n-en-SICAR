Public Class sicar_consulta_facturas
    Inherits SICAR_WebBase

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtNroDocumento As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents btnLimpiar As System.Web.UI.WebControls.Button
    Protected WithEvents gridDetalle As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dgDetalle As System.Web.UI.WebControls.DataGrid

    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Introducir aquí el código de usuario para inicializar la página
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")

        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        End If
    End Sub

    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click

        Dim objPagosSap As New SAP_SIC_Pagos.clsPagos
        Dim dsComprobante As New DataSet
        Dim dtDetalle As New DataTable

        Dim msgErr As String
        Try
            dsComprobante = objPagosSap.Get_ConsultaComprobante(txtNroDocumento.Text.Trim, Session("ALMACEN"))

            If dsComprobante.Tables.Count > 0 Then
                If dsComprobante.Tables(5).Rows.Count > 0 Then
                    Dim drMsg As System.Data.DataRow
                    For Each drMsg In dsComprobante.Tables(5).Rows
                        If CStr(drMsg("TYPE")) = "E" Then
                            Throw New Exception("Documento NO se encuentra en cuotas. " & CStr(drMsg("MSG")))
                        End If
                    Next
                End If
                If dsComprobante.Tables(0).Rows.Count > 0 Then

                    dtDetalle = dsComprobante.Tables(1)
                    dgDetalle.DataSource = dtDetalle
                    dgDetalle.DataBind()
                Else
                    dgDetalle.DataSource = New ArrayList
                    dgDetalle.DataBind()
                    Response.Write("<script>alert('Documento NO se encuentra en cuotas')</script>")
                End If
            Else
                dgDetalle.DataSource = New ArrayList
                dgDetalle.DataBind()
                Response.Write("<script>alert('Documento NO se encuentra en cuotas')</script>")
            End If

            RegistrarAuditoria("SICAR Consulta de Facturas")

        Catch ex As Exception
            Response.Write("<script>alert('" & ex.Message & "')</script>")
        Finally
            objPagosSap = Nothing
            dsComprobante = Nothing
        End Try
    End Sub

    Private Sub btnLimpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click
        txtNroDocumento.Text = ""
        dgDetalle.DataSource = New ArrayList
        dgDetalle.DataBind()
    End Sub

   
    Private Sub RegistrarAuditoria(ByVal descTrans As String)
        Try
            Dim nombreHost As String = System.Net.Dns.GetHostName
            Dim nombreServer As String = System.Net.Dns.GetHostName
            Dim ipServer As String = System.Net.Dns.GetHostByName(nombreServer).AddressList(0).ToString
            Dim hostInfo As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(nombreHost)
            Dim usuario_id As String = CurrentUser
            Dim ipcliente As String = CurrentTerminal
            Dim strMensaje As String

            Dim strCodServ As String = ConfigurationSettings.AppSettings("CONS_COD_SICAR_SERV")
            Dim strCodTrans As String = ConfigurationSettings.AppSettings("codTrsConFac")

            Dim objAuditoriaWS As New COM_SIC_Activaciones.clsAuditoriaWS

            Dim auditoriaGrabado As Boolean
            auditoriaGrabado = objAuditoriaWS.RegistrarAuditoria(strCodTrans, strCodServ, ipcliente, nombreHost, ipServer, nombreServer, usuario_id, "", "0", descTrans)

        Catch ex As Exception
            ' Throw New Exception("Error. Registrar Auditoria.")
        End Try
    End Sub
End Class
