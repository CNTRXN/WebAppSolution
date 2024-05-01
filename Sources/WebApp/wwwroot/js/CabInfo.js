const imageType = Object.freeze({ "page": 1, "form": 2 });
const allSkeleton = document.querySelectorAll('.skeleton');

var isEditMode = false;
const anim_time = 300;

class CabInfo {
    id;
    num;
    planNum;
    responsiblePerson;
    group;
    floor;
    height;
    length;
    width;
    squareFloor = this.width * this.length;
    squareWall1 = this.length * this.height;
    squareWall2 = this.width * this.height;

    setPropertyValue(propName, value) {
        if (this.hasOwnProperty(propName)) {
            this[propName] = value;
        }
    }

    getPropertyValue(propName) {
        if (this.hasOwnProperty(propName)) {
            return this[propName];
        } else {
            return undefined;
        }
    }
}

$(window).on("load", () => {
    var cookie = getCookie();

    GetFormImages(parseInt(cookie.cabid), imageType.page);
    //#5555FF

    var buttonsContainer = document.getElementById("cab-select-info-buttons").childNodes;
    var buttons = [];

    var currentIndex = 1;

    for (var i = 0; i < buttonsContainer.length; i++) {
        var element = buttonsContainer[i];
        if (element.tagName == "BUTTON") {
            buttons.push(element);
            if (i > 0) {
                $(element).css("color", "#5555FF");
            }
        }
    }

    $(buttons[currentIndex - 1]).css("color", "white");

    showCabinetData('/show-equipments');

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

    $("#cab-info-edit").on("click", () => {
        switchEditMode();
    });

    $("#to-equipments").on("click", (e) => {
        showCabinetData('/show-equipments');
    });

    $("#to-test-table").on("click", () => {
        showCabinetData('/show-test');
    });

    /*$("#to-test-table").on("click", function () {
        $.ajax({
            url: '/show-test',
            type: 'GET',
            dataType: "html",
            headers: {
                "Access-Control-Allow-Origin": "true",
            },
            success: function (response) {
                // При успешном получении ответа, обновляем содержимое контейнера с partial view
                $("#main-table").html(response);
            },
            error: function () {
                console.log('error load');
            }
        });
    });*/

    function showCabinetData(_url) {
        $.ajax({
            url: _url,
            type: 'GET',
            dataType: "html",
            headers: {
                "Access-Control-Allow-Origin": "true",
                "cabId": parseInt(cookie.cabid)
            },
            success: function (response) {
                // При успешном получении ответа, обновляем содержимое контейнера с partial view
                $("#main-table").html(response);
            },
            error: function () {
                console.log('error load');
            }
        });
    }
});

function switchEditMode() {
    var parent = document.getElementById('cab-text-info');

    var cabInfo = new CabInfo();

    parent.childNodes.forEach(item => {
        var newElem;
        if (item.tagName == "P") {
            //редактирование
            newElem = document.createElement('input');
            newElem.value = item.textContent;

            cabInfo.setPropertyValue(item.id, item.value);
            newElem.setAttribute('type', 'text');
        }
        else if (item.tagName == "INPUT") {
            //сохранение
            newElem = document.createElement('p');

            newElem.textContent = item.value;
        }

        if (newElem != undefined) {
            newElem.setAttribute('id', item.id);

            parent.replaceChild(newElem, item);

            isEditMode = !isEditMode;
        }
    });
}

/*function openImageForm(id) {
    GetFormImages(id, imageType.form);
}*/

var GetFormImages = function(id, type) {
    const showFormButton = document.getElementById("show-image-form-button").content.cloneNode(true);
    const imagePageList = document.getElementById("cab-photos");
    const img = imagePageList.querySelectorAll('.skeleton');

    /*const imageForm = document.getElementById('show-image-list');
    const imageFormList = document.getElementById('image-list');*/

    /*$.ajax({
        type: "GET",
        url: "http://localhost:5215/api/File/images/cabId=" + id,
        dataType: "json",
        headers: {
            "Access-Control-Allow-Origin": "true"
        },
        success: function (data) {
            if (type == imageType.page) {  
                
                var i = 0;

                data.some((element, index) => {
                    if (index < 3)
                    {
                        img[index].src = element;
                        img[index].className = "";
                    }
                    else {
                        i = index;
                        return true;
                    }
                })

                for (; i < img.length; i++) {
                    imagePageList.removeChild(img[i]);
                }

                imagePageList.appendChild(showFormButton);
            }

            if (type == imageType.form) {
                var images = [];
                data.forEach(url => {
                    var img = new Image();
                    img.src = url;
                    images.push(img);
                });


                imageFormList.replaceChildren(...images);

                imageForm.style.visibility = "visible";
            }
        },
        error: function (xhr, status, error) {
            img.forEach(item => {
                imagePageList.removeChild(item);
            });

            if (type == imageType.page) {
                imagePageList.appendChild(showFormButton);
            }

            if (type == imageType.form) {
                imageForm.style.visibility = "visible"
            }

            console.error(error);
        }
    });*/

    switch (type) {
        case imageType.page:
            $.ajax({
                type: "GET",
                url: "http://localhost:5215/api/File/images/cabId=" + id,
                dataType: "json",
                headers: {
                    "Access-Control-Allow-Origin": "true"
                },
                success: function (data) {
                    var i = 0;

                    data.some((element, index) => {
                        if (index < 3) {
                            img[index].src = element;
                            img[index].className = "";
                        }
                        else {
                            i = index;
                            return true;
                        }
                    })

                    for (; i < img.length; i++) {
                        imagePageList.removeChild(img[i]);
                    }

                    imagePageList.appendChild(showFormButton);

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
                },
                error: (error) => {

                }
            });
            break;
    }

    console.clear();
}

function getCookie() {
    return document.cookie.split('; ').reduce((acc, item) => {
        const [name, value] = item.split('=')
        acc[name] = value
        return acc
    }, {})
}