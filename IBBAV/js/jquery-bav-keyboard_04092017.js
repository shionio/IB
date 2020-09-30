(function ($) {

    // These are the defaults
    var defaults = {
        // keys array with sub arrays for rows
        keys: [['1', '2', '3', '4', '5', '6', '7', '8', '9', '0']],
        keysA: [['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u','v', 'w','x', 'y', 'z']],
        keysM: [['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z']],
        keysS: [['.', '!', '@', '#', '$', '%', '&', '*']],
        target: 'body',
        inputId: [],
        radioId: []
    };

    Array.prototype.shuffle = function () {
        var i = this.length, j, temp;
        if (i == 0) return this;
        while (--i) {
            j = Math.floor(Math.random() * (i + 1));
            temp = this[i];
            this[i] = this[j];
            this[j] = temp;
        }

        return this;
    }


    $.fn.jkeyboard = function (opt) {

        var
			args = arguments,
			$this = this,
			init,
			setupKeyboard,
			insertAtCaret,
			ifocus = '',
			options = ($.isPlainObject(opt) || !opt) ? $.extend(true, {}, defaults, opt) : $.extend(true, {}, defaults);
        init = function () {

            setupKeyboard(this, options);

        }
        setupKeyboard = function (t, def) {
            var $this = t;

            var $keyboard = $('<div/>').addClass('jkeyboard-jk');
            var buttons = [];
            var $wrap = $('<div/>');
            var $wrapL = $('<div style="float:left; width:300px; text-align:center" />');
            var $wrapR = $('<div style="float:right; width:100px"/>');

            //lado derecho
            for (var i = 0; i < def.keys.length; i++) {

                var keyarr = def.keys[i].shuffle();
                for (var k = 0; k < keyarr.length; k++) {
                    var key = keyarr[k];
                    var button = '';
                    var isHtml = key.substring(0, 2);

                    button = $('<button type="button"  tabindex="-1" />').addClass('btn btn-danger').data("val", key);
                    if (isHtml == '&#') {
                        button.html(key);
                    }
                    else {
                        button.text(key);
                    }

                    $wrapR.append(button);
                }
            }
            var icon = $("<span />").addClass("glyphicon glyphicon-arrow-left").attr("aria-hidden", "true");
            var button = $('<button type="button"  tabindex="-1" />').addClass('btn btn-danger jkeyboard-clear command').append(icon);
            button.on('click', function (e) {
                e.preventDefault();

                var i = ifocus == '' ? def.inputId[0] : ifocus;
                
                $("#" + i).val("").focus();
            });
            $wrapR.append(button);
            $wrap.append($wrapR);

            // fin lado derecho

            //lado izquierdo

            for (var i = 0; i < def.keysS.length; i++) {

                var keyarr = def.keysS[i].shuffle();
                for (var k = 0; k < keyarr.length; k++) {
                    var key = keyarr[k];
                    var button = '';
                    var isHtml = key.substring(0, 2);
                    button = $('<button type="button"  tabindex="-1" />').addClass('btn btn-danger').text(key).data("val", key);
                    $wrapL.append(button);
                }
            }



            /*
            var wrapMin = $('<div class="minus" />');
            
            for (var i = 0; i < def.keysA.length; i++) {

                var keyarr = def.keysA[i].shuffle();
                for (var k = 0; k < keyarr.length; k++) {
                    var key = keyarr[k];
                    var button = '';
                    var isHtml = key.substring(0, 2);
                    button = $('<button type="button" />').addClass('btn btn-danger').text(key).data("val", key);
                    wrapMin.append(button);
                }            
            }
            $wrapL.append(wrapMin);
            */


            var wrapMay = $('<div class="mayus" style="text-align:center" />');
            for (var i = 0; i < def.keysM.length; i++) {

                var keyarr = def.keysM[i].shuffle();
                for (var k = 0; k < keyarr.length; k++) {
                    var key = keyarr[k];
                    var button = '';
                    var isHtml = key.substring(0, 2);
                    button = $('<button type="button"  tabindex="-1" />').addClass('btn btn-danger').text(key).data("val", key);
                    wrapMay.append(button);
                }
            }

            $wrapL.append(wrapMay);


            $wrap.append($wrapL);

            //shift
            /*
            var icon = $("<span />").addClass("glyphicon glyphicon-arrow-up").attr("aria-hidden", "true").html("Minus");
            var button = $('<button type="button" style="width:90%" />').addClass('btn btn-danger jkeyboard-shift command').data("may","0").append(icon);
            button.on('click', function (e) {
                e.preventDefault();
                var t =$(this);
                if (t.data("may") == "0") {
                    $(".minus").hide();
                    $(".mayus").show();
                    t.data("may", "1");
                    t.find("span").html("Mayus");
                } else {
                    $(".minus").show();
                    $(".mayus").hide();
                    t.data("may", "0");
                    t.find("span").html("Minus");
                }

                

            });
            $wrapL.append(button);*/
            //end shift
            //fin lado izquierdo

            buttons.push($wrap);
            $keyboard.append(buttons).css("position", "relative");

            $(def.target).append($keyboard);

            var o = $(def.target).find('.jkeyboard-jk');

            if (def.radioId.length > 0) {
                for (var i = 0 ; i < def.radioId.length; i++) {
                    $("#" + def.radioId[i]).attr("data-input", def.inputId[i]);
                    $("#" + def.inputId[i]).attr("data-radio", def.radioId[i]);

                    $("#" + def.radioId[i]).on("click", function () {
                        ifocus = $(this).attr("data-input");
                        $("#" + $(this).attr("data-input")).focus();
                        
                    });

                    $("#" + def.inputId[i]).on("click", function () {
                        ifocus = $(this).attr("id");
                        $("#" + $(this).attr("data-radio")).prop("checked",true);
                        
                    });
                }
            }

            o.mouseover(function () {

                $.each(o.find('button:not(.command)'), function (i, item) {
                    $(item).text("*");
                });

            }).mouseleave(function () {
                $.each(o.find('button:not(.command)'), function (i, item) {
                    $(item).text($(item).data("val"));
                });
            });


            o.find('button[type="button"]:not(.command)').on('click', function (e) {

                e.preventDefault();
                //console.log();
                var key = $(this).data("val");


                var i = ifocus == '' ? def.inputId[0] : ifocus;
                
                insertAtCaret(i, key);
            });

            
                    
                

        }

        insertAtCaret = function (areaId, text) {

            var maxlen = $("#" + areaId).attr("maxlength");
            var curlen = $("#" + areaId).val().length;
            if (curlen == maxlen) return false;

            var txtarea = document.getElementById(areaId);

            var scrollPos = txtarea.scrollTop;
            var strPos = 0;
            var br = ((txtarea.selectionStart || txtarea.selectionStart == '0') ?
                "ff" : (document.selection ? "ie" : false));


            if (br == "ie") {
                txtarea.focus();
                var range = document.selection.createRange();
                range.moveStart('character', -txtarea.value.length);
                strPos = range.text.length;
            }
            else if (br == "ff") strPos = txtarea.selectionStart;

            var front = (txtarea.value).substring(0, strPos);
            var back = (txtarea.value).substring(strPos, txtarea.value.length);
            txtarea.value = front + text + back;
            strPos = strPos + text.length;
            if (br == "ie") {
                txtarea.focus();
                var range = document.selection.createRange();
                range.moveStart('character', -txtarea.value.length);
                range.moveStart('character', strPos);
                range.moveEnd('character', 0);
                range.select();
            }
            else if (br == "ff") {
                txtarea.selectionStart = strPos;
                txtarea.selectionEnd = strPos;
                txtarea.focus();
            }
            txtarea.scrollTop = scrollPos;

        }

        init(opt);

    };

    $.fn.jkeyboard.defaults = defaults;

}(jQuery));

