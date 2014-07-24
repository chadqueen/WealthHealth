
module Authentication {
    'use strict';

    export var moduleName = 'Authentication';

    export var instance =
        angular.module(
            Authentication.moduleName,
            [
                'ngRoute',
                'ngAnimate',
                'RouteResolver',
                'Notify'
            ]
        );
} 