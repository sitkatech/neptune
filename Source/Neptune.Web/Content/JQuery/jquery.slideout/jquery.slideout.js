(function ($) {

    $.fn.slideout = function (options) {
        var targetSelector = options.targetSelector;

        $(targetSelector).append("<div class='slideout-wrap'></div>");
        $(targetSelector + " .slideout-wrap").append(this);
        this.addClass('slideout-content');
        $(targetSelector).append("<span class='expando-bar'> --&gt; </span>");

        $(targetSelector + " .expando-bar").on("click",
            function() {
                this.expand = !this.expand;

                if (this.expand) {
                    $(targetSelector + " .slideout-wrap").addClass("slideout-expant");
                    $(this).removeClass("glyphicon-menu-right");
                    $(this).addClass("glyphicon-menu-left");

                } else {
                    $(targetSelector + " .slideout-wrap").removeClass("slideout-expant");
                    $(this).addClass("glyphicon-menu-right");
                    $(this).removeClass("glyphicon-menu-left");
                }
            });
    };

}(jQuery));