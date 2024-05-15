export function downloadFile(url, filename) {
    let link = document.createElement('a');
    link.href = url;
    link.download = filename;

    document.body.appendChild(link);

    link.click();

    document.body.removeChild(link);
}