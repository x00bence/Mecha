const AutoAramBoost = () => {
    fetch(`/lol-login/v1/session/invoke?destination=lcdsServiceProxy&method=call&args=["", "teambuilder-draft", "activateBattleBoostV1", ""]`, { method: "POST" })
}