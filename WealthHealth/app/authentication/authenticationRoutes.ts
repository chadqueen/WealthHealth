
module Authentication {
    'use strict';

    export var routes = ['$routeProvider', 'routeResolverProvider',
        ($routeProvider: ng.route.IRouteProvider, routeResolverProvider: RouteResolver.RouteResolverProvider) => {

            // helpers for quickly defining routes
            var rr = routeResolverProvider;
            var authenticationSectionPathPrefix = '../app/authentication/';
            var authenticationTemplatePathPrefix = authenticationSectionPathPrefix + 'templates/';

            var buildRouteTemplatePath = (templateName: string): string => {
                return authenticationTemplatePathPrefix + templateName + '.html';
            };

            // define the routes
            $routeProvider
                // register
                .when('/register', rr.createRoute({
                    routeName: 'register',
                    controller: 'registerCtrl',
                    templateUrl: buildRouteTemplatePath('register'),
                }));
        }];
}

Authentication.instance.config(Authentication.routes);
 