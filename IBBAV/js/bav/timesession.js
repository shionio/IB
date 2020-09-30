//var timedouturl = ;//"/login.aspx?msg=1";
var min;
var sec;
BAV.TimeOut = 
{
    Minutes : function (data) 
    {
		for (var i = 0; i < data.length; i++)
			if (data.substring(i, i + 1) == ":")
			break;
		return (data.substring(0, i));
	},
	Seconds : function (data) 
	{
		for (var i = 0; i < data.length; i++)
			if (data.substring(i, i + 1) == ":")
			break;
		return (data.substring(i + 1, data.length));
	},
	Display : function (min, sec) 
	{
		var disp;
		if (min <= 9) disp = " 0";
		else disp = " ";
		disp += min + ":";
		if (sec <= 9) disp += "0" + sec;
		else disp += sec; 
		return (disp);
	},
	WriteCurrentTimeStatus : function ()
	{
	    //window.status = textCurrent + 
	    BAV.TimeOut.Display(min, sec);
	    return true;
	},
	Down : function ()
	{
	    if(htime.value != hcurrenttime.value)
		{
			BAV.TimeOut.TimeIt();
			return;
		}
		if(min > 0 || sec > 0)
		{
			sec--;   
			if (sec == -1) { sec = 59; min--; }
			$BAV("time").innerHTML = BAV.TimeOut.Display(min, sec);
			BAV.TimeOut.WriteCurrentTimeStatus();
			if(min == 0 && sec == 59) 
			{
			    if($BAV("time").style.display == 'none')
			    {	
			        //alert(textAlert);
			        //$BAV("time").style.display = 'block';
			    }
			}
			
		}
		if (min == 0 && sec == 0) {
			$BAV("time").innerHTML = BAV.TimeOut.Display(min, sec);
			//alert(textFinished);
			window.parent.location.href = loginurl;
		}
		else down = setTimeout("BAV.TimeOut.Down()", 1000);
	},
	TimeIt : function ()
	{
	    hcurrenttime.value = timeoutsession;
		htime.value = hcurrenttime.value  ;
		min = 1 * BAV.TimeOut.Minutes(htime.value);
		sec = 0 + BAV.TimeOut.Seconds(htime.value);
		$BAV("time").innerHTML = BAV.TimeOut.Display(min, sec);
		this.Down();
	}
};        