$(window).on("load", () => {
    var rightMenu = document.getElementById("rightMenu");

    var open = false;

    var openMenuTime = 400;
    var closeMenuTime = 400;

    var hiddenElemnts = document.getElementsByClassName("hidden-elem");

    $("#closeMenuButton").click(() => {
        if (open) {
            closeRightMenu();
        }
        else {
            openRightMenu();
        }
    });

    if (document.querySelector('#myRequests'))
        $('#myRequests').on("click", () => {
            alert('!mr');
        });

    if (document.querySelector('#newRequests'))
        $('#newRequests').on("click", () => {
            alert('!nr');
        });

    function openRightMenu() {
        if ($("#" + rightMenu.id).width() == 0) {
            $("#" + rightMenu.id).animate({
                width: "+=250"
            }, openMenuTime, () => {
                
            });

            Array.from(hiddenElemnts).forEach(element => {
                element.style.visibility = 'visible';
            });

            $("#closeMenuButton").text("X");

            open = true;
        }
    }

    function closeRightMenu() {
        if ($("#" + rightMenu.id).width() == 250) {
            $("#" + rightMenu.id).animate({
                width: "-=250"
            }, closeMenuTime, () => {
                
            });

            Array.from(hiddenElemnts).forEach(element => {
                element.style.visibility = 'hidden';
            });

            $("#closeMenuButton").text("≡");

            open = false;
        }
    }
});