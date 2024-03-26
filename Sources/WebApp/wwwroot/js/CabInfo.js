const imageType = Object.freeze({ "page": 1, "form": 2 });
const allSkeleton = document.querySelectorAll('.skeleton');

var isEditMode = false;

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
    GetFormImages(parseInt($("#cab-info-container").data("id")), imageType.page);
});
function switchEditMode() {
    var parent = document.getElementById('cab-text-info');

    var cabInfo = new CabInfo();

    parent.childNodes.forEach(item => {
        var newElem;
        if (item.tagName == "P") {
            newElem = document.createElement('input');
            newElem.value = item.textContent;

            cabInfo.setPropertyValue(item.id, item.value);
            newElem.setAttribute('type', 'text');
        }
        else if (item.tagName == "INPUT") {
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

function openImageForm(id) {
    GetFormImages(id, imageType.form);
}

var GetFormImages = function(id, type) {
    const showFormButton = document.getElementById("show-image-form-button").content.cloneNode(true);
    const imagePageList = document.getElementById("cab-photos");
    const img = imagePageList.querySelectorAll('.skeleton');

    const imageForm = document.getElementById('show-image-list');
    const imageFormList = document.getElementById('image-list');

    $.ajax({
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
    });

    console.clear();
}

function closeImageForm() {
    const imageForm = document.getElementById('show-image-list');
    const imageList = document.getElementById('image-list');

    imageList.childNodes.forEach(child => {
        imageList.removeChild(child);
    });

    imageForm.style.visibility = "hidden";
}