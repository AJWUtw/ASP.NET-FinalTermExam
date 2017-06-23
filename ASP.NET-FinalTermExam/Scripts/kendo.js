$(document).ready(function () {
    function Grid() {
        var grid = this;

        this.search = function () {
            $.post("/Cus/SearchCus", $("#searchCus").serialize(),
               function (data) {
                   $('#CusGrid').data("kendoGrid").dataSource.data(data);
               }, "json");
        }


        this.insert = function (e) {
            var validator = $("#insertForm").kendoValidator().data("kendoValidator");

            var data = $('#insertForm').serializeObject();
            if (validator.validate()) {
                console.log(validator);
                $.ajax({
                    url: '/Cus/InsertCus',
                    type: 'POST',
                    data: JSON.stringify(data),
                    contentType: 'application/json',
                    success: function (data) {
                        console.log(data);
                        if (data) {
                            var window = $("#insertWindow").data("kendoWindow");
                            window.close();
                        } else {
                            alert("資料輸入失敗");
                        }
                    }
                });
            }
        }

        this.getInsertWindow = function (e) {
            $("#insertWindow").data("kendoWindow").open();
        }

        this.getUpdateWindow = function (e) {
            var dataItem = this.dataItem($(e.currentTarget).closest('tr'));
            console.log(dataItem);
            var cusID = dataItem.CustomerID;
            $("#updateWindow").kendoWindow({
                position: {
                    top: 100,
                    left: "20%"
                },
                title: "UPDATE",
                visible: false,
                actions: ["Close"],
                content: {
                    url: "/Cus/GetUpdateWindow?id=" + cusID
                }
            });
            $("#updateWindow").data("kendoWindow").open();
        }

        this.del = function (id) {
            console.log(id);
            $.ajax({
                url: '/Cus/DeleteCus?id='+id,
                type: 'GET',
                contentType: 'application/json',
                success: function (data) {
                    console.log(data);
                }
            });
        }


        this.setDropDownList = function (action, columns) {
            $("#" + action).kendoDropDownList({
                optionLabel: "-Select-",
                placeholder: "Select " + columns,
                dataTextField: "text",
                dataValueField: "value",
                validate: {
                    required: true,
                },
                autoBind: true,
                editable: false,
                dataSource: {
                    type: "json",
                    transport: {
                        read: {
                            url: "/Cus/Read" + columns + "List",
                        }
                    }
                }
            });
        }

        this.cleanSearch = function () {
            $("#searchCus")[0].reset();
        }





    }

    var grid = new Grid();

    $("#CusGrid").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: "./Cus/GetCusGrid"
            },
            pageSize: 20
        },
        height: 550,
        groupable: true,
        sortable: true,
        editable: "popup",
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        remove: function (e) {
            console.log()
            grid.del(e.model.CustomerID);
        },
        columns: [{
            field: "CustomerID",
            title: "編號"
        }, {
            field: "CompanyName",
            title: "名稱"
        }, {
            field: "ContactName",
            title: "聯絡人姓名"
        }, {
            field: "CodeVal",
            title: "聯絡人職稱"
        }, {
            command: { text: "Update", click: grid.getUpdateWindow }, title: "Update"
        }, {
            command: "destroy", title: "Delete"
        }
        ]
    });

    grid.setDropDownList("searchContactTitle", "ContactTitle");
    grid.setDropDownList("insertContactTitle", "ContactTitle");


    $("#insertCreationDate").kendoDatePicker({ dateInput: true, format: "yyyy-MM-dd", parseFormats: ["yyyy/MM/dd"] });

    $("#searchBtn").kendoButton({ click: grid.search });
    $("#cleanSearchBtn").kendoButton({ click: grid.cleanSearch });
    $("#insertBtn").kendoButton({ click: grid.insert });
    $("#getInsertWindow").kendoButton({ click: grid.getInsertWindow });

    $("#insertWindow").kendoWindow({
        position: {
            top: 100,
            left: "20%"
        },
        title: "INSERT",
        visible: false,
        actions: ["Close"],
        close: function () {
            $("#insertWindow").find('form')[0].reset();
        }
    });


    $.fn.serializeObject = function () {
        var o = {};
        var a = this.serializeArray();
        $.each(a, function () {
            if (o[this.name]) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    };
});