module WealthHealth {
    'use strict';

    export interface IDashboardScope extends ng.IScope {
    }

    export class DashboardCtrl {
        static $inject = ['$scope'];

        constructor(
            public $scope: IDashboardScope
        ) {
        }
    }
}

WealthHealth.instance.controller('dashboardCtrl', WealthHealth.DashboardCtrl);
 