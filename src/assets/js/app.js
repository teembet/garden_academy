$(document).on('ready', function () {
    // INITIALIZATION OF HEADER
    // =======================================================
    var header = new HSHeader($('#header')).init();


    // INITIALIZATION OF MEGA MENU
    // =======================================================
    var megaMenu = new HSMegaMenu($('.js-mega-menu'), {
        desktop: {
            position: 'left'
        }
    }).init();


    // INITIALIZATION OF UNFOLD
    // =======================================================
    var unfold = new HSUnfold('.js-hs-unfold-invoker').init();


    // INITIALIZATION OF TEXT ANIMATION (TYPING)
    // =======================================================
    var typed = $.HSCore.components.HSTyped.init(".js-text-animation");


    // INITIALIZATION OF AOS
    // =======================================================
    AOS.init({
        duration: 650,
        once: true
    });


    // INITIALIZATION OF FORM VALIDATION
    // =======================================================
    $('.js-validate').each(function () {
        $.HSCore.components.HSValidation.init($(this), {
            rules: {
                confirmPassword: {
                    equalTo: '#signupPassword'
                }
            }
        });
    });


    // INITIALIZATION OF SHOW ANIMATIONS
    // =======================================================
    $('.js-animation-link').each(function () {
        var showAnimation = new HSShowAnimation($(this)).init();
    });


    // INITIALIZATION OF COUNTER
    // =======================================================
    $('.js-counter').each(function () {
        var counter = new HSCounter($(this)).init();
    });


    // INITIALIZATION OF STICKY BLOCK
    // =======================================================
    var cbpStickyFilter = new HSStickyBlock($('#cbpStickyFilter'));


    // INITIALIZATION OF CUBEPORTFOLIO
    // =======================================================
    $('.cbp').each(function () {
        var cbp = $.HSCore.components.HSCubeportfolio.init($(this), {
            layoutMode: 'grid',
            filters: '#filterControls',
            displayTypeSpeed: 0
        });
    });

    $('.cbp').on('initComplete.cbp', function () {
        // update sticky block
        cbpStickyFilter.update();
    });

    $('.cbp').on('filterComplete.cbp', function () {
        // update sticky block
        cbpStickyFilter.update();
    });

    $('.cbp').on('pluginResize.cbp', function () {
        // update sticky block
        cbpStickyFilter.update();
    });

    // animated scroll to cbp container
    $('#cbpStickyFilter').on('click', '.cbp-filter-item', function (e) {
        $('html, body').stop().animate({
            scrollTop: $('#demoExamplesSection').offset().top
        }, 200);
    });



    $('.js-go-to').each(function () {
        var goTo = new HSGoTo($(this)).init();
    });
});