<%
    Public Function FormatAsMoney(sVal)
	
	dim sBegin,sEnd
	dim IFoundPos,IExtraNumber,ICommaCounter,ICurrentPos,sCurStr,stmp
	dim sFirstNumber,sSecondNumber
	dim ICarry

	sBegin = ""
	sEnd = ""

        IFoundPos = 0
    	IExtraNumber = 0
    	ICommaCounter = 0
    	ICurrentPos = 0
    	ICarry=0
	sCurStr = ""
    	stmp = cstr(sval)
    
'calculate the correct amount of spare change'    
	IFoundPos = instr(1,stmp,".")    	
	if IFoundPos = 0 Then
	   sEnd = "00"
	   sBegin = mid(stmp,1,len(stmp))
	else
	   sEnd = mid(stmp,IFoundPos + 1,len(stmp))
	   sBegin = mid(stmp,1,IFoundPos - 1)
	   'make sure we are ONLY 2 cents long'
	   if len(sEnd) > 2 Then '><
		sFirstNumber = CINT(mid(sEnd,1,1))
		sSecondNumber = CINT(mid(sEnd,2,1))
		IExtraNumber = CINT(mid(sEnd,3,1))
		sEnd = mid(sEnd,1,2)
		sEnd = cint(sEnd)
		if IExtraNumber > 5 Then 
			'sEnd = sEnd + 1'
			sSecondNumber = sSecondNumber + 1
			if len(cstr(sSecondNumber)) > 1 Then '<make sure we carry the number
				sFirstNumber = sFirstNumber + 1
				sSecondNumber = mid(cstr(sSecondNumber),2,1)
			End if
			if len(cstr(sFirstNumber)) > 1 Then '<make sure we carry the number
				sBegin = cint(sBegin) + 1
				sFirstNumber = mid(cstr(sFirstNumber),2,1)    				
			end if    				    				
		End if
		sEnd = cstr(sFirstNumber) & cstr(sSecondNumber)
	   End if
    	End if
    	if len(sEnd) = 0 Then sEnd = "00"
    	if len(sEnd) = 1 Then sEnd = sEnd & "0"'
'add commas'
    	IExtraNumber = len(sBegin)
    	if IExtraNumber > 3 Then '><
    		For ICurrentPos = 1 To IExtraNumber
    			ICommaCounter = ICommaCounter + 1
    			sCurStr = mid(Right(sBegin, ICurrentPos),1,1) & sCurStr
    			if ICommaCounter = 3 Then
    				ICommaCounter = 0
    				sCurStr = "," & sCurStr
    			End if
    		next
    		sBegin = sCurStr
    	End if
    	if mid(sBegin,1,1) = "," Then
    		sBegin = mid(sBegin,2,len(sBegin))
    	End if
'    	Response.write("$" & sBegin & "." & sEnd)
    	Response.write(sBegin & "." & sEnd)
    End Function
    
    
    Function FormatDate(strDate)
    Dim strAuxDate
    Dim intAux
    	if len(trim(strDate))>0 then
    	      if  instr(strDate,"/")>0 then 
		strDate=trim(strDate)
		intAux = cint(left(strDate,instr(strDate,"/")-1))
		if intAux<10 then
			strAuxDate = "0" + cstr(intAux)
		else
			strAuxDate = cstr(intAux)
		end if
		strDate=right(strDate,len(strDate)-instr(strDate,"/"))
		intAux = cint(left(strDate,instr(strDate,"/")-1))
		if intAux<10 then
			'strAuxDate =  "0" + cstr(intAux) + "-" + strAuxDate
			strAuxDate =  strAuxDate + "-" +  "0" + cstr(intAux) 
		else
			strAuxDate = strAuxDate + "-" + cstr(intAux)
		end if    		
		strAuxDate = strAuxDate + "-" + right(strDate,4)    	
	     else
	     	strAuxDate = strDate
	     end if
	end if
    	FormatDate=strAuxDate
    End Function
    
    Function FormateFechaStyle(strDate)
    	FormateFechaStyle = left(strDate,2) & "-" & mid(strDate,3,2) & "-" & right(strDate,4)
    End Function
    
    Function FormatDateLess(strDate)
    Dim strAuxDate
    Dim intAux
    	if len(trim(strDate))>0 then
		strDate=trim(strDate)
		intAux = cint(left(strDate,instr(strDate,"/")-1))
		if intAux<10 then
			strAuxDate = "0" + cstr(intAux)
		else
			strAuxDate = cstr(intAux)
		end if
		strDate=right(strDate,len(strDate)-instr(strDate,"/"))
		intAux = cint(left(strDate,instr(strDate,"/")-1))
		if intAux<10 then
			'strAuxDate =  "0" + cstr(intAux) + "-" + strAuxDate
			strAuxDate =  strAuxDate + "0" + cstr(intAux) 
		else
			strAuxDate = strAuxDate + cstr(intAux)
		end if    		
		strAuxDate = strAuxDate + right(strDate,4)    	
	end if
    	FormatDateLess=strAuxDate
    End Function
    
    
%>
