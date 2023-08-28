$(function () {

    var media = $('.type').eq(0).find('.item');
    var mainmedia = media.filter(function () {
        return $(this).css('z-index') !== 'auto';
    });


    var playb = mainmedia.find(".play-mid");

    var video = $('img').eq(mainmedia.index());
    playb.bind('click', nextclick);

    $('.hide-player').bind('click', hideplayer);

    $('.prevb').css('display', 'none');
    //#region slide


    for (let i = 3; i < media.length; i++) {
        media.eq(i).css('display', 'none');
        //media.eq(i).find('video').css('opacity', '0');
        //media.eq(i).find('video').css('transform', 'translateY(-70px)');
    }

    var ind = mainmedia.index();
    for (let i = 0; i < media.length; i++) {
        if (i != ind) {
            media.eq(i).find('p').each(function () {
                $(this).css('display', 'none');
            })
        }
    }

    var next = $('.nextb');
    next.click(function () {
        var ind = mainmedia.next().index();
        $.ajax({
            type: 'GET',
            url: '/MyWorks?handler=NewPhoto',
            data: { index: ind, queue:'main' },
            success: function (data) {
                mainmedia.next().find('img').attr('src', 'data:image/png;base64,' + data.image);
                mainmedia.next().find('#title').text(data.title);
                mainmedia.next().find('.likes').append(data.likes);
                mainmedia.next().find('.views').append(data.views);



                var tempstyle = mainmedia.prev().attr('style');
                mainmedia.next().attr('style', mainmedia.attr('style'));
                mainmedia.attr('style', tempstyle);
                mainmedia.prev().css('display', 'none');

                mainmedia.next().find('img').addClass('add-an');
                var pars = mainmedia.find('p');
                pars.each(function () {
                    $(this).css('display', 'none');
                });
                /*media.eq(mainmedia.index() + 2).find('video').animate({ opacity: 1, transform: "translateY(0)" }, {duration:1, easing:'linear'} );*/
                playb.css('display', 'block');
                mainmedia.next().find('p').each(function () {
                    $(this).css('display', 'block');
                });
                media.eq(mainmedia.index() - 2).css('display', 'none');
                $.ajax({
                    type: 'GET',
                    url: '/MyWorks?handler=NewPhoto',
                    data: { index: ind+1, queue: 'next' },
                    success: function (data) {
                        media.eq(mainmedia.index() + 2).find('img').attr('src', 'data:image/png;base64,' + data);
                        media.eq(mainmedia.index() + 2).css('display', 'block');
                        media.eq(mainmedia.index() + 2).css('marginLeft', '30px');


                        var likes = mainmedia.next().find('.likes').text().length;
                        var views = mainmedia.next().find('.views').text().length;
                        if (views + 1 > likes)
                            mainmedia.next().find('.views i').css('marginRight', '5px');


                        media = $('.type').eq(0).find('.item');
                        mainmedia = media.filter(function () {
                            return $(this).css('z-index') !== 'auto';
                        });
                        playb = mainmedia.find(".play-mid");
                        console.log(mainmedia);
                        video = $('img').eq(mainmedia.index());

                        playb.bind('click', nextclick);

                        $('.hide-player').bind('click', hideplayer);
                        console.log(mainmedia.index());
                        if (mainmedia.index() - 1 <= 0)
                            $('.prevb').css('display', 'none');
                        else
                            $('.prevb').css('display', 'block');
                        if (mainmedia.index() + 2 >= media.length)
                            $('.nextb').css('display', 'none');
                        else
                            $('.nextb').css('display', 'block');
                    }
                });
               

               

            }
        });
       

        
    });

    function nextclick() {
        var repl = $('video').eq(mainmedia.index());
        var ind = mainmedia.index();
        $.ajax({
            type: 'GET',
            url: '/MyWorks?handler=Video',
            data: {index:ind},
            success: function (data) {
                repl.attr('src', 'data:video/mp4;base64,' + data);
                repl.css('display', 'block');
                video.css('display', 'none');
                console.log(video);

                if (video.attr("controls")) {
                    repl.removeAttr("controls")
                } else {
                    repl.attr("controls", "controls");
                }
                playb.css('display', 'none');
                
                $('.item > p').css('marginTop', '10px');
                $('.hide-player').css('display', 'block');
                $('.items').css('padding', '0px 0px 200px 0px');
            }
        });
        
    };


    function hideplayer() {
        $('.item > p').css('marginTop', '-40px');
        video.removeAttribute("controls");
        playb.css('display', 'block');
        $('.hide-player').css('display', 'none');
        $('.items').css('padding', '0px 0px 80px 0px');
        video.pause();
        video.currentTime = 0;
    };


    var prev = $('.prevb');
    prev.click(function () {
        mainmedia = media.filter(function () {
            return $(this).css('z-index') !== 'auto';
        });

        var tempstyle = mainmedia.next().attr('style');
        mainmedia.prev().attr('style', mainmedia.attr('style'));
        mainmedia.attr('style', tempstyle);
        mainmedia.next().css('display', 'none');

        mainmedia.prev().find('video').addClass('add-an');

        var pars = mainmedia.find('p');
        pars.each(function () {
            $(this).css('display', 'none');
        });
        playb.css('display', 'block');
        mainmedia.prev().find('p').each(function () {
            $(this).css('display', 'block');
        });

        media.eq(mainmedia.index() - 2).css('display', 'block');
        media.eq(mainmedia.index() - 2).css('marginLeft', '200px');

        var likes = mainmedia.prev().find('.likes').text().length;
        var views = mainmedia.prev().find('.views').text().length;
        if (views + 1 > likes)
            mainmedia.prev().find('.views i').css('marginRight', '5px');


        media = $('.type').eq(0).find('.item');
        mainmedia = media.filter(function () {
            return $(this).css('z-index') !== 'auto';
        });
        playb = mainmedia.find(".play-mid");
        video = $('video').get(mainmedia.index());

        playb.bind('click', nextclick);

        $('.hide-player').bind('click', hideplayer);
        if (mainmedia.index() - 1 <= 0)
            $('.prevb').css('display', 'none');
        else
            $('.prevb').css('display', 'block');
        if (mainmedia.index() + 2 >= media.length)
            $('.nextb').css('display', 'none');
        else
            $('.nextb').css('display', 'block');
    });
    //#endregion

});