document.addEventListener("DOMContentLoaded", async () => {
    const container = document.getElementById("events-container");

    try {
        const response = await fetch("/api/Events/v1/events");

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

    main.innerHTML = `
        <div class="selection-container" style="padding: 20px; text-align: center; color: white;">
            
            <article>
                <h1 style="font-size: 1.5rem; margin-bottom: 1rem;">
                    ${event.name}
                </h1>
                <p>${event.venue}</p>
            </article>

            <article>
                <h3 style="background: #333; padding: 5px; margin: 0 auto 2rem auto; width: 60%; border-radius: 0 0 50px 50px;">
                    Pantalla
                </h3>
            </article>

            <article class="sector" style="display: flex; justify-content: center; gap: 20px; flex-wrap: wrap;">
                <table style="border-spacing: 5px;"><tbody></tbody></table>
                <table style="border-spacing: 5px;"><tbody></tbody></table>
                <table style="border-spacing: 5px;"><tbody></tbody></table>
            </article>

            <article class="statusBar" style="display: flex; justify-content: center; gap: 20px; margin-top: 2rem;">
                <div style="display: flex; align-items: center; gap: 5px;">
                    <div style="width: 20px; height: 20px; background: #444; border-radius: 4px;"></div>
                    <span>Disponible</span>
                </div>
                <div style="display: flex; align-items: center; gap: 5px;">
                    <div style="width: 20px; height: 20px; background: #ff4444; border-radius: 4px;"></div>
                    <span>No Disponible</span>
                </div>
                <div style="display: flex; align-items: center; gap: 5px;">
                    <div style="width: 20px; height: 20px; background: #a855f7; border-radius: 4px;"></div>
                    <span>Seleccionado</span>
                </div>
            </article>

            <div style="margin-top: 3rem; border-top: 1px solid #333; padding-top: 1.5rem; text-align: center;">
    
        <button id="confirmBtn" style="
            background: linear-gradient(135deg, #a855f7, #7c3aed);
            color: white;
            border: none;
            padding: 12px 30px;
            font-size: 1rem;
            border-radius: 30px;
            cursor: pointer;
            transition: all 0.2s ease;
            box-shadow: 0 5px 15px rgba(168,85,247,0.4);
        ">
            Confirmar compra
        </button>

</div>

        </div>
    `;
    generateSeats();
}
function generateSeats() {
    const tables = document.querySelectorAll(".sector table tbody");
    const selectedSeatsContainer = document.querySelector(".selectedSeats");

    const rows = 10;
    const cols = 3;
    const rowLetters = "ABCDEFGHIJ";

    let selectedSeats = [];

    tables.forEach((tbody, sectorIndex) => {
        tbody.innerHTML = "";

        for (let i = 0; i < rows; i++) {
            const tr = document.createElement("tr");

            for (let j = 0; j < cols; j++) {
                const td = document.createElement("td");

                const seat = document.createElement("div");
                const seatLabel = `${rowLetters[i]}${j + 1 + (sectorIndex * cols)}`;

                seat.textContent = seatLabel;
                seat.style.width = "30px";
                seat.style.height = "30px";
                seat.style.background = "#444";
                seat.style.borderRadius = "4px";
                seat.style.display = "flex";
                seat.style.alignItems = "center";
                seat.style.justifyContent = "center";
                seat.style.cursor = "pointer";
                seat.style.fontSize = "12px";

                // CLICK
                seat.addEventListener("click", () => {
                    if (seat.classList.contains("occupied")) return;

                    if (seat.classList.contains("selected")) {
                        seat.classList.remove("selected");
                        seat.style.background = "#444";
                        selectedSeats = selectedSeats.filter(s => s !== seatLabel);
                    } else {
                        seat.classList.add("selected");
                        seat.style.background = "#a855f7";
                        selectedSeats.push(seatLabel);
                    }

                    renderSelectedSeats(selectedSeats, selectedSeatsContainer);
                });

                // Simular algunos ocupados
                if (Math.random() < 0.2) {
                    seat.classList.add("occupied");
                    seat.style.background = "#ff4444";
                    seat.textContent = "X";
                    seat.style.cursor = "not-allowed";
                }

                td.appendChild(seat);
                tr.appendChild(td);
            }

            tbody.appendChild(tr);
        }
    });
}