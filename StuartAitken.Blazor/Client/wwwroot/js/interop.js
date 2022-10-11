
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