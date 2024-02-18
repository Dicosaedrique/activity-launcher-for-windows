
// use highlight.js library to get the highlighted code snippet for a powershell script
function getHighlightedPowerShellScript(script) {
    try {
        return hljs.highlight(script, { language: 'powershell' }).value;
    }
    catch {
        return script;
    }
}

window.blazorInterop = {
    getHighlightedPowerShellScript,
};