
function publicHouseTemplate(data) {
    var res = [];
    $.each(data.PublicHouses, function (idx, elem) {
        res.push(elem.PublicHouseName);
    });
    return res.join(", ");
}


function authorsTemplate(data) {
    var res = [];
    $.each(data.Authors, function (idx, elem) {
        res.push(elem.FirstName + " " + elem.LastName);
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
        {selectable: true, width: "50px" },
        {field: "Name" },
        { field: "Authors", template: authorsTemplate },
        { field: "YearOfPublishing" },
        { field: "PublicHouses", template: publicHouseTemplate }
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
        url: "/Books/Read"
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
    BookId: {type: "number" },
                    Name: {type: "string" },
                    YearOfPublishing: { type: "number" }
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
