/*
comment here
Deletes the row and shows the updated Grid
This GridUtil is common and it is used in Layout.cshtml
this function is called from, When the user is clicked on the grid delete button. 
*/

function ConfirmDelete(id) {
    //if (document.getElementById("hdnPermission").value == "ReadOnly") {
    //    bootbox.alert(RND.Constants.AccessDenied);
    //}
    //else
    {
        bootbox.confirm({
            message: VistaEDI.Constants.AreYouDelete,
            buttons: {
                confirm: {
                    label: 'Yes',
                    className: 'btn-success'
                },
                cancel: {
                    label: 'No',
                    className: 'btn-danger'
                }
            },
            callback: function (result) {
               
                return result;
            }
        });
    }
  
}


function DeleteGridRow(id, url, gridId) {
    //if (document.getElementById("hdnPermission").value == "ReadOnly") {
    //    bootbox.alert(RND.Constants.AccessDenied);
    //}
    //else
    {
        bootbox.confirm({
            message: VistaEDI.Constants.AreYouDelete,
            buttons: {
                confirm: {
                    label: 'Yes',
                    className: 'btn-success'
                },
                cancel: {
                    label: 'No',
                    className: 'btn-danger'
                }
            },
            callback: function (result) {
                if (result) {
                    var obj = { id: id };
                    $.ajax({
                        type: 'post',
                        url: url,
                        data: obj
                    })
                    .done(function (data) {
                       
                        if (data && data.isSuccess) {
                           
                            $('#' + gridId).repeater('render');
                        }
                        else {
                          
                            bootbox.alert(data.message);
                        }
                    });
                }
            }
        });
    }
 
}

function LoadGrid(id) {
    // initialize the repeater
    var repeater = $('#' + id);
    repeater.repeater({
        list_selectable: false, // (single | multi)
        list_noItemsHTML: 'No Records Found.',

        // override the column output via a custom renderer.
        // this will allow you to output custom markup for each column.
        list_columnRendered: customColumnRenderer,

        // override the row output via a custom renderer.
        // this example will use this to add an "id" attribute to each row.
        list_rowRendered: customRowRenderer,

        // setup your custom datasource to handle data retrieval;
        // responsible for any paging, sorting, filtering, searching logic
        dataSource: customDataSource
    });
}