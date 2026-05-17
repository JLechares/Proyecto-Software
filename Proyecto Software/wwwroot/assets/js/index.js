import { getEvents, getSectors, getSeats, createReservation, processPayment } from "./api.js";
import { createEventCard, injectSeatSelectionTemplate, injectModalTemplate, showToast, getOrCreateCartContainer, createCartItemElement } from "./ui.js";
import { startTimer, stopTimer } from "./timer.js";

let selectedSeats = [];
let currentEvent = null;
let lastReservationId = null;
let alreadyReservedInBackend = [];

document.addEventListener("DOMContentLoaded", async () => {
    const container = document.getElementById("events-container");
    try {
        const events = await getEvents();
        renderEvents(events, container);
    } catch (error) {
        console.error(error);
        container.innerHTML = "<p>Error cargando eventos</p>";
    }
});

function renderEvents(events, container) {
    container.innerHTML = "";
    events.forEach(event => {
        const card = createEventCard(event);
        card.addEventListener("click", () => renderSeatSelection(event));
        container.appendChild(card);
    });
}

function renderSeatSelection(event) {
    const main = document.getElementById("main-content");
    selectedSeats = [];
    alreadyReservedInBackend = [];
    lastReservationId = null;
    currentEvent = event;
    stopTimer();

  
    injectSeatSelectionTemplate(main, event);

    injectModalTemplate(main);

    generateSeats(event.id);

    setTimeout(() => {
        setupConfirmButton();
    }, 50);

    const goBack = () => {
        stopTimer();
        selectedSeats = [];
        currentEvent = null;
        location.reload();
    };

    document.getElementById("btnBack").onclick = goBack;
    document.getElementById("btnClose").onclick = goBack;
}

async function generateSeats(eventId) {
    const container = document.getElementById("sectors-container");
    if (!container) return;

    container.innerHTML = "";
    const sectors = await getSectors(eventId);

    if (!sectors || !Array.isArray(sectors)) {
        console.error("No se pudieron cargar los sectores válidos.");
        return;
    }

    for (const sector of sectors) {
        const sectorDiv = document.createElement("div");
        sectorDiv.innerHTML = `<h4>${sector.name}</h4><table><tbody></tbody></table>`;
        const tbody = sectorDiv.querySelector("tbody");

        const seats = await getSeats(eventId, sector.id);
        const seatsPerRow = 5;

        for (let i = 0; i < seats.length; i += seatsPerRow) {
            const tr = document.createElement("tr");
            const rowSeats = seats.slice(i, i + seatsPerRow);

            rowSeats.forEach(seat => {
                const td = document.createElement("td");
                const seatDiv = document.createElement("div");

                seatDiv.dataset.id = seat.id;
                seatDiv.textContent = seat.name || " ";

                // Estilos fijos
                seatDiv.style.width = "30px";
                seatDiv.style.height = "30px";
                seatDiv.style.borderRadius = "4px";
                seatDiv.style.display = "flex";
                seatDiv.style.justifyContent = "center";
                seatDiv.style.alignItems = "center";
                seatDiv.style.fontSize = "11px";
                seatDiv.style.color = "white";

                const isMineInCart = selectedSeats.includes(seat.id);

                if (seat.status === "Sold") {
                    seatDiv.style.background = "#ff4444";
                    seatDiv.style.cursor = "not-allowed";
                }
                else if (seat.status === "Reserved" && !isMineInCart) {
                    seatDiv.style.background = "#a855f7";
                    seatDiv.style.cursor = "not-allowed";
                }
                else {
                    if (isMineInCart) {
                        seatDiv.style.background = "#22c55e";
                        seatDiv.style.cursor = "not-allowed";
                    } else {
                        seatDiv.style.background = "#444";
                        seatDiv.style.cursor = "pointer";

                        seatDiv.addEventListener("click", () => {
                            if (!selectedSeats.includes(seat.id)) {
                                selectedSeats.push(seat.id);
                                seatDiv.style.background = "#22c55e";
                                lastReservationId = null;
                            }
                        });
                    }
                }

                td.appendChild(seatDiv);
                tr.appendChild(td);
            });
            tbody.appendChild(tr);
        }
        container.appendChild(sectorDiv);
    }
}

function setupConfirmButton() {
    const confirmBtn = document.getElementById("confirmBtn");
    if (!confirmBtn) {
        console.error("No se encontró el botón confirmBtn en el DOM todavía.");
        return;
    }

    confirmBtn.onclick = async () => {
        if (selectedSeats.length === 0) {
            showToast("Por favor, selecciona al menos un asiento para continuar.", "error");
            return;
        }

        let newReservations = [];
        let seatNamesReserved = [];

        for (const id of selectedSeats) {
            if (alreadyReservedInBackend.includes(id)) {
                continue;
            }

            try {
                const response = await createReservation(id);

                if (response.status === 409) {
                    showToast(`La Butaca ${id.toString().substring(0, 3)} ya fue reservada por otro usuario.`, "error");
                    selectedSeats = selectedSeats.filter(seatId => seatId !== id);
                    await generateSeats(currentEvent.id);
                    continue;
                }

                if (response.status === 400) {

                    let errorMsg = "Solicitud inválida o asiento no disponible.";
                    try {
                        const errorData = await response.json();
                        errorMsg = errorData.message || errorData.title || errorMsg;
                    } catch (e) {
                    }

                    showToast(`Error: ${errorMsg}`, "error");

                    selectedSeats = selectedSeats.filter(seatId => seatId !== id);
                    await generateSeats(currentEvent.id);
                    continue; 
                }

                if (response.ok) {
                    const data = await response.json();
                    const resId = data.reservationId || data.id;

                    if (resId) {
                        newReservations.push(resId);
                        seatNamesReserved.push(`Butaca ${id.toString().substring(0, 3)}`);
                    }
                    alreadyReservedInBackend.push(id);
                }
            } catch (error) {
                console.error("Error de red:", error);
            }
        }

        if (newReservations.length > 0) {
            const groupReservationId = newReservations[0];

            const cartContainer = getOrCreateCartContainer();
            const cartItem = createCartItemElement(groupReservationId, seatNamesReserved, "05:00");
            cartContainer.appendChild(cartItem);

            selectedSeats = [];
            await generateSeats(currentEvent.id);
            showToast("Asientos reservados y añadidos al Carrito Flotante.", "success");

            let timeLeft = 300;
            const timerInterval = setInterval(() => {
                timeLeft--;

                const minutes = Math.floor(timeLeft / 60).toString().padStart(2, '0');
                const seconds = (timeLeft % 60).toString().padStart(2, '0');
                const timeString = `${minutes}:${seconds}`;

                const timerDisplay = document.getElementById(`cart-timer-${groupReservationId}`);
                if (timerDisplay) {
                    timerDisplay.textContent = `⏱️ ${timeString}`;
                }

                if (timeLeft <= 0) {
                    clearInterval(timerInterval);
                    cartItem.remove();
                    showToast("El tiempo de una de tus reservas expiró y los asientos se liberaron.", "error");

                    alreadyReservedInBackend = alreadyReservedInBackend.filter(id => !newReservations.includes(id));
                    generateSeats(currentEvent.id);
                }
            }, 1000);

            const payCartBtn = cartItem.querySelector(`#btn-pay-cart-${groupReservationId}`);

            if (payCartBtn) {
                payCartBtn.onclick = () => {
                    const modal = document.getElementById("paymentModal");
                    if (modal) modal.style.display = "flex";

                    document.getElementById("btnExecutePayment").onclick = async () => {
                        try {
                            document.getElementById("btnExecutePayment").disabled = true;
                            let allOk = true;

                            for (const resId of newReservations) {
                                const paymentResponse = await processPayment(resId);
                                if (!paymentResponse.ok) {
                                    allOk = false;
                                    console.error(`Error pagando la reserva: ${resId}`);
                                }
                            }

                            if (allOk) {
                                clearInterval(timerInterval); 
                                cartItem.remove(); 

                                if (modal) modal.style.display = "none";
                                showToast("¡Pago procesado con éxito! Tu reserva está firme.", "success");


                                alreadyReservedInBackend = alreadyReservedInBackend.filter(id => !newReservations.includes(id));

                                await generateSeats(currentEvent.id);
                            } else {
                                showToast("Hubo un error al procesar el pago de esta reserva.", "error");
                            }
                        } catch (e) {
                            console.error(e);
                        } finally {
                            document.getElementById("btnExecutePayment").disabled = false;
                        }
                    };

                    document.getElementById("btnCancelPayment").onclick = () => {
                        if (modal) modal.style.display = "none";
                        showToast("Pago pausado. Tu carrito sigue guardado en la esquina.", "info");
                    };
                };
            } else {
                console.error("No se pudo encontrar el botón de pago dentro del elemento del carrito.");
            }
        }
    };
}