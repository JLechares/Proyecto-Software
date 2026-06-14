let purchaseTimer;

export function startTimer(duration, onTick, onExpire) {
    if (purchaseTimer) clearInterval(purchaseTimer);
    let timer = duration;

    purchaseTimer = setInterval(() => {
        let minutes = parseInt(timer / 60, 10);
        let seconds = parseInt(timer % 60, 10);

        minutes = minutes < 10 ? "0" + minutes : minutes;
        seconds = seconds < 10 ? "0" + seconds : seconds;

        onTick(`${minutes}:${seconds}`);

        if (--timer < 0) {
            clearInterval(purchaseTimer);
            onExpire();
        }
    }, 1000);
}

export function stopTimer() {
    if (purchaseTimer) clearInterval(purchaseTimer);
}