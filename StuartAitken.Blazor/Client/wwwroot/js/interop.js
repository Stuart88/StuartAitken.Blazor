
window.getCvWidth = function () {

    var cvContainer = document.getElementById('cv-container');

    if (cvContainer) {
        setInterval(() => { }, 500);
        return cvContainer.clientWidth;
    }
    else {
        return 0;
    }
}

window.isMobile = function () {
    return window.visualViewport.height > window.visualViewport.width;
}

window.downloadCv = function () {
    var link = document.createElement('a');
    link.href = `/downloads/Stuart-Aitken-CV-Oct-2022-Default.pdf`;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

var quill;

window.prepareEditor = function () {
    var toolbarOptions = [
        ['bold', 'italic', 'underline', 'strike'], // toggled buttons
        ['blockquote', 'code-block'],
        [{ 'header': 1 }, { 'header': 2 }], // custom button values
        [{ 'list': 'ordered' }, { 'list': 'bullet' }],
        [{ 'script': 'sub' }, { 'script': 'super' }], // superscript/subscript
        [{ 'indent': '-1' }, { 'indent': '+1' }], // outdent/indent
        /*[{ 'direction': 'rtl' }], */ // text direction
        [{ 'size': ['small', false, 'large', 'huge'] }], // custom dropdown
        [{ 'header': [1, 2, 3, 4, 5, 6, false] }],
        ['link', 'image' /*, 'video', 'formula'*/], // adds image support
        [{ 'color': [] }, { 'background': [] }], // dropdown with defaults from theme
        //[{ 'font': [] }],
        [{ 'align': [] }],
        ['clean'] // remove formatting button
    ];

    if (quillAreaExists() && !quillEditorExists()) {
        quill = new Quill('#quill-editor',
            {
                modules: {
                    toolbar: toolbarOptions
                },

                theme: 'snow'
            });
    }
}

window.prepareQuillIfNotExist = function () {
    if (!quillAreaExists()) {
        prepareEditor()
        prepareEditor();
    }
}

window.getEditorText = function () {
    if (quillAreaExists()) {
        prepareEditor()
        return quill.root.innerHTML;
    }
    else {
        return '';
    }
}

window.setEditorText = function (text) {
    if (quillAreaExists()) {
        prepareEditor()
        quill.root.innerHTML = text;
    }
}

/** 
 * This checks to ensure the container area exists for initialising the quill editor 
 * */
function quillAreaExists() {
    var quillArea = document.getElementById('quill-editor');
    if (quillArea) {
        return true;
    }
    else {
        return false;
    }
}

/**
 * This checks if there is a quill editor currently existing
 * */
function quillEditorExists() {
    var quillEditor = document.getElementsByClassName('ql-editor')[0];
    if (quillEditor) {
        return true;
    }
    else {
        return false;
    }
}