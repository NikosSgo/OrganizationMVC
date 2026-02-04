// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function buildPayload(endpoint, data) {
    if (endpoint.includes("/organizations/batch-insert") || endpoint.includes("/organizations/batch-update")) {
        return { organizations: data };
    }
    if (endpoint.includes("/organizations/batch-delete")) {
        return { organizationIds: data };
    }
    if (endpoint.includes("/organizations/employees/batch-insert") || endpoint.includes("/organizations/employees/batch-update")) {
        return { employees: data };
    }
    if (endpoint.includes("/organizations/employees/batch-delete")) {
        return { employeeIds: data };
    }
    return data;
}

async function sendBatch(endpoint, textareaId, resultId) {
    const textarea = document.getElementById(textareaId);
    const result = document.getElementById(resultId);
    if (!textarea || !result) {
        return;
    }

    let data;
    try {
        data = JSON.parse(textarea.value);
    } catch (err) {
        result.textContent = "Invalid JSON: " + err.message;
        result.classList.remove("text-muted");
        result.classList.add("text-danger");
        return;
    }

    const payload = buildPayload(endpoint, data);

    try {
        const response = await fetch(endpoint, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(payload)
        });

        const body = await response.json().catch(() => ({}));
        if (response.ok) {
            result.textContent = body.message || "Success";
            result.classList.remove("text-danger");
            result.classList.add("text-success");
        } else {
            const errorText = body.message || "Request failed";
            const details = body.errors ? " " + body.errors.join(", ") : "";
            result.textContent = errorText + details;
            result.classList.remove("text-success");
            result.classList.add("text-danger");
        }
    } catch (err) {
        result.textContent = "Network error: " + err.message;
        result.classList.remove("text-success");
        result.classList.add("text-danger");
    }
}

document.addEventListener("click", (event) => {
    const target = event.target.closest("[data-batch-btn]");
    if (!target) {
        return;
    }

    const endpoint = target.getAttribute("data-endpoint");
    const textareaId = target.getAttribute("data-textarea");
    const resultId = target.getAttribute("data-result");
    if (!endpoint || !textareaId || !resultId) {
        return;
    }

    sendBatch(endpoint, textareaId, resultId);
});
