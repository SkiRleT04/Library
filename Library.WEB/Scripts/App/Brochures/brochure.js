var selectedItems = [];
function onSelected(arg) {
    selectedItems = this.selectedKeyNames();
    console.log(selectedItems);
}
$("#grid").kendoGrid({
    height: 700,
    columns: [
        { selectable: true, width: "50px" },
        { field: "Name" },
        { field: "TypeOfCover" },
        { field: "NumberOfPages" },
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
                url: "/Brochures/Read"
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
                    Name: { type: "string" },
                    TypeOfCover: { type: "string" },
                    NumberOfPages: { type: "number" }
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