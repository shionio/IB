if (!window.$BAV) { 
    var $BAV = function(x)
    {
        var result = undefined;
         if (document.getElementById)
            result = document.getElementById(x);
         else if (document.all) 
            result = document.all[x];
         return result;
    };
}

if (!window.$$BAV) 
{ 
    var $$BAV = function(element, tagName)
    {
        if (element && tagName) {
            if (element.getElementsByTagName) {
                return element.getElementsByTagName(tagName);
            }
            if (element.all && element.all.tags) {
                return element.all.tags(tagName);
            }
        }
        return null;
    }; 
}

if (!window.BAV) 
{ 
    var BAV = 
    {
        Debug : false,
        Head: $$BAV(document, 'head')[0],
        Body: $$BAV(document, 'body')[0],
        Doc : document,
        Ele : document.documentElement,
        CreateMetaTag : function (name, content)
        {
            var meta = this.Doc.createElement('meta');
            meta.name = name;
            meta.content = content;
            this.Head.appendChild(meta);
        },
        ScriptLoader : function(u, t)
		{
			var sc = this.Doc.createElement('script');
			sc.src = u;
			sc.type = t;
			sc.language='javascript';
			this.Head.appendChild(sc);
		},
		getObj : function(name)
		{
            this.obj = $(name);
            this.style = this.obj.style;
        },
        getWinSize : function getWinSize()
        {
            var iWidth = 0, iHeight = 0;
            if (this.Ele && this.Ele.clientHeight)
            {
                iWidth = parseInt(window.innerWidth,10);
                iHeight = parseInt(window.innerHeight,10);
            }
            else if (this.Body)
            {
                iWidth = parseInt(this.Body.offsetWidth,10);
                iHeight = parseInt(this.Body.offsetHeight,10);
            }
            return {width:iWidth, height:iHeight};
        },
        resizeObj : function(obj) 
        {
			var oContent = new this.getObj(obj);
			var oWinSize = this.getWinSize();
			var h = oWinSize.height - parseInt(oContent.obj.offsetTop,10)-10;
            if ((oWinSize.height - parseInt(oContent.obj.offsetTop,10))>0)
			    oContent.style.height = h+"px";
		}
    }; 
}
