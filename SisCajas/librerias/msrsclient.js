//
//  msrsClient.js
//  A client for Remote Scripting supporting the MSRS (Microsoft) Remote Scripting
//  protocol.  This code is based on JSRS (by Brent Ashley - jsrs@megahuge.com)
//  and has been adapted to the MSRS protocol with his permission.
//
//  SYNOPSIS:  Make MSRS protocol asynchronous Remote Scripting calls to the server
//             side without an applet.  Can be used in .NET with Thycotic.Web.RemoteScripting
//             http://www.thycotic.com/dotnet_remotescripting.html
//
//  You can get the latest version of this library here:
//  http://www.thycotic.com/dotnet_remotescripting_client.html
//  Please report bugs and enhancement requests here too.
//
//  Author:  
//    Jonathan Cogley
//    Thycotic Software Ltd
//    http://www.thycotic.com
//
//  See license.txt for copyright and license information - this is carried forward
//  from Brent's original license.
// 
//  Changes:
//  0.21    02/25/2003
//          Fixed bug with window opening when using POST
//  0.2     02/24/2003
//          Added support for HTTP POST.
//  0.1     02/10/2003
//          Initial release - only supports GET and asynchronous calls.
//          Does not support the RSGetASPObject - yet!
//

var msrsVersion = 0.21;

// callback pool needs global scope
var msrsContextPoolSize = 0;
var msrsContextMaxPool = 100;
var msrsContextPool = new Array();
var msrsBrowser = msrsBrowserSniff();
var msrsPOST = false;
var msrsVisibility = false;

// constructor for context object
function msrsContextObj( contextID ){
  // properties
  this.id = contextID;
  this.busy = true;
  this.callback = null;
  this.errorcallback = null;
  this.context = null;
  this.container = contextCreateContainer( contextID );
  this.request = null;  
  // methods
  this.GET = contextGET;
  this.POST = contextPOST;
  this.setVisibility = contextSetVisibility;
}

//  method functions are not privately scoped 
//  because Netscape's debugger chokes on private functions
function contextCreateContainer( containerName ){
  // creates hidden container to receive server data 
  var container;
  switch( msrsBrowser ) {
    case 'NS':
      container = new Layer(100);
      container.contextID = containerName;
      container.name = containerName;
      container.visibility = 'hidden';
      container.clip.width = 100;
      container.clip.height = 100;
      break;
    
    case 'IE':
      document.body.insertAdjacentHTML( "afterBegin", '<span id="SPAN' + containerName + '"></span>' );
      var span = document.all( "SPAN" + containerName );
      var html = '<iframe contextID="' + containerName + '" onload="contextLoaded(this,this.contentWindow.document.documentElement.innerHTML)" name="' + containerName + '" src=""></iframe>';
      span.innerHTML = html;
      span.style.display = 'none';
      container = window.frames[ containerName ];
      break;
      
    case 'MOZ':  
      var span = document.createElement('SPAN');
      span.id = "SPAN" + containerName;
      document.body.appendChild( span );
      var iframe = document.createElement('IFRAME');
      iframe.contextID = containerName;
      iframe.onload = function() { contextLoaded(this,this.contentWindow.document.documentElement.innerHTML); };
      iframe.name = containerName;
      span.appendChild( iframe );
      container = iframe;
      break;
  }
  return container;
}

function contextPOST( rsPage, func, parms ){
  var d = new Date();
  var unique = d.getTime() + '' + Math.floor(1000 * Math.random());
  
  var doc = (msrsBrowser == "IE" ) ? this.container.document : this.container.contentDocument;
  this.container.inrequest = true;
  doc.open();
  doc.write('<html><body>');
  doc.write('<form name="msrsForm" method="post" target="" ');
  doc.write(' action="' + rsPage + '?U=' + unique + '">');
  doc.write('<input type="hidden" name="C" value="' + this.id + '">');

  // write the method to call and parameters as hidden form inputs
  if (func != null){
    doc.write('<input type="hidden" name="_method" value="' + func + '">');
    if (parms != null || parms.length == 0){
		// assume parms is array of strings
		for( var i=0; i < parms.length; i++ ){
			doc.write( '<input type="hidden" name="p' + i + '" '
                   + 'value="' + msrsEscapeQQ(parms[i]) + '">');
		}
		doc.write( '<input type="hidden" name="pcount" '
                   + 'value="' + parms.length + '">');
    } else {
		doc.write( '<input type="hidden" name="pcount" '
                   + 'value="0">');
    } // parms
  } // func

  doc.write('</form></body></html>');
  doc.close();
  doc.forms['msrsForm'].submit();
}

function contextGET( rsPage, func, parms ){
  // build URL to call
  var URL = rsPage;
  // always send context
  URL += "?C=" + this.id;
  if (func != null){
	URL += "&_method=" + escape(func);
	if (parms != null || parms.length == 0){
		// assume parms is array of strings
		for( var i=0; i < parms.length; i++ ){
			URL += "&p" + i + "=" + escape(parms[i]+'') + "";
		}
		URL += "&pcount=" + parms.length;
    } else {
		URL += "&pcount=0";
    } // parms
  } // func

  // unique string to defeat cache
  var d = new Date();
  URL += "&U=" + d.getTime();
  // make the call
  switch( msrsBrowser ) {
    case 'NS':
      this.container.src = URL;
      break;
    case 'IE':
      this.container.document.location.replace(URL);
      break;
    case 'MOZ':
      this.container.src = '';
      this.container.src = URL; 
      break;
  }  
  
}

function contextSetVisibility( vis ){
  switch( msrsBrowser ) {
    case 'NS':
      this.container.visibility = (vis)? 'show' : 'hidden';
      break;
    case 'IE':
      document.all("SPAN" + this.id ).style.display = (vis)? '' : 'none';
      break;
    case 'MOZ':
      document.getElementById("SPAN" + this.id).style.visibility = (vis)? '' : 'hidden';
      this.container.width = (vis)? 250 : 0;
      this.container.height = (vis)? 100 : 0;
      break;
  }  
}

// end of context constructor

function msrsGetContextID(){
  var contextObj;
  for (var i = 1; i <= msrsContextPoolSize; i++){
    contextObj = msrsContextPool[ 'msrs' + i ];
    if ( !contextObj.busy ){
      contextObj.busy = true;      
      return contextObj.id;
    }
  }
  // if we got here, there are no existing free contexts
  if ( msrsContextPoolSize <= msrsContextMaxPool ){
    // create new context
    var contextID = "msrs" + (msrsContextPoolSize + 1);
    msrsContextPool[ contextID ] = new msrsContextObj( contextID );
    msrsContextPoolSize++;
    return contextID;
  } else {
    alert( "msrs Error:  context pool full" );
    return null;
  }
}

function RSExecute( rspage, method ) {
  // get pooling context
  var contextObj = msrsContextPool[ msrsGetContextID() ];
  // get parameters
  var parameters = new Array();	
  var callback = null;
  var errorcallback = null;
  var context = null;
  var asynchronous = false;  // this is still being tested
  var finishedParameters = false;
  var length = RSExecute.arguments.length;
  var arg;
  for (var n=2; n < length; n++) {
    arg = RSExecute.arguments[n];
	if (typeof(arg) == 'function') {
	  asynchronous = true;
	  finishedParameters = true;
	  if (callback == null) {
	    callback = arg;
	  } else {
	    errorcallback = arg; 
	    break;
	  }
	} else if (!finishedParameters) {
		parameters[parameters.length] = arg;
	} else {
		context = arg;
	}
  }
  
  // assign callbacks and context
  contextObj.callback = callback;
  contextObj.errorcallback = errorcallback;
  contextObj.context = context;

  // set visible if set
  contextObj.setVisibility( msrsVisibility );

  if (msrsPOST && ((msrsBrowser == 'IE') || (msrsBrowser == 'MOZ'))){
    contextObj.POST( rspage, method, parameters );
  } else {
    // This doesn't seem to work (attempted getting synchronously ...)
    //var command = "msrsContextPool[" + contextObj.id + "].GET( \"" + rspage + "\",\"" + method + "\",new Array());"; 
	//alert(command);
	//setTimeout(command,100);
    contextObj.GET( rspage, method, parameters );
  }  
  
  //var request = null;
  // still being tested - this doesn't appear to work
  // wait if necessary
  //if (!asynchronous) {
    // wait
    //while (contextObj.busy) {
  	//	request = contextObj.request;
    //}
  //}
  //return request;
  return contextObj.id;
}

function msrsEscapeQQ( thing ){
  return thing.replace(/'"'/g, '\\"');
}

function msrsBrowserSniff(){
  if (document.layers) return "NS";
  if (document.all) return "IE";
  if (document.getElementById) return "MOZ";
  return "OTHER";
}

/////////////////////////////////////////////////
//
// user functions

function msrsDebugInfo(){
  // use for debugging by attaching to f1 (works with IE)
  // with onHelp = "return msrsDebugInfo();" in the body tag
  var doc = window.open().document;
  doc.open;
  doc.write( 'Pool Size: ' + msrsContextPoolSize + '<br><font face="arial" size="2"><b>' );
  for( var i in msrsContextPool ){
    var contextObj = msrsContextPool[i];
    doc.write( '<hr>' + contextObj.id + ' : ' + (contextObj.busy ? 'busy' : 'available') + '<br>');
    doc.write( contextObj.container.document.location.pathname + '<br>');
    doc.write( contextObj.container.document.location.search + '<br>');
    doc.write( '<table border="1"><tr><td>' + contextObj.container.document.body.innerHTML + '</td></tr></table>' );
  }
  doc.write('</table>');
  doc.close();
  return false;
}

//*****************************************************************
// Handle parsing the data when loading and creating the msrs return object
//*****************************************************************
function contextLoaded(container,data) {
	// check that it contains successfully returned data
	if (data.indexOf('<METHOD') == -1 && data.indexOf('<method') == -1) {
		return;
	}
	// get context object and invoke callback
	var contextObj = msrsContextPool[ container.contextID ];
	var request = new RSCallObject();
	request.data = data;
	request.context = contextObj.context;
	evalRequest(request);
	contextObj.request = request;
	if (request.status != MSRS_INVALID) {
		if (request.status == MSRS_FAIL)
		{	
			if (typeof(contextObj.error_callback) == 'function')
			{
				contextObj.error_callback(request);
			}
			else 
			{
				alert('Remote Scripting Error\n' + request.message);
			}
		}
		else
		{
			if (typeof(contextObj.callback) == 'function')
			{
				contextObj.callback(request);
			}
		}	
		// clean up and return context to pool
		contextObj.callback = null;
		contextObj.busy = false;
	}
}


//*****************************************************************
// Constants from rs.htm for MSRS
//*****************************************************************
var MSRS_FAIL = -1;
var MSRS_COMPLETED = 0;
var MSRS_PENDING = 1;
var MSRS_PARTIAL = 2;
var MSRS_INVALID = 3;
	
//*****************************************************************
// function evalRequest(request)
//
//	This function evaluates the data returned to the request. 
//	Marshalled jscript objects are re-evaluated on the client.
//*****************************************************************
function evalRequest(request)
{
	var data = request.data;
	var start_index = 0;
	var end_index = 0;
	var start_key = '<' + 'return_value';
	var end_key = '<' + '/return_value>';
	// check if there otherwise switch case 
	if (data.indexOf(start_key) == -1) {
		start_key = start_key.toUpperCase();
		end_key = end_key.toUpperCase();
	}
	if ((start_index = data.indexOf(start_key)) != -1)
	{
		var data_start_index = data.indexOf('>',start_index) + 1;
		end_index = data.indexOf(end_key,data_start_index);
		if (end_index == -1) 
			end_index = data.length;
		var metatag = data.substring(start_index,data_start_index);
		if (metatag.indexOf('SIMPLE') != -1)
		{
			request.return_value = unescape(data.substring(data_start_index,end_index));
		}
		else if (metatag.indexOf('EVAL_OBJECT') != -1)
		{
			request.return_value = data.substring(data_start_index,end_index);
			request.return_value = eval(unescape(request.return_value));
		}
		else if (metatag.indexOf('ERROR') != -1)
		{
			request.status = MSRS_FAIL;
			request.message = unescape(data.substring(data_start_index,end_index));		
		}
	}
	else
	{
		request.status = MSRS_INVALID;
		request.message = 'REMOTE SCRIPTING ERROR: Page invoked does not support remote scripting.';			
		// extra debug for errors
		var win = window.open('','_blank','height=600,width=450,scrollbars=yes');
		win.document.write( request.data );
	}
}

function RSCallObject()
{
	this.status = MSRS_PENDING;
	this.message = '';
	this.data = '';
	this.return_value = '';
	this.context = null;
}
