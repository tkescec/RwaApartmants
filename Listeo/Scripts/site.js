
// Captcha Code
let code;

const loadContent = (controller, divId, page, items, showMoreId, showLoadMore, rooms = 0, adults = 0, children = 0, city = "", sort = 0) => {

    const cookieData = {
        rooms: rooms,
        adults: adults,
        children: children,
        city: city,
        sort: sort
    };

    setCookie(window.btoa(unescape(encodeURIComponent(JSON.stringify(cookieData)))), 365);

    $.ajax({
        url: `/${controller}/IndexPartial`,
        data: {
            "page": page,
            "items": items,
            "rooms": rooms,
            "adults": adults,
            "children": children,
            "city": city,
            "sort": sort
        },
        cache: false,
        type: "GET",
        beforeSend: function () {
            $(`#${showMoreId}`).hide();
            $(`#${divId}`).append('<div class="loading"></div>');
        },
        success: function (response) {
            $(`#${divId}`).append(response);
            if (showLoadMore != 'True' || response.length == 0) {
                $(`#${showMoreId}`).hide();
            } else {
                $(`#${showMoreId}`).show();
            }
        },
        error: function (xhr) {

        },
        complete: function () {
            $('.loading').remove();
        }
    });
}
const getContet = (controller, divId, page, items, showMoreId, showLoadMore, filterRoomsId, filterAdultsId, filterChildrenId, filterCityId, sortById) => {

    const rooms = $(`#${filterRoomsId}`).val() || 0;
    const adults = $(`#${filterAdultsId}`).val() || 0;
    const children = $(`#${filterChildrenId}`).val() || 0;
    const city = $(`#${filterCityId}`).val() || "";
    const sort = $(`#${sortById}`).val() || 0;

    loadContent(controller, divId, page, items, showMoreId, showLoadMore, rooms, adults, children, city, sort);
}
const filterContent = ({ controller, divId, page, items, showMoreId, showLoadMore, filterBtnId, filterRoomsId, filterAdultsId, filterChildrenId, filterCityId, sortById }) => {

    getContet(controller, divId, page, items, showMoreId, showLoadMore, filterRoomsId, filterAdultsId, filterChildrenId, filterCityId, sortById);


    $(`#${filterBtnId}`).off('click');
    $(`#${filterBtnId}`).on('click', (e) => {
        e.preventDefault();

        $(`#${divId}`).empty();
        getContet(controller, divId, page, items, showMoreId, showLoadMore, filterRoomsId, filterAdultsId, filterChildrenId, filterCityId, sortById);
    });

    $(`#${sortById}`).off('change');
    $(`#${sortById}`).on('change', (e) => {
        e.preventDefault();

        $(`#${divId}`).empty();
        getContet(controller, divId, page, items, showMoreId, showLoadMore, filterRoomsId, filterAdultsId, filterChildrenId, filterCityId, sortById);
    });
}
const setCookie = (data, exdays) => {
    const date = new Date();
    date.setTime(date.getTime() + (exdays * 24 * 60 * 60 * 1000));

    const expires = `expires=${date.toUTCString()}`;
    const listeoCookie = `listeo=${data};${expires};path=/`;

    document.cookie = listeoCookie;
}


const handleCaptcha = (options) => {

    // clear the contents of captcha div first 
    $("#captcha").empty();

    const charsArray = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ@!#$%^&*";
    const lengthOtp = 6;
    const captcha = [];

    for (var i = 0; i < lengthOtp; i++) {
        //below code will not allow Repetition of Characters
        const index = Math.floor(Math.random() * charsArray.length + 1); //get the next character from the array
        if (captcha.indexOf(charsArray[index]) == -1)
            captcha.push(charsArray[index]);
        else i--;
    }

    const canv = document.createElement("canvas");
    canv.id = "captcha";
    canv.width = 100;
    canv.height = 50;
    const ctx = canv.getContext("2d");
    ctx.font = "25px Georgia";
    ctx.strokeText(captcha.join(""), 0, 30);
    //storing captcha so that can validate you can save it somewhere else according to your specific requirements
    code = captcha.join("");
    $("#captcha").append(canv); // adds the canvas to the body element
};
const validateCaptcha = (options) => {
    const value = $("#captchaTextBox").val();

    if (value != code) {
        alert("Invalid Captcha. Try Again");
        handleCaptcha();

        return false;
    }

    return true;
};
const handleReservation = (options) => {

    $("#reservationForm").on("submit", (e) => {
        e.preventDefault();

        const formData = {
            ApartmentId: $("#ApartmentId").val() != "" ? $("#ApartmentId").val() : null,
            UserId: $("#UserId").val() != "" ? $("#UserId").val() : null,
            UserName: $("#UserName").val() != "" ? $("#UserName").val() : null,
            UserEmail: $("#UserEmail").val() != "" ? $("#UserEmail").val() : null,
            UserPhone: $("#UserPhone").val() != "" ? $("#UserPhone").val() : null,
            UserAddress: $("#UserAddress").val() != "" ? $("#UserAddress").val() : null,
            Details: $("#Details").val() != "" ? $("#Details").val() : null
        }

        if (!formData.UserId && !validateCaptcha()) {
            return;
        }

        sendData("Apartments", "Reservation", formData, "#reservationForm", "button#book");
    })
};
const handleReview = (options) => {

    $("#reviewForm").on("submit", (e) => {
        e.preventDefault();

        $("#ratingError").hide();
        if ($("#ReviewStars input:checked").val() == undefined) {
            $("#ratingError").show();
            return;
        }

        const formData = {
            ApartmentId: $("#ApartmentId").val() != "" ? $("#ApartmentId").val() : null,
            UserId: $("#UserId").val() != "" ? $("#UserId").val() : null,
            Details: $("#ReviewDetails").val() != "" ? $("#ReviewDetails").val() : null,
            Stars: $("#ReviewStars input:checked").val() != undefined ? $("#ReviewStars input:checked").val() : null
        }

        if (!formData.UserId && !formData.ApartmentId) {
            return;
        }

        sendData("Apartments", "Review", formData, "#reviewForm", "button#SubmitReview");
    })
};
const sendData = (controller, action, data, form, button) => {
    $.ajax({
        url: `/${controller}/${action}`,
        data: data,
        cache: false,
        type: "POST",
        beforeSend: function () {
            $(button).prop("disabled", true);
        },
        success: function (response) {
            if (response.success) {
                $(form).prepend(
                    `<div class="notification success">
                        <p><h4>Success!</h4> ${response.message}</p>
                    </div>`
                );
            }
            else {
                $(form).prepend(
                    `<div class="notification error">
                        <p><h4>Error!</h4> ${response.message}</p>
                    </div>`
                );
            }
        },
        error: function (xhr) {

        },
        complete: function () {
            setTimeout(() => {
                $(form)[0].reset();
                $(button).prop("disabled", false);
            }, 1000);
            setTimeout(() => {
                $(".notification").hide();
            }, 5000);
        }
    });
}


const App = function() {
    "use strict";

    return {
        init: function (option){
            this.initCaptcha(option);
            this.initReservation(option);
            this.initReview(option);
        },
        initCaptcha: function(option){
            handleCaptcha(option);
        },
        initReservation: function(option){
            handleReservation(option);
        },
        initReview: function(option){
            handleReview(option);
        }
    };
}();

