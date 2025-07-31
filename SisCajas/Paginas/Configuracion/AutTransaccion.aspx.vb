Imports System.Data.OracleClient
Public Class AutTransaccion
    Inherits System.Web.UI.Page
    Dim dsData As New DataSet
    Dim SelAutoriza As New DropDownList
    Dim objConfig As New COM_SIC_Configura.clsConfigura
    Protected WithEvents btnCancelar As System.Web.UI.HtmlControls.HtmlInputButton
    Dim i As Integer
#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

	End Sub
    Protected WithEvents DGAutorizaTran As System.Web.UI.WebControls.DataGrid
    Protected WithEvents cmdGrabar As System.Web.UI.HtmlControls.HtmlInputButton

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
        'Dim orConector As New OracleConnection
        'Dim orComando As New OracleCommand
        'Dim orDataAd As New OracleDataAdapter
        Response.Write("<script language='javascript' src='../../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            If Not Page.IsPostBack Then
                'orConector.ConnectionString = "user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"
                'orConector.Open()

                'orComando.Connection = orConector
                'orComando.Parameters.Add("K_CODCANAL", OracleType.VarChar, 5)
                'orComando.Parameters.Add("K_APLIC_COD", OracleType.Number)
                'orComando.Parameters.Add("K_COD_PDV", OracleType.VarChar, 5)
                'orComando.Parameters.Add("K_FEHC_TRX", OracleType.DateTime)
                'orComando.Parameters.Add("K_CUR_LISTAUT", OracleType.Cursor)

                'orComando.Parameters(0).Value = Session("CANAL")
                'orComando.Parameters(1).Value = ConfigurationSettings.AppSettings("codAplicacion")
                'orComando.Parameters(2).Value = Session("ALMACEN")
                'orComando.Parameters(3).Value = Request("strFecha")

                'orComando.Parameters(0).Direction = ParameterDirection.Input
                'orComando.Parameters(1).Direction = ParameterDirection.Input
                'orComando.Parameters(2).Direction = ParameterDirection.Input
                'orComando.Parameters(3).Direction = ParameterDirection.Input
                'orComando.Parameters(4).Direction = ParameterDirection.Output

                'orComando.CommandText = "CONF_PARAMETROS_CAJA.CONF_LISTA_AUT_TRANSACCION"
                'orComando.CommandType = CommandType.StoredProcedure

                'orDataAd.SelectCommand = orComando
                'orDataAd.Fill(dsData)
                dsData = objConfig.FP_Lista_Aut_Transaccion(Session("CANAL"), ConfigurationSettings.AppSettings("codAplicacion"), Session("ALMACEN"), Request("strFecha"))
                DGAutorizaTran.DataSource = dsData.Tables(0)
                DGAutorizaTran.DataBind()
        End If
        End If
    End Sub

    Private Sub DGAutorizaTran_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DGAutorizaTran.ItemDataBound
        Dim s As DropDownList
        Dim t As DropDownList
        Dim txtMot As TextBox
        Dim dsMotivos As DataSet
        s = CType(e.Item.FindControl("DropDownList1"), DropDownList)
        If Not IsNothing(s) Then
            s.Items.Insert(0, New ListItem("SI", "1"))
            s.Items.Insert(1, New ListItem("NO", "2"))
            s.SelectedValue = dsData.Tables(0).Rows(e.Item.ItemIndex).Item("AUTRAN_VALIDA")
        End If

        t = CType(e.Item.FindControl("cboMotivo"), DropDownList)
        If Not IsNothing(t) Then
            dsMotivos = objConfig.FP_Lista_Motivos()
            t.DataValueField = "MOTIV_CODMOTIVO"
            t.DataTextField = "MOTIV_DESMOTIVO"
            t.DataSource = dsMotivos.Tables(0)
            t.DataBind()
            t.Items.Insert(0, New ListItem("", ""))
            t.Attributes.Add("onChange", "f_OtroMotivo(" & (e.Item.ItemIndex + 1) & ")")
        End If

        txtMot = CType(e.Item.FindControl("txtOtroMot"), TextBox)
        If Not IsNothing(txtMot) Then
            txtMot.Attributes.Add("style", "Display:none")
            txtMot.Attributes.Add("onkeypress", "e_mayuscula()")
        End If


    End Sub

    Private Sub cmdGrabar_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGrabar.ServerClick
        'Dim orConector As New OracleConnection
        'Dim orComdElim As New OracleCommand
        'Dim orComdAct As New OracleCommand

        'Dim orDAElim As New OracleDataAdapter
        'Dim orDAAct As New OracleDataAdapter

        'Dim dsEliminar As New DataSet
        'Dim dsActualiza As New DataSet
        Dim intResultado As Integer
        Dim SelMotivo As DropDownList
        Dim txtOtroMot As TextBox

        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecarga")
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim strIdentifyLog As String = Funciones.CheckStr(Session("strUsuario"))   'strNroPedidoSAP

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Autorizacion.")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")


        'orConector.ConnectionString = "user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"
        'orConector.Open()

        'orComdAct.Connection = orConector
        'Response.Write(DGAutorizaTran.Items.Count)
        For i = 0 To DGAutorizaTran.Items.Count - 1
            SelAutoriza = CType(DGAutorizaTran.Items(i).FindControl("DropDownList1"), DropDownList)
            SelMotivo = CType(DGAutorizaTran.Items(i).FindControl("cboMotivo"), DropDownList)
            txtOtroMot = CType(DGAutorizaTran.Items(i).FindControl("txtOtroMot"), TextBox)
            'orComdAct.Parameters.Add("K_COD_AUTRAN", OracleType.Number)
            'orComdAct.Parameters.Add("K_AUTORIZA", OracleType.Number)
            'orComdAct.Parameters.Add("K_RETVAL", OracleType.Number)
            'orComdAct.Parameters(0).Direction = ParameterDirection.Input
            'orComdAct.Parameters(1).Direction = ParameterDirection.Input
            'orComdAct.Parameters(2).Direction = ParameterDirection.Output

            'orComdAct.Parameters(0).Value = DGAutorizaTran.Items(i).Cells(1).Text
            'orComdAct.Parameters(1).Value = SelAutoriza.SelectedValue

            'orComdAct.CommandText = "CONF_PARAMETROS_CAJA.CONF_AUTORIZA_TRANSACCION"
            'orComdAct.CommandType = CommandType.StoredProcedure
            'intResultado = orComdAct.ExecuteNonQuery

			'CARIAS: Consistencia para el motivo si es que la respuesta es SI
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "SelAutoriza.SelectedValue:" & SelAutoriza.SelectedValue)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "intTran:" & DGAutorizaTran.Items(i).Cells(1).Text)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "intAutoriza:" & SelAutoriza.SelectedValue)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "codmotivo:" & SelMotivo.SelectedValue)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "otroMotivo:" & Funciones.CheckStr(Session("strUsuario")) & " - " & txtOtroMot.Text)

            If SelAutoriza.SelectedValue = "1" Then
                If SelMotivo.SelectedValue = "" Then
					SelAutoriza.SelectedValue = "2"
					objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "1")
                    Response.Write("<script>alert('Debe indicar el motivo si se va a autorizar la operación')</script>")
                Else
					If CDbl(SelMotivo.SelectedValue) = 8 Then
						If Len(Trim(txtOtroMot.Text)) > 0 Then
							objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "2")
							intResultado = objConfig.FP_Autoriza_Transaccion(DGAutorizaTran.Items(i).Cells(1).Text, SelAutoriza.SelectedValue, SelMotivo.SelectedValue, Funciones.CheckStr(Session("strUsuario")) & " - " & txtOtroMot.Text)
						Else
							objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "3")
							SelAutoriza.SelectedValue = "2"
							Response.Write("<script>alert('Debe escribir el motivo si se va a autorizar la operación')</script>")
						End If
					Else
						objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "4")
						intResultado = objConfig.FP_Autoriza_Transaccion(DGAutorizaTran.Items(i).Cells(1).Text, SelAutoriza.SelectedValue, SelMotivo.SelectedValue, Funciones.CheckStr(Session("strUsuario")) & " - " & txtOtroMot.Text)
					End If
                End If
            Else
				intResultado = objConfig.FP_Autoriza_Transaccion(DGAutorizaTran.Items(i).Cells(1).Text, SelAutoriza.SelectedValue, SelMotivo.SelectedValue, Funciones.CheckStr(Session("strUsuario")) & " - " & txtOtroMot.Text)
            End If
            'CARIAS: Fin de cambio

		Next
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Autorizacion.")
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
		Response.Write("<script>alert('Se actualizaron las autorizaciones')</script>")

    End Sub

    Private Sub btnCancelar_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.ServerClick
        Response.Redirect("BusConfTran.aspx")
    End Sub
End Class

