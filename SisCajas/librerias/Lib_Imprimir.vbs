
Sub AbreCajonera()
On Error Resume Next

	Set obCajonera = CreateObject("Etiquetera.clsImpresora")
	obCajonera.AbreCajonera
	Set fso = Nothing

end sub