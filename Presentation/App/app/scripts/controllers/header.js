define([
  'angular'
], function (angular) {
  'use strict';
  angular.module('ageRangerApp.controllers.HeaderCtrl', [])
    .controller('HeaderCtrl', ['$scope', '$location', function ($scope, $location) {
      $scope.isActived = function (path) {
        return $location.path() === path;
      }
    }]);
});
