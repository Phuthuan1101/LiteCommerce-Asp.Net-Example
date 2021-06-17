
$("#myform").validate({
    rules: {
        'CategoryName': {
            required: true,
            minlength: 3,
            maxlength: 250
        },
        'Description': {
            required: true
        },
     
    },
    messages: {
        'CategoryName': {
            required: "Vui lòng nhập tên đăng nhập!",
            minlength: "Độ dài quá ngắn",
            maxlength: "Quá giới hạn"
        },
        'Description': {
            required: "Vui lòng nhập mô tả!"
        },
       
    }

});