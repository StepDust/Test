
// 禁用刷新，数据表格左右切换
$(this).bind("keydown", function (e) {
    // 禁用刷新
    if (e.key == "F5" && e.keyCode == 116) {

        // 去掉id
        var index = location.href.lastIndexOf('#');
        var url = location.href;
        if (index > 0)
            url = location.href.substring(0, index);

        parent.iframe.prop("src", url);
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
            if ($(a).prop("href"))
                parent.iframe.prop("src", $(a).prop("href"));
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

/**
 * 算法模块，ajax提交数据
 * @param {当前按钮} obj
 * @param {请求路径} url
 */
function Algo(obj, url) {

    var val = $(obj).parents(".layui-row").find(".layui-input").eq(0).val();

    if (val.length <= 0) {
        layer.msg("数据为空，停止计算！", { icon: 0 });
        return false;
    }
    var data = { input: val };
    AjaxAlert(url, data);

}

/**
 * ajax返回弹框
 * @param {跳转路径} url
 * @param {数据} data
 */
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

/**
 * 选项卡悬浮
 * @param {选择器} sel
 */
function Float(sel) {
    var element = $(sel);
    if (element.length <= 0)
        return false;
    // 获取当前元素离浏览器顶部距离
    var top = element.position().top - $("body").css("padding-top").replace('px', '');
    // 获取当前元素的定位方式
    var pos = element.css("position");
    var back = "#fff";

    var width = element.width() - 15;
    var height = element.height() + top;
    var copy = element.clone();
    copy.css({
        display: "none",
        opacity: 0
    });
    // 将拷贝标签添加至父级
    $(element).parent().append(copy);

    element.css({
        opacity: 0,
        top: top
    });

    copy.css({
        display: 'block',
        opacity: 100,
        width: "100%",
        "padding-top": top,
        position: "fixed",
        'z-index': "999",
        'background-color': back,
        left: 5,
        top: 0
    });

};

/**
 * 加载层
 * @param {any} time
 */
function Loging(time) {
    if (time == undefined)
        time = 99999999;

    layer.msg('加载中...', {
        time: time
        ,icon: 16
        , shade: 0.5
    });
}