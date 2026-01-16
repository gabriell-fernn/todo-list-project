sap.ui.define(
  [
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel",
    "sap/m/MessageToast",
    "sap/ui/core/Fragment"
  ],
  function (Controller, JSONModel, MessageToast, Fragment) {
    "use strict";

    return Controller.extend("com.gabriell.controller.Main", {
      _sApiUrl: "https://localhost:7085/api/todos",

      onInit: function () {
        var oDataModel = new JSONModel({
          items: [],
        });
        this.getView().setModel(oDataModel, "todos");

        var oCreateModel = new JSONModel({
            title: "",
            userId: 1
        });
        this.getView().setModel(oCreateModel, "create");

        var oViewModel = new JSONModel({
          filterTitle: "",
          sortField: "title",
          sortOrder: "asc",
          page: 1,
          pageSize: 10,
          busy: false,
        });
        this.getView().setModel(oViewModel, "view");

        this._loadTodos();
      },

      _loadTodos: async function () {
        var oViewModel = this.getView().getModel("view");
        var oDataModel = this.getView().getModel("todos");

        oViewModel.setProperty("/busy", true);

        var oParams = new URLSearchParams({
          page: oViewModel.getProperty("/page"),
          pageSize: oViewModel.getProperty("/pageSize"),
          sort: oViewModel.getProperty("/sortField"),
          order: oViewModel.getProperty("/sortOrder"),
        });

        var sTitle = oViewModel.getProperty("/filterTitle");
        if (sTitle) {
          oParams.append("title", sTitle);
        }

        try {
          const response = await fetch(
            `${this._sApiUrl}?${oParams.toString()}`
          );

          if (!response.ok) throw new Error("Erro ao buscar tarefas");

          const data = await response.json();

          oDataModel.setProperty("/items", data.data);
          oViewModel.setProperty("/totalCount", data.totalCount);
          oViewModel.setProperty("/totalPages", data.totalPages);
        } catch (error) {
          MessageToast.show("Falha na comunicação: " + error.message);
        } finally {
          oViewModel.setProperty("/busy", false);
        }
      },

      onFilterSearch: function (oEvent) {
        var sQuery = oEvent.getParameter("query");
        var oViewModel = this.getView().getModel("view");

        oViewModel.setProperty("/page", 1);
        oViewModel.setProperty("/filterTitle", sQuery);

        this._loadTodos();
      },

      onPrevPage: function () {
        var oViewModel = this.getView().getModel("view");
        var iCurrentPage = oViewModel.getProperty("/page");

        if (iCurrentPage > 1) {
          oViewModel.setProperty("/page", iCurrentPage - 1);
          this._loadTodos();
        }
      },

      onNextPage: function () {
        var oViewModel = this.getView().getModel("view");
        var iCurrentPage = oViewModel.getProperty("/page");

        oViewModel.setProperty("/page", iCurrentPage + 1);
        this._loadTodos();
      },

      onSortById: function () {
        var oViewModel = this.getView().getModel("view");

        oViewModel.setProperty("/sortField", "id");

        var currentOrder = oViewModel.getProperty("/sortOrder");
        oViewModel.setProperty("/sortOrder", currentOrder === "asc" ? "desc" : "asc");

        this._loadTodos();
      },

      onOpenCreateDialog: function () {
          if (!this._oCreateDialog) {
              this._oCreateDialog = sap.ui.xmlfragment(
                  "com.gabriell.view.CreateTodoDialog",
                  this
              );
              this.getView().addDependent(this._oCreateDialog);
          }

          this.getView().getModel("create").setData({
              title: "",
              userId: ""
          });

          this._oCreateDialog.open();
      },

      onCloseCreateDialog: function () {
          this._oCreateDialog.close();
      },

      onShowDetails: async function (oEvent) {
          try {
              var oContext = oEvent.getSource().getBindingContext("todos");
              if (!oContext) {
                  MessageToast.show("Não foi possível obter a tarefa.");
                  return;
              }

              var oTodo = oContext.getObject();
              var id = oTodo.id;

              if (!this._pDetailsDialog) {
                  this._pDetailsDialog = Fragment.load({
                      id: this.getView().getId(),
                      name: "com.gabriell.view.TodoDetailsDialog",
                      controller: this
                  });
              }

              var oDialog = await this._pDetailsDialog;

              var oDetailsModel = new JSONModel({ busy: true });
              oDialog.setModel(oDetailsModel, "details");
              oDialog.open();

              const response = await fetch(`${this._sApiUrl}/${id}`);
              if (!response.ok) {
                  throw new Error("Erro ao buscar detalhes da tarefa");
              }

              const details = await response.json();

              oDetailsModel.setData({
                  ...details,
                  busy: false
              });
          } catch (error) {
              console.error("Erro ao abrir detalhes:", error);
              MessageToast.show(error.message || "Falha ao abrir detalhes");
          }
      },

      onCloseDetailsDialog: async function () {
          if (!this._pDetailsDialog) return;
          var oDialog = await this._pDetailsDialog;
          oDialog.close();
      },

      onCreateTodo: async function () {
          var oCreateModel = this.getView().getModel("create");
          var oData = oCreateModel.getData();

          if (!oData.title) {
              sap.m.MessageToast.show("Informe o título");
              return;
          }

          try {
              const response = await fetch(
                  "https://localhost:7085/api/todos",
                  {
                      method: "POST",
                      headers: {
                          "Content-Type": "application/json"
                      },
                      body: JSON.stringify(oData)
                  }
              );

              if (!response.ok) {
                  const err = await response.json();
                  throw new Error(err.errorMessages?.[0] || "Erro ao criar tarefa");
              }

              sap.m.MessageToast.show("Tarefa criada com sucesso!");

              this._oCreateDialog.close();
              this._loadTodos();
          } catch (error) {
              sap.m.MessageToast.show(error.message);
          }
      },

      onToggleCompleted: async function (oEvent) {
        var oContext = oEvent.getSource().getBindingContext("todos");
        var oTask = oContext.getObject();
        var sPath = oContext.getPath();

        var oPayload = {
          completed: oEvent.getParameter("selected"),
        };

        try {
          const response = await fetch(`${this._sApiUrl}/${oTask.id}`, {
            method: "PUT",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify(oPayload),
          });

          if (!response.ok) {
            const err = await response.json();
            this.getView()
              .getModel("todos")
              .setProperty(sPath + "/completed", !oPayload.completed);
            throw new Error(err.errorMessages?.[0] || "Erro ao atualizar status");
          }

          MessageToast.show("Status atualizado!");
        } catch (error) {
          MessageToast.show(error.message);
        }
      },

      onSyncData: async function () {
        try {
          const response = await fetch("https://localhost:7085/api/todos/sync", { method: "POST" });
          if (response.ok) {
            MessageToast.show("Sincronização concluída");
            this._loadTodos();
          }
        } catch (error) {
          console.error(error);
        }
      },
    });
  }
);
