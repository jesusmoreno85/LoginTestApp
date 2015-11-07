
var app = angular.module("LoginTestApp", ["angularModalService"]);

app.controller("LoginCtrl", function ($scope, $http, $rootScope) {

    $scope.alias = "";
    $scope.password = "";
    $scope.messages = "";
    $scope.isWorking = false;

    $scope.$watch("alias", function (newValue, oldValue) {
        $rootScope.alias = newValue;
    });

    $scope.checkUserCredentials = function () {

        $scope.isWorking = true;

        $http.get("/Login/Login", {
            params: {
                alias: $scope.alias,
                password: $scope.password
            }
        })
        .success(function (data, status, headers, config) {

            $scope.isWorking = false;

            if (data.isValid) {
                location.href = data.redirectUrl;
            } else {
                $scope.messages = "Invalid user credentials";
            }
        }).error(function (data, status, headers, config) {

            $scope.isWorking = false;
            $scope.messages = "Oops... something went wrong";
        });
    };
});

app.controller("ForgotPassCtrl", function($scope, modalService) {

    $scope.showForgotPassDialog = function () {

        var modalOptions = {
            templateUrl: "/Content/Dialogs/passwordRecoveryRequest.html",
            controller: "ForgotPassDialogCtrl"
        };

        modalService.showModal(modalOptions);
    };
});

app.controller("ForgotPassDialogCtrl", function ($scope, $modalInstance) {

    $scope.helpType = "";
    $scope.isRequestInProgress = false;
    $scope.messages = "";
    $scope.okDisabled = true;

    $scope.headerText = "Forgot your password?";
    $scope.bodyText = "We can help you for sure, please choose the type of help you want.";
    $scope.bodyTextFooter = "We are going to send an email to your registered account, make sure you have access to it.";

    $scope.recoveryOptions = [
        { name: "ResetLink", description: "Send a Reset Link" },
        { name: "RecoveryClue", description: "Send a Recovery Clue" }
    ];

    $scope.ok = function(result) {

        $scope.isRequestInProgress = true;

        $http.get("/Login/PasswordRecoveryRequest", {
                params: {
                    alias: $rootScope.alias,
                    recoveryOption: $scope.helpType
                }
            })
            .success(function(data, status, headers, config) {

                $scope.isRequestInProgress = false;

                if (data.IsError === false) {
                    $modalInstance.close(result);
                } else {
                    $scope.messages = data.Message;
                }
            }).error(function(data, status, headers, config) {
                $scope.isRequestInProgress = false;
                $scope.messages = "Oops... something went wrong";
            });
    };

    $scope.close = function (reason) {
        $modalInstance.dismiss(reason);
    };

    $scope.$watch("[helpType,isRequestInProgress]", function (newValue, oldValue) {

        if ($scope.helpType === "" || $scope.isRequestInProgress === true) {
            $scope.okDisabled = true;
        } else {
            $scope.okDisabled = false;
        }
    });

});

app.controller("PassRecoveryCtrl", function ($scope, $http, $rootScope, modalService) {

    function showDialog() {

        var modalOptions = {
            templateUrl: "/Content/Dialogs/passwordRecoveryByResetLink.html",
            controller: "PassRecoveryDialogCtrl",
            size: "sm"
        };

        modalService.showModal(modalOptions);
    }
    
    $scope.setViewModel = function(newViewModel) {
        if (typeof newViewModel !== "undefined" 
                && newViewModel !== null
                && typeof newViewModel.passwordRecoveryGuidId !== "undefined") {
            showDialog();
        }
    }
});

app.controller("PassRecoveryDialogCtrl", function ($scope, $modalInstance) {

    $scope.headerText = "Password Reset";
    $scope.bodyText = "Please, confirm your information.";
    $scope.actionButtonText = "Change Password";

    $scope.alias = "";
    $scope.oldPassword = "";
    $scope.newPassword = "";

    $scope.okDisabled = true;
    $scope.isWorking = true;
    $scope.messages = "";

    $scope.ok = function (result) {
        $modalInstance.close(result);
    };

    $scope.close = function (reason) {
        $modalInstance.dismiss(reason);
    };

    function isValidDataState() {

        if ($scope.alias === ""
            || $scope.oldPassword === ""
            || $scope.newPassword === "") {
            return false;
        }
        else if ($scope.oldPassword === $scope.newPassword) {
            $scope.messages = "New password must be different to the old one";
            return false;
        }

        return true;
    }

    $scope.$watch(["alias,oldPassword,newPassword,isWorking"], function (newValue, oldValue) {

        if (isValidDataState() &&
            !$scope.isWorking) {
            $scope.okDisabled = false;
        } else {
            $scope.okDisabled = true;
        }
    });
});

// Toggle Function
$(".toggle").click(function () {

    // Switches the Icon
    $(this).children("i").toggleClass("fa-pencil");
    // Switches the forms  
    $(".form").animate({
        height: "toggle",
        "padding-top": "toggle",
        "padding-bottom": "toggle",
        opacity: "toggle"
    }, "slow");
});