

// Menu JavaScript Start //

(function ($) {

    $.fn.menumaker = function (options) {

        var cssmenu = $(this), settings = $.extend({
            title: "",
            format: "dropdown",
            sticky: false,
            isCategoryMenu: false
        }, options);

        return this.each(function () {
            cssmenu.prepend('<div id="menu-button">' + settings.title + '</div>');
            $(this).find("#menu-button").on('click', function () {
                $(this).toggleClass('menu-opened');
                var mainmenu = $(this).next('ul');
                if (mainmenu.hasClass('open')) {
                    mainmenu.hide().removeClass('open');
                }
                else {
                    mainmenu.show().addClass('open');
                    if (settings.format === "dropdown") {
                        mainmenu.find('ul').show();
                    }
                }
            });

            cssmenu.find('li ul').parent().addClass('has-sub');

            multiTg = function () {
                cssmenu.find(".has-sub").prepend('<span class="submenu-button"></span>');
                cssmenu.find('.submenu-button').on('click', function () {
                    $(this).toggleClass('submenu-opened');
                    if ($(this).siblings('ul').hasClass('open')) {
                        $(this).siblings('ul').removeClass('open').hide();
                    }
                    else {
                        $(this).siblings('ul').addClass('open').show();
                    }
                });
            };

            if (settings.format === 'multitoggle') multiTg();
            else cssmenu.addClass('dropdown');

            if (settings.sticky === true) cssmenu.css('position', 'fixed');

            resizeFix = function () {
                if ($(window).width() > (settings.isCategoryMenu ? 766 : 991)) {
                    cssmenu.find('ul').show();
                }

                if ($(window).width() <= (settings.isCategoryMenu ? 766 : 991)) {
                    cssmenu.find('ul').hide().removeClass('open');
                }
            };
            resizeFix();
            return $(window).on('resize', resizeFix);

        });
    };
})(jQuery);

(function ($) {
    $(document).ready(function () {

        $("#cssmenu").menumaker({
            title: "",
            format: "multitoggle"
        });


        $("#catmenu").menumaker({
            title: "",
            format: "multitoggle",
            isCategoryMenu: true
        });

    });
})(jQuery);

// Menu JavaScript End //

// Carousel Assets Start //

$(document).ready(function () {
    $("#owl-demo").owlCarousel({
        navigation: false,
        slideSpeed: 300,
        paginationSpeed: 400,
        singleItem: true
    });

});

// Carousel Assets End //	

// JavaScript Scroll Effect //

jQuery(document).ready(function () {
    jQuery('.container, #slider, .product-cat ul li, .col-lg-6, .product-listing ul li, .left-box').addClass("hidden").viewportChecker({
        classToAdd: 'visible animated fadeInUp',
        offset: 100
    });
});

