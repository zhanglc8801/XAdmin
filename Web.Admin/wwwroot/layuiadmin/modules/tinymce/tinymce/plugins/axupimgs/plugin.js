(function() {
	'use strict';

	var global = tinymce.util.Tools.resolve('tinymce.PluginManager');


	var openDialog = function(editor) {
		var axupimgs = {}; //扔外部公共变量，也可以扔一个自定义的位置
		
		var baseURL = tinymce.baseURL;
		var iframe1 = baseURL + '/plugins/axupimgs/upfiles.html';
		axupimgs.images_upload_handler = editor.getParam('images_upload_handler', undefined, 'function');
		axupimgs.images_upload_base_path = editor.getParam('images_upload_base_path', '', 'string');
		axupimgs.axupimgs_filetype = editor.getParam('axupimgs_filetype', '.png,.gif,.jpg,.jpeg', 'string');
		axupimgs.res = [];
		editor.axupimgs = axupimgs;
		return editor.windowManager.openUrl({
			title: 'axupimgs code',
			size: 'large',
			url: iframe1,
			buttons: [{
					type: 'cancel',
					text: 'Close'
				},
				{
					type: 'custom',
					text: 'Save',
					name: 'save',
					primary: true
				},
			],
			onAction: function(api, details) {
				switch (details.name) {
					case 'save':
						var html = '';
						var imgs = axupimgs.res;
						var len = imgs.length;
						for (let i = 0; i < len; i++) {
							if (imgs[i].url) {
								html += '<img src="' + imgs[i].url + '" />';
							}
						}
						editor.insertContent(html);
						axupimgs.res = [];
						api.close();
						break;
					default:
						break;
				}

			}
		});
	};

	var register = function(editor){
		editor.addCommand('axupimgs', function () {
		  openDialog(editor);
		});
	}

	var register$1 = function(editor){
		editor.ui.registry.addButton('axupimgs', {
			icon: 'axupimgs',
			tooltip: 'axupimgs code',
			onAction: function() {
				return openDialog(editor);
			}
		});
		editor.ui.registry.addMenuItem('axupimgs', {
			icon: 'axupimgs',
			text: '图片批量上传...',
			onAction: function() {
				return openDialog(editor);
			}
		});
	}


	function Plugin () {
		global.add('axupimgs', function(editor) {
			console.log(editor)
			register(editor);
			register$1(editor);
		});
	};
	
	Plugin();
}())
