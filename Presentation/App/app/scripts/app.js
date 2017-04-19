/*jshint unused: vars */
define(['angular', 'errorHandler', 'controllers/header', 'controllers/main', 'controllers/person'] /*deps*/ ,
  function (angular, errorHandler, HeaderCtrl, MainCtrl, PersonCtrl) /*invoke*/ {
    'use strict';

    /**
     * @ngdoc overview
     * @name ageRangerApp
     * @description
     * # ageRangerApp
     *
     * Main module of the application.
     */
    return angular
      .module('ageRangerApp', ['ngRoute', 'ngResource',
        'ageRangerApp.errorHandler',
        'ageRangerApp.controllers.HeaderCtrl',
        'ageRangerApp.controllers.MainCtrl',
        'ageRangerApp.controllers.PersonCtrl'
      ])
      .config(['$routeProvider', '$resourceProvider', '$locationProvider', '$httpProvider',
        function ($routeProvider, $resourceProvider, $locationProvider, $httpProvider) {
          $routeProvider
            .when('/', {
              templateUrl: 'views/main.html',
              controller: 'MainCtrl'
            })
            .when('/person', {
              templateUrl: 'views/person.html',
              controller: 'PersonCtrl'
            })
            .otherwise({
              redirectTo: '/'
            });
          // use the HTML5 History API
          //$locationProvider.html5Mode(true);
          $resourceProvider.defaults.stripTrailingSlashes = false;
          //register error errorHandler
          $httpProvider.interceptors.push('globalErrorHandler');
        }
      ]);
  });
