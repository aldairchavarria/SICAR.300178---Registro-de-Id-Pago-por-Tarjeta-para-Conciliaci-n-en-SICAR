
function Obt_Dpto(codDpto) {
	var resp = "";
	var dataOrigen=eval('parent.cabecera.document.frmUbigeo.Dept');
	if (dataOrigen) {
		strTmp=dataOrigen.value;
		aTmp=strTmp.split(":");
		n=aTmp.length-1;
		for(i=0;i<=n;i++) {
			aData=aTmp[i].split("|");
		 	strNombre=aData[1];
 			strValor=aData[0];
			if (codDpto==strValor) {
				resp = strNombre;	
			}
		}
	}
	return resp;
}

function Obt_Prov(codDpto,codProv) {
	var resp = "";
	var dataOrigen=eval('parent.cabecera.document.frmUbigeo.Prov'+codDpto);
	if (dataOrigen) {
		strTmp=dataOrigen.value;
		aTmp=strTmp.split(":");
		n=aTmp.length-1;
		for(i=0;i<=n;i++) {
			aData=aTmp[i].split("|");
		 	strNombre=aData[1];
 			strValor=aData[0];
			if (codProv==strValor) {
				resp = strNombre;	
			}
		}
	}
	return resp;
}

function Obt_Dist(codProv,codDist) {
	var resp = "";
	var dataOrigen=eval('parent.cabecera.document.frmUbigeo.Dist'+codProv);
	if (dataOrigen) {
		strTmp=dataOrigen.value;
		aTmp=strTmp.split(":");
		n=aTmp.length-1;
		for(i=0;i<=n;i++) {
			aData=aTmp[i].split("|");
		 	strNombre=aData[1];
 			strValor=aData[0];
			if (codDist==strValor) {
				resp = strNombre;	
			}
		}
	}
	return resp;
}

function Obt_CPost(codDist) {
	var resp = "";
	var dataOrigen=eval('parent.cabecera.document.frmUbigeo.CPost'+codDist);
	if (dataOrigen) {
		resp = dataOrigen.value;	
	}
	return resp;
}

function CargaCombo(origen, sel) {
	strNombre=" ";
	strValor="xx";
	document.frmPrincipal.cboProv.length=0;
	document.frmPrincipal.cboProv.options[0]=new Option(strNombre,strValor);
	document.frmPrincipal.cboDstr.length=0;
	document.frmPrincipal.cboDstr.options[0]=new Option(strNombre,strValor);
	document.frmPrincipal.txtCodPostal.value = "";
	if (origen == "xx") {
		return;
	};
	var dataOrigen=eval('parent.cabecera.document.frmUbigeo.Prov' + origen);
	if (dataOrigen) {
		strTmp=dataOrigen.value;
		aTmp=strTmp.split(":");
		n=aTmp.length-1;
		for(i=0;i<=n;i++) {
			aData=aTmp[i].split("|");
		 	strNombre=aData[1];
 			strValor=aData[0];
			document.frmPrincipal.cboProv.options[i+1]=new Option(strNombre,strValor);
			if (sel==strValor) {
				document.frmPrincipal.cboProv.selectedIndex = i+1;
			}
	 	}
	}
}

function CargaComboI(origen, sel) {
	strNombre=" ";
	strValor="xx";
	document.frmPrincipal.cboDstr.length=0;
	document.frmPrincipal.cboDstr.options[0]=new Option(strNombre,strValor);
	document.frmPrincipal.txtCodPostal.value = "";
	if(origen == "xx") {
		return;
	};
	var dataOrigen=eval('parent.cabecera.document.frmUbigeo.Dist' + origen);
	if (dataOrigen) {
		strTmp=dataOrigen.value;
		aTmp=strTmp.split(":");
		n=aTmp.length-1;
		for(i=0;i<=n;i++) {
			aData=aTmp[i].split("|");
			strNombre=aData[1];
			strValor=aData[0];
			document.frmPrincipal.cboDstr.options[i+1]=new Option(strNombre,strValor);
			if (sel==strValor) {
				document.frmPrincipal.cboDstr.selectedIndex = i+1;
			}
		}
	}
}

function CargaCodPost(origen) {
	document.frmPrincipal.txtCodPostal.value = "";
	if(origen == "xx") {
		return;
	};
	var dataOrigen=eval('parent.cabecera.document.frmUbigeo.CPost' + origen);
	if (dataOrigen) {
		document.frmPrincipal.txtCodPostal.value=dataOrigen.value;
	}
}

//Cambio para Iframe: Walter Arambulo M.
function Obt_Dpto_If(codDpto) {
	var resp = "";
	var dataOrigen=eval('parent.parent.cabecera.document.frmUbigeo.Dept');
	if (dataOrigen) {
		strTmp=dataOrigen.value;
		aTmp=strTmp.split(":");
		n=aTmp.length-1;
		for(i=0;i<=n;i++) {
			aData=aTmp[i].split("|");
		 	strNombre=aData[1];
 			strValor=aData[0];
			if (codDpto==strValor) {
				resp = strNombre;	
			}
		}
	}
	return resp;
}

function Obt_Prov_If(codDpto,codProv) {
	var resp = "";
	var dataOrigen=eval('parent.parent.cabecera.document.frmUbigeo.Prov'+codDpto);
	if (dataOrigen) {
		strTmp=dataOrigen.value;
		aTmp=strTmp.split(":");
		n=aTmp.length-1;
		for(i=0;i<=n;i++) {
			aData=aTmp[i].split("|");
		 	strNombre=aData[1];
 			strValor=aData[0];
			if (codProv==strValor) {
				resp = strNombre;	
			}
		}
	}
	return resp;
}

function Obt_Dist_If(codProv,codDist) {
	var resp = "";
	var dataOrigen=eval('parent.parent.cabecera.document.frmUbigeo.Dist'+codProv);
	if (dataOrigen) {
		strTmp=dataOrigen.value;
		aTmp=strTmp.split(":");
		n=aTmp.length-1;
		for(i=0;i<=n;i++) {
			aData=aTmp[i].split("|");
		 	strNombre=aData[1];
 			strValor=aData[0];
			if (codDist==strValor) {
				resp = strNombre;	
			}
		}
	}
	return resp;
}

function Obt_CPost_If(codDist) {
	var resp = "";
	var dataOrigen=eval('parent.parent.cabecera.document.frmUbigeo.CPost'+codDist);
	if (dataOrigen) {
		resp = dataOrigen.value;	
	}
	return resp;
}

function CargaComboIf(origen, sel) {
	strNombre=" ";
	strValor="xx";
	document.frmPrincipal.cboProv.length=0;
	document.frmPrincipal.cboProv.options[0]=new Option(strNombre,strValor);
	document.frmPrincipal.cboDstr.length=0;
	document.frmPrincipal.cboDstr.options[0]=new Option(strNombre,strValor);
	document.frmPrincipal.txtCodPostal.value = "";
	if (origen == "xx") {
		return;
	};
	var dataOrigen=eval('parent.parent.cabecera.document.frmUbigeo.Prov' + origen);
	if (dataOrigen) {
		strTmp=dataOrigen.value;
		aTmp=strTmp.split(":");
		n=aTmp.length-1;
		for(i=0;i<=n;i++) {
			aData=aTmp[i].split("|");
		 	strNombre=aData[1];
 			strValor=aData[0];
			document.frmPrincipal.cboProv.options[i+1]=new Option(strNombre,strValor);
			if (sel==strValor) {
				document.frmPrincipal.cboProv.selectedIndex = i+1;
			}
	 	}
	}
}

function CargaComboIIf(origen, sel) {
	strNombre=" ";
	strValor="xx";
	document.frmPrincipal.cboDstr.length=0;
	document.frmPrincipal.cboDstr.options[0]=new Option(strNombre,strValor);
	document.frmPrincipal.txtCodPostal.value = "";
	if(origen == "xx") {
		return;
	};
	var dataOrigen=eval('parent.parent.cabecera.document.frmUbigeo.Dist' + origen);
	if (dataOrigen) {
		strTmp=dataOrigen.value;
		aTmp=strTmp.split(":");
		n=aTmp.length-1;
		for(i=0;i<=n;i++) {
			aData=aTmp[i].split("|");
			strNombre=aData[1];
			strValor=aData[0];
			document.frmPrincipal.cboDstr.options[i+1]=new Option(strNombre,strValor);
			if (sel==strValor) {
				document.frmPrincipal.cboDstr.selectedIndex = i+1;
			}
		}
	}
}

function CargaCodPostIf(origen) {
	document.frmPrincipal.txtCodPostal.value = "";
	if(origen == "xx") {
		return;
	};
	var dataOrigen=eval('parent.parent.cabecera.document.frmUbigeo.CPost' + origen);
	if (dataOrigen) {
		document.frmPrincipal.txtCodPostal.value=dataOrigen.value;
	}
}