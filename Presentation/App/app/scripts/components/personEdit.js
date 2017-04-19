define([
  'angular'
], function (angular) {
  'use strict';

  angular.module('ageRangerApp.components.PersonEdit', ['ageRangerApp.services.PersonService'])
    .directive('personEdit', ['$timeout', 'personService', function ($timeout, personService) {
      return {
        templateUrl: '../../templates/components/personEdit.html',
        restrict: 'AE',
        controller: function ($scope) {
          $scope.created = false;
          $scope.person = angular.copy($scope.$resolve.person);
          $scope.operator = $scope.$resolve.operator;
          $scope.ok = function (personEditForm) {
            if ($scope.operator === 'Create') {
              //id is allowed to be null
              //validate user input
              $scope.person.validate();
              if ($scope.person.validateResult.length === 0) {
                personService.save($scope.person,
                  function () {
                    personEditForm.$setPristine();
                    personEditForm.$setUntouched();
                    $scope.created = true;
                    $timeout(function () {
                      $scope.created = false;
                    }, 1000);
                  })
              } else {
                throw new Error("create person error");
              }
            } else {
              //id is not allowed to be null
              //validate user input
              $scope.person.validate();
              if ($scope.person.validateResult.length === 0) {
                personService.update($scope.person,
                  function () {
                    personEditForm.$setPristine();
                    personEditForm.$setUntouched();
                    $scope.cancel({
                      $value: $scope.person
                    });
                  })
              } else {
                throw new Error("update person error");
              }
            }
          };
          $scope.cancel = $scope.$close;
        }
      };
    }]);
});
