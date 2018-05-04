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
        { field: "FirstName" },
        { field: "LastName" },
        { field: "DateOfBirth", template: "#= kendo.toString(kendo.parseDate(DateOfBirth, 'yyyy-MM-dd'), 'MM/dd/yyyy') #" },
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
                url: "/Authors/Read"
            },
            create: {
                url: "/Authors/Create"
            },
            update: {
                url: "/Authors/Update"
            },
            destroy: {
                url: "/Authors/Destroy"
            },
            change: function (e) {
                console.log(e);
            }
        },
        schema: {
            data: "Data",
            model: {
                id: "AuthorId",
                fields: {
                    Id: { type: "number" },
                    FirstName: { type: "string", validation: { required: true } },
                    LastName: { type: "string", validation: { required: true } },
                    DateOfBirth: { type: "date" }
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