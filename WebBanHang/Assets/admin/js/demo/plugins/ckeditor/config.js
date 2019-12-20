/**
 * @license Copyright (c) 2003-2019, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
	// config.uiColor = '#AADC6E';
    //config.extraPlugins = 'syntaxhighlight';
    config.syntaxhighlight_lang = 'csharp';
    config.syntaxhighlight_hideControls = true;
    config.language = 'vi';
    config.filebrowserBrowseUrl = '/Assets/admin/js/demo/plugins/ckfinder/ckfinder.html';

    config.filebrowserImageBrowseUrl = '/Assets/admin/js/demo/plugins/ckfinder/ckfinder.html?type=Images';

    config.filebrowserFlashBrowseUrl = '/Assets/admin/js/demo/plugins/ckfinder/ckfinder.html?type=Flash';

    config.filebrowserUploadUrl = '/Assets/admin/js/demo/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';

    config.filebrowserImageUploadUrl = '/Data';

    config.filebrowserFlashUploadUrl = '/Assets/admin/js/demo/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';
    CKFinder.setupCKEditor(null,'/Assets/admin/js/demo/plugins/ckfinder/')
};
