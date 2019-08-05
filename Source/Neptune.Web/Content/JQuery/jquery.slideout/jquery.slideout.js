(function ($) {


    // note that currently the target element cannot have position:static.
    $.fn.slideout = function (options) {
        var targetSelector = options.targetSelector;

        if ($(targetSelector).css("position") == "static") {
            console.log("oopso, better fix that boi!");
            $(targetSelector).css("position", "relative");
        }

        $(targetSelector).append("<div class='slideout-wrap'></div>");
        $(targetSelector + " .slideout-wrap").append(this);
        this.addClass('slideout-content');
        $(targetSelector).append("<span class='expando-bar'><span class='expando-glyph glyphicon glyphicon-menu-right'></span></span>");

        $(targetSelector + " .expando-bar").on("click",
            function() {
                this.expand = !this.expand;

                if (this.expand) {
                    $(targetSelector + " .slideout-wrap").addClass("slideout-expant");
                    $(targetSelector + " .expando-glyph").removeClass("glyphicon-menu-right");
                    $(targetSelector + " .expando-glyph").addClass("glyphicon-menu-left");

                } else {
                    $(targetSelector + " .slideout-wrap").removeClass("slideout-expant");
                    $(targetSelector + " .expando-glyph").addClass("glyphicon-menu-right");
                    $(targetSelector + " .expando-glyph").removeClass("glyphicon-menu-left");
                }
            });
    };

}(jQuery));