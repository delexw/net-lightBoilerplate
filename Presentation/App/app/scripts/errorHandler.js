define([
  'angular'
], function (angular) {
  'use strict';
  angular.module('ageRangerApp.errorHandler', []).factory('globalErrorHandler', ['$q', '$window', '$log',

    function ($q, $window, $log) {
      var requestInterceptor = {
        responseError: function (response) {
          if (response.status === 400 || response.status === 500) {
            $log.info(response.data.code);
          } else {
            $window.alert('Server error');
          }
          return $q.reject(response);
        }
      };
      return requestInterceptor;
    }
  ])
});
