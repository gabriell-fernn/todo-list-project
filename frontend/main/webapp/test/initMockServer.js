sap.ui.define([
    "com/gabriell/localService/mockServer"
], function (MockServer) {
    "use strict";

    MockServer.init();

    sap.ui.require(["sap/ui/core/ComponentSupport"]);
});
