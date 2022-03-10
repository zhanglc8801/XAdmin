(function() {
	'use strict';
	//加载tinymce
	var global = tinymce.util.Tools.resolve('tinymce.PluginManager');

	var blobInfo = {
		file: null
	}
	blobInfo.blob = function() {
		return this.file;
	}
	var videof_id = "videof-" + new Date().getTime();
	var showDialog = function(editor) {
		var videof = {};
		
		var baseURL = tinymce.baseURL;
		var iframe1 = baseURL + '/plugins/videofan/video.html';
		videof.images_upload_base_path = editor.getParam('images_upload_base_path', '', 'string');
		videof.videof_filetype = editor.getParam('videof_filetype', '.mp4', 'string');

		editor.videof = videof;
		var input = document.createElement('input');
		input.id = videof_id;
		input.setAttribute('type', 'file');
		//input.setAttribute('multiple', 'multiple');
		input.setAttribute('accept', videof.videof_filetype);
		input.click();
		input.onchange = function() {
			var files = this.files;
			var fileName = files[0].name;
			var s = fileName.substring(fileName.lastIndexOf(".") + 1)
			console.log(s)
			var type = videof.videof_filetype;
			console.log(type)
			if (type.indexOf(s) > -1) {
				upAllFiles(files[0],editor)
			} else {
				alert('请上传视频');
			}
		}
	}

	function upAllFiles(n,editor) {
		blobInfo.file = n;
		var images_upload_handler = editor.getParam('images_upload_handler', undefined, 'function');
		images_upload_handler(blobInfo, function(url) {
			console.log(url);
			var html = '<video width="300" height="150" controls="controls"><source src="'+url+'" type="video/mp4" /></video>';
			editor.insertContent(html);
		}, function(err) {
			alert(err);
		});
	}

	//注册事件
	var register = function(editor) {
		editor.addCommand('videofan', function() {
			showDialog(editor);
		});
	}

	var register$1 = function(editor) {
		editor.ui.registry.addButton('videofan', {
			icon: 'video',
			tooltip: 'video code',
			onAction: function() {
				return showDialog(editor);
			}
		});
		editor.ui.registry.addMenuItem('videofan', {
			icon: 'video',
			text: 'video code',
			onAction: function() {
				return showDialog(editor);
			}
		});
	}

	function Plugin() {
		global.add('videofan', function(editor) {
			console.log(editor)
			register(editor);
			register$1(editor);
		});
	};
	Plugin();

}())
