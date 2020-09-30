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

    
});

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