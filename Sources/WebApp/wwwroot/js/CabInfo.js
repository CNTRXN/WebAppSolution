const imageType = Object.freeze({ "page": 1, "form": 2 });
const allSkeleton = document.querySelectorAll('.skeleton');

var isEditMode = false;
const anim_time = 300;

$(window).on("load", () => {
    var cookie = getCookie();

    GetFormImages(parseInt(cookie.cabid), imageType.page);

    var buttonsContainer = document.getElementById("cab-select-info-buttons").childNodes;
    var buttons = [];

    var currentIndex = 1;

    for (var i = 0; i < buttonsContainer.length; i++) {
        var element = buttonsContainer[i];
        if (element.tagName == "BUTTON" && element.classList == "cab-select-info-button") {
            buttons.push(element);
            if (i > 0) {
                $(element).css("color", "#5555FF");
            }
        }
    }

    $(buttons[currentIndex - 1]).css("color", "white");

    showCabinetData('/show-equipments');

    /*
        
    
    */

    $.each(buttons, function () {
        $(this).on("click", function () {
            var newIndex = Array.from(buttons).indexOf(this) + 1;
            var offset = newIndex - currentIndex;
            var height = parseInt($("#cab-select-info-button-bg").css("height")) + 10;

            if (offset < 0) {
                $("#cab-select-info-button-bg").animate({
                    top: "-=" + (Math.abs(offset) * height)
                }, anim_time, () => {
                    //alert('!');
                    buttons.forEach(btn => {
                        $(btn).css("color", "#5555FF")
                    });

                    $(this).css("color", "white");
                });
            }
            else if (offset > 0) {
                $("#cab-select-info-button-bg").animate({
                    top: "+=" + (offset * height)
                }, anim_time, () => {
                    //alert('!');
                    buttons.forEach(btn => {
                        $(btn).css("color", "#5555FF")
                    });

                    $(this).css("color", "white");
                });
            }

            currentIndex = newIndex;
        });
    });

    //Редактирование информации
    /*$("#cab-info-edit").on("click", () => {
        $.ajax({
            url: "/show-cabinet-edit-form",
            type: 'GET',
            dataType: "html",
            headers: {
                "Access-Control-Allow-Origin": "true",
                "cabId": parseInt(cookie.cabid)
            },
            success: function (response) {
                $('body').append(response);

                registerEditInfoFormSector(parseInt(cookie.cabid));
            },
            error: function () {
                console.log('error load');
            }
        });
    });*/

    $("#to-equipments").on("click", (e) => {
        showCabinetData('/show-equipments');
    });

    $("#to-test-table").on("click", () => {
        showCabinetData('/show-test', "test");
    });

    //Показать заявки кабинета (от. прав доступа Мастер, Админ) 
    if (document.querySelector('#show-cabinet-requests'))
        $("#show-cabinet-requests").on("click", () => {
            showRequests();
        });

    function showCabinetData(_url, type = "equipment") {

        $.ajax({
            url: _url,
            type: 'GET',
            dataType: "html",
            headers: {
                "Access-Control-Allow-Origin": "true",
                "cabId": parseInt(cookie.cabid)
            },
            success: function (response) {
                $("#main-table").html(response);

                searchEquipment(type);
                tableButtons(type);
            },
            error: function () {
                console.log('error load');
            }
        });

        function searchEquipment(type) {
            if (document.querySelector('#search-data')) {
                var searchField = document.querySelector('#search-data');

                searchField.addEventListener("focus", () => {
                    searchField.addEventListener("keyup", enterDown);
                });

                searchField.addEventListener("blur", () => {
                    searchField.removeEventListener("keyup", enterDown);
                });

                function enterDown(evt) {
                    if (evt.code === 'Enter') {
                        $.ajax({
                            url: "/equip-search",
                            type: 'POST',
                            dataType: "html",
                            headers: {
                                "Access-Control-Allow-Origin": "true",
                                "searchField": searchField.value,
                                "cabId": parseInt(cookie.cabid),
                                "type": type
                            },
                            success: (resp) => {
                                $("#main-table").html(resp);

                                searchEquipment(type);
                            },
                            error: (err) => {
                                console.log('search error');
                            }
                        });
                    }
                }
            }
        }

        function tableButtons(type) {
            $("#attaching-button").on("click", () => {
                if (type == "equipment") {
                    $.ajax({
                        url: '/show-select-object',
                        type: 'GET',
                        dataType: "html",
                        headers: {
                            "Access-Control-Allow-Origin": "true",
                            "getType": "equipments"
                        },
                        success: function (response) {
                            $("body").append(response);
                            cssSelectedForm();
                            

                            //onSelectObject(containerOnChange, ContainerType.equipment, openSelectType.page);

                            onSelectObject();


                            $("#close-select-form").on("click", (e) => {
                                $("#other-form-container").remove();
                            });
                        },
                        error: function () {

                        }
                    });
                }
            });

            function onSelectObject() {
                var selectedObjects = [];
                $("#select-object").on("change", (e) => {
                    $("select option:selected").each(function () {
                        selectedObjects.push($(this).attr("value"));
                    });

                    if (selectedObjects.length > 0) {
                        $("#select-this-object").show();
                    }
                    else {
                        $("#select-this-object").hide();
                    }
                });

                $("#select-this-object").on("click", () => {
                    $.ajax({
                        url: '/attach-equipments-to-cabinet',
                        type: 'GET',
                        contentType: 'application/json',
                        headers: {
                            "Access-Control-Allow-Origin": "true",
                            "cabId": cookie.cabid !== 0 ? parseInt(cookie.cabid) : 0,
                        },
                        data: {
                            "r_equipmentsIds": JSON.stringify(selectedObjects)
                        },
                        success: (response) => {
                            window.location.reload();
                        },
                        error: (err) => {

                        }
                    });
                });
            }

            function cssSelectedForm() {
                $("#other-form-container").css("width", "100%");
                $("#other-form-container").css("height", "100%");
                $("#other-form-container").css("position", "fixed");
                $("#other-form-container").css("top", "0");
                $("#other-form-container-bg").addClass("black-screen");
                $("#other-form-container-bg").removeAttr("id");
            }
        }
    }

    function showRequests() {
        $.ajax({
            url: "/show-cabinet-requests",
            type: 'GET',
            dataType: "html",
            headers: {
                "Access-Control-Allow-Origin": "true",
                "cabId": parseInt(cookie.cabid)
            },
            success: function (response) {
                $("#main-table").html(response);

                registerCabinetRequestsSector();
            },
            error: function () {
                console.log('error load');
            }
        });
    }
});

let registerEditInfoFormSector = (cabId) => {
    let responsiblePerson = null;
    const editForm = document.querySelector('#edit-cab-info');

    $('#close-edit-cab-info-form').on("click", () => {
        editForm.remove();
    });

    if ($('#select-resp-person').length) {
        //отв. лицо есть в кабинете
        $('#change-resp-person').on("click", () => {
            $.ajax({
                url: "/show-cabinet-edit-form-resp-person",
                type: 'GET',
                dataType: "html",
                headers: {
                    "Access-Control-Allow-Origin": "true"
                }, success: function (response) {
                    $("#edit-cab-info-form-container").append(response);

                    $("#close-change-responsible-person-form").on("click", () => {
                        $("#change-responsible-person-form-container").remove();
                    });

                    
                },
                error: function () {

                }
            });
        });

        $('#delete-resp-person').on("click", () => {
            changeResponsiblePerson("non-selected");
        });
    } else {
        $('#change-resp-person').on("click", () => {
            $.ajax({
                url: "/show-cabinet-edit-form-resp-person",
                type: 'GET',
                dataType: "html",
                headers: {
                    "Access-Control-Allow-Origin": "true"
                }, success: function (response) {
                    $("#edit-cab-info-form-container").append(response);

                    $("#close-change-responsible-person-form").on("click", () => {
                        $("#change-responsible-person-form-container").remove();
                    });

                    changeResponsiblePerson("selected");

                    //
                },
                error: function () {

                }
            });
        });
    }

    //if()
    function changeResponsiblePerson(changeTo) {
        const selectedTempo = document.getElementById("select-resp-person-template").content.cloneNode(true);
        const nonSelectedTempo = document.getElementById("non-select-resp-person-template").content.cloneNode(true);

        //console.log('delete pers');
        switch (changeTo) {
            case "selected":
                $("#responsible-person-container").html(selectedTempo);
                break;
            case "non-selected":
                $("#responsible-person-container").html(nonSelectedTempo);
                break;
        }
    }

    function openSelectRespPersonForm() {
        //сделать  запрос для открытия формы выбора
    }
};

let registerCabinetRequestsSector = () => {
    var requestCategory = document.getElementsByClassName('request-categories-header');

    Array.from(requestCategory).forEach(elem => {
        const category = elem.parentElement;
        const categoryRequstContainer = category.querySelector('.cab-requests-container');

        $(elem).on("click", () => {
            let click = true;

            const isOpen = category.classList.contains('open');
            const sign = elem.querySelector('.request-categories-header-sign');

            if (click) {
                if (!isOpen) {
                    category.classList.add('open');
                    category.classList.remove('close');

                    $(categoryRequstContainer).animate({
                        height: "+=500px",
                    }, 900, () => {
                        click = false;
                    });

                    $(sign).rotate({
                        animateTo: 0,
                    });
                } else {
                    $(categoryRequstContainer).animate({
                        height: "0px",
                    }, 900, () => {
                        click = false;
                        category.classList.remove('open');
                        category.classList.add('close');
                    });

                    $(sign).rotate({
                        animateTo: 180
                    });
                }
            }
        });
    });
}
var GetFormImages = function(id, type) {
    const showFormButton = document.getElementById("show-image-form-button").content.cloneNode(true);

    const imagePageList = document.getElementById("cab-photos");
    const img = imagePageList.querySelectorAll('.file-container');

    switch (type) {
        case imageType.page:
            $.ajax({
                type: "GET",
                url: "http://localhost:5215/api/Cabinet/image/getImagesByCab=" + id,
                dataType: "json",
                headers: {
                    "Access-Control-Allow-Origin": "true"
                },
                success: function (data) {
                    var i = 0;

                    data.some((element, index, arr) => {
                        if (index > 3)
                            return true;

                        $(img[index]).css('background-image', 'url(' + element + ')');

                        let downloadButton = img[index].getElementsByTagName('button')[0];

                        $(downloadButton).on("click", () => {
                            console.log(index);
                            downloadFile(element, index);
                        });

                        i++;
                    });

                    for (; i < img.length; i++) {
                        imagePageList.removeChild(img[i]);
                    }

                    imagePageList.appendChild(showFormButton);

                    
                    initDownloadButton(img);

                    $("#open-image-form").on("click", function () {
                        var cookie = getCookie();
                        GetFormImages(parseInt(cookie.cabid), imageType.form);
                    });
                },
                error: (error) => {
                    img.forEach(item => {
                        imagePageList.removeChild(item);
                    });

                    imagePageList.appendChild(showFormButton);
                }
            });
            break;
        case imageType.form:
            $.ajax({
                type: "GET",
                url: "/open-image-form",
                dataType: "html",
                headers: {
                    "Access-Control-Allow-Origin": "true",
                    "cabId": id
                },
                success: (response) => {
                    $('body').append(response);
                    $("#image-list-close").on("click", function () {
                        $("#show-image-list").remove();
                    });

                    const imgContainer = document.getElementById("image-list");
                    const images = imgContainer.querySelectorAll('.file-container');

                    initDownloadButton(images);
                    //console.log(images);
                },
                error: (error) => {

                }
            });
            break;
    }
}

function getCookie() {
    return document.cookie.split('; ').reduce((acc, item) => {
        const [name, value] = item.split('=')
        acc[name] = value
        return acc
    }, {})
}

function downloadFile(url, filename) {
    let link = document.createElement('a');
    link.href = url;
    link.download = filename;

    document.body.appendChild(link);

    link.click();

    document.body.removeChild(link);
}

let initDownloadButton = (imgContainer) => {
    let isOver = false;

    Array.from(imgContainer).forEach(elem => {
        $(elem).hover(function () {
            if (!isOver) {
                const downloadButton = elem.querySelector('button');
                $(downloadButton).animate({
                    top: "-=25"
                }, 200);
            }

            isOver = true;

        }, function () {
            if (isOver) {
                const downloadButton = elem.querySelector('button');
                $(downloadButton).animate({
                    top: "+=25"
                }, 200);
            }

            isOver = false;
        }
        );
    });
};