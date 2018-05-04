$("#grid").kendoGrid({
    height: 700,
    editable: "popup",
    columns: [
        { field: "Name" },
        { field: "TypeOfCover" },
        { field: "NumberOfPages" },
        { field: "Actions", command: ["edit", "destroy"], width: 180 }
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
                url: "/Brochures/Read"
            },
            create: {
                url: "/Brochures/Create"
            },
            update: {
                url: "/Brochures/Update"
            },
            destroy: {
                url: "/Brochures/Destroy"
            },
            change: function (e) {
                console.log(e);
            }
        },
        schema: {
            data: "Data",
            model: {
                id: "BrochureId",
                fields: {
                    BrochureId: { type: "number" },
                    Name: { type: "string", validation: { required: true } },
                    TypeOfCover: { type: "string", validation: { required: true } },
                    NumberOfPages: { type: "number", validation: { required: true } }
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