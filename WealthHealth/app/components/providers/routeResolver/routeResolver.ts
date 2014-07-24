
module RouteResolver {
    'use strict';

    export var moduleName = 'RouteResolver';

    export var instance =
        angular.module(
            RouteResolver.moduleName,
            [
                'ngRoute'
            ]
        );
}
