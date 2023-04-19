// Select2
$(document).ready(function() {
    $('.data-owner-select').select2({
        theme: "bootstrap-5",
        minimumInputLength: 2,
        allowClear: true,
        ajax: {
            url: "/UserList",
            dataType: 'json',
            delay: 250,
            processResults: function (data) {
                return {
                    results: $.map(data, function (item) {
                        return {
                            id: item.accountName,
                            text: item.displayName
                        }
                    })
                };
            },
            data: function (params) {
                var query = {
                  search: params.term,
                }
                // Query parameters will be ?search=[term]
                return query;
            }

        } 
    });
});

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