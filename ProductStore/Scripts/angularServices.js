'use strict';

angular.module('prodeApp').
       factory('GroupsPredicction', ['$resource', function ($resource) {
           return $resource(
               "/api/groupspredictions",
               { userId: '@userId', id: '@Id', resultado: '@Resultado' }
          );
       }]).
       factory('BracketsPredicction', ['$resource', function ($resource) {
           return $resource(
               "/api/bracketspredictions",
               { userId: '@userId' }
          );
       }]).
    factory('MejoresPredicction', ['$resource', function ($resource) {
        return $resource(
            "/api/mejores",
            { userId: '@userId' }
       );
    }]).
       factory('Users', ['$resource', function ($resource) {
           return $resource(
               "/api/users"
          );
       }]).
       factory('GroupsPosibleResults', ['$resource', function ($resource) {
           return $resource("/api/groupspredictions/GetPosibleResults", { situationId: '@situationId' });
       }]).
         factory('BracketsPosibleResults', ['$resource', function ($resource) {
             return $resource("/api/bracketspredictions/GetPosibleResult", { situationId: '@situationId', result: '@Description' });
         }]);

angular.module('prodeApp')
    .service('Authentication', ['$http', 'Session', function ($http, Session) {
        this.loginWithToken = function (token) {
            return $http({
                method: 'GET',
                url: '/api/authentication?token=' + token
            }).success(function (user, status, headers, config) {
                //ga('set', '&uid', user.Id); // Establezca el ID de usuario mediante el user_id con el que haya iniciado sesion.
                Session.create(user);
            }).error(function (data, status, headers, config) {
                if (status == 404)
                    alert("Codigo incorrecto.");
                else
                    alert("Error. Status = " + status);
                console.log(data);
            });
        };
        this.isAuthenticated= function () {
            return !!Session.getSession().userId;
        }
        this.logout = function () {
            Session.destroy();
        }
    }])
    .service('Session', function ($cookieStore) {
        this.create = function (user) {
            this.id = user.Id;
            this.userId = user.Id;
            this.userName = user.Username;

            $cookieStore.put('session', {
                id: this.id,
                userId: this.userId,
                userName: this.userName
            });

            logCurrentUser = user.Id;
            log('logIn', '');
        };
        this.userLogged = function () {
            return this.userId != null && this.userId != 0;
        };
        this.getSession = function () {
            if (!this.userId) {
                var sessionCookie = $cookieStore.get('session');
                if (sessionCookie) {
                    this.id = sessionCookie.id;
                    this.userId = sessionCookie.userId;
                    this.userName = sessionCookie.userName;
                }
            }
            logCurrentUser = this.id;
            return this;
        };
        this.destroy = function () {

            log('logOut', '');
            logCurrentUser = 0;
            
            this.id = null;
            this.userId = null;
            this.userName = null;

            $cookieStore.remove('session');
        };
        return this;
    })
    