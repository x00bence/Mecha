const AutoAccept = Data => {
    if (Data[2].uri == "/lol-lobby-team-builder/v1/matchmaking") {
        if (Config.AutoAccept && Data[2].data.searchState == "Found")
            fetch("/lol-matchmaking/v1/ready-check/accept", { method: "POST" })
    }
}