'use strict';

angular.module('prodeApp').
       factory('GroupsPredicction', ['$resource', function ($resource) {
           return $resource(
               "/api/groupspredictions",
               {userId: '@userId'}
          );
       }]);

angular.module('prodeApp').
       factory('BracketsPredicction', ['$resource', function ($resource) {
           return $resource(
               "/api/bracketspredictions",
               { userId: '@userId' }
          );
       }]);

angular.module('prodeApp')
    .service('Authentication', ['$http', 'Session', function ($http, Session) {
        this.loginWithToken = function (token) {
            return $http({
                method: 'GET',
                url: '/api/authentication?token=' + token
            }).success(function (user, status, headers, config) {
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
    }])
    .service('Session', function ($cookies) {
        this.create = function (user) {
            this.id = user.Id;
            this.userId = user.Id;
            this.userName = user.Username;

            $cookies.id = this.id;
            $cookies.userId = this.userId;
            $cookies.userName = this.userName;
        };
        this.userLogged = function () {
            return this.userId != null && this.userId != 0;
        };
        this.getSession = function () {
            if (!this.id)
                this.id = $cookies.id;
            if (!this.userId)
                this.userId = $cookies.userId;
            if (!this.userName)
                this.userName = $cookies.userName;
            return this;
        };
        this.destroy = function () {
            this.id = null;
            this.userId = null;
            this.userName = null;

            $cookies.id = {};
            $cookies.userId = {};
            $cookies.userName = {};
        };
        return this;
    });