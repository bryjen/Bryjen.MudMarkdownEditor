export function getTextAreaInfo(textAreaId) {
    const textArea = document.getElementById(textAreaId);
    const text = textArea ? textArea.value : "";
    console.log({ text: text, startPos: textArea.selectionStart, endPos: textArea.selectionEnd });
    return { text: text, startPos: textArea.selectionStart, endPos: textArea.selectionEnd };
}

export function modifyTextArea(textAreaId, newValue, newSelectionStart, newSelectionEnd) {
    const textArea = document.getElementById(textAreaId);
    
    if (textArea) {
        textArea.value = newValue;
        textArea.selectionStart = newSelectionStart;
        textArea.selectionEnd = newSelectionEnd;
        
        textArea.focus();
    }
}

export function getTextareaHeight(textAreaId) {
    return document.getElementById(textAreaId).offsetHeight;
}