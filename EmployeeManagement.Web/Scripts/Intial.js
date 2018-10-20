$(document).ready(function () {

    $('.dataTable').DataTable();
});

function WaitAlert(title) {
    swal({
        title: title,
        onOpen: () => {
            swal.showLoading();
        },
        animation: false,
        showCancelButton: false,
        showConfirmButton: false,
        allowOutsideClick: false,
        allowEscapeKey: false,
        allowEnterKey: false
    });
};
function SuccessAlert(title, message) {
    swal({
        title: title,
        text: message,
        confirmButtonClass: 'btn btn-success',
        type: "success"
    });
};

function AutoColseSuccessAlert(title, message) {
    swal({
        type: 'success',
        title: title,
        text: message,
        showConfirmButton: false,
        timer: 1500,
        showCancelButton: false,
        allowOutsideClick: false,
        allowEscapeKey: false,
        allowEnterKey: false
    });
};

function FailAlert(title, message) {
    swal({
        title: title,
        text: message,
        confirmButtonClass: 'btn btn-danger',
        confirmButtonText: "Close !",
        type: "error"
    });
};

