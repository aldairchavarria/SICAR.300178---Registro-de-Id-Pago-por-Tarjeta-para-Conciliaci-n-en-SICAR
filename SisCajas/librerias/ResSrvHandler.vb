Imports System
Imports System.IO
Imports System.Reflection
Imports System.Web
Imports System.Xml


'//=============================================================================
'// File    : ResSrvHandler.cs
'// Author  : Eric Woodruff
'// Updated : 04/02/2006
'// Compiler: Microsoft Visual C#
'// Modify by: RR in VB.Net
'// This file contains a derived System.Web.IHttpHandler class that acts as a
'// resource server to send resources to the client browser such as scripts,
'// images, etc.

Public Class ResSrvHandler
    Implements System.Web.IHttpHandler


    '/// The ASPX page name that will cause requests to get routed
    '/// The path to the image resources.  The default value is
    Public Const ResSrvHandlerPageName As String = "Claro.PVU.Web.Recursos.aspx"
    Private Const cImageResPath As String = "Claro.PVU.Web."
    '/// The path to the script resources.  The default value is
    Private Const cScriptResPath As String = "Claro.PVU.Web."

    '''/// This can be called to format a URL to a resource name that is
    '''/// embedded within the assembly.
    Public Shared Function ResourceUrl(ByVal strResourceName As String, _
                    ByVal bCacheResource As Boolean) As String
        Return String.Format("{0}?Res={1}{2}", ResSrvHandlerPageName, _
                    strResourceName, IIf((bCacheResource), "", "&NoCache=1"))
    End Function

    '/// This can be called to format a URL to a resource name that is
    '/// embedded within the assembly.
    Public Shared Function ResourceUrl(ByVal strAssemblyName As String, _
                    ByVal strResourceHandlerName As String, ByVal strResourceName As String, _
                    ByVal bCacheResource As Boolean) As String
        Return String.Format("{0}?Assembly={1}&Res={2}{3}", _
                        strResourceHandlerName, _
                        HttpContext.Current.Server.UrlEncode(strAssemblyName), _
                        strResourceName, IIf((bCacheResource), "", "&NoCache=1"))
    End Function


    ' /// This property is used to indicate that the object instance can
    '/// be used by other requests.  It always returns true.
    Public ReadOnly Property IsReusable() As Boolean Implements System.Web.IHttpHandler.IsReusable
        Get
            Return True
        End Get
    End Property


    '/// Load the resource specified in the query string and return it
    '/// as the HTTP response.
    Public Sub ProcessRequest(ByVal context As System.Web.HttpContext) Implements System.Web.IHttpHandler.ProcessRequest

        Dim asm As [Assembly] = Nothing


        Dim sr As StreamReader = Nothing
        Dim s As Stream = Nothing

        Dim strResName, strType As String
        Dim byImage() As Byte
        Dim nLen As Integer
        Dim bUseInternalPath As Boolean = True

        '// TODO: Be sure to adjust the QueryString names if you are
        '// using something other than Res and NoCache.
        '// Get the resource name and base the type on the extension

        strResName = context.Request.QueryString("Res")
        strType = strResName.Substring(strResName.LastIndexOf(".") + 1).ToLower()

        Try
            context.Response.Clear()

            '// If caching is not disabled, set the cache parameters so that
            '// the response is cached on the client for up to one day.
            If (context.Request.QueryString("NoCache") Is Nothing) Then

                '// TODO: Adjust caching length as needed.

                context.Response.Cache.SetExpires(DateTime.Now.AddDays(1))
                context.Response.Cache.SetCacheability(HttpCacheability.Public)
                context.Response.Cache.SetValidUntilExpires(False)

                '// Vary by parameter name.  Note that if you have more
                '// than one, add additional lines to specify them.
                context.Response.Cache.VaryByParams("Res") = True

            Else
                '// The response is not cached
                context.Response.Cache.SetExpires(DateTime.Now.AddDays(-1))
                context.Response.Cache.SetCacheability(HttpCacheability.NoCache)
            End If

            '// Get the resource from this assembly or another?
            If (context.Request.QueryString("Assembly") Is Nothing) Then
                asm = System.Reflection.Assembly.GetExecutingAssembly()
            Else

                Dim asmList() As [Assembly] = AppDomain.CurrentDomain.GetAssemblies()
                Dim strSearchName As String = context.Request.QueryString("Assembly")
                Dim a As [Assembly]
                For Each a In asmList
                    If (a.GetName().Name = strSearchName) Then
                        asm = a
                        Exit For
                    End If
                Next

                If (asm Is Nothing) Then
                    Throw New ArgumentOutOfRangeException("Assembly", _
                        strSearchName, "Assembly not found")
                End If
                '// Now get the resources listed in the assembly manifest
                '// and look for the filename.  Note the fact that it is
                '// matched on the filename and not necessarily the path
                '// within the assembly.  This may restricts you to using
                '// a filename only once, but it also prevents the problem
                '// that the VB.NET compiler has where it doesn't seem to
                '// output folder names on resources.
                Dim strResource As String
                For Each strResource In asm.GetManifestResourceNames()
                    If (strResource.EndsWith(strResName)) Then
                        strResName = strResource
                        bUseInternalPath = False
                        Exit For
                    End If
                Next
            End If
            Select Case strType

                Case "gif", "jpg", "jpeg", "tiff", "bmp", "png", "tif"
                    If (strType = "jpg") Then
                        strType = "jpeg"
                    Else
                        If (strType = "png") Then
                            strType = "x-png"
                        Else
                            If (strType = "tif") Then
                                strType = "tiff"
                            End If
                        End If
                    End If


                    context.Response.ContentType = "image/" + strType

                    If (bUseInternalPath = True) Then
                        strResName = cImageResPath + strResName
                    End If

                    s = asm.GetManifestResourceStream(strResName)

                    nLen = Convert.ToInt32(s.Length)
                    ReDim byImage(nLen)
                    'byImage = New Byte(nLen)
                    s.Read(byImage, 0, nLen)

                    context.Response.OutputStream.Write(byImage, 0, nLen)

                Case "js", "vb", "vbs"
                    If (strType = "js") Then
                        context.Response.ContentType = "text/javascript"
                    Else
                        context.Response.ContentType = "text/vbscript"
                    End If

                    If (bUseInternalPath = True) Then
                        strResName = cScriptResPath + strResName
                    End If

                    sr = New StreamReader(asm.GetManifestResourceStream(strResName))
                    context.Response.Write(sr.ReadToEnd())

                Case "css" '// Some style sheet info
                    '// Not enough to embed so we'll write it out from here
                    context.Response.ContentType = "text/css"

                    If (bUseInternalPath = True) Then
                        context.Response.Write(".Style1 { font-weight: bold; " + _
                            "color: #dc143c; font-style: italic; " + _
                            "text-decoration: underline; }\n" + _
                            ".Style2 { font-weight: bold; color: navy; " + _
                            "text-decoration: underline; }\n")
                    Else
                        '// CSS from some other source
                        sr = New StreamReader( _
                            asm.GetManifestResourceStream(strResName))
                        context.Response.Write(sr.ReadToEnd())
                    End If



                Case "htm", "html"
                    context.Response.ContentType = "text/html"

                    sr = New StreamReader( _
                        asm.GetManifestResourceStream(strResName))
                    context.Response.Write(sr.ReadToEnd())

                Case "xml" '   // Even some XML
                    context.Response.ContentType = "text/xml"

                    sr = New StreamReader( _
                        asm.GetManifestResourceStream( _
                        "ResServerTest.Web.Controls." + strResName))

                    '// This is used to demonstrate the NoCache option.
                    '// We'll modify the XML to show the current server
                    '// date and time.
                    Dim strXML As String = sr.ReadToEnd()
                    context.Response.Write(strXML.Replace("DATETIME", _
                                      DateTime.Now.ToString()))
                Case Else ' // Unknown resource type
                    Throw New Exception("Unknown resource type")
            End Select
        Catch excp As Exception
            Dim xml As XmlDocument
            Dim node, element As XmlNode

            Dim strMsg As String = excp.Message.Replace("\r\n", " ")

            context.Response.Clear()
            context.Response.Cache.SetExpires(DateTime.Now.AddDays(-1))
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache)

            ' // For script, write out an alert describing the problem.
            '// For XML, send an XML response containing the exception.
            '// For all other resources, just let it display a broken link
            '// or whatever.
            Select Case strType
                Case "js"
                    context.Response.ContentType = "text/javascript"
                    context.Response.Write("alert('Could not load resource " + _
                        strResName + ": " + strMsg + "');")
                Case "vb", "vbs"

                    context.Response.ContentType = "text/vbscript"
                    context.Response.Write("MsgBox ""Could not load resource '" + _
                                strResName + "': " + strMsg + " "");")
                Case "xml"
                    xml = New XmlDocument
                    node = xml.CreateElement("ResourceError")

                    element = xml.CreateElement("Resource")
                    element.InnerText = "Could not load resource: " + _
                        strResName
                    node.AppendChild(element)

                    element = xml.CreateElement("Exception")
                    element.InnerText = strMsg
                    node.AppendChild(element)

                    xml.AppendChild(node)
                    context.Response.Write(xml.InnerXml)

            End Select
        Finally
            If Not sr Is Nothing Then
                sr.Close()
            End If
            If Not s Is Nothing Then
                s.Close()
            End If
        End Try

    End Sub

End Class

