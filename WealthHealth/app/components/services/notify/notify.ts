/* tslint:disable:interface-name */
interface Window { toastr: Toastr; }
/* tslint:enable */

module Notify {
    'use strict';

    export var moduleName = 'Notify';

    export var instance =
        angular.module(
            moduleName,
            []
            )
            .value('toastr', window.toastr);
} 