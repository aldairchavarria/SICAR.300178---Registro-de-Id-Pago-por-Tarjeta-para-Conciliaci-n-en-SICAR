Imports SisCajas.GenFunctions
Imports SisCajas.clsAudi

Public Class rep_Anulacion
    Inherits System.Web.UI.Page


#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents CboCajero As System.Web.UI.WebControls.DropDownList
    Protected WithEvents DgAnulacion As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtFecha As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents btnBuscar As System.Web.UI.WebControls.Button

    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

#End Region
    '    Public strFecha As String
    Public auxfechaact
    'Introducir aquí el código de usuario para inicializar la página
    Public objComponente, objRecordSet, StrXml
    Public iAux, sFechaActual, sHoraActual, sTipoTienda, sAncho
    Public strFecha, DLin, DCol, sOficVenta, sValorA, sValorB
    Public auxprint As String = ""
    Dim dsCajero As New DataSet
    Dim dsAnulacion As New DataSet

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            Dim codUsuario As String = Session("USUARIO")
            Dim codPerfil As String = Session("PERFIL_SAP")
            strFecha = Request("strFecha")
            Dim strTipoUsuario As String = "C"

            'Dim ObjAux As New GenFunctions(Server, Request, Response)
            ' Dim ObjAudi As New clsAudi
            Dim objPoolPagos As New SAP_SIC_Pagos.clsPagos
            auxfechaact = Me.strFecha

            Session("sFecVenta") = strFecha

            objComponente = Nothing
            objRecordSet = Nothing

            sFechaActual = Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000") 'Now.Date 'Session("sFecVenta")
            sHoraActual = TimeOfDay().Hour & ":" & TimeOfDay().Minute
            sTipoTienda = Trim(Session("CANAL"))

            objRecordSet = Nothing
            objComponente = Nothing

            If Session("STRMessage") = "" Then
                sAncho = "500"
            Else
                sAncho = "200"
            End If

            Session("STRMessage") = ""
            If (Len(Session("STRMessage")) > 0) Then
                Response.Write("<script language=JavaScript type='text/javascript'>")
                Response.Write("alert('" & Session("STRMessage") & "');")
                Response.Write("</script>")
            End If
            Session("STRMessage") = ""

            If Not Page.IsPostBack Then
                txtFecha.Value = Format(Day(Now), "00") & "/" & Format(Month(Now), "00") & "/" & Format(Year(Now), "0000") 'Now.Date.ToString("d")
                dsCajero = objPoolPagos.Get_ConsultaCajeros(Session("ALMACEN"), strTipoUsuario)
                If codPerfil = "R" Or codPerfil = "S" Then
                    CboCajero.DataSource = dsCajero.Tables(0)
                    CboCajero.DataValueField = "Usuario"
                    CboCajero.DataTextField = "Nombre"
                    CboCajero.DataBind()
                    CboCajero.Items.Insert(0, " -- Seleccione -- ")
                Else
                    Dim drFila As DataRow

                    For Each drFila In dsCajero.Tables(0).Rows
                        'Response.Write(CStr(drFila("Usuario")))
                        If CStr(drFila("Usuario")) = codUsuario.PadLeft(10, "0") Then
                            Dim lstItem As New ListItem(CStr(drFila("Nombre")), CStr(drFila("Usuario")))
                            lstItem.Selected = True
                            CboCajero.Items.Add(lstItem)
                            Exit For
                        End If
                    Next
                End If
                'Cambiar por RFC de Pagos Anulados
                LlenaGrilla()
        End If
        End If
    End Sub

    Private Sub LlenaGrilla()

        Dim objPoolPagos As New SAP_SIC_Pagos.clsPagos
        'dsAnulacion = objPoolPagos.Get_ConsultaPoolFactura(Session("ALMACEN"), txtFecha.Value, "R", "", "", "", "20", "1")
        'dsAnulacion = objPoolPagos.Get_ConsultaPagosUsuario(txtFecha.Value, txtFecha.Value, "X", CboCajero.SelectedValue, Session("ALMACEN"))
        dsAnulacion = objPoolPagos.Get_ConsultaPagosUsuario(txtFecha.Value, "", "X", CboCajero.SelectedValue, Session("ALMACEN"))
        'Incidencia demora en la carga de pool documentos pagados --> Solicidtud de Luis Palacios enviar solo una fecha 18/06/2012

        DgAnulacion.DataSource = dsAnulacion.Tables(0)
        DgAnulacion.DataBind()

    End Sub


    Private Sub CboCajero_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CboCajero.SelectedIndexChanged

    End Sub

    Private Sub CboCajero_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub DataGrid1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub DgAnulacion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DgAnulacion.SelectedIndexChanged

    End Sub

    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        LlenaGrilla()
    End Sub
End Class
