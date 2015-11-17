//  angularModalService.js
//
//  Service for showing modal dialogs.

/***** JSLint Config *****/
/*global angular  */
(function () {

    "use strict";

    var module = angular.module("angularModalService", ["ui.bootstrap", "ui.bootstrap.modal"]);

    module.factory("modalService", ["$modal",
        function ($modal) {

            function serviceConfigurations() {

                var configurations = [
                    {   
                        name: "confirmation", 
                        defaults: {
                            backdrop: true,
                            keyboard: true,
                            modalFade: true,
                            templateUrl: "/Content/Dialogs/confirmation.html" 
                        }/*, 
                        options: {
                            closeButtonText: "Close",
                            actionButtonText: "OK",
                            headerText: "Confirmation Required",
                            bodyText: "Are you sure you want to perform this action?"
                        } */
                    },
                    // Waiting Configuration
                    {   
                        name: "waiting", 
                        defaults: {
                            backdrop: true,
                            keyboard: true,
                            modalFade: true,
                            templateUrl: "/Content/Dialogs/waiting.html"
                        }/*, 
                        options: {
                            closeButtonText: "",
                            actionButtonText: "",
                            headerText: "",
                            bodyText: "Please wait until we finish the requested operation..."
                        } */
                    }
                ];

                return configurations;
            }

            function modalService() {

                var configurations = new serviceConfigurations();
                var self = this;

                self.showModal = function (customModalOptions) {

                    if (!customModalOptions) customModalOptions = {};

                    customModalOptions.backdrop = "static";
                    
                    //Create temp objects to work with since we"re in a singleton service
                    var tempModalOptions = {};
                    var defaultConfigName = "confirmation";

                    var config = $.grep(configurations, function (cfg) {
                        return cfg.name === defaultConfigName;
                    })[0]; //It has to find something and just one occurence

                    //Map angular-ui modal custom defaults to modal defaults defined in service
                    angular.extend(tempModalOptions, config.defaults, customModalOptions);

                    //If there is no controller defined
                    if (!tempModalOptions.controller) {

                        tempModalOptions.controller = function($scope, $modalInstance) {

                            $scope.ok = function (result) {
                                $modalInstance.close(result);
                            };

                            $scope.cancel = function(reason) {
                                $modalInstance.dismiss(reason);
                            };
                        }
                    }

                    return $modal.open(tempModalOptions).result;
                };
            }

            return new modalService();
        }
    ]);
}()); //() Executes the Function
