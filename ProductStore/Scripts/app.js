'use strict';

angular
  .module('prodeApp', [
    'ngCookies',
    'ngResource',
    'ngSanitize',
    'ngRoute',
    'ui.bootstrap',
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
            redirectTo: '/',
            data: {
                userRequired: true
            }
        });
  })
.run(function ($rootScope, $location, Authentication) {
    $rootScope.$on('$routeChangeStart', function (event, next, current) {
        if (next.data) {
            var userRequired = next.data.userRequired;
            if (userRequired && !Authentication.isAuthenticated()) {
                // user is not allowed
                $location.url("/login");
                //$rootScope.$broadcast(AUTH_EVENTS.notAuthenticated);
            }
        }
        var dimensionValue = 'page redirecct';
        ga('set', 'dimension2', dimensionValue);
    });
});
