'use strict';

angular.module('prodeApp')
  .controller('MainCtrl', function ($scope, $routeParams, $http, Session) {
      $scope.username = Session.getSession().userName;
     
      $http({ method: 'GET', url: 'api/positions/' }).
               success(function (table, status, headers, config) {
                   $scope.posiciones = table;
               })
  });

angular.module('prodeApp')
  .controller('HeaderController', function ($scope, $location, Session) {
      $scope.isActive = function (viewLocation) {
          return viewLocation === $location.path();
      };
      $scope.userLogged = function () {
          return Session.userLogged();
      }
  });

angular.module('prodeApp')
  .controller('GroupsPredictionsCtrl', function ($scope, $timeout, GroupsPredicction, Session) {
      var response = GroupsPredicction.query({ userId: Session.getSession().userId }, function () {
          $scope.predictions = [{
              Letter: 'A',
              Matches: $.grep(response, function (n, i) {
                  return n.Letter == 'A';
              })
          }, {
              Letter: 'B',
              Matches: $.grep(response, function (n, i) {
                  return n.Letter == 'B';
              })
          }, {
              Letter: 'C',
              Matches: $.grep(response, function (n, i) {
                  return n.Letter == 'C';
              })
          }, {
              Letter: 'D',
              Matches: $.grep(response, function (n, i) {
                  return n.Letter == 'D';
              })
          }, {
              Letter: 'E',
              Matches: $.grep(response, function (n, i) {
                  return n.Letter == 'E';
              })
          }, {
              Letter: 'F',
              Matches: $.grep(response, function (n, i) {
                  return n.Letter == 'F';
              })
          }, {
              Letter: 'G',
              Matches: $.grep(response, function (n, i) {
                  return n.Letter == 'G';
              })
          }, {
              Letter: 'H',
              Matches: $.grep(response, function (n, i) {
                  return n.Letter == 'H';
              })
          }];
          var t = 0;
          for (var i = 0; i < response.length; i++) {
              t = t + response[i].PuntosGanados;
          }
          $scope.subtotal = t;
      });
  });

angular.module('prodeApp')
  .controller('MejoresCtrl', function ($scope, $location, Session) {

  });


angular.module('prodeApp')
    .controller('LoginCtrl', function ($scope, $routeParams, $location, Authentication) {
        $scope.token = '';
        $scope.login = function (token) {
            Authentication.loginWithToken(token).then(function () {
                $location.path('/')
            });
        };
        if ($routeParams.accessToken){
            $scope.login($routeParams.accessToken)
        }
    })

angular.module('prodeApp')
  .controller('eliminatoriasCtrl', function ($scope, $location, BracketsPredicction, Session) {

      var matches = BracketsPredicction.get({ userId: Session.userId }, function () {
          $scope.matches = matches;
      });
  });
