Public Class depGaranManual
    Inherits System.Web.UI.Page

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cboViaPago As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cbotipoDoc As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtNumDocumento As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtFecReg As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtFecVenc As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtImporte As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtCorrRef As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
    Protected WithEvents txtTipoCargo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNumOperacion As System.Web.UI.WebControls.TextBox

    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

#End Region

    Dim mstrCodOficina As String
    Public gIntCarneExtranjeriaMax As Integer

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            mstrCodOficina = Session("ALMACEN")
            gIntCarneExtranjeriaMax = ConfigurationSettings.AppSettings("gIntCarneExtranjeriaMax")

            If Not Page.IsPostBack Then
                LlenaCombos()
                txtFecReg.Value = Now.Date.ToString("d")
                txtFecVenc.Value = Now.Date.ToString("d")
            End If

            If Me.txtCorrRef.Value.Trim().Length = 0 Then
                GeneraCorrelativo()
            End If
        End If
    End Sub


    Private Sub GeneraCorrelativo()
        Dim obSAP As New SAP_SIC_Pagos.clsPagos
        Dim strUltNum As String
        Dim dsTmp As DataSet = obSAP.Get_NroCorrGarantia(mstrCodOficina, ConfigurationSettings.AppSettings("cteDesTipoCargoFijoDG"), strUltNum)
        Me.txtCorrRef.Value = strUltNum
    End Sub

    Private Sub LlenaCombos()
        Dim obSAP As New SAP_SIC_Pagos.clsPagos

        '*****vias pago
        Dim dsVias As DataSet = obSAP.Get_ConsultaViasPago(mstrCodOficina)
        cboViaPago.DataSource = dsVias.Tables(0)
        cboViaPago.DataTextField = "VTEXT"
        cboViaPago.DataValueField = "CCINS"
        cboViaPago.DataBind()
        cboViaPago.SelectedValue = "ZEFE"
        '******** Tipos Documentos de Identidad
        cbotipoDoc.Items.Clear()
        Dim dsTipDoc As DataSet = obSAP.Get_LeeTipoDocCliente()
        If dsTipDoc.Tables(0).Rows.Count > 0 Then
            Dim drFila As DataRow
            For Each drFila In dsTipDoc.Tables(0).Rows
                If CStr(drFila(0)) <> "-" Then
                    cbotipoDoc.Items.Add(New ListItem(CStr(drFila(1)), CStr(drFila(0))))
                End If
            Next
        End If

    End Sub

    Public Sub Grabar()

        Dim obSap As New SAP_SIC_Pagos.clsPagos
        '*************** MONEDA
        Dim strMoneda As String = ""
        '***************usuario
        Dim strUsuario As String = ""
        Dim strBELNR As String
        Dim strXBLNR As String

        Dim dsResult As DataSet = obSap.Set_DepositoGarantia("", Me.cbotipoDoc.SelectedValue, Me.txtNumDocumento.Value, mstrCodOficina, Me.txtFecReg.Value, Me.txtFecVenc.Value, Me.txtImporte.Value, strMoneda, Me.cboViaPago.SelectedValue, Me.txtCorrRef.Value, strUsuario, ConfigurationSettings.AppSettings("cteDesTipoCargoFijoDG"), "0", txtNumOperacion.Text, "", strBELNR, strXBLNR)
        If dsResult.Tables(0).Rows.Count > 0 Then
            Dim drFila As DataRow
            For Each drFila In dsResult.Tables(0).Rows
                If drFila("TYPE") = "E" Then
                    Throw New ApplicationException(drFila("MESSAGE"))
                End If
            Next
        End If
    End Sub


    Private Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Try
            Grabar()
            txtImporte.Value = ""
            txtNumDocumento.Value = ""
            GeneraCorrelativo()
            Response.Write("<script language=javascript> alert('El proceso se termino con exito'); </script> ")
        Catch ex As Exception
            Response.Write("<script language=javascript> alert('" + ex.Message + "'); </script> ")
        End Try
    End Sub


End Class
