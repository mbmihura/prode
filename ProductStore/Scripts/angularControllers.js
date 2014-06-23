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
  .controller('HeaderController', function ($scope, $location, Authentication, Session) {
      $scope.isActive = function (viewLocation) {
          return viewLocation === $location.path();
      };
      $scope.userLogged = function () {
          return Session.userLogged();
      }
      $scope.logout = function () {
          Authentication.logout();
          $location.url('/login');
      };
  })
    .controller('EditResultModalCtrl', function ($scope, $modalInstance, situationText, resourceClass, resourceId) {

        $scope.results = resourceClass.query({ situationId: resourceId }, function (data) {
          data.forEach(function (pr) {
              if (pr.IsActualResult) {
                  $scope.selectedResult = pr;
              };
          });
      });

      $scope.text = 'Resultado para ' + situationText + ':';

      $scope.status = {
          isopen: false
      };

      $scope.toggleDropdown = function ($event) {
          $event.preventDefault();
          $event.stopPropagation();
          $scope.status.isopen = !$scope.status.isopen;
      };

      $scope.select = function (r) {
          $scope.selectedResult = r;
          $scope.status.isopen = false;
      }
      
      $scope.ok = function (result) {
          $modalInstance.close(result);
      };

      $scope.cancel = function () {
          $modalInstance.dismiss('cancel');
      };
  });

angular.module('prodeApp')
  .controller('GroupsPredictionsCtrl', function ($scope, $timeout, $routeParams, $modal, $log, GroupsPredicction, GroupsPosibleResults, Session, Users) {
      var userId = $routeParams.view ? $routeParams.view : Session.getSession().userId;

      $scope.users = Users.query(function (data) {
          data.forEach(function(e) {
              if (e.Id == userId) {
                  $scope.displayingUser = e.Username;
              };
          });
      });
      

      $scope.status = {
          isopen: false
      };

      $scope.toggleDropdown = function ($event) {
          $event.preventDefault();
          $event.stopPropagation();
          $scope.status.isopen = !$scope.status.isopen;
      };

      var response = GroupsPredicction.query({ userId: userId }, function () {
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

      $scope.alerts = [];
      $scope.closeAlert = function (index) {
          $scope.alerts.splice(index, 1);
      };

      $scope.showEditOption = true;
      $scope.open = function (situation) {

          var modalInstance = $modal.open({
              templateUrl: 'Content/views/editResultModal.html',
              controller: 'EditResultModalCtrl',
              resolve: {
                  situationText: function () {
                      return situation.TeamL + ' - ' + situation.TeamV + ' (Grupo ' + situation.Letter + ')';
                  }, resourceClass: function () {
                      return GroupsPosibleResults
                  }, resourceId: function () {
                      return situation.Id;
                  }
              }
          });

          modalInstance.result.then(function (result) {
              situation.Resultado = result.Description;
              var a = situation.$save().finally(function () {
                  $scope.alerts.push({ type: 'warning', strong: 'Resultado guardado. ', msg: 'Recargar la pagina para ver el nuevo puntaje.' });
              });
          }, function () {
              $log.info('Modal dismissed at: ' + new Date());
          });
      };
  });

angular.module('prodeApp')
  .controller('MejoresCtrl', function ($scope, $location, Session) {

  });


angular.module('prodeApp')
    .controller('LoginCtrl', function ($scope, $routeParams, $location, Authentication) {
        $scope.token = '';
        $scope.login = function (token) {
            Authentication.loginWithToken(token).then(function () {
                $location.url('/')
            });
        };
        if ($routeParams.accessToken){
            $scope.login($routeParams.accessToken)
        }
    })

angular.module('prodeApp')
  .controller('eliminatoriasCtrl', function ($scope, $location, $routeParams, $modal, $log, BracketsPredicction, BracketsPosibleResults, Users, Session) {
      var userId = $routeParams.view ? $routeParams.view : Session.getSession().userId;

      $scope.users = Users.query(function (data) {
          data.forEach(function (e) {
              if (e.Id == userId) {
                  $scope.displayingUser = e.Username;
              };
          });
      });


      $scope.status = {
          isopen: false
      };

      $scope.toggleDropdown = function ($event) {
          $event.preventDefault();
          $event.stopPropagation();
          $scope.status.isopen = !$scope.status.isopen;
      };

      var matches = BracketsPredicction.get({ userId: userId }, function () {
          $scope.matches = matches;
      });

      $scope.alerts = [];
      $scope.closeAlert = function (index) {
          $scope.alerts.splice(index, 1);
      };

      $scope.showEditOption = true;
      $scope.open = function (situation) {

          var modalInstance = $modal.open({
              templateUrl: 'Content/views/editResultModal.html',
              controller: 'EditResultModalCtrl',
              resolve: {
                  situationText: function () {
                      return situation.Key + ' (Etapa ' + situation.Etapa + ')';
                  }, resourceClass: function () {
                      return BracketsPosibleResults
                  }, resourceId: function () {
                      return situation.Id;
                  }
              }
          });

          modalInstance.result.then(function (result) {
              result.situationId = situation.Id;
              var a = result.$save().finally(function () {
                  $scope.alerts.push({ type: 'warning', strong: 'Resultado guardado. ', msg: 'Recargar la pagina para ver el nuevo puntaje.' });
              });
          }, function () {
              $log.info('Modal dismissed at: ' + new Date());
          });
      };
  });

