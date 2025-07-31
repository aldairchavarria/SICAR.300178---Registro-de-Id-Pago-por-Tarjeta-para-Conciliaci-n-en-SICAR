Imports COM_SIC_Activaciones

Public Class datosCliente
    Inherits System.Web.UI.Page
    Dim objVentas As New SAP_SIC_Ventas.clsVentas
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtTipDocumento As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNumDocumento As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTipoCliente As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRSocial As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtAPaterno As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtAMaterno As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboTitulo As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtFecNacimiento As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboSexo As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cboPref As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtTelefono As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFax As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmail As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboECivil As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtCFamiliar As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNConyuge As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboPrefijo As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDireccion As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtReferencia As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboDepa As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cboProv As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cboDstr As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtCodPostal As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPrefijo As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkTelPag As System.Web.UI.WebControls.CheckBox
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
    Protected WithEvents Grabar As System.Web.UI.WebControls.Button
    Protected WithEvents trRazSoc As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trNomApPat As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trApMat As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trTitulo As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trFecNac As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trEstCiv As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trConyuge As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents txtNombre As System.Web.UI.WebControls.TextBox

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
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecarga")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim objAct As COM_SIC_Activaciones.clsConsultaPvu


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio Page_Load")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio de la pàgina Datos del clientes.")


            Dim objPagos As New SAP_SIC_Pagos.clsPagos
            Dim objBDSiscajas As New COM_SIC_Activaciones.clsBDSiscajas '*** SINERGIA 11.03.2015 ***'

            Dim dsTipDoc As DataSet
            Dim dvFiltro As New DataView
            Dim dsDepartamento As DataSet
            Dim dsEstCiv As DataSet
            Dim dsProvincia As DataSet
            Dim dsDistrito As DataSet

            Dim strTipDoc As String
            Dim strNumDoc As String

            objAct = New COM_SIC_Activaciones.clsConsultaPvu

            objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio asignaciòn JS")
            cboPref.Attributes.Add("onchange", "f_Prefijo();")
            btnGrabar.Attributes.Add("onClick", "f_Grabar()")
            cboTitulo.Attributes.Add("onchange", "f_CambiaTitulo()")
            cboDepa.Attributes.Add("onChange", "f_CambiaDepar()")
            cboProv.Attributes.Add("onChange", "f_CambiaProv()")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin asignaciòn JS")

            If Not Page.IsPostBack Then

                If Len(Trim(Session("MenCliente"))) > 0 Then
                    Response.Write("<script>alert('" & Session("MenCliente") & "')</script>")
                    Session("MenCliente") = ""
                End If

                strTipDoc = Trim(Request.Item("strTipDoc")) '"1"
                strNumDoc = Trim(Request.Item("strNumDoc")) '"75856966"

                If strTipDoc = 6 Then
                    trRazSoc.Visible = True
                    trNomApPat.Visible = False
                    trApMat.Visible = False
                    trTitulo.Visible = False
                    trFecNac.Visible = False
                    trEstCiv.Visible = False
                    trConyuge.Visible = False
                Else
                    trRazSoc.Visible = False
                    trNomApPat.Visible = True
                    trApMat.Visible = True
                    trTitulo.Visible = True
                    trFecNac.Visible = True
                    trEstCiv.Visible = True
                    trConyuge.Visible = True
                End If

                'dsTipDoc = objPagos.Get_LeeTipoDocCliente()
                'dsTipDoc = objBDSiscajas.ListaTipoDocumento(strNumDoc)
                Dim listaTipoDoc As New ArrayList
                Dim strDescriocion As String = ""
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Lista los tipos de documento")
                listaTipoDoc = objBDSiscajas.ListaTipoDocumento(strNumDoc)
                For Each item As ItemGenerico In listaTipoDoc
                    If strTipDoc = item.CODIGO Then
                        strTipDoc = item.CODIGO2
                        strDescriocion = item.DESCRIPCION
                        Exit For
                    End If
                Next

                'dvFiltro.Table = dsTipDoc.Tables(0)
                'dvFiltro.RowFilter = "J_1ATODC = '" & strTipDoc & "'"

                txtTipDocumento.Text = strDescriocion ' dvFiltro.Item(0).Item("TEXT30")
                txtNumDocumento.Text = strNumDoc

                '** LISTA DEPARTAMENTOS ***'
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Carga de datos Departamento")
                dsDepartamento = objAct.CargarDepartamento("", "A")
                'dsDepartamento = objVentas.Get_LeeDepartamento()
                If Not dsDepartamento Is Nothing Then
                    '**Prefijos: ##MODIFICAR:
                    '************************************************'
                    'cboPref.DataSource = dsDepartamento.Tables(0)
                    'cboPref.DataValueField = "COD_AREA"
                    'cboPref.DataTextField = "DESCRIPCION"
                    '************************************************'

                    '**Datos departamento:
                    cboDepa.DataSource = dsDepartamento.Tables(0)
                    'cboDepa.DataValueField = "DEPARTAMENTO"
                    'cboDepa.DataTextField = "DESCRIPCION"
                    cboDepa.DataValueField = "DEPAC_CODIGO"         'pvu
                    cboDepa.DataTextField = "DEPAV_DESCRIPCION"     'pvu
                End If

                objFileLog.Log_WriteLog(pathFile, strArchivo, "Carga de datos Estado Civil")
                dsEstCiv = objVentas.Get_LeeEstadoCivil()
                If Not dsEstCiv Is Nothing Then
                    cboECivil.DataSource = dsEstCiv.Tables(0)
                    cboECivil.DataValueField = "FAMST"
                    cboECivil.DataTextField = "FTEXT"
                End If

                objFileLog.Log_WriteLog(pathFile, strArchivo, "Bindea los combos")
                Me.DataBind()   '** bidena datos departamento.


                objFileLog.Log_WriteLog(pathFile, strArchivo, "Asigna los valores en blanco a los combos")
                cboPref.Items.Insert(0, "")
                cboECivil.Items.Insert(0, "")
                cboDepa.Items.Insert(0, "")
            Else

                Dim strDepartamento As String
                Dim strProvincia As String
                Dim strDistrito As String

                Session("Ubigeo_legal") = ""

                strDepartamento = Funciones.CheckStr(cboDepa.SelectedValue)
                strProvincia = Funciones.CheckStr(Request.Item("cboProv"))
                strDistrito = Funciones.CheckStr(Request.Item("cboDstr"))
                Session("Ubigeo_legal") = Trim(strDepartamento & strProvincia & strDistrito)

                '### CARGAR PROVINCIAS ####
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Carga las provincias")
                'dsProvincia = objVentas.Get_LeeProvincia()
                dsProvincia = objAct.CargarProvincia("", strDepartamento, "A")

                If Not dsProvincia Is Nothing Then              '** DG
                    dvFiltro.Table = dsProvincia.Tables(0)
                    If Trim(strDepartamento) = "" Then
                        strDepartamento = "0"
                    End If
                    'dvFiltro.RowFilter = "DEPARTAMENTO = " & strDepartamento
                    'cboProv.DataSource = dvFiltro

                    'cboProv.DataValueField = "PROVINCIA"
                    'cboProv.DataTextField = "DESCRIPCION"
                    cboProv.DataValueField = "PROVC_CODIGO"
                    cboProv.DataTextField = "PROVV_DESCRIPCION"

                    cboProv.DataBind()
                    cboProv.SelectedValue = Request.Item("cboProv")
                End If


                '### CARGAR DISTRITOS ####
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Carga las Distritos")
                'dsDistrito = objVentas.Get_LeeDistrito()
                strProvincia = Me.cboProv.SelectedValue
                dsDistrito = objAct.CargarDistrito("", strProvincia, strDepartamento, "A")
                If Not dsDistrito Is Nothing Then           '** DG   **'
                    'dvFiltro.Table = dsDistrito.Tables(0)
                    If Trim(strDepartamento) = "" Then
                        strDepartamento = "0"
                    End If
                    If Trim(strProvincia) = "" Then
                        strProvincia = "0"
                    End If

                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Filtra departamentos")
                    'dvFiltro.RowFilter = "DEPARTAMENTO = " & strDepartamento & " AND PROVINCIA = " & strProvincia
                    'cboDstr.DataSource = dvFiltro
                    'cboDstr.DataValueField = "DISTRITO"
                    'cboDstr.DataTextField = "DESCRIPCION"
                    cboDstr.DataValueField = "DISTC_CODIGO"
                    cboDstr.DataTextField = "DISTV_DESCRIPCION"
                    cboDstr.DataBind()
                    'cboDstr.SelectedValue = Request.Item("cboDstr")
                End If
            End If
        End If
    End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click

        objFileLog.Log_WriteLog(pathFile, strArchivo, "Incia el mètodo : btnGrabar_Click")
        Dim dsResult As DataSet
        Dim arrCliente(64) As String
        Dim i As Integer
        Dim objPvu As New COM_SIC_Activaciones.clsConsultaPvu               '*** SINERGIA ***'

        'strTrama = ";;;" & txtNombre.Text & ";" & txtAPaterno.Text & ";" & txtAMaterno.Text & ";" & txtRSocial.Text & ";"
        'strTrama &= txtFecNacimiento.Text & ";;" & txtFax.Text & ""

        For i = 0 To UBound(arrCliente)
            arrCliente(i) = ""
        Next

        objFileLog.Log_WriteLog(pathFile, strArchivo, "Incia el mètodo : Asigna datos del Cliente")
        arrCliente(0) = Trim(Request.Item("strNumDoc")) '"75856966" '
        arrCliente(1) = Trim(Request.Item("strTipDoc")) '"1" 
        arrCliente(2) = "02"  'Por default CONSUMER
        arrCliente(3) = txtNombre.Text
        arrCliente(4) = txtAPaterno.Text
        arrCliente(5) = txtAMaterno.Text
        arrCliente(6) = txtRSocial.Text
        arrCliente(7) = txtFecNacimiento.Text 'Right(txtFecNacimiento.Text, 4) & "/" & Mid(txtFecNacimiento.Text, 4, 2) & "/" & Left(txtFecNacimiento.Text, 2)
        arrCliente(9) = txtFax.Text
        arrCliente(10) = txtEmail.Text
        arrCliente(11) = txtNConyuge.Text

        arrCliente(12) = txtCFamiliar.Text
        arrCliente(13) = cboSexo.SelectedValue
        arrCliente(14) = UCase(Trim(cboPrefijo.SelectedValue & " " & txtDireccion.Text))
        arrCliente(15) = Session("Ubigeo_legal") 'Trim(cboDepa.SelectedValue & cboProv.SelectedValue & cboDstr.SelectedValue)

        arrCliente(36) = "0.00"
        arrCliente(37) = "0.00"
        arrCliente(41) = "0.00"

        arrCliente(47) = cboECivil.SelectedValue
        arrCliente(48) = cboTitulo.SelectedValue
        arrCliente(56) = txtPrefijo.Text
        arrCliente(57) = txtTelefono.Text
        arrCliente(62) = txtPrefijo.Text
        arrCliente(63) = txtReferencia.Text
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Incia el mètodo : Fin asigna datos del Cliente")


        'dsResult = objVentas.Set_ActualizaCreaCliente(Session("ALMACEN"), arrCliente)   '**REG
        Dim p_resultado As Integer = 0

        '** MÈTODO PARA ACTUALIZAR Y REGISTRAR CLIENTES **'
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Llama al mètodo : ActualizaDatosCliente")
        Try
            objPvu.ActualizaDatosCliente(Funciones.CheckStr(txtNumDocumento.Text), _
                                          Funciones.CheckStr(Request.Item("strTipDoc")), _
                                          Funciones.CheckStr(txtNombre.Text), _
                                          Funciones.CheckStr(txtAPaterno.Text), _
                                          Funciones.CheckStr(txtAMaterno.Text), _
                                          txtRSocial.Text, _
                                          Funciones.CheckDate(DateTime.Now.ToShortDateString), _
                                          Funciones.CheckStr(txtTelefono.Text), _
                                          Funciones.CheckStr(txtEmail.Text), _
                                          "", _
                                          "", _
                                          "", _
                                          "", _
                                          "", _
                                          "", _
                                          "", _
                                          "", _
                                          Funciones.CheckStr(IIf(arrCliente(14).Length = 0, System.Configuration.ConfigurationSettings.AppSettings("CLIEV_DIRECCION_LEGAL"), Funciones.CheckStr(arrCliente(14)))), _
                                          "", _
                                          IIf(arrCliente(15) = "", "011271236", Funciones.CheckStr(arrCliente(15)).ToString.Trim), _
                                          "", _
                                          "", _
                                          "", _
                                          "", _
                                          "", _
                                          "", _
                                          "", _
                                          "", _
                                          "", _
                                          "", _
                                          "", _
                                          "", _
                                          "", _
                                          Funciones.CheckDate(DateTime.Now.ToShortDateString), _
                                          "", _
                                          "", _
                                          "", _
                                          "", _
                                          "", _
                                          "", _
                                          "", _
                                          "", _
                                          "", _
                                          "", _
                                          Funciones.CheckInt(System.Configuration.ConfigurationSettings.AppSettings("CLIEN_COND_CLIENTE")), _
                                          "", _
                                          "", _
                                          "", _
                                          0, _
                                          0, _
                                          "", _
                                          "", _
                                          "", _
                                          0, _
                                          "", _
                                          "", _
                                          "", _
                                          Funciones.CheckStr(Session("USUARIO")), _
                                          Funciones.CheckStr(Session("USUARIO")), _
                                          Request.Item("strTipDoc"), _
                                          p_resultado)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin mètodo : ActualizaDatosCliente")

            If p_resultado = 0 Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Retorna cero el resultado de inserciòn/actualizaciòn")
                Response.Write("<script language=javascript>alert('El registro/actualizaciòn de datos del cliente no se pudo concretar.')</script>")
                Exit Sub
            End If

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Error al ejecutar el mètodo: ActualizaDatosCliente (sisact_pkg_cons_maestra_sap_6.ssapsu_cliente)")
            Response.Write("<script language=javascript>alert('Error en la ejecucipon del mètodo: ActualizaDatosCliente')</script>")
            Exit Sub
        End Try

        'For i = 0 To dsResult.Tables(1).Rows.Count - 1
        '    If dsResult.Tables(1).Rows(i).Item("TYPE") = "E" Then
        '        Response.Write("<script language=javascript>alert('" & dsResult.Tables(1).Rows(i).Item("MESSAGE") & "')</script>")
        '        Exit Sub
        '    End If
        'Next

        Session("TipDoc") = Trim(Request.Item("strTipDoc"))
        Session("NumDoc") = Trim(Request.Item("strNumDoc"))

        If Session("RecFrec") = "" Then
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Llama a la venta ràpida.")
            Response.Redirect("ventaArticulos.aspx")
        Else
            Session("RecFrec") = ""
            Response.Redirect("RecargaVirt.aspx")
        End If
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin registro del cliente.")
    End Sub

    Private Sub Grabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Grabar.Click
        Session("TipDoc") = Trim(Request.Item("strTipDoc"))
        Session("NumDoc") = Trim(Request.Item("strNumDoc"))
        If Session("RecFrec") = "" Then
            Response.Redirect("ventaArticulos.aspx")
        Else
            Session("RecFrec") = ""
            Response.Redirect("RecargaVirt.aspx")
        End If

    End Sub
End Class
