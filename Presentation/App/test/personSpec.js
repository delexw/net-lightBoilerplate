/*jshint unused: vars */
define(['angular', 'angular-mocks', 'app'], function (angular, mocks, app) {
  'use strict';

  describe('Controller: PersonCtrl', function () {

    // load the controller's module
    beforeEach(module('ageRangerApp.controllers.PersonCtrl'));

    var PersonCtrl;
    var scope;

    // Initialize the controller and a mock scope
    beforeEach(inject(function ($controller, $rootScope) {
      scope = $rootScope.$new();
      PersonCtrl = $controller('PersonCtrl', {
        $scope: scope,
        // place here mocked dependencies
        personService: {
          return: {
            query: function (params, callback) {
              var persons = [{
                  id: 1,
                  firstName: "Adam",
                  lastName: "Liu",
                  age: 5
                },
                {
                  id: 2,
                  firstName: "Adam2",
                  lastName: "Liu2",
                  age: 6
                }
              ];
              callback();
              return params;
            }
          }
        }
      });
    }));

    it('should return a list of persons by calling GetPersons', function () {
      //expect(PersonCtrl.awesomeThings.length).toBe(3);
      expect(PersonCtrl.getPersons().length===2).toBe(true);
    });
  });
});
