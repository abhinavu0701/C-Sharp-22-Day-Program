// ===================================================
// Hospital Bed Availability Dashboard
// ===================================================

// -----------------------------
// Bed Data (Mock Backend Data)
// -----------------------------
let beds = [
    { bedNumber: 1, isOccupied: false },
    { bedNumber: 2, isOccupied: true },
    { bedNumber: 3, isOccupied: false },
    { bedNumber: 4, isOccupied: true },
    { bedNumber: 5, isOccupied: false },
    { bedNumber: 6, isOccupied: false },
    { bedNumber: 7, isOccupied: true },
    { bedNumber: 8, isOccupied: false },
    { bedNumber: 9, isOccupied: true },
    { bedNumber: 10, isOccupied: false },
    { bedNumber: 11, isOccupied: false },
    { bedNumber: 12, isOccupied: true }
];

// -----------------------------
// Render Beds Function
// -----------------------------
function renderBeds() {

    let container = document.getElementById("bedContainer");

    // Clear existing beds
    container.innerHTML = "";

    let occupiedCount = 0;

    // Loop through all beds
    for (let i = 0; i < beds.length; i++) {

        let bed = beds[i];

        // Create bed element
        let bedDiv = document.createElement("div");

        // Common class
        bedDiv.classList.add("bed");

        // Occupied Bed
        if (bed.isOccupied) {

            occupiedCount++;

            bedDiv.classList.add("occupied");
            bedDiv.innerHTML =
                `<strong>Bed ${bed.bedNumber}</strong><br>Occupied`;

            // Disable clicking
            bedDiv.style.cursor = "not-allowed";

        }
        // Available Bed
        else {

            bedDiv.classList.add("available");
            bedDiv.innerHTML =
                `<strong>Bed ${bed.bedNumber}</strong><br>Available`;

            // Click only available beds
            bedDiv.onclick = function () {

                bed.isOccupied = true;

                // Refresh dashboard
                renderBeds();
            };
        }

        container.appendChild(bedDiv);
    }

    // Calculate counts
    let availableCount = beds.length - occupiedCount;

    // Update dashboard statistics
    document.getElementById("bedStats").innerText =
        `Total Beds: ${beds.length} | Occupied: ${occupiedCount} | Available: ${availableCount}`;
}

// -----------------------------
// Initial Page Load
// -----------------------------
renderBeds();