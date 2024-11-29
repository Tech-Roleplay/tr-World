// @ts-nocheck
window.addEventListener('keydown', e => {
    if (e.key === 'Escape') {
        if ('alt' in window) {
            alt.emit('adminmenu:Close:Webview')
        } else {
            console.log('Closing Window');
        }
    }
})