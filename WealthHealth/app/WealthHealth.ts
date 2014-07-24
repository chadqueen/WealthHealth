
module WealthHealth {
    'use strict';

    export var moduleName = 'WealthHealth';

    export var instance =
        angular.module(
            WealthHealth.moduleName,
            [
                'ngRoute',
                'ngAnimate',
                'Notify',
                'RouteResolver',
                'Authentication'
            ]
        );
} 