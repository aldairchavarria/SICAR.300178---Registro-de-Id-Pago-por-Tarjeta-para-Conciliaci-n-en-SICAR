Public Class WebForm1
    Inherits System.Web.UI.Page


#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

#End Region

    Private ImagenIzq, lRsPerfil, wObjPerf, objMenu, RutaCompleta, FlagHijos, nAux
    Private strMenu As String
    Private sqlConsulta4, sqlConsulta3, sqlConsulta2, sqlConsulta1 As String
    Private rsConsulta4, rsConsulta3, rsConsulta2, rsConsulta1
    Private cmConsulta3, cmConsulta2
    Private cmConsulta4, cmConsulta1

    Private cont, cont2, cont3, cont4 As Integer
    Private ContadorNivel1, ContadorNivel2, ContadorNivel3, ContadorNivel4 As Integer

    Private codAplicacion As Integer

    Private ArrOpcionN1(100), ArrOpcionN2(200), ArrOpcionN3(200), ArrOpcionN4(200) As String
    Private ArrHijosN1(100), ArrHijosN2(200), ArrHijosN3(200), ArrHijosN4(200) As String
    Private strRutaSite As String = "http://tim-it2desar/SistemaCajas"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Introducir aquí el código de usuario para inicializar la página
        'Response.Write("<script language='javascript' src='/librerias/Lib_FuncValidacion.js'></script>")
        'Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        'If (Session("USUARIO") Is Nothing) Then
        '    Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
        '    Response.Redirect(strRutaSite)
        '    Response.End()
        '    Exit Sub
        'Else
        '    Dim strcad As String
        '    Session("codUsuario") = CInt(Page.Request.QueryString.Get("codusuario")) '762 '
        '    codAplicacion = CInt(Page.Request.QueryString.Get("codAplicacion")) '102
        '    Session("flag_menu_usuario") = CInt(Request.Form.Get("flag_menu_usuario"))
        '    Session("menu_usuario") = Request.Form.Get("menu_usuario")

        '    cont = 1
        '    cont2 = 1
        '    cont3 = 1
        '    cont4 = 1

        '    ContadorNivel1 = 0
        '    ContadorNivel2 = 0
        '    ContadorNivel3 = 0
        '    ContadorNivel4 = 0
        '    ImagenIzq = "<img src='" & strRutaSite & "/images/mnu_cuadrado.gif' border=0> "
        '    Page.Response.Write("<HTML>" & Chr(13) & Chr(10) & Chr(13) & Chr(10))
        '    Page.Response.Write("<HEAD>" & Chr(13) & Chr(10))
        '    Page.Response.Write("<TITLE>Aplicativo TIM</TITLE>" & Chr(13) & Chr(10))
        '    Page.Response.Write("<meta http-equiv='Pragma' content='no-cache'>" & Chr(13) & Chr(10))
        '    Page.Response.Write("<meta HTTP-EQUIV='Expires' CONTENT='Mon, 06 Jan 1990 00:00:01 GMT'>" & Chr(13) & Chr(10))
        '    Page.Response.Write("<link href='../../estilos/est_General.css' rel='styleSheet' type='text/css'>" & Chr(13) & Chr(10))
        '    Page.Response.Write("</HEAD>" & Chr(13) & Chr(10))

        '    Page.Response.Write("<body topmargin='0' leftmargin='0' marginwidth='0' marginheight='0'>" & Chr(13) & Chr(10))
        '    Page.Response.Write("<table height='100%' width='100%'><tbody>" & Chr(13) & Chr(10))
        '    Page.Response.Write("<tr>" & Chr(13) & Chr(10))
        '    Page.Response.Write("<td width=170 valign=top>" & Chr(13) & Chr(10))

        '    Page.Response.Write("<link rel='stylesheet' href='" & strRutaSite & "/Estilos/estilo.css' type='text/css'>" & Chr(13) & Chr(10) & Chr(13) & Chr(10))

        '    strcad = "<script language='JavaScript1.2'>" & Chr(13) & Chr(10)
        '    strcad = strcad & "if(window.event + '' == 'undefined') event = null;" & Chr(13) & Chr(10)
        '    strcad = strcad & "function HM_f_PopUp(){return false};" & Chr(13) & Chr(10)

        '    strcad = strcad & "function solicitprep(){" & Chr(13) & Chr(10)
        '    strcad = strcad & "window.open('" & strRutaSite & "/paginas/ventas/prepago/solicitud/SolicitudPrepago.asp','Solicitud','status=no,resizable=no,toolbar=no,scrollbars=yes,modal=yes,left=100,top=100,width=850,height=300');" & Chr(13) & Chr(10)
        '    strcad = strcad & "}" & Chr(13) & Chr(10)

        '    strcad = strcad & "function HM_f_PopDown(){return false};" & Chr(13) & Chr(10)
        '    strcad = strcad & "popUp = HM_f_PopUp;" & Chr(13) & Chr(10)
        '    strcad = strcad & "popDown = HM_f_PopDown;" & Chr(13) & Chr(10) & Chr(13) & Chr(10)

        '    strcad = strcad & "HM_PG_MenuWidth = 170;" & Chr(13) & Chr(10)
        '    strcad = strcad & "HM_PG_FontFamily = 'Verdana';" & Chr(13) & Chr(10)
        '    strcad = strcad & "HM_PG_FontSize = 7.5;" & Chr(13) & Chr(10)
        '    strcad = strcad & "HM_PG_FontBold = 1;" & Chr(13) & Chr(10)
        '    strcad = strcad & "HM_PG_FontItalic = 0;" & Chr(13) & Chr(10)
        '    strcad = strcad & "HM_PG_FontColor = 'white';" & Chr(13) & Chr(10)
        '    strcad = strcad & "HM_PG_FontColorOver = 'white';" & Chr(13) & Chr(10)
        '    strcad = strcad & "HM_PG_ItemPadding = 3;" & Chr(13) & Chr(10) & Chr(13) & Chr(10)

        '    strcad = strcad & "HM_PG_BorderWidth = 1;" & Chr(13) & Chr(10)
        '    strcad = strcad & "HM_PG_BorderColor = 'black';" & Chr(13) & Chr(10)
        '    strcad = strcad & "HM_PG_BorderStyle = 'solid';" & Chr(13) & Chr(10)
        '    strcad = strcad & "HM_PG_SeparatorSize = 1;" & Chr(13) & Chr(10)
        '    strcad = strcad & "HM_PG_ImageSrc = '" & strRutaSite & "/images/mnu_flecha.gif';" & Chr(13) & Chr(10)
        '    strcad = strcad & "HM_PG_ImageSrcLeft = '" & strRutaSite & "/include/mnu_flecha.gif';" & Chr(13) & Chr(10) & Chr(13) & Chr(10)

        '    strcad = strcad & "HM_PG_ImageSize = 7;" & Chr(13) & Chr(10)
        '    strcad = strcad & "HM_PG_ImageHorizSpace = 0;" & Chr(13) & Chr(10)
        '    strcad = strcad & "HM_PG_ImageVertSpace = 3;" & Chr(13) & Chr(10) & Chr(13) & Chr(10)

        '    strcad = strcad & "HM_PG_KeepHilite = true; " & Chr(13) & Chr(10)
        '    strcad = strcad & "HM_PG_ClickStart = 0;" & Chr(13) & Chr(10)
        '    strcad = strcad & "HM_PG_ClickKill = false;" & Chr(13) & Chr(10)
        '    strcad = strcad & "HM_PG_ChildOverlap = 1; //adentro" & Chr(13) & Chr(10)
        '    strcad = strcad & "HM_PG_ChildOffset = 0; //arriba" & Chr(13) & Chr(10)
        '    strcad = strcad & "HM_PG_ChildPerCentOver = null;" & Chr(13) & Chr(10)
        '    strcad = strcad & "HM_PG_TopSecondsVisible = .3;" & Chr(13) & Chr(10)
        '    strcad = strcad & "HM_PG_StatusDisplayBuild =0;" & Chr(13) & Chr(10)
        '    strcad = strcad & "HM_PG_StatusDisplayLink = 0;" & Chr(13) & Chr(10)
        '    strcad = strcad & "HM_PG_UponDisplay = null;" & Chr(13) & Chr(10)
        '    strcad = strcad & "HM_PG_UponHide = null;" & Chr(13) & Chr(10)
        '    strcad = strcad & "HM_PG_RightToLeft = false;" & Chr(13) & Chr(10) & Chr(13) & Chr(10)

        '    strcad = strcad & "HM_PG_ShowLinkCursor = 1;" & Chr(13) & Chr(10)
        '    strcad = strcad & "HM_PG_NSFontOver = true;" & Chr(13) & Chr(10) & Chr(13) & Chr(10)

        '    strcad = strcad & "var iWidth= 180;" & Chr(13) & Chr(10)
        '    strcad = strcad & "var iTopPos= 0;" & Chr(13) & Chr(10)
        '    strcad = strcad & "var iLeftPos= 0;" & Chr(13) & Chr(10)
        '    strcad = strcad & "var sFontColor= '#FFFFFF';" & Chr(13) & Chr(10)
        '    strcad = strcad & "var sFontHighColor= '#FFFFFF';" & Chr(13) & Chr(10)
        '    strcad = strcad & "var sBackgroundColor= '#FF0000';" & Chr(13) & Chr(10)
        '    strcad = strcad & "var sBackgroundHighColor= '#00007b';" & Chr(13) & Chr(10)
        '    strcad = strcad & "var sTableBorderColor= '#EFEFEF';" & Chr(13) & Chr(10)
        '    strcad = strcad & "var sLineColor= '#FF9369';" & Chr(13) & Chr(10) & Chr(13) & Chr(10)

        '    strcad = strcad & "var intRows= 13;" & Chr(13) & Chr(10)
        '    strcad = strcad & "var intHeight= 23.9;" & Chr(13) & Chr(10)
        '    strcad = strcad & "var iBlankspace= 14;" & Chr(13) & Chr(10)
        '    strcad = strcad & "//Fin de Variables" & Chr(13) & Chr(10) & Chr(13) & Chr(10)

        '    strcad = strcad & "HM_DOM = (document.getElementById) ? true : false;" & Chr(13) & Chr(10)
        '    strcad = strcad & "HM_NS4 = (document.layers) ? true : false;" & Chr(13) & Chr(10)
        '    strcad = strcad & "HM_IE = (document.all) ? true : false;" & Chr(13) & Chr(10)
        '    strcad = strcad & "HM_IE4 = HM_IE && !HM_DOM;" & Chr(13) & Chr(10)
        '    strcad = strcad & "HM_Mac = (navigator.appVersion.indexOf('Mac') != -1);" & Chr(13) & Chr(10)
        '    strcad = strcad & "HM_IE4M = HM_IE4 && HM_Mac;" & Chr(13) & Chr(10)
        '    strcad = strcad & "HM_IsMenu = (HM_DOM || HM_NS4 || (HM_IE4 && !HM_IE4M));" & Chr(13) & Chr(10) & Chr(13) & Chr(10)

        '    strcad = strcad & "HM_BrowserString = HM_NS4 ? 'NS4' : HM_DOM ? 'DOM' : 'IE4';" & Chr(13) & Chr(10) & Chr(13) & Chr(10)

        '    strcad = strcad & "if(window.event + '' == 'undefined') event = null;" & Chr(13) & Chr(10)
        '    strcad = strcad & "function HM_f_PopUp(){return false};" & Chr(13) & Chr(10)
        '    strcad = strcad & "function HM_f_PopDown(){return false};" & Chr(13) & Chr(10)
        '    strcad = strcad & "popUp = HM_f_PopUp;" & Chr(13) & Chr(10)
        '    strcad = strcad & "popDown = HM_f_PopDown;" & Chr(13) & Chr(10) & Chr(13) & Chr(10)
        '    strcad = strcad & "if(HM_IsMenu) {" & Chr(13) & Chr(10)
        '    strcad = strcad & "	//Inicio de Opciones" & Chr(13) & Chr(10)
        '    strcad = strcad & "	HM_Array1= [ [iWidth,  iLeftPos, iTopPos, sFontColor, sFontHighColor, sBackgroundColor, sBackgroundHighColor, sTableBorderColor, sLineColor, 1,	0, 0, 1, 1, 1, 'null', 'null'," & Chr(13) & Chr(10)
        '    strcad = strcad & "	]," & Chr(13) & Chr(10)


        '    'Inicio Nivel 1******************************************************************

        '    strMenu = ""

        '    'If Session("flag_menu_usuario") Then
        '    'strMenu = Session("menu_usuario")
        '    'Else

        '    rsConsulta1 = Server.CreateObject("ADODB.Recordset")
        '    objMenu = Server.CreateObject("Segu_DB.Acceso")
        '    rsConsulta1 = objMenu.GetOpcionesMenu(Session("codUsuario"), codAplicacion, 2, 0)

        '    objMenu = Nothing
        '    'Dim oReader As SqlClient.SqlDataReader
        '    'oReader = Nothing
        '    While Not rsConsulta1.EOF
        '        nAux = Replace(rsConsulta1.Fields("OPCIC_NOMPAG").value & " ", "\", "/")
        '        If Trim(nAux & " ") <> "" Then
        '            If InStr(1, Trim(nAux), "?") = 0 Then
        '                RutaCompleta = strRutaSite & Trim(nAux) & "?cu=" & Session("codUsuario") & "&co=" & rsConsulta1.Fields("OPCIC_COD").value & "&ca=" & codAplicacion & "&cp=" & Session("codPerfil")
        '            Else
        '                RutaCompleta = strRutaSite & Trim(nAux) & "&cu=" & Session("codUsuario") & "&co=" & rsConsulta1.Fields("OPCIC_COD").value & "&ca=" & codAplicacion & "&cp=" & Session("codPerfil")
        '            End If
        '            FlagHijos = "0"
        '        Else
        '            RutaCompleta = ""
        '            FlagHijos = "1"
        '        End If

        '        strMenu = strMenu & "[""" & rsConsulta1.Fields("OPCIC_DES").value & """,""" & RutaCompleta & """,1,0," & FlagHijos & "]" & vbCrLf

        '        ContadorNivel1 = ContadorNivel1 + 1
        '        ArrOpcionN1(ContadorNivel1) = rsConsulta1.Fields("OPCIC_COD").value
        '        ArrHijosN1(ContadorNivel1) = FlagHijos

        '        rsConsulta1.MoveNext()
        '        If Not rsConsulta1.EOF Then
        '            strMenu = strMenu & ","
        '        End If
        '    End While

        '    cmConsulta1 = Nothing
        '    rsConsulta1.Close()
        '    rsConsulta1 = Nothing

        '    strMenu = strMenu & "]" & vbCrLf
        '    'Inicio Nivel 2******************************************************************

        '    For cont = 1 To ContadorNivel1

        '        ContadorNivel2 = 0
        '        ReDim ArrOpcionN2(200)
        '        ReDim ArrHijosN2(200)

        '        rsConsulta2 = Server.CreateObject("ADODB.Recordset")
        '        objMenu = Server.CreateObject("Segu_DB.Acceso")
        '        rsConsulta2 = objMenu.GetOpcionesMenu(Session("codUsuario"), codAplicacion, 3, ArrOpcionN1(cont))
        '        objMenu = Nothing

        '        If ArrHijosN1(cont) = "1" Then

        '            strMenu = strMenu & "HM_Array1_" & cont & " = [" & vbCrLf
        '            strMenu = strMenu & vbTab & "[]," & vbCrLf

        '            While Not rsConsulta2.EOF
        '                nAux = Replace(rsConsulta2.Fields("OPCIC_NOMPAG").value & " ", "\", "/")
        '                Dim tamcad
        '                tamcad = Len(nAux)
        '                If tamcad >= 10 Then
        '                    tamcad = 10
        '                End If
        '                If Trim(nAux & " ") <> "" Then
        '                    If LCase(Mid(nAux, 1, tamcad)) = "javascript" Then
        '                        RutaCompleta = nAux
        '                    Else
        '                        If InStr(1, Trim(nAux), "?") = 0 Then
        '                            RutaCompleta = strRutaSite & Trim(nAux) & "?cu=" & Session("codUsuario") & "&co=" & rsConsulta2.Fields("OPCIC_COD").value & "&ca=" & codAplicacion & "&cp=" & Session("codPerfil")
        '                        Else
        '                            RutaCompleta = strRutaSite & Trim(nAux) & "&cu=" & Session("codUsuario") & "&co=" & rsConsulta2.Fields("OPCIC_COD").value & "&ca=" & codAplicacion & "&cp=" & Session("codPerfil")
        '                        End If
        '                    End If
        '                    FlagHijos = "0"
        '                Else
        '                    RutaCompleta = ""
        '                    FlagHijos = "1"
        '                End If

        '                strMenu = strMenu & vbCrLf
        '                strMenu = strMenu & "[""&nbsp;" & rsConsulta2.Fields("OPCIC_DES").value & """,""" & RutaCompleta & """,1,0," & FlagHijos & "]" & vbCrLf
        '                strMenu = strMenu & vbCrLf

        '                ContadorNivel2 = ContadorNivel2 + 1
        '                ArrOpcionN2(ContadorNivel2) = rsConsulta2.Fields("OPCIC_COD").value
        '                ArrHijosN2(ContadorNivel2) = FlagHijos

        '                rsConsulta2.MoveNext()
        '                If Not rsConsulta2.EOF Then

        '                    strMenu = strMenu & " , " & vbCrLf

        '                End If
        '            End While

        '            strMenu = strMenu & "]" & vbCrLf
        '        End If

        '        cmConsulta2 = Nothing
        '        rsConsulta2.Close()
        '        rsConsulta2 = Nothing

        '        'Inicio Nivel 3*******************************************
        '        For cont2 = 1 To ContadorNivel2

        '            ContadorNivel3 = 0
        '            ReDim ArrOpcionN3(300)
        '            ReDim ArrHijosN3(300)

        '            rsConsulta3 = Server.CreateObject("ADODB.Recordset")
        '            objMenu = Server.CreateObject("Segu_DB.Acceso")
        '            rsConsulta3 = objMenu.GetOpcionesMenu(Session("codUsuario"), codAplicacion, 4, ArrOpcionN2(cont2))
        '            objMenu = Nothing

        '            If ArrHijosN2(cont2) = "1" Then

        '                strMenu = strMenu & "HM_Array1_" & cont & "_" & cont2 & " = [" & vbCrLf
        '                strMenu = strMenu & "[]," & vbCrLf

        '                While Not rsConsulta3.EOF
        '                    nAux = Replace(rsConsulta3.Fields("OPCIC_NOMPAG").value & " ", "\", "/")
        '                    If Trim(nAux & " ") <> "" Then
        '                        If InStr(1, Trim(nAux), "?") = 0 Then
        '                            RutaCompleta = strRutaSite & Trim(nAux) & "?cu=" & Session("codUsuario") & "&co=" & rsConsulta3.Fields("OPCIC_COD").value & "&ca=" & codAplicacion & "&cp=" & Session("codPerfil")
        '                        Else
        '                            RutaCompleta = strRutaSite & Trim(nAux) & "&cu=" & Session("codUsuario") & "&co=" & rsConsulta3.Fields("OPCIC_COD").value & "&ca=" & codAplicacion & "&cp=" & Session("codPerfil")
        '                        End If
        '                        FlagHijos = "0"
        '                    Else
        '                        RutaCompleta = ""
        '                        FlagHijos = "1"
        '                    End If

        '                    strMenu = strMenu & vbCrLf
        '                    strMenu = strMenu & "[""&nbsp;" & rsConsulta3.Fields("OPCIC_DES").value & """,""" & RutaCompleta & """,1,0," & FlagHijos & "]" & vbCrLf

        '                    ContadorNivel3 = ContadorNivel3 + 1
        '                    ArrOpcionN3(ContadorNivel3) = rsConsulta3.Fields("OPCIC_COD").value
        '                    ArrHijosN3(ContadorNivel3) = FlagHijos

        '                    rsConsulta3.MoveNext()
        '                    If Not rsConsulta3.EOF Then

        '                        strMenu = strMenu & " , " & vbCrLf

        '                    End If
        '                End While

        '                strMenu = strMenu & "]" & vbCrLf

        '            End If

        '            cmConsulta3 = Nothing
        '            rsConsulta3.Close()
        '            rsConsulta3 = Nothing

        '            'Inicio Nivel 4**********************************************

        '            For cont3 = 1 To ContadorNivel3

        '                ContadorNivel4 = 0
        '                ReDim ArrOpcionN4(300)
        '                ReDim ArrHijosN4(300)

        '                rsConsulta4 = Server.CreateObject("ADODB.Recordset")
        '                objMenu = Server.CreateObject("Segu_DB.Acceso")
        '                rsConsulta4 = objMenu.GetOpcionesMenu(Session("codUsuario"), codAplicacion, 5, ArrOpcionN3(cont3))
        '                objMenu = Nothing

        '                If ArrHijosN3(cont3) = "1" Then

        '                    strMenu = strMenu & "HM_Array1_" & cont & "_" & cont2 & "_" & cont3 & " = [" & vbCrLf
        '                    strMenu = strMenu & "[]," & vbCrLf


        '                    While Not rsConsulta4.EOF
        '                        nAux = Replace(rsConsulta4.Fields("OPCIC_NOMPAG").value & " ", "\", "/")
        '                        If Trim(nAux & " ") <> "" Then
        '                            If InStr(1, Trim(nAux), "?") = 0 Then
        '                                RutaCompleta = strRutaSite & Trim(nAux) & "?cu=" & Session("codUsuario") & "&co=" & rsConsulta4.Fields("OPCIC_COD").value & "&ca=" & codAplicacion & "&cp=" & Session("codPerfil")
        '                            Else
        '                                RutaCompleta = strRutaSite & Trim(nAux) & "&cu=" & Session("codUsuario") & "&co=" & rsConsulta4.Fields("OPCIC_COD").value & "&ca=" & codAplicacion & "&cp=" & Session("codPerfil")
        '                            End If
        '                            FlagHijos = "0"
        '                        Else
        '                            RutaCompleta = ""
        '                            FlagHijos = "1"
        '                        End If

        '                        strMenu = strMenu & "[""&nbsp;" & rsConsulta4.Fields("OPCIC_DES").value & """,""" & RutaCompleta & """,1,0," & FlagHijos & "]" & vbCrLf

        '                        ContadorNivel4 = ContadorNivel4 + 1
        '                        ArrOpcionN4(ContadorNivel4) = rsConsulta4.Fields("OPCIC_COD").value
        '                        ArrHijosN4(ContadorNivel4) = FlagHijos

        '                        rsConsulta4.MoveNext()
        '                        If Not rsConsulta4.EOF Then
        '                            strMenu = strMenu & " , " & vbCrLf

        '                        End If
        '                    End While

        '                    strMenu = strMenu & "]" & vbCrLf
        '                End If

        '                cmConsulta4 = Nothing
        '                rsConsulta4.Close()
        '                rsConsulta4 = Nothing
        '            Next
        '        Next
        '    Next

        '    Session("flag_menu_usuario") = 1
        '    Session("menu_usuario") = Replace(strMenu, Chr(34), Chr(39))
        '    'End If

        '    Page.Response.Write(strcad)
        '    Page.Response.Write(Replace(strMenu, Chr(39), Chr(34)))
        '    Page.Response.Write("document.write(" & Chr(34) & "<SCRIPT LANGUAGE='JavaScript1.2' SRC='" & strRutaSite & "/include/incScript" & Chr(34) & "+ HM_BrowserString + " & Chr(34) & ".js' TYPE='text/javascript'><\/SCRIPT>" & Chr(34) & ");")
        '    '	document.write("<SCR" + "IPT LANGUAGE='JavaScript1.2' SRC='/include/incScript"+ HM_BrowserString +".js' TYPE='text/javascript'><\/SCR" + "IPT>");

        '    Page.Response.Write("}" & Chr(10) & Chr(13) & "</script>" & Chr(10) & Chr(13))


        '    Page.Response.Write("</td>" & Chr(10) & Chr(13))
        '    Page.Response.Write("<td width=' * '>" & Chr(10) & Chr(13))
        '    Page.Response.Write("<iframe id='dataFrame' src='about:blank'" & Chr(10) & Chr(13))
        '    Page.Response.Write("		height='100%' width='100%' frameborder='0' scrolling='auto'>" & Chr(10) & Chr(13))
        '    Page.Response.Write("	</td>" & Chr(10) & Chr(13))
        '    Page.Response.Write("</tr>" & Chr(10) & Chr(13))
        '    Page.Response.Write("</table>" & Chr(10) & Chr(13))

        '    Page.Response.Write("</body>" & Chr(10) & Chr(13))
        'Page.Response.Write("</html>" & Chr(10) & Chr(13))
        'End If
    End Sub

End Class
