Public Class ConsultaCajaInd_detalle
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents DGLista As System.Web.UI.WebControls.DataGrid

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
    Private Const optTODOS As String = "0"
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = "Log_ConsultaIndividual"
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogConsultaCuadre")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim strUsuario As String = String.Empty
    Dim strIdentifyLog As String = String.Empty
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
                If Not Request.QueryString("ov") Is Nothing Then
                    CargarDatos()
                End If
            End If
        End If
    End Sub

#End Region

#Region "Metodos"
    Private Sub CargarDatos()
        Dim objClsOffline As New COM_SIC_OffLine.clsOffline
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Detalle - Ind - Fin -- Inicio CargarDatos ")
            objclsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas
            Dim strOficinaVenta As String = Request.QueryString("ov")
            Dim strFecha As String = Request.QueryString("fc")
            Dim strCodCajero As String = Request.QueryString("cc")


            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio GetMontoCuadre ")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fecha " & CDate(strFecha).ToString("yyyyMMdd"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Cajero " & strCodCajero)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Oficina " & strOficinaVenta)
            Dim dsDatos As DataSet = objClsOffline.GetMontoCuadre(strOficinaVenta, CDate(strFecha).ToString("yyyyMMdd"), strCodCajero)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin GetMontoCuadre ")
            Dim dsCajero As DataSet = objclsAdmCaja.GetVendedores(String.Empty, strOficinaVenta, optTODOS)

            Dim dtResult As New DataTable
            Dim drFila As DataRow

            dtResult.Columns.Add("OFICINA", GetType(System.String))
            dtResult.Columns.Add("FECHA", GetType(System.String))
            dtResult.Columns.Add("CAJERO", GetType(System.String))
            dtResult.Columns.Add("DESCRIPCION", GetType(System.String))
            dtResult.Columns.Add("MONTO", GetType(System.String))

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Cantidad : " & dsDatos.Tables(0).Rows.Count.ToString())
            For i As Int32 = 0 To dsDatos.Tables(0).Rows.Count - 1
                drFila = dtResult.NewRow

                If Not IsDBNull(dsDatos.Tables(0).Rows(i).Item("OFICINA")) Then
                    drFila("OFICINA") = dsDatos.Tables(0).Rows(i).Item("OFICINA")
                Else
                    drFila("OFICINA") = String.Empty
                End If

                If Not IsDBNull(dsDatos.Tables(0).Rows(i).Item("FECHA")) Then
                    drFila("FECHA") = dsDatos.Tables(0).Rows(i).Item("FECHA")
                Else
                    drFila("FECHA") = String.Empty
                End If

                If Not IsDBNull(dsDatos.Tables(0).Rows(i).Item("CAJERO")) Then
                    Dim cajero As String = CStr(dsDatos.Tables(0).Rows(i).Item("CAJERO"))
                    Dim dvCaj As New DataView
                    dvCaj.Table = dsCajero.Tables(0)
                    dvCaj.RowFilter = "CODIGO = '" & cajero & "'"
                    With dvCaj
                        drFila("CAJERO") = Trim(.Item(0).Item("DESCRIPCION"))
                    End With
                Else
                    drFila("CAJERO") = String.Empty
                End If

                If Not IsDBNull(dsDatos.Tables(0).Rows(i).Item("DESC_CONCEPTO")) Then
                    drFila("DESCRIPCION") = dsDatos.Tables(0).Rows(i).Item("DESC_CONCEPTO")
                Else
                    drFila("DESCRIPCION") = String.Empty
                End If

                If Not IsDBNull(dsDatos.Tables(0).Rows(i).Item("MONTO")) Then
                    drFila("MONTO") = dsDatos.Tables(0).Rows(i).Item("MONTO")
                Else
                    drFila("MONTO") = String.Empty
                End If

                dtResult.Rows.Add(drFila)
            Next

            Session("ConsultaCajaIndExp") = dtResult

            Me.DGLista.DataSource = dtResult
            Me.DGLista.DataBind()

            If dtResult.Rows.Count <= 0 Then
                Response.Write("<script language=jscript> alert('No se encontró datos'); </script>")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Detalle - Ind - Fin -- Mensaje: No se encontró datos ")
            End If
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Detalle - Ind - Fin -- Mensaje: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Detalle - Ind - Fin -- Detalle - Fin CargarDatos ")
            objClsOffline = Nothing
            objclsAdmCaja = Nothing
        End Try
    End Sub

#End Region

End Class
