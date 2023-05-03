// Enable Bootstrap tooltips
// const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
// const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => 
//     new bootstrap.Tooltip(tooltipTriggerEl, {}))

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
        // console.log(actionUrl);
        // console.log(dataToSend);
        console.log(formId);

        $.post(actionUrl, dataToSend).done(function (data) {
            var newBody = $('.modal-body', data);
            placeholderElement.find('.modal-body').replaceWith(newBody);
            // console.log(data);
            if (data.success) 
            {   
                console.log("success");
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