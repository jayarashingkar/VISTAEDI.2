// define the columns in your datasource
var columns = [
     //{
     //    label: 'Record Id',
     //    property: 'RecId',
     //    sortable: true,
     //    width: '50px'
     //},
       {
           label: 'Select',
           property: 'RecId',
           sortable: true,
           width: '50px'
       },
    {
        label: 'Heat Number',
        property: 'HeatNumber',
        sortable: true,
        width: '50px'
    },
    {
        label: 'Deviations',
        property: 'Deviations'       
    },
    {
        label: 'Status',
        property: 'Status',      
        width: '50px'
    },
    {
        label: 'Accept on Deviation',
        property: 'Accept',
        width: '50px',

    },
    {
        label: 'Reject',
        property: 'Reject',
        width: '50px',
    }
];

function customColumnRenderer(helpers, callback) {
    // determine what column is being rendered
    var column = helpers.columnAttr;

    // get all the data for the entire row
    var rowData = helpers.rowData;
    var customMarkup = '';

    // only override the output for specific columns.
    // will default to output the text value of the row item
    switch (column) {
        case 'RecId':
            // let's combine name and description into a single column
           // customMarkup = '<div style="font-size:12px;">' + rowData.RecId + '</div>';
            customMarkup = ' <input type="checkbox" name="DeviationRecID" id="DeviationRecID' + rowData.RecId + '" class="DeviationRecID" value="' + rowData.RecId + '"/>';

            break;       
        case 'Accept':
            // let's combine name and description into a single column
            customMarkup = '<button onclick="GridAcceptClicked(' + rowData.RecId + ')" id="gridAccept" name="gridAccept" class="btn btn-success btn-sm center-block"><i class="fa fa-check-square-o"></i></button>';
            break;
        case 'Reject':
            // let's combine name and description into a single column
            customMarkup = '<button onclick="GridRejectClicked(' + rowData.RecId + ')" id="gridReject" name="gridReject" class="btn btn-danger btn-sm center-block"><i class="fa fa-trash"></i></button>';
            break;
        default:
            // otherwise, just use the existing text value
            customMarkup = helpers.item.text();
            break;
    }

    helpers.item.html(customMarkup);

    callback();
}

function customRowRenderer(helpers, callback) {
    // let's get the id and add it to the "tr" DOM element
    var item = helpers.item;
    item.attr('id', 'row' + helpers.rowData.RecId);

    callback();
}

var selectedRecords = "";
// this example uses an API to fetch its datasource.
// the API handles filtering, sorting, searching, etc.
function customDataSource(options, callback) {
    // set options   
    selectedRecords = "none";

    var pageIndex = options.pageIndex;
    var pageSize = options.pageSize;
    var search = '';
   
    if ($('#searchHeatNo').val())
        search += ';' + 'searchHeatNo:' + $('#searchHeatNo').val();
    if ($('#searchStatus').val())
        search += ';' + 'Status:' + $('#searchStatus').val();
   
    var options = {
      //  Screen: 'DeviationList',
        pageIndex: pageIndex,
        pageSize: pageSize,
        sortDirection: options.sortDirection,
        sortBy: options.sortProperty,
        filterBy: options.filter.value || '',
        searchBy: search || ''
    };

    $.ajax({
        type: 'post',
        //   url: GetRootDirectory() + '/Grid/GetDeviationList',
        url: '../Grid/GetDeviationList',
        data: options
    })
       
        .done(function (data) {          
           if (data) {
               var items = data.items;
               var totalItems = data.total;
               var totalPages = Math.ceil(totalItems / pageSize);
               var startIndex = (pageIndex * pageSize) + 1;
               var endIndex = (startIndex + pageSize) - 1;

               if (items) {
                   if (endIndex > items.length) {
                       endIndex = items.length;
                   }
               }
               // configure datasource
               var dataSource = {
                   page: pageIndex,
                   pages: totalPages,
                   count: totalItems,
                   start: startIndex,
                   end: endIndex,
                   columns: columns,
                   items: items
               };

               // invoke callback to render repeater
               callback(dataSource);
            
               $("input[class='DeviationRecID']").on('click', function () {

                   var recID = $(this).val();

                   if ($(this).prop('checked')) {
                       if (selectedRecords == "none")
                           //selectedRecords += recID;
                           selectedRecords = recID;
                       else
                           selectedRecords += "," + recID;
                   }
                   else {
                       var removeString = selectedRecords.replace(recID, "");
                       selectedRecords = removeString;
                   }
                   var removecomma = selectedRecords.replace(",,", ",");
                   selectedRecords = removecomma;
               });
           }
           else {
               alert("no data ");
           }
        });

}


// on button click funtions
$('#btnDeleteAll').on('click', function () {
    // DeleteRecords(selectedRecords);
    DeleteRecords();
});
$('#btnSelectDelete').on('click', function () {  
    // DeleteRecords(selectedRecords);
    DeleteRecords();
});

function GridAcceptClicked(id) {
    bootbox.confirm({
        message: VistaEDI.Constants.Accept,
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
                //status= '3' is accepted deviation
                var options = {
                    RecId: id,
                    status: 3
                };
                $.ajax({
                    type: 'post',
                    url: GetRootDirectory() + '/Parser/DeviationListUpdate',
                    data: options
                })
              .done(function (data) {
                  if (data.isSuccess) {
                      $('#lblmessage').text(data.message);
                  }
                  else {
                      $('#lblmessage').text(data.message);
                  }
                  $('#DeviationRepeater').repeater('render');
              });
            }
            else
                $('#lblmessage').text('Record is not inserted');
        }
    });
}

function GridRejectClicked(id) {
    bootbox.confirm({
        message: VistaEDI.Constants.Reject,
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
                //status= '0' is rejected deviation
                var options = {
                    RecId: id,
                    status: 0
                };
                $.ajax({
                    type: 'post',
                    url: GetRootDirectory() + '/Parser/DeviationListUpdate',
                    data: options
                })
              .done(function (data) {
                  if (data.isSuccess) {
                      $('#lblmessage').text(data.message);
                  }
                  else {
                      $('#lblmessage').text(data.message);
                  }
                  $('#DeviationRepeater').repeater('render');
              });
            }
            else
                $('#lblmessage').text('Record is not inserted');
        }
    });
}

//function DeleteRecords(selectedRecords) {
function DeleteRecords() {   
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
                //status= '0' is rejected deviation
                var options = {
                    selectedRecords: selectedRecords,
                };
              
                $.ajax({
                    type: 'post',
                    url: GetRootDirectory() + '/Parser/DeleteRecords',
                    data: options
                })
              .done(function (data) {
                 
                  if (data.isSuccess) { 
                      $('#lblmessage').text(data.message + ': ' +selectedRecords);
                  }
                  else {
                      $('#lblmessage').text(data.message + ': ' +selectedRecords);
                  }
                  $('#DeviationRepeater').repeater('render');
              });
            }
            else
                $('#lblmessage').text('Record not Deleted');
        }
    });
}

$('#btnSearch').on('click', function () {
    $('#DeviationRepeater').repeater('render');
    return false;
});

$('#btnClear').on('click', function () {
    //check if this should be drop down menu - currently keep text for search
    $('#searchHeatNo').val('');

    $('#searchStatus').val(''); 
    $('#DeviationRepeater').repeater('render');
    return false;
});

$(document).ready(function () {    
    $('#searchHeatNo').val('');
    $('#searchStatus').val('');
});