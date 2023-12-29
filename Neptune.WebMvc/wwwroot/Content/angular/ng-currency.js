// This has been customized but was sourced from:
// ng-currency
// http://alaguirre.com/
// Version: 0.7.0 - 2014-05-08
// License: MIT
angular.module("ng-currency", [])
    .directive("ngCurrency", function ($filter, $locale) {
        return {
            require: "ngModel",
            link: function (scope, element, attrs, ngModel) {

                function decimalRex(dChar) {
                    return RegExp("\\d|\\" + dChar, "g");
                }

                function clearRex(dChar) {
                    return RegExp("((\\" + dChar + ")|([0-9]{1,}\\" + dChar + "?))&?[0-9]{0,2}", "g");
                }

                function decimalSepRex(dChar) {
                    return RegExp("\\" + dChar, "g");
                }

                function clearValue(value) {
                    value = value.toString();
                    var dSeparator = $locale.NUMBER_FORMATS.DECIMAL_SEP;
                    var clear;

                    if (value.match(decimalSepRex(dSeparator))) {
                        clear = value.match(decimalRex(dSeparator))
                            .join("").match(clearRex(dSeparator));
                        clear = clear ? clear[0].replace(dSeparator, ".") : "";
                    }
                    else if (value.match(decimalSepRex("."))) {
                        clear = value.match(decimalRex("."))
                            .join("").match(clearRex("."));
                        clear = clear ? clear[0] : "";
                    }
                    else {
                        clear = value.match(/\d/g);
                        clear = clear ? clear.join("") : "";
                    }
                    return clear;
                }

                ngModel.$parsers.push(function (viewValue) {
                    var cVal = clearValue(viewValue);
                    return Sitka.Methods.isUndefinedNullOrEmpty(cVal) ? null : Math.round(parseFloat(cVal));
                });

                element.on("blur", function () {
                    element.val($filter("nfcurrency")(ngModel.$modelValue));
                });

                ngModel.$formatters.unshift(function (value) {
                    return $filter("nfcurrency")(value);
                });
            }
        };
    });
