
/* tslint:disable:interface-name */
interface Window { toastr: Toastr; }
/* tslint:enable */

module Notify {
    'use strict';

    export var moduleName = 'Notify';

    export var instance =
        angular.module(
            Notify.moduleName,
            []
        )
        .value('toastr', window.toastr);
}
