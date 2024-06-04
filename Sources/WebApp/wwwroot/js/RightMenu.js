$(window).on("load", () => {
    const cookie = getCookie();
    const apiUrl = "http://localhost:5215";//cookie.apiUrl;

    //console.log(apiUrl);

    //TODO: fix that

    var userId = null;
    if (document.querySelector('#userId'))
        userId = $('#userId').val();

    

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

    if (userId != null) {
        const hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(apiUrl + "/notification?user=" + userId, {
                skipNegotiation: true,
                transport: signalR.HttpTransportType.WebSockets
            })
            .withAutomaticReconnect()
            .build();

        /*hubConnection.on("", () => {

        });*/

        hubConnection.start();

        $("#rmExitProfile").on("click", () => {
            console.log("logout");
            hubConnection.stop();

            window.location = '../logout';
        });
    }

    

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


function getCookie() {
    return document.cookie.split('; ').reduce((acc, item) => {
        const [name, value] = item.split('=')
        acc[name] = value
        return acc
    }, {})
}