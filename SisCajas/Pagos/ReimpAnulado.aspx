<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ReimpAnulado.aspx.vb" Inherits="SisCajas.ReimpAnulado"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
		<%

		Dim objPagos As New SAP_SIC_Pagos.clsPagos
        Dim dsReturn As System.Data.DataSet	
        Dim dsImpres As System.Data.DataSet
        dim strReferencia as string 
        dim strUltNumero as string 
        
        Dim i As Integer

        dsReturn = objPagos.Get_ParamGlobal(Session("ALMACEN"))
        
        objPagos.Get_NumeroSUNAT(Session("ALMACEN"),"ZFBR",trim(cstr(session("CodImprTicket"))),"x",strReferencia,strUltNumero)

        If Trim(dsReturn.Tables(0).Rows(0).Item("IMPRESION_SAP")) <> "" Then
            dsImpres = objPagos.Set_ImpresionFormulario("ZRVTICKET_ANULDO", Trim(CStr(Session("CodImprTicket"))), strReferencia, "")
            For i = 0 To dsImpres.Tables(0).Rows.Count - 1
                If dsImpres.Tables(0).Rows(i).Item("TYPE") = "E" Then
                    Response.Write("<script language=javascript>alert('" & dsImpres.Tables(0).Rows(i).Item("MESSAGE") & "')</script>")
                End If
            Next
            
            
        else    

		%>
		<iframe id="IfrmImpresion" name="IfrmImpresion" src="#" style="VISIBILITY:hidden;WIDTH:0px;HEIGHT:0px">
			</iframe>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<script language="javascript">
									function Imprimir()
									{	
										
										
										var objIframe = document.getElementById("IfrmImpresion");
										window.open(objIframe.contentWindow.location);
									}
									function f_Imprimir(){
										var objIframe = document.getElementById("IfrmImpresion");
										objIframe.style.visibility = "visible";
										objIframe.style.width = 0;
										objIframe.style.height = 0;
										objIframe.contentWindow.location.replace('ImpresionAnularTicket.aspx?NroSunat=' + '<%=strReferencia%>');
									}
									f_Imprimir();
									
		</script>
	<% end if %>	
	

