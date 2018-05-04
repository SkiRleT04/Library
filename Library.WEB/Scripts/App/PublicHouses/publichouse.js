var selectedItems = [];
function onSelected(arg) {
    selectedItems = this.selectedKeyNames();
    console.log(selectedItems);
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
    columns: [
        { selectable: true, width: "50px" },
        { field: "PublicHouseName" },
        { field: "Country" },
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
                url: "/PublicHouses/Read"
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
                    PublicHouseId: { type: "number" },
                    PublicHouseName: { type: "string" },
                    Country: { type: "string" }
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