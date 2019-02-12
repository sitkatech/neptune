jQuery(function () {
    jQuery(window).scroll(function () {
        // this is fragile, the 10px buffer is for when the document height is just equal to the height of the view port + the height of the header
        var outerHeight = jQuery(document).height();
        var heightOfViewPort = jQuery(window).height();
        var headerHeight = jQuery('.neptuneHeader')[0].offsetHeight;

        if (jQuery(window).scrollTop() > headerHeight && (outerHeight > (heightOfViewPort + headerHeight + 10))) {
            jQuery('.neptuneNavbar').addClass('navbar-fixed-top');
            jQuery("#mainnavandcontent").css("margin-top", jQuery("#neptune-collapse-navbar").css("height"));
        }
        if (jQuery(window).scrollTop() <= headerHeight) {
            jQuery('.neptuneNavbar').removeClass('navbar-fixed-top');
            jQuery("#mainnavandcontent").css("margin-top", "0");
        }
    });
});