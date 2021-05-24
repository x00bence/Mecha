const Init = setInterval(() => {
    if (Viewport = document.getElementById("rcp-fe-viewport-root"), Viewport)
        Observable()
}, 500);

const Observable = () => {
    clearInterval(Init)

    new MutationObserver(Event => {
        if (Mutation = Event.find(Record => Array.from(Record.addedNodes).includes(document.querySelector("div.lol-settings-options > lol-uikit-scrollable"))), Mutation)
            Settings(Mutation.addedNodes[0])

        if (Mutation = Event.find(Record => Record.target == document.querySelector("div.rcp-fe-lol-champ-select")), Mutation)
            Dodge.Default(Mutation.target.querySelector("div.bottom-right-buttons"))
    }).observe(document.body, {
        attributes: true,
        childList: true,
        subtree: true,
        characterData: true
    });
}