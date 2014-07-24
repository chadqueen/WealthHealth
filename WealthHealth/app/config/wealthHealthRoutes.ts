
module WealthHealth.Config {
    'use strict';

    export var routes = ['$routeProvider', '$locationProvider', 'routeResolverProvider',
        (
            $routeProvider: ng.route.IRouteProvider,
            $locationProvider: ng.ILocationProvider,
            routeResolverProvider: RouteResolver.RouteResolverProvider
        ) => {

            var appTemplatePathPrefix = '../app/';

            var buildRouteTemplatePath = (templateRelativeFilePath: string): string => {
                return appTemplatePathPrefix + templateRelativeFilePath + '.html';
            };

            $locationProvider.html5Mode(true);

            // helpers for quickly defining routes
            var rr = routeResolverProvider;

            // define the routes
            $routeProvider
            // register
                .when('/', rr.createRoute({
                    routeName: 'dashboard',
                    controller: 'dashboardCtrl',
                    templateUrl: buildRouteTemplatePath('sections/dashboard/templates/dashboard')
                }));
        }];
}

WealthHealth.instance.config(WealthHealth.Config.routes);
