
export function createEventCard(event) {
    const date = new Date(event.date);
    const day = date.getDate();
    const month = date.toLocaleString("es-AR", { month: "short" }).replace(".", "");

    const card = document.createElement("div");
    card.className = "card event-card";
    card.innerHTML = `
        <div class="card-img-container">
            <img src="./assets/img/headerBackground.png" class="card-img-top" alt="${event.name}">
            <div class="date-badge">
                <div class="date-day">${day}</div>
                <div class="date-month">${month}</div>
            </div>
            <div class="check-badge"><i class="bi bi-check-lg"></i></div>
        </div>
        <div class="card-body card-header-body">
            <h5 class="card-title event-title">${event.name}</h5>
            <h6 class="card-subtitle event-subtitle">${event.description ?? ""}</h6>
        </div>
        <div class="card-body card-details-body">
            <div class="detail-item"><i class="bi bi-geo-alt detail-icon"></i><span class="detail-text">${event.venue}</span></div>
            <div class="detail-item"><i class="bi bi-calendar-event detail-icon"></i><span class="detail-text">${date.toLocaleDateString()}</span></div>
            <div class="detail-item"><i class="bi bi-clock detail-icon"></i><span class="detail-text">${date.toLocaleTimeString()}</span></div>
            <div class="category-tag">${event.category ?? "Evento"}</div>
        </div>`;
    return card;
}

export function injectSeatSelectionTemplate(mainContainer, event) {
    mainContainer.innerHTML = `
        <div class="selection-container" style="padding: 20px; text-align: center; color: white;">
            <header style="display: flex; justify-content: space-between; align-items: center; margin-bottom: 2rem;">
                <button id="btnBack" style="background: #222; border: none; color: white; padding: 10px 15px; border-radius: 8px; cursor: pointer;"><i class="bi bi-arrow-left"></i></button>
                <div style="font-weight: bold; font-size: 1.1rem;">Entradas</div>
                <button id="btnClose" style="background: #222; border: none; color: white; padding: 10px 15px; border-radius: 8px; cursor: pointer;"><i class="bi bi-x-lg"></i></button>
            </header>
            <article>
                <h1 style="font-size: 1.5rem; margin-bottom: 1rem;">${event.name}</h1>
                <p>${event.venue}</p>
            </article>
            <article><h3 style="background: #333; padding: 5px; margin: 0 auto 2rem auto; width: 60%; border-radius: 0 0 50px 50px;">Pantalla</h3></article>
            <article id="sectors-container" class="sector" style="display: flex; justify-content: center; gap: 20px; flex-wrap: wrap;"></article>
            <article class="statusBar" style="display: flex; justify-content: center; gap: 20px; margin-top: 2rem;">
                <div style="display: flex; align-items: center; gap: 5px;"><div style="width: 20px; height: 20px; background: #444; border-radius: 4px;"></div><span>Disponible</span></div>
                <div style="display: flex; align-items: center; gap: 5px;"><div style="width: 20px; height: 20px; background: #ff4444; border-radius: 4px;"></div><span>Vendido</span></div>
                <div style="display: flex; align-items: center; gap: 5px;"><div style="width: 20px; height: 20px; background: #a855f7; border-radius: 4px;"></div><span>Reservado</span></div>
            </article>
            <div style="margin-top: 3rem; border-top: 1px solid #333; padding-top: 1.5rem; text-align: center;">
                <button id="confirmBtn" style="background: linear-gradient(135deg, #a855f7, #7c3aed); color: white; border: none; padding: 12px 30px; font-size: 1rem; border-radius: 30px; cursor: pointer; box-shadow: 0 5px 15px rgba(168,85,247,0.4);">Confirmar compra</button>
            </div>
        </div>`;
}


export function injectModalTemplate(mainContainer) {

    const oldModal = document.getElementById("paymentModal");
    if (oldModal) oldModal.remove();

    const modalHtml = document.createElement("div");
    modalHtml.id = "paymentModal";

    modalHtml.style.cssText = `
        display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%;
        background: rgba(0,0,0,0.7); backdrop-filter: blur(5px); z-index: 1000;
        justify-content: center; align-items: center; color: white;
    `;

    modalHtml.innerHTML = `
        <div style="background: #222; padding: 2rem; border-radius: 15px; text-align: center; max-width: 400px; width: 90%; border: 1px solid #333;">
            <i class="bi bi-credit-card-2-front" style="font-size: 3rem; color: #a855f7;"></i>
            <h2 style="margin: 1rem 0;">Pasarela de Pago</h2>
            <p style="color: #aaa; margin-bottom: 2rem;">¿Deseas proceder con el pago de tus asientos reservados?</p>
            
            <div style="display: flex; gap: 1rem; justify-content: center;">
                <button id="btnCancelPayment" style="background: #444; color: white; border: none; padding: 10px 20px; border-radius: 8px; cursor: pointer;">
                    No pagar (Cancelar)
                </button>
                <button id="btnExecutePayment" style="background: linear-gradient(135deg, #a855f7, #7c3aed); color: white; border: none; padding: 10px 20px; border-radius: 8px; cursor: pointer; font-weight: bold;">
                    Pagar ahora
                </button>
            </div>
        </div>
    `;
    mainContainer.appendChild(modalHtml);
}



export function showToast(message, type = 'info') {
 
    let container = document.getElementById("toast-container");
    if (!container) {
        container = document.createElement("div");
        container.id = "toast-container";
        container.style.position = "fixed";
        container.style.top = "20px";
        container.style.right = "20px";
        container.style.zIndex = "99999";
        container.style.display = "flex";
        container.style.flexDirection = "column";
        container.style.gap = "10px";
        document.body.appendChild(container);
    }

    let borderColor = "#a855f7"; 
    let emoji = "⚠️";

    if (type === 'success') {
        borderColor = "#22c55e"; 
        emoji = "✅";
    } else if (type === 'error') {
        borderColor = "#ff4444"; 
        emoji = "❌";
    }


    const toast = document.createElement("div");
    toast.innerHTML = `<span>${emoji} &nbsp; ${message}</span>`;

    toast.style.background = "#1e1e28";
    toast.style.color = "#ffffff";
    toast.style.padding = "16px 24px";
    toast.style.borderRadius = "8px";
    toast.style.border = `2px solid ${borderColor}`; 
    toast.style.boxShadow = "0 10px 30px rgba(0, 0, 0, 0.7)";
    toast.style.fontWeight = "bold";
    toast.style.fontSize = "14px";
    toast.style.minWidth = "280px";
    toast.style.display = "flex";
    toast.style.alignItems = "center";
    toast.style.transition = "all 0.4s ease";
    toast.style.opacity = "0";
    toast.style.transform = "translateX(50px)";

    container.appendChild(toast);

    setTimeout(() => {
        toast.style.opacity = "1";
        toast.style.transform = "translateX(0)";
    }, 10);

    setTimeout(() => {
        toast.style.opacity = "0";
        toast.style.transform = "translateY(-20px)";
        setTimeout(() => {
            toast.remove();
        }, 400);
    }, 4000);
}




export function getOrCreateCartContainer() {
    let cartContainer = document.getElementById("floating-cart-container");
    if (!cartContainer) {
        cartContainer = document.createElement("div");
        cartContainer.id = "floating-cart-container";

        cartContainer.style.position = "fixed";
        cartContainer.style.bottom = "20px";
        cartContainer.style.right = "20px";
        cartContainer.style.zIndex = "9999";
        cartContainer.style.display = "flex";
        cartContainer.style.flexDirection = "column";
        cartContainer.style.gap = "15px";
        cartContainer.style.maxWidth = "350px";

        document.body.appendChild(cartContainer);
    }
    return cartContainer;
}


export function createCartItemElement(reservationId, seatNames, timeStringInitial = "05:00") {
    const cartItem = document.createElement("div");
    cartItem.id = `cart-item-${reservationId}`;

    cartItem.style.background = "rgba(30, 30, 40, 0.95)";
    cartItem.style.border = "1px solid #a855f7";
    cartItem.style.borderRadius = "12px";
    cartItem.style.padding = "15px";
    cartItem.style.boxShadow = "0 10px 25px rgba(0,0,0,0.5)";
    cartItem.style.color = "white";
    cartItem.style.fontFamily = "sans-serif";
    cartItem.style.display = "flex";
    cartItem.style.flexDirection = "column";
    cartItem.style.gap = "8px";

    cartItem.innerHTML = `
        <div style="display: flex; justify-content: space-between; align-items: center; border-bottom: 1px solid #444; padding-bottom: 6px;">
            <span style="font-weight: bold; color: #a855f7;">🛒 Mi Carrito</span>
            <span id="cart-timer-${reservationId}" style="background: #a855f7; padding: 2px 8px; border-radius: 20px; font-weight: bold; font-size: 13px;">
                ⏱️ ${timeStringInitial}
            </span>
        </div>
        <div style="font-size: 13px; color: #ccc;">
            <strong>Asientos:</strong> ${seatNames.join(", ")}
        </div>
        <button id="btn-pay-cart-${reservationId}" style="background: #22c55e; color: white; border: none; padding: 6px; border-radius: 6px; font-weight: bold; cursor: pointer; font-size: 12px; margin-top: 4px;">
            Pagar esta reserva
        </button>
    `;

    return cartItem;
}