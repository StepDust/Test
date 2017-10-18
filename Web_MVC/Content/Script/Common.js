
// 禁用刷新，数据表格左右切换
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

    if (curr.length > 0) {
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
                    time: 2000
                });
        }


    }

});

// 锚点平滑滚动
function GetMao(id) {
    var mao = $("#" + id);
    // 判断对象是否存在
    if (mao.length > 0) {
        // 获取对象相对当前窗口的偏移
        var pos = mao.offset().top;
        // 移动滚动条
        $("html,body").stop().animate({ scrollTop: pos - 5 }, 500);
    }
}

// 算法模块，ajax提交数据
function Algo(obj, url) {

    var val = $(obj).parents(".layui-row").find(".layui-input").eq(0).val();

    if (val.length <= 0) {
        layer.msg("数据为空，停止计算！", { icon: 0 });
        return false;
    }
    var data = { input: val };
    AjaxAlert(url, data);

}

// ajax返回弹框
function AjaxAlert(url, data) {
    $.ajax({
        url: url,
        type: 'Post',
        data: data,
        dataType: 'json',
        success: function (res) {
            layer.open({
                shade: false,
                resize: false,
                title: res.layer_Title,
                content: res.Msg,
                icon: res.layer_Icon
            });
        },
        error: function (res) {
            layer.msg("错误信息：" + res.status + "，" + res.statusText, {
                icon: 0,
                resize: false
            });
        }
    });
}