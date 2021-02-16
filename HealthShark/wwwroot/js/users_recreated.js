
var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/User/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "email", "width": "15%" },
            { "data": "role", "width": "15%" },
            {
                "data": { id:"id",emailConfirmed: "emailConfirmed" }, 
                "render": function (data) {
                    if (data.emailConfirmed === true)
                    {
                        return `
                            <div class="text-center">
                               
                            </div>
                           `;
                    }
                    else
                    {
                        return `
                            <div class="text-center">

                                <a onclick=ConfirmUser("${data.id}") class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-lock-open"></i>&nbsp; Confirm Account 
                                </a>
                           `;
                    }
                    
                }, "width": "30%"
            }
        ]
    });
}



//function Delete(url) {
//    swal({
//        title: "Are you sure you want to Delete?",
//        text: "You will not be able to restore the data!",
//        icon: "warning",
//        buttons: true,
//        dangerMode: true
//    }).then((willDelete) => {
//        if (willDelete) {
//            $.ajax({
//                type: "DELETE",
//                url: url,
//                success: function (data) {
//                    if (data.success) {
//                        toastr.success(data.message);
//                        dataTable.ajax.reload();
//                    }
//                    else {
//                        toastr.error(data.message);
//                    }
//                }
//            });
//        }
//    });
//}


function ConfirmUser(id) {

    $.ajax({
        type: "POST",
        url: '/Admin/User/ConfirmUser',
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTable.ajax.reload();
            }
            else {
                toastr.error(data.message);
            }
        }
    });

}

