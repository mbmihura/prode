'use strict';

angular
  .module('prodeApp', [
    'ngCookies',
    'ngResource',
    'ngSanitize',
    'ngRoute',
    'TimeAgoModule'
  ])
  .config(function ($routeProvider) {
      $routeProvider
        .when('/', {
            templateUrl: 'Content/views/main.html',
            controller: 'MainCtrl',
            data: {
                userRequired: true
            }
        })
        .when('/grupos', {
            templateUrl: 'Content/views/grupos.html',
            controller: 'GroupsPredictionsCtrl',
            data: {
                userRequired: true
            }
        })
        .when('/eliminatorias', {
            templateUrl: 'Content/views/eliminatorias.html',
            controller: 'eliminatoriasCtrl',
            data: {
                userRequired: true
            }
        })
          .when('/mejores', {
              templateUrl: 'Content/views/mejores.html',
              controller: 'MejoresCtrl',
              data: {
                  userRequired: true
              }
          })
           .when('/login', {
               templateUrl: 'Content/views/login.html',
               controller: 'LoginCtrl',
               data: {
                   userRequired: false
               }
           })
        .otherwise({
            redirectTo: '/'
        });
  })
//.run(function ($rootScope, $location, AUTH_EVENTS, AuthService) {
//    $rootScope.$on('$routeChangeStart', function (event, next, current) {
//        var userRequired = next.data.userRequired;
//        if (userRequired && !AuthService.isAuthenticated()) {
//            $location.path("/login");
//            // user is not allowed
//            $rootScope.$broadcast(AUTH_EVENTS.notAuthenticated);
//        }
//    });
//});
