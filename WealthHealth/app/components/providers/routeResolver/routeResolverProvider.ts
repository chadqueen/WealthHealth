
module RouteResolver {
    'use strict';

    export interface IRouteDefinition extends ng.route.IRoute {
        routeName: string;
        controller: string;
        templateUrl: string;
    }

    export class RouteResolverProvider {

        private routes: { [routeName: string]: IRouteDefinition } = {};

        $get() {
            return this;
        }

        createRoute(config: IRouteDefinition): IRouteDefinition {
            
            if (!config) {
                throw new Error('route config is required.');
            }

            if (!config.routeName) {
                throw new Error('routeName is required.');
            }

            if (this.routes[config.routeName]) {
                throw new Error('A route with this name already exists: ' + config.routeName);
            }

            if (!config.controller) {
                throw new Error('controller is required.');
            }

            if (!config.templateUrl) {
                throw new Error('templateUrl is required.');
            }

            config = <IRouteDefinition> config;
            this.routes[config.routeName] = config;

            return config;
        }

        public getRoute(routeName: string): IRouteDefinition {
            if (!this.routes[routeName]) {
                throw new Error('Route does not exist: ' + routeName);
            }

            return this.routes[routeName];
        }
    }
}

//Must be a provider since it will be injected into module.config()    
RouteResolver.instance.provider('routeResolver', [RouteResolver.RouteResolverProvider]);
