'use strict';

angular.module('prodeApp').
       factory('Authentication', ['$resource', function ($resource) {
           return $resource(
               "/api/authentication",
               {},
               { login: { method: "PUT", username: '@username', password: "@password" } }
          );
       }])
       .factory('AuthService', function ($http, Session) {
            return {
                login: function (credentials) {
                    return $http
                      .post('/login', credentials)
                      .then(function (res) {
                          Session.create(res.id, res.userid, res.role);
                      });
                },
                isAuthenticated: function () {
                    return !!Session.userId;
                }
            };
        })

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

angular.module('prodeApp').
       service('Session', function ($cookies, $http) {
           this.create = function (sessionId, userId, userName) {
               this.id = sessionId;
               this.userId = userId;
               this.userName = userName;

               $cookies.session = {
                   id: this.id,
                   userId: this.userId,
                   userName: this.userName
               };
           };
           
           var session = this;
           this.createFromUserId = function (userId,callback) {
               $http({ method: 'GET', url: '/api/authentication?token=' + userId }).
                success(function (user, status, headers, config) {
                    session.id = user.Id;
                    session.userId = user.Id;
                    session.userName = user.Username;

                    $cookies.id= session.id;
                    $cookies.userId= session.userId;
                    $cookies.userName = session.userName;

                    if (callback)
                        callback();
                }).
                error(function (data, status, headers, config) {
                    alert('can not connect. browser incopatibility')
                });
           };

           this.getSession = function () {
               this.id = $cookies.id;
               this.userId = $cookies.userId;
               this.userName = $cookies.userName;
               return this;
           };

           this.destroy = function () {
               this.id = null;
               this.userId = null;
               this.userName = null;

               $cookies.session = {};
           };
           return this;
       })