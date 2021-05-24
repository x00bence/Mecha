const Dodge = {
    Funcs: {
        FixClient: () => {
            const Data = new FormData(), USP = new URLSearchParams()
            Data.append("destination", "gameService")
            Data.append("method", "quitGame")
            Data.append("args", [])

            for (const Pair of Data)
                USP.append(Pair[0], Pair[1]);

            fetch("/lol-login/v1/session/invoke", { method: "POST", body: USP })
            fetch("/lol-lobby/v2/lobby", { method: "DELETE" })
        },

        DodgeMatchmaking: () => {
            fetch("/lol-lobby/v2/lobby", { method: "DELETE" })
            fetch("/lol-lobby/v2/matchmaking/quick-search", {
                method: "POST",
                headers: {
                    "Accept": "application/json",
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({ queueId: 1110 })
            })
        }
    },
    Default: Buttons => {
        Buttons.style.maxWidth = "250px"
        Buttons.style.flexWrap = "wrap"

        const Tool = document.createElement("tool")
        Tool.style.display = "flex"
        Tool.style.flexBasis = "100%"
        Tool.style.alignItems = "center"
        Tool.style.justifyContent = "space-between"
        Tool.style.marginBottom = "5px"

        Tool.prepend(UI.Button("Leave", Style => {
            Style.flexBasis = "46%"
        }, () => Dodge.Funcs.FixClient()))
        Tool.prepend(UI.Button("Dodge", Style => {
            Style.flexBasis = "50%", Style.marginRight = "5px"
        }, () => Dodge.Funcs.DodgeMatchmaking()))

        Buttons.prepend(Tool)
    }
}