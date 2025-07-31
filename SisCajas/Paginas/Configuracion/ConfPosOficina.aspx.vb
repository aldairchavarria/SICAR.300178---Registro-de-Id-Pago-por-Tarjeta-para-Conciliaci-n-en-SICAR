Imports System.Data
Imports System.Web

Public Class ConfPosOficina
    Inherits SICAR_WebBase

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents gridDetalle As System.Web.UI.WebControls.DataGrid
    Protected WithEvents chkTodosOf As System.Web.UI.WebControls.CheckBox
    Protected WithEvents lbCodOficina As System.Web.UI.WebControls.ListBox
    Protected WithEvents lbOficina As System.Web.UI.WebControls.ListBox
    Protected WithEvents hidCodOficina As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents cmdBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents btnLimpiar As System.Web.UI.WebControls.Button
    Protected WithEvents loadDataHandler As System.Web.UI.WebControls.Button
    Protected WithEvents loadHidEditar As System.Web.UI.WebControls.Button
    Protected WithEvents hidEditar As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtOculto As System.Web.UI.WebControls.TextBox
    Protected WithEvents hidIntegra As System.Web.UI.HtmlControls.HtmlInputHidden

    Protected WithEvents chkIntegra As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkPago As System.Web.UI.WebControls.CheckBox
    Protected WithEvents btnGrabarIntegraVISA As System.Web.UI.WebControls.Button
    Protected WithEvents btnGrabarIntegraMCD As System.Web.UI.WebControls.Button
    Protected WithEvents btnGrabarPago As System.Web.UI.WebControls.Button

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
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogPOS")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogPOS")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim strUsuario As String
    Dim dsDatosBusqueda As New DataSet
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Write("<script language='javascript' src='../../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            If Not Page.IsPostBack Then
                If Not Request.QueryString("exito") Is Nothing Then
                    If Request.QueryString("exito") = "0" Then
                        Response.Write("<script>alert('Registro Actualizado con exito');</script>")
                    End If

                    If Not Session("dgListaOficina") Is Nothing Then
                        Dim dt As DataTable = DirectCast(Session("dgListaOficina"), DataTable)
                        If Not Session("ConCajTot_codOficina") Is Nothing Then
                            Dim codigoOfi As String = CStr(Session("ConCajTot_codOficina"))
                            Dim dv As New DataView
                            Dim strCodigos As String = ""
                            Dim strOficinas As String = ""
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
                                        strCodigos = strCodigos + Trim(drvResultado("CODIGO")) + ";"
                                        lbOficina.Items.Add(New ListItem(Trim(drvResultado("DESCRIPCION"))))
                                        strOficinas = strOficinas + Trim(drvResultado("DESCRIPCION")) + ";"
                                    End With
                                End If
                            Next

                            hidCodOficina.Value = CStr(Session("ConCajTot_codOficina"))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "loadDataHandler_Click : " & "hidCodOficina : " & hidCodOficina.Value)
                            chkTodosOf.Checked = False
                            cmdBuscar_Click(sender, e)
                        End If
                    Else
                        If Request.QueryString("exito") <> "" Then
                            chkTodosOf.Checked = True
                            cmdBuscar_Click(sender, e)
                    End If
                    End If

                Else
                    Session("dgListaOficina") = Nothing
                End If


            Else
                hidCodOficina.Value = CStr(Session("ConCajTot_codOficina"))
                hidEditar.Value = txtOculto.Text
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Page_Load : " & "hidCodOficina : " & hidCodOficina.Value)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Page_Load : " & "hidEditar : " & hidEditar.Value)
            End If
            End If
    End Sub

    Private Sub cmdBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        strUsuario = Session("USUARIO")
        Dim sMensaje As String = String.Empty
        Try
            Dim strOficinaVta As String = hidCodOficina.Value
            Dim strCodErr As String = String.Empty
            Dim strMsjErr As String = String.Empty
            Dim objTransac As New COM_SIC_Activaciones.clsTransaccionPOS
            Dim dsDatos As New DataSet
            Dim strValorCheck As String = 0
            Dim strCadena As String = String.Empty
            objFileLog.Log_WriteLog(pathFile, strArchivo, " Inicio cmdBuscar_Click")

            If strOficinaVta.Equals(String.Empty) And Not chkTodosOf.Checked Then
                strMsjErr = "Seleccione oficina"
                Response.Write("<script>alert('" & strMsjErr & "');</script>")
                Exit Sub
            End If

            If chkTodosOf.Checked Then
                strOficinaVta = "0"
                strValorCheck = "1"
            Else
                strValorCheck = "0"
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, "cmdBuscar_Click : " & "hidCodOficina : " & hidCodOficina.Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "cmdBuscar_Click : " & "strValorCheck : " & strValorCheck.ToString)

            dsDatos = objTransac.ObtenerOficinasxCajasPOS("", strOficinaVta, "", "", strValorCheck, strCodErr, strMsjErr)

            gridDetalle.DataSource = dsDatos
            gridDetalle.DataBind()

            dsDatosBusqueda = dsDatos

            If dsDatos.Tables(0).Rows.Count > 0 Then

                For i As Integer = 0 To dsDatos.Tables(0).Rows.Count - 1

                    Dim childtableInt As Table = CType(gridDetalle.Controls(0), Table)
                    Dim chkIntegra As CheckBox = CType(childtableInt.Rows(0).FindControl("chkIntegraVISA"), CheckBox)

                    If Convert.ToString(dsDatos.Tables(0).Rows(i).Item("POSC_FLG_SICAR_V")) = "0" Or _
                            Convert.ToString(dsDatos.Tables(0).Rows(i).Item("POSC_FLG_SICAR_V")) = "" Then

                        chkIntegra.Checked = False
                        Exit For
                    ElseIf Convert.ToString(dsDatos.Tables(0).Rows(i).Item("POSC_FLG_SICAR_V")) = "1" Then

                        chkIntegra.Checked = True

                    End If
                Next

                For i As Integer = 0 To dsDatos.Tables(0).Rows.Count - 1

                    Dim childtableInt As Table = CType(gridDetalle.Controls(0), Table)
                    Dim chkIntegra As CheckBox = CType(childtableInt.Rows(0).FindControl("chkIntegraMCD"), CheckBox)

                    If Convert.ToString(dsDatos.Tables(0).Rows(i).Item("POSC_FLG_SICAR_M")) = "0" Or _
                            Convert.ToString(dsDatos.Tables(0).Rows(i).Item("POSC_FLG_SICAR_M")) = "" Then

                        chkIntegra.Checked = False
                        Exit For
                    ElseIf Convert.ToString(dsDatos.Tables(0).Rows(i).Item("POSC_FLG_SICAR_M")) = "1" Then

                        chkIntegra.Checked = True

                    End If
                Next

                For i As Integer = 0 To dsDatos.Tables(0).Rows.Count - 1

                    Dim childtablePago As Table = CType(gridDetalle.Controls(0), Table)
                    Dim chkPagoAut As CheckBox = CType(childtablePago.Rows(0).FindControl("chkPago"), CheckBox)

                    If Convert.ToString(dsDatos.Tables(0).Rows(i).Item("POSC_FLG_AUTOM")) = "0" Or _
                            Convert.ToString(dsDatos.Tables(0).Rows(i).Item("POSC_FLG_AUTOM")) = "" Then

                        chkPagoAut.Checked = False
                        Exit For

                    ElseIf Convert.ToString(dsDatos.Tables(0).Rows(i).Item("POSC_FLG_AUTOM")) = "1" Then

                        chkPagoAut.Checked = True

                    End If
                Next
            Else

                Dim childtablePago As Table = CType(gridDetalle.Controls(0), Table)
                Dim chkPagoAut As CheckBox = CType(childtablePago.Rows(0).FindControl("chkPago"), CheckBox)

                Dim childtableInt As Table = CType(gridDetalle.Controls(0), Table)
                Dim chkIntegra As CheckBox = CType(childtableInt.Rows(0).FindControl("chkIntegra"), CheckBox)

                chkPagoAut.Checked = False
                chkIntegra.Checked = False



            End If


            If chkTodosOf.Checked Then
                Me.lbCodOficina.Items.Clear()
                Me.lbOficina.Items.Clear()
            End If



        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "error cmdBuscar_Click: " & ex.Message)
        Finally

            sMensaje = "ObtenerOficinasxCajasPOS - Consultar"

            RegistrarAuditoria(sMensaje, ConfigurationSettings.AppSettings("ConsMantCajasPOS_codTrsAuditoria"))

            objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin cmdBuscar_Click")
        End Try
    End Sub

    Private Sub loadDataHandler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles loadDataHandler.Click
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, "   Inicio IND - loadDataHandler_Click ")
            If Not Session("dgListaOficina") Is Nothing Then
                Dim dt As DataTable = DirectCast(Session("dgListaOficina"), DataTable)
                If Not Session("ConCajTot_codOficina") Is Nothing Then
                    Dim codigoOfi As String = CStr(Session("ConCajTot_codOficina"))
                    Dim dv As New DataView
                    Dim strCodigos As String = ""
                    Dim strOficinas As String = ""
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
                                strCodigos = strCodigos + Trim(drvResultado("CODIGO")) + ";"
                                lbOficina.Items.Add(New ListItem(Trim(drvResultado("DESCRIPCION"))))
                                strOficinas = strOficinas + Trim(drvResultado("DESCRIPCION")) + ";"
                            End With
                        End If
                    Next


                    hidCodOficina.Value = CStr(Session("ConCajTot_codOficina"))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "loadDataHandler_Click : " & "hidCodOficina : " & hidCodOficina.Value)
                    chkTodosOf.Checked = False
                End If
            End If
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "error loadDataHandler_Click: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin loadDataHandler_Click")
        End Try
    End Sub

    Private Sub loadHidEditar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles loadHidEditar.Click
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, "   Inicio IND - loadHidEditar_Click ")

            Dim COD_PDV As String = String.Empty
            Dim BEZEI As String = String.Empty
            Dim POSV_NROTIENDA As String = String.Empty
            Dim POSV_NROCAJA As String = String.Empty
            Dim POSV_IDESTABLEC As String = String.Empty
            Dim POSV_IPCAJA As String = String.Empty
            Dim POSV_EQUIPO As String = String.Empty
            Dim POSC_ESTADO As String = String.Empty
            Dim POSC_FLG_SICAR_V As String = String.Empty
            Dim POSC_FLG_SICAR_M As String = String.Empty
            Dim POSC_FLG_AUTOM As String = String.Empty
            Dim FLAG_TIPO_TARJ As String = String.Empty

            Dim strTrama As String = hidEditar.Value
            Dim arreglo() As String = strTrama.ToString().Split("&")

            COD_PDV = Funciones.CheckStr(arreglo(0).Substring(arreglo(0).IndexOf("=") + 1))
            BEZEI = Funciones.CheckStr(arreglo(1).Substring(arreglo(1).IndexOf("=") + 1))
            POSV_NROTIENDA = Funciones.CheckStr(arreglo(2).Substring(arreglo(2).IndexOf("=") + 1))
            POSV_NROCAJA = Funciones.CheckStr(arreglo(3).Substring(arreglo(3).IndexOf("=") + 1))
            POSV_IDESTABLEC = Funciones.CheckStr(arreglo(4).Substring(arreglo(4).IndexOf("=") + 1))
            POSV_IPCAJA = Funciones.CheckStr(arreglo(5).Substring(arreglo(5).IndexOf("=") + 1))
            POSV_EQUIPO = Funciones.CheckStr(arreglo(6).Substring(arreglo(6).IndexOf("=") + 1))
            POSC_ESTADO = Funciones.CheckStr(arreglo(7).Substring(arreglo(7).IndexOf("=") + 1))
            POSC_FLG_SICAR_V = Funciones.CheckStr(arreglo(8).Substring(arreglo(8).IndexOf("=") + 1))
            POSC_FLG_SICAR_M = Funciones.CheckStr(arreglo(8).Substring(arreglo(9).IndexOf("=") + 1))
            POSC_FLG_AUTOM = Funciones.CheckStr(arreglo(9).Substring(arreglo(10).IndexOf("=") + 1))
            FLAG_TIPO_TARJ = Funciones.CheckStr(arreglo(10).Substring(arreglo(11).IndexOf("=") + 1))

            objFileLog.Log_WriteLog(pathFile, strArchivo, "cmdBuscar_Click : " & "COD_PDV : " & COD_PDV)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "cmdBuscar_Click : " & "BEZEI : " & BEZEI)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "cmdBuscar_Click : " & "POSV_NROTIENDA : " & POSV_NROTIENDA)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "cmdBuscar_Click : " & "POSV_NROCAJA : " & POSV_NROCAJA)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "cmdBuscar_Click : " & "POSV_IDESTABLEC : " & POSV_IDESTABLEC)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "cmdBuscar_Click : " & "POSV_IPCAJA : " & POSV_IPCAJA)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "cmdBuscar_Click : " & "POSV_EQUIPO : " & POSV_EQUIPO)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "cmdBuscar_Click : " & "POSC_ESTADO : " & POSC_ESTADO)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "cmdBuscar_Click : " & "POSC_FLG_SICAR : " & POSC_FLG_SICAR_V)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "cmdBuscar_Click : " & "POSC_FLG_SICAR : " & POSC_FLG_SICAR_M)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "cmdBuscar_Click : " & "POSC_FLG_AUTOM : " & POSC_FLG_AUTOM)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "cmdBuscar_Click : " & "FLAG_TIPO_TARJ : " & FLAG_TIPO_TARJ)


            objFileLog.Log_WriteLog(pathFile, strArchivo, "cmdBuscar_Click : " & "hidCodOficina : " & hidCodOficina.Value)


            Response.Redirect("ConfPosRegistrar.aspx?COD_PDV=" & COD_PDV & "&BEZEI=" & BEZEI & "&POSV_NROTIENDA=" & POSV_NROTIENDA & "&POSV_NROCAJA=" & POSV_NROCAJA & "&POSV_IDESTABLEC=" & POSV_IDESTABLEC & "&POSV_IPCAJA=" & POSV_IPCAJA & "&POSV_EQUIPO=" & POSV_EQUIPO & "&POSC_ESTADO=" & POSC_ESTADO & "&POSC_FLG_SICAR_V=" & POSC_FLG_SICAR_V & "&POSC_FLG_SICAR_M=" & POSC_FLG_SICAR_M & "&POSC_FLG_AUTOM=" & POSC_FLG_AUTOM & "&FLAG_TIPO_TARJ=" & FLAG_TIPO_TARJ, False)


        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "error loadHidEditar_Click: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin loadHidEditar_Click")
        End Try

    End Sub

    Private Sub btnLimpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click
        gridDetalle.DataSource = Nothing
        gridDetalle.DataBind()

        Me.lbCodOficina.Items.Clear()
        Me.lbOficina.Items.Clear()
        Me.chkTodosOf.Checked = False
        Session("ConCajTot_codOficina") = ""

    End Sub

    Private Sub RegistrarAuditoria(ByVal DesTrx As String, ByVal CodTrx As String)
        Try
            Dim user As String = Me.CurrentUser
            Dim ipHost As String = CurrentTerminal
            Dim nameHost As String = System.Net.Dns.GetHostName
            Dim nameServer As String = System.Net.Dns.GetHostName
            Dim ipServer As String = System.Net.Dns.GetHostByName(nameServer).AddressList(0).ToString
            Dim hostInfo As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(nameHost)

            Dim CadMensaje As String
            Dim CodServicio As String = ConfigurationSettings.AppSettings("CONS_COD_SICAR_SERV")
            Dim oAuditoria As New COM_SIC_Activaciones.clsAuditoriaWS

            oAuditoria.RegistrarAuditoria(CodTrx, _
                                            CodServicio, _
                                            ipHost, _
                                            nameHost, _
                                            ipServer, _
                                            nameServer, _
                                            user, _
                                            "", _
                                            "0", _
                                            DesTrx)

        Catch ex As Exception

        End Try

    End Sub

    Public Sub sellectAll(ByVal sender As Object, ByVal e As EventArgs)
        If chkIntegra.Checked Then
            Response.Write("<script language=jscript> deshabilitarGrilla(6); </script>")
        Else
            Response.Write("<script language=jscript> habilitarGrilla(6); </script>")
        End If
    End Sub

    Private Sub btnGrabarIntegraVISA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabarIntegraVISA.Click
        Try

            Dim strOficinaVta As String = hidCodOficina.Value
            Dim objTransac As New COM_SIC_Activaciones.clsTransaccionPOS
            Dim valor As String = String.Empty
            Dim strCodErr As String = String.Empty
            Dim strMsjErr As String = String.Empty
            Dim strMensaje As String = String.Empty

            Dim childtable As Table = CType(gridDetalle.Controls(0), Table)
            Dim chkIntegraVISA As CheckBox = CType(childtable.Rows(0).FindControl("chkIntegraVISA"), CheckBox)

            If gridDetalle.Items.Count <= 0 Then

                If chkIntegra.Checked Then
                    chkIntegra.Checked = False
                Else
                    chkIntegra.Checked = True
                End If

                strMensaje = "No se encontraron datos"
                Response.Write("<script language=jscript> alert('" + strMensaje + "'); </script>")
                Return
            End If

            If chkIntegraVISA.Checked Then

                Dim dtgItem As DataGridItem
                Dim Check As CheckBox

                For Each dtgItem In gridDetalle.Items
                    Check = CType(dtgItem.Cells(6).Controls(1), CheckBox)

                    Check.Checked = True
                Next

                valor = "1"
            Else

                Dim dtgItem As DataGridItem
                Dim Check As CheckBox

                For Each dtgItem In gridDetalle.Items
                    Check = CType(dtgItem.Cells(6).Controls(1), CheckBox)

                    Check.Checked = False
                Next

                valor = "0"
            End If

            objTransac.RegistrarOficinasxCajasPOS("", "V", Session("USUARIO"), "", "", "", _
                                                      strOficinaVta, "", "", "", _
                                                      "", valor, "", "1", "1", strCodErr, strMsjErr)

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "error btnGrabarIntegra_Click: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin btnGrabarIntegra_Click")
        End Try

    End Sub
    Private Sub btnGrabarIntegraMCD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabarIntegraMCD.Click
        Try

            Dim strOficinaVta As String = hidCodOficina.Value
            Dim objTransac As New COM_SIC_Activaciones.clsTransaccionPOS
            Dim valor As String = String.Empty
            Dim strCodErr As String = String.Empty
            Dim strMsjErr As String = String.Empty
            Dim strMensaje As String = String.Empty

            Dim childtable As Table = CType(gridDetalle.Controls(0), Table)
            Dim chkIntegraMCD As CheckBox = CType(childtable.Rows(0).FindControl("chkIntegraMCD"), CheckBox)

            If gridDetalle.Items.Count <= 0 Then

                If chkIntegra.Checked Then
                    chkIntegra.Checked = False
                Else
                    chkIntegra.Checked = True
                End If

                strMensaje = "No se encontraron datos"
                Response.Write("<script language=jscript> alert('" + strMensaje + "'); </script>")
                Return
            End If

            If chkIntegraMCD.Checked Then

                Dim dtgItem As DataGridItem
                Dim Check As CheckBox

                For Each dtgItem In gridDetalle.Items
                    Check = CType(dtgItem.Cells(7).Controls(1), CheckBox)

                    Check.Checked = True
                Next

                valor = "1"
            Else

                Dim dtgItem As DataGridItem
                Dim Check As CheckBox

                For Each dtgItem In gridDetalle.Items
                    Check = CType(dtgItem.Cells(7).Controls(1), CheckBox)

                    Check.Checked = False
                Next

                valor = "0"
            End If

            objTransac.RegistrarOficinasxCajasPOS("", "M", Session("USUARIO"), "", "", "", _
                                                      strOficinaVta, "", "", "", _
                                                      "", valor, "", "1", "1", strCodErr, strMsjErr)

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "error btnGrabarIntegra_Click: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin btnGrabarIntegra_Click")
        End Try

    End Sub
    Private Sub btnGrabarPago_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabarPago.Click
        Try

            Dim strOficinaVta As String = hidCodOficina.Value
            Dim objTransac As New COM_SIC_Activaciones.clsTransaccionPOS
            Dim valor As String = String.Empty
            Dim strCodErr As String = String.Empty
            Dim strMsjErr As String = String.Empty

            Dim strMensaje As String = String.Empty

            Dim childtable As Table = CType(gridDetalle.Controls(0), Table)
            Dim chkPagox As CheckBox = CType(childtable.Rows(0).FindControl("chkPago"), CheckBox)

            If chkPagox.Checked Then
                Dim dtgItem As DataGridItem
                Dim Check As CheckBox

                For Each dtgItem In gridDetalle.Items
                    Check = CType(dtgItem.Cells(8).Controls(1), CheckBox)

                    Check.Checked = True
                Next
                valor = "1"
            Else
                Dim dtgItem As DataGridItem
                Dim Check As CheckBox

                For Each dtgItem In gridDetalle.Items
                    Check = CType(dtgItem.Cells(8).Controls(1), CheckBox)

                    Check.Checked = False
                Next
                valor = "0"
            End If

            If gridDetalle.Items.Count <= 0 Then
                If chkPagox.Checked Then
                    chkPagox.Checked = False
                Else
                    chkPagox.Checked = True
                End If
                strMensaje = "No se encontraron datos"
                Response.Write("<script language=jscript> alert('" + strMensaje + "'); </script>")
                Return
            End If

            objTransac.RegistrarOficinasxCajasPOS("", "", Session("USUARIO"), "", "", "", _
                                                      strOficinaVta, "", "", "", _
                                                      "", "", valor, "1", "0", strCodErr, strMsjErr)

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "error btnGrabarIntegra_Click: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin btnGrabarIntegra_Click")
        End Try
    End Sub
End Class
