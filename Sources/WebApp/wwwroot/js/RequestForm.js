﻿var objectType = Object.freeze({
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
        //симуляция страницы без инфорамции о кабинете
        deleteCookie("cabid");

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
        //симуляция страницы с кабинетом
        document.cookie = "cabid=1";

        $.ajax({
            url: '/show-request-from',
            type: 'GET',
            dataType: "html",
            headers: {
                "Access-Control-Allow-Origin": "true",
                "cabId": cookie.cabid !== 0 ? parseInt(cookie.cabid) : 0
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

var registerRequestFormEvents = (response) => {
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

    startContainer();
};

var startContainer = () => {
    const cabContaintainer = document.getElementById("cabinetcontainer");
    const equipContainer = document.getElementById("equipmentscontainer");
    //попытка получения выбранного элемента кабинета

    /*
    при загрузке после удаления куки - нет проверки, что их нет
    поэтому проверяется старая форма, а полсе изменения контента не изменяется внешний вид
    
    
    */
    

    if (isCabinetPage()) {
        console.log('is cabinet page');
    }
    else {
        console.log('isn`t cabinet page');
    }

    try {
        //cabContaintainer.querySelector(".delete")
        //Кабинет
        if (isCabinetPage()) {
            //есть объект

            setSelectedContainer(cabContaintainer, ContainerType.cabinet);
        } else {
            

            setEmptyContainer(cabContaintainer, ContainerType.cabinet);
        }

        let formHasEquipments = equipContainer.querySelector(".delete") != null;
        //console.log(formHasEquipments);


        //Оборудование
        if (isCabinetPage()) {
            if (formHasEquipments) {
                //есть объект
                setSelectedContainer(equipContainer, ContainerType.equipment);
            } else {
                /*const equipmentContainer = document.getElementById("equipmentscontainer");
    
                //need to fix!!!!!!!!!!!!!!!!!!
                deleteChild(equipmentContainer);*/

                setEmptyContainer(equipContainer, ContainerType.equipment);
            }
        } else {
            equipContainer.hidden = true;
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
            else {
                containerOnChange.appendChild(createdCabTemplate);
            }
            
            
            //Нажатие на добавление объекта
            addNewCabinet.addEventListener("click", (e) => {
                //setSelectedContainer(containerOnChange, ContainerType.cabinet);

                //$("#equipmentscontainer").show();

                //открытие формы выбора кабинета
                $.ajax({
                    url: '/show-select-object',
                    type: 'GET',
                    dataType: "html",
                    headers: {
                        "Access-Control-Allow-Origin": "true",
                        "cabId": isCabinetPage() ? parseInt(getCookie().cabid) : 0,
                        "getType": "cabinets"
                    },
                    success: function (response) {

                        console.log("cabinet select open");

                        $("#repair-request-container").append(response);

                        //событие при выборе кабинета
                        $("#select-object").on("change", (e) => {
                            console.log(e.target);
                        });
                        //setEmptyContainer(cabContaintainer, ContainerType.cabinet);

                        $("#close-select-form").on("click", (e) => {
                            $("#other-form-container").remove();
                        });
                    },
                    error: function () {
                        console.log("error");
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
    var cookie = getCookie();

    switch (type) {
        case ContainerType.cabinet:
            var createdCabTemplate = null;

            if (cookie.cabid == undefined)
                createdCabTemplate = createObject(objectType.selected, ContainerType.cabinet);
            else
                createdCabTemplate = document.getElementById("cabinetcontainer");

            var deleteCabinetButton = createdCabTemplate.querySelector(".delete");




            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            if (containerOnChange.querySelector('.add') || containerOnChange.querySelector('.delete')) {
                //containerOnChange.replaceChild(createdCabTemplate, containerOnChange.firstElementChild)
                if (isCabinetPage())
                    //если есть куки с номером кабинета, 
                    //то скрывается кнопку удаления кабинета
                    $(deleteCabinetButton).hide();
                    
                else {
                    //при добавлении кабинета
                    containerOnChange.replaceChild(createdCabTemplate, containerOnChange.firstElementChild);
                    let equipmentContainer = document.getElementById("equipmentscontainer");

                    $(equipmentContainer).show();
                    //setEmptyContainer(containerOnChange, ContainerType.cabinet);
                    setEmptyContainer(equipmentContainer, ContainerType.equipment);
                }
            }
            else {
                containerOnChange.appendChild(createdCabTemplate);
            }





            //Нажатие на удаление кабинета
            deleteCabinetButton.addEventListener("click", (event) => {
                setEmptyContainer(containerOnChange, ContainerType.cabinet);
                $("#equipmentscontainer").hide();
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
            }
            else
                containerOnChange.appendChild(createdEquipmentTemplate); 

            addNewEquipmentButton(containerOnChange);
            break;
    }
}
let addCabinetData = () => {

}

function addNewEquipmentButton(containerOnChange) {
    var cookie = getCookie();

    var createdEquipmentTemplate = createObject(objectType.empty, ContainerType.equipment);

    var addNewEquipment = createdEquipmentTemplate.querySelector(".add");
    addNewEquipment.textContent = "Добавить оборудование";

    if (containerOnChange.querySelector('.add')) {
        containerOnChange.replaceChild(createdEquipmentTemplate, containerOnChange.firstElementChild);
    }
    else {
        containerOnChange.appendChild(createdEquipmentTemplate);
    }

    //добавление нового оборудования
    addNewEquipment.addEventListener("click", (e) => {
        console.log('click!');
        $.ajax({
            url: '/show-select-object',
            type: 'GET',
            dataType: "html",
            headers: {
                "Access-Control-Allow-Origin": "true",
                "cabId": cookie.cabid !== 0 ? parseInt(cookie.cabid) : 0,
                "getType" : "equipments"
            },
            success: function (response) {
                $("#repair-request-container").append(response);

                //событие при выборе оборудование
                $("#select-object").on("change", (e) => {
                    $("select option:selected").each(function () {
                        console.log($(this).value);
                    });
                });
                //setEmptyContainer(cabContaintainer, ContainerType.cabinet);

                $("#close-select-form").on("click", (e) => {
                    $("#other-form-container").remove();
                });
            },
            error: function () {

            }
        });

        //var newEquipment = createObject(objectType.selected, ContainerType.equipment);
        //var deleteEquipmentButton = newEquipment.querySelector(".delete");

        ////удаление оборудования из списка
        //deleteEquipmentButton.addEventListener("click", (e) => {
        //    newEquipment.remove();
        //});

        //containerOnChange.insertBefore(newEquipment, containerOnChange.firstElementChild);
    });
}

function isCabinetPage() {
    let cookie = getCookie();

    let isCabinetPage = cookie.cabid != undefined;

    return isCabinetPage;
}

function createObject(obj_type, container_type) { 
    switch (container_type) {
        case ContainerType.cabinet:
            var cabinetObject = byType();
            cabinetObject.setAttribute("for", "cabinet");
            var cabinetInput = cabinetObject.querySelector('input');
            cabinetInput.setAttribute("id", "cabinet");
            cabinetInput.setAttribute("name", "cabinet");

            return cabinetObject;
        case ContainerType.equipment:
            var equipmentObject = byType();
            equipmentObject.setAttribute("for", "equipment");
            var equipmentInput = equipmentObject.querySelector('input');
            equipmentInput.setAttribute("id", "equipment");
            equipmentInput.setAttribute("name", "equipment");

            return equipmentObject;
    }

    function byType() {
        switch (obj_type) {
            case objectType.empty:
                var emptyObject = emptyObj();
                emptyObject.classList.add("empty");
                return emptyObject;
            case objectType.selected:
                var selectedObject = selectedObj();
                selectedObject.classList.add("selected");
                return selectedObject;
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

function deleteCookie(name) {
    document.cookie = name + '=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;';
}

function deleteChild(container) {
    let child = container.lastElementChild;
    while (child) {
        e.removeChild(child);
        child = e.lastElementChild;
    }
}