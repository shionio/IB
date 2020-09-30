function setFields() {
    $(".form-numeric").on("keypress", function (e) { return OnlyNumeric(e) });
    $(".form-alphabetic").on("keypress", function (e) { return OnlyAlphabetic(e) });
    $(".form-alphanumeric").on("keypress", function (e) { return OnlyAlphaNumeric(e) });
    $(".form-numericdecimal")
		.on("keypress", function (e) { return OnlyNumeric(e); })
		.on("keyup", function (e) { return OnlyAlphaNumericDecimal(e) });

}

function OnlyNumeric(e) { var charCode = e.which ? e.which : e.keyCode; if (charCode == 13) return true; if (charCode == 8) return true; if (/[^0-9]/.test(String.fromCharCode(charCode))) return false; return true; }
function OnlyAlphabetic(e) { var charCode = e.which ? e.which : e.keyCode; if (charCode == 13) return true; if (charCode == 8) return true; if (/[^A-Za-z ]/.test(String.fromCharCode(charCode))) return false; return true; }
function OnlyAlphaNumeric(e) { var charCode = e.which ? e.which : e.keyCode; if (charCode == 13) return true; if (charCode == 8) return true; if (/[^A-Za-z0-9 ]/.test(String.fromCharCode(charCode))) return false; return true; } var amountformat = true;
function OnlyAlphaNumericDecimal(e) { var kcode; var elem; if (e.which) { kcode = e.which; elem = e.target } else if (e.keyCode) { kcode = e.keyCode; elem = e.srcElement } var amountMaxLen = 20; if (elem.value.length >= amountMaxLen) { return false } var val; var newVal = ""; switch (kcode) { case 8: { break } default: { if (elem) if (amountformat) elem.value = replaceAll(elem.value, ","); if (amountformat) { if ((kcode < 48 || kcode > 57) && kcode != 13) { if (e.which) e.preventDefault(); e.returnValue = false; if (elem) formatValor1(elem, true) } else if (kcode != 13) formatValor1(elem, false); else formatValor1(elem, true) } else { if ((kcode < 48 || kcode > 57) && kcode != 46 && kcode != 13) { if (e.which) e.preventDefault(); e.returnValue = false } else if (kcode == 46 && elem.value.indexOf(",") !== -1) { if (e.which) e.preventDefault(); e.returnValue = false; } } } } }
function replaceAll(value, char) { var result = value; var posi = value.indexOf(char); if (posi > -1) { while (posi > -1) { result = value.substring(0, posi); result = result + value.substring(posi + 1); posi = result.indexOf(char); value = result } } return result; }
function formatValor1(campo, preformat) { var vr = campo.value; vr = replaceAll(vr, "."); vr = replaceAll(vr, ","); campo.value = ""; var sign = ""; if (vr.indexOf("-") != -1) { vr = replaceAll(vr, "-"); sign = "-" } var tam = preformat ? vr.length : vr.length + 1; campo.maxLength = 15; if (tam <= 2) { campo.value = vr } if (tam > 2 && tam <= 5) { campo.maxLength = 16; campo.value = vr.substr(0, tam - 2) + "," + vr.substr(tam - 2, tam) } if (tam >= 6 && tam <= 8) { campo.maxLength = 17; campo.value = vr.substr(0, tam - 5) + "." + vr.substr(tam - 5, 3) + "," + vr.substr(tam - 2, tam) } if (tam >= 9 && tam <= 11) { campo.maxLength = 18; campo.value = vr.substr(0, tam - 8) + "." + vr.substr(tam - 8, 3) + "." + vr.substr(tam - 5, 3) + "," + vr.substr(tam - 2, tam) } if (tam >= 12 && tam <= 14) { campo.maxLength = 19; campo.value = vr.substr(0, tam - 11) + "." + vr.substr(tam - 11, 3) + "." + vr.substr(tam - 8, 3) + "." + vr.substr(tam - 5, 3) + "," + vr.substr(tam - 2, tam) } if (tam >= 15 && tam <= 17) { campo.maxLength = 20; campo.value = vr.substr(0, tam - 14) + "." + vr.substr(tam - 14, 3) + "." + vr.substr(tam - 11, 3) + "." + vr.substr(tam - 8, 3) + "." + vr.substr(tam - 5, 3) + "," + vr.substr(tam - 2, tam) } var pos = campo.value.indexOf(","); if (pos != -1) { vr = campo.value.substr(0, pos); if (vr == "00" || vr.length == 2 && vr.substr(0, 1) == "0") campo.value = campo.value.substr(1, tam) } campo.value = sign + campo.value; }


function validateEmail(sEmail) {
    var filter = new RegExp(/^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i);
    if (filter.test(sEmail))
        return true;
    else
        return false;
}

$(document).ready(function () {
    $(document).on('focus', ':input', function () {
        $(this).attr('autocomplete', 'off');
    });
});