﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="IBBAV.css.main" %>

/*! HTML5 Boilerplate v4.3.0 | MIT License | http://h5bp.com/ */
@font-face {
	font-family: 'MyriadWebPro';
	src: url('<%=ResolveUrl("~/fonts/main/MyriadWebPro.eot") %>');
	src: local('?'), url('<%=ResolveUrl("~/fonts/main/MyriadWebPro.woff")%>') format('woff'), url('<%=ResolveUrl("~/fonts/main/MyriadWebPro.ttf")%>') format('truetype'), url('<%=ResolveUrl("~/fonts/main/MyriadWebPro.svg")%>') format('svg');
	font-weight: normal;
	font-style: normal;
}


html,
button,
input,
select,
textarea {
    color: #222;
}


html {
    font-size: 1em;
    line-height: 1.4;
}

::-moz-selection {
    background: #b3d4fc;
    text-shadow: none;
}

::selection {
    background: #b3d4fc;
    text-shadow: none;
}

hr {
    display: block;
    height: 1px;
    border: 0;
    border-top: 1px solid #ccc;
    margin: 1em 0;
    padding: 0;
}

h4{
    text-align:center;
}

audio,
canvas,
img,
video {
    vertical-align: middle;
}

fieldset {
    border: 0;
    margin: 0;
    padding: 0;
}

textarea {
    resize: vertical;
}

.browsehappy {
    margin: 0.2em 0;
    background: #ccc;
    color: #000;
    padding: 0.2em 0;
}


/* ===== Initializr Styles ==================================================
   Author: Jonathan Verrecchia - verekia.com/initializr/responsive-template
   ========================================================================== */

body {
    font: 16px/26px Helvetica, Helvetica Neue, Arial;
}

.wrapper {
    width: 90%;
    margin: 0 5%;
}

/* ===================
    ALL: Orange Theme
   =================== */


.footer-container,
.main aside {
    border-top: 20px solid #e44d26;
}


.title {
    color: white;
}

/* ==============
    MOBILE: Menu
   ============== */

/*nav ul {
    margin: 0;
    padding: 0;
}

nav a {
    display: block;
    margin-bottom: 10px;
    padding: 15px 0;

    text-align: center;
    text-decoration: none;
    font-weight: bold;

    color: white;
    background: #e44d26;
}

nav a:hover,
nav a:visited {
    color: white;
}

nav a:hover {
    text-decoration: underline;
}*/

/* ==============
    MOBILE: Main
   ============== */

.main {
    padding: 30px 0;
}

.main article h1 {
    font-size: 2em;
}

.main aside {
    color: white;
    padding: 0px 5% 10px;
}

.footer-container footer {
    color: white;
    padding: 20px 0;
}

/* ===============
    ALL: IE Fixes
   =============== */

.ie7 .title {
    padding-top: 20px;
}

/* ==========================================================================
   Author's custom styles
   ========================================================================== */















/* ==========================================================================
   Media Queries
   ========================================================================== */

@media only screen and (min-width: 480px) {
    

/* ====================
    INTERMEDIATE: Menu
   ==================== */

    /*nav a {
        float: left;
        width: 27%;
        margin: 0 1.7%;
        padding: 25px 2%;
        margin-bottom: 0;
    }

    nav li:first-child a {
        margin-left: 0;
    }

    nav li:last-child a {
        margin-right: 0;
    }*/

/* ========================
    INTERMEDIATE: IE Fixes
   ======================== */

    /*nav ul li {
        display: inline;
    }

    .oldie nav a {
        margin: 0 0.7%;
    }*/
}

@media only screen and (min-width: 768px) {

/* ====================
    WIDE: CSS3 Effects
   ==================== */

    

/* ============
    WIDE: Menu
   ============ */

    .title {
        float: left;
    }

    nav {
        float: right;
        width: 38%;
    }

/* ============
    WIDE: Main
   ============ */

    .main article {
        float: left;
        width: 57%;
    }

    .main aside {
        float: right;
        width: 28%;
    }
}

@media only screen and (min-width: 1140px) {

/* ===============
    Maximal Width
   =============== */

    .wrapper {
        width: 1026px; /* 1140px - 10% for margins */
        margin: 0 auto;
    }
}

/* ==========================================================================
   Helper classes
   ========================================================================== */

.ir {
    background-color: transparent;
    border: 0;
    overflow: hidden;
    *text-indent: -9999px;
}

.ir:before {
    content: "";
    display: block;
    width: 0;
    height: 150%;
}

.hidden {
    display: none !important;
    visibility: hidden;
}

.visuallyhidden {
    border: 0;
    clip: rect(0 0 0 0);
    height: 1px;
    margin: -1px;
    overflow: hidden;
    padding: 0;
    position: absolute;
    width: 1px;
}

.visuallyhidden.focusable:active,
.visuallyhidden.focusable:focus {
    clip: auto;
    height: auto;
    margin: 0;
    overflow: visible;
    position: static;
    width: auto;
}

.invisible {
    visibility: hidden;
}

.clearfix:before,
.clearfix:after {
    content: " ";
    display: table;
}

.clearfix:after {
    clear: both;
}

.clearfix {
    *zoom: 1;
}

/* ==========================================================================
   Print styles
   ========================================================================== */

@media print {
    * {
        background: transparent !important;
        color: #000 !important;
        box-shadow: none !important;
        text-shadow: none !important;
    }

    a,
    a:visited {
        text-decoration: underline;
    }

    a[href]:after {
        content: " (" attr(href) ")";
    }

    abbr[title]:after {
        content: " (" attr(title) ")";
    }

    .ir a:after,
    a[href^="javascript:"]:after,
    a[href^="#"]:after {
        content: "";
    }

    pre,
    blockquote {
        border: 1px solid #999;
        page-break-inside: avoid;
    }

    thead {
        display: table-header-group;
    }

    tr,
    img {
        page-break-inside: avoid;
    }

    img {
        max-width: 100% !important;
    }

    @page {
        margin: 0.5cm;
    }

    p,
    h2,
    h3 {
        orphans: 3;
        widows: 3;
    }

    h2,
    h3 {
        page-break-after: avoid;
    }
}
