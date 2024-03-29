﻿var acc = {
    init: function () {
        acc.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/Account/ChangStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response)
                {
                    console.log(response);
                    if (response.status == true) {
                        btn.text('True');
                    }
                    else {
                        btn.text('False');
                    }
                }
            });
        });
    }
}
acc.init();