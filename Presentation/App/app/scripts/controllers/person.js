define(['angular',
  //entity
  '../models/person',
  '../models/aggregates/personAge',
  //service
  '../services/personService',
  //component
  '../components/personEdit'
], function (angular, person, personAge) {
  'use strict';

  /**
   * @ngdoc function
   * @name ageRangerApp.controller:PersonCtrl
   * @description
   * # PersonCtrl
   * Controller of the ageRangerApp
   */
  angular.module('ageRangerApp.controllers.PersonCtrl', ['ngAnimate', 'ageRangerApp.components.PersonEdit',
      'ageRangerApp.services.PersonService', 'ui.bootstrap'
    ])
    .controller('PersonCtrl', ['$uibModal', '$scope', 'personService',
      function ($uibModal, $scope, personService) {

        //query criteria
        $scope.pageIndex = 1;
        $scope.pageCount = 10;
        $scope.queryfilter = '';
        //person entity
        $scope.person = new person(0, '', '', 0);
        //person version
        $scope.persons = [];

        function _entityMapper(personsFromServer) {
          $scope.persons = [];
          personsFromServer.forEach(function (element) {
            var p = new personAge(element.id,
              element.firstName, element.lastName, element.age, element.group ? element.group.description : '');
            p.setVersion(element.rowVersion);
            $scope.persons.push(p);
          }, this);
        };
        //query person
        var getPersons = function (filter) {
          var f = typeof filter === 'undefined' ? '' : 'filter=' + filter;
          var p = 'pageIndex=' + $scope.pageIndex + '&pageCount=' + $scope.pageCount;

          var persons = personService.query({
              filter: f === '' ? f + p : f + '&' + p
            },
            function () {
              _entityMapper(persons);
            })
        };

        $scope.refresh = function () {
          $scope.queryfilter = ''
        };

        //Create one person
        $scope.createPerson = function () {
          $uibModal.open({
            component: 'personEdit',
            backdrop: 'static',
            resolve: {
              operator: function () {
                return 'Create';
              },
              person: function () {
                return $scope.person;
              }
            }
          }).result.then(function () {
            getPersons();
          })
        };

        //Edit one person
        $scope.editPerson = function (person) {
          $uibModal.open({
            component: 'personEdit',
            backdrop: 'static',
            resolve: {
              operator: function () {
                return 'Update';
              },
              person: function () {
                return person;
              }
            }
          }).result.then(function (updatedPerson) {
            if (typeof updatedPerson !== 'undefined') {
              person.firstName = updatedPerson.$value.firstName;
              person.lastName = updatedPerson.$value.lastName;
              person.age = updatedPerson.$value.age;
            }
          })
        };

        $scope.changePage = function (action) {
          if (action === 'next') {
            if ($scope.persons.length < $scope.pageCount) {
              return;
            }
            $scope.pageIndex++;
          } else {
            $scope.pageIndex--;
          }
          if ($scope.pageIndex < 1) {
            $scope.pageIndex = 1;
            return;
          }
          if ($scope.queryfilter !== '') {
            var filter = 'FirstName.Contains(' + $scope.queryfilter + ') or LastName.Contains(' + $scope.queryfilter + ')';
            getPersons(filter);
          } else {
            getPersons();
          }
        }
        //Query in real time
        $scope.$watch('queryfilter', function (newValue, oldValue) {
          $scope.pageIndex = 1;
          $scope.pageCount = 10;
          if (newValue === '') {
            getPersons();
            return;
          }
          var filter = 'FirstName.Contains(' + newValue + ') or LastName.Contains(' + newValue + ')';
          getPersons(filter);
        });

        getPersons();
      }
    ]);
});
