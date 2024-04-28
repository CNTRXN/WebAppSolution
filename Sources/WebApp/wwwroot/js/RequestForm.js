var objectType = Object.freeze({
    empty:0,
    selected:1
});

var ContainerType = Object.freeze({
    cabinet: 0,
    equipment: 1
});

const oncabinetchange = new Event("cabinetchange");
const onequipmentchange = new Event("equipmentchange");

window.onload = () => {
    var cookie = getCookie();

    //test button
    const showRequestFormWithoutData = document.getElementById("show-request-form-without-data");
    const showRequestFormWithCabId = document.getElementById("show-request-form-with-cabid");

    //test
    showRequestFormWithoutData.addEventListener("click", (e) => {
        $.ajax({
            url: '/show-request-from',
            type: 'GET',
            dataType: "html",
            headers: {
                "Access-Control-Allow-Origin": "true",
            },
            success: function (response) { 
                $(document.body).append(response);

                registerRequestFormEvents();
            },
            error: function () {
                console.log('error load request form');
            }
        });
    });

    showRequestFormWithCabId.addEventListener("click", (e) => {
        $.ajax({
            url: '/show-request-from',
            type: 'GET',
            dataType: "html",
            headers: {
                "Access-Control-Allow-Origin": "true",
                "cabId": cookie.cabid !== null ? parseInt(cookie.cabid) : 0
            },
            success: function (response) {
                $(document.body).append(response);

                registerRequestFormEvents();
            },
            error: function () {
                console.log('error load request form');
            }
        });
    });

    /*var deleteCabinet = document.getElementById("deleteCab");
    var selectedObject = document.getElementById("selected-object-temp");

    deleteCabinet.addEventListener("click", () => {
        var deleteButton = selectedObject.cloneNode();
        deleteButton.id = "delteCabinet"
    });*/
};

var registerRequestFormEvents = () => {
    const requestForm = document.getElementById("form-container");

    const dropContainer = document.getElementById('dropcontainer');
    const fileInput = document.getElementById("images");

    dropContainer.addEventListener('dragover', function (e) {
        e.preventDefault();
        dropContainer.classList.add('dragover');
    });

    dropContainer.addEventListener('drop', function (e) {
        e.preventDefault();
        dropContainer.classList.remove('dragover');

        var files = e.dataTransfer.files;
        fileInput.files = deleteNotImage(files);
    });

    var perviousFiles = [];

    fileInput.addEventListener('change', function (e) {
        var newFiles = e.target.files;

        if (newFiles.length == 0) {
            fileInput.files = perviousFiles;
        } else {
            fileInput.files = newFiles;
            perviousFiles = newFiles;
        }
    });

    var closeFormButton = document.getElementById("close-request-form");

    closeFormButton.addEventListener("click", (e) => {
        $("#repair-request-container").remove();
    });

    startContainer(requestForm);
};

var startContainer = (requestForm) => {
    const cabContaintainer = document.getElementById("cabinetcontainer");
    const equipContainer = document.getElementById("equipmentscontainer");
    //попытка получения выбранного элемента кабинета
    try {
        //Кабинет
        if (cabContaintainer.querySelector(".delete")) {
            //есть объект
            setSelectedContainer(cabContaintainer, ContainerType.cabinet);
        } else {
            setEmptyContainer(cabContaintainer, ContainerType.cabinet);
        }

        //Оборудование
        if (equipContainer.querySelector(".delete")) {
            //есть объект
            setSelectedContainer(equipContainer, ContainerType.equipment);
        } else {
            setEmptyContainer(equipContainer, ContainerType.equipment);
        }
    } catch (err) {

    }
}

var setEmptyContainer = (containerOnChange, type) => {
    switch (type) {
        case ContainerType.cabinet:
            var createdCabTemplate = createObject(objectType.empty, ContainerType.cabinet);

            var addNewCabinet = createdCabTemplate.querySelector(".add");
            addNewCabinet.textContent = "Добавить кабинет";

            if (containerOnChange.querySelector('.delete') || containerOnChange.querySelector('.add'))
                containerOnChange.replaceChild(createdCabTemplate, containerOnChange.firstElementChild);
            else
                containerOnChange.appendChild(createdCabTemplate);

            //Нажатие на добавление объекта
            addNewCabinet.addEventListener("click", (e) => {
                //setSelectedContainer(containerOnChange, ContainerType.cabinet);
                //открытие формы выбора кабинета
                var cookie = getCookie();

                $.ajax({
                    url: '/show-select-object',
                    type: 'GET',
                    dataType: "html",
                    headers: {
                        "Access-Control-Allow-Origin": "true",
                        "cabId": parseInt(cookie.cabid)
                    },
                    success: function (response) {
                        $("#repair-request-container").append(response);

                        $("#close-select-form").on("click", (e) => {
                            $("#other-form-container").remove();
                        });
                    },
                    error: function () {

                    }
                });
            });
            break;
        case ContainerType.equipment:
            addNewEquipmentButton(containerOnChange);
            break;
    }
}

var setSelectedContainer = (containerOnChange, type) => {
    switch (type) {
        case ContainerType.cabinet:
            var createdCabTemplate = createObject(objectType.selected, ContainerType.cabinet);

            var deleteCabinetButton = createdCabTemplate.querySelector(".delete");

            if (containerOnChange.querySelector('.add') || containerOnChange.querySelector('.delete'))
                containerOnChange.replaceChild(createdCabTemplate, containerOnChange.firstElementChild);
            else
                containerOnChange.appendChild(createdCabTemplate);

            //Нажатие на удаление кабинета
            deleteCabinetButton.addEventListener("click", (event) => {
                setEmptyContainer(containerOnChange, ContainerType.cabinet);
            });
            break;
        case ContainerType.equipment:
            var createdEquipmentTemplate = createObject(objectType.selected, ContainerType.equipment);
            
            if (containerOnChange.querySelector(".delete")) {
                var deleteEquipment = containerOnChange.querySelector(".delete");

                //удаление оборудования из списка
                deleteEquipment.addEventListener("click", () => {
                    deleteEquipment.parentElement.remove();
                });

                addNewEquipmentButton(containerOnChange);
            }
            else
                containerOnChange.appendChild(createdEquipmentTemplate);     
            break;
    }
}

function addNewEquipmentButton(containerOnChange) {
    var createdEquipmentTemplate = createObject(objectType.empty, ContainerType.equipment);

    var addNewEquipment = createdEquipmentTemplate.querySelector(".add");
    addNewEquipment.textContent = "Добавить оборудование";

    if (containerOnChange.querySelector('.add'))
        containerOnChange.replaceChild(createdEquipmentTemplate, containerOnChange.firstElementChild);
    else
        containerOnChange.appendChild(createdEquipmentTemplate);

    //добавление нового оборудования
    addNewEquipment.addEventListener("click", (e) => {
        var newEquipment = createObject(objectType.selected, ContainerType.equipment);
        var deleteEquipmentButton = newEquipment.querySelector(".delete");

        //удаление оборудования из списка
        deleteEquipmentButton.addEventListener("click", (e) => {
            newEquipment.remove();
        });

        containerOnChange.insertBefore(newEquipment, containerOnChange.firstElementChild);
    });
}

function createObject(obj_type, container_type) { 
    switch (container_type) {
        case ContainerType.cabinet:
            var cabinetObject = byType();
            cabinetObject.classList.add("selected-cabinet");
            return cabinetObject;
            break;
        case ContainerType.equipment:
            var equipmentObject = byType();
            equipmentObject.classList.add("selected-equipment");
            return equipmentObject;
            break;
    }

    function byType() {
        switch (obj_type) {
            case objectType.empty:
                var emptyObject = emptyObj();
                return emptyObject;
                break;
            case objectType.selected:
                var selectedObject = selectedObj();
                return selectedObject;
                break;
        }
    }

    function selectedObj() {
        const selectedObject = document.getElementById("selected-object-temp");
        const cloneSelectedObjectTemplate = selectedObject.content.cloneNode(true).firstElementChild;

        return cloneSelectedObjectTemplate;
    }

    function emptyObj() {
        const emptySelectedObject = document.getElementById("non-selected-object-temp");
        const emptyObjectTemplate = emptySelectedObject.content.cloneNode(true).firstElementChild;

        return emptyObjectTemplate;
    }
};

function checkImages(files) {
    var isImageFile = false;

    for (var i = 0; i < files.length; i++) {
        var file = files[i];
        if (file.type.startsWith('image/')) {
            //console.log('Файл ' + file.name + ' является изображением.');
            // Здесь можно выполнить дополнительные действия с изображением, например, загрузить его
            if (!isImageFile)
                isImageFile = true;
        } else {
            //console.log('Файл ' + file.name + ' не является изображением и будет проигнорирован.');
            isImageFile = false;
        }
    }

    return isImageFile;
}

function deleteNotImage(files) {
    var checkedImages = [];

    for (var i = 0; i < files.length; i++) {
        var file = files[i];
        if (file.type.startsWith('image/')) {
            checkedImages.push(file);
        }
    }

    var images = new DataTransfer();
    for (var j = 0; j < checkedImages.length; j++) {
        images.items.add(checkedImages[j]);
    }

    return images.files;
}

function getCookie() {
    return document.cookie.split('; ').reduce((acc, item) => {
        const [name, value] = item.split('=')
        acc[name] = value
        return acc
    }, {})
}