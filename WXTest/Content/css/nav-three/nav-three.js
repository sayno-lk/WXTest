
$(document).ready(function () {
    $('.inactive').click(function () {
        if ($(this).siblings('ul').css('display') == 'none') {
            //alert("a");
            $(this).parent('li').siblings('li').removeClass('inactives');
            $(this).addClass('inactives');
   
            console.log($(this).parents('.menu1 li').siblings('li').find('ul:visible').length);
            if ($(this).parents('.menu1 li').siblings('li').find('ul:visible').length>0) {
                $(this).parents('.menu1 li').siblings('li').find('ul').parent('li').children('a').removeClass('inactives');
                $(this).parents('.menu1 li').siblings('li').find('ul').slideUp(300);
            }
            $(this).siblings('ul').slideDown(300).children('li');
        
        } else {
            //控制自身变成+号
            $(this).removeClass('inactives');
            //控制自身菜单下子菜单隐藏
            $(this).siblings('ul').slideUp(300);
            //控制自身子菜单变成+号
            $(this).siblings('ul').children('li').children('ul').parent('li').children('a').addClass('inactives');
            //控制自身菜单下子菜单隐藏
            $(this).siblings('ul').children('li').children('ul').slideUp(300);

            //控制同级菜单只保持一个是展开的（-号显示）
            $(this).siblings('ul').children('li').children('a').removeClass('inactives');
        }
    })
});
