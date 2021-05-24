const UI = {
    Checkbox: (Label, Name, Value, Event) => {
        const SettingsGeneralRow = document.createElement("div")
        SettingsGeneralRow.classList.add("lol-settings-general-row")

        const Checkbox = document.createElement("lol-uikit-flat-checkbox")
        Checkbox.setAttribute("for", Label)

        const Input = document.createElement("input")
        Input.setAttribute("slot", "input")
        Input.setAttribute("name", Name)
        Input.setAttribute("type", "checkbox")

        Input.checked = Value
        Input.onchange = Event

        const NameLabel = document.createElement("label")
        NameLabel.setAttribute("slot", "label")

        NameLabel.innerText = Label

        SettingsGeneralRow.append(Checkbox)
        Checkbox.append(Input)
        Checkbox.append(NameLabel)

        return SettingsGeneralRow
    },

    Text: Text => {
        const SettingsGeneralRow = document.createElement("div")
        SettingsGeneralRow.classList.add("lol-settings-general-row")

        const Paragraph = document.createElement("p")
        Paragraph.classList.add("lol-settings-window-size-text")

        Paragraph.innerText = Text

        SettingsGeneralRow.append(Paragraph)

        return SettingsGeneralRow
    },

    Button: (Text, Style, Event) => {
        const Button = document.createElement("lol-uikit-flat-button")
        Button.innerText = Text
        Button.onclick = Event
        Style(Button.style)

        return Button
    }
}