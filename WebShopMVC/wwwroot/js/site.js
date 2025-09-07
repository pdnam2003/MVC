document.addEventListener("DOMContentLoaded", () => {
    document.querySelectorAll(".card-body").forEach(item => {
        item.addEventListener("click", () => {
            alert("Bạn đã chọn: " + item.innerText);
        });
    });
});