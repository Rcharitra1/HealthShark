
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
            { "data": "user.name", "width": "20%" },
            { "data": "userPlan.name", "width": "20%" },
            { "data": "dietician.name", "width": "20%" },
            { "data": "diet.description", "width": "20%" },
    
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/EmployeeAssignment/Update/${data}" class="btn btn-danger text-white px-2" style="cursor:pointer">
                                    <i class="fas fa-info"></i>&nbsp;Details
                                 </a>
                            </div>
                           `;
                }, "width": "20%"
            }
        ]
    });
}





