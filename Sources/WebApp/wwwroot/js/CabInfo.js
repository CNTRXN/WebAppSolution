//import { downloadFile } from "./js/Files";

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

    //�������������� ����������
    $("#cab-info-edit").on("click", () => {
        $.ajax({
            url: "/show-cabinet-edit-form",
            type: 'GET',
            dataType: "html",
            headers: {
                "Access-Control-Allow-Origin": "true",
                "cabId": parseInt(cookie.cabid)
            },
            success: function (response) {
                // ��� �������� ��������� ������, ��������� ���������� ���������� � partial view
                /*$(document.body).insertAfter(response);*/
                $('body').append(response);

                registerEditInfoFormSector(parseInt(cookie.cabid));
            },
            error: function () {
                console.log('error load');
            }
        });
    });

    $("#to-equipments").on("click", (e) => {
        showCabinetData('/show-equipments');
    });

    $("#to-test-table").on("click", () => {
        showCabinetData('/show-test');
    });

    //�������� ������ �������� (��. ���� ������� ������, �����) 
    if (document.querySelector('#show-cabinet-requests'))
        $("#show-cabinet-requests").on("click", () => {
            showRequests();
        });

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
                // ��� �������� ��������� ������, ��������� ���������� ���������� � partial view
                $("#main-table").html(response);
            },
            error: function () {
                console.log('error load');
            }
        });
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
                // ��� �������� ��������� ������, ��������� ���������� ���������� � partial view
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
    const editContainer = document.querySelector('#edit-cab-info-form-container');

    if ($('#select-resp-person').length) {


        console.log(cabId);
    } else {
        console.log('!');
    }

    //if()
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
    const imageTemplate = document.getElementById("file-template");

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
                        if (index < 3) {
                            $(img[index]).css('background-image', 'url(' + element + ')');

                            let downloadButton = img[index].querySelector('button');

                            $(downloadButton).on("click", () => {
                                console.log(index);
                                downloadFile(element, index);
                            });
                        }
                        else {
                            i = index;
                            return true;
                        }
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

    console.clear();
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