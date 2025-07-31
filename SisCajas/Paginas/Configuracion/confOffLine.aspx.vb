Public Class confOffLine
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
    Protected WithEvents chkSAP As System.Web.UI.WebControls.CheckBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim objOffLine As New COM_SIC_OffLine.clsOffline
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Write("<script language='javascript' src='../../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            Dim intSAP = objOffLine.Get_ConsultaSAP

            If Not Page.IsPostBack Then
                If intSAP = 1 Then
                    chkSAP.Checked = True
                Else
                    chkSAP.Checked = False
                End If
            End If
        End If
    End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click

        Dim dsRecaudacion As DataSet
        Dim dsLogRecaudacion As DataSet
        Dim dsEstRecaudacion As DataSet
        Dim i As Integer

        If chkSAP.Checked Then
            'procesar aqui todas las recaudaciones offline

            Dim objSAP As New SAP_SIC_Recaudacion.clsRecaudacion

            dsRecaudacion = objOffLine.Get_Con_Recaudacion
            dsLogRecaudacion = objOffLine.Get_Con_LogRecaudacion
            dsEstRecaudacion = objOffLine.Get_Con_EstRecaudacion

            For i = 0 To dsLogRecaudacion.Tables(0).Rows.Count - 1
                'MsgBox("ingreso:" & dsLogRecaudacion.Tables(0).Rows(i).Item("logrc_ingreso"))
                objSAP.Set_LogRecaudacion(dsLogRecaudacion.Tables(0).Rows(i).Item("logrc_ingreso"), IIf(IsDBNull(dsLogRecaudacion.Tables(0).Rows(i).Item("logrc_documlog")), "", dsLogRecaudacion.Tables(0).Rows(i).Item("logrc_documlog")))
                Threading.Thread.Sleep(500)
            Next

            For i = 0 To dsRecaudacion.Tables(0).Rows.Count - 1
                objSAP.Set_RegistroDeuda(dsRecaudacion.Tables(0).Rows(i).Item("RECAU_DEUDA"), dsRecaudacion.Tables(0).Rows(i).Item("RECAU_RECIBOS"), dsRecaudacion.Tables(0).Rows(i).Item("RECAU_PAGOS"), "")
                Threading.Thread.Sleep(500)
            Next

            For i = 0 To dsEstRecaudacion.Tables(0).Rows.Count - 1
                objSAP.Set_EstadoRecaudacion(dsEstRecaudacion.Tables(0).Rows(i).Item("ESTREC_TRACEORG"), dsEstRecaudacion.Tables(0).Rows(i).Item("ESTREC_ESTAN"), dsEstRecaudacion.Tables(0).Rows(i).Item("ESTREC_POSTMP"), dsEstRecaudacion.Tables(0).Rows(i).Item("ESTREC_TRACEANL"))
                Threading.Thread.Sleep(500)
            Next

            'objOffLine.Get_EliminaOffline()

            'objOffLine.Set_RegSAP(1)
        Else
            objOffLine.Set_RegSAP(0)
        End If

    End Sub
End Class
