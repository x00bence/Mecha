const Config = {
    AutoAccept: false,
    AramBoost: false,

    Socket: {
        URI: document.querySelector(`link[rel="riot:plugins:websocket"]`).href
    }
}

const Socket = new WebSocket(Config.Socket.URI, "wamp")

Socket.onopen = () => Socket.send(JSON.stringify([5, "OnJsonApiEvent"]))
Socket.onmessage = async Message => {
    const Data = JSON.parse(Message.data);

    AutoAccept(Data);

    if (Data[2].uri == "/lol-gameflow/v1/gameflow-phase") {
        if (Config.AramBoost && Data[2].data == "ChampSelect") {
            AutoAramBoost();
            Dodge();
        }
    }

    if (Data[2].eventType == "Update" && Data[2].uri == "/lol-gameflow/v1/session") {
        if (Data[2].data.gameData.queue.id == 1110 && !Data[2].data.gameClient.running) {
            fetch("/lol-end-of-game/v1/state/dismiss-stats", { method: "POST" })
        }
    }
}