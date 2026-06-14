const API_BASE_URL = "https://localhost:7210";

export async function getEvents() {
    const response = await fetch(`${API_BASE_URL}/api/v1/events`);
    if (!response.ok) throw new Error("Error al obtener eventos");
    return await response.json();
}

export async function getSectors(eventId) {
    const response = await fetch(`${API_BASE_URL}/api/v1/events/${eventId}/sectors`);
    return await response.json();
}

export async function getSeats(eventId, sectorId) {
    const response = await fetch(`${API_BASE_URL}/api/v1/events/${eventId}/sectors/${sectorId}/seats`);
    return await response.json();
}

export async function createReservation(seatId) {
    return await fetch(`${API_BASE_URL}/api/v1/reservations`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ userId: 2, seatId })
    });
}

export async function processPayment(reservationId) {
    return await fetch(`${API_BASE_URL}/api/v1/reservations/${reservationId}/payments`, {
        method: "POST",
        headers: { "Content-Type": "application/json" }
    });
}