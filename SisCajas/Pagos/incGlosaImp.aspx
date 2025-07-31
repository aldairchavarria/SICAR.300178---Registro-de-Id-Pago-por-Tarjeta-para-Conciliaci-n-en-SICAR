<%
'Traer Telefonos Asociados al documento
'Encontrar el numero de pedido a partir del numero SAP 

dim dsComPagos as System.Data.DataSet = objComponentePagos.Get_ConsultaPedido("", oficinaVenta, docSap,"")
if dsComPagos.tables(0).rows.count>0 then
	strNum_Doc = cstr(dsComPagos.tables(0).rows(0)("DOCUMENTO"))
else
	strNum_Doc = ""		
end if

If Len(strNum_Doc) <> 0 Then
	strNum_Doc = Cdbl(strNum_Doc)
end if
'Response.Write( "<script>Alert('demostracion:" & strNum_Doc & "')</script>")
'Response.End
dim dsTrio as System.Data.DataSet = objComponentePagos.ConsultaTriacionPrePost(strNum_Doc, "", "", "")
'if dsTrio.tables(0).rows.count>0 then 'modificado JCR
if Not dsTrio Is Nothing then
	dim dsComp as System.Data.DataSet = objComponentePagos.Get_ConsultaComprobante(strNum_Doc, oficinaVenta)
	
	IF  dsComp.tables(3).rows.count>0 then
		strMsgGlosa = cstr(dsComp.tables(3).rows(0)("GLOSA_TRIACION"))
	else
		strMsgGlosa = ""
	end if
	
end if

%>

<%If trim(tipo) = "ZBOL" Then%>
	<div ID="" STYLE="position:absolute; left:50; top:<%=posicion%>; ">
		<table border="0" bordercolor="#000000" width="600" style="FONT-FAMILY: 'Arial' ; font-size:8pt">
			<tr >
				<td align="left">
				<%=strMsgGlosa%>
				</td>
			</tr>
			<%
			if Not dsTrio Is Nothing then
			'if dsTrio.tables(0).rows.count>0 then 'modificado JCR
			'If not objRSCam.eof Then
				dim drFila as System.Data.DataRow
				for each drFila in dsTrio.tables(0).rows
				'Do while not objRSCam.eof
					numtelPre = cstr(drFila("NRO_TEL_PREPAGO"))
					numtelPos = cstr(drFila("NRO_TEL_POSTPAGO"))
			%>
			<tr><td align="left"><%=numtelPos%>&nbsp;&nbsp;&nbsp;<%=numtelPre%></td></tr>
			<%
				'objRSCam.moveNext
				'Loop
				next
			End if
			%>
		</table>
	</div>
<%else%>
	<div ID="" STYLE="position:absolute; left:50; top:<%=posicion%>;">
		<table border="0" bordercolor="#000000" width="600" style="FONT-FAMILY: 'Arial' ; font-size:8pt">
			<tr>
				<td align="left">
				<%=strMsgGlosa%>
				</td>
			</tr>
			<%
			if dsTrio.tables(0).rows.count>0 then			
			'If not objRSCam.eof Then
				dim drFila as System.Data.DataRow
				for each drFila in dsTrio.tables(0).rows
				'Do while not objRSCam.eof
					numtelPre = cstr(drFila("NRO_TEL_PREPAGO"))
					numtelPos = cstr(drFila("NRO_TEL_POSTPAGO"))				
			%>
			<tr><td align="left"><%=numtelPos%>&nbsp;&nbsp;&nbsp;<%=numtelPre%></td></tr>
			<%
				'objRSCam.moveNext
				'Loop
				next
			End if
			%>
		</table>
	</div>				
<%end if%>	
