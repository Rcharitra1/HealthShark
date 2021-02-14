
var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/EmployeeAssignment/GetAll"
        },
        "columns": [
            { "data": "user.name" },
            { "data": "userPlan.name" },
            { "data": "dietician.name" },
            { "data": "deit.description", "defaultContent":""},
           
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/EmployeeAssignment/Update/${data}" class="btn btn-primary text-white px-2" style="cursor:pointer">
                                    <i class="fas fa-pen></i>&nbsp; Update
                                 </a>
                            </div>
                           `;
                }
            }
        ],
    });
}





