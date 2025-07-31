Public Class sicar_fe_comprobante
    Inherits SICAR_WebBase 'PROY-24724 - IIteracion 3
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents hidEstdFinalSini As System.Web.UI.HtmlControls.HtmlInputText 'PROY-24724 - Iteracion 2 Siniestros 
    Protected WithEvents hidCodMaterialSiniestro As System.Web.UI.HtmlControls.HtmlInputText 'PROY-24724 - Iteracion 2 Siniestros 
    Protected WithEvents btnImprimir As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents btnCancelar As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents hidIgvActual As System.Web.UI.HtmlControls.HtmlInputHidden 'PROY- 31766'
    Protected WithEvents hidIgvActualP As System.Web.UI.HtmlControls.HtmlInputHidden 'PROY- 31766'
		
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecargaVirtual")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        'PROY-24724 - Iteracion 2 Siniestros 
        hidEstdFinalSini.Value = Funciones.CheckStr((clsKeyAPP.strEstdFinalizadoPagoSiniestro).Split(";")(0))
        hidCodMaterialSiniestro.Value = Funciones.CheckStr(clsKeyAPP.strCodMaterialSiniestro)
		
        'PROY- 31766'
        Try
            Dim strCodRpta As String = String.Empty
            Dim strMsgRpta As String = String.Empty
            hidIgvActual.Value = Nothing
            hidIgvActualP.Value = ""
            Dim objConsultaIGV As New COM_SIC_Activaciones.clsWConsultaIGV
            objFileLog.Log_WriteLog(pathFile, strArchivo, "== INICIO - Proceso Met. ObtenerIGVActual() ==")
            Dim constIGV As Double = objConsultaIGV.ObtenerIGV(strCodRpta, strMsgRpta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "     Valor IGV: " & constIGV)
            hidIgvActual.Value = constIGV
            objFileLog.Log_WriteLog(pathFile, strArchivo, "     Valor IGV 1: " & hidIgvActual.Value)
            hidIgvActualP.Value = (constIGV * 100)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "     Valor IGV 2: " & hidIgvActualP.Value)
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "== ERROR -" & ex.Message.ToString & "==")
            hidIgvActual.Value = System.Configuration.ConfigurationSettings.AppSettings("valorIGV")
            hidIgvActualP.Value = System.Configuration.ConfigurationSettings.AppSettings("valorIGVImpresion").ToString()
            objFileLog.Log_WriteLog(pathFile, strArchivo, "     Valor IGV 1: " & hidIgvActual.Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "     Valor IGV 2: " & hidIgvActualP.Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "FIN")
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, "== FIN - Proceso Met. ObtenerIGVActual() ==")
        End Try

        'PROY- 31766'
		
    End Sub

End Class
