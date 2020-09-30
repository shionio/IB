function Emitir(){
	if (validarDatosCliente()){
		mostrarResumen();
	}
}

function mostrarResumen() {
    $("#divResumen").show();
    return true;
}



function validarDatosCliente() {
	var result = true;
	var iError = 0;
	
	$("#Step_1").find("input, select").each(function(i, input){
		var inp = $(input);
		if($(inp).val()=="0" || inp.val().trim()==""){
			inp.parent().parent().addClass("has-error");
			inp.focus();
			iError++;
			
		}else{
			inp.parent().parent().removeClass("has-error").addClass("has-success");
			
		}
	});
	result = iError == 0;
	
    return result;
}

jQuery.fn.serializeJSON=function() {
  var json = {};
  jQuery.map(jQuery(this).serializeArray(), function(n, i) {
    var _ = n.name.indexOf('[');
    if (_ > -1) {
      var o = json;
      _name = n.name.replace(/\]/gi, '').split('[');
      for (var i=0, len=_name.length; i<len; i++) {
        if (i == len-1) {
          if (o[_name[i]]) {
            if (typeof o[_name[i]] == 'string') {
              o[_name[i]] = [o[_name[i]]];
            }
            o[_name[i]].push(n.value);
          }
          else o[_name[i]] = n.value || '';
        }
        else o = o[_name[i]] = o[_name[i]] || {};
      }
    }
    else {
      if (json[n.name] !== undefined) {
        if (!json[n.name].push) {
          json[n.name] = [json[n.name]];
        }
        json[n.name].push(n.value || '');
      }
      else json[n.name] = n.value || '';      
    }
  });
  return json;
};

$(document).on("ready", function(){
	$("#exit").on("click",function(){
		//window.close();	
	});
});
