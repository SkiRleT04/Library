$("#grid").kendoGrid({
    height: 700,
    editable: "popup",
    columns: [
        { field: "Name" },
        { field: "AuthorName" },
        { field: "YearOfPublishing" },
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
                url: "/Magazines/Read"
            },
            create: {
                url: "/Magazines/Create"
            },
            update: {
                url: "/Magazines/Update"
            },
            destroy: {
                url: "/Magazines/Destroy"
            },
            change: function (e) {
                console.log(e);
            }
        },
        schema: {
            data: "Data",
            model: {
                id: "MagazineId",
                fields: {
                    MagazineId: { type: "number" },
                    Name: { type: "string", validation: { required: true } },
                    AuthorName: { type: "string", validation: { required: true } },
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