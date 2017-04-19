define([
  'angular'
], function (angular) {
  'use strict';

  angular.module('ageRangerApp.services.PersonService', ['ngResource'])
    .service('personService', ["$resource", function ($resource) {
      var apiAddress = window.apiBase + "Person/:id?:filter";
      var resource = $resource(apiAddress, {id : '@id'}, {
        'update': {
          method: 'PUT'
        }
      });
      return resource;
    }]);
});
