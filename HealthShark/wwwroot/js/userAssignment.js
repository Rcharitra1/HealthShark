
var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/UserAssignment/GetAll"
        },
        "columns": [
            {"data": "user.name", "width": "20%" },
            { "data": "trainer.name", "width": "20%" },
            { "data": "dietician.name", "width": "20%" },
            { "data": "userPlan.description", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/UserAssignment/GetOne/${data}" class="btn btn-danger text-white px-2" style="cursor:pointer">
                                    <i class="fas fa-info"></i>&nbsp;Details
                                 </a>
                            </div>
                           `;
                }, "width": "20%"
            }
        ]
    });
}





