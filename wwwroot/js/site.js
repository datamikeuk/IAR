// Import autocomplete from the lib folder and initialize it
// import Autocomplete from "/lib/bootstrap5-autocomplete/autocomplete.js";
// Autocomplete.init("input.autocomplete", {
//     highlightTyped: true,
//     onSelectItem: console.log,
//     server: "~/AssetList",
//     valueField: "id",
//     labelField: "name",
//     fullWidth: true,
//     onSelectItem: (data) => window.location.href = "Edit/" + data.id,
//     fixed: true,
//     maximumItems: 10,
// });

// Enable Bootstrap tooltips
var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
  return new bootstrap.Tooltip(tooltipTriggerEl)
})

// Disable the edit fields when the checkbox is checked
function toggleDisable(checkbox) {
    var toggle = document.getElementById("editFields");
    $(toggle).prop('disabled', $(checkbox).prop('checked'));
}

// Show the modal and get the form
$(function () {
    var placeholderElement = $('#modal-placeholder');

    $('button[data-toggle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
        });
    });

    placeholderElement.on('click', '[data-save="modal"]', function (event) {
        event.preventDefault();
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var dataToSend = form.serialize();
        var formId = form.attr('id');
        console.log(actionUrl);
        console.log(dataToSend);
        console.log(form.attr('id'));

        $.post(actionUrl, dataToSend).done(function (data) {
            var newBody = $('.modal-body', data);
            placeholderElement.find('.modal-body').replaceWith(newBody);
            // console.log(data);
            if (data.success) 
            {   
                // If the form is the asset form, reload the table
                if (formId == "assetModal") {
                // window.location.href = "/Edit/" + data.id;
                $('#asset-table').DataTable().ajax.reload();
                }

                // if the form is the third party form, reload the table
                if (formId == "thirdPartyModal") {
                    var tableElement = $('#third-party-table');

                    var tableUrl = tableElement.data('url');
                    $.get(tableUrl).done(function (table) {
                        tableElement.replaceWith(table);
                    });
                }
                placeholderElement.find('.modal').modal('hide');
            }
        });
    });
});