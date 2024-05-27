const infoType = Object.freeze({
    "equipment": 2,
    "user": 3,
    "permission": 4,
    "requestType": 5,
    "requestStatus": 6
});

function openEditor(info_type, item_id = null) {
    $.ajax({
        url: "/show-add-edit-form",
        type: 'GET',
        dataType: "html",
        headers: {
            "Access-Control-Allow-Origin": "true",
            "infoTypeCode": info_type,
            "itemId": item_id
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
}