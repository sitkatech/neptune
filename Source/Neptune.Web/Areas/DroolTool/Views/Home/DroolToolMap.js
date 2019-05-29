function initResizeHandler(mapInitJson) {
    $(window).on("load",
        function () {
            jQuery("#" + mapInitJson.MapDivID).height(jQuery(window).height() -
                jQuery("header").height() -
                jQuery(".neptuneNavbar").height());
        });

    $(window).on("resize",
        function () {
            jQuery("#" + mapInitJson.MapDivID).height(jQuery(window).height() -
                jQuery("header").height() -
                jQuery(".neptuneNavbar").height());
        });
}
