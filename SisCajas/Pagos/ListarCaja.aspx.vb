Public Class ListarCaja
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents BotonOptm As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents cmdBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents lbOficina As System.Web.UI.WebControls.ListBox
    Protected WithEvents lbCodOficina As System.Web.UI.WebControls.ListBox
    Protected WithEvents hidCodOficina As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents loadDataHandler As System.Web.UI.WebControls.Button
    Protected WithEvents chkTodosOf As System.Web.UI.WebControls.CheckBox

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
    Dim nameFile As String = "Log_MantenimientoCajaOficina"
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogConsultaCuadre")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim strUsuario As String
    Dim strIdentifyLog As String
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
            If Not Page.IsPostBack Then
            End If
        End If
    End Sub

    Private Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        strUsuario = Session("USUARIO")
        strIdentifyLog = Session("ALMACEN") & "|" & strUsuario
        Try
            Dim strOficinaVta As String = hidCodOficina.Value
            Dim strUrl As String = "ListarCaja_CargarDatos.aspx"
            Dim strMsjErr As String = String.Empty

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio cmdBuscar_Click")

            If strOficinaVta.Equals(String.Empty) And Not chkTodosOf.Checked Then
                strMsjErr = "Seleccione oficina"
                Response.Write("<script>alert('" & strMsjErr & "');</script>")
                Exit Sub
            End If

            If chkTodosOf.Checked Then
                strOficinaVta = "0"
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   in oficina: " & strOficinaVta)

            Session("strOficinaDesc") = strOficinaVta
            Response.Redirect(strUrl)
        Catch ex1 As Threading.ThreadAbortException
            'Do Nothing
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   error cmdBuscar_Click: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin cmdBuscar_Click")
        End Try
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
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   error loadDataHandler_Click: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin IND - loadDataHandler_Click")
        End Try
    End Sub

#End Region

End Class
