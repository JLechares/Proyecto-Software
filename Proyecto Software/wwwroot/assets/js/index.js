let selectedSeats = []; 
let purchaseTimer;

document.addEventListener("DOMContentLoaded", async () => {
    const container = document.getElementById("events-container");

    try {
        const response = await fetch("/api/v1/events");
       

        if (!response.ok) {
            throw new Error("Error al obtener eventos");
        }

        const events = await response.json();

        renderEvents(events, container);
    } catch (error) {
        console.error(error);
        container.innerHTML = "<p>Error cargando eventos</p>";
    }
});

async function getSectors(eventId) {
    const response = await fetch(`/api/v1/${eventId}/sectors`);
    return await response.json();
}

async function getSeats(eventId, sectorId) {
    const response = await fetch(`/api/v1/${eventId}/sectors/${sectorId}/seats`);
    return await response.json();
}

function renderEvents(events, container) {
    container.innerHTML = "";

    events.forEach(event => {
        const card = document.createElement("div");
        card.className = "card event-card";

        const date = new Date(event.date);

        const day = date.getDate();

        const month = date.toLocaleString("es-AR", {
            month: "short"
        }).replace(".", "");

        card.innerHTML = `
            <div class="card-img-container">
                <img src="./assets/img/headerBackground.png" class="card-img-top" alt="${event.name}">

                <div class="date-badge">
                    <div class="date-day">${day}</div>
                    <div class="date-month">${month}</div>
                </div>

                <div class="check-badge">
                    <i class="bi bi-check-lg"></i>
                </div>
            </div>

            <div class="card-body card-header-body">
                <h5 class="card-title event-title">${event.name}</h5>
                <h6 class="card-subtitle event-subtitle">${event.description ?? ""}</h6>
            </div>

            <div class="card-body card-details-body">
                <div class="detail-item">
                    <i class="bi bi-geo-alt detail-icon"></i>
                    <span class="detail-text">${event.venue}</span>
                </div>
                <div class="detail-item">
                    <i class="bi bi-calendar-event detail-icon"></i>
                    <span class="detail-text">${date.toLocaleDateString()}</span>
                </div>
                <div class="detail-item">
                    <i class="bi bi-clock detail-icon"></i>
                    <span class="detail-text">${date.toLocaleTimeString()}</span>
                </div>

                <div class="category-tag">
                    ${event.category ?? "Evento"}
                </div>
            </div>
        `;
      
        container.appendChild(card);
        card.addEventListener("click", () => {
            renderSeatSelection(event); 
        });

        
    });
}

function renderSeatSelection(event) {
    const main = document.getElementById("main-content");

    selectedSeats = [];
    if (purchaseTimer) clearInterval(purchaseTimer);

    main.innerHTML = `
        <div class="selection-container" style="padding: 20px; text-align: center; color: white;">
            
            <!-- Header con Navegación y Timer -->
            <header style="display: flex; justify-content: space-between; align-items: center; margin-bottom: 2rem;">
                <button id="btnBack" style="background: #222; border: none; color: white; padding: 10px 15px; border-radius: 8px; cursor: pointer;">
                    <i class="bi bi-arrow-left"></i>
                </button>
                
                <div style="font-weight: bold; font-size: 1.1rem;">
                    Entradas · <span id="timerDisplay">05:00</span>
                </div>

                <button id="btnClose" style="background: #222; border: none; color: white; padding: 10px 15px; border-radius: 8px; cursor: pointer;">
                    <i class="bi bi-x-lg"></i>
                </button>
            </header>

            <article>
                <h1 style="font-size: 1.5rem; margin-bottom: 1rem;">${event.name}</h1>
                <p>${event.venue}</p>
            </article>

            <article>
                <h3 style="background: #333; padding: 5px; margin: 0 auto 2rem auto; width: 60%; border-radius: 0 0 50px 50px;">
                    Pantalla
                </h3>
            </article>

            <article id="sectors-container" class="sector" style="display: flex; justify-content: center; gap: 20px; flex-wrap: wrap;">
                <!-- Aquí se cargan las tablas -->
            </article>

            <article class="statusBar" style="display: flex; justify-content: center; gap: 20px; margin-top: 2rem;">
                <div style="display: flex; align-items: center; gap: 5px;">
                    <div style="width: 20px; height: 20px; background: #444; border-radius: 4px;"></div>
                    <span>Disponible</span>
                </div>
                <div style="display: flex; align-items: center; gap: 5px;">
                    <div style="width: 20px; height: 20px; background: #ff4444; border-radius: 4px;"></div>
                    <span>Vendido</span>
                </div>
                <div style="display: flex; align-items: center; gap: 5px;">
                    <div style="width: 20px; height: 20px; background: #a855f7; border-radius: 4px;"></div>
                    <span>Reservado</span>
                </div>
            </article>

            <div style="margin-top: 3rem; border-top: 1px solid #333; padding-top: 1.5rem; text-align: center;">
                <button id="confirmBtn" style="background: linear-gradient(135deg, #a855f7, #7c3aed); color: white; border: none; padding: 12px 30px; font-size: 1rem; border-radius: 30px; cursor: pointer; transition: all 0.2s ease; box-shadow: 0 5px 15px rgba(168,85,247,0.4);">
                    Confirmar compra
                </button>
            </div>
        </div>
    `;


    startTimer(300); 
    generateSeats(event.id);

    const goBack = () => {
        clearInterval(purchaseTimer);
        selectedSeats = [];
        location.reload(); 
    };

    document.getElementById("btnBack").onclick = goBack;
    document.getElementById("btnClose").onclick = goBack;
}
function startTimer(duration) {
    let timer = duration, minutes, seconds;
    const display = document.getElementById("timerDisplay");

    purchaseTimer = setInterval(() => {
        minutes = parseInt(timer / 60, 10);
        seconds = parseInt(timer % 60, 10);

        minutes = minutes < 10 ? "0" + minutes : minutes;
        seconds = seconds < 10 ? "0" + seconds : seconds;

        display.textContent = minutes + ":" + seconds;

        if (--timer < 0) {
            clearInterval(purchaseTimer);
            alert("El tiempo de reserva ha expirado");
            location.reload(); 
        }
    }, 1000);
}



function renderSelectedSeats(seats, container) {
    container.innerHTML = "";

    seats.forEach(id => {
        const div = document.createElement("div");
        div.textContent = id; 
        container.appendChild(div);
    });
}

async function generateSeats(eventId) {
    const container = document.getElementById("sectors-container");

    if (!container) {
        console.error("❌ No existe #sectors-container");
        return;
    }

    container.innerHTML = "";
    selectedSeats = []; 

    const sectors = await getSectors(eventId);

    for (const sector of sectors) {
        const sectorDiv = document.createElement("div");

        sectorDiv.innerHTML = `
            <h4>${sector.name}</h4>
            <table>
                <tbody></tbody>
            </table>
        `;

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

                seatDiv.style.width = "30px";
                seatDiv.style.height = "30px";
                seatDiv.style.background = "#444";
                seatDiv.style.cursor = "pointer"; 
                seatDiv.style.borderRadius = "4px";

                if (seat.status === "Sold") {
                    seatDiv.style.background = "#ff4444"; 
                    seatDiv.style.cursor = "not-allowed";

                } else if (seat.status === "Reserved") {

                    seatDiv.style.background = "#a855f7"; 
                    seatDiv.style.cursor = "not-allowed";

                } else {
                    seatDiv.style.background = "#444"; 
                    seatDiv.style.cursor = "pointer";

                    seatDiv.addEventListener("click", () => {
                        const seatId = seat.id;

                        if (selectedSeats.includes(seatId)) {
                            selectedSeats = selectedSeats.filter(id => id !== seatId);
                            seatDiv.style.background = "#444";
                        } else {
                            selectedSeats.push(seatId);
                            seatDiv.style.background = "#22c55e"; 
                        }
                    });
                }


                td.appendChild(seatDiv);
                tr.appendChild(td);
            });

            tbody.appendChild(tr);
        }

        container.appendChild(sectorDiv);
    }


    const confirmBtn = document.getElementById("confirmBtn");

    if (confirmBtn) {
        confirmBtn.onclick = async () => {
            if (selectedSeats.length === 0) {
                alert("Selecciona al menos un asiento");
                return;
            }

          
            let successCount = 0;

           
            for (const id of selectedSeats) {
                try {
                    const response = await fetch("/api/v1/reservations", {
                        method: "POST",
                        headers: {
                            "Content-Type": "application/json"
                        },
                       
                        body: JSON.stringify({
                            userId: 2,
                            seatId: id
                        })
                    });

                    if (response.ok) {
                        successCount++;
                    } else {
                        console.error(`Fallo al reservar el asiento: ${id}`);
                    }
                } catch (error) {
                    console.error("Error de red:", error);
                }
            }

            if (successCount === selectedSeats.length) {
                alert("¡Todas tus reservas se realizaron con éxito!");
               
                selectedSeats = [];
                location.reload();
            } else {
                alert(`Se reservaron ${successCount} de ${selectedSeats.length} asientos. Revisa la consola.`);
            }
        };
    }
}