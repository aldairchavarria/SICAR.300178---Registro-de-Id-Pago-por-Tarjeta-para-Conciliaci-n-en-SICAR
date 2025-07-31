Public Class devolDepo
    Inherits System.Web.UI.Page

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtCorrRef As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtFecReg As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button

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
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            If Not Page.IsPostBack Then
                txtFecReg.Value = Now.Date.ToString("d")
            End If
        End If
    End Sub



    Private Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click

        Try
            If ValidaDatos() Then
                GrabarDevolucion()
                txtCorrRef.Value = ""
            End If
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
        End Try
    End Sub


    Public Sub GrabarDevolucion()

        '**************** PARAMETROS
        Dim strTipoOfiVta As String = Session("CANAL")
        Dim strUsuario As String = Session("USUARIO")
        Dim strOficina As String = Session("ALMACEN")
        '*********************

        Dim strFecha As String
        '**********************


        strFecha = txtFecReg.Value

        Dim obSap As New SAP_SIC_Pagos.clsPagos
        Dim dsResult As DataSet

        If strTipoOfiVta = "MT" Then
            dsResult = obSap.Set_DevolDepositoGarantia(strOficina, Me.txtCorrRef.Value, strFecha, strUsuario)
        Else
            dsResult = obSap.Set_DevolDepositoGarantia(strOficina, Me.txtCorrRef.Value, strFecha, "")
        End If
        If dsResult.Tables(0).Rows.Count > 0 Then
            Dim drMsg As DataRow
            For Each drMsg In dsResult.Tables(0).Rows
                If CStr(drMsg("TYPE")) = "E" Then
                    Throw New ApplicationException(CStr(drMsg("MESSAGE")))
                End If
            Next
        End If

    End Sub

    Private Function ValidaDatos() As Boolean
        Dim bresult As Boolean = True

        If txtFecReg.Value.Trim().Length = 0 Then
            Throw New ApplicationException("No ha ingresado la fecha de registro...!!")
        End If

        If Not IsDate(txtFecReg.Value) Then
            Throw New ApplicationException("Error en el formato de la fecha...!!")
        End If

        If txtCorrRef.Value.Trim().Length = 0 Then
            Throw New ApplicationException("No ha ingresado el número correlativo...!!")
        End If

        Return bresult
    End Function



End Class
