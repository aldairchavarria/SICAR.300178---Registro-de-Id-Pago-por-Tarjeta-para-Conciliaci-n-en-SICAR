Public Class rep_pagosPorDelivery
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents txtFecha As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents btnExportar As System.Web.UI.HtmlControls.HtmlInputButton

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Public sFechaActual, sHoraActual
    Public sValor As String = String.Empty
    Public strcadenaprint As String = ""
    Dim objRecordSet As DataTable

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            If (Session("USUARIO") Is Nothing) Then
                Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
                Response.Redirect(strRutaSite)
                Response.End()
                Exit Sub
            Else
                If Not Page.IsPostBack Then
                    sFechaActual = ""
                    sHoraActual = ""
                    strcadenaprint = String.Empty
                Else
                    sFechaActual = Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000") 'TimeOfDay().Date.Now.ToShortDateString
                    sHoraActual = TimeOfDay().Hour & ":" & TimeOfDay().Minute
                End If
            End If
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
        End Try
        
    End Sub

    Private Sub cargarTabla()
        Dim objConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap
        Dim dsDatosPedido As DataSet
        Dim strCod As String = String.Empty
        Dim msj As String = String.Empty
        Dim i As Integer
        Dim total As Decimal = 0
        objRecordSet = Nothing
        strcadenaprint = String.Empty
        sValor = String.Empty

        Try
            Dim strOficina As String = Funciones.CheckStr(Session("ALMACEN"))
            Dim strFecha As String = Funciones.CheckStr(txtFecha.Value)

            objRecordSet = objConsultaMsSap.ConsultarReporte(strFecha, strOficina, strCod, msj)

            If Not objRecordSet Is Nothing Then
                If objRecordSet.Rows.Count > 0 Then
                    For i = 0 To objRecordSet.Rows.Count - 1
                        strcadenaprint = strcadenaprint & "<tr align=center bgcolor=#DEE9FA class=Arial11B onMouseOut=this.className='Arial11B';return false>"
                        If (objRecordSet.Rows(i).Item(24) Is DBNull.Value) Then
                            total += Decimal.Parse(0)
                        Else
                            total += Decimal.Parse(objRecordSet.Rows(i).Item(24))
                        End If

                        If (objRecordSet.Rows(i).Item(3) Is DBNull.Value) Then
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & "-" & "</td>"
                        Else
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item(3)) & "</td>"
                        End If
                        If (objRecordSet.Rows(i).Item(0) Is DBNull.Value) Then
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & "-" & "</td>"
                        Else
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item(0)) & "</td>"
                        End If
                        If (objRecordSet.Rows(i).Item(19) Is DBNull.Value) Then
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & "-" & "</td>"
                        Else
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item(19)) & "</td>"
                        End If
                        If (objRecordSet.Rows(i).Item(20) Is DBNull.Value) Then
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & "-" & "</td>"
                        Else
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item(20)) & "</td>"
                        End If
                        If (objRecordSet.Rows(i).Item(21) Is DBNull.Value) Then
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & "-" & "</td>"
                        Else
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item(21)) & "</td>"
                        End If
                        If (objRecordSet.Rows(i).Item(26) Is DBNull.Value) Then
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & "-" & "</td>"
                        Else
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item(26)) & "</td>"
                        End If
                        If (objRecordSet.Rows(i).Item(23) Is DBNull.Value) Then
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & "-" & "</td>"
                        Else
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item(23)) & "</td>"
                        End If

                        If (objRecordSet.Rows(i).Item(27) Is DBNull.Value) Then
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & "-" & "</td>"
                        Else
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item(27)) & "</td>"
                        End If

                        If (objRecordSet.Rows(i).Item(28) Is DBNull.Value) Then
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & "-" & "</td>"
                        Else
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item(28)) & "</td>"
                        End If

                        If (objRecordSet.Rows(i).Item(24) Is DBNull.Value) Then
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & "0" & "</td>"
                        Else
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item(24)) & "</td>"
                        End If
                        strcadenaprint = strcadenaprint & "</tr>"
                    Next
                    strcadenaprint = strcadenaprint & "<tr bgcolor=#DEE9FA class=Arial11B onMouseOut=this.className='Arial11B';return false>"
                    strcadenaprint = strcadenaprint & "<td colspan='9' height=25 align=right>" & "TOTAL : " & "</td>"
                    strcadenaprint = strcadenaprint & "<td height=25 align=center>" & total & "</td>"
                    strcadenaprint = strcadenaprint & "</tr>"
                    sValor = "1"
                Else
                    strcadenaprint = strcadenaprint & "<tr bgcolor=#DEE9FA class=Arial11B onMouseOut=this.className='Arial11B';return false>"
                    strcadenaprint = strcadenaprint & "<td colspan='9' height=25 align=center>" & "NO SE ENCONTRARON DATOS" & "</td>"
                    strcadenaprint = strcadenaprint & "</tr>"
                    Response.Write("<script language=jscript> alert('" + "No se encontrarán pagos de pedido de delivery efectuados en la fecha seleccionada. Intente con una fecha diferente." + "'); </script>")
                End If
            Else
                strcadenaprint = strcadenaprint & "<tr bgcolor=#DEE9FA class=Arial11B onMouseOut=this.className='Arial11B';return false>"
                strcadenaprint = strcadenaprint & "<td colspan='9' height=25 align=center>" & "NO SE ENCONTRARON DATOS" & "</td>"
                strcadenaprint = strcadenaprint & "</tr>"
                Response.Write("<script language=jscript> alert('" + "No se encontrarán pagos de pedido de delivery efectuados en la fecha seleccionada. Intente con una fecha diferente." + "'); </script>")
            End If
        Catch ex As Exception
            strcadenaprint = String.Empty
            strcadenaprint = strcadenaprint & "<tr bgcolor=#DEE9FA class=Arial11B onMouseOut=this.className='Arial11B';return false>"
            strcadenaprint = strcadenaprint & "<td colspan='9' height=25 align=center>" & "NO SE ENCONTRARON DATOS" & "</td>"
            strcadenaprint = strcadenaprint & "</tr>"
            Response.Write("<script language=jscript> alert('" + ex.Message.ToString() + "'); </script>")
        End Try
    End Sub

    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click

        sFechaActual = Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000") 'TimeOfDay().Date.Now.ToShortDateString
        sHoraActual = TimeOfDay().Hour & ":" & TimeOfDay().Minute
        cargarTabla()

    End Sub
End Class
