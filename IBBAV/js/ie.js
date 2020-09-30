// JavaScript Document

	$(document).ready(initIE);
	
	function initIE(e) {
        iexplorer = true;
		$('tr:nth-child(2n) td').css('background-color','#eee'); 
		$('tr td').css('border-top','5px solid #FFF');
		$('tr td:last-child').css('border-right','none');
		$('.columns, .column').last().css('background-color','#000000');
	};
	
	if(typeof String.prototype.trim !== 'function') {
	  String.prototype.trim = function() {
		return this.replace(/^\s+|\s+$/g, ''); 
	  }
	}
