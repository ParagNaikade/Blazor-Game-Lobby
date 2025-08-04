window.playJoinSound = () => {
    const audio = new Audio("/sounds/game-start.mp3");
    audio.volume = 0.6;
    audio.play();
};

window.playGameOverSound = () => {
    const audio = new Audio("/sounds/game-over.mp3");
    audio.volume = 0.6;
    audio.play();
};

window.savePlayerData = (data) => {
    localStorage.setItem("playerData", JSON.stringify(data));
};

window.getPlayerData = () => {
    const data = localStorage.getItem("playerData");
    return data ? JSON.parse(data) : null;
};
