BAV.Form =
{
    SetUniqueRadioButton : function(nameregex, current)
    {
        re = new RegExp(nameregex);
        for(i = 0; i < document.forms[0].elements.length; i++)
        {

            elm = document.forms[0].elements[i]

            if (elm.type == 'radio')
            {
                if (re.test(elm.name))
                {
                    elm.checked = false;
                }
            }
        }
        current.checked = true;
    },
    SetTextValue : function (nameregex, val)
    {
	    re = new RegExp(nameregex);
	    for(i = 0; i < document.forms[0].elements.length; i++)
        {
            elm = document.forms[0].elements[i]
            if (elm.type == 'text')
            {
                if (re.test(elm.id))
                {
                    elm.value = val;
                }
            }
        }
    },
    PrintToFrame : function (title, paneles, css)
    {
        try
        {
            var oIframe;
            if(BAV.BrowserDetect.browser=="Chrome")
            {
                oIframe = window.open('','print');
                oIframe.focus();
            }
            else
            {
                oIframe = document.createElement("IFRAME");
                oIframe.setAttribute("id", "iImprimir"); 
                oIframe.setAttribute("name", "iImprimir"); 
                oIframe.setAttribute("src", ""); 
                oIframe.setAttribute("frameborder", "0"); 
                oIframe.style.width = 0+"px";
                oIframe.style.height = 0+"px";
                BAV.Body.appendChild(oIframe); 
            }
            
            var oDoc;
            if(BAV.BrowserDetect.browser!="Chrome")
                oDoc = (oIframe.contentWindow || oIframe.contentDocument);
            else
                oDoc = oIframe;
            if (oDoc.document) oDoc = oDoc.document;
            oDoc.write("<html><head>");
            oDoc.write("<style type='text/css'>");
            oDoc.write("HTML { MARGIN: 0px }");
            oDoc.write("BODY { MARGIN: 0px }");
            oDoc.write("@media Print { * { BACKGROUND-IMAGE: none! important; BACKGROUND-COLOR: white! important }}");
            oDoc.write(".romper{page-break-after: always;}");
            oDoc.write("</style>");
            oDoc.write("<link type='text/css' rel='stylesheet' href='" + css + "' />");
            oDoc.write("</head><body onload='this.focus();this.print();'>");
            var i = 0;
            for( i = 0; i < paneles.length; i++)
            {
                if(i == (paneles.length - 1))
                {
                    if($BAV(paneles[i]))
                        oDoc.write($BAV(paneles[i]).innerHTML);
                    else
                        oDoc.write(paneles[i].innerHTML);
                }
                else
                {
                    oDoc.write("<div class='romper'>");
                    if($BAV(paneles[i]))
                        oDoc.write($BAV(paneles[i]).innerHTML);
                    else
                        oDoc.write(paneles[i].innerHTML);
                    oDoc.write("</div>");
                }
            }
            oDoc.write("</body></html>");
            oDoc.close();
            oIframe.onload = new function(){setTimeout("if(BAV.BrowserDetect.browser=='Chrome') {oIframe.close();} else {BAV.Body.removeChild(oIframe);}",2000);};
        }
        catch(e)
        {
            self.print();
        }
    },
    BlockButtonByName : function (btns, dis)
    {
		
		for (var i = 0; i < btns.length; i++ )
        {
			var element = document.getElementById(btns[i]);
			if(element)
				element.disabled = dis;
        }
    },
    BlockElemetByType : function (tagnames, stype, value)
    {
        for (var tag = 0;tag < tagnames.length; tag++)
        {
            var elements = $$BAV(document,tagnames[tag]);
            for (var element = 0;element < elements.length;element++)
                if(typeof stype != 'undefined')
                {
                    for (var typ=0;typ< stype.length;typ++)
                        if(elements[element].type == stype[typ])
                            elements[element].disabled = value;
                }
                else
                {
                    elements[element].disabled = value;
                }
        }
    }
};

function SetUniqueRadioButton(nameregex, current)
{
    re = new RegExp(nameregex);
    for(i = 0; i < document.forms[0].elements.length; i++)
    {

        elm = document.forms[0].elements[i]

        if (elm.type == 'radio')
        {
            if (re.test(elm.name))
            {
                elm.checked = false;
            }
        }
    }

    current.checked = true;
}

function limpiartext(nameregex)
{
	re = new RegExp(nameregex);
	for(i = 0; i < document.forms[0].elements.length; i++)
    {

        elm = document.forms[0].elements[i]

        if (elm.type == 'text')
        {
            if (re.test(elm.id))
            {
                elm.value = '';
            }
        }
    }
}

