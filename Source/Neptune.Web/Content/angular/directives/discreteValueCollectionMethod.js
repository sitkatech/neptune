angular.module("discreteValueCollectionMethodDirective", [])
    .directive('discreteValueCollectionMethod',
        function() {
            var object = {
                restrict: 'EA',                
                scope: {
                    propertiesToObserve: '=',
                    maximumValueOfObservations: '@',
                    minimumValueOfObservations: '@',
                    measurementUnitLabelAndUnit: '@',
                    datasource: '=' //Two-way data binding
                },
                templateUrl: '/Content/angular/directives/discreteValueCollectionMethod.html',
                link: function (scope) {
                    scope.addObservation = function () {
                        scope.datasource.SingleValueObservations.push({
                            PropertyObserved: null,
                            ObservationValue: null,
                            Notes: null
                        });
                    };

                    scope.deleteObservation = function (observation) {
                        Sitka.Methods.removeFromJsonArray(scope.datasource.SingleValueObservations, observation);
                    };

                    scope.disableAddObservation = function() {
                        return scope.datasource.SingleValueObservations.length >= scope.maximumNumberOfObservations;
                    };
                }
            };
            return object;
        }
    );
