Imports Thycotic.Web.RemoteScripting
Public Class ifrm_Combos
    Inherits RSPage
    'Inherits System.Web.UI.Page
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim objVentas As New SAP_SIC_Ventas.clsVentas
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
            Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
            Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
            If (Session("USUARIO") Is Nothing) Then
                Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
                Response.Redirect(strRutaSite)
                Response.End()
                Exit Sub
            End If
        End If
    End Sub

    <RemoteScriptingMethod(Description:="Combo")> _
        Public Function CargaListaPre(ByVal strCampana As String, ByVal strArticulo As String) As String
        Dim clsConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu           '**** SINEGIA60 ****'
        Dim dsLista As DataSet
        Dim strCombo As String
        Dim cod_LP As String = ""
        Dim Desc_LP As String = ""
        Dim i As Integer

        'dsLista = objVentas.Get_ConsultaUtilizacion(Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000"), strCampana, "02", strArticulo, "")
        dsLista = clsConsultaPvu.ConsultaListaPrecios(System.Configuration.ConfigurationSettings.AppSettings("TPROC_CODIGO"), _
                                                    System.Configuration.ConfigurationSettings.AppSettings("TIPO_VENTA"), _
                                                    System.Configuration.ConfigurationSettings.AppSettings("CANAC_CODIGO"), _
                                                    System.Configuration.ConfigurationSettings.AppSettings("DEPAC_CODIGO"), _
                                                    IIf(strArticulo = "", "000000000000000000", strArticulo), _
                                                    strCampana, _
                                                    System.Configuration.ConfigurationSettings.AppSettings("TOPEN_CODIGO"), _
                                                    System.Configuration.ConfigurationSettings.AppSettings("PLZAC_CODIGO"))

        strCombo = "<select id=cboLPre name=cboLPre class=clsSelectEnable5 onChange=f_CambiaLista()>"
        strCombo = strCombo & "<option value=''></option>"

        If strArticulo = ConfigurationSettings.AppSettings("MATERIAL_RV") Then
            Desc_LP = ConfigurationSettings.AppSettings("Descripcion_LP_PVP")
            cod_LP = ConfigurationSettings.AppSettings("Codigo_LP_PVP")
            strCombo = strCombo & "<option selected value=" & cod_LP & ">" & Desc_LP & "</option>"
        Else
            For i = 0 To dsLista.Tables(0).Rows.Count - 1
                'strCombo = strCombo & "<option value=" & dsLista.Tables(0).Rows(i).Item("ABRVW") & ">" & dsLista.Tables(0).Rows(i).Item("BEZEI") & "</option>"
                strCombo = strCombo & "<option value=" & dsLista.Tables(0).Rows(i).Item("LIPRN_CODIGOLISTAPRECIO") & ">" & dsLista.Tables(0).Rows(i).Item("LIPRV_DESCRIPCION") & "</option>"
            Next
        End If

        strCombo = strCombo & "</select>"
        CargaListaPre = strCombo
    End Function

    <RemoteScriptingMethod(Description:="Combo")> _
        Public Function CargaPlanTarif(ByVal strUtilizacion As String) As String
        Dim clsConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu           '**** SINEGIA60 ****'
        Dim dsLista As DataSet
        Dim strCombo As String
        Dim descrPlan As String = ""
        Dim codPlan As String = ""
        Dim i As Integer

        'dsLista = objVentas.Get_ConsultaPlanTarifario(strUtilizacion, "02")
        dsLista = clsConsultaPvu.ConsultaPlanXTipoVenta("", "", _
                                                                        System.Configuration.ConfigurationSettings.AppSettings("TIPO_VENTA"), _
                                                                        System.Configuration.ConfigurationSettings.AppSettings("PLANC_ESTADO"))

        strCombo = "<select id=cboPlanT name=cboPlanT class=clsSelectEnable5>"
        strCombo = strCombo & "<option value=''></option>"
        For i = 0 To dsLista.Tables(0).Rows.Count - 1
            'strCombo = strCombo & "<option value=" & dsLista.Tables(0).Rows(i).Item("PLAN_TARIFARIO") & ">" & dsLista.Tables(0).Rows(i).Item("DESCRIPCION") & "</option>"
            strCombo = strCombo & "<option value=" & dsLista.Tables(0).Rows(i).Item("PLNV_CODIGO") & ">" & dsLista.Tables(0).Rows(i).Item("PLNV_DESCRIPCION") & "</option>"

            If dsLista.Tables(0).Rows(i).Item("PLNV_CODIGO") = ConfigurationSettings.AppSettings("Codigo_PlanTarifario_NoAplica") Then
                descrPlan = ConfigurationSettings.AppSettings("Descr_PlanTarifario_NoAplica")
                codPlan = ConfigurationSettings.AppSettings("Codigo_PlanTarifario_NoAplica")
            End If
        Next

        If codPlan = "" Then
            descrPlan = ConfigurationSettings.AppSettings("Descr_PlanTarifario_NoAplica")
            codPlan = ConfigurationSettings.AppSettings("Codigo_PlanTarifario_NoAplica")
            strCombo = strCombo & "<option selected value=" & codPlan & ">" & descrPlan & "</option>"
        End If

        strCombo = strCombo & "</select>"

        CargaPlanTarif = strCombo

    End Function

    <RemoteScriptingMethod(Description:="Combo")> _
    Public Function CargaIMEIS(ByVal strMaterial As String, ByVal strNroTelef As String) As String
        Dim dsLista As DataSet
        Dim clsConsultaMssap As New COM_SIC_Activaciones.clsConsultaMsSap       '**** SINEGIA60 ****'
        Dim strCombo As String
        Dim i As Integer
        Dim strOficina As String
        strOficina = Session("ALMACEN")

        'dsLista = objVentas.Get_ConsultaIMEITelf(strOficina, strMaterial, "", strNroTelef)

        Dim objVentasSans As New NEGOCIO_SIC_SANS.SansNegocio
        Dim usuario_id As String = Session("codUsuario")
        If strMaterial <> "" Then
            ''dsLista = objVentasSans.Get_ConsultaIMEITelf(ConsultaPuntoVenta(strOficina), strMaterial, "", strNroTelef, usuario_id)
            dsLista = objVentasSans.Get_ConsultaIMEITelf(ConsultaPuntoVenta(Session("ALMACEN")), strMaterial, "", strNroTelef, usuario_id)
        Else
            dsLista = Nothing
        End If

        strCombo = "<select id=cboIMEIArt name=cboIMEIArt class=clsSelectEnable5 onChange=f_NroTelef()>"
        strCombo = strCombo & "<option value=''></option>"
        For i = 0 To dsLista.Tables(0).Rows.Count - 1
            'strCombo = strCombo & "<option value=" & dsLista.Tables(0).Rows(i).Item("MATNR") & "#" & dsLista.Tables(0).Rows(i).Item("NRO_TELEF") & ">" & dsLista.Tables(0).Rows(i).Item("SERNR") & "</option>"
            strCombo = strCombo & "<option value=" & dsLista.Tables(0).Rows(i).Item("MATEC_CODMATERIAL") & "#" & "000000000" & ">" & dsLista.Tables(0).Rows(i).Item("SERIC_CODSERIE") & "</option>"
        Next
        strCombo = strCombo & "</select>"

        CargaIMEIS = strCombo

    End Function

    Private Function ConsultaPuntoVenta(ByVal P_OVENC_CODIGO As String) As String
        Try
            Dim obj As New COM_SIC_Activaciones.clsConsultaMsSap
            Dim dsReturn As DataSet
            dsReturn = obj.ConsultaPuntoVenta(P_OVENC_CODIGO)
            If dsReturn.Tables(0).Rows.Count > 0 Then
                Return dsReturn.Tables(0).Rows(0).Item("OVENV_CODIGO_SINERGIA")
            End If
            Return Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    <RemoteScriptingMethod(Description:="Combo")> _
        Public Function CargaCentro(ByVal strMotivo As String) As String
        Dim dsLista As DataSet
        Dim strCombo As String
        Dim i As Integer
        Dim strOficina As String
        strOficina = Session("ALMACEN")

        dsLista = objVentas.Get_ConsultaCentroCostos(strMotivo, strOficina, "")
        strCombo = "<select id=cboCentroCosto name=cboCentroCosto class=clsSelectEnable5>"
        strCombo = strCombo & "<option value=''></option>"
        For i = 0 To dsLista.Tables(0).Rows.Count - 1
            strCombo = strCombo & "<option value=" & dsLista.Tables(0).Rows(i).Item("MVGR4") & ">" & dsLista.Tables(0).Rows(i).Item("KTEXT") & "</option>"
        Next
        strCombo = strCombo & "</select>"

        CargaCentro = strCombo

    End Function

    <RemoteScriptingMethod(Description:="Combo")> _
    Public Function ComparaArt(ByVal strArticulo As String, ByVal strGrupo As String) As Boolean
        Dim clsConsultaMssap As New COM_SIC_Activaciones.clsConsultaMsSap       '**** SINEGIA60 ****'
        Dim dsLista As DataSet
        Dim dsArti As DataSet
        Dim strOficina As String
        Dim blnResp As Boolean
        Dim i As Integer
        Dim intGrupo As Integer

        strOficina = Session("ALMACEN")
        dsLista = objVentas.Get_GroupArt
        blnResp = False

        intGrupo = CInt(strGrupo)

        'intGrupo = 1  Chips
        'intGrupo = 2  Packs
        'intGrupo = 3  Servicios

        dsArti = objVentas.Get_ConsultaArticulo(Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000"), strArticulo, "02", strOficina, "")
        '**HARCODE **'
        'dsArti = clsConsultaMssap.ConsultaStock("P019", "", ConfigurationSettings.AppSettings("tipo_oficina"))

        'MATEC_TIPOMATERIAL=MATKL
        For i = 0 To dsLista.Tables(intGrupo - 1).Rows.Count - 1
            If dsLista.Tables(intGrupo - 1).Rows(i).Item("MATKL") = dsArti.Tables(0).Rows(0).Item("MATKL") Then
                'If dsLista.Tables(intGrupo - 1).Rows(i).Item("MATEC_TIPOMATERIAL") = dsArti.Tables(0).Rows(0).Item("MATEC_TIPOMATERIAL") Then
                blnResp = True
            End If
        Next

        ComparaArt = blnResp


    End Function
    <RemoteScriptingMethod(Description:="Combo")> _
    Public Function CargaProv(ByVal strDepartamento As String) As String
        Dim i As Integer
        Dim dsLista As DataSet
        Dim dvFiltro As New DataView
        Dim strCombo As String
        Dim objAct As New COM_SIC_Activaciones.clsConsultaPvu

        'dsLista = objVentas.Get_LeeProvincia()
        dsLista = objAct.CargarProvincia("", strDepartamento, "A")
        'dvFiltro.Table = dsLista.Tables(0)

        If Trim(strDepartamento) = "" Then
            strDepartamento = "0"
        End If
        'dvFiltro.RowFilter = "DEPARTAMENTO = " & strDepartamento

        strCombo = "<select id=cboProv name=cboProv class=clsSelectEnableC onChange=f_CambiaProv()>"
        strCombo = strCombo & "<option value=''></option>"

        For i = 0 To dsLista.Tables(0).Rows.Count - 1
            'strCombo = strCombo & "<option value=" & dvFiltro.Item(i).Item("PROVINCIA") & ">" & dvFiltro.Item(i).Item("DESCRIPCION") & "</option>"
            strCombo = strCombo & "<option value=" & dsLista.Tables(0).Rows(i).Item("PROVC_CODIGO") & ">" & dsLista.Tables(0).Rows(i).Item("PROVV_DESCRIPCION") & "</option>"
        Next

        strCombo = strCombo & "</select>"

        CargaProv = strCombo

    End Function

    <RemoteScriptingMethod(Description:="Combo")> _
    Public Function CargaDist(ByVal strDepartamento As String, ByVal strProvincia As String) As String
        Dim i As Integer
        Dim dsLista As DataSet
        Dim dvFiltro As New DataView
        Dim strCombo As String
        Dim objAct As New COM_SIC_Activaciones.clsConsultaPvu


        dsLista = objAct.CargarDistrito("", strProvincia, strDepartamento, "A")
        'dsLista = objVentas.Get_LeeDistrito()
        'dvFiltro.Table = dsLista.Tables(0)

        If Trim(strDepartamento) = "" Then
            strDepartamento = "0"
        End If
        If Trim(strProvincia) = "" Then
            strProvincia = "0"
        End If

        'dvFiltro.RowFilter = "DEPARTAMENTO = " & strDepartamento & " AND PROVINCIA = " & strProvincia

        strCombo = "<select id=cboDstr name=cboDstr class=clsSelectEnableC>"
        strCombo = strCombo & "<option value=''></option>"

        For i = 0 To dsLista.Tables(0).Rows.Count - 1
            'strCombo = strCombo & "<option value=" & dvFiltro.Item(i).Item("DISTRITO") & ">" & dvFiltro.Item(i).Item("DESCRIPCION") & "</option>"
            strCombo = strCombo & "<option value=" & dsLista.Tables(0).Rows(i).Item("DISTC_CODIGO") & ">" & dsLista.Tables(0).Rows(i).Item("DISTV_DESCRIPCION") & "</option>"
        Next

        strCombo = strCombo & "</select>"
        CargaDist = strCombo

    End Function


End Class
