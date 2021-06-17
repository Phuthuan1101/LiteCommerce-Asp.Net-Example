
function previewImages() {

	var preview = document.querySelector('#previewonly');

	if (this.files) {
		[].forEach.call(this.files, readAndPreview);
	}

	function readAndPreview(file) {

		// Make sure `file.name` matches our extensions criteria
		if (!/\.(jpe?g|png|gif)$/i.test(file.name)) {
			return alert(file.name + " không được hỗ trợ");
		} // else...

		var reader = new FileReader();

		reader.addEventListener("load", function () {
			var image = new Image();
			image.height = 100;
			image.margin = 5;
			image.title = file.name;
			image.src = this.result;
			preview.appendChild(image);
		});

		reader.readAsDataURL(file);

	}

}

document.querySelector('#Photo').addEventListener("change", previewImages);
$("#Photo").change(function () {
	filename = this.files[0].name
	console.log(filename);
});

$("#formproduct").validate({
    rules: {
        'ProductName': {
            required: true
        },
        'Photo': {
            required: true
        },
        'Unit': {
            required: true
        },
        'Price': {
            required: true
        }
    },
    messages: {
        'ProductName': {
            required: "Vui lòng nhập dữ liệu!"
        },
        'Photo': {
            required: "Vui lòng nhập thông tin ở đây!"
        },
        'Unit': {
            required: "Vui lòng nhập thông tin ở đây!"
        },
        'Price': {
            required: "Vui lòng nhập thông tin ở đây!"
        }
    }
});