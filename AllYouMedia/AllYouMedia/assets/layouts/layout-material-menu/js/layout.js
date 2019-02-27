(function ($) {
    "use strict";

    /*---- Start Top Menu ----*/

    var CUSTOM_SETTINGS = window.CUSTOM_SETTINGS || {};

    CUSTOM_SETTINGS.navClick = function () {
        $(function () {
            var button = $('#navtoggle');
            var navarea = $(".menucontainer");
            button.mouseup(function () {
                navarea.toggleClass("wsoffcanvasopener");
                return false;
            });
            $(".topmenu").mouseup(function () {
                return false;
            });
            $(this).mouseup(function (e) {
                navarea.removeClass("wsoffcanvasopener");
            });
        });
    };

    CUSTOM_SETTINGS.navClick();

    var responsiveWidth = 991;
    var widthResize;

    $(window).on('load', function () {
        if(widthResize) {
            $('.menu-list li').has('ul').prepend('');
        }
    });

    $(window).on('resize', function () {
        widthResize = $(this).width() > responsiveWidth;
        if (widthResize) {
            $('.menu-list li ul').removeAttr("style");
        }
    }).resize();

    $(document).delegate(".menu-list li > a, .menu-list li > span", "click", function (e) {
        if(!widthResize) {
            $('.menu-list').children('li > span').has('.ws-activearrow').removeClass("ws-activearrow");
            $(this).parent().toggleClass("active");
            $(this).parent().children('span').toggleClass('ws-activearrow');
            $(this).parent().children('ul').slideToggle(300);
        }
    });

    var $body = $("body");
    $.extend(CORE_TEMP.function, {
        SiteColor: function () {
            var $site_color = $('.site-color'), /* variable used on Site Color Settings */
                $site_color_elements = $('#site-color'),
                $site_dark_color_elements = $('#site-dark-color'),
                $site_dark_color_switch = $('.dark-color-switch'), /* variable used on Site Color Settings */
                $cssRoot = '../../../assets/layouts/layout-material-menu/css/color/'; /* variable used on Site CSS path */


            /*----- START SITE COLOR SETTINGS JS -----*/

            $site_color.children('div').on('click', function (e) {
                e.preventDefault();
                var $this = $(this), /* global variable */
                    $this_color = $this.attr('data-color'),
                $site_dark_color = localStorage.getItem("site_dark_color");
                /* Global Color Value */

                // Remove and ADD color active class
                $site_color.children('div').removeClass('color-active'),
                    $this.addClass('color-active');

                // Change CSS href
                $site_color_elements.attr("href", $cssRoot + "light/color-" + $this_color + ".min.css");

                if ($site_dark_color !== null && $site_dark_color !== "") {
                    if ($("#site-dark-color").length == 0) {
                        $site_color_elements.after('<link id="site-dark-color" rel="stylesheet" href="' + $cssRoot + 'dark/color-' + $this_color + '-' + $site_dark_color + '.min.css">');
                    } else {
                        $('#site-dark-color').attr('href', $cssRoot + 'dark/color-' + $this_color + '-' + $site_dark_color + '.min.css');
                    }
                }

                // Check Color value is null or not
                if ($this_color == null || $this_color == "") {
                    localStorage.setItem("site_color", $this_color);
                } else {
                    localStorage.setItem("site_color", $this_color);
                }

            });

            // Get Template Color from local storage
            if (localStorage.getItem("site_color") !== null) {
                $site_color.children('div[data-color=' + localStorage.getItem("site_color") + ']').trigger('click');
            }

            /*----- END SITE COLOR SETTINGS JS -----*/

            /*----- START SITE DARK COLOR SETTINGS JS -----*/

            $site_dark_color_switch.on('click', function () {
                // Check value true is true
                if ($(".site-dark-check").prop("checked") == true) {
                    $('.dark-color').css("font-weight", "700").html("Dark");
                    localStorage.setItem("site_dark_color", "gray");

                    // Get Template Color and dark color from local storage
                    var site_color = localStorage.getItem("site_color");
                    var site_dark_color = localStorage.getItem("site_dark_color");

                    if (site_dark_color !== null && site_dark_color !== "") {
                        if (site_color == null) {
                            $site_color_elements.after('<link id="site-dark-color" rel="stylesheet" href="' + $cssRoot + 'dark/color-default-' + site_dark_color + '.min.css">');
                        } else if ($("#site-dark-color").length == 0) {
                            $site_color_elements.after('<link id="site-dark-color" rel="stylesheet" href="' + $cssRoot + 'dark/color-' + site_color + '-' + site_dark_color + '.min.css">');
                        } else {
                            $('#site-dark-color').attr('href', $cssRoot + 'dark/color-' + site_color + '-' + site_dark_color + '.min.css');
                        }
                    } else {
                        $('#site-dark-color').remove();
                    }

                } else {
                    $('.dark-color').css("font-weight", "normal").html("Light");
                    localStorage.removeItem("site_dark_color");
                    $('#site-dark-color').remove();
                }
            });


            // Det Template Dark Color from local storage
            if (localStorage.getItem("site_dark_color") !== null) {
                $('.dark-color').css("font-weight", "700").html("Dark");
                $('.dark-color-switch').children('.js-switch').prop('checked', true);
                $site_dark_color_switch.trigger('click');
            }

            /*----- END SITE DARK COLOR SETTINGS JS -----*/
        },
        MaterialMenu: function () {
            var $contain = $('.container-fluid, .footer-inner');
            $('.hamburger').on('click',function () {
                $(".material-left-back").toggleClass("animation-menu");
                $(".material_navbar").toggleClass("open");
                $contain.toggleClass('isOut');
                var isOut = $contain.hasClass('isOut');
                if ($(window).width() >= 992) {
                    $contain.animate({margin: isOut ? '0 0 0 250px' : '0'}, 320);
                }
            });

            var forEach=function(t,o,r){if("[object Object]"===Object.prototype.toString.call(t))for(var c in t)Object.prototype.hasOwnProperty.call(t,c)&&o.call(r,t[c],c,t);else for(var e=0,l=t.length;l>e;e++)o.call(r,t[e],e,t)};

            var hamburgers = document.querySelectorAll(".hamburger");
            if (hamburgers.length > 0) {
                forEach(hamburgers, function(hamburger) {
                    hamburger.addEventListener("click", function() {
                        this.classList.toggle("is-active");
                    }, false);
                });
            }
        },
        MaterialMenuResize: function () {
            var $contain = $('.container-fluid, .footer-inner');
            var isOut = $contain.hasClass('isOut');
            if ($(window).width() < 992) {
                $contain.removeAttr('style');
            }else{
                $contain.animate({margin: isOut ? '0 0 0 250px' : '0'}, 320);
            }
        },
        MaterialMenuEffect: function () {
            var material_menu = document.getElementById( 'material_navbar' ),
                img = material_menu.querySelector( 'div.hamburger' ),
                // isOpen = isAnimating = false,
                isOpen = isAnimating,
                isAnimating = false,
                // show/hide search area
                toggleMenu = function(evt) {

                    var offsets = material_navbar.getBoundingClientRect();
                    if( isOpen ) {
                        classie.remove( material_menu, 'open' );


                        img.blur();
                    }
                    else {
                        classie.add( material_menu, 'open' );
                    }
                    isOpen = !isOpen;
                };

            // events
            img.addEventListener( 'click', toggleMenu );
            // esc key closes search overlay
        },
        Leftsidemenu: function () {
            $('.sidebar-menu .sub-item > a').on('click', function () {
                var $this = $(this).parent('li');
                $this.toggleClass('active');
                $this.children('.sub-menu').slideToggle(300);
            });

        },
        NavigetionMenuActive: function () {
            if ($body.hasClass('nav-menu-icon')) {
                var $pathArray = window.location.pathname.split("/").pop(),
                    $currentpageurl = $("a[href='" + $pathArray + "']");

                $currentpageurl.parent('li').addClass('active');
                if ($currentpageurl.parent('.sub-menu') && $currentpageurl.parent('.main-menu')) {
                    $currentpageurl.parents('li').addClass('active');
                }
            }
        },
        LeftNavigetionSearchMenu: function () {
            $.extend($.expr[":"], {
                "Contains": function(elem, i, match, array) {
                    return (elem.textContent || elem.innerText || "").toLowerCase().indexOf((match[3] || "").toLowerCase()) >= 0;
                }
            });
            var $menusearch = $("#left-menu-search");
            $menusearch.on('change',function(){
                var $this = $(this).val();
                if ($this) {
                    $('.menu-title').hide();
                    $('#site-menu').find("li a:not(:Contains(" + $this + "))").hide().parent().hide();
                    var $filter = $('#site-menu').find("li a:Contains(" + $this + ")");
                    $filter.parent().hasClass("sub-item") ? ($filter.show().parents("li").show().addClass("open").closest("li").children("a").show().children("li").show(), $filter.siblings("ul").length > 0 && $filter.siblings("ul").children("li").show().children("a").show()) : $filter.show().parents("li").show().addClass("open").closest("li").children("a").show();
                }else{
                    $('.menu-title').show();
                    $('#site-menu').find("li a").show().parent().show().removeClass("open");
                }
            }).keyup(function() {
                $(this).change()
            });
        },
        Loaderinit: function () {
            $body.addClass('loaded');
        }
    });
    $.extend(CORE_TEMP.onReady, {
        layoutOnReady: function () {
            CORE_TEMP.function.Leftsidemenu();
            CORE_TEMP.function.SiteColor();
            CORE_TEMP.function.MaterialMenu();
        }
    });
    $.extend(CORE_TEMP.onLoad, {
        layoutOnLoad: function () {
            CORE_TEMP.function.NavigetionMenuActive();
            CORE_TEMP.function.Loaderinit();
            CORE_TEMP.function.LeftNavigetionSearchMenu();
            CORE_TEMP.function.MaterialMenuEffect();
            CORE_TEMP.function.MaterialMenuResize();
        }
    });
    $.extend(CORE_TEMP.onResize, {
        layoutOnResize: function () {
            CORE_TEMP.function.MaterialMenuResize();
        }
    });
})(jQuery);