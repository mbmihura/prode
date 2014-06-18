'use strict';

angular.module('prodeApp')
  .controller('MainCtrl', function ($scope, $routeParams, Session) {
      Session.createFromUserId($routeParams.accessToken);
      $scope.username = Session.userName;
  });

angular.module('prodeApp')
  .controller('HeaderController', function ($scope, $location, Session) {
      $scope.isActive = function (viewLocation) {
          return viewLocation === $location.path();
      };
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
    .controller('LoginCtrl', function ($scope, $rootScope, AUTH_EVENTS, AuthService) {

        Session.createFromUserId($routeParams.accessToken);
          
        $scope.credentials = {
            username: '',
            password: ''
        };
        $scope.login = function (credentials) {
            AuthService.login(credentials).then(function () {
                $rootScope.$broadcast(AUTH_EVENTS.loginSuccess);
            }, function () {
                $rootScope.$broadcast(AUTH_EVENTS.loginFailed);
            });
        };
    })
    .constant('AUTH_EVENTS', {
        loginSuccess: 'auth-login-success',
        loginFailed: 'auth-login-failed',
        logoutSuccess: 'auth-logout-success',
        sessionTimeout: 'auth-session-timeout',
        notAuthenticated: 'auth-not-authenticated'
    })
  //.controller('loginCtrl', function ($scope, Authentication) {
  //    $scope.login = function () {
  //        Authentication.login({ username: $scope.email, password: $scope.password }, function (response) {
  //            window.location = '/';
  //        }, function (error) {
  //            if (error.status == 403)
  //                alert("Usuario y/o contraseña incorrectos.");
  //            else
  //                alert("status: " + error.status + "  -  ")
  //        });
  //    }


  //});



angular.module('prodeApp')
  .controller('eliminatoriasCtrl', function ($scope, $location, BracketsPredicction, Session) {

      var matches = BracketsPredicction.get({ userId: Session.userId }, function () {
          $scope.matches = matches;
      });
  });
