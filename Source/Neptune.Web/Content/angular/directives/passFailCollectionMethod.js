angular.module("passFailCollectionMethodDirective", [])
    .directive('passFailCollectionMethod',
        function() {
            var object = {
                restrict: 'EA',                
                scope: {
                    observationTypeId: '@',
                    passingScoreLabel: '@',
                    failingScoreLabel: '@',
                    datasource: '=' //Two-way data binding
                },
                templateUrl: '/Content/angular/directives/passFailCollectionMethod.html'
            };
            return object;
        }
    );
