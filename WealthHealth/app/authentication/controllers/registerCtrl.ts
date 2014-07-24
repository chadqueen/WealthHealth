module Authentication {
    'use strict';

    export interface IRegisterScope extends ng.IScope {
    }

    export class RegisterCtrl  {
        static $inject = ['$scope'];

        constructor(
            public $scope: IRegisterScope
        ) {
        }
    }
}

Authentication.instance.controller('registerCtrl', Authentication.RegisterCtrl);
