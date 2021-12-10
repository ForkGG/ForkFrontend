// Scroll to bottom of the element if autoscroll is enabled
function ScrollToBottom(element) {
    element.scrollTop = element.scrollHeight - element.clientHeight;
    console.log(element)
}

function IsScrolledToBottom(element) {
    return element.scrollHeight - Math.abs(element.scrollTop) === element.clientHeight;
}


