
// 禁用刷新
$(this).bind("keydown", function (e) {
    // 禁用刷新
    if (e.key == "F5" && e.keyCode == 116)
        parent.layer.iframeSrc(parent.iframe, location.href);
    return false;
});