BAV.Disable = 
{
    hideStatus : true,
    disableContextMenu : true,
    disableBackSpace : true,
    disableCopyContent: true,
    disableEFEDOCE: true,
    disableCtrlAlt:true,
    messageStatus : 'BAV - Banco Agrícola de Venezuela',
    messageContextMenu : 'Funcionalidad no permitida',
    Hidestatus : function(){window.status=BAV.Disable.messageStatus; return true;},
	Event : 'undefined',
	Init : function()
    {
        window.status=BAV.Disable.messageStatus;
        this.Event = window.event;
        
        if(this.disableContextMenu)
        {
			document.oncontextmenu = function(){alert(BAV.Disable.messageContextMenu); return false};
        }
		if(this.hideStatus)
        {
            if (document.layers)
	            document.captureEvents(this.Event.MOUSEOVER | this.Event.MOUSEOUT);
            document.onmouseover=this.Hidestatus;
            document.onmouseout=this.Hidestatus;
        }
		if(this.disableBackSpace)
        {
            if (typeof this.Event != 'undefined') // IE
                document.onkeydown = function() // IE
                {
                    var t=BAV.Disable.Event.srcElement.type;
                    var kc=BAV.Disable.Event.keyCode;
                    if ((kc != 8 && kc != 13) || ( t == 'text' &&  kc != 13 ) ||
                        (t == 'textarea') || ( t == 'submit' &&  kc == 13)|| 
                             ( t == 'password' &&  kc != 13))
                        return true
                    else 
                        return false
                }
            else
                document.onkeypress = function(e)  // FireFox/Otros 
                {
                    var t=e.target.type;
                    var kc=e.keyCode;
                    
                    if ((kc != 8 && kc != 13) || ( t == 'text' &&  kc != 13 ) ||
                        (t == 'textarea') || ( t == 'submit' &&  kc == 13)|| 
                             ( t == 'password' &&  kc != 13))
                        return true
                    else 
                        return false
                }
        }
        if(this.disableCopyContent)
        {
            var target = $$BAV(document,'body')[0];
            if(target)
            {
		        if (typeof target.onselectstart!="undefined") //IE route
			        target.onselectstart = function(){return false};
		        else 
			        if (typeof target.style.MozUserSelect!="undefined") //Firefox route
				        target.style.MozUserSelect = "none";
			        else //All other route (ie: Opera)
				        target.onmousedown=function(){return false}; 
		        target.style.cursor = "default"; 
		    }
        }
        if (this.disableEFEDOCE) {
            document.onkeypress = function (event) {
                event = (event || window.event);
                if (event.keyCode == 123) {
                    return false;
                }
            }
            document.onmousedown = function (event) {
                event = (event || window.event);
                if (event.keyCode == 123) {
                    return false;
                }
            }
            document.onkeydown = function (event) {
                event = (event || window.event);
                if (event.keyCode == 123) {
                    return false;
                }
            }
        }
        if (this.disableCtrl) {
            window.onkeypress = function(event){ return disableKeys(event);} 
            window.onkeydown = function (event) { return disableKeys(event); }
            window.ondragstart = function (event) { return false; }
            window.ondrop = function (event) { return false; }
            function disableKeys(e){

                var ev=(!e)?window.event:e;//IE:Moz
                
                if((ev.ctrlKey==true))
                { 
                    return false;
                }
            }
        }
    }
};
window.onload = function(){BAV.Disable.Init();}; 

