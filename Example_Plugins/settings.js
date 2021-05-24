const Settings = (Scrollable) => {
    Scrollable.querySelector("div.lol-settings-general-row").remove()

    Scrollable.prepend(UI.Checkbox("Aram Boost", "aram-boost", Config.AramBoost, Event => Config.AramBoost = Event.target.checked))
    Scrollable.prepend(UI.Checkbox("Auto Accept", "auto-accept", Config.AutoAccept, Event => Config.AutoAccept = Event.target.checked))
    Scrollable.prepend(UI.Text("R3nzTheCodeGOD"))
}