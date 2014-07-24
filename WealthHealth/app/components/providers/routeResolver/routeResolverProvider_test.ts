/// <reference path="../../../../scripts/typings/jasmine/jasmine.d.ts" />

describe('RouteResolverProvider', () => {

    var injectedRouteResolver: RouteResolver.RouteResolverProvider;

    beforeEach(module(RouteResolver.moduleName));

    beforeEach(inject((routeResolver: RouteResolver.RouteResolverProvider) => {
        injectedRouteResolver = routeResolver;
    }));

    it('should throw exception when creating routes without provided config', () => {
        // Arrange
        var createRouteWithNullConfig = () => { injectedRouteResolver.createRoute(null); };

        // Act

        // Assert
        expect(createRouteWithNullConfig).toThrow(new Error('route config is required.'));
    });

    it('should throw exception when creating routes without routeName in config', () => {
        // Arrange
        var testRouteConfig: RouteResolver.IRouteDefinition = {
            routeName: 'routeName',
            controller: 'controller',
            templateUrl: 'path'
        };

        testRouteConfig.routeName = null;

        var createRouteWithNullConfigField = () => {
            injectedRouteResolver.createRoute(testRouteConfig);
        };

        // Act

        // Assert
        expect(createRouteWithNullConfigField).toThrow(new Error('routeName is required.'));
    });

    it('should throw exception when creating routes without controller in config', () => {
        // Arrange
        var testRouteConfig: RouteResolver.IRouteDefinition = {
            routeName: 'routeName',
            controller: 'controller',
            templateUrl: 'path'
        };

        testRouteConfig.controller = null;

        var createRouteWithNullConfigField = () => {
            injectedRouteResolver.createRoute(testRouteConfig);
        };

        // Act

        // Assert
        expect(createRouteWithNullConfigField).toThrow(new Error('controller is required.'));
    });

    it('should throw exception when creating routes without templateUrl in config', () => {
        // Arrange
        var testRouteConfig: RouteResolver.IRouteDefinition = {
            routeName: 'routeName',
            controller: 'controller',
            templateUrl: 'path'
        };

        testRouteConfig.templateUrl = null;

        var createRouteWithNullConfigField = () => {
            injectedRouteResolver.createRoute(testRouteConfig);
        };

        // Act

        // Assert
        expect(createRouteWithNullConfigField).toThrow(new Error('templateUrl is required.'));
    });

    it('should be able to retrive previously created route by routeName', () => {
        // Arrange
        var testRouteConfig: RouteResolver.IRouteDefinition = {
            routeName: 'routeName',
            controller: 'controller',
            templateUrl: 'path'
        };

        // Act
        injectedRouteResolver.createRoute(testRouteConfig);
        var confirmedRoute = injectedRouteResolver.getRoute('routeName');

        // Assert
        expect(confirmedRoute).toEqual(testRouteConfig);
    });

    it('should not allow two creation calls with the same routeName', () => {
        // Arrange
        var testRouteConfig: RouteResolver.IRouteDefinition = {
            routeName: 'routeName',
            controller: 'controller',
            templateUrl: 'path'
        };

        var createRouteWithNullConfig = () => {
            injectedRouteResolver.createRoute(testRouteConfig);
        };

        // Act
        injectedRouteResolver.createRoute(testRouteConfig);

        // Assert
        expect(createRouteWithNullConfig).toThrow(new Error('A route with this name already exists: routeName'));
    });

    it('should not override the controller setting if set in the passed config', () => {
        // Arrange
        var testRouteConfig: RouteResolver.IRouteDefinition = {
            routeName: 'routeName',
            controller: 'controller',
            templateUrl: 'path'
        };

        // Act
        var confirmedRoute = injectedRouteResolver.createRoute(testRouteConfig);

        // Assert
        expect(confirmedRoute.controller).toEqual(testRouteConfig.controller);
    });
});
