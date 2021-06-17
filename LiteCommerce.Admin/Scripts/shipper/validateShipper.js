jQuery.validator.addMethod('valid_phone', function (value) {
    var regex = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;
    return value.trim().match(regex);
});
$("#myform").validate({
    rules: {        
        'ShipperName': {
            required: true,
            minlength: 3,
            maxlength: 250
        },
        'Phone': {
            required: true,
            valid_phone: true
        }
    },
    messages: {
        'ShipperName': {
            required: "Vui lòng nhập thông tin vào đây!",
            minlength: "Độ dài quá ngắn",
            maxlength: "chuỗi quá giới hạn cho phép"
        },
        'Phone': {
            required: "Xin nhập số điện thoại !",
            valid_phone: "Số điện thoại không hợp lệ?",


        }       
    }

});