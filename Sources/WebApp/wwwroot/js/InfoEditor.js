const infoType = Object.freeze({
    "cabinet": 1,
    "equipment": 2,
    "user": 3,
    "permission": 4,
    "requestType": 5,
    "requestStatus": 6
});

const sendType = Object.freeze({
    "new": 1,
    "edit" : 2
});

function openEditor(info_type, send_type, item_id = null) {
    $.ajax({
        url: "/show-editor-form",
        type: 'GET',
        dataType: "html",
        headers: {
            "Access-Control-Allow-Origin": "true",
            "infoTypeCode": info_type,
            "itemId": item_id,
            "sendType": send_type
        },
        success: function (response) {
            // При успешном получении ответа, обновляем содержимое контейнера с partial view
            /*$(document.body).insertAfter(response);*/
            $('body').append(response);

            registerEditForm();
        },
        error: function () {
            console.log('error load');
        }
    });
}

let registerEditForm = () => {
    var multiplieContainer = Array.from(document.querySelectorAll('.add-edit-select-container'));

    multiplieContainer.forEach(elem => {
        var selectContainer = elem.querySelector('select');
        //TODO: исправить
        $(selectContainer).on("change", () => {
            var selectedItem = multiplieContainer.querySelector('.selected-item');

            selectedItem.val(selectContainer.val());
        });
    });

    var addEditContainer = document.getElementById('add-edit-container');

    var closeButton = document.getElementById('close-add-edit-form');

    $(closeButton).on("click", () => {
        $(addEditContainer).remove();
    });
}