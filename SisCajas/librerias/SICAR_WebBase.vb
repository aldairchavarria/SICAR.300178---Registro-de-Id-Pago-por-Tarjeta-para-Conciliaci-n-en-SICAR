Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Configuration
Imports System.Globalization
Imports System.IO
Imports System.Reflection
Imports System.Text
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Public Class SICAR_WebBase
    Inherits System.Web.UI.Page
    '// This is a reference to the form that this page contains.  If the
    '// page contains no form, the control points to the page itself.
    Private ctlForm As Control
    '// The control that should have focus when the page finishes loading
    Private focusedControl As String
    Private bfindControl As Boolean


    Public Sub New()
        ctlForm = Me
    End Sub

    '/// <summary>
    '/// This property is used to set or get a reference to the form
    '/// control that appears on the page as indicated by the presence
    '/// of a &lt;form&gt; tag.
    '/// </summary>
    '/// <value>If not explicitly set, it defaults to the form on
    '/// the page or the page itself if there isn't one.
    '/// <p/>Derived classes can use this property to access the form
    '/// in order to insert additional controls into it.  For example,
    '/// the derived <see cref="MenuPage"/> classes use it to insert the
    '/// supporting table structure and the menu control around the form
    '/// that makes up the page's content.</value>
    '/// <exception cref="System.ArgumentException">
    '/// This property must be set to an
    '/// <see cref="System.Web.UI.HtmlControls.HtmlForm"/> or a
    '/// <see cref="System.Web.UI.Page"/> object or it will throw an
    '/// exception.</exception>
    <Browsable(False), _
    DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Property PageForm() As Control
        Get
            Return ctlForm
        End Get
        Set(ByVal Value As Control)
            '// Must be derived from one of these types
            If (TypeOf Value Is System.Web.UI.HtmlControls.HtmlForm OrElse _
                  TypeOf Value Is System.Web.UI.Page) Then
                ctlForm = Value
            Else
                Throw New ArgumentException( _
                      "PageForm must be set to an HtmlForm or Page object")
            End If
        End Set
    End Property


    '/// <summary>
    '/// This property can be used to get the current user ID without the
    '/// domain qualifier if one is present.
    '/// </summary>
    '/// <remarks>This property is only useful when using basic or
    '/// integrated security with your web application.  It is useful
    '/// for auditing purposes or looking up security related information
    '/// and saves you from having to manually remove the domain name from
    '/// the user ID.</remarks>
    '/// <value>Returns the value of the <b>User.Identity.Name</b>
    '/// property without the domain qualifier.  For example if it
    '/// is <b>MYDOMAIN\EWOODRUFF</b>, this property returns
    '/// <b>EWOODRUFF</b>.</value>
    <Browsable(False)> _
    Public ReadOnly Property CurrentUser() As String
        Get
            Dim domainUser As String = Request.ServerVariables("LOGON_USER")
            'Dim usuarioLogin As String = Trim(Mid(domainUser, InStr(1, domainUser, "\", vbTextCompare) + 1, 20))
            Dim usuarioLogin As String = domainUser.Substring(domainUser.IndexOf("\") + 1)
            If usuarioLogin Is Nothing Then usuarioLogin = ""
            Return usuarioLogin.Trim().ToUpper()
        End Get
    End Property

    Public ReadOnly Property CurrentTerminal() As String
        Get
            Return Request.ServerVariables("REMOTE_HOST")
            'Return HttpContext.Current.Request.UserHostAddress
        End Get
    End Property
    Public Sub SetEnabledAll(ByVal enabled As Boolean, ByVal ctlPageForm As Control)

        Dim form As Control = Nothing
        Dim controlType As String

        '// If null, default to the current page
        If ctlPageForm Is Nothing Then ctlPageForm = Me.PageForm
        '// Yes, I could add a reference to the MS IE Web Controls, but
        '// I don't want this library to have a dependency on it so we'll
        '// just check for IE Web Controls by type name string instead.
        controlType = ctlPageForm.ToString()

        '// If passed a form, panel, multi-page, or page view, use it
        '// directly.  If passed a page, see if it contains a form.  If so,
        '// use that form.  If not, use the page.
        If (TypeOf ctlPageForm Is System.Web.UI.HtmlControls.HtmlForm OrElse _
               TypeOf ctlPageForm Is System.Web.UI.WebControls.Panel OrElse _
               controlType.IndexOf("MultiPage") <> -1 OrElse _
               controlType.IndexOf("PageView") <> -1) Then
            form = ctlPageForm
        Else
            If TypeOf ctlPageForm Is System.Web.UI.Page AndAlso _
                  Not ctlPageForm Is PageForm Then
                form = SICAR_WebBase.FindPageForm(CType(ctlPageForm, Page))
            End If
        End If
        '// Ignore anything unexpected
        If (form Is Nothing) Then
            Exit Sub
        End If

        Dim ctl As Control
        '// Disable each edit control on the page
        For Each ctl In form.Controls
            If (TypeOf ctl Is System.Web.UI.WebControls.TextBox OrElse _
                TypeOf ctl Is System.Web.UI.WebControls.DropDownList OrElse _
                TypeOf ctl Is System.Web.UI.WebControls.ListBox OrElse _
                TypeOf ctl Is System.Web.UI.WebControls.CheckBox OrElse _
                TypeOf ctl Is System.Web.UI.WebControls.CheckBoxList OrElse _
                TypeOf ctl Is System.Web.UI.WebControls.RadioButton OrElse _
                TypeOf ctl Is System.Web.UI.WebControls.RadioButtonList) Then
                CType(ctl, WebControl).Enabled = enabled
            Else
                '    // As above, done this way to avoid a dependency
                controlType = ctl.ToString()
                If (TypeOf ctl Is System.Web.UI.WebControls.Panel OrElse _
                     controlType.IndexOf("MultiPage") <> -1 OrElse _
                     controlType.IndexOf("PageView") <> -1) Then
                    Me.SetEnabledAll(enabled, ctl)   '// Recursive for these
                End If
            End If
        Next

    End Sub

    Private Shared Function FindPageForm(ByVal cpage As Page) As Control
        Dim form As Control = Nothing

        '// Find the form on this page if there is one
        Dim ctlItem As Control
        For Each ctlItem In cpage.Controls
            If (TypeOf ctlItem Is System.Web.UI.HtmlControls.HtmlForm) Then
                form = ctlItem
                Exit For
            End If
        Next
        Return form

    End Function


    Protected Overrides Sub OnInit(ByVal e As EventArgs)

        MyBase.OnInit(e)

        '// Find the form on the page if there is one
        If TypeOf ctlForm Is System.Web.UI.Page Then

            Dim form As Control = SICAR_WebBase.FindPageForm(CType(ctlForm, Page))

            If (Not form Is Nothing) Then
                ctlForm = form
            End If
        End If
       'PROY-140126
       WebComunes.CargarAppSettings() 

    End Sub


    '/// This sets the control that should have the focus when the page
    '/// has finished loading by control reference.
    Public Sub SetFocusExtended(ByVal ctl As WebControl)
        If Not ctl Is Nothing Then
            focusedControl = ctl.ClientID
            bfindControl = False
        Else
            focusedControl = Nothing
        End If
    End Sub


    '/// This sets the control that should have the focus when the page
    '/// has finished loading by control ID.
    Public Sub SetFocusExtended(ByVal clientId As String)
        focusedControl = clientId
        bfindControl = True
    End Sub


    Protected Overrides Sub OnPreRender(ByVal e As EventArgs)

        MyBase.OnPreRender(e)
        Dim sb As StringBuilder

        If Not focusedControl Is Nothing Then
            sb = New StringBuilder
            If Not focusedControl Is Nothing Then
                sb.Append("<script type='text/javascript'>" + Chr(13) + _
                        "<!--" + Chr(13) + "BP_funSetFocus('")
                sb.Append(focusedControl)

                If (bfindControl = False) Then
                    sb.Append("', false);" + Chr(13))
                Else
                    sb.Append("', true);" + Chr(13))
                End If
                sb.Append("//-->" + Chr(13) + "</script>")
                Me.RegisterStartupScript("BP_FocusCtl", sb.ToString())
                sb.Remove(0, sb.Length)
            End If

            sb.Append("<script type='text/javascript' src='")
            sb.Append(ResSrvHandler.ResSrvHandlerPageName)
            sb.Append("?Res=SetFocus.js'></script>")

            Me.RegisterClientScriptBlock("BP_SFJS", sb.ToString())
        End If

    End Sub
    Public Function GetParam(ByVal pstrNombreParam As String) As String
        Dim strParam As String = Request(pstrNombreParam)
        If (strParam Is Nothing) Then strParam = ""

        Return strParam.Trim()
    End Function

    Public Sub ManejaError(ByVal obExcepcion As Exception)

        Dim iPos As Integer = obExcepcion.Message.IndexOf("NO_DATA_FOUND")
        If iPos >= 0 Then
            Response.Write("<script language=jscript> alert('No existen datos');</script>")
        Else
            Response.Write("<font Color='Red' size='1' face='Verdana'><b>" & obExcepcion.Message & "</b></font>")
        End If
    End Sub
    Public Sub Alert(ByVal mensaje As String)
        'Response.Write("<script language=jscript> alert('" + mensaje + "');</script>")
        Response.Write("<script language=jscript> alert(" + """" + mensaje + """" + ");</script>")
    End Sub
    Public Sub AlertYCierra(ByVal mensaje As String)
        'Response.Write("<script language=jscript> alert('" + mensaje + "');</script>")
        Response.Write("<script language=jscript> alert(" + """" + mensaje + """" + ");window.close();</script>")
        Response.End()
    End Sub
  'PROY-27440 INI
  Public Function Devolver_TipoPago_POS(ByVal CodTipPago As String) As String
    Dim strDescripcion As String = ""
    Select Case CodTipPago
      Case "01"
        strDescripcion = "Ventas Rapidas"
      Case "02"
        strDescripcion = "Recarga Virtual Frecuente"
      Case "03"
        strDescripcion = "Documentos por pagar"
      Case "04"
        strDescripcion = "Pago de Recibos"
      Case "05"
        strDescripcion = "Recaudaciones a DAC"
      Case "06"
        strDescripcion = "Recaudación Clientes Corporativos"
      Case "07"
        strDescripcion = "Pago adelantado Postpago"
      Case "08"
        strDescripcion = "Pago Clientes Castigados"
      Case "09"
        strDescripcion = "Pago Recarga DTH"
      Case "10"
        strDescripcion = "Recaudación Clientes Fijos y Paginas"
      Case "11"
        strDescripcion = "Documentos Pagados"
      Case "12"
        strDescripcion = "Recaudaciones Procesadas"
      Case "13"
        strDescripcion = "Devoluciones"
      Case "14"
        strDescripcion = "Operaciones no financiera"
      Case "15"
                strDescripcion = "Recaudacion por DNI"
      Case Else
        strDescripcion = "Documentos por pagar"

    End Select
    Return strDescripcion

  End Function
  'PROY-27440 FIN

    Public Function LoadConfiguration(ByVal keyGroup As String) As clsTipificationConfiguration

        Dim propName As String
        Dim propValue As Object
        Dim keyList As New clsTipificationConfiguration

        Try

        Dim tipificationList As ArrayList = (New COM_SIC_Activaciones.clsConsultaPvu).ConsultaParametros(keyGroup)

            For Each prop As PropertyInfo In keyList.GetType.GetProperties()

                propName = prop.Name

                For Each oItem As COM_SIC_Activaciones.BEParametros In tipificationList

                    If prop.Name = oItem.strDescripcion Then
                        prop.SetValue(keyList, oItem.strValor, Nothing)
                        Exit For
                    End If

                Next

            Next

        Catch ex As Exception
            Throw ex
        End Try

        Return keyList

    End Function

End Class
