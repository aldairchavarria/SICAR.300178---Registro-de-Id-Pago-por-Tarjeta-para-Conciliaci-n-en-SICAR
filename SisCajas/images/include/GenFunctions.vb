Imports SisCajas.clsContantes_site

Public Class GenFunctions
    Public Server As System.Web.HttpServerUtility
    Public Request As System.Web.HttpRequest
    Public Response As System.Web.HttpResponse

    'Crea una nueva instancia de GenFunctions 

    Public Sub New(ByRef aux_Server As System.Web.HttpServerUtility, ByRef auxRequest As System.Web.HttpRequest, ByRef auxResponse As System.Web.HttpResponse)
        Me.Server = aux_Server
        Me.Request = auxRequest
        Me.Response = auxResponse
    End Sub


    '******************************************************
    ' Funcion general
    '******************************************************
    Public Sub createprueba(ByVal nombre, ByVal texto, ByVal extension)
        Dim fso
        Dim txt
        Dim FileName
        FileName = Request.ServerVariables("APPL_PHYSICAL_PATH") & "\Cache\" & nombre & extension

        fso = CreateObject("Scripting.FileSystemObject")
        If fso.FileExists(FileName) Then
            fso.DeleteFile(FileName)
        End If
        txt = fso.CreateTextFile(FileName)
        txt.Write(texto)
        txt.Close()
    End Sub

    Public Function XmlToRecordset(ByVal strXML, ByVal Nombre)
        Dim Tiempo
        Tiempo = Timer

        Dim RsResp
        Dim RecordXml
        Dim NumRecordsets
        Dim INodeRecordset
        Dim X, Z, Y
        Dim INodeFila, NodeCampo
        Dim ContCampos, NumFilas
        Dim Flag
        Dim arrNombre
        Dim Origen
        Dim TiempoCOM
        arrNombre = Split(Nombre, ":")

        Nombre = arrNombre(0)
        If UBound(arrNombre) > 0 Then
            Origen = arrNombre(1)
            If UBound(arrNombre) > 1 Then
                TiempoCOM = arrNombre(2)
            End If
        End If

        RsResp = Server.CreateObject("ADODB.RECORDSET")

        If Len(Trim(strXML)) = 0 Then
            'Response.Write("Hola")
            'Response.End
            Response.Redirect("ErrorNoHayData.asp")
        Else
            RecordXml = CreateObject("Microsoft.XMLDOM")
            RecordXml.loadXML(strXML)

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ' Agregrado por Richard
            ' OJO!!! Solo se debe activar para depuracion
            'if  RecordXml.firstChild is nothing then
            '	Call CreatePrueba (Nombre, strXML, ".xml")
            'end if
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            NumRecordsets = RecordXml.firstChild.childNodes.length
            INodeRecordset = RecordXml.firstChild.firstChild
            For X = 1 To NumRecordsets
                If UCase(INodeRecordset.baseName) = UCase(Nombre) Then
                    '***************************************
                    'Crea los campos para el Recordset
                    '***************************************
                    'Set NodeI = INodeRecordset

                    INodeFila = INodeRecordset.firstChild
                    If INodeRecordset.childNodes.length > 0 Then
                        NodeCampo = INodeFila.firstChild
                        ContCampos = INodeFila.childNodes.length
                        For Z = 1 To ContCampos
                            RsResp.Fields.Append(CStr(NodeCampo.baseName), 8) 'El Formato cadena es el # 8

                            NodeCampo = NodeCampo.nextSibling
                        Next
                        '***************************************
                        If RsResp.Fields.Count >= 1 Then
                            RsResp.Open()
                            Flag = "1"
                            '***********************************************
                            '**********GUARDA LOS VALORES DEL RS************
                            '***********************************************
                            NumFilas = INodeRecordset.childNodes.length
                            INodeFila = INodeRecordset.firstChild
                            For Y = 1 To NumFilas
                                RsResp.AddNew()
                                NodeCampo = INodeFila.firstChild
                                ContCampos = INodeFila.childNodes.length
                                For Z = 1 To ContCampos
                                    'RsResp(NodeCampo.baseName) = Replace(NodeCampo.Text, "^", "&")
                                    RsResp(Z - 1) = Replace(NodeCampo.Text, "^", "&")
                                    NodeCampo = NodeCampo.nextSibling
                                Next
                                INodeFila = INodeFila.nextSibling
                            Next
                        End If
                    End If
                    Exit For
                End If
                INodeRecordset = INodeRecordset.nextSibling
            Next
        End If

        If Flag = "1" Then
            RsResp.MoveFirst()
            XmlToRecordset = RsResp
        Else
            RsResp.Fields.Append("vacio", 8)
            RsResp.Open()
            XmlToRecordset = RsResp
        End If
        RecordXml = Nothing

        'Call CreatePrueba (year(date) & "-" & right("00" & month(Date),2) & "-" & Right("00" & day(Date),2) & " " & right("00" & hour(time), 2) & "-" & right("00" & minute(time),2) & "-" & right("00" & Second(time),2) & " -- " & Timer - Tiempo, "Tiempo en conversion XML " & Timer - Tiempo & vbcrlf & "Origen: " & Origen & vbcrlf & "Nombre: " & Nombre & vbcrlf & "Tiempo Respuesta COM+: " & TiempoCOM, ".txt")
    End Function

    'Public Function XmlToRecordset(strXML, Nombre)
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' Autor: Richard Carlo Rodriguez Balcazar
    ' Fecha: 13/07/2004
    ' Proposito: Crear un Recordset a partir de una cadena XML de la forma
    '       <DATASET_TAG>
    '           <DATATABLE_TAG>
    '           xxxxx;xxxxx;xxxxx;xxxxx|xxxxx;xxxxx;xxxxx;xxxxx|xxxxx;xxxxx;xxxxx;xxxxx
    '           </DATATABLE_TAG>
    '           <DATATABLE_TAG>
    '           xxxxx;xxxxx;xxxxx;xxxxx|xxxxx;xxxxx;xxxxx;xxxxx|xxxxx;xxxxx;xxxxx;xxxxx
    '           </DATATABLE_TAG>
    '           .
    '           .
    '           .
    '       </DATASET_TAG>
    ' Resultado: devuelve una cadena (String)
    '            o Nothing bajo cualquier otro circunstancia
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '    Dim XmlDoc
    '    Dim XmlGrupo
    '    Dim XmlRecordset
    '    Dim i, j, RecordsetIndex

    '    Set XmlDoc = Server.CreateObject("Microsoft.XMLDOM")
    '    Set XmlToRecordset = Nothing        ' Por defecto devolver Nothing

    '    Nombre = UCase(Trim(Nombre))

    '    If Len(Nombre) > 0 Then
    '        XmlDoc.loadXML (strXML)              ' Cargar texto XML
    '
    '        If XmlDoc.childNodes.length > 0 Then ' Si hay un Grupo
    '            Set XmlGrupo = XmlDoc.firstChild ' Grupo
    '
    '            RecordsetIndex = -1
    '            For i = 0 To XmlGrupo.childNodes.length - 1 ' Recorrer cada uno de los recordsets
    '                If UCase(XmlGrupo.childNodes(i).baseName) = Nombre Then
    '                    RecordsetIndex = i
    '                    Exit For
    '                End If
    '			Next

    '            If RecordsetIndex > -1 Then                 ' Si se encontro el recordset en el la cadena Xml
    '                Set XmlRecordset = XmlGrupo.childNodes(RecordsetIndex)
    '                If XmlRecordset.childNodes.length > 0 Then ' Si hay data en recordset
    '                    ' Determinar las filas
    '                    Dim Filas()
    '                    Filas = Split(XmlRecordset.childNodes(0).nodeValue, ROWSEPARATOR)

    '                    If UBound(Filas) > -1 Then
    '                        ' Crear los campos del Recordset
    '                        Set XmlToRecordset = Server.CreateObject("ADODB.RECORDSET")
    '                        XmlToRecordset.CursorLocation = adUseClient
    '                        XmlToRecordset.CursorType = adOpenStatic
    '                        XmlToRecordset.LockType = adLockOptimistic
    '
    ' Determinar la estructura de campos
    '                        Dim Campos()
    '                        Campos = Split(Filas(0), FIELDSEPARATOR) ' El primer registro guarda la estructura de campos
    '                        For i = 0 To UBound(Campos)
    '                            XmlToRecordset.Fields.Append Campos(i), adBSTR
    '                        Next

    '                        ' Cargar la data (la data esta desde la segunda fila en adelante)
    '                        XmlToRecordset.Open
    '                        For i = 1 To UBound(Filas)
    '                            XmlToRecordset.AddNew
    '                            Dim Valores()
    '                            Valores = Split(Filas(i), FIELDSEPARATOR)
    '                            For j = 0 To UBound(Valores)
    '                                XmlToRecordset.Fields(j).Value = FormatXMLToText(CSr(Valores(j)))
    '                            Next
    '                        Next
    '                        XmlToRecordset.MoveFirst
    '                    End If
    '                End If
    '            End If
    '        End If
    '    End If
    'End Function

    Function FormatXMLToText(ByVal Cadena)
        FormatXMLToText = UCase(Cadena)
        FormatXMLToText = Replace(FormatXMLToText, "^", "&")
        FormatXMLToText = Replace(FormatXMLToText, "~", "á")
        FormatXMLToText = Replace(FormatXMLToText, "}", "é")
        FormatXMLToText = Replace(FormatXMLToText, "{", "í")
        FormatXMLToText = Replace(FormatXMLToText, "#", "ú")
    End Function

    Function GetOutPutParams(ByVal rs)
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Autor: Richard Carlo Rodriguez Balcazar
        ' Fecha: 14/07/2004
        ' Proposito: Evalua un recordset resultado de una RFC en busca de
        '			 los valores devueltos (Exports)
        ' Resultado: devuelve un arreglo de valores devueltos
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim strOutPutParams
        strOutPutParams = ""
        If Not rs Is Nothing Then
            rs.Filter = rs.Fields(0).Name & "='X'"
            If rs.RecordCount > 0 Then
                rs.MoveFirst()
                Do While Not rs.EOF
                    strOutPutParams = strOutPutParams & rs.Fields(3) & ";"
                    rs.MoveNext()
                Loop
            End If
            rs.Filter = ""
        End If
        GetOutPutParams = Split(strOutPutParams, ";")
    End Function

    Function IsExistError(ByVal rs, ByVal ErrorFieldIndex)
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Autor: Richard Carlo Rodriguez Balcazar
        ' Fecha: 14/07/2004
        ' Proposito: Evalua un recordset resultado de una RFC en busca de
        '			 probables errores devueltos
        ' Resultado: devuelve una cadena (String) como descripcion del error
        '            o una cadena vacia en caso no hubiere errores
        '			 devuelve el mensaje "No hay respuesta de SAP" en caso de encontrar un recordset
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim bm
        Dim PosicionSalvada
        If Not rs Is Nothing Then
            If Not rs.eof And Not rs.bof Then
                bm = rs.Bookmark
                PosicionSalvada = True
            Else
                PosicionSalvada = False
            End If
            rs.Find(rs.Fields(0).Name & "='E'", , 1, 1)
            If rs.EOF Then
                IsExistError = ""
            Else
                IsExistError = rs.Fields(ErrorFieldIndex)
            End If
            If PosicionSalvada Then
                rs.Bookmark = bm
            End If
        Else
            IsExistError = "No hay respuesta de SAP"
        End If
    End Function

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '''''''''''''''Funciones para Implemetacion del Cache de tablas''''''''''''''''''''''''''
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Const TABLE_DISTRITO = "DISTRITO"
    Const TABLE_DEPARTAMENTO = "DEPARTAMENTO"
    Const TABLE_PROVINCIA = "PROVINCIA"

    Function GetTableCache(ByVal TableName)
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Autor: Richard Carlo Rodriguez Balcazar
        ' Fecha: 12/07/2004
        ' Proposito: Obtiene una tabla del cache de tablas, si la tabla no existe en el cache
        '            intenta obtenerla de la base de datos y almacenarla en el cache
        ' Resultado: devuelve un Recordset o Nothing bajo cualquier otra circunstancia
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim Fso
        Dim FileName
        TableName = UCase(Trim(TableName))      ' Convierte el nombre tabla a Mayusculas
        GetTableCache = Nothing                 ' Por defecto devuelve Nothing

        If Len(TableName) > 0 Then
            If IsTableCacheable(TableName) Then
                FileName = Request.ServerVariables("APPL_PHYSICAL_PATH") & "\Cache\" & TableName & ".xml"

                Fso = CreateObject("Scripting.FileSystemObject")
                If Fso.FileExists(FileName) Then
                    ' Si existe => recuperar la tabla del cache
                    GetTableCache = Server.CreateObject("ADODB.Recordset")
                    GetTableCache.Open(FileName)
                Else
                    ' Si no existe => recuperar la tabla de la base de datos
                    GetTableCache = GetTableDataBase(TableName)

                    If Not GetTableCache Is Nothing Then
                        ' Guardar Recordset en cache
                        GetTableCache.Save(FileName, 1)
                    End If
                End If
            End If
        End If
    End Function

    Function IsTableCacheable(ByVal TableName)
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Autor: Richard Carlo Rodriguez Balcazar
        ' Fecha: 12/07/2004
        ' Proposito: Verifica si una tabla puede ser guardada en el cache
        ' Resultado: devuelve un Recordset o Nothing bajo cualquier otra circunstancia
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        TableName = UCase(Trim(TableName))
        If TableName = TABLE_PROVINCIA Or _
      TableName = TABLE_DISTRITO Or _
      TableName = TABLE_DEPARTAMENTO Then
            IsTableCacheable = True
        Else
            IsTableCacheable = False
        End If
    End Function

    Function GetTableDataBase(ByVal TableName)
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Autor: Richard Carlo Rodriguez Balcazar
        ' Fecha: 12/07/2004
        ' Proposito: Obtiene una tabla de la base de datos.
        '            la tabla viene en formato XML por lo que se hace el llamado a la funcion
        '            XmlToRecordset para transformar el Xml a un Recordset
        ' Resultado: devuelve un Recordset o Nothing bajo cualquier otra circunstancia
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim objComponente
        Dim strXML

        GetTableDataBase = Nothing          ' Por defecto devuelve nothing

        Select Case TableName
            Case TABLE_PROVINCIA
                objComponente = Server.CreateObject("COM_PVU_General.SAPGeneral")
                strXML = objComponente.Get_LeeProvincia
            Case TABLE_DISTRITO
                objComponente = Server.CreateObject("COM_PVU_General.SAPGeneral")
                strXML = objComponente.Get_LeeDistrito
            Case TABLE_DEPARTAMENTO
                objComponente = Server.CreateObject("COM_PVU_General.SAPGeneral")
                strXML = objComponente.Get_LeeDepartamento
        End Select
        objComponente = Nothing

        If Len(Trim(strXML)) > 0 Then
            Select Case TableName
                Case TABLE_PROVINCIA : GetTableDataBase = XmlToRecordset(strXML, "RS")
                Case TABLE_DISTRITO : GetTableDataBase = XmlToRecordset(strXML, "RS")
                Case TABLE_DEPARTAMENTO : GetTableDataBase = XmlToRecordset(strXML, "RS")
            End Select
        End If
    End Function

    Function CreateObjectGeneral(ByVal Oficina)
        Dim objComponente

        Select Case Oficina
            Case "Ofic1"
                objComponente = Server.CreateObject("COM_PVU_General0009.SAPGeneral")
            Case "Ofic2"
                objComponente = Server.CreateObject("COM_PVU_General0006.SAPGeneral")
            Case "Ofic3"
                objComponente = Server.CreateObject("COM_PVU_General0008.SAPGeneral")
            Case "Ofic4"
                objComponente = Server.CreateObject("COM_PVU_General0007.SAPGeneral")
            Case "Ofic5"
                objComponente = Server.CreateObject("COM_PVU_General0011.SAPGeneral")
            Case "Ofic6"
                objComponente = Server.CreateObject("COM_PVU_General0097.SAPGeneral")
            Case "Ofic7"
                objComponente = Server.CreateObject("COM_PVU_General0013.SAPGeneral")
            Case "Ofic8"
                objComponente = Server.CreateObject("COM_PVU_General0017.SAPGeneral")
            Case "Ofic22"
                objComponente = Server.CreateObject("COM_PVU_GeneralRPPR.SAPGeneral")
            Case "Ofic23"
                objComponente = Server.CreateObject("COM_PVU_GeneralS023.SAPGeneral")
            Case "Ofic24"
                objComponente = Server.CreateObject("COM_PVU_GeneralS024.SAPGeneral")
            Case "Ofic25"
                objComponente = Server.CreateObject("COM_PVU_GeneralS025.SAPGeneral")
            Case "Ofic26"
                objComponente = Server.CreateObject("COM_PVU_GeneralS026.SAPGeneral")
            Case "Ofic27"
                objComponente = Server.CreateObject("COM_PVU_General0007.SAPGeneral")
            Case "Ofic28"
                objComponente = Server.CreateObject("COM_PVU_General0007.SAPGeneral")
            Case "Ofic29"
                objComponente = Server.CreateObject("COM_PVU_GeneralS030.SAPGeneral")
            Case "Ofic30"
                objComponente = Server.CreateObject("COM_PVU_GeneralS031.SAPGeneral")
            Case "Ofic31"
                objComponente = Server.CreateObject("COM_PVU_GeneralS032.SAPGeneral")
            Case "Ofic32"
                objComponente = Server.CreateObject("COM_PVU_GeneralS033.SAPGeneral")

            Case "Ofic10", "Ofic12", "Ofic15", "Ofic18"
                objComponente = Server.CreateObject("COM_PVU_GeneralFR12.SAPGeneral")
            Case "Ofic9", "Ofic11", "Ofic13", "Ofic21", "Ofic73"
                objComponente = Server.CreateObject("COM_PVU_GeneralFR13.SAPGeneral")
            Case "Ofic14", "Ofic16", "Ofic17", "Ofic19", "Ofic34", "ofic20"
                objComponente = Server.CreateObject("COM_PVU_GeneralFR14.SAPGeneral")
            Case "Ofic34", "Ofic35", "Ofic40", "Ofic44", "Ofic47", "Ofic50", "Ofic51", "Ofic52", "Ofic53", "Ofic54", "Ofic55", "Ofic56", "Ofic58", "Ofic60", "Ofic62", "Ofic63", "Ofic67", "Ofic69", "Ofic71", "Ofic72", "Ofic74"
                objComponente = Server.CreateObject("COM_PVU_GeneralFR15.SAPGeneral")
            Case "Ofic36", "Ofic37", "Ofic38", "Ofic41", "Ofic42", "Ofic43", "Ofic45", "Ofic46", "Ofic48", "Ofic49", "Ofic57", "Ofic59", "Ofic61", "Ofic64", "Ofic65", "Ofic66", "Ofic68", "Ofic70"
                objComponente = Server.CreateObject("COM_PVU_GeneralFR16.SAPGeneral")

            Case "Ofic39"
                objComponente = Server.CreateObject("COM_PVU_GeneralFR17.SAPGeneral")
            Case Else
                objComponente = Server.CreateObject("COM_PVU_General.SAPGeneral")
        End Select
        CreateObjectGeneral = objComponente
    End Function

    Function CreateObjectAdmin(ByVal Oficina)
        Dim objComponente
        Select Case Oficina
            Case "Ofic1"
                objComponente = Server.CreateObject("COM_PVU_Admin0009.SAPAdmin")
            Case "Ofic2"
                objComponente = Server.CreateObject("COM_PVU_Admin0006.SAPAdmin")
            Case "Ofic3"
                objComponente = Server.CreateObject("COM_PVU_Admin0008.SAPAdmin")
                'Case Ofic4
                '    objComponente = Server.CreateObject("COM_PVU_Admin0007.SAPAdmin")
                'Case Ofic5
                '    objComponente = Server.CreateObject("COM_PVU_Admin0011.SAPAdmin")
                'Case Ofic6
                '    objComponente = Server.CreateObject("COM_PVU_Admin0097.SAPAdmin")
                'Case Ofic7
                '    objComponente = Server.CreateObject("COM_PVU_Admin0013.SAPAdmin")
                'Case Ofic8
                '    objComponente = Server.CreateObject("COM_PVU_Admin0017.SAPAdmin")
                'Case Ofic22
                '    objComponente = Server.CreateObject("COM_PVU_AdminRPPR.SAPAdmin")
                'Case Ofic23
                '    objComponente = Server.CreateObject("COM_PVU_AdminS023.SAPAdmin")
                'Case Ofic24
                '    objComponente = Server.CreateObject("COM_PVU_AdminS024.SAPAdmin")
                'Case Ofic25
                '    objComponente = Server.CreateObject("COM_PVU_AdminS025.SAPAdmin")
                'Case Ofic26
                '    objComponente = Server.CreateObject("COM_PVU_AdminS026.SAPAdmin")
                'Case Ofic27
                '    objComponente = Server.CreateObject("COM_PVU_Admin0007.SAPAdmin")
                'Case Ofic28
                '    objComponente = Server.CreateObject("COM_PVU_AdminS029.SAPAdmin")
                'Case Ofic29
                '    objComponente = Server.CreateObject("COM_PVU_AdminS030.SAPAdmin")
                'Case Ofic30
                '    objComponente = Server.CreateObject("COM_PVU_AdminS031.SAPAdmin")
                'Case Ofic31
                '    objComponente = Server.CreateObject("COM_PVU_AdminS032.SAPAdmin")
                'Case Ofic32
                '    objComponente = Server.CreateObject("COM_PVU_AdminS033.SAPAdmin")

                'Case Ofic10, Ofic12, Ofic15, Ofic18
                '    objComponente = Server.CreateObject("COM_PVU_AdminFR12.SAPAdmin")
                'Case Ofic9, Ofic11, Ofic13, Ofic21, Ofic73
                '    objComponente = Server.CreateObject("COM_PVU_AdminFR13.SAPAdmin")
                'Case Ofic14, Ofic16, Ofic17, Ofic19, Ofic34, ofic20
                '    objComponente = Server.CreateObject("COM_PVU_AdminFR14.SAPAdmin")
                'Case Ofic34, Ofic35, Ofic40, Ofic44, Ofic47, Ofic50, Ofic51, Ofic52, Ofic53, Ofic54, Ofic55, Ofic56, Ofic58, Ofic60, Ofic62, Ofic63, Ofic67, Ofic69, Ofic71, Ofic72, Ofic74
                '    objComponente = Server.CreateObject("COM_PVU_AdminFR15.SAPAdmin")
                'Case Ofic36, Ofic37, Ofic38, Ofic41, Ofic42, Ofic43, Ofic45, Ofic46, Ofic48, Ofic49, Ofic57, Ofic59, Ofic61, Ofic64, Ofic65, Ofic66, Ofic68, Ofic70
                '    objComponente = Server.CreateObject("COM_PVU_AdminFR16.SAPAdmin")

                'Case Ofic39
                '    objComponente = Server.CreateObject("COM_PVU_AdminFR17.SAPAdmin")
            Case Else
                objComponente = Server.CreateObject("COM_PVU_Admin.SAPAdmin")
        End Select
        CreateObjectAdmin = objComponente
    End Function

    Function CreateObjectVenta(ByVal Oficina)
        Dim objComponente
        Select Case Oficina
            Case "Ofic1"
                objComponente = Server.CreateObject("COM_PVU_Venta0009.SAPVentas")
                'Case Ofic2
                '    objComponente = Server.CreateObject("COM_PVU_Venta0006.SAPVentas")
                'Case Ofic3
                '    objComponente = Server.CreateObject("COM_PVU_Venta0008.SAPVentas")
                'Case Ofic4
                '    objComponente = Server.CreateObject("COM_PVU_Venta0007.SAPVentas")
                '    'Set objComponente = Server.CreateObject("COM_PVU_Venta0008.SAPVentas")
                'Case Ofic5
                '    objComponente = Server.CreateObject("COM_PVU_Venta0011.SAPVentas")
                'Case Ofic6
                '    objComponente = Server.CreateObject("COM_PVU_Venta0097.SAPVentas")
                'Case Ofic7
                '    objComponente = Server.CreateObject("COM_PVU_Venta0013.SAPVentas")
                'Case Ofic8
                '    objComponente = Server.CreateObject("COM_PVU_Venta0017.SAPVentas")
                'Case Ofic22
                '    objComponente = Server.CreateObject("COM_PVU_VentaRPPR.SAPVentas")
                'Case Ofic23
                '    objComponente = Server.CreateObject("COM_PVU_VentaS023.SAPVentas")
                'Case Ofic24
                '    objComponente = Server.CreateObject("COM_PVU_VentaS024.SAPVentas")
                'Case Ofic25
                '    objComponente = Server.CreateObject("COM_PVU_VentaS025.SAPVentas")
                'Case Ofic26
                '    objComponente = Server.CreateObject("COM_PVU_VentaS026.SAPVentas")
                'Case Ofic27
                '    objComponente = Server.CreateObject("COM_PVU_Venta0007.SAPVentas")
                'Case Ofic28
                '    objComponente = Server.CreateObject("COM_PVU_Venta0007.SAPVentas")
                'Case Ofic29
                '    objComponente = Server.CreateObject("COM_PVU_VentaS030.SAPVentas")
                'Case Ofic30
                '    objComponente = Server.CreateObject("COM_PVU_VentaS031.SAPVentas")
                'Case Ofic31
                '    objComponente = Server.CreateObject("COM_PVU_VentaS032.SAPVentas")
                'Case Ofic32
                '    objComponente = Server.CreateObject("COM_PVU_VentaS033.SAPVentas")

                'Case Ofic10, Ofic12, Ofic15, Ofic18
                '    objComponente = Server.CreateObject("COM_PVU_VentaFR12.SAPVentas")
                'Case Ofic9, Ofic11, Ofic13, Ofic21, Ofic73
                '    objComponente = Server.CreateObject("COM_PVU_VentaFR13.SAPVentas")
                'Case Ofic14, Ofic16, Ofic17, Ofic19, Ofic34, ofic20
                '    objComponente = Server.CreateObject("COM_PVU_VentaFR14.SAPVentas")
                'Case Ofic34, Ofic35, Ofic40, Ofic44, Ofic47, Ofic50, Ofic51, Ofic52, Ofic53, Ofic54, Ofic55, Ofic56, Ofic58, Ofic60, Ofic62, Ofic63, Ofic67, Ofic69, Ofic71, Ofic72, Ofic74
                '    objComponente = Server.CreateObject("COM_PVU_VentaFR15.SAPVentas")
                'Case Ofic36, Ofic37, Ofic38, Ofic41, Ofic42, Ofic43, Ofic45, Ofic46, Ofic48, Ofic49, Ofic57, Ofic59, Ofic61, Ofic64, Ofic65, Ofic66, Ofic68, Ofic70
                '    objComponente = Server.CreateObject("COM_PVU_VentaFR16.SAPVentas")

                'Case Ofic39
                '    objComponente = Server.CreateObject("COM_PVU_VentaFR17.SAPVentas")
            Case Else
                objComponente = Server.CreateObject("COM_PVU_Venta.SAPVentas")
        End Select
        CreateObjectVenta = objComponente
    End Function

    Function CreateObjectImpresion(ByVal Oficina)
        Dim objComponente
        Select Case Oficina
            Case "Ofic1"
                objComponente = Server.CreateObject("COM_PVU_Impresion0009.SAPImpresion")
                'Case Ofic2
                '    objComponente = Server.CreateObject("COM_PVU_Impresion0006.SAPImpresion")
                'Case Ofic3
                '    objComponente = Server.CreateObject("COM_PVU_Impresion0008.SAPImpresion")
                'Case Ofic4
                '    objComponente = Server.CreateObject("COM_PVU_Impresion0007.SAPImpresion")
                'Case Ofic5
                '    objComponente = Server.CreateObject("COM_PVU_Impresion0011.SAPImpresion")
                'Case Ofic6
                '    objComponente = Server.CreateObject("COM_PVU_Impresion0097.SAPImpresion")
                'Case Ofic7
                '    objComponente = Server.CreateObject("COM_PVU_Impresion0013.SAPImpresion")
                'Case Ofic8
                '    objComponente = Server.CreateObject("COM_PVU_Impresion0017.SAPImpresion")
                'Case Ofic22
                '    objComponente = Server.CreateObject("COM_PVU_ImpresionRPPR.SAPImpresion")
                'Case Ofic23
                '    objComponente = Server.CreateObject("COM_PVU_ImpresionS023.SAPImpresion")
                'Case Ofic24
                '    objComponente = Server.CreateObject("COM_PVU_ImpresionS024.SAPImpresion")
                'Case Ofic25
                '    objComponente = Server.CreateObject("COM_PVU_ImpresionS025.SAPImpresion")
                'Case Ofic26
                '    objComponente = Server.CreateObject("COM_PVU_ImpresionS026.SAPImpresion")
                'Case Ofic27
                '    objComponente = Server.CreateObject("COM_PVU_Impresion0007.SAPImpresion")
                'Case Ofic28
                '    objComponente = Server.CreateObject("COM_PVU_Impresion0007.SAPImpresion")
                'Case Ofic29
                '    objComponente = Server.CreateObject("COM_PVU_ImpresionS030.SAPImpresion")
                'Case Ofic30
                '    objComponente = Server.CreateObject("COM_PVU_ImpresionS031.SAPImpresion")
                'Case Ofic31
                '    objComponente = Server.CreateObject("COM_PVU_ImpresionS032.SAPImpresion")
                'Case Ofic32
                '    objComponente = Server.CreateObject("COM_PVU_ImpresionS033.SAPImpresion")

                'Case Ofic10, Ofic12, Ofic15, Ofic18
                '    objComponente = Server.CreateObject("COM_PVU_ImpresionFR12.SAPImpresion")
                'Case Ofic9, Ofic11, Ofic13, Ofic21, Ofic73
                '    objComponente = Server.CreateObject("COM_PVU_ImpresionFR13.SAPImpresion")
                'Case Ofic14, Ofic16, Ofic17, Ofic19, Ofic34, ofic20
                '    objComponente = Server.CreateObject("COM_PVU_ImpresionFR14.SAPImpresion")    '14
                'Case Ofic34, Ofic35, Ofic40, Ofic44, Ofic47, Ofic50, Ofic51, Ofic52, Ofic53, Ofic54, Ofic55, Ofic56, Ofic58, Ofic60, Ofic62, Ofic63, Ofic67, Ofic69, Ofic71, Ofic72, Ofic74
                '    objComponente = Server.CreateObject("COM_PVU_ImpresionFR15.SAPImpresion")
                'Case Ofic36, Ofic37, Ofic38, Ofic41, Ofic42, Ofic43, Ofic45, Ofic46, Ofic48, Ofic49, Ofic57, Ofic59, Ofic61, Ofic64, Ofic65, Ofic66, Ofic68, Ofic70
                '    objComponente = Server.CreateObject("COM_PVU_ImpresionFR16.SAPImpresion")

                'Case Ofic39
                '    objComponente = Server.CreateObject("COM_PVU_ImpresionFR17.SAPImpresion")
            Case Else
                objComponente = Server.CreateObject("COM_PVU_Impresion.SAPImpresion")
        End Select
        CreateObjectImpresion = objComponente
    End Function

    Function CreateObjectAcuerdoPCS(ByVal Oficina)
        Dim objComponente
        Select Case Oficina
            Case "Ofic1"
                objComponente = Server.CreateObject("COM_PVU_AcuerdoPCS0009.SAPAcuerdoPCS")
                'Case Ofic2
                '    objComponente = Server.CreateObject("COM_PVU_AcuerdoPCS0006.SAPAcuerdoPCS")
                'Case Ofic3
                '    objComponente = Server.CreateObject("COM_PVU_AcuerdoPCS0008.SAPAcuerdoPCS")
                'Case Ofic4
                '    objComponente = Server.CreateObject("COM_PVU_AcuerdoPCS0007.SAPAcuerdoPCS")
                'Case Ofic5
                '    objComponente = Server.CreateObject("COM_PVU_AcuerdoPCS0011.SAPAcuerdoPCS")
                'Case Ofic6
                '    objComponente = Server.CreateObject("COM_PVU_AcuerdoPCS0097.SAPAcuerdoPCS")
                'Case Ofic7
                '    objComponente = Server.CreateObject("COM_PVU_AcuerdoPCS0013.SAPAcuerdoPCS")
                'Case Ofic8
                '    objComponente = Server.CreateObject("COM_PVU_AcuerdoPCS0017.SAPAcuerdoPCS")
                'Case Ofic22
                '    objComponente = Server.CreateObject("COM_PVU_AcuerdoPCSRPPR.SAPAcuerdoPCS")
                'Case Ofic23
                '    objComponente = Server.CreateObject("COM_PVU_AcuerdoPCSS023.SAPAcuerdoPCS")
                'Case Ofic24
                '    objComponente = Server.CreateObject("COM_PVU_AcuerdoPCSS024.SAPAcuerdoPCS")
                'Case Ofic25
                '    objComponente = Server.CreateObject("COM_PVU_AcuerdoPCSS025.SAPAcuerdoPCS")
                'Case Ofic26
                '    objComponente = Server.CreateObject("COM_PVU_AcuerdoPCSS026.SAPAcuerdoPCS")
                'Case Ofic27
                '    objComponente = Server.CreateObject("COM_PVU_AcuerdoPCS0007.SAPAcuerdoPCS")
                'Case Ofic28
                '    objComponente = Server.CreateObject("COM_PVU_AcuerdoPCSS029.SAPAcuerdoPCS")
                'Case Ofic29
                '    objComponente = Server.CreateObject("COM_PVU_AcuerdoPCSS030.SAPAcuerdoPCS")
                'Case Ofic30
                '    objComponente = Server.CreateObject("COM_PVU_AcuerdoPCSS031.SAPAcuerdoPCS")
                'Case Ofic31
                '    objComponente = Server.CreateObject("COM_PVU_AcuerdoPCSS032.SAPAcuerdoPCS")
                'Case Ofic32
                '    objComponente = Server.CreateObject("COM_PVU_AcuerdoPCSS033.SAPAcuerdoPCS")

                'Case Ofic10, Ofic12, Ofic15, Ofic18
                '    objComponente = Server.CreateObject("COM_PVU_AcuerdoPCSFR12.SAPAcuerdoPCS")
                'Case Ofic9, Ofic11, Ofic13, Ofic21, Ofic73
                '    objComponente = Server.CreateObject("COM_PVU_AcuerdoPCSFR13.SAPAcuerdoPCS")
                'Case Ofic14, Ofic16, Ofic17, Ofic19, Ofic34, ofic20
                '    objComponente = Server.CreateObject("COM_PVU_AcuerdoPCSFR14.SAPAcuerdoPCS")
                'Case Ofic34, Ofic35, Ofic40, Ofic44, Ofic47, Ofic50, Ofic51, Ofic52, Ofic53, Ofic54, Ofic55, Ofic56, Ofic58, Ofic60, Ofic62, Ofic63, Ofic67, Ofic69, Ofic71, Ofic72, Ofic74
                '    objComponente = Server.CreateObject("COM_PVU_AcuerdoPCSFR15.SAPAcuerdoPCS")
                'Case Ofic36, Ofic37, Ofic38, Ofic41, Ofic42, Ofic43, Ofic45, Ofic46, Ofic48, Ofic49, Ofic57, Ofic59, Ofic61, Ofic64, Ofic65, Ofic66, Ofic68, Ofic70
                '    objComponente = Server.CreateObject("COM_PVU_AcuerdoPCSFR16.SAPAcuerdoPCS")
                'Case Ofic39
                '    objComponente = Server.CreateObject("COM_PVU_AcuerdoPCSFR17.SAPAcuerdoPCS")
            Case Else
                objComponente = Server.CreateObject("COM_PVU_AcuerdoPCS.SAPAcuerdoPCS")
        End Select
        CreateObjectAcuerdoPCS = objComponente
    End Function


    '********************************************************************************************
    '********************************************************************************************
    '***********************************Agregado el 06/08/2004***********************************
    '********************************************************************************************
    '********************************************************************************************

    Function CreateObjectCliente(ByVal Oficina)
        Dim objComponente
        Select Case Oficina
            Case "Ofic1"
                objComponente = Server.CreateObject("COM_PVU_Cliente0009.SAPCliente")
                'Case Ofic2
                '    objComponente = Server.CreateObject("COM_PVU_Cliente0006.SAPCliente")
                'Case Ofic3
                '    objComponente = Server.CreateObject("COM_PVU_Cliente0008.SAPCliente")
                'Case Ofic4
                '    objComponente = Server.CreateObject("COM_PVU_Cliente0007.SAPCliente")
                'Case Ofic5
                '    objComponente = Server.CreateObject("COM_PVU_Cliente0011.SAPCliente")
                'Case Ofic6
                '    objComponente = Server.CreateObject("COM_PVU_Cliente0097.SAPCliente")
                'Case Ofic7
                '    objComponente = Server.CreateObject("COM_PVU_Cliente0013.SAPCliente")
                'Case Ofic8
                '    objComponente = Server.CreateObject("COM_PVU_Cliente0017.SAPCliente")
                'Case Ofic22
                '    objComponente = Server.CreateObject("COM_PVU_ClienteRPPR.SAPCliente")
                'Case Ofic23
                '    objComponente = Server.CreateObject("COM_PVU_ClienteS023.SAPCliente")
                'Case Ofic24
                '    objComponente = Server.CreateObject("COM_PVU_ClienteS024.SAPCliente")
                'Case Ofic25
                '    objComponente = Server.CreateObject("COM_PVU_ClienteS025.SAPCliente")
                'Case Ofic26
                '    objComponente = Server.CreateObject("COM_PVU_ClienteS026.SAPCliente")
                'Case Ofic27
                '    objComponente = Server.CreateObject("COM_PVU_Cliente0007.SAPCliente")
                'Case Ofic28
                '    objComponente = Server.CreateObject("COM_PVU_Cliente0007.SAPCliente")
                'Case Ofic29
                '    objComponente = Server.CreateObject("COM_PVU_ClienteS030.SAPCliente")
                'Case Ofic30
                '    objComponente = Server.CreateObject("COM_PVU_ClienteS031.SAPCliente")
                'Case Ofic31
                '    objComponente = Server.CreateObject("COM_PVU_ClienteS032.SAPCliente")
                'Case Ofic32
                '    objComponente = Server.CreateObject("COM_PVU_ClienteS033.SAPCliente")

                'Case Ofic10, Ofic12, Ofic15, Ofic18
                '    objComponente = Server.CreateObject("COM_PVU_ClienteFR12.SAPCliente")
                'Case Ofic9, Ofic11, Ofic13, Ofic21, Ofic73
                '    objComponente = Server.CreateObject("COM_PVU_ClienteFR13.SAPCliente")
                'Case Ofic14, Ofic16, Ofic17, Ofic19, Ofic34, ofic20
                '    objComponente = Server.CreateObject("COM_PVU_ClienteFR14.SAPCliente")
                'Case Ofic34, Ofic35, Ofic40, Ofic44, Ofic47, Ofic50, Ofic51, Ofic52, Ofic53, Ofic54, Ofic55, Ofic56, Ofic58, Ofic60, Ofic62, Ofic63, Ofic67, Ofic69, Ofic71, Ofic72, Ofic74
                '    objComponente = Server.CreateObject("COM_PVU_ClienteFR15.SAPCliente")
                'Case Ofic36, Ofic37, Ofic38, Ofic41, Ofic42, Ofic43, Ofic45, Ofic46, Ofic48, Ofic49, Ofic57, Ofic59, Ofic61, Ofic64, Ofic65, Ofic66, Ofic68, Ofic70
                '    objComponente = Server.CreateObject("COM_PVU_ClienteFR16.SAPCliente")
                'Case Ofic39
                '    objComponente = Server.CreateObject("COM_PVU_ClienteFR17.SAPCliente")
            Case Else
                objComponente = Server.CreateObject("COM_PVU_Cliente.SAPCliente")
        End Select
        CreateObjectCliente = objComponente
    End Function

    Function CreateObjectPagos(ByVal Oficina)
        Dim objComponente
        Select Case Oficina
            Case "Ofic1"
                objComponente = Server.CreateObject("COM_PVU_Pagos0009.SAPPagos")
                'Case Ofic2
                '    objComponente = Server.CreateObject("COM_PVU_Pagos0006.SAPPagos")
                'Case Ofic3
                '    objComponente = Server.CreateObject("COM_PVU_Pagos0008.SAPPagos")
                'Case Ofic4
                '    objComponente = Server.CreateObject("COM_PVU_Pagos0007.SAPPagos")
                'Case Ofic5
                '    objComponente = Server.CreateObject("COM_PVU_Pagos0011.SAPPagos")
                'Case Ofic6
                '    objComponente = Server.CreateObject("COM_PVU_Pagos0097.SAPPagos")
                'Case Ofic7
                '    objComponente = Server.CreateObject("COM_PVU_Pagos0013.SAPPagos")
                'Case Ofic8
                '    objComponente = Server.CreateObject("COM_PVU_Pagos0017.SAPPagos")
                'Case Ofic22
                '    objComponente = Server.CreateObject("COM_PVU_PagosRPPR.SAPPagos")
                'Case Ofic23
                '    objComponente = Server.CreateObject("COM_PVU_PagosS023.SAPPagos")
                'Case Ofic24
                '    objComponente = Server.CreateObject("COM_PVU_PagosS024.SAPPagos")
                'Case Ofic25
                '    objComponente = Server.CreateObject("COM_PVU_PagosS025.SAPPagos")
                'Case Ofic26
                '    objComponente = Server.CreateObject("COM_PVU_PagosS026.SAPPagos")
                'Case Ofic27
                '    objComponente = Server.CreateObject("COM_PVU_Pagos0007.SAPPagos")
                'Case Ofic28
                '    objComponente = Server.CreateObject("COM_PVU_Pagos0007.SAPPagos")
                'Case Ofic29
                '    objComponente = Server.CreateObject("COM_PVU_PagosS030.SAPPagos")
                'Case Ofic30
                '    objComponente = Server.CreateObject("COM_PVU_PagosS031.SAPPagos")
                'Case Ofic31
                '    objComponente = Server.CreateObject("COM_PVU_PagosS032.SAPPagos")
                'Case Ofic32
                '    objComponente = Server.CreateObject("COM_PVU_PagosS033.SAPPagos")

                'Case Ofic10, Ofic12, Ofic15, Ofic18
                '    objComponente = Server.CreateObject("COM_PVU_PagosFR12.SAPPagos")
                'Case Ofic9, Ofic11, Ofic13, Ofic21, Ofic73
                '    objComponente = Server.CreateObject("COM_PVU_PagosFR13.SAPPagos")
                'Case Ofic14, Ofic16, Ofic17, Ofic19, Ofic34, ofic20
                '    objComponente = Server.CreateObject("COM_PVU_PagosFR14.SAPPagos")
                'Case Ofic34, Ofic35, Ofic40, Ofic44, Ofic47, Ofic50, Ofic51, Ofic52, Ofic53, Ofic54, Ofic55, Ofic56, Ofic58, Ofic60, Ofic62, Ofic63, Ofic67, Ofic69, Ofic71, Ofic72, Ofic74
                '    objComponente = Server.CreateObject("COM_PVU_PagosFR15.SAPPagos")
                'Case Ofic36, Ofic37, Ofic38, Ofic41, Ofic42, Ofic43, Ofic45, Ofic46, Ofic48, Ofic49, Ofic57, Ofic59, Ofic61, Ofic64, Ofic65, Ofic66, Ofic68, Ofic70
                '    objComponente = Server.CreateObject("COM_PVU_PagosFR16.SAPPagos")
                'Case Ofic39
                '    objComponente = Server.CreateObject("COM_PVU_PagosFR17.SAPPagos")

            Case Else
                objComponente = Server.CreateObject("COM_PVU_Pagos.SAPPagos")
        End Select
        CreateObjectPagos = objComponente
    End Function

    Function CreateObjectComprobante(ByVal Oficina)
        Dim objComponente
        Select Case Oficina
            Case "Ofic1"
                objComponente = Server.CreateObject("COM_PVU_Comprob0009.SAPComprobante")
                'Case Ofic2
                '    objComponente = Server.CreateObject("COM_PVU_Comprob0006.SAPComprobante")
                'Case Ofic3
                '    objComponente = Server.CreateObject("COM_PVU_Comprob0008.SAPComprobante")
                'Case Ofic4
                '    objComponente = Server.CreateObject("COM_PVU_Comprob0007.SAPComprobante")
                'Case Ofic5
                '    objComponente = Server.CreateObject("COM_PVU_Comprob0011.SAPComprobante")
                'Case Ofic6
                '    objComponente = Server.CreateObject("COM_PVU_Comprob0097.SAPComprobante")
                'Case Ofic7
                '    objComponente = Server.CreateObject("COM_PVU_Comprob0013.SAPComprobante")
                'Case Ofic8
                '    objComponente = Server.CreateObject("COM_PVU_Comprob0017.SAPComprobante")
                'Case Ofic22
                '    objComponente = Server.CreateObject("COM_PVU_ComprobRPPR.SAPComprobante")
                'Case Ofic23
                '    objComponente = Server.CreateObject("COM_PVU_ComprobS023.SAPComprobante")
                'Case Ofic24
                '    objComponente = Server.CreateObject("COM_PVU_ComprobS024.SAPComprobante")
                'Case Ofic25
                '    objComponente = Server.CreateObject("COM_PVU_ComprobS025.SAPComprobante")
                'Case Ofic26
                '    objComponente = Server.CreateObject("COM_PVU_ComprobS026.SAPComprobante")
                'Case Ofic27
                '    objComponente = Server.CreateObject("COM_PVU_Comprob0007.SAPComprobante")
                'Case Ofic28
                '    objComponente = Server.CreateObject("COM_PVU_Comprob0007.SAPComprobante")
                'Case Ofic29
                '    objComponente = Server.CreateObject("COM_PVU_ComprobS030.SAPComprobante")
                'Case Ofic30
                '    objComponente = Server.CreateObject("COM_PVU_ComprobS031.SAPComprobante")
                'Case Ofic31
                '    objComponente = Server.CreateObject("COM_PVU_ComprobS032.SAPComprobante")
                'Case Ofic32
                '    objComponente = Server.CreateObject("COM_PVU_ComprobS033.SAPComprobante")

                'Case Ofic10, Ofic12, Ofic15, Ofic18
                '    objComponente = Server.CreateObject("COM_PVU_ComprobFR12.SAPComprobante")
                'Case Ofic9, Ofic11, Ofic13, Ofic21, Ofic73
                '    objComponente = Server.CreateObject("COM_PVU_ComprobFR13.SAPComprobante")
                'Case Ofic14, Ofic16, Ofic17, Ofic19, Ofic34, ofic20
                '    objComponente = Server.CreateObject("COM_PVU_ComprobFR14.SAPComprobante")
                'Case Ofic34, Ofic35, Ofic40, Ofic44, Ofic47, Ofic50, Ofic51, Ofic52, Ofic53, Ofic54, Ofic55, Ofic56, Ofic58, Ofic60, Ofic62, Ofic63, Ofic67, Ofic69, Ofic71, Ofic72, Ofic74
                '    objComponente = Server.CreateObject("COM_PVU_ComprobFR15.SAPComprobante")
                'Case Ofic36, Ofic37, Ofic38, Ofic41, Ofic42, Ofic43, Ofic45, Ofic46, Ofic48, Ofic49, Ofic57, Ofic59, Ofic61, Ofic64, Ofic65, Ofic66, Ofic68, Ofic70
                '    objComponente = Server.CreateObject("COM_PVU_ComprobFR16.SAPComprobante")
                'Case Ofic39
                '    objComponente = Server.CreateObject("COM_PVU_ComprobFR17.SAPComprobante")

            Case Else
                objComponente = Server.CreateObject("COM_PVU_Comprobante.SAPComprobante")
        End Select
        CreateObjectComprobante = objComponente
    End Function


    Function CreateObjectConsultaInv(ByVal Oficina)
        Dim objComponente
        Select Case Oficina
            Case "Ofic1"
                objComponente = Server.CreateObject("COM_PVU_ConsInv0009.SAPConsultaInv")
                'Case Ofic2
                '    objComponente = Server.CreateObject("COM_PVU_ConsInv0006.SAPConsultaInv")
                'Case Ofic3
                '    objComponente = Server.CreateObject("COM_PVU_ConsInv0008.SAPConsultaInv")
                'Case Ofic4
                '    objComponente = Server.CreateObject("COM_PVU_ConsInv0007.SAPConsultaInv")
                'Case Ofic5
                '    objComponente = Server.CreateObject("COM_PVU_ConsInv0011.SAPConsultaInv")
                'Case Ofic6
                '    objComponente = Server.CreateObject("COM_PVU_ConsInv0097.SAPConsultaInv")
                'Case Ofic7
                '    objComponente = Server.CreateObject("COM_PVU_ConsInv0013.SAPConsultaInv")
                'Case Ofic8
                '    objComponente = Server.CreateObject("COM_PVU_ConsInv0017.SAPConsultaInv")
                'Case Ofic22
                '    objComponente = Server.CreateObject("COM_PVU_ConsInvRPPR.SAPConsultaInv")
                'Case Ofic23
                '    objComponente = Server.CreateObject("COM_PVU_ConsInvS023.SAPConsultaInv")
                'Case Ofic24
                '    objComponente = Server.CreateObject("COM_PVU_ConsInvS024.SAPConsultaInv")
                'Case Ofic25
                '    objComponente = Server.CreateObject("COM_PVU_ConsInvS025.SAPConsultaInv")
                'Case Ofic26
                '    objComponente = Server.CreateObject("COM_PVU_ConsInvS026.SAPConsultaInv")
                'Case Ofic27
                '    objComponente = Server.CreateObject("COM_PVU_ConsInv0007.SAPConsultaInv")
                'Case Ofic28
                '    objComponente = Server.CreateObject("COM_PVU_ConsInv0007.SAPConsultaInv")
                'Case Ofic29
                '    objComponente = Server.CreateObject("COM_PVU_ConsInvS030.SAPConsultaInv")
                'Case Ofic30
                '    objComponente = Server.CreateObject("COM_PVU_ConsInvS031.SAPConsultaInv")
                'Case Ofic31
                '    objComponente = Server.CreateObject("COM_PVU_ConsInvS032.SAPConsultaInv")
                'Case Ofic32
                '    objComponente = Server.CreateObject("COM_PVU_ConsInvS033.SAPConsultaInv")

                'Case Ofic10, Ofic12, Ofic15, Ofic18
                '    objComponente = Server.CreateObject("COM_PVU_ConsInvFR12.SAPConsultaInv")
                'Case Ofic9, Ofic11, Ofic13, Ofic21, Ofic73
                '    objComponente = Server.CreateObject("COM_PVU_ConsInvFR13.SAPConsultaInv")
                'Case Ofic14, Ofic16, Ofic17, Ofic19, Ofic34, ofic20
                '    objComponente = Server.CreateObject("COM_PVU_ConsInvFR14.SAPConsultaInv")
                'Case Ofic34, Ofic35, Ofic40, Ofic44, Ofic47, Ofic50, Ofic51, Ofic52, Ofic53, Ofic54, Ofic55, Ofic56, Ofic58, Ofic60, Ofic62, Ofic63, Ofic67, Ofic69, Ofic71, Ofic72, Ofic74
                '    objComponente = Server.CreateObject("COM_PVU_ConsInvFR15.SAPConsultaInv")
                'Case Ofic36, Ofic37, Ofic38, Ofic41, Ofic42, Ofic43, Ofic45, Ofic46, Ofic48, Ofic49, Ofic57, Ofic59, Ofic61, Ofic64, Ofic65, Ofic66, Ofic68, Ofic70
                '    objComponente = Server.CreateObject("COM_PVU_ConsInvFR16.SAPConsultaInv")
                'Case Ofic39
                '    objComponente = Server.CreateObject("COM_PVU_ConsInvFR17.SAPConsultaInv")
            Case Else
                objComponente = Server.CreateObject("COM_PVU_ConsultaInv.SAPConsultaInv")
        End Select
        CreateObjectConsultaInv = objComponente
    End Function

    Function CreateObjectConsultaIMEI(ByVal Oficina)
        Dim objComponente
        Select Case Oficina
            Case "Ofic1"
                objComponente = Server.CreateObject("COM_PVU_ConsIMEI0009.SAPConsultaIMEI")
                'Case Ofic2
                '    objComponente = Server.CreateObject("COM_PVU_ConsIMEI0006.SAPConsultaIMEI")
                'Case Ofic3
                '    objComponente = Server.CreateObject("COM_PVU_ConsIMEI0008.SAPConsultaIMEI")
                'Case Ofic4
                '    objComponente = Server.CreateObject("COM_PVU_ConsIMEI0007.SAPConsultaIMEI")
                'Case Ofic5
                '    objComponente = Server.CreateObject("COM_PVU_ConsIMEI0011.SAPConsultaIMEI")
                'Case Ofic6
                '    objComponente = Server.CreateObject("COM_PVU_ConsIMEI0097.SAPConsultaIMEI")
                'Case Ofic7
                '    objComponente = Server.CreateObject("COM_PVU_ConsIMEI0013.SAPConsultaIMEI")
                'Case Ofic8
                '    objComponente = Server.CreateObject("COM_PVU_ConsIMEI0017.SAPConsultaIMEI")
                'Case Ofic22
                '    objComponente = Server.CreateObject("COM_PVU_ConsIMEIRPPR.SAPConsultaIMEI")
                'Case Ofic23
                '    objComponente = Server.CreateObject("COM_PVU_ConsIMEIS023.SAPConsultaIMEI")
                'Case Ofic24
                '    objComponente = Server.CreateObject("COM_PVU_ConsIMEIS024.SAPConsultaIMEI")
                'Case Ofic25
                '    objComponente = Server.CreateObject("COM_PVU_ConsIMEIS025.SAPConsultaIMEI")
                'Case Ofic26
                '    objComponente = Server.CreateObject("COM_PVU_ConsIMEIS026.SAPConsultaIMEI")
                'Case Ofic27
                '    objComponente = Server.CreateObject("COM_PVU_ConsIMEI0007.SAPConsultaIMEI")
                'Case Ofic28
                '    objComponente = Server.CreateObject("COM_PVU_ConsIMEI0007.SAPConsultaIMEI")
                'Case Ofic29
                '    objComponente = Server.CreateObject("COM_PVU_ConsIMEIS030.SAPConsultaIMEI")
                'Case Ofic30
                '    objComponente = Server.CreateObject("COM_PVU_ConsIMEIS031.SAPConsultaIMEI")
                'Case Ofic31
                '    objComponente = Server.CreateObject("COM_PVU_ConsIMEIS032.SAPConsultaIMEI")
                'Case Ofic32
                '    objComponente = Server.CreateObject("COM_PVU_ConsIMEIS033.SAPConsultaIMEI")

                'Case Ofic10, Ofic12, Ofic15, Ofic18
                '    objComponente = Server.CreateObject("COM_PVU_ConsIMEIFR12.SAPConsultaIMEI")
                'Case Ofic9, Ofic11, Ofic13, Ofic21, Ofic73
                '    objComponente = Server.CreateObject("COM_PVU_ConsIMEIFR13.SAPConsultaIMEI")
                'Case Ofic14, Ofic16, Ofic17, Ofic19, Ofic34, ofic20
                '    objComponente = Server.CreateObject("COM_PVU_ConsIMEIFR14.SAPConsultaIMEI")
                'Case Ofic34, Ofic35, Ofic40, Ofic44, Ofic47, Ofic50, Ofic51, Ofic52, Ofic53, Ofic54, Ofic55, Ofic56, Ofic58, Ofic60, Ofic62, Ofic63, Ofic67, Ofic69, Ofic71, Ofic72, Ofic74
                '    objComponente = Server.CreateObject("COM_PVU_ConsIMEIFR15.SAPConsultaIMEI")
                'Case Ofic36, Ofic37, Ofic38, Ofic41, Ofic42, Ofic43, Ofic45, Ofic46, Ofic48, Ofic49, Ofic57, Ofic59, Ofic61, Ofic64, Ofic65, Ofic66, Ofic68, Ofic70
                '    objComponente = Server.CreateObject("COM_PVU_ConsIMEIFR16.SAPConsultaIMEI")

                'Case Ofic39
                '    objComponente = Server.CreateObject("COM_PVU_ConsIMEIFR17.SAPConsultaIMEI")
            Case Else
                objComponente = Server.CreateObject("COM_PVU_ConsultaIMEI.SAPConsultaIMEI")
        End Select
        CreateObjectConsultaIMEI = objComponente
    End Function


    Function CreateObjectCuadreCaja(ByVal Oficina)
        Dim objComponente
        Select Case Oficina
            'Case "Ofic1"
            '   objComponente = Server.CreateObject("COM_PVU_CuadreCaja0009.SAPCuadreCaja")
        Case Ofic2
                objComponente = Server.CreateObject("COM_PVU_CuadreCaja0006.SAPCuadreCaja")
                'Case Ofic3
                '    objComponente = Server.CreateObject("COM_PVU_CuadreCaja0008.SAPCuadreCaja")
                'Case Ofic4
                '    objComponente = Server.CreateObject("COM_PVU_CuadreCaja0007.SAPCuadreCaja")
                'Case Ofic5
                '    objComponente = Server.CreateObject("COM_PVU_CuadreCaja0011.SAPCuadreCaja")
                'Case Ofic6
                '    objComponente = Server.CreateObject("COM_PVU_CuadreCaja0097.SAPCuadreCaja")
                'Case Ofic7
                '    objComponente = Server.CreateObject("COM_PVU_CuadreCaja0013.SAPCuadreCaja")
                'Case Ofic8
                '    objComponente = Server.CreateObject("COM_PVU_CuadreCaja0017.SAPCuadreCaja")
                'Case Ofic22
                '    objComponente = Server.CreateObject("COM_PVU_CuadreCajaRPPR.SAPCuadreCaja")
                'Case Ofic23
                '    objComponente = Server.CreateObject("COM_PVU_CuadreCajaS023.SAPCuadreCaja")
                'Case Ofic24
                '    objComponente = Server.CreateObject("COM_PVU_CuadreCajaS024.SAPCuadreCaja")
                'Case Ofic25
                '    objComponente = Server.CreateObject("COM_PVU_CuadreCajaS025.SAPCuadreCaja")
                'Case Ofic26
                '    objComponente = Server.CreateObject("COM_PVU_CuadreCajaS026.SAPCuadreCaja")
                'Case Ofic27
                '    objComponente = Server.CreateObject("COM_PVU_CuadreCaja0007.SAPCuadreCaja")
                'Case Ofic28
                '    objComponente = Server.CreateObject("COM_PVU_CuadreCaja0007.SAPCuadreCaja")
                'Case Ofic29
                '    objComponente = Server.CreateObject("COM_PVU_CuadreCajaS030.SAPCuadreCaja")
                'Case Ofic30
                '    objComponente = Server.CreateObject("COM_PVU_CuadreCajaS031.SAPCuadreCaja")
                'Case Ofic31
                '    objComponente = Server.CreateObject("COM_PVU_CuadreCajaS032.SAPCuadreCaja")
                'Case Ofic32
                '    objComponente = Server.CreateObject("COM_PVU_CuadreCajaS033.SAPCuadreCaja")

                'Case Ofic10, Ofic12, Ofic15, Ofic18
                '    objComponente = Server.CreateObject("COM_PVU_CuadreCajaFR12.SAPCuadreCaja")
                'Case Ofic9, Ofic11, Ofic13, Ofic21, Ofic73
                '    objComponente = Server.CreateObject("COM_PVU_CuadreCajaFR13.SAPCuadreCaja")
                'Case Ofic14, Ofic16, Ofic17, Ofic19, Ofic34, ofic20
                '    objComponente = Server.CreateObject("COM_PVU_CuadreCajaFR14.SAPCuadreCaja")
                'Case Ofic34, Ofic35, Ofic40, Ofic44, Ofic47, Ofic50, Ofic51, Ofic52, Ofic53, Ofic54, Ofic55, Ofic56, Ofic58, Ofic60, Ofic62, Ofic63, Ofic67, Ofic69, Ofic71, Ofic72, Ofic74
                '    objComponente = Server.CreateObject("COM_PVU_CuadreCajaFR15.SAPCuadreCaja")
                'Case Ofic36, Ofic37, Ofic38, Ofic41, Ofic42, Ofic43, Ofic45, Ofic46, Ofic48, Ofic49, Ofic57, Ofic59, Ofic61, Ofic64, Ofic65, Ofic66, Ofic68, Ofic70
                '    objComponente = Server.CreateObject("COM_PVU_CuadreCajaFR16.SAPCuadreCaja")
                'Case Ofic39
                '    objComponente = Server.CreateObject("COM_PVU_CuadreCajaFR17.SAPCuadreCaja")
            Case Else
                objComponente = Server.CreateObject("COM_PVU_CuadreCaja.SAPCuadreCaja")
        End Select
        CreateObjectCuadreCaja = objComponente
    End Function

    Function CreateObjectDGarant(ByVal Oficina)
        Dim objComponente
        Select Case Oficina
            Case "Ofic1"
                objComponente = Server.CreateObject("COM_PVU_DGarant0009.SAPDGarant")
                'Case Ofic2
                '    objComponente = Server.CreateObject("COM_PVU_DGarant0006.SAPDGarant")
                'Case Ofic3
                '    objComponente = Server.CreateObject("COM_PVU_DGarant0008.SAPDGarant")
                'Case Ofic4
                '    objComponente = Server.CreateObject("COM_PVU_DGarant0007.SAPDGarant")
                'Case Ofic5
                '    objComponente = Server.CreateObject("COM_PVU_DGarant0011.SAPDGarant")
                'Case Ofic6
                '    objComponente = Server.CreateObject("COM_PVU_DGarant0097.SAPDGarant")
                'Case Ofic7
                '    objComponente = Server.CreateObject("COM_PVU_DGarant0013.SAPDGarant")
                'Case Ofic8
                '    objComponente = Server.CreateObject("COM_PVU_DGarant0017.SAPDGarant")
                'Case Ofic22
                '    objComponente = Server.CreateObject("COM_PVU_DGarantRPPR.SAPDGarant")
                'Case Ofic23
                '    objComponente = Server.CreateObject("COM_PVU_DGarantS023.SAPDGarant")
                'Case Ofic24
                '    objComponente = Server.CreateObject("COM_PVU_DGarantS024.SAPDGarant")
                'Case Ofic25
                '    objComponente = Server.CreateObject("COM_PVU_DGarantS025.SAPDGarant")
                'Case Ofic26
                '    objComponente = Server.CreateObject("COM_PVU_DGarantS026.SAPDGarant")
                'Case Ofic27
                '    objComponente = Server.CreateObject("COM_PVU_DGarant0007.SAPDGarant")
                'Case Ofic28
                '    objComponente = Server.CreateObject("COM_PVU_DGarant0007.SAPDGarant")
                'Case Ofic29
                '    objComponente = Server.CreateObject("COM_PVU_DGarantS030.SAPDGarant")
                'Case Ofic30
                '    objComponente = Server.CreateObject("COM_PVU_DGarantS031.SAPDGarant")
                'Case Ofic31
                '    objComponente = Server.CreateObject("COM_PVU_DGarantS032.SAPDGarant")
                'Case Ofic32
                '    objComponente = Server.CreateObject("COM_PVU_DGarantS033.SAPDGarant")

                'Case Ofic10, Ofic12, Ofic15, Ofic18
                '    objComponente = Server.CreateObject("COM_PVU_DGarantFR12.SAPDGarant")
                'Case Ofic9, Ofic11, Ofic13, Ofic21, Ofic73
                '    objComponente = Server.CreateObject("COM_PVU_DGarantFR13.SAPDGarant")
                'Case Ofic14, Ofic16, Ofic17, Ofic19, Ofic34, ofic20
                '    objComponente = Server.CreateObject("COM_PVU_DGarantFR14.SAPDGarant")
                'Case Ofic34, Ofic35, Ofic40, Ofic44, Ofic47, Ofic50, Ofic51, Ofic52, Ofic53, Ofic54, Ofic55, Ofic56, Ofic58, Ofic60, Ofic62, Ofic63, Ofic67, Ofic69, Ofic71, Ofic72, Ofic74
                '    objComponente = Server.CreateObject("COM_PVU_DGarantFR15.SAPDGarant")
                'Case Ofic36, Ofic37, Ofic38, Ofic41, Ofic42, Ofic43, Ofic45, Ofic46, Ofic48, Ofic49, Ofic57, Ofic59, Ofic61, Ofic64, Ofic65, Ofic66, Ofic68, Ofic70
                '    objComponente = Server.CreateObject("COM_PVU_DGarantFR16.SAPDGarant")
                'Case Ofic39
                '    objComponente = Server.CreateObject("COM_PVU_DGarantFR17.SAPDGarant")
            Case Else
                objComponente = Server.CreateObject("COM_PVU_DGarant.SAPDGarant")
        End Select
        CreateObjectDGarant = objComponente
    End Function


    Function CreateObjectGrupoArt(ByVal Oficina)
        Dim objComponente
        Select Case Oficina
            Case "Ofic1"
                objComponente = Server.CreateObject("COM_PVU_GrupoArt0009.SAPGrupoArt")
                'Case Ofic2
                '    objComponente = Server.CreateObject("COM_PVU_GrupoArt0006.SAPGrupoArt")
                'Case Ofic3
                '    objComponente = Server.CreateObject("COM_PVU_GrupoArt0008.SAPGrupoArt")
                'Case Ofic4
                '    objComponente = Server.CreateObject("COM_PVU_GrupoArt0007.SAPGrupoArt")
                'Case Ofic5
                '    objComponente = Server.CreateObject("COM_PVU_GrupoArt0011.SAPGrupoArt")
                'Case Ofic6
                '    objComponente = Server.CreateObject("COM_PVU_GrupoArt0097.SAPGrupoArt")
                'Case Ofic7
                '    objComponente = Server.CreateObject("COM_PVU_GrupoArt0013.SAPGrupoArt")
                'Case Ofic8
                '    objComponente = Server.CreateObject("COM_PVU_GrupoArt0017.SAPGrupoArt")
                'Case Ofic22
                '    objComponente = Server.CreateObject("COM_PVU_GrupoArtRPPR.SAPGrupoArt")
                'Case Ofic23
                '    objComponente = Server.CreateObject("COM_PVU_GrupoArtS023.SAPGrupoArt")
                'Case Ofic24
                '    objComponente = Server.CreateObject("COM_PVU_GrupoArtS024.SAPGrupoArt")
                'Case Ofic25
                '    objComponente = Server.CreateObject("COM_PVU_GrupoArtS025.SAPGrupoArt")
                'Case Ofic26
                '    objComponente = Server.CreateObject("COM_PVU_GrupoArtS026.SAPGrupoArt")
                'Case Ofic27
                '    objComponente = Server.CreateObject("COM_PVU_GrupoArt0007.SAPGrupoArt")
                'Case Ofic28
                '    objComponente = Server.CreateObject("COM_PVU_GrupoArt0007.SAPGrupoArt")
                'Case Ofic29
                '    objComponente = Server.CreateObject("COM_PVU_GrupoArtS030.SAPGrupoArt")
                'Case Ofic30
                '    objComponente = Server.CreateObject("COM_PVU_GrupoArtS031.SAPGrupoArt")
                'Case Ofic31
                '    objComponente = Server.CreateObject("COM_PVU_GrupoArtS032.SAPGrupoArt")
                'Case Ofic32
                '    objComponente = Server.CreateObject("COM_PVU_GrupoArtS033.SAPGrupoArt")

                'Case Ofic10, Ofic12, Ofic15, Ofic18
                '    objComponente = Server.CreateObject("COM_PVU_GrupoArtFR12.SAPGrupoArt")
                'Case Ofic9, Ofic11, Ofic13, Ofic21, Ofic73
                '    objComponente = Server.CreateObject("COM_PVU_GrupoArtFR13.SAPGrupoArt")
                'Case Ofic14, Ofic16, Ofic17, Ofic19, Ofic34, ofic20
                '    objComponente = Server.CreateObject("COM_PVU_GrupoArtFR14.SAPGrupoArt")
                'Case Ofic34, Ofic35, Ofic40, Ofic44, Ofic47, Ofic50, Ofic51, Ofic52, Ofic53, Ofic54, Ofic55, Ofic56, Ofic58, Ofic60, Ofic62, Ofic63, Ofic67, Ofic69, Ofic71, Ofic72, Ofic74
                '    objComponente = Server.CreateObject("COM_PVU_GrupoArtFR15.SAPGrupoArt")
                'Case Ofic36, Ofic37, Ofic38, Ofic41, Ofic42, Ofic43, Ofic45, Ofic46, Ofic48, Ofic49, Ofic57, Ofic59, Ofic61, Ofic64, Ofic65, Ofic66, Ofic68, Ofic70
                '    objComponente = Server.CreateObject("COM_PVU_GrupoArtFR16.SAPGrupoArt")
                'Case Ofic39
                '    objComponente = Server.CreateObject("COM_PVU_GrupoArtFR17.SAPGrupoArt")
            Case Else
                objComponente = Server.CreateObject("COM_PVU_GrupoArt.SAPGrupoArt")
        End Select
        CreateObjectGrupoArt = objComponente
    End Function


    Function CreateObjectVCuotas(ByVal Oficina)
        Dim objComponente
        Select Case Oficina
            Case "Ofic1"
                objComponente = Server.CreateObject("COM_PVU_VCuotas0009.SAPVCuotas")
                'Case Ofic2
                '    objComponente = Server.CreateObject("COM_PVU_VCuotas0006.SAPVCuotas")
                'Case Ofic3
                '    objComponente = Server.CreateObject("COM_PVU_VCuotas0008.SAPVCuotas")
                'Case Ofic4
                '    objComponente = Server.CreateObject("COM_PVU_VCuotas0007.SAPVCuotas")
                'Case Ofic5
                '    objComponente = Server.CreateObject("COM_PVU_VCuotas0011.SAPVCuotas")
                'Case Ofic6
                '    objComponente = Server.CreateObject("COM_PVU_VCuotas0097.SAPVCuotas")
                'Case Ofic7
                '    objComponente = Server.CreateObject("COM_PVU_VCuotas0013.SAPVCuotas")
                'Case Ofic8
                '    objComponente = Server.CreateObject("COM_PVU_VCuotas0017.SAPVCuotas")
                'Case Ofic22
                '    objComponente = Server.CreateObject("COM_PVU_VCuotasRPPR.SAPVCuotas")
                'Case Ofic23
                '    objComponente = Server.CreateObject("COM_PVU_VCuotasS023.SAPVCuotas")
                'Case Ofic24
                '    objComponente = Server.CreateObject("COM_PVU_VCuotasS024.SAPVCuotas")
                'Case Ofic25
                '    objComponente = Server.CreateObject("COM_PVU_VCuotasS025.SAPVCuotas")
                'Case Ofic26
                '    objComponente = Server.CreateObject("COM_PVU_VCuotasS026.SAPVCuotas")
                'Case Ofic27
                '    objComponente = Server.CreateObject("COM_PVU_VCuotas0007.SAPVCuotas")
                'Case Ofic28
                '    objComponente = Server.CreateObject("COM_PVU_VCuotas0007.SAPVCuotas")
                'Case Ofic29
                '    objComponente = Server.CreateObject("COM_PVU_VCuotasS030.SAPVCuotas")
                'Case Ofic30
                '    objComponente = Server.CreateObject("COM_PVU_VCuotasS031.SAPVCuotas")
                'Case Ofic31
                '    objComponente = Server.CreateObject("COM_PVU_VCuotasS032.SAPVCuotas")
                'Case Ofic32
                '    objComponente = Server.CreateObject("COM_PVU_VCuotasS033.SAPVCuotas")

                'Case Ofic10, Ofic12, Ofic15, Ofic18
                '    objComponente = Server.CreateObject("COM_PVU_VCuotasFR12.SAPVCuotas")
                'Case Ofic9, Ofic11, Ofic13, Ofic21, Ofic73
                '    objComponente = Server.CreateObject("COM_PVU_VCuotasFR13.SAPVCuotas")
                'Case Ofic14, Ofic16, Ofic17, Ofic19, Ofic34, ofic20
                '    objComponente = Server.CreateObject("COM_PVU_VCuotasFR14.SAPVCuotas")
                'Case Ofic34, Ofic35, Ofic40, Ofic44, Ofic47, Ofic50, Ofic51, Ofic52, Ofic53, Ofic54, Ofic55, Ofic56, Ofic58, Ofic60, Ofic62, Ofic63, Ofic67, Ofic69, Ofic71, Ofic72, Ofic74
                '    objComponente = Server.CreateObject("COM_PVU_VCuotasFR15.SAPVCuotas")
                'Case Ofic36, Ofic37, Ofic38, Ofic41, Ofic42, Ofic43, Ofic45, Ofic46, Ofic48, Ofic49, Ofic57, Ofic59, Ofic61, Ofic64, Ofic65, Ofic66, Ofic68, Ofic70
                '    objComponente = Server.CreateObject("COM_PVU_VCuotasFR16.SAPVCuotas")

                'Case Ofic39
                '    objComponente = Server.CreateObject("COM_PVU_VCuotasFR17.SAPVCuotas")

            Case Else
                objComponente = Server.CreateObject("COM_PVU_VCuotas.SAPVCuotas")
        End Select
        CreateObjectVCuotas = objComponente
    End Function



    Function CreateObjectSegVenta(ByVal Oficina)
        Dim objComponente
        Select Case Oficina
            Case "Ofic1"
                objComponente = Server.CreateObject("COM_PVU_SegVenta0009.SAPSegVenta")
                'Case Ofic2
                '    objComponente = Server.CreateObject("COM_PVU_SegVenta0006.SAPSegVenta")
                'Case Ofic3
                '    objComponente = Server.CreateObject("COM_PVU_SegVenta0008.SAPSegVenta")
                'Case Ofic4
                '    objComponente = Server.CreateObject("COM_PVU_SegVenta0007.SAPSegVenta")
                'Case Ofic5
                '    objComponente = Server.CreateObject("COM_PVU_SegVenta0011.SAPSegVenta")
                'Case Ofic6
                '    objComponente = Server.CreateObject("COM_PVU_SegVenta0097.SAPSegVenta")
                'Case Ofic7
                '    objComponente = Server.CreateObject("COM_PVU_SegVenta0013.SAPSegVenta")
                'Case Ofic8
                '    objComponente = Server.CreateObject("COM_PVU_SegVenta0017.SAPSegVenta")
                'Case Ofic22
                '    objComponente = Server.CreateObject("COM_PVU_SegVentaRPPR.SAPSegVenta")
                'Case Ofic23
                '    objComponente = Server.CreateObject("COM_PVU_SegVentaS023.SAPSegVenta")
                'Case Ofic24
                '    objComponente = Server.CreateObject("COM_PVU_SegVentaS024.SAPSegVenta")
                'Case Ofic25
                '    objComponente = Server.CreateObject("COM_PVU_SegVentaS025.SAPSegVenta")
                'Case Ofic26
                '    objComponente = Server.CreateObject("COM_PVU_SegVentaS026.SAPSegVenta")
                'Case Ofic27
                '    objComponente = Server.CreateObject("COM_PVU_SegVenta0007.SAPSegVenta")
                'Case Ofic28
                '    objComponente = Server.CreateObject("COM_PVU_SegVenta0007.SAPSegVenta")
                'Case Ofic29
                '    objComponente = Server.CreateObject("COM_PVU_SegVentaS030.SAPSegVenta")
                'Case Ofic30
                '    objComponente = Server.CreateObject("COM_PVU_SegVentaS031.SAPSegVenta")
                'Case Ofic31
                '    objComponente = Server.CreateObject("COM_PVU_SegVentaS032.SAPSegVenta")
                'Case Ofic32
                '    objComponente = Server.CreateObject("COM_PVU_SegVentaS033.SAPSegVenta")


                'Case Ofic10, Ofic12, Ofic15, Ofic18
                '    objComponente = Server.CreateObject("COM_PVU_SegVentaFR12.SAPSegVenta")

                'Case Ofic9, Ofic11, Ofic13, Ofic21, Ofic73
                '    objComponente = Server.CreateObject("COM_PVU_SegVentaFR13.SAPSegVenta")

                'Case Ofic14, Ofic16, Ofic17, Ofic19, Ofic34
                '    objComponente = Server.CreateObject("COM_PVU_SegVentaFR14.SAPSegVenta")

                'Case Ofic34, Ofic35, Ofic40, Ofic44, Ofic47, Ofic50, Ofic51, Ofic52, Ofic53, Ofic54, Ofic55, Ofic56, Ofic58, Ofic60, Ofic62, Ofic63, Ofic67, Ofic69, Ofic71, Ofic72, Ofic74
                '    objComponente = Server.CreateObject("COM_PVU_SegVentaFR15.SAPSegVenta")

                'Case Ofic36, Ofic37, Ofic38, Ofic41, Ofic42, Ofic43, Ofic45, Ofic46, Ofic48, Ofic49, Ofic57, Ofic59, Ofic61, Ofic64, Ofic65, Ofic66, Ofic68, Ofic70
                '    objComponente = Server.CreateObject("COM_PVU_SegVentaFR16.SAPSegVenta")

                'Case Ofic39
                '    objComponente = Server.CreateObject("COM_PVU_SegVentaFR17.SAPSegVenta")

            Case Else
                objComponente = Server.CreateObject("COM_PVU_SegVenta.SAPSegVenta")
        End Select
        CreateObjectSegVenta = objComponente
    End Function

    Function CreateObjectCliente_Bus(ByVal Oficina)
        Dim objComponente
        Select Case Oficina
            Case "Ofic1"
                objComponente = Server.CreateObject("COM_PVU_ClienteB0009.SAPCliente")
                'Case Ofic2
                '    objComponente = Server.CreateObject("COM_PVU_ClienteB0006.SAPCliente")
                'Case Ofic3
                '    objComponente = Server.CreateObject("COM_PVU_ClienteB0008.SAPCliente")
                'Case Ofic4
                '    objComponente = Server.CreateObject("COM_PVU_ClienteB0007.SAPCliente")
                'Case Ofic5
                '    objComponente = Server.CreateObject("COM_PVU_ClienteB0011.SAPCliente")
                'Case Ofic6
                '    objComponente = Server.CreateObject("COM_PVU_ClienteB0097.SAPCliente")
                'Case Ofic7
                '    objComponente = Server.CreateObject("COM_PVU_ClienteB0013.SAPCliente")
                'Case Ofic8
                '    objComponente = Server.CreateObject("COM_PVU_ClienteB0017.SAPCliente")
                'Case Ofic22
                '    objComponente = Server.CreateObject("COM_PVU_ClienteBRPPR.SAPCliente")
                'Case Ofic23
                '    objComponente = Server.CreateObject("COM_PVU_ClienteBS023.SAPCliente")
                'Case Ofic24
                '    objComponente = Server.CreateObject("COM_PVU_ClienteBS024.SAPCliente")
                'Case Ofic25
                '    objComponente = Server.CreateObject("COM_PVU_ClienteBS025.SAPCliente")
                'Case Ofic26
                '    objComponente = Server.CreateObject("COM_PVU_ClienteBS026.SAPCliente")
                'Case Ofic27
                '    objComponente = Server.CreateObject("COM_PVU_ClienteB0007.SAPCliente")
                'Case Ofic28
                '    objComponente = Server.CreateObject("COM_PVU_ClienteB0007.SAPCliente")
                'Case Ofic29
                '    objComponente = Server.CreateObject("COM_PVU_ClienteBS030.SAPCliente")
                'Case Ofic30
                '    objComponente = Server.CreateObject("COM_PVU_ClienteBS031.SAPCliente")
                'Case Ofic31
                '    objComponente = Server.CreateObject("COM_PVU_ClienteBS032.SAPCliente")
                'Case Ofic32
                '    objComponente = Server.CreateObject("COM_PVU_ClienteBS033.SAPCliente")

                'Case Ofic10, Ofic12, Ofic15, Ofic18
                '    objComponente = Server.CreateObject("COM_PVU_ClienteFR12.SAPCliente")

                'Case Ofic9, Ofic11, Ofic13, Ofic21, Ofic73
                '    objComponente = Server.CreateObject("COM_PVU_ClienteFR13.SAPCliente")

                'Case Ofic14, Ofic16, Ofic17, Ofic19, Ofic34
                '    objComponente = Server.CreateObject("COM_PVU_ClienteFR14.SAPCliente")

                'Case Ofic34, Ofic35, Ofic40, Ofic44, Ofic47, Ofic50, Ofic51, Ofic52, Ofic53, Ofic54, Ofic55, Ofic56, Ofic58, Ofic60, Ofic62, Ofic63, Ofic67, Ofic69, Ofic71, Ofic72, Ofic74
                '    objComponente = Server.CreateObject("COM_PVU_ClienteFR15.SAPCliente")

                'Case Ofic36, Ofic37, Ofic38, Ofic41, Ofic42, Ofic43, Ofic45, Ofic46, Ofic48, Ofic49, Ofic57, Ofic59, Ofic61, Ofic64, Ofic65, Ofic66, Ofic68, Ofic70
                '    objComponente = Server.CreateObject("COM_PVU_ClienteFR16.SAPCliente")

                'Case Ofic39
                '    objComponente = Server.CreateObject("COM_PVU_ClienteBFR17.SAPCliente")
            Case Else
                objComponente = Server.CreateObject("COM_PVU_Cliente_Bus.SAPCliente")
        End Select
        CreateObjectCliente_Bus = objComponente
    End Function

    Function CreateObjectReniec(ByVal Oficina)
        Dim objComponente
        Select Case Oficina
            Case "Ofic1"
                objComponente = Server.CreateObject("COM_PVU_Reniec0009.clsTxReniec")
                'Case Ofic2
                '    objComponente = Server.CreateObject("COM_PVU_Reniec0006.clsTxReniec")
                'Case Ofic3
                '    objComponente = Server.CreateObject("COM_PVU_Reniec0008.clsTxReniec")
                'Case Ofic4
                '    objComponente = Server.CreateObject("COM_PVU_Reniec0007.clsTxReniec")
                'Case Ofic5
                '    objComponente = Server.CreateObject("COM_PVU_Reniec0011.clsTxReniec")
                'Case Ofic6
                '    objComponente = Server.CreateObject("COM_PVU_Reniec0097.clsTxReniec")
                'Case Ofic7
                '    objComponente = Server.CreateObject("COM_PVU_Reniec0013.clsTxReniec")
                'Case Ofic8
                '    objComponente = Server.CreateObject("COM_PVU_Reniec0017.clsTxReniec")
                'Case Ofic22
                '    objComponente = Server.CreateObject("COM_PVU_ReniecRPPR.clsTxReniec")
                'Case Ofic23
                '    objComponente = Server.CreateObject("COM_PVU_ReniecS023.clsTxReniec")
                'Case Ofic24
                '    objComponente = Server.CreateObject("COM_PVU_ReniecS024.clsTxReniec")
                'Case Ofic25
                '    objComponente = Server.CreateObject("COM_PVU_ReniecS025.clsTxReniec")
                'Case Ofic26
                '    objComponente = Server.CreateObject("COM_PVU_ReniecS026.clsTxReniec")
                'Case Ofic27
                '    objComponente = Server.CreateObject("COM_PVU_Reniec0007.clsTxReniec")
                'Case Ofic28
                '    objComponente = Server.CreateObject("COM_PVU_Reniec0007.clsTxReniec")
                'Case Ofic29
                '    objComponente = Server.CreateObject("COM_PVU_ReniecS030.clsTxReniec")
                'Case Ofic30
                '    objComponente = Server.CreateObject("COM_PVU_ReniecS031.clsTxReniec")
                'Case Ofic31
                '    objComponente = Server.CreateObject("COM_PVU_ReniecS032.clsTxReniec")
                'Case Ofic32
                '    objComponente = Server.CreateObject("COM_PVU_ReniecS033.clsTxReniec")

                'Case Ofic10, Ofic12, Ofic15, Ofic18
                '    objComponente = Server.CreateObject("COM_PVU_ReniecFR12.clsTxReniec")

                'Case Ofic9, Ofic11, Ofic13, Ofic21, Ofic73
                '    objComponente = Server.CreateObject("COM_PVU_ReniecFR13.clsTxReniec")

                'Case Ofic14, Ofic16, Ofic17, Ofic19, Ofic34
                '    objComponente = Server.CreateObject("COM_PVU_ReniecFR14.clsTxReniec")

                'Case Ofic34, Ofic35, Ofic40, Ofic44, Ofic47, Ofic50, Ofic51, Ofic52, Ofic53, Ofic54, Ofic55, Ofic56, Ofic58, Ofic60, Ofic62, Ofic63, Ofic67, Ofic69, Ofic71, Ofic72, Ofic74
                '    objComponente = Server.CreateObject("COM_PVU_ReniecFR15.clsTxReniec")

                'Case Ofic36, Ofic37, Ofic38, Ofic41, Ofic42, Ofic43, Ofic45, Ofic46, Ofic48, Ofic49, Ofic57, Ofic59, Ofic61, Ofic64, Ofic65, Ofic66, Ofic68, Ofic70
                '    objComponente = Server.CreateObject("COM_PVU_ReniecFR16.clsTxReniec")

                'Case Ofic39
                '    objComponente = Server.CreateObject("COM_PVU_ReniecFR17.clsTxReniec")

            Case Else
                objComponente = Server.CreateObject("COM_PVU_Reniec.clsTxReniec")
        End Select
        CreateObjectReniec = objComponente
    End Function

    Function CreateObjectRVirtual(ByVal Oficina)
        Dim objComponente
        Select Case Oficina

            Case "Ofic1"
                objComponente = Server.CreateObject("COM_PVU_RVirtual0009.NvTIMPPVCaller")
                'Case Ofic2
                '    objComponente = Server.CreateObject("COM_PVU_RVirtual0006.NvTIMPPVCaller")
                'Case Ofic3
                '    objComponente = Server.CreateObject("COM_PVU_RVirtual0008.NvTIMPPVCaller")
                'Case Ofic4
                '    objComponente = Server.CreateObject("COM_PVU_RVirtual0007.NvTIMPPVCaller")
                'Case Ofic5
                '    objComponente = Server.CreateObject("COM_PVU_RVirtual0011.NvTIMPPVCaller")
                'Case Ofic6
                '    objComponente = Server.CreateObject("COM_PVU_RVirtual0097.NvTIMPPVCaller")
                'Case Ofic7
                '    objComponente = Server.CreateObject("COM_PVU_RVirtual0013.NvTIMPPVCaller")
                'Case Ofic8
                '    objComponente = Server.CreateObject("COM_PVU_RVirtual0017.NvTIMPPVCaller")
                'Case Ofic10, Ofic12, Ofic15, Ofic18
                '    objComponente = Server.CreateObject("COM_PVU_RVirtualFR12.NvTIMPPVCaller")
                'Case Ofic9, Ofic11, Ofic13, Ofic21, Ofic73
                '    objComponente = Server.CreateObject("COM_PVU_RVirtualFR13.NvTIMPPVCaller")
                'Case Ofic14, Ofic16, Ofic17, Ofic19, Ofic34, ofic20
                '    objComponente = Server.CreateObject("COM_PVU_RVirtualFR14.NvTIMPPVCaller")
                'Case Ofic34, Ofic35, Ofic40, Ofic44, Ofic47, Ofic50, Ofic51, Ofic52, Ofic53, Ofic54, Ofic55, Ofic56, Ofic58, Ofic60, Ofic62, Ofic63, Ofic67, Ofic69, Ofic71, Ofic72, Ofic74
                '    objComponente = Server.CreateObject("COM_PVU_RVirtualFR15.NvTIMPPVCaller")
                'Case Ofic36, Ofic37, Ofic38, Ofic41, Ofic42, Ofic43, Ofic45, Ofic46, Ofic48, Ofic49, Ofic57, Ofic59, Ofic61, Ofic64, Ofic65, Ofic66, Ofic68, Ofic70
                '    objComponente = Server.CreateObject("COM_PVU_RVirtualFR16.NvTIMPPVCaller")
                'Case Ofic39
                '    objComponente = Server.CreateObject("COM_PVU_RVirtualFR17.NvTIMPPVCaller")
            Case Else
                objComponente = Server.CreateObject("COM_PVU_RVirtual.NvTIMPPVCaller")
        End Select
        CreateObjectRVirtual = objComponente
    End Function



    'Function CreateObjectRVirtual(Oficina)
    'Dim objComponente
    '	Set objComponente = Server.CreateObject("COM_PVU_RVirtual.NvTIMPPVCaller")
    '	Set CreateObjectRVirtual = objComponente
    'End Function


    '====================================================================================
    ' 20040923: Percy Silva
    ' Se Agrega Función por Observación de QA
    '====================================================================================
    Function CreateObjectGCarpetas()
        Dim objComponente
        objComponente = Server.CreateObject("COM_PVU_GCarpetas.Documento")
        CreateObjectGCarpetas = objComponente
    End Function


    '====================================================================================
    ' Consultas Reniec - Topes
    ' 20041011: Luis Mascaro
    ' Se Agrega Función por Observación de QA
    '====================================================================================
    Function CreateObjectInstitucion(ByVal Oficina)
        Dim objComponente
        objComponente = Server.CreateObject("COM_PVU_Tope_Bus.clsInstitucion")
        CreateObjectInstitucion = objComponente
    End Function

    Function CreateObjectTopeOfVenta(ByVal Oficina)
        Dim objComponente
        objComponente = Server.CreateObject("COM_PVU_Tope_Bus.clsTopeOfVenta")
        CreateObjectTopeOfVenta = objComponente
    End Function

    Function CreateObjectTopeVendedor(ByVal Oficina)
        Dim objComponente
        objComponente = Server.CreateObject("COM_PVU_Tope_Bus.clsTopeVendedor")
        CreateObjectTopeVendedor = objComponente
    End Function

    Function CreateObjectTopeOfVentaXML(ByVal Oficina)
        Dim objComponente
        objComponente = Server.CreateObject("COM_PVU_Tope_Bus.clsTopeOfVentaXML")
        CreateObjectTopeOfVentaXML = objComponente
    End Function

    Function CreateObjectTopeVendedorXML(ByVal Oficina)
        Dim objComponente
        objComponente = Server.CreateObject("COM_PVU_Tope_Bus.clsTopeVendedorXML")
        CreateObjectTopeVendedorXML = objComponente
    End Function

    Function CreateObjectCanal(ByVal Oficina)
        Dim objComponente
        objComponente = Server.CreateObject("COM_PVU_Tope_Bus.clsCanal")
        CreateObjectCanal = objComponente
    End Function

    Function CreateObjectOfVenta(ByVal Oficina)
        Dim objComponente
        objComponente = Server.CreateObject("COM_PVU_Tope_Bus.clsOfVenta")
        CreateObjectOfVenta = objComponente
    End Function

    Function CreateObjectTablaGeneral(ByVal Oficina)
        Dim objComponente
        objComponente = Server.CreateObject("COM_PVU_Tope_Bus.clsGeneral")
        CreateObjectTablaGeneral = objComponente
    End Function


    '===============================================================================
    ' Autor:  Percy Silva
    ' Fecha:  28/10/2004
    ' Método: GetTokenXml
    ' Uso:	  Permite obtener un valor a partir de una trama en formato XML, 
    '		  tomando en cuenta las SubTramas.
    ' Ejemplo: 
    '		  strResultadoSAP = GetTokenXml(StrXml, "RS05", "MESSAGE")
    '===============================================================================
    Function GetTokenXml(ByVal strDataXML, ByVal strNombreBloque, ByVal strToken)
        ' Dada una trama XML, se entrega el item que se quiere extraer su data.
        Dim intPos10, intPos20
        Dim strSubTramaXML
        intPos10 = InStr(1, strDataXML, "<" & strNombreBloque & ">") + Len(strNombreBloque) + 2
        intPos20 = InStr(1, strDataXML, "</" & strNombreBloque & ">")

        If intPos10 < intPos20 Then
            strSubTramaXML = Mid(strDataXML, intPos10, intPos20 - intPos10)
            GetTokenXml = GetDataXML(strSubTramaXML, strToken)
        Else
            GetTokenXml = ""
        End If
    End Function

    '===============================================================================
    ' Autor:  Percy Silva
    ' Fecha:  28/10/2004
    ' Método: GetDataXML
    ' Uso:	  Permite obtener un valor a partir de una SubTrama en formato XML
    ' Ejemplo: 
    '		  strResultadoSAP = GetDataXML(StrXml, "MESSAGE")
    '===============================================================================
    Function GetDataXML(ByVal strCadXML, ByVal strItemXML)
        ' Dada una trama XML, se entrega el item que se quiere extraer su data.
        Dim intPos1, intPos2
        intPos1 = InStr(1, strCadXML, "<" & strItemXML & ">") + Len(strItemXML) + 2
        intPos2 = InStr(1, strCadXML, "</" & strItemXML & ">")
        If intPos1 < intPos2 Then
            GetDataXML = Mid(strCadXML, intPos1, intPos2 - intPos1)
        Else
            GetDataXML = ""
        End If
    End Function



    '===============================================================================
    ' Autor:  Percy Silva
    ' Fecha:  28/10/2004
    ' Método: GetTokenXmlByIndex
    ' Uso:	  Permite obtener un valor a partir de una trama en formato XML, 
    '		  tomando en cuenta las SubTramas,
    '		  realizando la búsqueda por el índice correspondiente
    ' Ejemplo: 
    '		  strResultadoSAP = GetTokenXmlByIndex(StrXml, "RS01", 1)
    '===============================================================================
    Function GetTokenXmlByIndex(ByVal strDataXML, ByVal strNombreBloque, ByVal intIndex)
        ' Dada una trama XML, se entrega el item que se quiere extraer su data.
        Dim intPos10, intPos20
        Dim strSubTramaXML
        Dim strItemXML

        strItemXML = "<ITEMXML>"
        intPos10 = InStr(1, strDataXML, "<" & strNombreBloque & ">") + Len(strNombreBloque) + 2 + Len(strItemXML)
        intPos20 = InStr(1, strDataXML, "</" & strNombreBloque & ">")

        If intPos10 < intPos20 Then
            strSubTramaXML = Mid(strDataXML, intPos10, intPos20 - intPos10)

            GetTokenXmlByIndex = GetDataXMLByIndex(strSubTramaXML, intIndex)
        Else
            GetTokenXmlByIndex = ""
        End If
    End Function

    '===============================================================================
    ' Autor:  Percy Silva
    ' Fecha:  28/10/2004
    ' Método: GetDataXMLByIndex
    ' Uso:	  Permite obtener un valor a partir de una SubTrama en formato XML,
    '		  realizando la búsqueda por el índice correspondiente
    ' Ejemplo: 
    '		  strResultadoSAP = GetDataXMLByIndex(StrXml, 1)
    '===============================================================================
    Function GetDataXMLByIndex(ByVal strCadXML, ByVal intIndex)
        ' Dada una trama XML, se entrega el item que se quiere extraer su data.
        Dim intPos1, intPos2
        Dim intIdx
        Dim strCadXMLtmp

        strCadXMLtmp = strCadXML
        For intIdx = 0 To intIndex - 1
            intPos1 = InStr(1, strCadXMLtmp, "</") + 1
            strCadXMLtmp = Right(strCadXMLtmp, Len(strCadXMLtmp) - intPos1)
            intPos1 = InStr(1, strCadXMLtmp, ">") + 1
            strCadXMLtmp = Right(strCadXMLtmp, Len(strCadXMLtmp) - intPos1)
        Next

        ' Si se encuentra el Item solicitado:
        If InStr(1, strCadXMLtmp, ">") > 0 Then
            intPos1 = InStr(1, strCadXMLtmp, ">")
            strCadXMLtmp = Right(strCadXMLtmp, Len(strCadXMLtmp) - intPos1)
            intPos2 = InStr(1, strCadXMLtmp, "<") - 1
            strCadXMLtmp = Left(strCadXMLtmp, intPos2)

            GetDataXMLByIndex = strCadXMLtmp
        Else
            GetDataXMLByIndex = ""
        End If
    End Function


    '===============================================================================
    ' Autor:  Percy Silva
    ' Fecha:  28/10/2004
    ' Método: GetNumItemsDataXML
    ' Uso:	  Permite obtener el número de elementos contenidos en la trama XML
    ' Ejemplo: 
    '		  intNumItems = GetNumItemsDataXML(StrXml, "RS01")
    '===============================================================================
    Function GetNumItemsDataXML(ByVal strDataXML, ByVal strNombreBloque)
        ' Dada una trama XML, se entrega el item que se quiere extraer su data.
        Dim intPos1, intPos2
        Dim intIdx
        Dim strCadXMLtmp
        Dim intPos10, intPos20
        Dim strSubTramaXML
        Dim strItemXML

        strItemXML = "<ITEMXML>"
        intPos10 = InStr(1, strDataXML, "<" & strNombreBloque & ">") + Len(strNombreBloque) + 2 + Len(strItemXML)
        intPos20 = InStr(1, strDataXML, "</" & strNombreBloque & ">")

        If intPos10 < intPos20 Then
            strSubTramaXML = Mid(strDataXML, intPos10, intPos20 - intPos10)

            strCadXMLtmp = strSubTramaXML
            intIdx = 0
            Do Until Len(strCadXMLtmp) <= 0
                'for intIdx = 0 to intIndex - 1
                intIdx = intIdx + 1
                intPos1 = InStr(1, strCadXMLtmp, "</") + 1
                If Len(strCadXMLtmp) - intPos1 < 0 Then
                    Exit Do
                End If
                strCadXMLtmp = Right(strCadXMLtmp, Len(strCadXMLtmp) - intPos1)
                intPos1 = InStr(1, strCadXMLtmp, ">") + 1
                If Len(strCadXMLtmp) - intPos1 < 0 Then
                    Exit Do
                End If
                strCadXMLtmp = Right(strCadXMLtmp, Len(strCadXMLtmp) - intPos1)
                'next
            Loop
            If intIdx > 0 Then
                GetNumItemsDataXML = intIdx - 1
            End If
        Else
            GetNumItemsDataXML = 0
        End If
    End Function

    '===============================================================================
    ' Autor:  Percy Silva
    ' Fecha:  28/10/2004
    ' Método: GetArregloDataXML
    ' Uso:	  Permite obtener el un arreglo a partir de los elementos contenidos 
    '		  en la trama XML
    ' Ejemplo: 
    '		  arrDataXML = GetArregloDataXML(StrXml, "RS01")
    '===============================================================================
    Function GetArregloDataXML(ByVal strDataXML, ByVal strNombreBloque)
        ' Dada una trama XML, se entrega el item que se quiere extraer su data.
        Dim iNItemsDataXML, iAux
        Dim arrData()
        iNItemsDataXML = GetNumItemsDataXML(strDataXML, strNombreBloque)
        For iAux = 0 To iNItemsDataXML - 1
            ReDim Preserve arrData(iAux)
            arrData(iAux) = GetTokenXmlByIndex(strDataXML, strNombreBloque, iAux)
        Next
        GetArregloDataXML = arrData
    End Function


End Class
