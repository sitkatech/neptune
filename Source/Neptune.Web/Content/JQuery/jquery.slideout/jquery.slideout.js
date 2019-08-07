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

        if ($(targetSelector).css("position") === "static") {
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

        var clickToExpandSelector;
        if (options.clickTargetToExpand) {
            $(targetSelector).addClass("click-to-expand-element");
            clickToExpandSelector = targetSelector;
        } else {
            $(targetSelector + " .expando-bar").addClass("click-to-expand-element");
            clickToExpandSelector = targetSelector + " .expando-bar";
        }

        $(clickToExpandSelector).on("click",
            function () {

                if (options.mediaQuery) {
                    if (!window.matchMedia(options.mediaQuery).matches) {
                        return;
                    }
                }

                this.expand = !this.expand;

                if (this.expand) {
                    // ReSharper disable once MisuseOfOwnerFunctionThis
                    this.openSlideout();

                    if (options.xorSlideouts) {
                        var slideouts = window.xorSlideoutGroup.slideouts;
                        for (var i = 0; i < slideouts.length; i++) {
                            if (slideouts[i] !== this) {

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

        // intended for use in conjunction with mediaQuery
        // clicking the alternate will open the drawer, but with a class that fullscreens it, and with a close button appended
        if (options.alternate) {
            $(targetSelector).append("<div class='expand-slideout-alternate-mobile-container'><button class='btn btn-sm btn-neptune expand-slideout-alternate-mobile'><span class='glyphicon glyphicon-option-horizontal '></span></button></div>");
            this.append("<button class=' btn btn-sm btn-neptune glyphicon glyphicon-remove close-slideout-button-mobile'></button>");
            $(targetSelector + " .close-slideout-button-mobile").on("click",
                function() {
                    $(targetSelector + " .slideout-wrap").removeClass("slideout-fullscreen-mobile");
                    this.addClass("slideout-hid");
                }.bind(this));

            $(targetSelector + " .expand-slideout-alternate-mobile").on("click",
                function () {
                    $(targetSelector + " .slideout-wrap").addClass("slideout-fullscreen-mobile");
                    this.removeClass("slideout-hid");
                }.bind(this));
        }
    };

}(jQuery));