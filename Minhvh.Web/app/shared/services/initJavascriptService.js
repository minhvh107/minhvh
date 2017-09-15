(function (app) {
    app.factory('initJavascriptService', initJavascriptService);
    function initJavascriptService() {
        function init() {
            var CURRENT_URL = window.location.href.split('?')[0],
                $BODY = $('body'),
                $MENU_TOGGLE = $('#menu_toggle'),
                $SIDEBAR_MENU = $('#sidebar-menu'),
                $SIDEBAR_FOOTER = $('.sidebar-footer'),
                $LEFT_COL = $('.left_col'),
                $RIGHT_COL = $('.right_col'),
                $NAV_MENU = $('.nav_menu'),
                $FOOTER = $('.fixed-table-pagination'),
                $X_CONTENT = $(".x_content");

            function setContentHeight() {
                // reset height
                $RIGHT_COL.css('min-height', $(window).height());
                var bodyHeight = $BODY.outerHeight(),
                    footerHeight = $BODY.hasClass('footer_fixed') ? 0 : $FOOTER.height(),
                    leftColHeight = $LEFT_COL.eq(1).height() + $SIDEBAR_FOOTER.height(),
                    contentHeight = bodyHeight < leftColHeight ? leftColHeight : bodyHeight;
                // normalize content
                // contentHeight -= $NAV_MENU.height() + footerHeight;
                var nav_menuHeight = $NAV_MENU.outerHeight(),
                    page_titleHeight = $(".page-title").outerHeight(),
                    x_titleHeight = $(".x_title").outerHeight();
                var x_contentHeight = bodyHeight - nav_menuHeight - page_titleHeight - x_titleHeight - 80;
                $X_CONTENT.css('height', x_contentHeight);
                $RIGHT_COL.css('min-height', contentHeight);
            };
            setContentHeight();
            function setXContent() {
                // reset height
                $RIGHT_COL.css('min-height', $(window).height());

                var bodyHeight = $BODY.outerHeight();
                // normalize content
                //contentHeight -= $NAV_MENU.height() + footerHeight;
                var nav_menuHeight = $NAV_MENU.outerHeight(),
                    page_titleHeight = $(".page-title").outerHeight(),
                    x_titleHeight = $(".x_title").outerHeight();
                var x_contentHeight = bodyHeight - nav_menuHeight - page_titleHeight - x_titleHeight - 80;

                $X_CONTENT.css('height', x_contentHeight);

            }
            setXContent();
            $SIDEBAR_MENU.find('a').on('click', function (ev) {
                var $li = $(this).parent();

                if ($li.is('.active')) {
                    $li.removeClass('active active-sm');
                    $('ul:first', $li).slideUp(function () {
                        setContentHeight();
                    });
                } else {
                    // prevent closing menu if we are on child menu
                    if (!$li.parent().is('.child_menu')) {
                        $SIDEBAR_MENU.find('li').removeClass('active active-sm');
                        $SIDEBAR_MENU.find('li ul').slideUp();
                    }

                    $li.addClass('active');

                    $('ul:first', $li).slideDown(function () {
                        setContentHeight();
                    });
                }
            });
            $SIDEBAR_MENU.find('a').on('mouseenter', function (ev) {
                var $li = $(this).parent();

                if ($li.is('.active')) {
                    $li.removeClass('active active-sm');
                    $('ul:first', $li).stop().slideUp(function () {
                        setContentHeight();
                    });
                    $(this).parent().parent().find('li').removeClass('active');
                } else {
                    // prevent closing menu if we are on child menu
                    if (!$li.parent().is('.child_menu')) {
                        $SIDEBAR_MENU.find('li').removeClass('active active-sm');
                        $SIDEBAR_MENU.find('li ul').stop().slideUp();
                    }

                    $(this).parent().parent().find('li').removeClass('active');

                    $li.addClass('active');

                    $('ul:first', $li).stop().slideDown(function () {
                        setContentHeight();
                    });
                }
            });

            $("ul.side-menu").on('mouseleave', function (e) {
                $("ul.side-menu > li.active > ul.child_menu").stop().slideUp(function () {
                    setContentHeight();
                });
                $("ul.side-menu li.active").removeClass('active');
            });

            $(document).mouseup(function (e) {
                if (!$SIDEBAR_MENU.is(e.target) // if the target of the click isn't the container...
                    && $SIDEBAR_MENU.has(e.target).length === 0) // ... nor a descendant of the container
                {
                    $("ul.side-menu > li.active > ul.child_menu").slideUp(function () {
                        setContentHeight();
                    });
                    $("ul.side-menu li.active").removeClass('active');
                }
            });

            // check active menu
            var arr = CURRENT_URL.replace("#", "").split('/');
            var controller = arr[4];
           // $SIDEBAR_MENU.find('a[href="#!/' + CURRENT_URL + '"]').parent('li').parent().find('li').removeClass('current-page');
           // $SIDEBAR_MENU.find('a[href="#!/' + CURRENT_URL + '"]').parent('li').addClass('current-page');

            $SIDEBAR_MENU.find('a').filter(function () {
                var href = this.href.split('/')[4];
                if (href != null && href != undefined && href.toLowerCase() == controller.toLowerCase()) {
                    return this;
                }
            }).parent('li').addClass('current-page');
            $SIDEBAR_MENU.find('a').filter(function () {
                var href = this.href.split('/')[4];
                if (href != null && href != undefined && href.toLowerCase() == controller.toLowerCase()) {
                    return this;
                }
            }).parent().parent().parent('li').addClass('current-page');
            // fixed sidebar
            if ($.fn.mCustomScrollbar) {
                $('.menu_fixed').mCustomScrollbar({
                    autoHideScrollbar: true,
                    theme: 'minimal',
                    mouseWheel: { preventDefault: true }
                });
            }
            $('.collapse-link').on('click', function () {
                var $BOX_PANEL = $(this).closest('.x_panel'),
                    $ICON = $(this).find('i'),
                    $BOX_CONTENT = $BOX_PANEL.find('.x_content');

                // fix for some div with hardcoded fix class
                if ($BOX_PANEL.attr('style')) {
                    $BOX_CONTENT.slideToggle(200, function () {
                        $BOX_PANEL.removeAttr('style');
                    });
                } else {
                    $BOX_CONTENT.slideToggle(200);
                    $BOX_PANEL.css('height', 'auto');
                }

                $ICON.toggleClass('fa-chevron-up fa-chevron-down');
            });

            $('.close-link').click(function () {
                var $BOX_PANEL = $(this).closest('.x_panel');

                $BOX_PANEL.remove();
            });

            $('[data-toggle="tooltip"]').tooltip({
                container: 'body'
            });

            if ($(".js-switch")[0]) {
                var elems = Array.prototype.slice.call(document.querySelectorAll('.js-switch'));
                elems.forEach(function (html) {
                    var switchery = new Switchery(html, {
                        color: '#26B99A'
                    });
                });
            }

            if ($("input.flat")[0]) {
                $(document).ready(function () {
                    $('input.flat').iCheck({
                        checkboxClass: 'icheckbox_flat-green',
                        radioClass: 'iradio_flat-green'
                    });
                });
            }

        }
        function initTable() {
            console.log("123123123");
            $('#listContent').find('tbody tr:first').addClass('selected');
            $('table.bulk_action td').on('click', function () {
                // $("table.bulk_action tr").removeClass('selected');
                $(this).parent().parent().find('tr').removeClass('selected');
                $(this).parent().toggleClass('selected');
            });
        }
        return{
            init: init,
            restrict: 'E',
            initTable:initTable
        }
    }
    
})(angular.module('minhvh.common'));