
// 禁用刷新
$(this).bind("keydown", function (e) {
    // 禁用刷新
    if (e.key == "F5" && e.keyCode == 116) {

        // 去掉id
        var index = location.href.lastIndexOf('#');
        var url = location.href;
        if (index > 0)
            url = location.href.substring(0, index);

        parent.layer.iframeSrc(parent.iframe, url);
        return false;
    }

    var curr = $(".default .current");

    if (curr) {
        var a;
        var msg = "";
        // 表格方向键：下一页
        if (e.key == "ArrowRight" && e.keyCode == 39) {
            a = $(curr).next();
            msg = "已是最后一页！";
        }

        // 表格方向键 ：上一页
        if (e.key == "ArrowLeft" && e.keyCode == 37) {
            a = $(curr).prev();
            msg = "已是第一页！";
        }

        if (a) {
            layer.msg($(a).html());
            if ($(a).prop("href"))
                parent.layer.iframeSrc(parent.iframe, $(a).prop("href"));
            else
                layer.msg(msg, {
                    icon: 5,
                    anim: 6,
                    time:2000
                });
        }


    }

});