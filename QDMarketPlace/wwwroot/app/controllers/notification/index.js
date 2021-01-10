var notificationController = function () {
    this.initialize = function () {
        registerEvents();
    }

    var registerEvents = function () {
        $('#frmSend').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'en',
            rules: {
                subject: {
                    required: true
                },
                content: {
                    required: true
                }
            }
        });
        $('#btnSend').on('click', function (e) {
            if ($('#frmSend').valid()) {
                e.preventDefault();
                var subject = $('#txtSubject').val();
                var body = $('#txtContent').val();
                send(subject, body);
            }

        });
    }

    var send = function (subject, body) {
        $.ajax({
            type: 'POST',
            data: {
                subject: subject,
                body: body
            },
            dateType: 'json',
            url: '/admin/notification/Send',
            success: function (res) {
                if (res != null) {
                    tedu.notify('Send successed', 'success');
                }
                else {
                    tedu.notify('Send failed', 'error');
                }
            }
        })
    }
}