



var my_window = "";

$().ready(function() {
    cargarTimers();
	
	
    $("#btnRefresh").click(function () {
        //window.location.reload(); //= "/frame.aspx";
        reiniciarTimer();
        $.ajax({
            type: "POST",
            url: urlUpdate,
            contentType: "application/json; charset=utf-8",
            data: "{ \"sesion\" : \""+sesionVal+"\" }",
            dataType: "json",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Content-type",
                                         "application/json; charset=utf-8");
            },
            success: function (res) {
                //alert(JSON.stringify(res));
                res = res.d;
                if (res.Resultado == "OK") {
                    
                    
                }

            },
            error: function (res) {
                
                //that.trigger('load-error', res.status);
            },
            complete: function () {
                $('#myModalLogout').modal('hide');
                
            }
        });            

    });

    	menuLinkChanger();
		             
//alert(reverse("HOLA"));
		//alert("antesss" + hex_sha512("33333"));
		//Script.load("sha512.js"); // includes code for myFancyMethod();
		//alert("mientras");

		//alert(sha512_vm_test());
		//alert("despues");

		
});

function reverse(s){
    return s.split("").reverse().join("");
}

//$$$ Function to change dinamically the links in absence of source
function menuLinkChanger(){

	
	 menuItem = document.getElementById("BAVMenu1_rptMenuLvl1_ctl03_liChilds");	 

	 var numAfiliacionPos = 0;
     var numTransacccionesPos = 1;
	 var numFavoritosPos = 2;
	 
	 var afiliacionLink = menuItem.children[1].children[numAfiliacionPos].innerHTML;
	 afiliacionLink = afiliacionLink.replace('P2P123',sesionVal);
	 menuItem.children[1].children[numAfiliacionPos].innerHTML=afiliacionLink;
	 
	 var transacccionesLink = menuItem.children[1].children[numTransacccionesPos].innerHTML;
	 transacccionesLink = transacccionesLink.replace('P2P123',sesionVal);
	 menuItem.children[1].children[numTransacccionesPos].innerHTML=transacccionesLink;

	 var favoritosLink = menuItem.children[1].children[numFavoritosPos].innerHTML;
	 favoritosLink = favoritosLink.replace('P2P123',sesionVal);
	 menuItem.children[1].children[numFavoritosPos].innerHTML=favoritosLink;
	 
	 //*****
	 
	 var afiliacionLink = menuItem.children[1].children[3].innerHTML;
	 afiliacionLink = afiliacionLink.replace('P2P123',sesionVal);
	 menuItem.children[1].children[3].innerHTML=afiliacionLink;
	 
	 var transacccionesLink = menuItem.children[1].children[4].innerHTML;
	 transacccionesLink = transacccionesLink.replace('P2P123',sesionVal);
	 menuItem.children[1].children[4].innerHTML=transacccionesLink;

	 var favoritosLink = menuItem.children[1].children[5].innerHTML;
	 favoritosLink = favoritosLink.replace('P2P123',sesionVal);
	 menuItem.children[1].children[5].innerHTML=favoritosLink;
	 
}





function reiniciarTimer()
{
    $(document).stopTime();
    cargarTimers();
}

function cargarTimers()
{    
    $(document).oneTime(timeSessionMessage, function() {  
        //my_window = window.open(urlMensaje,"InactividadIBBAV","status=1,width=450,height=380");
        //my_window.focus();        
        $('#myModalLogout').modal('show');
            $(document).oneTime(timeSessionClose, function () {
            //window.location.href = urlSalir;            
            window.parent.location.href = urlSalir;
        });
    });
    


}
/*
var my_window = "";

$().ready(function () {
    cargarTimers();
});

function reiniciarTimer() {
    $(document).stopTime();
    cargarTimers();
}

function cargarTimers() {
    $(document).oneTime(timeSessionMessage, function () {
        my_window = window.open(urlMensaje, "InactividadWindow", "status=1,width=350,height=240");
        my_window.focus();


        $(document).oneTime(timeSessionClose, function () {
            my_window.close();
            window.location.href = urlSalir;
        });
    });
}
*/