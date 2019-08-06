(function ($) {

    var createXorSlideoutGroupIfNotExists = function() {
        if (!window.xorSlideoutGroup) {
            window.xorSlideoutGroup = {slideouts: []};
        }
    };

    // note that currently the target element cannot have position:static.
    $.fn.slideout = function (options) {
        if (options.xorSlideouts) {
            createXorSlideoutGroupIfNotExists();
            window.xorSlideoutGroup.slideouts.push(this);
        }

        var targetSelector = options.targetSelector;

        if ($(targetSelector).css("position") == "static") {
            $(targetSelector).css("position", "relative");
        }

        $(targetSelector).append("<div class='slideout-wrap'></div>");
        $(targetSelector + " .slideout-wrap").append(this);
        this.addClass('slideout-content slideout-hid');
        $(targetSelector).append("<span class='expando-bar'><span class='expando-glyph glyphicon glyphicon-menu-right'></span></span>");

        this.openSlideout = function() {
            $(targetSelector + " .slideout-wrap").addClass("slideout-expant");
            this.removeClass("slideout-hid");
            $(targetSelector + " .slideout-wrap").css("width", options.width);
            $(targetSelector + " .expando-glyph").removeClass("glyphicon-menu-right");
            $(targetSelector + " .expando-glyph").addClass("glyphicon-menu-left");
        };

        this.closeSlideout = function() {
            $(targetSelector + " .slideout-wrap").removeClass("slideout-expant");
            this.addClass("slideout-hid");
            $(targetSelector + " .expando-glyph").addClass("glyphicon-menu-right");
            $(targetSelector + " .expando-glyph").removeClass("glyphicon-menu-left");
        };

        $(targetSelector + " .expando-bar").on("click",
            function() {
                this.expand = !this.expand;

                if (this.expand) {
                    // ReSharper disable once MisuseOfOwnerFunctionThis
                    this.openSlideout();

                    if (options.xorSlideouts) {
                        var slideouts = window.xorSlideoutGroup.slideouts;
                        for (var i = 0; i < slideouts.length; i++) {
                            if (slideouts[i] != this) {

                                slideouts[i].closeSlideout();
                                slideouts[i].expand = false;
                            }
                        }
                    }
                } else {
                    // ReSharper disable once MisuseOfOwnerFunctionThis seriously how hard is it to read the .bind(this) at the end of the function dec?
                    this.closeSlideout();
                }
            }.bind(this));
    };

}(jQuery));