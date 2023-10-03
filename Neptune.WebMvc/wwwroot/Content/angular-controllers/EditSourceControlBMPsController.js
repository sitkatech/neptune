angular.module("NeptuneApp")
    .controller("EditSourceControlBMPsController", function ($scope, angularModelAndViewData) {
        $scope.AngularModel = angularModelAndViewData.AngularModel;
        $scope.orderSourceControlBMPsByAttributeCategory = _.sortBy(_.groupBy($scope.AngularModel.SourceControlBMPSimples, 'SourceControlBMPAttributeCategoryName'), [function (o) { return o.SourceControlBMPAttributeID; }]);
    });
