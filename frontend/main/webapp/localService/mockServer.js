sap.ui.define([], function () {
    "use strict";

    var oMockServer = {
        init: function () {
            var oOriginalFetch = window.fetch;

            window.fetch = function (url, options) {
                if (url && url.toString().includes("/api/todos")) {
                    console.log("MockServer: Intercepting request to " + url);

                    return new Promise(function (resolve) {
                        sap.ui.require(["sap/ui/core/util/MockServer"], function () {
                            setTimeout(function () {
                                jQuery.ajax({
                                    url: sap.ui.require.toUrl("com/gabriell/localService/mockdata/todos.json"),
                                    dataType: "json",
                                    success: function (data) {
                                        if (options && options.method === "PUT") {
                                            resolve(new Response(JSON.stringify(JSON.parse(options.body)), {
                                                status: 200,
                                                headers: { "Content-Type": "application/json" }
                                            }));
                                            return;
                                        }

                                        var aItems = data;

                                        resolve(new Response(JSON.stringify(aItems), {
                                            status: 200,
                                            headers: { "Content-Type": "application/json" }
                                        }));
                                    },
                                    error: function () {
                                        resolve(new Response("Error loading mock data", { status: 500 }));
                                    }
                                });
                            }, 500);
                        });
                    });
                }

                return oOriginalFetch.apply(this, arguments);
            };

            console.log("MockServer initialized");
        }
    };

    return oMockServer;
});
