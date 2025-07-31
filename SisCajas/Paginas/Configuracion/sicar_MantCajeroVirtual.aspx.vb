Imports COM_SIC_Configura
'Imports System.Drawing
Imports System.Web.UI.WebControls


Public Class MantCajeroVirtual
    Inherits SICAR_WebBase

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents gridDetalle As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnCancelar As System.Web.UI.WebControls.Button
    Protected WithEvents btnBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents btnLimpiar As System.Web.UI.WebControls.Button
    Protected WithEvents ddlOficinaVenta As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCajero As System.Web.UI.WebControls.DropDownList

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
        Response.Write("<script language='javascript' src='../../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            If Not Page.IsPostBack Then
                Inicio()
            Else
                'BuscarCajeroVirtual("", "", "")
            End If
        End If
    End Sub

    Private Sub Inicio()
        ddlCajero.Enabled = False
        LlenaComboOficinaVenta()
        'Me.txtMonto.Attributes.Add("onkeypress", "javascript:ValidaNumero(this)")
        'BuscarRecargas("", "", "")
    End Sub

    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click

        BuscarCajeroVirtual(ddlOficinaVenta.SelectedValue, ddlCajero.SelectedValue)

    End Sub

    Private Sub BuscarCajeroVirtual(ByVal strOficinaVenta As String, ByVal strCodCajero As String)

        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Dim dsCajeroVirtual As New System.Data.DataSet
        Dim dtCajeroVirtual As New System.Data.DataTable
        Dim CodRpta As String = String.Empty
        Dim MsjRpta As String = String.Empty

        strOficinaVenta = IIf(strOficinaVenta = "SELECCIONE", String.Empty, strOficinaVenta)
        strCodCajero = IIf(strCodCajero = "SELECCIONE", String.Empty, strCodCajero)

        If Funciones.CheckStr(strOficinaVenta) = String.Empty And Funciones.CheckStr(strCodCajero) = String.Empty Then
            Response.Write("<script>alert('Ingrese datos para la busqueda.')</script>")
            Exit Sub
        End If

        dsCajeroVirtual = objOffline.Get_MantCajeroVirtual(strOficinaVenta, strCodCajero, CodRpta, MsjRpta)
        If Not dsCajeroVirtual.Tables Is Nothing And dsCajeroVirtual.Tables.Count > 0 Then
            dtCajeroVirtual = dsCajeroVirtual.Tables(0)
            gridDetalle.DataSource = dtCajeroVirtual
            gridDetalle.DataBind()
        Else
            gridDetalle.DataSource = New ArrayList
            gridDetalle.DataBind()

            Response.Write("<script>alert('No existen registros para la consulta.')</script>")
        End If
        'RegistrarAuditoria()


    End Sub

    Private Sub btnLimpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click

        'txtMonto.Text = ""
        'txtDescripcion.Text = ""

        gridDetalle.DataSource = New ArrayList
        gridDetalle.DataBind()

    End Sub

    Private Sub LlenaComboOficinaVenta()
        Dim dtEstado As New System.Data.DataTable
        Dim objOficinas As New COM_SIC_OffLine.clsOffline
        Dim dsOficinas As DataSet

        dsOficinas = objOficinas.Get_ConsultaOficinaVenta("", "")
        dtEstado = dsOficinas.Tables(0)



        ddlOficinaVenta.DataSource = dtEstado
        ddlOficinaVenta.DataValueField = "VKBUR"
        ddlOficinaVenta.DataTextField = "BEZEI"
        ddlOficinaVenta.DataBind()
        ddlOficinaVenta.Items.Insert(0, "SELECCIONE")

    End Sub


    Public Sub ddlOficinaVenta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOficinaVenta.SelectedIndexChanged
        LlenarCajero(ddlOficinaVenta.SelectedValue)
    End Sub

    Public Sub LlenarCajero(ByVal ValorCombo As String)
        If ValorCombo <> "0" Then
            ddlCajero.Enabled = True
            Dim objOffline As New COM_SIC_OffLine.clsOffline
            Dim dsCajeros As DataSet
            dsCajeros = objOffline.Get_ConsultaCajeros(ddlOficinaVenta.SelectedValue, "C")
            If Not dsCajeros Is Nothing Then
                ddlCajero.DataTextField = "NOMBRE"
                ddlCajero.DataValueField = "USUARIO"
                ddlCajero.DataSource = dsCajeros.Tables(0)
                ddlCajero.DataBind()
                ddlCajero.Items.Insert(0, "SELECCIONE")
            End If
        Else
            ddlCajero.Items.Insert(0, "SELECCIONE")
            ddlCajero.Enabled = False
        End If

    End Sub

    Private Sub gridDetalle_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gridDetalle.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim img As New Image

            Dim strEstado As String = Funciones.CheckInt(e.Item.Cells(7).Text)
            If strEstado = 1 Then
                img.ImageUrl = "../../images/botones/imgColorVerde.gif"
                e.Item.Cells(8).Controls.Add(img)
            Else
                img.ImageUrl = "../../images/botones/imgColorRojo.gif"
                e.Item.Cells(8).Controls.Add(img)
            End If


        End If
    End Sub
End Class
