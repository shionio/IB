
function OnlyNumeric(e, valor) {
    var charCode;
    if (navigator.appName == 'Netscape')
        charCode = e.which;
    else
        charCode = e.keyCode;

    if (charCode == 13)
        return true;
    if (charCode == 8)
        return true;

    var chars = "0123456789";
    if (chars.indexOf(String.fromCharCode(charCode)) == -1)
        return false;

    return true;
}


function OnlyAlphaNumeric(e, valor) {
    var charCode;
    if (navigator.appName == 'Netscape')
        charCode = e.which;
    else
        charCode = e.keyCode;

    if (charCode == 13)
        return true;
    if (charCode == 8)
        return true;



    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

function OnlyNumericDate(e, valor) {
    var charCode;
    if (navigator.appName == 'Netscape')
        charCode = e.which;
    else
        charCode = e.keyCode;

    if (charCode == 13)
        return true;
    if (charCode == 8)
        return true;

    var chars = "0123456789/";
    if (chars.indexOf(String.fromCharCode(charCode)) == -1)
        return false;

    return true;
}

function OnlyNumericXDecimal(e, txt, decimales, caracter) {
    var valor = txt.value;
    var charCode;
    if (navigator.appName == 'Netscape')
        charCode = e.which;
    else
        charCode = e.keyCode;
    if (charCode == 13)
        return true;
    if (charCode == 8)
        return true;

    if (charCode == 0) return true;
    var str = valor;

    var chars = "." + caracter + "0123456789";
    if (chars.indexOf(String.fromCharCode(charCode)) == -1)
        return false;

    if (String.fromCharCode(charCode) == caracter || String.fromCharCode(charCode) == ".") {
        if (str == "")
            return false;
        if (str.indexOf(".") > 0)
            return false;
        if (str.indexOf(caracter) > 0)
            return false;
    }

    pos = str.indexOf('.');
    if (pos == -1)
        pos = str.indexOf(caracter);
    if (pos > 0) {
        if (valor.substr(pos).length > decimales)
            return false;

    }
    return true;
}

function Refill(x) {
    var str = x.value;
    var pos = str.indexOf(',');
    if (pos == -1) {
        if (str == '') str = '0';
        x.value = str + ',00';
    }
    else {
        var decimales = str.substr(pos + 1);
        if (decimales == '')
            x.value = str + '00';
        if (decimales != '')
            x.value = str.substr(0, pos) + "," + padright(decimales, '0', 2);

    }

}

function padright(val, ch, num) {
    var re = new RegExp("^.{" + num + "}");
    var pad = "";
    if (!ch) ch = " ";
    do {
        pad += ch;
    } while (pad.length < num);
    return re.exec(val + pad)[0];
}



function ReplacePointToComma(x) {
    var str = x.value;
    if (str.indexOf('.') > -1)
        x.value = x.value.replace(".", ",");
}

function isEmailAddress(str) {
    var pattern = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    return pattern.test(str);  // returns a boolean 
}