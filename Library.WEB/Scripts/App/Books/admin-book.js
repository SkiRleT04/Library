function publicHouseEditor(container, options) {
    $("<select multiple='multiple' data-bind='value : PublicHouses'/>")
        .appendTo(container).kendoMultiSelect({
            dataSource: {
                transport: {
                    read: {
                        url: '/PublicHouses/ReadForDropDownSelect'
                    },
                    serverFiltering: true
                }
            },
            dataTextField: "PublicHouseName",
            dataValueField: "PublicHouseId"
        });
}


function authorsEditor(container, options) {
    $("<select multiple='multiple' data-bind='value : Authors'/>")
        .appendTo(container).kendoMultiSelect({
            dataSource: {
                transport: {
                    read: {
                        url: '/Authors/ReadForDropDownSelect'
                    },
                    serverFiltering: true
                }
            },
            dataTextField: "LastName",
            dataValueField: "AuthorId"
        });
}


function authorsTemplate(data) {
    var res = [];
    $.each(data.Authors, function (idx, elem) {
        res.push(elem.FirstName + " " + elem.LastName);
    });
    return res.join(", ");
}


function publicHouseTemplate(data) {
    var res = [];
    $.each(data.PublicHouses, function (idx, elem) {
        res.push(elem.PublicHouseName);
    });
    return res.join(", ");
}



    $("#grid").kendoGrid({
        height: 700,
        editable: "popup",
        columns: [
            {field: "Name" },
            { field: "Authors", template: authorsTemplate, editor: authorsEditor },
            { field: "YearOfPublishing",  },
            {
                field: "PublicHouses",
                editor: publicHouseEditor,
                template: publicHouseTemplate
            },
            { field: "Actions", command: ["edit", "destroy"], width: 180 },
        ],
        toolbar:
            [
                "create",
                "pdf",
            ],
        dataSource: {
            type: "aspnetmvc-ajax",
            transport: {
            read: {
            url: "/Books/Read"
                },
                create: {
            url: "/Books/Create"
                },
                update: {
            url: "/Books/Update"
                },
                destroy: {
                    url: "/Books/Destroy"
                },
                change: function (e) {
                    console.log(e);
                }
            },
            schema: {
            data: "Data",
                model: {
            id: "BookId",
                    fields: {
                        Id: { type: "number" },
                        Name: { type: "string", validation: { required: true } },
                        AuthorName: { type: "string", validation: { required:true } },
                        YearOfPublishing: { type: "number", validation: { required: true, min: 1950, max: (new Date()).getFullYear() } }
                    }
                },
                pageSize: 4
            },
            serverPaging: true,
            serverSorting: true,
            serverSorting: true,
        },


        pageable: {
            pageSizes: true
        },

        scrollable: true,
        columnMenu: true,
        navigatable: true

    });