
module Notify {
    'use strict';

    export class NotifyService {

        private toastsByGroup: { [group: string]: JQuery[] } = {};

        constructor(private toastr: Toastr) {
            toastr.options.timeOut = 10000;
            toastr.options.positionClass = 'toast-top-right';
        }

        public error(message: string, title: string = 'Error', group: string = null) {
            this.log('Error: ' + message);
            this.addToast(this.toastr.error(message, title), group);
        }

        public info(message: string, title: string = 'Info', group: string = null) {
            this.log('Info: ' + message);
            this.addToast(this.toastr.info(message, title), group);
        }

        public success(message: string, title: string = 'Success', group: string = null) {
            this.log('Success: ' + message);
            this.addToast(this.toastr.success(message, title), group);
        }

        public warning(message: string, title: string = 'Warning', group: string = null) {
            this.log('Warning: ' + message);
            this.addToast(this.toastr.warning(message, title), group);
        }

        public log(message: string): void {
            var console = window.console;

            /* tslint:disable */
            console.log(message);
            /* tslint:enable */
        }

        /**
         * Clears the toasts by group or all toasts if group is null
         */
        public clear(group: string = null) {
            if (group && this.toastsByGroup[group]) {
                _.each(this.toastsByGroup[group], (toastElem: JQuery) => {
                    this.toastr.clear(toastElem);
                });
                delete this.toastsByGroup[group];
            } else if (group === null) {
                this.toastr.clear();
            }
        }

        private addToast(toastElem: JQuery, group: string) {
            if (group) {
                this.toastsByGroup[group] = this.toastsByGroup[group] || [];
                this.toastsByGroup[group].push(toastElem);
            }
        }
    }
}

Notify.instance.service('notify', ['toastr', Notify.NotifyService]);
