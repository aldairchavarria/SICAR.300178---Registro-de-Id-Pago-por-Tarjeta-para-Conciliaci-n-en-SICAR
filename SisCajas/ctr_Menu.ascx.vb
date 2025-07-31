Public Class ctr_Menu
    Inherits System.Web.UI.UserControl

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


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Introducir aquí el código de usuario para inicializar la página
        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = "Log_Menu_WS_Auditoria"
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)

        Try
            Dim ImagenIzq, lRsPerfil, wObjPerf, objMenu, RutaCompleta, FlagHijos, nAux
            Dim strMenu As String


            Dim cont, cont2, cont3, cont4 As Integer
            Dim codAplicacion As Integer

            Dim ArrOpcionN1(100), ArrOpcionN2(200), ArrOpcionN3(200), ArrOpcionN4(200) As String
            Dim ArrHijosN1(100), ArrHijosN2(200), ArrHijosN3(200), ArrHijosN4(200) As String
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("strRutaSite")


            'Introducir aquí el código de usuario para inicializar la página
            Dim strcad As String
            codAplicacion = ConfigurationSettings.AppSettings("CodAplicacion")
            Session("flag_menu_usuario") = CInt(Request.Form.Get("flag_menu_usuario"))
            Session("menu_usuario") = Request.Form.Get("menu_usuario")

            cont = 1
            cont2 = 1
            cont3 = 1
            cont4 = 1

            ImagenIzq = "<img src='" & strRutaSite & "/images/mnu_cuadrado.gif' border=0> "
            Page.Response.Write("<HTML>" & Chr(13) & Chr(10) & Chr(13) & Chr(10))
            Page.Response.Write("<HEAD>" & Chr(13) & Chr(10))
            Page.Response.Write("<TITLE>Aplicativo TIM</TITLE>" & Chr(13) & Chr(10))
            Page.Response.Write("<meta http-equiv='Pragma' content='no-cache'>" & Chr(13) & Chr(10))
            Page.Response.Write("<meta HTTP-EQUIV='Expires' CONTENT='Mon, 06 Jan 1990 00:00:01 GMT'>" & Chr(13) & Chr(10))
            Page.Response.Write("<link href='../estilos/est_General.css' rel='styleSheet' type='text/css'>" & Chr(13) & Chr(10))
            Page.Response.Write("</HEAD>" & Chr(13) & Chr(10))

            Page.Response.Write("<body topmargin='0' leftmargin='0' marginwidth='0' marginheight='0'>" & Chr(13) & Chr(10))
            Page.Response.Write("<table height='100%' width='100%'><tbody>" & Chr(13) & Chr(10))
            Page.Response.Write("<tr>" & Chr(13) & Chr(10))
            Page.Response.Write("<td width=170 valign=top>" & Chr(13) & Chr(10))

            Page.Response.Write("<link rel='stylesheet' href='" & strRutaSite & "/Estilos/estilo.css' type='text/css'>" & Chr(13) & Chr(10) & Chr(13) & Chr(10))

            strcad = "<script language='JavaScript1.2'>" & Chr(13) & Chr(10)
            strcad = strcad & "if(window.event + '' == 'undefined') event = null;" & Chr(13) & Chr(10)
            strcad = strcad & "function HM_f_PopUp(){return false};" & Chr(13) & Chr(10)

            strcad = strcad & "function solicitprep(){" & Chr(13) & Chr(10)
            strcad = strcad & "window.open('" & strRutaSite & "/paginas/ventas/prepago/solicitud/SolicitudPrepago.asp','Solicitud','status=no,resizable=no,toolbar=no,scrollbars=yes,modal=yes,left=100,top=100,width=850,height=300');" & Chr(13) & Chr(10)
            strcad = strcad & "}" & Chr(13) & Chr(10)

            strcad = strcad & "function HM_f_PopDown(){return false};" & Chr(13) & Chr(10)
            strcad = strcad & "popUp = HM_f_PopUp;" & Chr(13) & Chr(10)
            strcad = strcad & "popDown = HM_f_PopDown;" & Chr(13) & Chr(10) & Chr(13) & Chr(10)

            strcad = strcad & "HM_PG_MenuWidth = 170;" & Chr(13) & Chr(10)
            strcad = strcad & "HM_PG_FontFamily = 'Verdana';" & Chr(13) & Chr(10)
            strcad = strcad & "HM_PG_FontSize = 7.5;" & Chr(13) & Chr(10)
            strcad = strcad & "HM_PG_FontBold = 1;" & Chr(13) & Chr(10)
            strcad = strcad & "HM_PG_FontItalic = 0;" & Chr(13) & Chr(10)
            strcad = strcad & "HM_PG_FontColor = 'white';" & Chr(13) & Chr(10)
            strcad = strcad & "HM_PG_FontColorOver = 'white';" & Chr(13) & Chr(10)
            strcad = strcad & "HM_PG_ItemPadding = 3;" & Chr(13) & Chr(10) & Chr(13) & Chr(10)

            strcad = strcad & "HM_PG_BorderWidth = 1;" & Chr(13) & Chr(10)
            strcad = strcad & "HM_PG_BorderColor = 'black';" & Chr(13) & Chr(10)
            strcad = strcad & "HM_PG_BorderStyle = 'solid';" & Chr(13) & Chr(10)
            strcad = strcad & "HM_PG_SeparatorSize = 1;" & Chr(13) & Chr(10)
            strcad = strcad & "HM_PG_ImageSrc = '" & strRutaSite & "/images/mnu_flecha.gif';" & Chr(13) & Chr(10)
            strcad = strcad & "HM_PG_ImageSrcLeft = '" & strRutaSite & "/include/mnu_flecha.gif';" & Chr(13) & Chr(10) & Chr(13) & Chr(10)

            strcad = strcad & "HM_PG_ImageSize = 7;" & Chr(13) & Chr(10)
            strcad = strcad & "HM_PG_ImageHorizSpace = 0;" & Chr(13) & Chr(10)
            strcad = strcad & "HM_PG_ImageVertSpace = 3;" & Chr(13) & Chr(10) & Chr(13) & Chr(10)

            strcad = strcad & "HM_PG_KeepHilite = true; " & Chr(13) & Chr(10)
            strcad = strcad & "HM_PG_ClickStart = 0;" & Chr(13) & Chr(10)
            strcad = strcad & "HM_PG_ClickKill = false;" & Chr(13) & Chr(10)
            strcad = strcad & "HM_PG_ChildOverlap = 1; //adentro" & Chr(13) & Chr(10)
            strcad = strcad & "HM_PG_ChildOffset = 0; //arriba" & Chr(13) & Chr(10)
            strcad = strcad & "HM_PG_ChildPerCentOver = null;" & Chr(13) & Chr(10)
            strcad = strcad & "HM_PG_TopSecondsVisible = .3;" & Chr(13) & Chr(10)
            strcad = strcad & "HM_PG_StatusDisplayBuild =0;" & Chr(13) & Chr(10)
            strcad = strcad & "HM_PG_StatusDisplayLink = 0;" & Chr(13) & Chr(10)
            strcad = strcad & "HM_PG_UponDisplay = null;" & Chr(13) & Chr(10)
            strcad = strcad & "HM_PG_UponHide = null;" & Chr(13) & Chr(10)
            strcad = strcad & "HM_PG_RightToLeft = false;" & Chr(13) & Chr(10) & Chr(13) & Chr(10)

            strcad = strcad & "HM_PG_ShowLinkCursor = 1;" & Chr(13) & Chr(10)
            strcad = strcad & "HM_PG_NSFontOver = true;" & Chr(13) & Chr(10) & Chr(13) & Chr(10)

            strcad = strcad & "var iWidth= 180;" & Chr(13) & Chr(10)
            strcad = strcad & "var iTopPos= 0;" & Chr(13) & Chr(10)
            strcad = strcad & "var iLeftPos= 0;" & Chr(13) & Chr(10)
            strcad = strcad & "var sFontColor= '#FFFFFF';" & Chr(13) & Chr(10)
            strcad = strcad & "var sFontHighColor= '#FFFFFF';" & Chr(13) & Chr(10)
            strcad = strcad & "var sBackgroundColor= '#5991bb';" & Chr(13) & Chr(10)
            strcad = strcad & "var sBackgroundHighColor= '#00007b';" & Chr(13) & Chr(10)
            strcad = strcad & "var sTableBorderColor= '#EFEFEF';" & Chr(13) & Chr(10)
            'strcad = strcad & "var sLineColor= '#FF9369';" & Chr(13) & Chr(10) & Chr(13) & Chr(10)
            strcad = strcad & "var sLineColor= 'white';" & Chr(13) & Chr(10) & Chr(13) & Chr(10)

            strcad = strcad & "var intRows= 13;" & Chr(13) & Chr(10)
            strcad = strcad & "var intHeight= 23.9;" & Chr(13) & Chr(10)
            strcad = strcad & "var iBlankspace= 14;" & Chr(13) & Chr(10)
            strcad = strcad & "//Fin de Variables" & Chr(13) & Chr(10) & Chr(13) & Chr(10)

            strcad = strcad & "HM_DOM = (document.getElementById) ? true : false;" & Chr(13) & Chr(10)
            strcad = strcad & "HM_NS4 = (document.layers) ? true : false;" & Chr(13) & Chr(10)
            strcad = strcad & "HM_IE = (document.all) ? true : false;" & Chr(13) & Chr(10)
            strcad = strcad & "HM_IE4 = HM_IE && !HM_DOM;" & Chr(13) & Chr(10)
            strcad = strcad & "HM_Mac = (navigator.appVersion.indexOf('Mac') != -1);" & Chr(13) & Chr(10)
            strcad = strcad & "HM_IE4M = HM_IE4 && HM_Mac;" & Chr(13) & Chr(10)
            strcad = strcad & "HM_IsMenu = (HM_DOM || HM_NS4 || (HM_IE4 && !HM_IE4M));" & Chr(13) & Chr(10) & Chr(13) & Chr(10)

            strcad = strcad & "HM_BrowserString = HM_NS4 ? 'NS4' : HM_DOM ? 'DOM' : 'IE4';" & Chr(13) & Chr(10) & Chr(13) & Chr(10)

            strcad = strcad & "if(window.event + '' == 'undefined') event = null;" & Chr(13) & Chr(10)
            strcad = strcad & "function HM_f_PopUp(){return false};" & Chr(13) & Chr(10)
            strcad = strcad & "function HM_f_PopDown(){return false};" & Chr(13) & Chr(10)
            strcad = strcad & "popUp = HM_f_PopUp;" & Chr(13) & Chr(10)
            strcad = strcad & "popDown = HM_f_PopDown;" & Chr(13) & Chr(10) & Chr(13) & Chr(10)
            strcad = strcad & "if(HM_IsMenu) {" & Chr(13) & Chr(10)
            strcad = strcad & "	//Inicio de Opciones" & Chr(13) & Chr(10)
            strcad = strcad & "	HM_Array1= [ [iWidth,  iLeftPos, iTopPos, sFontColor, sFontHighColor, sBackgroundColor, sBackgroundHighColor, sTableBorderColor, sLineColor, 1,	0, 0, 1, 1, 1, 'null', 'null'," & Chr(13) & Chr(10)
            strcad = strcad & "	]," & Chr(13) & Chr(10)


            'Acceder a web service para generar el menú dinámico
            Dim pWSMenu As New COM_SIC_Activaciones.AuditoriaWS.EbsAuditoriaService
            pWSMenu.Url = ConfigurationSettings.AppSettings("consRutaWSSeguridad")
            Dim pMenuRequest As New COM_SIC_Activaciones.AuditoriaWS.OpcionesUsuarioRequest
            pMenuRequest.usuario = Session("codUsuario") 'Código obtenido del método DatosUsuario 
            'Nota: Debe mantenerse el código de la aplicación en la base de datos de forma segura
            pMenuRequest.aplicacion = codAplicacion
            Dim pMenuResponse As New COM_SIC_Activaciones.AuditoriaWS.OpcionesUsuarioResponse
            pMenuResponse = pWSMenu.leerOpcionesPorUsuario(pMenuRequest)
            strMenu = ""


            'Inicio Nivel 1******************************************************************
            For Each item As COM_SIC_Activaciones.AuditoriaWS.MenuType In pMenuResponse.menues.menuItem
                If Not item.datosMenu Is Nothing Then
                    With item.datosMenu
                        If .pagina.Trim() = "" Then
                            FlagHijos = "1"
                            strMenu = strMenu & "[""" & .descripcion.Trim() & """,""" & .pagina.Trim() & """,1,0," & FlagHijos & "]" & vbCrLf
                        Else
                            FlagHijos = "0"
                            strMenu = strMenu & "[""" & .descripcion.Trim() & """,""" & strRutaSite & .pagina.Trim() & """,1,0," & FlagHijos & "]" & vbCrLf
                        End If
                    End With
                    strMenu = strMenu & " , " & vbCrLf
                End If
            Next

            strMenu = strMenu.Substring(0, strMenu.Length - 4)
            strMenu = strMenu & "]" & vbCrLf

            'Inicio Nivel 2******************************************************************
            cont = 0
            For Each item As COM_SIC_Activaciones.AuditoriaWS.MenuType In pMenuResponse.menues.menuItem
                With item
                    cont = cont + 1
                    If Not .padres Is Nothing And .datosMenu.pagina.Trim() = "" Then
                        'Armar submenú de padre
                        strMenu = strMenu & "HM_Array1_" & cont & " = [" & vbCrLf
                        strMenu = strMenu & vbTab & "[]," & vbCrLf
                        For Each itemPadre As COM_SIC_Activaciones.AuditoriaWS.PadreType In .padres.padreItem
                            If Not itemPadre.datosPadre.descripcion Is Nothing Then
                                With itemPadre
                                    strMenu = strMenu & vbCrLf
                                    If Not .hijos.hijoItem Is Nothing Then
                                        FlagHijos = "1"
                                        strMenu = strMenu & "["" " & .datosPadre.descripcion.Trim() & """,""" & .datosPadre.pagina.Trim() & """,1,0," & FlagHijos & "]" & vbCrLf
                                    Else
                                        FlagHijos = "0"
                                        strMenu = strMenu & "["" " & .datosPadre.descripcion.Trim() & """,""" & strRutaSite & .datosPadre.pagina.Trim() & """,1,0," & FlagHijos & "]" & vbCrLf
                                    End If

                                    strMenu = strMenu & vbCrLf
                                    strMenu = strMenu & " , " & vbCrLf
                                End With
                            End If
                        Next

                        strMenu = strMenu.Substring(0, strMenu.Length - 4)
                        strMenu = strMenu & "]" & vbCrLf

                        'Inicio Nivel 3*******************************************
                        cont2 = 0
                        For Each itemPadre As COM_SIC_Activaciones.AuditoriaWS.PadreType In .padres.padreItem
                            If Not itemPadre.datosPadre.descripcion Is Nothing Then
                                With itemPadre
                                    cont2 = cont2 + 1
                                    If Not .hijos.hijoItem Is Nothing Then
                                        'Armar submenú de hijos                                        
                                        strMenu = strMenu & "HM_Array1_" & cont & "_" & cont2 & " = [" & vbCrLf
                                        strMenu = strMenu & "[]," & vbCrLf
                                        For Each itemHijo As COM_SIC_Activaciones.AuditoriaWS.HijoType In .hijos.hijoItem
                                            With itemHijo
                                                strMenu = strMenu & vbCrLf
                                                If Not .nietos.nietoItem Is Nothing Then
                                                    FlagHijos = "1"
                                                    strMenu = strMenu & "["" " & .datosHijos.descripcion.Trim() & """,""" & .datosHijos.pagina.Trim() & """,1,0," & FlagHijos & "]" & vbCrLf
                                                Else
                                                    FlagHijos = "0"
                                                    strMenu = strMenu & "["" " & .datosHijos.descripcion.Trim() & """,""" & strRutaSite & .datosHijos.pagina.Trim() & """,1,0," & FlagHijos & "]" & vbCrLf
                                                End If

                                                strMenu = strMenu & vbCrLf
                                                strMenu = strMenu & " , " & vbCrLf
                                            End With
                                        Next

                                        strMenu = strMenu.Substring(0, strMenu.Length - 4)
                                        strMenu = strMenu & "]" & vbCrLf

                                        'Inicio Nivel 4*******************************************
                                        cont3 = 0
                                        For Each itemHijo As COM_SIC_Activaciones.AuditoriaWS.HijoType In .hijos.hijoItem
                                            With itemHijo
                                                cont3 = cont3 + 1
                                                If Not .nietos.nietoItem Is Nothing Then
                                                    'Armar submenú de nietos
                                                    'cont3 = cont3 + 1
                                                    strMenu = strMenu & "HM_Array1_" & cont & "_" & cont2 & "_" & cont3 & " = [" & vbCrLf
                                                    strMenu = strMenu & "[]," & vbCrLf
                                                    For Each itemNieto As COM_SIC_Activaciones.AuditoriaWS.NietoType In .nietos.nietoItem
                                                        With itemNieto
                                                            FlagHijos = "0"
                                                            strMenu = strMenu & vbCrLf
                                                            strMenu = strMenu & "["" " & .nieto.descripcion.Trim() & """,""" & strRutaSite & .nieto.pagina.Trim() & """,1,0," & FlagHijos & "]" & vbCrLf
                                                            strMenu = strMenu & vbCrLf
                                                            strMenu = strMenu & " , " & vbCrLf
                                                        End With
                                                    Next

                                                    strMenu = strMenu.Substring(0, strMenu.Length - 4)
                                                    strMenu = strMenu & "]" & vbCrLf
                                                End If
                                            End With
                                        Next

                                    End If
                                End With
                            End If
                        Next
                    End If
                End With
            Next

            Session("flag_menu_usuario") = 1
            Session("menu_usuario") = Replace(strMenu, Chr(34), Chr(39))

            strcad += Replace(strMenu, Chr(39), Chr(34))
            strcad += "document.write(" & Chr(34) & "<SCRIPT LANGUAGE='JavaScript1.2' SRC='" & strRutaSite & "../../include/incScript" & Chr(34) & "+ HM_BrowserString + " & Chr(34) & ".js' TYPE='text/javascript'><\/SCRIPT>" & Chr(34) & ");"
            objFileLog.Log_WriteLog(pathFile, strArchivo, Session("codUsuario") & "- " & "strcad1: " & "document.write(" & Chr(34) & "<SCRIPT LANGUAGE='JavaScript1.2' SRC='" & strRutaSite & "/include/incScript" & Chr(34) & "+ HM_BrowserString + " & Chr(34) & ".js' TYPE='text/javascript'><\/SCRIPT>" & Chr(34) & ");")
            objFileLog.Log_WriteLog(pathFile, strArchivo, Session("codUsuario") & "- " & "strcad2: " & "document.write(" & Chr(34) & "<SCRIPT LANGUAGE='JavaScript1.2' SRC='" & strRutaSite & "../../include/incScript" & Chr(34) & "+ HM_BrowserString + " & Chr(34) & ".js' TYPE='text/javascript'><\/SCRIPT>" & Chr(34) & ");")
            strcad += "}" & Chr(10) & Chr(13) & "</SCRIPT>" & Chr(10) & Chr(13)
            strcad += "</td>" & Chr(10) & Chr(13)
            strcad += "	</tr>" & Chr(10) & Chr(13)
            strcad += "</table>" & Chr(10) & Chr(13)
            Page.Response.Write(strcad)

        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: Page_Load)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, Session("codUsuario") & "- " & "ERROR: " & ex.Message.ToString() & MaptPath)
            'FIN PROY-140126           
            objFileLog.Log_WriteLog(pathFile, strArchivo, Session("codUsuario") & "- " & "ERROR: " & ex.StackTrace.ToString())
        End Try
    End Sub

End Class
