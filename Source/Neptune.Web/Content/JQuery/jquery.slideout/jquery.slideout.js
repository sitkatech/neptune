(function ($) {

    $.fn.slideout = function (options) {
        var targetSelector = options.targetSelector;

        $(targetSelector).append("<div class='slideout-wrap'></div>");
        $(targetSelector + " .slideout-wrap").append(this);
        this.addClass('slideout-content');
        $(targetSelector).append("<span class='expando-bar'><span class='expando-glyph glyphicon glyphicon-menu-right'></span></span>");
        //debugger;
        //$(targetSelector + " .expando-bar").append("<span class='expando-glyph glyphicon glyphicon-menu-right></span>");

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