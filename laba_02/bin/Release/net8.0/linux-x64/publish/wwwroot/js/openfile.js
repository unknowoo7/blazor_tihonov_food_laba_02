function BlazorDownloadFile(fileName, mimeType, base64Data) {
    const link = document.createElement('a');
    link.href = `data:${mimeType};base64,${base64Data}`;
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

function BlazorOpenFile(filename, content) {

    // Create the <a> element and click on it
    const a = document.createElement("a");
    document.body.appendChild(a);
    a.href = filename;
    a.download = filename;
    a.target = "_self";
    a.click();
}