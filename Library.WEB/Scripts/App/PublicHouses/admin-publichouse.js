function booksEditor(container, options) {
    $("<select multiple='multiple' data-bind='value : Books'/>")
        .appendTo(container).kendoMultiSelect({
            dataSource: {
                transport: {
                    read: {
                        url: '/Books/ReadForDropDownSelect'
                    },
                    serverFiltering: true
                }
            },
            dataTextField: "Name",
            dataValueField: "BookId"
        });
}


function booksTemplate(data) {
    var res = [];
    $.each(data.Books, function (idx, elem) {
        res.push(elem.Name);
    });
    return res.join(", ");
}



$("#grid").kendoGrid({
    height: 700,
    editable: "popup",
    columns: [
        { field: "PublicHouseName" },
        { field: "Country" },
        {
            field: "Books",
            editor: booksEditor,
            template: booksTemplate
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
                url: "/PublicHouses/Read"
            },
            create: {
                url: "/PublicHouses/Create"
            },
            update: {
                url: "/PublicHouses/Update"
            },
            destroy: {
                url: "/PublicHouses/Destroy"
            },
            change: function (e) {
                console.log(e);
            }
        },
        schema: {
            data: "Data",
            model: {
                id: "PublicHouseId",
                fields: {
                    Id: { type: "number" },
                    PublicHouseName: { type: "string", validation: { required: true } },
                    Country: { type: "string", validation: { required: true } }
                }
            },
            pageSize: 4
        },
        serverPaging: true,
        serverSorting: true
    },


    pageable: {
        pageSizes: true
    },

    scrollable: true,
    columnMenu: true,
    navigatable: true

});