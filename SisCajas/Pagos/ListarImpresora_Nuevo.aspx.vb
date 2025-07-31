Public Class ListarImpresora_Nuevo
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTitNuevoTicketera As System.Web.UI.WebControls.Label
    Protected WithEvents lblOficina As System.Web.UI.WebControls.Label    
    Protected WithEvents lblCaja As System.Web.UI.WebControls.Label
    Protected WithEvents lblDescripcion As System.Web.UI.WebControls.Label
    Protected WithEvents lblSerie As System.Web.UI.WebControls.Label
    Protected WithEvents txtCaja As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtDescripcion As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtSerie As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents cmdGrabar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents hidCodOficina As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtOficina As System.Web.UI.WebControls.TextBox
    Protected WithEvents loadDataHandler As System.Web.UI.WebControls.Button

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
    Private objclsAdmCaja As COM_SIC_Adm_Cajas.clsAdmCajas
    Private objFileLog As New SICAR_Log
    Private nameFile As String = "Log_MantenimientoTicketeraOficina"
    Private pathFile As String = ConfigurationSettings.AppSettings("constRutaLogMantTicketera")
    Private strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Private strUsuario As String
    Private strIdentifyLog As String
#End Region

#Region "Evento"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            If Not Page.IsPostBack Then
                Dim dsOficinas As DataSet
                txtOficina.Enabled = False

                strUsuario = Session("USUARIO")
                strIdentifyLog = Session("ALMACEN") & "|" & strUsuario

                If Not Request.QueryString("CodTicketOfi") Is Nothing Then
                    If CInt(Request.QueryString("CodTicketOfi")) = 0 Then 'Nuevo
                        txtDescripcion.Value = String.Empty
                        txtCaja.Value = String.Empty
                        txtSerie.Value = String.Empty
                    Else 'Editar
                        lblTitNuevoTicketera.Text = "Modificar Ticketera"
                        CargarDatos(CLng(Request.QueryString("CodTicketOfi")))
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub cmdGrabar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGrabar.ServerClick
        strUsuario = Session("USUARIO")
        strIdentifyLog = Session("ALMACEN") & "|" & strUsuario
        Try
            Dim strOficina As String = Request.Item("hidCodOficina")
            Dim strMsjErr As String = String.Empty
            Dim strMsg As String = String.Empty
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Ini cmdGrabar_ServerClick")

            Dim lCodImpOfic As Long = CLng(Request.QueryString("CodTicketOfi"))
            objclsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas

            If strOficina.Equals(String.Empty) Then
                Response.Write("<script>alert('Seleccionar una Oficina')</script>")
                Exit Sub
            End If

            If Trim(txtCaja.Value) = "" Then
                Response.Write("<script>alert('Ingrese el Nro Ticketera');</script>")
                Exit Sub
            End If

            Dim bEditar As Boolean = False

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   In strOfiVta: " & strOficina)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   In Descripcion: " & CStr(txtDescripcion.Value))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   In Caja: " & CStr(txtCaja.Value))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   In Serie: " & CStr(txtSerie.Value))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   In Usado Por: " & CStr(Session("strUsuario")))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   In codigo: " & lCodImpOfic)
            If lCodImpOfic = 0 Then ' nuevo
                strMsjErr = objclsAdmCaja.SetTicketeraOficina(strOficina, CStr(txtCaja.Value.Trim()), CStr(txtDescripcion.Value.Trim()), CStr(txtSerie.Value.Trim()), CStr(Session("strUsuario")))
                bEditar = False
            Else 'editar
                strMsjErr = objclsAdmCaja.ActualizarTicketeraOficina(lCodImpOfic, strOficina, CStr(txtCaja.Value.Trim()), CStr(txtDescripcion.Value.Trim()), CStr(txtSerie.Value.Trim()), CStr(Session("strUsuario")))
                bEditar = True
            End If

            If Not strMsjErr.Equals(String.Empty) Then
                Response.Write("<script>alert('" & strMsjErr & "');</script>")
                Exit Sub
            Else
                If bEditar Then
                    strMsg = "Se guardó los cambios correctamente."
                Else
                    strMsg = "Se registro correctamente."
                End If
            End If
            Response.Write("<script>window.opener.f_Refrescar();</script>")
            Response.Write("<script language=jscript> alert('" + strMsg + "'); </script>")
            Response.Write("<script>window.close();</script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Mensaje: " & strMsg)
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: " & ex.Message)
        Finally
            objclsAdmCaja = Nothing
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN  cmdGrabar_ServerClick")
        End Try
    End Sub

    Private Sub loadDataHandler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles loadDataHandler.Click
        strUsuario = Session("USUARIO")
        strIdentifyLog = Session("ALMACEN") & "|" & strUsuario
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
                End If
            End If
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   error loadDataHandler_Click: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin IND - loadDataHandler_Click")
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub CargarDatos(ByVal IdImpOficina As Long)
        strUsuario = Session("USUARIO")
        strIdentifyLog = Session("ALMACEN") & "|" & strUsuario
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Ini CargarDatos")
        objclsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas
        Dim dsDatosCaja As DataSet
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP ID_LISTA :" & IdImpOficina)
        dsDatosCaja = objclsAdmCaja.GetTicketeraOficina(String.Empty, String.Empty, IdImpOficina)
        Try
            If Not dsDatosCaja Is Nothing Then
                Dim drDatosCaja As DataRow
                drDatosCaja = dsDatosCaja.Tables(0).Rows(0)
                If Not drDatosCaja Is Nothing Then
                    If Not IsDBNull(drDatosCaja("COD_OFICINA")) Then
                        hidCodOficina.Value = drDatosCaja("COD_OFICINA")
                        txtOficina.Text = drDatosCaja("COD_OFICINA") & " - " & drDatosCaja("OFICINA")
                    End If

                    If Not IsDBNull(drDatosCaja("CAJA")) Then
                        txtCaja.Value = drDatosCaja("CAJA")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Nro Ticktera :" & txtCaja.Value)
                    End If

                    If Not IsDBNull(drDatosCaja("DESCRIPCION")) Then
                        txtDescripcion.Value = drDatosCaja("DESCRIPCION")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Descripcion :" & txtDescripcion.Value)
                    End If

                    If Not IsDBNull(drDatosCaja("SERIE")) Then
                        txtSerie.Value = drDatosCaja("SERIE")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP Serie :" & txtSerie.Value)
                    End If

                End If
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error:" & ex.Message.ToString())
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin CargarDatos")
        End Try
    End Sub

#End Region

End Class
