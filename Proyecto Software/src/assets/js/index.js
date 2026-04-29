// Seleccionamos la card por su ID
const card = document.getElementById('card-coldplay');

card.addEventListener('click', () => {
    // toggle() añade la clase si no la tiene, y la quita si ya la tiene
    

    // Opcional: Imprimir en consola si está seleccionada
    if (card.classList.contains('selected')) {
        console.log("Card seleccionada");
    }
});

const cardColdplay = document.getElementById('card-coldplay');
const mainContainer = document.getElementById('main-content');

cardColdplay.addEventListener('click', () => {
    // 1. Opcional: Agregar un pequeño retraso o animación antes del cambio
    cardColdplay.classList.add('selected');

    // 2. Insertamos el nuevo contenido en el main
    // Usamos backticks (``) para poder pegar el HTML multilínea fácilmente
    mainContainer.innerHTML = `
      
        <header>
            <nav style="display: flex; justify-content: space-between; align-items: center; padding: 1rem; background: #12141d;">
                <button onclick="window.location.reload()">⇠</button>
                <div>Entradas 05:00</div>
                <button onclick="window.location.reload()">x</button>
            </nav>
        </header>
        <div class="selection-container" style="padding: 20px; text-align: center; color: white;">
            <article>
                <h1 style="font-size: 1.5rem; margin-bottom: 2rem;">SELECCIONÁ TU BUTACA</h1>
            </article>
            <article>
                <h3 style="background: #333; padding: 5px; margin: 0 auto 2rem auto; width: 60%; border-radius: 0 0 50px 50px;">Pantalla</h3>
            </article>
            
            <article class="sector" style="display: flex; justify-content: center; gap: 20px; flex-wrap: wrap;">
                <table border="0" style="border-spacing: 5px;">
                    <tbody>
                        ${generarFilasButacas(4, 6)}
                    </tbody>
                </table>
                <table border="0" style="border-spacing: 5px;">
                    <tbody>
                        ${generarFilasButacas(4, 6)}
                    </tbody>
                </table>
            </article>

            <article class="statusBar" style="display: flex; justify-content: center; gap: 20px; margin-top: 2rem;">
                <div style="display: flex; align-items: center; gap: 5px;">
                    <div style="width: 20px; height: 20px; background: #444; border-radius: 4px;"></div>
                    <span>Disponible</span>
                </div>
                <div style="display: flex; align-items: center; gap: 5px;">
                    <div style="width: 20px; height: 20px; background: #ff4444; border-radius: 4px; display: flex; align-items: center; justify-content: center;">X</div>
                    <span>No Disponible</span>
                </div>
                <div style="display: flex; align-items: center; gap: 5px;">
                    <div style="width: 20px; height: 20px; background: #a855f7; border-radius: 4px;"></div>
                    <span>Seleccionado</span>
                </div>
            </article>

            <footer style="margin-top: 3rem; border-top: 1px solid #333; padding-top: 1rem;">
                <h2>Butacas seleccionadas:</h2>
                <div class="selectedSeats" style="display: flex; justify-content: center; gap: 10px; margin-top: 10px;">
                    <div style="background: #a855f7; padding: 5px 15px; border-radius: 5px;"><span>E9</span></div>
                    <div style="background: #a855f7; padding: 5px 15px; border-radius: 5px;"><span>E8</span></div>
                </div>
            </footer>
        </div>
    `;
});

// Función auxiliar para no repetir tanto código de botones en el JS
function generarFilasButacas(filas, cols) {
    let html = '';
    for (let i = 0; i < filas; i++) {
        html += '<tr>';
        for (let j = 0; j < cols; j++) {
            html += `<td><button type="button" style="width: 35px; height: 35px; background: #444; border: none; border-radius: 5px; cursor: pointer;" onclick="this.style.background='#a855f7'">&nbsp;</button></td>`;
        }
        html += '</tr>';
    }
    return html;
}