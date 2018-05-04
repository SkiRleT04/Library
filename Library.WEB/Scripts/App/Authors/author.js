function booksTemplate(data) {
    var res = [];
    $.each(data.Books, function (idx, elem) {
        res.push(elem.Name);
    });
    return res.join(", ");
}

var selectedItems = [];
function onSelected(arg) {
    selectedItems = this.selectedKeyNames();
    console.log(selectedItems);
}
$("#grid").kendoGrid({
    height: 700,
    columns: [
        { selectable: true, width: "50px" },
        { field: "FirstName" },
        { field: "LastName" },
        { field: "DateOfBirth", template: "#= kendo.toString(kendo.parseDate(DateOfBirth, 'yyyy-MM-dd'), 'MM/dd/yyyy') #" },
        { field: "Books", template: booksTemplate }
    ],
    toolbar:
        [
            "pdf",
            {
                name: "SaveToJson",
                text: "Save to JSON",
            },
            {
                name: "SaveToXml",
                text: "Save to XML",
            }
            ,
            {
                name: "LoadFromFile",
                text: "Load from file",
            }
            ,
            {
                name: "Refresh",
                text: "Refresh",
            }
        ],
    dataSource: {
        type: "aspnetmvc-ajax",
        transport: {
            read: {
                url: "/Authors/Read"
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
                    AuthorId: { type: "number" },
                    FirstName: { type: "string" },
                    LastName: { type: "string" },
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
    persistSelection: true,
    change: onSelected,
    scrollable: true,
    columnMenu: true,
    navigatable: true,
    editable: "inline",

});