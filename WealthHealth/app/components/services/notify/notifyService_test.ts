/// <reference path="../../../../scripts/typings/jasmine/jasmine.d.ts" />

describe('Notify', () => {

    var toastrMock;

    var injectedNotifier: Notify.Notify;

    beforeEach(angular.mock.module(Notify.moduleName));
    
    // Setup mock for the toastr library
    beforeEach(() => {

        toastrMock = {
            error: (message: string, title: string) => {
            },
            info: (message: string, title: string) => {
            },
            success: (message: string, title: string) => {
            },
            warning: (message: string, title: string) => {
            },
            clear: () => {
            },
            options: {
                timeOut: 0,
                positionClass: ''
            }
        };

        angular.mock.module(($provide) => {
            $provide.value('toastr', toastrMock);
        });
    });

    beforeEach(inject((notify: Notify.Notify) => {
        injectedNotifier = notify;
    }));

    it('should configure toastr options', () => {
        // Arrange

        // Act
        injectedNotifier.clear();

        // Assert
        expect(toastrMock.options.timeOut).toBeGreaterThan(0);
        expect(toastrMock.options.positionClass).not.toEqual('');
    });

    it('should pass through error message to toastr error', () => {
        // Arrange
        var message = 'ErrorTest';
        var title = 'MyError';

        spyOn(toastrMock, "error");
        spyOn(toastrMock, "info");
        spyOn(toastrMock, "success");
        spyOn(toastrMock, "warning");

        // Act
        injectedNotifier.error(message, title);

        // Assert
        expect(toastrMock.info).not.toHaveBeenCalled();
        expect(toastrMock.success).not.toHaveBeenCalled();
        expect(toastrMock.warning).not.toHaveBeenCalled();
        expect(toastrMock.error).toHaveBeenCalled();
        expect(toastrMock.error).toHaveBeenCalledWith(message, title);
    });

    it('should pass through error message and default title to toastr error', () => {
        // Arrange
        var message = 'ErrorTest';

        spyOn(toastrMock, "error");

        // Act
        injectedNotifier.error(message);

        // Assert
        expect(toastrMock.error).toHaveBeenCalled();
        expect(toastrMock.error).toHaveBeenCalledWith(message, 'Error');
    });

    it('should pass through info message to toastr info', () => {
        // Arrange
        var message = 'InfoTest';
        var title = 'MyInfo';

        spyOn(toastrMock, "error");
        spyOn(toastrMock, "info");
        spyOn(toastrMock, "success");
        spyOn(toastrMock, "warning");

        // Act
        injectedNotifier.info(message, title);

        // Assert
        expect(toastrMock.error).not.toHaveBeenCalled();
        expect(toastrMock.success).not.toHaveBeenCalled();
        expect(toastrMock.warning).not.toHaveBeenCalled();
        expect(toastrMock.info).toHaveBeenCalled();
        expect(toastrMock.info).toHaveBeenCalledWith(message, title);
    });

    it('should pass through info message and default title to toastr info', () => {
        // Arrange
        var message = 'InfoTest';

        spyOn(toastrMock, "info");

        // Act
        injectedNotifier.info(message);

        // Assert
        expect(toastrMock.info).toHaveBeenCalled();
        expect(toastrMock.info).toHaveBeenCalledWith(message, 'Info');
    });

    it('should pass through success message to toastr success', () => {
        // Arrange
        var message = 'SuccessTest';
        var title = 'MySuccess';

        spyOn(toastrMock, "error");
        spyOn(toastrMock, "info");
        spyOn(toastrMock, "success");
        spyOn(toastrMock, "warning");

        // Act
        injectedNotifier.success(message, title);

        // Assert
        expect(toastrMock.error).not.toHaveBeenCalled();
        expect(toastrMock.info).not.toHaveBeenCalled();
        expect(toastrMock.warning).not.toHaveBeenCalled();
        expect(toastrMock.success).toHaveBeenCalled();
        expect(toastrMock.success).toHaveBeenCalledWith(message, title);
    });

    it('should pass through success message and default title to toastr success', () => {
        // Arrange
        var message = 'SuccessTest';

        spyOn(toastrMock, "success");

        // Act
        injectedNotifier.success(message);

        // Assert
        expect(toastrMock.success).toHaveBeenCalled();
        expect(toastrMock.success).toHaveBeenCalledWith(message, 'Success');
    });

    it('should pass through warning message to toastr warning', () => {
        // Arrange
        var message = 'WarningTest';
        var title = 'MyWarning';

        spyOn(toastrMock, "error");
        spyOn(toastrMock, "info");
        spyOn(toastrMock, "success");
        spyOn(toastrMock, "warning");

        // Act
        injectedNotifier.warning(message, title);

        // Assert
        expect(toastrMock.error).not.toHaveBeenCalled();
        expect(toastrMock.info).not.toHaveBeenCalled();
        expect(toastrMock.success).not.toHaveBeenCalled();
        expect(toastrMock.warning).toHaveBeenCalled();
        expect(toastrMock.warning).toHaveBeenCalledWith(message, title);
    });

    it('should pass through warning message and default title to toastr warning', () => {
        // Arrange
        var message = 'WarningTest';

        spyOn(toastrMock, "warning");

        // Act
        injectedNotifier.warning(message);

        // Assert
        expect(toastrMock.warning).toHaveBeenCalled();
        expect(toastrMock.warning).toHaveBeenCalledWith(message, 'Warning');
    });

    it('should keep track of notifications by group and allow to clear by group', () => {
        // Arrange
        var message = 'Message';
        var title = 'Title';
        var groupName = 'group';

        spyOn(toastrMock, "clear");

        // Act
        injectedNotifier.warning(message, title, groupName);
        injectedNotifier.warning(message, title, groupName);
        injectedNotifier.warning(message, title, groupName);
        injectedNotifier.warning(message, title, groupName);
        injectedNotifier.warning(message, title, groupName);
        injectedNotifier.clear(groupName);

        // Assert
        expect(toastrMock.clear).toHaveBeenCalled();
        expect(toastrMock.clear.calls.count()).toEqual(5);
    });

    it('should clear all notifications with single paramaterless call to clear function', () => {
        // Arrange
        var message = 'Message';
        var title = 'Title';
        var groupName = 'group';

        spyOn(toastrMock, "clear");

        // Act
        injectedNotifier.warning(message, title, groupName);
        injectedNotifier.warning(message, title, groupName);
        injectedNotifier.warning(message, title, groupName);
        injectedNotifier.warning(message, title, groupName);
        injectedNotifier.warning(message, title, groupName);
        injectedNotifier.clear();

        // Assert
        expect(toastrMock.clear).toHaveBeenCalled();
        expect(toastrMock.clear.calls.count()).toEqual(1);
    });
});