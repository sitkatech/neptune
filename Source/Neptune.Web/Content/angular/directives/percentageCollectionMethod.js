angular.module("percentageCollectionMethodDirective", [])
    .directive('percentageCollectionMethod',
        function() {
            var object = {
                restrict: 'EA',                
                scope: {
                    observationTypeId: '@',
                    measurementUnitLabelAndUnit: '@',
                    datasource: '=' //Two-way data binding
                },
                templateUrl: '/Content/angular/directives/percentageCollectionMethod.html',
                link: function (scope) {
                    scope.calculateRemainingPercent = function() {
                        var sum = _.reduce(scope.datasource.SingleValueObservations,
                            function(sum, n) {
                                var toAdd = n.ObservationValue == null ? 0 : n.ObservationValue;
                                return sum + toAdd;
                            },
                            0);
                        return Math.round((100 - sum) * 100) / 100;
                    };
                }
            };
            return object;
        }
    );
