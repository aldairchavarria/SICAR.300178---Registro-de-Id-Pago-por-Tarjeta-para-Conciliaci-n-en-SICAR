Public Class ConsultaCajaGen_cargardatos
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents DGLista As System.Web.UI.WebControls.DataGrid
    Protected WithEvents liberarHandler As System.Web.UI.WebControls.Button
    Protected WithEvents refreshHandler As System.Web.UI.WebControls.Button
    Protected WithEvents hidIDCajaGeneral As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents btnCancelar As System.Web.UI.WebControls.Button
    Protected WithEvents hidOficinas As System.Web.UI.HtmlControls.HtmlInputHidden 'INI-936 - JH

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
    Dim nameFile As String = "Log_ConsultaGeneral"
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogConsultaCuadre")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim strUsuario As String = String.Empty
    Dim strIdentifyLog As String = String.Empty
    Public tblExportar As DataTable 'INI-936 - JH
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
                'Inicio - INI-936 - JH - Modificada logica para consultar sesion "oficinas" en lugar
                hidOficinas.Value = Session("oficinas")
                Session("ExportarConsultaCajaGen") = Nothing

                If Not Session("oficinas") Is Nothing Then
                    CargarDatos()
                End If
                'Fin - INI-936 - JH***
            End If
        End If
    End Sub

    Private Sub liberarHandler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles liberarHandler.Click
        Try
            Dim strMsjErr As String = String.Empty
            Dim iIdCaja As Long = CInt(Request.Item("hidIDCajaGeneral"))
            objclsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio LiberarCuadreCajaGeneral ")
            strMsjErr = objclsAdmCaja.LiberarCuadreCajaGeneral(iIdCaja)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin LiberarCuadreCajaGeneral ")
            If Not strMsjErr.Equals(String.Empty) Then
                Response.Write("<script>alert('" & strMsjErr & "');</script>")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Mensaje: " & strMsjErr)
                Exit Sub
            Else
                Response.Write("<script language=jscript>alert('Liberado correctamente.'); </script>")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Mensaje: Liberado correctamente")
            End If
            CargarDatos()
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: " & ex.Message)
        Finally
            objclsAdmCaja = Nothing
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin liberarHandler ")
        End Try
    End Sub

    Private Sub refreshHandler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles refreshHandler.Click
        CargarDatos()
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Dim strURL As String
        strURL = "ConsultaCajaGen.aspx"
        Response.Redirect(strURL)
    End Sub

#End Region

#Region "Metodos"

    Private Sub CargarDatos()
        Try
            objclsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio CargarDatos ")
            Dim strOficinaVenta As String = hidOficinas.Value 'INI-936 - JH
            Dim strFechaInicio As String = CDate(Request.QueryString("fi")).ToString("yyyyMMdd")
            Dim strFechaFin As String = CDate(Request.QueryString("ff")).ToString("yyyyMMdd")
            Dim strCodCajero As String = Request.QueryString("cc")
            Dim strEstado As String = Request.QueryString("st")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio GetCajaGeneral ")
            Dim dsDatos As DataSet = objclsAdmCaja.GetCajaGeneral(strOficinaVenta, strFechaInicio, _
                                                    strFechaFin, strCodCajero, strEstado)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin GetCajaGeneral ")
            Dim dsCajero As DataSet = objclsAdmCaja.GetVendedores(String.Empty, strOficinaVenta, optTODOS)

            Dim dtResult As New DataTable
            Dim drFila As DataRow

            dtResult.Columns.Add("ID_TRS_CUADRE_GEN", GetType(System.String))
            dtResult.Columns.Add("CODOFICINA", GetType(System.String))
            dtResult.Columns.Add("OFICINA", GetType(System.String))
            dtResult.Columns.Add("CODCAJERO", GetType(System.String))
            dtResult.Columns.Add("CAJERO", GetType(System.String))
            dtResult.Columns.Add("NOMBRE_CAJA", GetType(System.String)) 'INICIATIVA-318
            dtResult.Columns.Add("FECHA", GetType(System.String))
            dtResult.Columns.Add("FECHA_CIERRE", GetType(System.String))
            dtResult.Columns.Add("DIAS_PENDIENTES", GetType(System.String)) 'INICIATIVA-318
            dtResult.Columns.Add("ESTADO", GetType(System.String))
            dtResult.Columns.Add("CUADRE_REALIZADO", GetType(System.String))
            dtResult.Columns.Add("LIBERAR", GetType(System.String))

            For i As Int32 = 0 To dsDatos.Tables(0).Rows.Count - 1
                drFila = dtResult.NewRow

                If Not IsDBNull(dsDatos.Tables(0).Rows(i).Item("ID_TRS_CUADRE_GEN")) Then
                    drFila("ID_TRS_CUADRE_GEN") = dsDatos.Tables(0).Rows(i).Item("ID_TRS_CUADRE_GEN")
                Else
                    drFila("ID_TRS_CUADRE_GEN") = String.Empty
                End If

                If Not IsDBNull(dsDatos.Tables(0).Rows(i).Item("CODOFICINA")) Then
                    drFila("CODOFICINA") = dsDatos.Tables(0).Rows(i).Item("CODOFICINA")
                Else
                    drFila("CODOFICINA") = String.Empty
                End If

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
                    drFila("CODCAJERO") = dsDatos.Tables(0).Rows(i).Item("CAJERO")
                Else
                    drFila("CODCAJERO") = String.Empty
                End If

                'INICIATIVA-318 INI
                If Not IsDBNull(dsDatos.Tables(0).Rows(i).Item("NOMBRE_CAJA")) Then
                    drFila("NOMBRE_CAJA") = dsDatos.Tables(0).Rows(i).Item("NOMBRE_CAJA")
                Else
                    drFila("NOMBRE_CAJA") = String.Empty
                End If
                'INICIATIVA-318 FIN

                If Not IsDBNull(dsDatos.Tables(0).Rows(i).Item("FECHA_CIERRE")) Then
                    drFila("FECHA_CIERRE") = dsDatos.Tables(0).Rows(i).Item("FECHA_CIERRE")
                Else
                    drFila("FECHA_CIERRE") = String.Empty
                End If

                'INICIATIVA-318 INI
                If Not IsDBNull(dsDatos.Tables(0).Rows(i).Item("DIAS_PENDIENTES")) Then
                    drFila("DIAS_PENDIENTES") = dsDatos.Tables(0).Rows(i).Item("DIAS_PENDIENTES")
                Else
                    drFila("DIAS_PENDIENTES") = String.Empty
                End If
                'INICIATIVA-318 FIN

                If Not IsDBNull(dsDatos.Tables(0).Rows(i).Item("CAJERO")) Then
                    Dim cajero As String = CStr(dsDatos.Tables(0).Rows(i).Item("CAJERO"))
                    Dim dvCaj As New DataView
                    dvCaj.Table = dsCajero.Tables(0)
                    dvCaj.RowFilter = "CODIGO = '" & cajero & "'"
                    If dvCaj.Count > 0 Then
                        drFila("CAJERO") = Trim(dvCaj.Item(0).Item("DESCRIPCION"))
                    Else
                        drFila("CAJERO") = String.Empty
                    End If
                Else
                    drFila("CAJERO") = String.Empty
                End If

                If Not IsDBNull(dsDatos.Tables(0).Rows(i).Item("ESTADO")) Then
                    drFila("ESTADO") = dsDatos.Tables(0).Rows(i).Item("ESTADO")
                Else
                    drFila("ESTADO") = String.Empty
                End If

                If Not IsDBNull(dsDatos.Tables(0).Rows(i).Item("CUADRE_REALIZADO")) Then
                    drFila("CUADRE_REALIZADO") = dsDatos.Tables(0).Rows(i).Item("CUADRE_REALIZADO")
                Else
                    drFila("CUADRE_REALIZADO") = String.Empty
                End If

                If Not IsDBNull(dsDatos.Tables(0).Rows(i).Item("LIBERAR")) Then
                    drFila("LIBERAR") = dsDatos.Tables(0).Rows(i).Item("LIBERAR")
                Else
                    drFila("LIBERAR") = String.Empty
                End If

                dtResult.Rows.Add(drFila)
            Next

            Me.DGLista.DataSource = dtResult
            Me.DGLista.DataBind()
            tblExportar = dtResult 'INI-936 - JH

            Session("ConsultaCajaGenExc") = Nothing

            If dtResult.Rows.Count <= 0 Then
                Response.Write("<script language=jscript> alert('No se encontró datos'); </script>")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: No se encontró datos.")
            End If
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: " & ex.Message)
        Finally
            objclsAdmCaja = Nothing
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin CargarDatos ")
        End Try
    End Sub

#End Region

End Class
