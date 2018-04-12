

$(document).ready(function () {
    $(".faq-body").hide();
    $(".faq-header").click(function () {
        $(".faq-body").hide(1000);
        $(this).next(".faq-body").toggle(1000);
    });
    

});